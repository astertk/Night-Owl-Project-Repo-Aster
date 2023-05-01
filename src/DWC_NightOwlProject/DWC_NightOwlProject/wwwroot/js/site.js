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


