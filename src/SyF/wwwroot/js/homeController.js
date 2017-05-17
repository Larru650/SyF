//homeController.js

(function () {

    "use strict";


    angular.module("app-recipes")
        .controller("homeController", homeController);


    function homeController($location) {


        var vm = this;

        vm.redirect = function () {

            $location.path("/recipes");

        };

        vm.redirectToSearch = function () {

            $location.path("/search");
        };
 }
})();