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



