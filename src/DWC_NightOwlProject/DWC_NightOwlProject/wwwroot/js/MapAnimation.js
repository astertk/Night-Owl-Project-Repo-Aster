let leftX;
let leftY;
let rightX;
let rightY;
let drawToggle = 0;

function preload() {
    left = loadImage('css/left-shoe-footprint.png');
    right = loadImage('css/right-shoe-footprint.png');
}
//

function setup() {
    background(255);
    createCanvas(windowWidth, windowHeight);
    angleMode(DEGREES);
    leftX = 1;
    rightX = 0;
    leftY = height / 2;
    rightY = height / 2;
    
}

function draw() {

    

    if (drawToggle == 1) { 
    
  


    rightRate = frameCount + 60;
    leftRate = frameCount;



    image(left, leftX + 50, leftY - 50, 100, 100);
    image(right, rightX, rightY, 100, 100);


    if (leftRate % 120 == 0) {
        leftX += 150;

    }

    if (rightRate % 120 == 0) {
        rightX += 150;

    }


    }
}


function toggle() {
    drawToggle = 1;
}








