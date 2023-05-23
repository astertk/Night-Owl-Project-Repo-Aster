var rToggle = 0;
var png;

function showselectbackstory(){
    document.getElementById("Backstory").innerHTML=document.getElementById("BackstoryListSelect").value;
}
function showselectquest(){
    document.getElementById("Quest").innerHTML=document.getElementById("QuestListSelect").value;
}
function showselectencounter(){
    document.getElementById("Encounter").innerHTML=document.getElementById("EncounterListSelect").value;
}



function reveal(id, id2) {

    

    var myDiv = document.getElementById(id);



    if (rToggle == 0) {

        myDiv.style.display = 'block';
        rToggle = 1;
    }

    else {
        myDiv.style.display = 'none'
        rToggle = 0;
    }

    var hide = document.getElementById(id2);

    hide.style.display = 'none';

  

}


function uploading() {
}





/*

document.forms.edit_image_form.addEventListener('submit', e => {
        e.preventDefault();

        let png = ['<?xml version="1.0"?><png xmlns="http://www.w3.org/2000/png" width="200" height="200"><rect fill="blue" width="200" height="200" x="0" y="0"/></png>'];
        let file = new File(png, 'example.png', { type: 'image/png' });

        let formData = new FormData(e.target);
        formData.set('file', file, 'example.png');

        fetch('/', {
            method: 'POST',
            body: formData
        })
            .then(response => response.json())
            .then(data => console.log(data));
    });*/

/*Code from https://blog.shahednasser.com/how-to-style-an-audio-element/ */
const playerButton = document.querySelector('.player-button'),
    audio = document.querySelector('audio'),
    timeline = document.querySelector('.timeline'),
    soundButton = document.querySelector('.sound-button'),
    playIcon = `
        <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 20 20" fill="#b8a3a5">
    <path fill-rule="evenodd" d="M10 18a8 8 0 100-16 8 8 0 000 16zM9.555 7.168A1 1 0 008 8v4a1 1 0 001.555.832l3-2a1 1 0 000-1.664l-3-2z" clip-rule="evenodd" />
  </svg>
      `,
    pauseIcon = `
        <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 20 20" fill="#b8a3a5">
  <path fill-rule="evenodd" d="M18 10a8 8 0 11-16 0 8 8 0 0116 0zM7 8a1 1 0 012 0v4a1 1 0 11-2 0V8zm5-1a1 1 0 00-1 1v4a1 1 0 102 0V8a1 1 0 00-1-1z" clip-rule="evenodd" />
</svg>
      `,
    soundIcon = `
      <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 20 20" fill="#b8a3a5">
  <path fill-rule="evenodd" d="M9.383 3.076A1 1 0 0110 4v12a1 1 0 01-1.707.707L4.586 13H2a1 1 0 01-1-1V8a1 1 0 011-1h2.586l3.707-3.707a1 1 0 011.09-.217zM14.657 2.929a1 1 0 011.414 0A9.972 9.972 0 0119 10a9.972 9.972 0 01-2.929 7.071 1 1 0 01-1.414-1.414A7.971 7.971 0 0017 10c0-2.21-.894-4.208-2.343-5.657a1 1 0 010-1.414zm-2.829 2.828a1 1 0 011.415 0A5.983 5.983 0 0115 10a5.984 5.984 0 01-1.757 4.243 1 1 0 01-1.415-1.415A3.984 3.984 0 0013 10a3.983 3.983 0 00-1.172-2.828 1 1 0 010-1.415z" clip-rule="evenodd" />
</svg>`,
    muteIcon = `
      <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 20 20" fill="#b8a3a5">
  <path fill-rule="evenodd" d="M9.383 3.076A1 1 0 0110 4v12a1 1 0 01-1.707.707L4.586 13H2a1 1 0 01-1-1V8a1 1 0 011-1h2.586l3.707-3.707a1 1 0 011.09-.217zM12.293 7.293a1 1 0 011.414 0L15 8.586l1.293-1.293a1 1 0 111.414 1.414L16.414 10l1.293 1.293a1 1 0 01-1.414 1.414L15 11.414l-1.293 1.293a1 1 0 01-1.414-1.414L13.586 10l-1.293-1.293a1 1 0 010-1.414z" clip-rule="evenodd" />
</svg>`;

function toggleAudio() {
    if (audio.paused) {
        audio.play();
        playerButton.innerHTML = pauseIcon;
    } else {
        audio.pause();
        playerButton.innerHTML = playIcon;
    }
}

playerButton.addEventListener('click', toggleAudio);

/*function changeTimelinePosition() {
    const percentagePosition = (100 * audio.currentTime) / audio.duration;
    timeline.style.backgroundSize = `${percentagePosition}% 100%`;
    timeline.value = percentagePosition;
}*/

//audio.ontimeupdate = changeTimelinePosition;

function audioEnded() {
    playerButton.innerHTML = playIcon;
}

audio.onended = audioEnded;

function changeSeek() {
    const time = (timeline.value * audio.duration) / 100;
    audio.currentTime = time;
}

//timeline.addEventListener('change', changeSeek);

function toggleSound() {
    audio.muted = !audio.muted;
    soundButton.innerHTML = audio.muted ? muteIcon : soundIcon;
}

soundButton.addEventListener('click', toggleSound);

