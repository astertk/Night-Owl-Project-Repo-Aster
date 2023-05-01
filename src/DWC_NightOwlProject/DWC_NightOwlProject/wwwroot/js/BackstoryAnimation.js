let i = 0;
let p2 = "";
let press = 0;
let drawToggle = 0;
function setup() {
    createCanvas(windowWidth, windowHeight);
    background(255);
}

function draw() {
    if (drawToggle == 1) {
        /* const elem = document.getElementById("button");
     
         elem.onclick(alert("lets go"));*/



        /* if (press > 0) {*/


        var prologue = "It was night again. The Waystone Inn lay in silence, and it was a silence of three parts. The most obvious part was a hollow, echoing quiet, made by things that were lacking. If there had been a wind it would have sighed through the trees, set the inn’s sign creaking on its hooks, and brushed the silence down the road like trailing autumn leaves. If there had been a crowd, even a handful of men inside the inn, they would have filled the silence with conversation and laughter, the clatter and clamor one expects from a drinking house during the dark hours of night. If there had been music...but no, of course there was no music. In fact there were none of these things, and so the silence remained. Inside the Waystone a pair of men huddled at one corner of the bar. They drank with quiet determination, avoiding serious discussions of troubling news. In doing this they added a small, sullen silence to the larger, hollow one. It made an alloy of sorts, a counterpoint. The third silence was not an easy thing to notice. If you listened for an hour, you might begin to feel it in the wooden floor underfoot and in the rough, splintering barrels behind the bar. It was in the weight of the black stone hearth that held the heat of a long dead fire. It was in the slow back and forth of a white linen cloth rubbing along the grain of the bar. And it was in the hands of the man who stood there, polishing a stretch of mahogany that already gleamed in the lamplight. The man had true-red hair, red as flame. His eyes were dark and distant, and he moved with the subtle certainty that comes from knowing many things. The Waystone was his, just as the third silence was his. This was appropriate, as it was the greatest silence of the three, wrapping the others inside itself. It was deep and wide as autumn’s ending. It was heavy as a great river-smooth stone. It was the patient, cut-flower sound of a man who is waiting to die.";
        background(240);
        frameRate(10);
        pArray = split(prologue, " ");

        textSize(20);
        textFont("MedievalSharp");
        fill(255, 0, 0);
        stroke(255);
        // if(frameRate % 10 == 0){
        //   p2 += pArray[i];
        // }

        if (i < 360) {
            p2 += pArray[i] + " ";
        }

        textWrap(WORD);

        text(p2, width * .66, 200, 600, height);


        i++;


    }
}

function toggle() {
    drawToggle = 1;
}



