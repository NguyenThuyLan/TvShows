document.getElementById("langDropdownParent").addEventListener("click", langDropdown);

function langDropdown() {
    var x = document.getElementById("langDropdown");
    if (x.style.visibility === "hidden") {
        x.style.visibility = "visible";
    } else {
        x.style.visibility = "hidden";
    }
}