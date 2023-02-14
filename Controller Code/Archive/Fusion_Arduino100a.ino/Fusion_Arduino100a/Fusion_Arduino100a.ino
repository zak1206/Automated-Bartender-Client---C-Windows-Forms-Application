//#include <DHT.h>
//#include <DHT_U.h>
//------------------------------------- Definitions
//#define dht_APIN A7
//#define DHTTYPE DHT11
//------------------------------------- Public Variables
//--------------------- Pin Assignment Variables
//int DHTPIN = 12;
//#include "pico/stdlib.h"
//#include "pico/stdio.h"
//#include <stdio.h>
int pump1PIN = 2;
int pump2PIN = 3;
int pump3PIN = 4;
int pump4PIN = 5;
int pump5PIN = 6;
int pump6PIN = 7;
int pump7PIN = 8;
int pump8PIN = 9;
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
int recipeDrinkCount = 0;
String recipeName = "";
int pump1TimeToRun = 0;//ms
String pump1Liquid = "";
int pump2TimeToRun = 0;//ms
String pump2Liquid = "";
int pump3TimeToRun = 0;//ms
String pump3Liquid = "";
int pump4TimeToRun = 0;//ms
String pump4Liquid = "";
int pump5TimeToRun = 0;//ms
String pump5Liquid = "";
int pump6TimeToRun = 0;//ms
String pump6Liquid = "";
int pump7TimeToRun = 0;//ms
String pump7Liquid = "";
int pump8TimeToRun = 0;//ms
String pump8Liquid = "";

//------------------------------------- Libraries
//DHT dht(DHTPIN, DHTTYPE); //DHT Sensor Library [Temp/Humidity]

void setup() 
{
  Serial.begin(9600);
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
  //if (hasTempHumiditySensor) dht.begin();
}

void setup1()
{
  pinMode(25, OUTPUT);
  loop1();
}

//Core 0 Thread loop
bool oneShot = true;
void loop() 
{
  CheckForCommands();//Check For Sent Commands
  delay(75);
  if (!recipeRunning && recipeStarted && oneShot){ Serial.println("recipeRunning=1"); RunRecipe(); oneShot = false; delay(1500); } else { Serial.println("Connected!"); }
}

//Core 1 Thread loop
void loop1()
{
  digitalWrite(25, HIGH);
  delay(1000);
  digitalWrite(25, LOW);
  delay(1000);
  loop1();
}

