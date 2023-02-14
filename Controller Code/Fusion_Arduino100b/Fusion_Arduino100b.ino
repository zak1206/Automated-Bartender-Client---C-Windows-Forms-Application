//#include "hardware/uart.h"
//#include "hardware/irq.h"
//#include <stdio.h>
//#include "stdlib.h"
//#include "stdio.h"
//#include "hardware/gpio.h"
//#include "SoftwareSerial.h"


int pump1PIN = 2;
int pump2PIN = 3;
int pump3PIN = 4;
int pump4PIN = 5;
int pump5PIN = 6;
int pump6PIN = 7;
int pump7PIN = 8;
int pump8PIN = 9;
int doorOpenSensorPIN = 26;
//--------------------- Misc Variables
bool hasTempHumiditySensor = true;
float termperature = 0;
float humidity = 0;
String inData;
String inData2;
//--------------------- Recipe Variables
int drinksCompleted = 0;
bool pump1Running = false;
bool pump2Running = false;
bool pump3Running = false;
bool pump4Running = false;
bool pump5Running = false;
bool pump6Running = false;
bool pump7Running = false;
bool pump8Running = false;
bool pump1Used = false;
bool pump2Used = false;
bool pump3Used = false;
bool pump4Used = false;
bool pump5Used = false;
bool pump6Used = false;
bool pump7Used = false;
bool pump8Used = false;
bool recipeRunning = false;
bool recipeComplete  = false;
bool recipeStarted  = false;
int recipeDrinkCount = -1;
String recipeName = "Not Set";
int pump1TimeToRun = 0;//ms
String pump1Liquid = "Not Set";
int pump2TimeToRun = 0;//ms
String pump2Liquid = "Not Set";
int pump3TimeToRun = 0;//ms
String pump3Liquid = "Not Set";
int pump4TimeToRun = 0;//ms
String pump4Liquid = "Not Set";
int pump5TimeToRun = 0;//ms
String pump5Liquid = "Not Set";
int pump6TimeToRun = 0;//ms
String pump6Liquid = "Not Set";
int pump7TimeToRun = 0;//ms
String pump7Liquid = "Not Set";
int pump8TimeToRun = 0;//ms
String pump8Liquid = "Not Set";

void setup() 
{
  Serial.begin(115200);
  pinMode(pump1PIN, OUTPUT);
  digitalWrite(pump1PIN, HIGH);
  pinMode(pump2PIN, OUTPUT);
  digitalWrite(pump2PIN, HIGH);
  pinMode(pump3PIN, OUTPUT);
  digitalWrite(pump3PIN, HIGH);
  pinMode(pump4PIN, OUTPUT);
  digitalWrite(pump4PIN, HIGH);
  pinMode(pump5PIN, OUTPUT);
  digitalWrite(pump5PIN, HIGH);
  pinMode(pump6PIN, OUTPUT);
  digitalWrite(pump6PIN, HIGH);
  pinMode(pump7PIN, OUTPUT);
  digitalWrite(pump7PIN, HIGH);
  pinMode(pump8PIN, OUTPUT);
  digitalWrite(pump8PIN, HIGH);
  pinMode(25, OUTPUT); //Setup Status LED (ON-BOARD LED);
}

//Core 0 Thread loop - Monitors Incomming Commands
void loop() 
{
  UpdateStatusCore2();
  CheckForCommands();
  delay(50);
}

void setup1(){
  loop1();
}

void loop1(){
  while (1){
    digitalWrite(25, HIGH);
    delay(1000);
    UpdateStatusCore2();
    digitalWrite(25, LOW);
    delay(1000);
  }
}

//Core 1 Thread loop
bool updateStatus = false;
String prevLogMsg = "";
String logMsg = "";
bool oneShot = true;
void UpdateStatusCore2()
{
    Serial.println("2");
    UpdateStatusBits();
    UpdateData();

    //Reset Recipe Complete
    if (recipeComplete && !recipeStarted && !recipeRunning)
      recipeComplete = false;
    
    delay(100);
}

