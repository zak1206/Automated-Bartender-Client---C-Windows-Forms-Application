using Fusion_Bartender_1._0._0;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Fusion_Bartender_1._0
{
    public class Arduino
    {
        FormMainScreen _frmMain = null;
        public SerialPort Serial = null;
        Settings Settings = null;
        Logs Logs = null;
        BackgroundWorker _bgSendCommand = null;

        #region Status Properties
        Recipe recipe { get; set; } = null;
        public DateTime timeSinceLastConnectionUpdate { get; set; }
        public Boolean Reconnecting { get; set; }
        public Boolean SilenceLogs { get; set; }
        public Boolean SendingCommand { get; set; }
        public String CMDToSend { get; set; }
        public Boolean IsConnected { get; set; } = false;
        public Boolean FridgeDoorOpen { get; set; } = false;
        public Boolean Pump1Active { get; set; } = false;
        public Boolean Pump2Active { get; set; } = false;
        public Boolean Pump3Active { get; set; } = false;
        public Boolean Pump4Active { get; set; } = false;
        public Boolean Pump5Active { get; set; } = false;
        public Boolean Pump6Active { get; set; } = false;
        public Boolean Pump7Active { get; set; } = false;
        public Boolean Pump8Active { get; set; } = false;
        public Boolean RecipeRunning { get; set; } = false;
        public Boolean RecipeComplete { get; set; } = false;
        public Boolean RecipeStarted { get; set; } = false;
        public String RecipeMessage { get; set; } = "";
        public String RecipeRunningName { get; set; } = "";
        public String RecipePump1Liquid { get; set; } = "";
        public int RecipePump1TimeToRun { get; set; } = -1;//ms
        public double RecipePump1TimeRemaining { get; set; } = -1;//sec
        public int RecipeRunningDrinkCount { get; set; } = 0;
        public String RecipePump2Liquid { get; set; } = "";
        public int RecipePump2TimeToRun { get; set; } = -1;//ms
        public double RecipePump2TimeRemaining { get; set; } = -1;//sec
        public String RecipePump3Liquid { get; set; } = "";
        public int RecipePump3TimeToRun { get; set; } = -1;//ms
        public double RecipePump3TimeRemaining { get; set; } = -1;//sec
        public String RecipePump4Liquid { get; set; } = "";
        public int RecipePump4TimeToRun { get; set; } = -1;//ms
        public double RecipePump4TimeRemaining { get; set; } = -1;//sec
        public String RecipePump5Liquid { get; set; } = "";
        public int RecipePump5TimeToRun { get; set; } = -1;//ms
        public String RecipePump6Liquid { get; set; } = "";
        public int RecipePump6TimeToRun { get; set; } = -1;//ms
        public String RecipePump7Liquid { get; set; } = "";
        public int RecipePump7TimeToRun { get; set; } = -1;//ms
        public String RecipePump8Liquid { get; set; } = "";
        public int RecipePump8TimeToRun { get; set; } = -1;//ms
        public Double ElectricalBoxTemperature { get; set; } = 0;
        public Double ElectricalBoxHumidity { get; set; } = 0;
        public String LastMsgReceived { get; set; } = "";
        public Double Temperature { get; set; } = 0;
        public Double Humidity { get; set; } = 0;
        public Boolean SilenceArduinoData { get; set; } = false;
        public BackgroundWorker thread = new BackgroundWorker();
        #endregion

        public Arduino(FormMainScreen frmMain, String Port, Settings settings, Logs logs)
        {
            _frmMain = frmMain;
            Settings = settings;
            Logs = logs;
            _bgSendCommand = new BackgroundWorker();
            _bgSendCommand.DoWork += SendCommand_DoWork;
            _bgSendCommand.RunWorkerCompleted += SendCommand_RunWorkerCompleted;
            Connect();                  
        }

        public Boolean Connect()
        {
            Boolean bRetVal = true;

            Serial = new SerialPort(/*Settings.ArduinoCOMPort*/"COM4");
            Serial.BaudRate = 115200;
            Serial.DtrEnable = true; // Used For Pico In Order To Communicate Over Serial.
            Serial.DataReceived += SerialPort_DataReceived;
            try { Serial.Open(); } catch (Exception ex) { }
            bRetVal = Serial.IsOpen;
            IsConnected = bRetVal;

            return bRetVal;
        }

        public Boolean Disconnect()
        {
            Boolean bRetVal = true;

            Serial.Close();
            Serial.Dispose();
            bRetVal = !Serial.IsOpen;

            return bRetVal;
        }

        private void SendStringSerial(String cmd)
        {
            Serial.WriteLine(cmd);
        }

        private void SendCommand_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            SendingCommand = false;
        }

        private void StartSendCommand()
        {
            while (!_bgSendCommand.IsBusy) { Thread.Sleep(100); }
            _bgSendCommand.RunWorkerAsync();
        }

        int drinkCount = 0;
        public void SetRecipeParams(Recipe _recipe)
        {
            string pump1Liq = "NONE";
            int pump1TimeToRun = 0;
            string pump2Liq = "NONE";
            int pump2TimeToRun = 0;
            string pump3Liq = "NONE";
            int pump3TimeToRun = 0;
            string pump4Liq = "NONE";
            int pump4TimeToRun = 0;
            recipe = _recipe;

            RecipeMessage = "Pouring Your " + recipe.RecipeName;

            foreach (Drink drink in recipe.Ingredients)
            {
                //Get Drink Count
                if (drink.PumpID != drinkCount && (drink.PumpID > drinkCount))
                {
                    drinkCount = drink.PumpID;
                }

                if (drink.PumpID == 1)
                {
                    pump1Liq = drink.LiquidName;
                    pump1TimeToRun = drink.DispenseTime;
                } 
                else if (drink.PumpID == 2)
                {
                    pump2Liq = drink.LiquidName;
                    pump2TimeToRun = drink.DispenseTime;
                }
                else if (drink.PumpID == 3)
                {
                    pump3Liq = drink.LiquidName;
                    pump3TimeToRun = drink.DispenseTime;
                }
                else if (drink.PumpID == 4)
                {
                    pump4Liq = drink.LiquidName;
                    pump4TimeToRun = drink.DispenseTime;
                }
            }

            string msg = $"DL={pump1Liq}:" +
                $"{pump1TimeToRun}:" +
                $"{pump2Liq}:" +
                $"{pump2TimeToRun}:" +
                $"{pump3Liq}:" +
                $"{pump3TimeToRun}:" +
                $"{pump4Liq}:" +
                $"{pump4TimeToRun}:" +
                $"{drinkCount}:" +
                $"{recipe.RecipeName}:" +
                $"{1}"; //Recipe Started = True

            SendCommand(msg);
        }

        private void Thread_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

        }

        bool oneShot = false;
        private void Thread_DoWork(object sender, DoWorkEventArgs e)
        {
            //Wait For Recipe Name
            while (RecipeRunningName != recipe.RecipeName)
            {
                if (!oneShot) { _frmMain.Logs.NewEntry(DateTime.Now, $"Waiting For Recipe Name = {recipe.RecipeName}.", "INFO"); oneShot = true; }
                Thread.Sleep(50);
            }

            //Wait For Ingredients Propeties [Time To Run / Liquid Name / etc.]
            foreach (Drink drink in recipe.Ingredients)
            {
                if (drink.PumpID == 1)
                {
                    drinkCount++;
                    oneShot = false;
                    while (RecipePump1Liquid != drink.LiquidName)
                    {
                        if (!oneShot) { _frmMain.Logs.NewEntry(DateTime.Now, $"Waiting For Pump {drink.PumpID} Liquid Name: {drink.LiquidName}.", "INFO"); oneShot = true; }
                        Thread.Sleep(50);
                    }
                    oneShot = false;
                    while (RecipePump1TimeToRun != drink.DispenseTime)
                    {
                        if (!oneShot) { _frmMain.Logs.NewEntry(DateTime.Now, $"Waiting For Pump {drink.PumpID} Time To Run: {drink.DispenseTime}ms", "INFO"); oneShot = true; }
                        _frmMain.frmHome.pump1time = drink.DispenseTime;
                        Thread.Sleep(50);
                    }
                    oneShot = false;
                }
                else if (drink.PumpID == 2)
                {
                    drinkCount++;
                    while (RecipePump2Liquid != drink.LiquidName)
                    {
                        if (!oneShot) { _frmMain.Logs.NewEntry(DateTime.Now, $"Waiting For Pump {drink.PumpID} Liquid Name: {drink.LiquidName}.", "INFO"); oneShot = true; }
                        Thread.Sleep(50);
                    }
                    oneShot = false;
                    while (RecipePump2TimeToRun != drink.DispenseTime)
                    {
                        if (!oneShot) { _frmMain.Logs.NewEntry(DateTime.Now, $"Waiting For Pump {drink.PumpID} Time To Run: {drink.DispenseTime}ms", "INFO"); oneShot = true; }
                        _frmMain.frmHome.pump2time = drink.DispenseTime;
                        Thread.Sleep(50);
                    }
                    oneShot = false;
                }
                else if (drink.PumpID == 3)
                {
                    drinkCount++;
                    while (RecipePump3Liquid != drink.LiquidName)
                    {
                        if (!oneShot) { _frmMain.Logs.NewEntry(DateTime.Now, $"Waiting For Pump {drink.PumpID} Liquid Name: {drink.LiquidName}.", "INFO"); oneShot = true; }
                        Thread.Sleep(50);
                    }
                    oneShot = false;
                    while (RecipePump3TimeToRun != drink.DispenseTime)
                    {
                        if (!oneShot) { _frmMain.Logs.NewEntry(DateTime.Now, $"Waiting For Pump {drink.PumpID} Time To Run: {drink.DispenseTime}ms", "INFO"); oneShot = true; }
                        _frmMain.frmHome.pump3time = drink.DispenseTime;
                        Thread.Sleep(50);
                    }
                    oneShot = false;
                } else if (drink.PumpID == 4)
                {
                    drinkCount++;
                    while (RecipePump4Liquid != drink.LiquidName)
                    {
                        if (!oneShot) { _frmMain.Logs.NewEntry(DateTime.Now, $"Waiting For Pump {drink.PumpID} Liquid Name: {drink.LiquidName}.", "INFO"); oneShot = true; }
                        Thread.Sleep(50);
                    }
                    oneShot = false;
                    while (RecipePump4TimeToRun != drink.DispenseTime)
                    {
                        if (!oneShot) { _frmMain.Logs.NewEntry(DateTime.Now, $"Waiting For Pump {drink.PumpID} Time To Run: {drink.DispenseTime}ms", "INFO"); oneShot = true; }
                        _frmMain.frmHome.pump4time = drink.DispenseTime;
                        Thread.Sleep(50);
                    }
                    oneShot = false;
                }
            }

            //Wait For Drink Count
            oneShot = false;
            while (RecipeRunningDrinkCount < 0)
            {
                if (!oneShot) { _frmMain.Logs.NewEntry(DateTime.Now, $"Waiting To Receive Recipe Drink Count: {drinkCount}...", "INFO"); oneShot = true; }
                Thread.Sleep(50);
            }

            //Wait For Recipe Running
            oneShot = false;
            while (!RecipeRunning)
            {
                if (!oneShot) { _frmMain.Logs.NewEntry(DateTime.Now, $"Waiting To Receive 'recipeRunning=1'...", "INFO"); oneShot = true; }
                Thread.Sleep(50); 
            }
            oneShot = false;
            if (!oneShot) { _frmMain.Logs.NewEntry(DateTime.Now, $"Recipe Running Has Been Received!", "INFO"); oneShot = true; }

            //Wait For Recipe Complete
            oneShot = false;
            while (!RecipeComplete)
            {
                if (!oneShot) { _frmMain.Logs.NewEntry(DateTime.Now, $"Waiting To Receive 'recipeComplete=1'...", "INFO"); oneShot = true; }
                Thread.Sleep(50);
            }
            oneShot = false;
            if (!oneShot) { _frmMain.Logs.NewEntry(DateTime.Now, $"Resetting Variables For Next Recipe...", "INFO"); oneShot = true; }
        }

        public void SendCommand(String cmd)
        {
            if (Serial.IsOpen)
            {
                SendingCommand = true;
                CMDToSend = cmd;
                SendStringSerial(CMDToSend);
                SendingCommand = false;
            }
        }

        private void SendCommand_DoWork(object sender, DoWorkEventArgs e)
        {
        }

        public void StartPouring(int pumpID)
        {
            string msg = $"OUT={(pumpID == 1 ? "1" : "0")}:{(pumpID == 2 ? "1" : "0")}:{(pumpID == 3 ? "1" : "0")}:{(pumpID == 4 ? "1" : "0")}";
            SendCommand(msg);
        }

        public void StopPouring(int pumpID)
        {
            string msg = "OFF=1";
            SendCommand(msg);
        }

        bool ReadingCMD { get; set; } = false;
        public void SerialPort_DataReceived(Object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            String s = Serial.ReadExisting();
            if (!ReadingCMD && s != LastMsgReceived)
            {
                ReadingCMD = true;
                String[] splitted = new String[1000];
                if (s.Contains("\r\n")) splitted = s.Replace("\r\n", "?").Split('?');
                bool stsMsg = splitted[0] != null && splitted[0].Contains("STS");
                bool dataMsg = splitted[0] != null && splitted[0].Contains("DATA");
                bool logMsg = splitted[0] != null && splitted[0].Contains("LOG");

                // ------- STATUS BIT MAPPING
                // STS:{"STS Bit Message From Pico"}
                // [1] Connected Status
                // [2] Pump 1 Running
                // [3] Pump 2 Running
                // [4] Pump 3 Running
                // [5] Pump 4 Running
                // [6] Recipe Started
                // [7] Recipe Running
                // [8] Recipe Complete

                // ------- Data MAPPING
                // DATA:{"Data Message From Pico"}
                // [1] Pump 1 Liquid Name
                // [2] Pump 1 Time To Run
                // [3] Pump 2 Liquid Name
                // [4] Pump 2 Time To Run
                // [5] Pump 3 Liquid Name
                // [6] Pump 3 Time To Run
                // [7] Pump 4 Liquid Name
                // [8] Pump 4 Time To Run
                // [9] Recipe Drink Count
                // [10] Recipe Name
                // [11] Pump 1 Dispense Time Remaining
                // [12] Pump 2 Dispense Time Remaining
                // [13] Pump 3 Dispense Time Remaining
                // [14] Pump 4 Dispense Time Remaining

                // ------- LOG MESSAGE
                // LOG:{"LOG MSG FROM ARDUINO/PICO"}

                //Status Bit Message Received
                if (stsMsg)
                {
                    string[] splittedSTS = s.Split(':');
                    bool connected = splittedSTS[1] == "1";
                    bool pump1Running = splittedSTS[2] == "1";
                    bool pump2Running = splittedSTS[3] == "1";
                    bool pump3Running = splittedSTS[4] == "1";
                    bool pump4Running = splittedSTS[5] == "1";
                    bool recipeStarted = splittedSTS[6] == "1";
                    bool recipeRunning = splittedSTS[7] == "1";
                    bool recipeComplete = splittedSTS[8] == "1";
                    bool doorOpen = splittedSTS[9].Replace("\r\n", "") == "1";

                    //Connection Update Stuff
                    IsConnected = connected || recipeRunning || recipeStarted;
                    Reconnecting = !connected;
                    IsConnected = connected;
                    timeSinceLastConnectionUpdate = DateTime.Now;
                    _frmMain._timerReconnect.Enabled = !connected;

                    //Pump Status
                    Pump1Active = pump1Running;
                    Pump2Active = pump2Running;
                    Pump3Active = pump3Running;
                    Pump4Active = pump4Running;

                    //Recipe Status
                    RecipeStarted = recipeStarted;
                    RecipeRunning = recipeRunning;
                    RecipeComplete = recipeComplete;
                    FridgeDoorOpen = doorOpen;
                }

                //Data Message Received
                if (dataMsg)
                {
                    int nTemp = -1;
                    double dTemp = -1;
                    string[] splittedDATA = s.Split(':');
                    string pump1Liquid = splittedDATA[1];
                    int.TryParse(splittedDATA[2], out nTemp);
                    int pump1TimeToRun = nTemp;
                    string pump2Liquid = splittedDATA[3];
                    int.TryParse(splittedDATA[4], out nTemp);
                    int pump2TimeToRun = nTemp;
                    string pump3Liquid = splittedDATA[5];
                    int.TryParse(splittedDATA[6], out nTemp);
                    int pump3TimeToRun = nTemp;
                    string pump4Liquid = splittedDATA[7];
                    int.TryParse(splittedDATA[8], out nTemp);
                    int pump4TimeToRun = nTemp;
                    int.TryParse(splittedDATA[9], out nTemp);
                    int drinkCount = nTemp;
                    string recipeName = splittedDATA[10];
                    //double.TryParse(splittedDATA[11], out dTemp);
                    //double pump1TimeRemaining = dTemp;
                    //double.TryParse(splittedDATA[12], out dTemp);
                    //double pump2TimeRemaining = dTemp;
                    //double.TryParse(splittedDATA[13], out dTemp);
                    //double pump3TimeRemaining = dTemp;
                    //double.TryParse(splittedDATA[14], out dTemp);
                    //double pump4TimeRemaining = dTemp;

                    //Pump 1 Data
                    RecipePump1Liquid = pump1Liquid;
                    //RecipePump1TimeRemaining = pump1TimeRemaining;
                    RecipePump1TimeToRun = pump1TimeToRun;
                    //Pump 2 Data
                    RecipePump2Liquid = pump2Liquid;
                    //RecipePump2TimeRemaining = pump2TimeRemaining;
                    RecipePump2TimeToRun = pump2TimeToRun;
                    //Pump 3 Data
                    RecipePump3Liquid = pump3Liquid;
                    //RecipePump3TimeRemaining = pump3TimeRemaining;
                    RecipePump3TimeToRun = pump3TimeToRun;
                    //Pump 4 Data
                    RecipePump4Liquid = pump4Liquid;
                    //RecipePump4TimeRemaining = pump4TimeRemaining;
                    RecipePump4TimeToRun = pump4TimeToRun;

                    //Misc Recipe Data
                    RecipeRunningDrinkCount = drinkCount;
                    RecipeRunningName = recipeName;
                    //TODO: Recipe Status MSG
                }

                if (s != "" && s != "\r\n")
                    LastMsgReceived = s;

                if (!SilenceLogs && s != "" && s.Contains("LOG:"))
                {
                    Logs.NewEntry(DateTime.Now, "[ARDUINO] " + s.Replace("LOG:", ""), "ARDUINO");
                    Logs.Save(_frmMain);
                }

                ReadingCMD = false;
            }
        }
    }
}
