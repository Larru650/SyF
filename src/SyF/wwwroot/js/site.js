// Write your Javascript code.
(function(){

var $icon = $("#sidebarToggle i.fa");//we store italic that is classed with the font awesome prop. inside sidebarToggle

var $sidebarWrapper = $("#sidebar, #wrapper");

$("#sidebarToggle").on("click", function () {
    $sidebarWrapper.toggleClass("hide-sidebar");


    if ($sidebarWrapper.hasClass("hide-sidebar")) {

        $icon.removeClass("fa-angle-left");
        $icon.addClass("fa-angle-right");

    }
    else {
        $icon.addClass("fa-angle-left");
        $icon.removeClass("fa-angle-right");
    }


});
})();