//Send Status Bits Through Serial Port
void UpdateStatusBits()
{
  char msgToSend[1024];
  String msg = "";
  String doorOpen = analogRead(doorOpenSensorPIN) > 10 ? "1" : "0";

  // ------- BIT MAPPING
  // [0] Connected Status
  // [1] Pump 1 Running
  // [2] Pump 2 Running
  // [3] Pump 3 Running
  // [4] Pump 4 Running
  // [5] Recipe Started
  // [6] Recipe Running
  // [7] Recipe Complete

  msg = "STS:1:" + String(pump1Running) + ":" + String(pump2Running) + ":" + String(pump3Running) + ":" + String(pump4Running) + ":" + String(recipeStarted) + ":" + String(recipeRunning) + ":" + String(recipeComplete) + ":" + String(doorOpen);
  msg.toCharArray(msgToSend, 1024);

  Serial.println(msgToSend);
}

//Send Property Data Through Serial Port
void UpdateData()
{
  char msgToSend[1024];
  String msg = "";

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

  // TODO:
  // [11] Pump 1 Dispense Time Remaining
  // [12] Pump 2 Dispense Time Remaining
  // [13] Pump 3 Dispense Time Remaining
  // [14] Pump 4 Dispense Time Remaining  

  msg = "DATA:" + pump1Liquid + ":" + pump1TimeToRun + ":" + pump2Liquid + ":" + pump2TimeToRun + ":" + pump3Liquid + ":" + pump3TimeToRun + ":" + pump4Liquid + ":" + pump4TimeToRun + ":" + recipeDrinkCount + ":" + recipeName;
  msg.toCharArray(msgToSend, 1024);

  Serial.println(msgToSend);
}

void Log(String log)
{
  logMsg = log;
}

void BlinkOnBoardLED()
{
  //Status LED [Blinks Forever]
  digitalWrite(25, HIGH);
}

bool newPumpCMD = false;
void CheckForCommands()
{
  Serial.println("1");
  char recieved = 'c';
  String Command = "";
  String Value = "";
  String dataStr = "";
  //Serial.println("LOG: " + analogRead(doorOpenSensorPIN));
  
  while (Serial.available() > 0)
  {
    //Record Serial + Convert To String
    recieved = Serial.read();
    inData += recieved;
  }
  
  Command = getValue(inData, '=', 0);
  dataStr = getValue(inData, '=', 1);
  inData = "";//Clear recieved buffer
  
  //CMD Check Helpers
  bool isRecipeDL_CMD = Command == "DL";
  bool isManualPour_CMD = Command == "OUT";
  bool isManualOff_CMD = Command == "OFF";

  //Stop Pouring All Pumps
  if (Command == "OFF")
  {
    StopPouringPump1();
    StopPouringPump2();
    StopPouringPump3();
    StopPouringPump4();
    
    UpdateStatusBits();
  }

  if (Command == "DL")
  {
    Serial.println("LOG: ---------------[PICO] RecipeDL Received!");
    logMsg = "------------------------";
    pump1Liquid = getValue(dataStr, ':', 0);
    Serial.println("LOG: ---------------[PICO] Pump 1 Liquid: " + pump1Liquid);
    pump1TimeToRun = getValue(dataStr, ':', 1).toInt();
    Serial.println("LOG: ---------------[PICO] Pump 1 Time: " + String(pump1TimeToRun));
    pump2Liquid = getValue(dataStr, ':', 2);
    Serial.println("LOG: ---------------[PICO] Pump 2 Liquid: " + pump2Liquid);
    pump2TimeToRun = getValue(dataStr, ':', 3).toInt();
    Serial.println("LOG: ---------------[PICO] Pump 2 Time: " + String(pump2TimeToRun));
    pump3Liquid = getValue(dataStr, ':', 4);
    Serial.println("LOG: ---------------[PICO] Pump 3 Liquid: " + pump3Liquid);
    pump3TimeToRun = getValue(dataStr, ':', 5).toInt();
    Serial.println("LOG: ---------------[PICO] Pump 3 Time: " + String(pump3TimeToRun));
    pump4Liquid = getValue(dataStr, ':', 6);
    Serial.println("LOG: ---------------[PICO] Pump 4 Liquid: " + pump4Liquid);
    pump4TimeToRun = getValue(dataStr, ':', 7).toInt();
    Serial.println("LOG: ---------------[PICO] Pump 4 Time: " + String(pump4TimeToRun));
    recipeDrinkCount = getValue(dataStr, ':', 8).toInt();
    Serial.println("LOG: ---------------[PICO] Drink Count: " + recipeDrinkCount);
    recipeName = getValue(dataStr, ':', 9);
    Serial.println("LOG: ---------------[PICO] Recipe Name: " + recipeName);

    recipeStarted = true;
    recipeRunning = true;
    UpdateData();
    UpdateStatusBits();
    RunRecipe();
    recipeName = "Not Set";
    recipeDrinkCount = 0;
    pump1Liquid = "Not Set";
    pump1TimeToRun = 0;
    pump2Liquid = "Not Set";
    pump2TimeToRun = 0;
    pump3Liquid = "Not Set";
    pump3TimeToRun = 0;
    pump4Liquid = "Not Set";
    pump4TimeToRun = 0;
    UpdateStatusBits();
  }

  if (Command = "OUT")
  {
    if (getValue(dataStr, ':', 0).toInt() == 1 || getValue(dataStr, ':', 1).toInt() == 1 || getValue(dataStr, ':', 2).toInt() == 1 || getValue(dataStr, ':', 3).toInt() == 1){
      //Pump 1 Manual Pour Bit
      if (getValue(dataStr, ':', 0).toInt() == 1)
      {
        PourPump1();
        UpdateStatusBits();
      }
      else if (getValue(dataStr, ':', 1).toInt() == 1)
      {
        PourPump2();
        UpdateStatusBits();
      } 
      else if (getValue(dataStr, ':', 2).toInt() == 1)
      {
        PourPump3();
        UpdateStatusBits();
      } 
      else if (getValue(dataStr, ':', 3).toInt() == 1)
      {
        PourPump4();
        UpdateStatusBits();
      }
      
      UpdateStatusBits();
    } else {
        StopPouringPump1();
        StopPouringPump2();
        StopPouringPump3();
        StopPouringPump4();
        
        UpdateStatusBits();
    }
    Command = "";    
  }
}

