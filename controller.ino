
#include <Bounce.h>

#define PININ A8

Bounce button0 = Bounce(11, 10); //10 = 10ms debounce time
Bounce button1 = Bounce(8, 10);


void setup() {
  Serial.begin(9600);
  
pinMode(PININ, INPUT);
pinMode(11, INPUT_PULLUP);
pinMode(8, INPUT_PULLUP);




}
 
void loop() {

  button0.update();
button1.update();

if(button0.fallingEdge()){
  Keyboard.print(" ");
}
if(button1.fallingEdge()){
  Keyboard.print("z");
}



  //Serial.println(analogRead(PININ));
  delay(100);

  
  }
