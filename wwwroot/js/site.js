document.addEventListener("DOMContentLoaded", function () {
    var currentUrl = window.location.pathname;
    if (currentUrl === "/") {
        currentUrl = "/trang-chu";
    }
    var navLinks = document.querySelectorAll(".navbar-nav a");
    navLinks.forEach(function (link) {
        if (link.getAttribute("href") === currentUrl) {
            link.parentNode.classList.add("active");
        }
    });
});
.pagination - centered
{
    display: inline - block;
    background - color : red;
    text - align: center;
}
.panel - footer.text - center
{
    background - color : red;
    text - align: center;
}
.navbar - inverse.navbar - nav > li.active > a,
.navbar - inverse.navbar - nav > li.active > a: hover,
.navbar - inverse.navbar - nav > li.active > a: focus
{
    color: #fff;
    background - color: #FF4C22;
    font - weight: bold;
}