void PourPump1(){
    pump1Running = true;
    digitalWrite(pump1PIN, HIGH);
    updateStatus = true;
}

void StopPouringPump1(){
    pump1Running = false;
    digitalWrite(pump1PIN, LOW);
    updateStatus = true;
}

void PourPump2(){
    digitalWrite(pump2PIN, HIGH);
    pump2Running = true;
    updateStatus = true;
}

void StopPouringPump2(){
    digitalWrite(pump2PIN, LOW);
    pump2Running = false;
    updateStatus = true;
}

void PourPump3(){
    digitalWrite(pump3PIN, HIGH);
    pump3Running = true;
    updateStatus = true;
}

void StopPouringPump3(){
    digitalWrite(pump3PIN, LOW);
    pump3Running = false;
    updateStatus = true;
}

void PourPump4(){
    digitalWrite(pump4PIN, HIGH);
    pump4Running = true;
    updateStatus = true;
}

void StopPouringPump4(){
    digitalWrite(pump4PIN, LOW);
    pump4Running = false;
    updateStatus = true;
}

void PourPump5(){
    digitalWrite(pump5PIN, HIGH);
    pump5Running = digitalRead(pump5PIN) == HIGH;
    updateStatus = true;
}

void StopPouringPump5(){
    digitalWrite(pump5PIN, LOW);
    pump5Running = digitalRead(pump5PIN) == HIGH;
    updateStatus = true;
}

void PourPump6(){
    digitalWrite(pump6PIN, HIGH);
    pump6Running = digitalRead(pump6PIN) == HIGH;
    updateStatus = true;
}

void StopPouringPump6(){
    digitalWrite(pump6PIN, LOW);
    pump6Running = digitalRead(pump6PIN) == HIGH;
    updateStatus = true;
}

void PourPump7(){
    digitalWrite(pump7PIN, HIGH);
    pump7Running = digitalRead(pump7PIN) == HIGH;
    updateStatus = true;
}

void StopPouringPump7(){
    digitalWrite(pump7PIN, LOW);
    pump7Running = digitalRead(pump7PIN) == HIGH;
    updateStatus = true;
}

void PourPump8(){
    digitalWrite(pump8PIN, HIGH);
    pump8Running = digitalRead(pump8PIN) == HIGH;
}

void StopPouringPump8(){
    digitalWrite(pump8PIN, LOW);
    pump8Running = digitalRead(pump8PIN) == HIGH;
}

void StartPump(int ID){
  if (ID == 1){
    pump1Running = true;
    digitalWrite(pump1PIN, HIGH);
  }
  if (ID == 2){
    pump2Running = true;
    digitalWrite(pump2PIN, HIGH);
  }
  if (ID == 3){
    pump3Running = true;
    digitalWrite(pump3PIN, HIGH);
  }
  if (ID == 4){
    pump4Running = true;
    digitalWrite(pump4PIN, HIGH);
  }
  if (ID == 5){
    pump5Running = true;
    digitalWrite(pump5PIN, HIGH);
  }
  if (ID == 6){
    pump6Running = true;
    digitalWrite(pump6PIN, HIGH);
  }
  if (ID == 7){
    pump7Running = true;
    digitalWrite(pump7PIN, HIGH);
  }
  if (ID == 8){
    pump8Running = true;
    digitalWrite(pump8PIN, HIGH);
  }
  newPumpCMD = true;
}

