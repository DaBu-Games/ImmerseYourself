using Godot;
using System;
using WiimoteLib; 
using System.Text.RegularExpressions;
using System.Threading;

public partial class ScaleInput : Node
{
    private Wiimote wiiDevice;
    private float offsetTL = 0f;   // Top Left
    private float offsetTR = 0f;   // Top Right
    private float offsetBL = 0f;   // Bottom Left
    private float offsetBR = 0f;   // Bottom Right

    private float baseWeightOffset = 0f;
    private float previousWeight = 0f;
    
    private float totalLeft = 0f;
    private float totalRight = 0f;
    private float totalWeight = 0f;
    
    private bool isHandlerAttached = false;
    
    public override void _Ready()
    {
        GD.Print("=== Wii Fit Board Connection Test ===");
        
        if (!Attempt_Connect())
        {
            GD.Print("Failed to connect to Wii Fit Board.");
            return;
        }
        
        GD.Print("✅ Connected! Reading weight data...");
    }

    public override void _ExitTree()
    {
        wiiDevice.Disconnect();
        GD.Print("Disconnected.");
    }

    // for testing
    private bool isSpaceHeld = false;
    public override void _Input(InputEvent @event)
    {
        if (@event.IsActionPressed("ui_accept"))
        {
            if (!isSpaceHeld)
            {
                isSpaceHeld = true;
                UpdateInput(true);
            }
        }
        
        if (@event.IsActionReleased("ui_accept"))
        {
            if (isSpaceHeld)
            {
                isSpaceHeld = false;
                UpdateInput(false);
            }
        }
    }

    public void UpdateInput(bool update)
    {
        if(wiiDevice == null)
            return;
        
        if (update && !isHandlerAttached)
        {
            wiiDevice.WiimoteChanged += OnWiimoteChanged;
            isHandlerAttached = true;
        }
        else if(!update && isHandlerAttached)
        {
            wiiDevice.WiimoteChanged -= OnWiimoteChanged;
            isHandlerAttached = false;
        }
    }

    public Vector2 GetInput()
    {
        return new Vector2(totalLeft, totalRight);
    }

    public float GetTotalWeight()
    {
        return totalWeight;
    }

    // Try to find and connect to a Wii Fit Board
    private bool Attempt_Connect()
    {
        try
        {
            var deviceCollection = new WiimoteCollection();
            deviceCollection.FindAllWiimotes();

            if (deviceCollection.Count == 0)
            {
                GD.Print("No Wii devices found. Make sure the board is paired and active.");
                return false;
            }

            if (deviceCollection.Count > 1)
            {
                GD.Print("To many devices found");
            }
            
            wiiDevice = deviceCollection[0];
            wiiDevice.Connect();
            

            if (wiiDevice.WiimoteState.ExtensionType != ExtensionType.BalanceBoard)
            {
                GD.Print("This device is not a Balance Board.");
            }
            else
            {
                GD.Print("Connected to Wii Fit Board.");
            }
            
            wiiDevice.SetReportType(InputReport.IRAccel, true);
            wiiDevice.SetLEDs(true, false, false, false);

            // Listen for updates
            

            // Start thread to keep processing updates
            new Thread(ThreadTick).Start();
            return true;
        }
        catch (Exception ex)
        {
            GD.Print("Error connecting to Wii Fit Board:\n" + ex);
            return false;
        }
    }

    // Thread loop to keep device alive
    private void ThreadTick()
    {
        while (true)
        {
            try
            {
                wiiDevice.GetStatus(); // request update
                Thread.Sleep(100);     // adjust rate as needed
            }
            catch
            {
                break; // exit if disconnected
            }
        }
    }

    private float GetEverage(float total, float added)
    {
        return total != 0f ? (total + added) / 2f : added;
    }

    // Called when WiimoteLib detects a state change
    private void OnWiimoteChanged(object sender, WiimoteChangedEventArgs e)
    {
        var bb = e.WiimoteState.BalanceBoardState;
        float total = bb.WeightKg;

        // Only process if the difference is greater than 0.1 kg
        if (Math.Abs(total - previousWeight) <= 0.225f)
            return;
        
        previousWeight = total;
        
        //left
        float tl = bb.SensorValuesKg.TopLeft;
        float bl = bb.SensorValuesKg.BottomLeft;
        
        //right
        float tr = bb.SensorValuesKg.TopRight;
        float br = bb.SensorValuesKg.BottomRight;
        
        // recalibrate because it means there is nothing on the board
        if (total < 1.5f)
        {
            offsetTL = GetEverage(offsetTL, tl);
            offsetBL = GetEverage(offsetBL, bl);
            
            offsetTR = GetEverage(offsetTR, tr);
            offsetBR = GetEverage(offsetBR, br);
            
            baseWeightOffset = GetEverage(baseWeightOffset, total);
        }

        // Clamp negative values to 0
        tl -= offsetTL;
        tl = Math.Max(0, tl);
        
        bl -= offsetBL;
        bl = Math.Max(0, bl);
        
        tr -= offsetTR;
        tr = Math.Max(0, tr);
        
        br -= offsetBR;
        br = Math.Max(0, br);
        
        total -= baseWeightOffset;
        total = Math.Max(0, total);

        // Compute left/right weights
        float left  = tl + bl;
        float right = tr + br;

        // Compute total weight
        float totalRaw = left + right;
        
        float leftPerc  = totalRaw > 0 ? left / totalRaw : 0f;
        float rightPerc = totalRaw > 0 ? right / totalRaw : 0f;
        
        totalLeft = total * leftPerc;
        totalRight = total * rightPerc;
        totalWeight = total; 
        
        
        GD.Print("=== Wii Fit Board Data ===");
        GD.Print($"balance: ");
        GD.Print($"lef percentage: {leftPerc}");
        GD.Print($"---------------------------");
        GD.Print($"Left:  {totalLeft} kg");
        GD.Print($"Right: {totalRight} kg");
        GD.Print($"---------------------------");
        GD.Print($"Total: {bb.WeightKg:F2} kg");
        GD.Print($"Total Weight: {total} kg");
    }
}
