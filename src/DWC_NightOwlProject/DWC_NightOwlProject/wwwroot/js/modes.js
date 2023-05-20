const page = document.querySelector("body");
const toggler = document.querySelector(".toggler_input");
const togglerIcon = page.querySelector('.toggler_icon');

const sunIcon = `<svg fill="none" stroke="currentColor" stroke-width="1.5" viewBox="0 0 24 24" xmlns="http://www.w3.org/2000/svg" aria-hidden="true" class="toggler_icon">
  <path stroke-linecap="round" stroke-linejoin="round" d="M12 3v2.25m6.364.386l-1.591 1.591M21 12h-2.25m-.386 6.364l-1.591-1.591M12 18.75V21m-4.773-4.227l-1.591 1.591M5.25 12H3m4.227-4.773L5.636 5.636M15.75 12a3.75 3.75 0 11-7.5 0 3.75 3.75 0 017.5 0z" class="toggler_icon"></path>
</svg>`,
    moonIcon = `<svg fill="none" stroke="currentColor" stroke-width="1.5" viewBox="0 0 24 24" xmlns="http://www.w3.org/2000/svg" aria-hidden="true" class="toggler_icon">
                        <path stroke-linecap="round" stroke-linejoin="round" d="M21.752 15.002A9.718 9.718 0 0118 15.75c-5.385 0-9.75-4.365-9.75-9.75 0-1.33.266-2.597.748-3.752A9.753 9.753 0 003 11.25C3 16.635 7.365 21 12.75 21a9.753 9.753 0 009.002-5.998z" class="toggler_icon"></path>
                    </svg>`;

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
        togglerIcon.innerHTML = moonIcon;
        togglerIcon.alt = 'Switch to Dark Mode';
    }
    else {
        togglerIcon.innerHTML = sunIcon;
        togglerIcon.alt = 'Switch to Light Mode';
    }
}

function isTrue(value) {
    return value === "true";
}

toggler.addEventListener("change", toggleTheme);