void StopPump(int ID){
  if (ID == 1){
    pump1Running = false;
    digitalWrite(pump1PIN, LOW);
  }
  if (ID == 2){
    pump2Running = false;
    digitalWrite(pump2PIN, LOW);
  }
  if (ID == 3){
    pump3Running = false;
    digitalWrite(pump3PIN, LOW);
  }
  if (ID == 4){
    pump4Running = false;
    digitalWrite(pump4PIN, LOW);
  }
  if (ID == 5){
    pump5Running = false;
    digitalWrite(pump5PIN, LOW);
  }
  if (ID == 6){
    pump6Running = false;
    digitalWrite(pump6PIN, LOW);
  }
  if (ID == 7){
    pump7Running = false;
    digitalWrite(pump7PIN, LOW);
  }
  if (ID == 8){
    pump8Running = false;
    digitalWrite(pump8PIN, LOW);
  }
  newPumpCMD = true;
}

void GetTempHumidity()
{
  if (hasTempHumiditySensor){
    Serial.print("Humidity: ");
    //Serial.print(dht.readHumidity());
    Serial.println(" %");
    delay(25);
    Serial.print("Temperature: ");
    //Serial.print(dht.readTemperature(true));
    Serial.println(" F");
    delay(25);
  }
}

String getValue(String data, char separator, int index)
{
  int found = 0;
  int strIndex[] = {0, -1};
  int maxIndex = data.length()-1;

  for(int i=0; i<=maxIndex && found<=index; i++){
    if(data.charAt(i)==separator || i==maxIndex){
        found++;
        strIndex[0] = strIndex[1]+1;
        strIndex[1] = (i == maxIndex) ? i+1 : i;
    }
  }

  return found>index ? data.substring(strIndex[0], strIndex[1]) : "";
}

void RunRecipe()
{
  UpdateData();
  Serial.println("LOG: Recipe Started!");
  recipeStarted = false;
  pump1Used = pump1TimeToRun > 0;
  pump2Used = pump2TimeToRun > 0;
  pump3Used = pump3TimeToRun > 0;
  pump4Used = pump4TimeToRun > 0;

  recipeRunning = true;
  UpdateStatusBits();
  
  if (pump1Used)                                                                                    //Start Pump-1 If Used
  { 
    Serial.println("LOG: [PICO] Pump 1 Running...");                                        
    StartPump1();
    UpdateStatusBits(); 
  } else {
    Serial.println("LOG: [PICO] Pump 1 Not Used.");                                                                                                           
  }
  
  if (pump2Used)                                                                                    //Start Pump-2 If Used
  { 
    Serial.println("LOG: [PICO] Pump 2 Running...");   
    StartPump2(); 
    UpdateStatusBits(); 
  }    else {
    Serial.println("LOG: [PICO] Pump 2 Not Used.");                                                              
  }   
                       
  if (pump3Used)                                                                                    //Start Pump-3 If Used
  { 
    Serial.println("LOG: [PICO] Pump 3 Running...");   
    StartPump3(); 
    UpdateStatusBits(); 
  }    else {
    Serial.println("LOG: [PICO] Pump 3 Not Used.");                                                                        
  }
    
  if (pump4Used)                                                                                    //Start Pump-4 If Used
  { 
    Serial.println("LOG: [PICO] Pump 4 Running...");   
    StartPump4(); 
    UpdateStatusBits(); 
  }    else {
    Serial.println("LOG: [PICO] Pump 4 Not Used.");                                                                        
  }  

  recipeComplete=true;
  recipeStarted=false;
  recipeRunning=false;
  UpdateStatusBits(); 
  drinksCompleted=0;
  pump1Used = false;
  pump2Used = false;
  pump3Used = false;
  pump4Used = false;
  Serial.println("LOG: Recipe Complete!");
  delay(2000);
  recipeComplete=false;
}