bool newPumpCMD = false;
void CheckForCommands()
{
  char recieved = 'c';
  String Command = "";
  String Value = "";
  while (Serial.available() > 0)
  {
    //Record Serial + Convert To String
    recieved = Serial.read();
    inData += recieved;
    Command = getValue(inData, '=', 0);
    Value = getValue(inData, '=', 1);

    //CMD Check Helpers
    bool isDrinkCountCMD = Command == "recipeDrinkCount";
    bool isRecipeNameCMD = Command == "recipeName";
    bool isRecipeRunningCMD = Command == "recipeRunning";
    bool isRecipeCompleteCMD = Command == "recipeComplete";
    bool isPump1TimeToRunCMD = Command == "pump1TimeToRun";
    bool isPump2TimeToRunCMD = Command == "pump2TimeToRun";
    bool isPump3TimeToRunCMD = Command == "pump3TimeToRun";
    bool isPump4TimeToRunCMD = Command == "pump4TimeToRun";
    bool isPump5TimeToRunCMD = Command == "pump5TimeToRun";
    bool isPump6TimeToRunCMD = Command == "pump6TimeToRun";
    bool isPump7TimeToRunCMD = Command == "pump7TimeToRun";
    bool isPump8TimeToRunCMD = Command == "pump8TimeToRun";
    bool isPump1LiquidCMD = Command == "pump1Liquid";
    bool isPump2LiquidCMD = Command == "pump2Liquid";
    bool isPump3LiquidCMD = Command == "pump3Liquid";
    bool isPump4LiquidCMD = Command == "pump4Liquid";
    bool isPump5LiquidCMD = Command == "pump5Liquid";
    bool isPump6LiquidCMD = Command == "pump6Liquid";
    bool isPump7LiquidCMD = Command == "pump7Liquid";
    bool isPump8LiquidCMD = Command == "pump8Liquid";
    bool isGetTempHumCMD = Command == "GetTempHum()";
    bool isStartPump1CMD = Command == "StartPump1";
    bool isStartPump2CMD = Command == "StartPump2";
    bool isStartPump3CMD = Command == "StartPump3";
    bool isStartPump4CMD = Command == "StartPump4";
    bool isStopPump1CMD = Command == "StartPump1";
    bool isStopPump2CMD = Command == "StartPump2";
    bool isStopPump3CMD = Command == "StartPump3";
    bool isStopPump4CMD = Command == "StartPump4";
    bool isStartPump5CMD = Command == "StartPump5";
    bool isStartPump6CMD = Command == "StartPump6";
    bool isStartPump7CMD = Command == "StartPump7";
    bool isStartPump8CMD = Command == "StartPump8";
    bool isStopPump5CMD = Command == "StartPump5";
    bool isStopPump6CMD = Command == "StartPump6";
    bool isStopPump7CMD = Command == "StartPump7";
    bool isStopPump8CMD = Command == "StartPump8";
    bool isRecipeStartedCMD = Command == "recipeStarted";
     
    if (recieved == '\n')
    {
      if (recipeComplete){
        delay(1000);
        recipeRunning=false;
        Serial.println("recipeRunning=0");
        delay(100);
        recipeComplete = false;
        Serial.println("recipeComplete=1");
      }
      if (isRecipeRunningCMD){ if (Value.toInt() == 1){ recipeRunning = true; Serial.println("recipeRunning=1"); } else { recipeRunning = false; Serial.println("recipeRunning=0"); } }        //Recipe Running
      else if (recipeComplete){ isRecipeCompleteCMD=true;}
      else if (isPump1LiquidCMD){ pump1Liquid = Value; Serial.println("pump1Liquid="+Value); }                                                                                                       //Pump 1 Liquid
      else if (isPump1TimeToRunCMD){ pump1TimeToRun = Value.toInt(); Serial.println("pump1TimeToRun="+Value); pump1Used = true; }                                                                   //Pump 1 TimeToRun
      else if (isPump2LiquidCMD){ pump2Liquid = Value; Serial.println("pump2Liquid="+Value); }                                                                                                      //Pump 2 Liquid
      else if (isPump2TimeToRunCMD){ pump2TimeToRun = Value.toInt(); Serial.println("pump2TimeToRun="+Value); pump2Used = true; }                                                                   //Pump 2 TimeToRun 
      else if (isPump3LiquidCMD){ pump3Liquid = Value; Serial.println("pump3Liquid="+Value); }                                                                                                      //Pump 3 Liquid
      else if (isPump3TimeToRunCMD){ pump3TimeToRun = Value.toInt(); Serial.println("pump3TimeToRun="+Value); pump3Used = true; }                                                                   //Pump 3 TimeToRun 
      else if (isPump4LiquidCMD){ pump4Liquid = Value; Serial.println("pump4Liquid="+Value); }                                                                                                      //Pump 4 Liquid
      else if (isPump4TimeToRunCMD){ pump4TimeToRun = Value.toInt(); Serial.println("pump4TimeToRun="+Value); pump4Used = true;  }                                                                   //Pump 4 TimeToRun 
      else if (isPump5LiquidCMD){ pump5Liquid = Value; Serial.println("pump5Liquid="+Value); }                                                                                                      //Pump 5 Liquid
      else if (isPump5TimeToRunCMD){ pump5TimeToRun = Value.toInt(); Serial.println("pump5TimeToRun="+Value); pump5Used = true; }                                                                   //Pump 5 TimeToRun
      else if (isPump6LiquidCMD){ pump6Liquid = Value; Serial.println("pump6Liquid="+Value); }                                                                                                      //Pump 6 Liquid
      else if (isPump6TimeToRunCMD){ pump6TimeToRun = Value.toInt(); Serial.println("pump6TimeToRun="+Value); pump6Used = true;  }                                                                   //Pump 6 TimeToRun 
      else if (isPump7LiquidCMD){ pump7Liquid = Value; Serial.println("pump7Liquid="+Value);  }                                                                                                      //Pump 7 Liquid
      else if (isPump7TimeToRunCMD){ pump7TimeToRun = Value.toInt(); Serial.println("pump7TimeToRun="+Value); pump7Used = true; }                                                                   //Pump 7 TimeToRun 
      else if (isPump8LiquidCMD){ pump8Liquid = Value; Serial.println("pump8Liquid="+Value); }                                                                                                      //Pump 8 Liquid
      else if (isPump8TimeToRunCMD){ pump8TimeToRun = Value.toInt(); Serial.println("pump8TimeToRun="+Value); pump8Used = true; }                                                                   //Pump 8 TimeToRun 
      else if (isDrinkCountCMD){ recipeDrinkCount = Value.toInt(); Serial.println("recipeDrinkCount="+Value); }                                                                                     //Drink Count
      else if (isRecipeNameCMD){ recipeName = Value; Serial.println("recipeName="+Value); }                                                                                                         //Recipe Name
      else if (isRecipeCompleteCMD && !oneShot){ if (Value.toInt() == 1){ Serial.println("recipeRunning=0"); recipeStarted = false; recipeComplete = true; oneShot = true; isRecipeCompleteCMD=false; isRecipeStartedCMD=false; recipeRunning=false; Serial.println("recipeRunning=0"); } else { if (recipeComplete) { Serial.println("recipeComplete=0"); } } }   //Recipe Completed
      else if (isRecipeStartedCMD){ if (Value.toInt() == 1){ recipeStarted = true; Serial.println("recipeStarted=1"); } else { recipeStarted = false; Serial.println("recipeStarted=0"); }  }        //Recipe Started
      else if (recipeRunning && !recipeStarted && !recipeComplete) { isRecipeStartedCMD = true; }                                                                                                                                   //Recipe Running
      //if (isGetTempHumCMD && !recipeRunning && !recipeStarted) GetTempHumidity();                                                                                                             //Temp/Humidity Request               
      else if (isStartPump1CMD && Value.toInt() == 1 && !isRecipeStartedCMD && !isRecipeCompleteCMD && !isRecipeRunningCMD) PourPump1();                                                           //Manual Pump 1
      else if (isStartPump2CMD && Value.toInt() == 1 && !isRecipeStartedCMD && !isRecipeCompleteCMD && !isRecipeRunningCMD) PourPump2();                                                           //Manual Pump 2          
      else if (isStartPump3CMD && Value.toInt() == 1 && !isRecipeStartedCMD && !isRecipeCompleteCMD && !isRecipeRunningCMD) PourPump3();                                                           //Manual Pump 3        
      else if (isStartPump4CMD && Value.toInt() == 1 && !isRecipeStartedCMD && !isRecipeCompleteCMD && !isRecipeRunningCMD) PourPump4();                                                           //Manual Pump 4                
      else if (isStopPump1CMD && Value.toInt() == 0 && !isRecipeStartedCMD && !isRecipeCompleteCMD && !isRecipeRunningCMD) { StopPouringPump1(); pump1Running = false; }                           //Manual Stop Pump 1
      else if (isStopPump2CMD && Value.toInt() == 0 && !isRecipeStartedCMD && !isRecipeCompleteCMD && !isRecipeRunningCMD) { StopPouringPump2(); pump2Running = false; }                           //Manual Stop Pump 2    
      else if (isStopPump3CMD && Value.toInt() == 0 && !isRecipeStartedCMD && !isRecipeCompleteCMD && !isRecipeRunningCMD) { StopPouringPump3(); pump3Running = false; }                           //Manual Stop Pump 3    
      else if (isStopPump4CMD && Value.toInt() == 0 && !isRecipeStartedCMD && !isRecipeCompleteCMD && !isRecipeRunningCMD) { StopPouringPump4(); pump4Running = false; }                           //Manual Stop Pump 4               
      else if (isStartPump5CMD && Value.toInt() == 1 && !isRecipeStartedCMD && !isRecipeCompleteCMD && !isRecipeRunningCMD) PourPump5();                                                           //Manual Pump 5
      else if (isStartPump6CMD && Value.toInt() == 1 && !isRecipeStartedCMD && !isRecipeCompleteCMD && !isRecipeRunningCMD) PourPump6();                                                           //Manual Pump 6          
      else if (isStartPump7CMD && Value.toInt() == 1 && !isRecipeStartedCMD && !isRecipeCompleteCMD && !isRecipeRunningCMD) PourPump7();                                                           //Manual Pump 7        
      else if (isStartPump8CMD && Value.toInt() == 1 && !isRecipeStartedCMD && !isRecipeCompleteCMD && !isRecipeRunningCMD) PourPump8();                                                           //Manual Pump 8                
      else if (isStopPump5CMD && Value.toInt() == 0 && !isRecipeStartedCMD && !isRecipeCompleteCMD && !isRecipeRunningCMD) { StopPouringPump5(); pump5Running = false; }                           //Manual Stop Pump 5
      else if (isStopPump6CMD && Value.toInt() == 0 && !isRecipeStartedCMD && !isRecipeCompleteCMD && !isRecipeRunningCMD) { StopPouringPump6(); pump6Running = false; }                           //Manual Stop Pump 6    
      else if (isStopPump7CMD && Value.toInt() == 0 && !isRecipeStartedCMD && !isRecipeCompleteCMD && !isRecipeRunningCMD) { StopPouringPump7(); pump7Running = false; }                           //Manual Stop Pump 7    
      else if (isStopPump8CMD && Value.toInt() == 0 && !isRecipeStartedCMD && !isRecipeCompleteCMD && !isRecipeRunningCMD) { StopPouringPump8(); pump8Running = false; }                           //Manual Stop Pump 8
      else if (pump1Running && !recipeRunning){ Serial.println("pump1Running=1"); }                                                                                                                  //Pump 1 Running
      else if (pump2Running && !recipeRunning){ Serial.println("pump2Running=1"); }                                                                                                                  //Pump 2 Running
      else if (pump3Running && !recipeRunning){ Serial.println("pump3Running=1"); }                                                                                                                  //Pump 3 Running
      else if (pump4Running && !recipeRunning){ Serial.println("pump4Running=1"); }                                                                                                                  //Pump 4 Running
      else if (pump5Running && !recipeRunning){ Serial.println("pump5Running=1"); }                                                                                                                  //Pump 5 Running
      else if (pump6Running && !recipeRunning){ Serial.println("pump6Running=1"); }                                                                                                                  //Pump 6 Running
      else if (pump7Running && !recipeRunning){ Serial.println("pump7Running=1"); }                                                                                                                  //Pump 7 Running
      else if (pump8Running && !recipeRunning){ Serial.println("pump8Running=1"); }                                                                                                                  //Pump 8 Running
      else if (!recipeRunning) { Serial.println("recipeRunning=0");}
      inData = "";//Clear recieved buffer
    }
  }
}

