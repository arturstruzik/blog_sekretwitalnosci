$(document).ready(function () {

    var x = document.getElementsByClassName("navbar-static-top-link");
    var numOfElem = x.length;
    for (var n = 0; n < numOfElem; n++) {
        if (window.location.href == x[n].href && window.innerWidth > 1024) {
            x[n].style.background = "white";
            x[n].style.color = "grey";
            x[n].style.boxShadow = "0px -5px 13px -5px rgba(0,0,0,0.75)";
        }
    }

    $(document).scroll(function () {
     
        if ($(window).scrollTop() > 50) {
            $(".navbar-fixed-top").css('top', '0px');
            $(".navbar-fixed-top").css('position', 'fixed');
        }
        if ($(window).scrollTop() <= 50) {
            $(".navbar-fixed-top").css('top', '50px');
            $(".navbar-fixed-top").css('position', 'absolute');
        }
    });

    $(".icon-search").on('click', function () {
        $('.search-box').fadeToggle();
    });
});