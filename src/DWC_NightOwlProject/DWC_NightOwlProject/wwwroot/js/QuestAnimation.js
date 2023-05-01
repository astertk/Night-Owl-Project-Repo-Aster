//This example is a modification of code from 'Session 6: Motion: Move and choreograph shapes...' from Kadenze

let angle = 0;
let offset;//provides a constant value that offsets the y position

let scalar = 60; //effects the amplitude of the sine wave (how far from the offset)
let spd = 0.07; //effects the spd of the motion
let drawToggle = 0;


function setup() {
    createCanvas(windowWidth, windowHeight);
    frameRate(60);
}

function draw() {
    background(255);
    if (drawToggle == 1) {

        offset = height * .8;


        let y = offset + sin(angle) * scalar;

        for (let i = 15; i < 30; i++) {

            if (frameCount > 90) {

                // frameCount++;
                fill(128, 0, 0);
                ellipse(i * 100 % frameCount, offset + sin(angle + 0.25 * i) * scalar, 50, 50);
                fill(205, 0, 50);
                ellipse(i * 100 % frameCount, offset + sin(angle + 0.5 * i) * scalar, 50, 50);
                fill(255, 153, 0);
                ellipse(i * 100 % frameCount, offset + sin(angle + 0.75 * i) * scalar, 50, 50);
                fill(255, 255, 102);
                ellipse(i * 100 % frameCount, offset + sin(angle + 1 * i) * scalar, 50, 50);
                noStroke();
                fill(255, 255, 102);
                //fireball();

            }
        }

        angle += spd; //increment the angle each time through draw(), see what happens if you change this!
    }
}


function fireball() {
    for (let i = 0; i < 10; i++) {

        if (frameCount > 90) {

            background(0);
            ellipse(i * 100 % frameCount, height / 2, 100, 100);
        }
    }

}

function toggle() {
    drawToggle = 1;
}


