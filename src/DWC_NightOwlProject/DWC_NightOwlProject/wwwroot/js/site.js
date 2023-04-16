var rToggle = 0;
var png;




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


    function uploading(){
   

        png = document.getElementById("fileInput");



    /*let formData = new FormData(e.target);
    formData.set('file', file, 'example.png');

    fetch('/', {
        method: 'POST',
        body: formData
    })
        .then(response => response.json())
        .then(data => console.log(data));*/
}
