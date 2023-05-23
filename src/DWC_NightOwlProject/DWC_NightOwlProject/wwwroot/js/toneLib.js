var musicToggle = 0;


function mToggle() {
	if (musicToggle == 0) {
		musicToggle = 1;
	}

/*	if (musicToggle == 1) {
		musicToggle = 0;
	}*/

}

function pause() {
	location.reload();
}



function music(noteString, instrumentID, rateID) {
	var audioCtx = new AudioContext();
	audioCtx.close();

	if (musicToggle == 1) {
		alert("Another Song is currently playing. Please stop the song to play another.");
	}

	if (musicToggle == 0) {
		mToggle();

		var synth;
		var beatValue;

		switch (rateID) {
			case 0:
				beatValue = 0.5;
				break;

			case 1:
				beatValue = 0.3
				break;

			case 2:
				beatValue = 0.1
				break;
		}

		switch (instrumentID) {
			case 0:
				synth = new Tone.AMSynth().toDestination();
				break;
			case 1:
				synth = new Tone.DuoSynth().toDestination();
				break;
			case 2:
				synth = new Tone.FMSynth().toDestination();
				break;
			case 3:
				synth = new Tone.MembraneSynth().toDestination();
				break;
			case 4:
				synth = new Tone.MetalSynth().toDestination();
				break;
			case 5:
				synth = new Tone.MonoSynth().toDestination();
				break;
			case 6:
				synth = new Tone.NoiseSynth().toDestination();
				break;
			case 7:
				synth = new Tone.PluckSynth().toDestination();
				break;

		}

		//const synth = new Tone.AMSynth().toDestination();

		const now = Tone.now();
		// start recording
		// generate a few notes
		var beat = 0.5;
		//var beatValue = 0.5;
		var beatCount = 0;
		var songTime;

		var noteArray = noteString.split(" ");

		//noteArray = ["C4","Eb4","G4","Bb4"];

		//var noteArray = ["G3", "Bb3", "Eb4", "G4", "Bb4", "Eb5", "F4", "G4", "Bb4", "Db5", "Eb5", "F5", "G4", "Bb4", "Eb4", "G3"];

		for (var i = 0; i < noteArray.length; i++) {

			if (musicToggle == 1) {
				synth.triggerAttackRelease(noteArray[i], "8n", now + beat);
				beat += beatValue;
				beatCount++;
			}
			else {
				mToggle();
			}
		}


		songTime = (beatValue * beatCount * 1000) + 1000;
		setTimeout(playing, songTime);
	}


	
}

function playing() {
	alert("Done playing!");
	musicToggle = 0;
}

function downloading() {
	alert("Download finished.");
	musicToggle = 0;
}


function record(noteString, instrumentID, rateID) {
	if (musicToggle == 1) {
		alert("Another Song is currently downloading. Please wait.");
	}
	if (musicToggle == 0) {
		alert("Download in progress");
		mToggle();
		const recorder = new Tone.Recorder();
		var synth;
		var beatValue;
		switch (rateID) {
			case 0:
				beatValue = 0.5;
				break;

			case 1:
				beatValue = 0.3
				break;

			case 2:
				beatValue = 0.1
				break;
		}
		switch (instrumentID) {
			case 0:
				synth = new Tone.AMSynth().connect(recorder);
				break;
			case 1:
				synth = new Tone.DuoSynth().connect(recorder);
				break;
			case 2:
				synth = new Tone.FMSynth().connect(recorder);
				break;
			case 3:
				synth = new Tone.MembraneSynth().connect(recorder);
				break;
			case 4:
				synth = new Tone.MetalSynth().connect(recorder);
				break;
			case 5:
				synth = new Tone.MonoSynth().connect(recorder);
				break;
			case 6:
				synth = new Tone.NoiseSynth().connect(recorder);
				break;
			case 7:
				synth = new Tone.PluckSynth().connect(recorder);
				break;

		}
		//const synth = new Tone.AMSynth().connect(recorder);
		recorder.start();

		const now = Tone.now();
		// start recording
		// generate a few notes
		var beat = 0.5;
		//var beatValue = .2;
		var beatCount = 0;
		var songTime;
		//var noteArray = ["G3", "Bb3", "Eb4", "G4", "Bb4", "Eb5", "F4", "G4", "Bb4", "Db5", "Eb5", "F5", "G4", "Bb4", "Eb4", "G3"];

		var noteArray = noteString.split(" ");

		for (var i = 0; i < noteArray.length; i++) {
			if (musicToggle == 1) {

				synth.triggerAttackRelease(noteArray[i], "4n", now + beat);
				beat += beatValue;
				beatCount++;
			}
		}

		songTime = (beatValue * beatCount * 1000) + 1000;
		// wait for the notes to end and stop the recording
		setTimeout(async () => {
			// the recorded audio is returned as a blob
			const recording = await recorder.stop();
			// download the recording by creating an anchor element and blob url
			const url = URL.createObjectURL(recording);
			const anchor = document.createElement("a");
			anchor.download = "new_song.mp3";
			anchor.href = url;
			anchor.click();
		}, songTime);
		setTimeout(downloading, songTime);
	}
	
}