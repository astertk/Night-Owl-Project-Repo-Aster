
function music() {
	const synth = new Tone.AMSynth().toDestination();

	const now = Tone.now();
	// start recording
	// generate a few notes
	var beat = 0.5;
	var beatValue = .2;
	var beatCount = 0;
	var songTime;
	var noteArray = ["G3", "Bb3", "Eb4", "G4", "Bb4", "Eb5", "F4", "G4", "Bb4", "Db5", "Eb5", "F5", "G4", "Bb4", "Eb4", "G3"];

	for (var i = 0; i < noteArray.length; i++) {

		synth.triggerAttackRelease(noteArray[i], "8n", now + beat);
		beat += beatValue;
		beatCount++;
	}

}


function record() {
	const recorder = new Tone.Recorder();
	const synth = new Tone.AMSynth().connect(recorder);
	recorder.start();

	const now = Tone.now();
	// start recording
	// generate a few notes
	var beat = 0.5;
	var beatValue = .2;
	var beatCount = 0;
	var songTime;
	var noteArray = ["G3", "Bb3", "Eb4", "G4", "Bb4", "Eb5", "F4", "G4", "Bb4", "Db5", "Eb5", "F5", "G4", "Bb4", "Eb4", "G3"];

	for (var i = 0; i < noteArray.length; i++) {

		synth.triggerAttackRelease(noteArray[i], "4n", now + beat);
		beat += beatValue;
		beatCount++;
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
}