void PourPump1(){
    pump1Running = true;
    digitalWrite(pump1PIN, LOW);
}

void StopPouringPump1(){
    pump1Running = false;
    digitalWrite(pump1PIN, HIGH);
    Serial.println("pump1Running=0");
}

void PourPump2(){
    pump2Running = true;
    digitalWrite(pump2PIN, LOW);
}

void StopPouringPump2(){
    pump2Running = false;
    digitalWrite(pump2PIN, HIGH);
    Serial.println("pump2Running=0");
}

void PourPump3(){
    pump3Running = true;
    digitalWrite(pump3PIN, HIGH);
}

void StopPouringPump3(){
    pump3Running = false;
    digitalWrite(pump3PIN, LOW);
    Serial.println("pump3Running=0");
}

void PourPump4(){
    pump4Running = true;
    digitalWrite(pump4PIN, HIGH);
}

void StopPouringPump4(){
    pump4Running = false;
    digitalWrite(pump4PIN, LOW);
    Serial.println("pump4Running=0");
}

void PourPump5(){
    pump5Running = true;
    digitalWrite(pump5PIN, HIGH);
}

void StopPouringPump5(){
    pump5Running = false;
    digitalWrite(pump5PIN, LOW);
    Serial.println("pump5Running=0");
}

void PourPump6(){
    pump6Running = true;
    digitalWrite(pump6PIN, HIGH);
}