function changeBiomePic() {
    var biomePic = document.getElementById("bgImgBiome").value;
    console.log(biomePic);
    if (biomePic === "Arctic") {
        document.body.style.position = "absolute";
        document.body.style.zIndex = "1";
        document.body.style.backgroundImage = "linear-gradient(rgba(0, 0, 0, 0.5), rgba(0, 0, 0, 0.5)), url('../biome photos/Ice_landscape.png')";
        document.getElementById("biomeLabel").style.color = "#b1b1b1";
        document.getElementById("typeLabel").style.color = "#b1b1b1";
        console.log(biomePic);
    }
    else if (biomePic === "Coastal") {
        document.body.style.position = "absolute";
        document.body.style.zIndex = "1";
        document.body.style.backgroundImage = "linear-gradient(rgba(0, 0, 0, 0.5), rgba(0, 0, 0, 0.5)), url('../biome photos/sbc-0005.jpg')";
        document.getElementById("biomeLabel").style.color = "#b1b1b1";
        document.getElementById("typeLabel").style.color = "#b1b1b1";
        console.log(biomePic);
    }
    else if (biomePic === "Desert") {
        document.body.style.position = "absolute";
        document.body.style.zIndex = "1";
        document.body.style.backgroundImage = "linear-gradient(rgba(0, 0, 0, 0.5), rgba(0, 0, 0, 0.5)), url('../biome photos/Otherworldly-Incantations-Desert-Worldbuidling-Compound-Dune.jpg')";
        document.getElementById("biomeLabel").style.color = "#b1b1b1";
        document.getElementById("typeLabel").style.color = "#b1b1b1";
        console.log(biomePic);
    }
    else if (biomePic === "Grassland") {
        document.body.style.position = "absolute";
        document.body.style.zIndex = "1";
        document.body.style.backgroundImage = "linear-gradient(rgba(0, 0, 0, 0.5), rgba(0, 0, 0, 0.5)), url('../biome photos/Plains.webp')";
        document.getElementById("biomeLabel").style.color = "#b1b1b1";
        document.getElementById("typeLabel").style.color = "#b1b1b1";
        console.log(biomePic);
    }
    else if (biomePic === "Forest") {
        document.body.style.position = "absolute";
        document.body.style.zIndex = "1";
        document.body.style.backgroundImage = "linear-gradient(rgba(0, 0, 0, 0.5), rgba(0, 0, 0, 0.5)), url('../biome photos/A-temperate-forest-in-the-Northern-Hemisphere.jpg')";
        document.getElementById("biomeLabel").style.color = "#b1b1b1";
        document.getElementById("typeLabel").style.color = "#b1b1b1";
        console.log(biomePic);
    }
    else if (biomePic === "Hill") {
        document.body.style.position = "absolute";
        document.body.style.zIndex = "1";
        document.body.style.backgroundImage = "linear-gradient(rgba(0, 0, 0, 0.5), rgba(0, 0, 0, 0.5)), url('../biome photos/claudio-testa--SO3JtE3gZo-unsplash.jpg')";
        document.getElementById("biomeLabel").style.color = "#b1b1b1";
        document.getElementById("typeLabel").style.color = "#b1b1b1";
        console.log(biomePic);
    }
    else if (biomePic === "Mountain") {
        document.body.style.position = "absolute";
        document.body.style.zIndex = "1";
        document.body.style.backgroundImage = "linear-gradient(rgba(0, 0, 0, 0.5), rgba(0, 0, 0, 0.5)), url('../biome photos/13-1920x1080-f76ef21af8de8ac97ad89c7f62925321.png')";
        document.getElementById("biomeLabel").style.color = "#b1b1b1";
        document.getElementById("typeLabel").style.color = "#b1b1b1";
        console.log(biomePic);
    }
    else if (biomePic === "Swamp") {
        document.body.style.position = "absolute";
        document.body.style.zIndex = "1";
        document.body.style.backgroundImage = "linear-gradient(rgba(0, 0, 0, 0.5), rgba(0, 0, 0, 0.5)), url('../biome photos/Marsh.webp')";
        document.getElementById("biomeLabel").style.color = "#b1b1b1";
        document.getElementById("typeLabel").style.color = "#b1b1b1";
        console.log(biomePic);
    }
    else if (biomePic === "Underground") {
        document.body.style.position = "absolute";
        document.body.style.zIndex = "1";
        document.body.style.backgroundImage = "linear-gradient(rgba(0, 0, 0, 0.5), rgba(0, 0, 0, 0.5)), url('../biome photos/underground_dragon_city_jpg_photo_large_by_peigong-d7at23k.jpg')";
        document.getElementById("biomeLabel").style.color = "#b1b1b1";
        document.getElementById("typeLabel").style.color = "#b1b1b1";
        console.log(biomePic);
    }
    else if (biomePic === "Underwater") {
        document.body.style.position = "absolute";
        document.body.style.zIndex = "1";
        document.body.style.backgroundImage = "linear-gradient(rgba(0, 0, 0, 0.5), rgba(0, 0, 0, 0.5)), url('../biome photos/underwater_kingdom_by_silberius_d6vdtk0-fullview.png')";
        document.getElementById("biomeLabel").style.color = "#b1b1b1";
        document.getElementById("typeLabel").style.color = "#b1b1b1";
        console.log(biomePic);
    }
    else if (biomePic === "Urban") {
        document.body.style.position = "absolute";
        document.body.style.zIndex = "1";
        document.body.style.backgroundImage = "linear-gradient(rgba(0, 0, 0, 0.5), rgba(0, 0, 0, 0.5)), url('../biome photos/Urban.png')";
        document.getElementById("biomeLabel").style.color = "#b1b1b1";
        document.getElementById("typeLabel").style.color = "#b1b1b1";
        console.log(biomePic);
    }
}