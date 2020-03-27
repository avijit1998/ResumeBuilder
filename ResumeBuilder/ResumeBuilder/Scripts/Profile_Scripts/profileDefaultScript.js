// JavaScript file for Public Profile Page
$(document).ready(function () {

    var onResize = function () {
        // apply dynamic padding at the top of the body according to the fixed navbar height
        $("body").css("padding-top", $(".fixed-top").height());
    };

    // attach the function to the window resize event
    $(window).resize(onResize);

    // call it also when the page is ready after load or reload
    $(function () {
        onResize();
    });
});