const page = document.querySelector("body");
const toggler = document.querySelector(".toggler_input");
const togglerIcon = page.querySelector('.toggler_icon');


setCheckedState();

function setCheckedState() {
    //check if localStorage alread a 'checked' value set at all
    if (!(localStorage.checked === undefined)) {
        // if it does, it sets the state of the toggle accordingly
        toggler.checked = isTrue(localStorage.getItem("checked"));
        // after setting the toggle state, the theme is adjusted according to the checked state
        toggleTheme();
    }
}

function toggleTheme() {
    replaceClass();
    togglerIconTheme();
    // update the localStorage object with the current status of the toggler
    localStorage.setItem("checked", toggler.checked);
}


function replaceClass() {
    if (toggler.checked) {
        page.classList.replace('lightMode', 'darkMode');
    }
    else {
        page.classList.replace('darkMode', 'lightMode');
    }
}

function togglerIconTheme() {
    if (page.classList.contains('lightMode')) {
        togglerIcon.src = './Image/Moon.svg';
        togglerIcon.alt = 'Switch to Dark Mode';
    }
    else {
        togglerIcon.src = './Image/Sun.svg';
        togglerIcon.alt = 'Switch to Light Mode';
    }
}

function isTrue(value) {
    return value === "true";
}

toggler.addEventListener("change", toggleTheme);