void StopPouringPump6(){
    pump6Running = false;
    digitalWrite(pump6PIN, LOW);
    Serial.println("pump6Running=0");
}

void PourPump7(){
    pump7Running = true;
    digitalWrite(pump7PIN, HIGH);
}

void StopPouringPump7(){
    pump7Running = false;
    digitalWrite(pump7PIN, LOW);
    Serial.println("pump7Running=0");
}

void PourPump8(){
    pump8Running = true;
    digitalWrite(pump8PIN, HIGH);
}

void StopPouringPump8(){
    pump8Running = false;
    digitalWrite(pump8PIN, LOW);
    Serial.println("pump8Running=0");
}

void StartPump(int ID){
  if (ID == 1){
    pump1Running = true;
    digitalWrite(pump1PIN, LOW);
  }
  if (ID == 2){
    pump2Running = true;
    digitalWrite(pump2PIN, LOW);
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
    digitalWrite(pump1PIN, HIGH);
  }
  if (ID == 2){
    pump2Running = false;
    digitalWrite(pump2PIN, HIGH);
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
  Serial.println("RECIPE: Running Recipe: " + recipeName);
  delay(1000);
  if (recipeStarted)
  {
    if (pump1Used) StartPump1();                                                                                        //Start Pump-1 If Used
    pump1Running = false;
    delay(100);
    if (pump2Used) StartPump2();                                                                                        //Start Pump-2 If Used
    pump2Running = false;
    delay(100);
    if (pump3Used) StartPump3();                                                                                        //Start Pump-3 If Used
    pump3Running = false;
    delay(100);
    if (pump4Used) StartPump4();                                                                                        //Start Pump-4 If Used
    pump4Running = false;
    delay(100);
    if (pump5Used) StartPump5();                                                                                        //Start Pump-5 If Used
    pump5Running = false;
    delay(100);
    if (pump6Used) StartPump6();                                                                                        //Start Pump-6 If Used
    pump6Running = false;
    delay(100);
    if (pump7Used) StartPump7();                                                                                        //Start Pump-7 If Used
    pump7Running = false;
    delay(100);
    if (pump8Used) StartPump8();                                                                                        //Start Pump-8 If Used
    pump8Running = false;
    delay(100);
    recipeStarted=false;
    Serial.println("recipeStarted=0");
    delay(100);
    drinksCompleted=0;
    pump1Used = false;
    pump2Used = false;
    pump3Used = false;
    pump4Used = false;
    pump5Used = false;
    pump6Used = false;
    pump7Used = false;
    pump8Used = false;
  } else {
    recipeRunning=false;
    Serial.println("recipeRunning=0");
    delay(100);
    recipeComplete=1;
    drinksCompleted=0;
    pump1Used = false;
    pump2Used = false;
    pump3Used = false;
    pump4Used = false;
    pump5Used = false;
    pump6Used = false;
    pump7Used = false;
    pump8Used = false;
  }
    recipeRunning=false;
    Serial.println("recipeRunning=0");
    delay(100);
    recipeComplete=1;
    drinksCompleted=0;
    pump1Used = false;
    pump2Used = false;
    pump3Used = false;
    pump4Used = false;
    pump5Used = false;
    pump6Used = false;
    pump7Used = false;
    pump8Used = false;
}

void StartPump1(){
  pump1Running = true;
  pump2Running = false;
  pump3Running = false;
  pump4Running = false;
  pump5Running = false;
  pump6Running = false;
  pump7Running = false;
  pump8Running = false;
  Serial.println("pump1Running=1");
  delay(25);
  Serial.println("pump2Running=0");
  delay(25);
  Serial.println("pump3Running=0");
  delay(25);
  Serial.println("pump4Running=0");
  delay(25);
  Serial.println("pump5Running=0");
  delay(25);
  Serial.println("pump6Running=0");
  delay(25);
  Serial.println("pump7Running=0");
  delay(25);
  Serial.println("pump8Running=0");
  delay(25);
  Serial.println("RECIPE: Pouring " + pump1Liquid);
  digitalWrite(2, LOW);
  delay(pump1TimeToRun);
  digitalWrite(2, HIGH);
  Serial.println("pump1Running=0");
  delay(50);
  drinksCompleted++;
  pump1Used = false;
  pump1Running = false;
}

void StartPump2(){
  pump2Running = true;
  pump1Running = false;
  pump3Running = false;
  pump4Running = false;
  pump5Running = false;
  pump6Running = false;
  pump7Running = false;
  pump8Running = false;
  Serial.println("pump2Running=1");
  delay(25);
  Serial.println("pump1Running=0");
  delay(25);
  Serial.println("pump3Running=0");
  delay(25);
  Serial.println("pump4Running=0");
  delay(25);
  Serial.println("pump5Running=0");
  delay(25);
  Serial.println("pump6Running=0");
  delay(25);
  Serial.println("pump7Running=0");
  delay(25);
  Serial.println("pump8Running=0");
  delay(25);
  Serial.println("RECIPE: Pouring " + pump2Liquid);
  digitalWrite(3, LOW);
  delay(pump2TimeToRun);
  digitalWrite(3, HIGH);
  Serial.println("pump2Running=0");
  delay(50);
  drinksCompleted++;
  pump2Used = false;
  pump2Running = false;
}

void StartPump3(){
  pump1Running = false;
  pump2Running = false;
  pump3Running = true;
  pump4Running = false;
  pump5Running = false;
  pump6Running = false;
  pump7Running = false;
  pump8Running = false;
  Serial.println("pump3Running=1");
  delay(25);
  Serial.println("pump1Running=0");
  delay(25);
  Serial.println("pump2Running=0");
  delay(25);
  Serial.println("pump4Running=0");
  delay(25);
  Serial.println("pump5Running=0");
  delay(25);
  Serial.println("pump6Running=0");
  delay(25);
  Serial.println("pump7Running=0");
  delay(25);
  Serial.println("pump8Running=0");
  delay(25);
  Serial.println("RECIPE: Pouring " + pump3Liquid);
  digitalWrite(4, LOW);
  delay(pump3TimeToRun);
  digitalWrite(4, HIGH);
  Serial.println("pump3Running=0");
  delay(50);
  drinksCompleted++;
  pump3Used = false;
  pump3Running = false;
}

void StartPump4(){
  pump1Running = false;
  pump2Running = false;
  pump3Running = false;
  pump4Running = true;
  pump5Running = false;
  pump6Running = false;
  pump7Running = false;
  pump8Running = false;
  Serial.println("pump4Running=1");
  delay(25);
  Serial.println("pump1Running=0");
  delay(25);
  Serial.println("pump2Running=0");
  delay(25);
  Serial.println("pump3Running=0");
  delay(25);
  Serial.println("pump5Running=0");
  delay(25);
  Serial.println("pump6Running=0");
  delay(25);
  Serial.println("pump7Running=0");
  delay(25);
  Serial.println("pump8Running=0");
  delay(25);
  Serial.println("RECIPE: Pouring " + pump4Liquid);
  digitalWrite(5, LOW);
  delay(pump4TimeToRun);
  digitalWrite(5, HIGH);
  Serial.println("pump4Running=0");
  delay(50);
  drinksCompleted++;
  pump4Used = false;
  pump4Running = false;
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
  Serial.println("pump5Running=1");
  delay(25);
  Serial.println("pump1Running=0");
  delay(25);
  Serial.println("pump2Running=0");
  delay(25);
  Serial.println("pump4Running=0");
  delay(25);
  Serial.println("pump3Running=0");
  delay(25);
  Serial.println("pump6Running=0");
  delay(25);
  Serial.println("pump7Running=0");
  delay(25);
  Serial.println("pump8Running=0");
  delay(25);
  Serial.println("RECIPE: Pouring " + pump5Liquid);
  delay(pump5TimeToRun);
  Serial.println("pump5Running=0");
  delay(50);
  drinksCompleted++;
  pump5Used = false;
  pump5Running = false;
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
  Serial.println("pump6Running=1");
  delay(25);
  Serial.println("pump1Running=0");
  delay(25);
  Serial.println("pump2Running=0");
  delay(25);
  Serial.println("pump4Running=0");
  delay(25);
  Serial.println("pump5Running=0");
  delay(25);
  Serial.println("pump3Running=0");
  delay(25);
  Serial.println("pump7Running=0");
  delay(25);
  Serial.println("pump8Running=0");
  delay(25);
  Serial.println("RECIPE: Pouring " + pump6Liquid);
  delay(pump6TimeToRun);
  Serial.println("pump6Running=0");
  delay(50);
  drinksCompleted++;
  pump6Used = false;
  pump6Running = false;
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
  Serial.println("pump7Running=1");
  delay(25);
  Serial.println("pump1Running=0");
  delay(25);
  Serial.println("pump2Running=0");
  delay(25);
  Serial.println("pump4Running=0");
  delay(25);
  Serial.println("pump5Running=0");
  delay(25);
  Serial.println("pump6Running=0");
  delay(25);
  Serial.println("pump3Running=0");
  delay(25);
  Serial.println("pump8Running=0");
  delay(25);
  Serial.println("RECIPE: Pouring " + pump7Liquid);
  delay(pump7TimeToRun);
  Serial.println("pump7Running=0");
  delay(50);
  drinksCompleted++;
  pump7Used = false;
  pump7Running = false;
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
  Serial.println("pump8Running=1");
  delay(25);
  Serial.println("pump1Running=0");
  delay(25);
  Serial.println("pump2Running=0");
  delay(25);
  Serial.println("pump4Running=0");
  delay(25);
  Serial.println("pump5Running=0");
  delay(25);
  Serial.println("pump6Running=0");
  delay(25);
  Serial.println("pump7Running=0");
  delay(25);
  Serial.println("pump3Running=0");
  delay(25);
  Serial.println("RECIPE: Pouring " + pump8Liquid);
  delay(pump8TimeToRun);
  Serial.println("pump8Running=0");
  delay(50);
  drinksCompleted++;
  pump8Used = false;
  pump8Running = false;
}