int p1Ctr = 0;
void StartPump1(){
  pump1Running = true;
  pump2Running = false;
  pump3Running = false;
  pump4Running = false;
  pump5Running = false;
  pump6Running = false;
  pump7Running = false;
  pump8Running = false;
  UpdateStatusBits(); 
  
  digitalWrite(2, HIGH);

  //Update Status While Pumping
  while(p1Ctr < (pump1TimeToRun))
  {
    p1Ctr++;
    UpdateStatusBits();
    delay(1);
  }
  p1Ctr = 0;
  UpdateStatusBits();
  
  digitalWrite(2, LOW);
  
  drinksCompleted++;
  pump1Used = false;
  pump1Running = false;
  UpdateStatusBits(); 
}

int p2Ctr = 0;
void StartPump2(){
  pump2Running = true;
  pump1Running = false;
  pump3Running = false;
  pump4Running = false;
  pump5Running = false;
  pump6Running = false;
  pump7Running = false;
  pump8Running = false;
  UpdateStatusBits();
  digitalWrite(3, HIGH);

  //Update Status While Pumping
  while(p2Ctr < (pump2TimeToRun))
  {
    p2Ctr++;
    UpdateStatusBits();
    delay(1);
  }
  p2Ctr = 0;
  UpdateStatusBits();
  
  digitalWrite(3, LOW);
  
  drinksCompleted++;
  pump2Used = false;
  pump2Running = false;
  UpdateStatusBits(); 
}

int p3Ctr = 0;
void StartPump3(){
  pump1Running = false;
  pump2Running = false;
  pump3Running = true;
  pump4Running = false;
  pump5Running = false;
  pump6Running = false;
  pump7Running = false;
  pump8Running = false;
  updateStatus = true;
  digitalWrite(4, HIGH);

  //Update Status While Pumping
  while(p3Ctr < (pump3TimeToRun))
  {
    p3Ctr++;
    UpdateStatusBits();
    delay(1);
  }
  p3Ctr = 0;
  UpdateStatusBits();
  
  digitalWrite(4, LOW);
  
  drinksCompleted++;
  pump3Used = false;
  pump3Running = false;
  updateStatus = true;
  UpdateStatusBits(); 
}

int p4Ctr = 0;
void StartPump4(){
  pump1Running = false;
  pump2Running = false;
  pump3Running = false;
  pump4Running = true;
  pump5Running = false;
  pump6Running = false;
  pump7Running = false;
  pump8Running = false;
  updateStatus = true;
  digitalWrite(5, HIGH);

  //Update Status While Pumping
  while(p4Ctr < (pump4TimeToRun))
  {
    p4Ctr++;
    UpdateStatusBits();
    delay(1);
  }
  p4Ctr = 0;
  UpdateStatusBits();
  
  digitalWrite(5, LOW);
  
  drinksCompleted++;
  pump4Used = false;
  pump4Running = false;
  updateStatus = true;
  UpdateStatusBits(); 
}

void StartPump5(){
  pump1Running = false;
  pump2Running = false;
  pump3Running = false;
  pump4Running = false;
  pump5Running = true;
  pump6Running = false;
  pump7Running = false;
  pump8Running = false;
  updateStatus = true;
  delay(pump5TimeToRun);
  //delay(50);
  drinksCompleted++;
  pump5Used = false;
  pump5Running = false;
  updateStatus = true;
}

void StartPump6(){
  pump1Running = false;
  pump2Running = false;
  pump3Running = false;
  pump4Running = false;
  pump5Running = false;
  pump6Running = true;
  pump7Running = false;
  pump8Running = false;
  updateStatus = true;
  delay(pump6TimeToRun);
  delay(50);
  drinksCompleted++;
  pump6Used = false;
  pump6Running = false;
  updateStatus = true;
}

void StartPump7(){
  pump1Running = false;
  pump2Running = false;
  pump3Running = false;
  pump4Running = false;
  pump5Running = false;
  pump6Running = false;
  pump7Running = true;
  pump8Running = false;
  updateStatus = true;
  delay(pump7TimeToRun);
  delay(50);
  drinksCompleted++;
  pump7Used = false;
  pump7Running = false;
  updateStatus = true;
}

void StartPump8(){
  pump1Running = false;
  pump2Running = false;
  pump3Running = false;
  pump4Running = false;
  pump5Running = false;
  pump6Running = false;
  pump7Running = false;
  pump8Running = true;
  updateStatus = true;
  delay(pump8TimeToRun);
  drinksCompleted++;
  pump8Used = false;
  pump8Running = false;
  updateStatus = true;
}
