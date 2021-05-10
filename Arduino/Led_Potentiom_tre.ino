#define SERIAL_USB
const int analogPin = 0;//the analog input pin attach to

const int ledPin = 9;//the led attach to

int inputValue = 0;//variable to store the value coming from sensor

int outputValue = 0;//variable to store the output value
int input4Pin = 7;
int input1Pin = 6; 
int input3Pin = 5;
int input2Pin = 4;

void setup() {
    Serial.begin(9600); // You can choose any baudrate, just need to also change it in Unity.
  // put your setup code here, to run once:
  pinMode(input4Pin, INPUT);
  pinMode(input3Pin, INPUT);
  pinMode(input2Pin, INPUT);
  pinMode(input1Pin, INPUT);
}

void loop() {
  // put your main code here, to run repeatedly:
inputValue = analogRead(analogPin);
int value = analogRead(A1);
//Serial.print("Capteur : ");
//Serial.println(value); 
//Serial.print("LED : ");
//Serial.println(inputValue); 
 outputValue = map(inputValue, 0, 1023, 0,255);
 analogWrite(ledPin, outputValue);
 float values[] = {inputValue,value,0,0,0,0};
 for(int i=4 ; i<=7 ; i++){
  if(checkPush(i)){
    values[i-2]=1;
  }
 }
 Serial.flush();
 Serial.print(map(values[0],0,1023,-100,100)); 
  Serial.print(",");
  Serial.print(map(values[1],0,100,0,100));
  Serial.print(",");
  Serial.print(map(values[2],0,1,0,1)); 
  Serial.print(","); 
  Serial.print(map(values[3],0,1,0,1)); 
  Serial.print(",");  
  Serial.print(map(values[4],0,1,0,1)); 
  Serial.print(",");  
  Serial.print(map(values[5],0,1,0,1)); 
  Serial.println();
 delay(10);
}
bool checkPush(int pinNumber){
  int pushed = digitalRead(pinNumber);
  if(pushed==HIGH){
    return 1;
  }
  return 0;
}
