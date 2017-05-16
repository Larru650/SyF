//app-recipes.js
(function () {

    "use strict";
    angular.module("app-recipes", ["ngRoute"])
        .config(function ($routeProvider) {

            $routeProvider.when("/", {
                controller: "homeController", //before recipes controller
                controllerAs: "vm",
                templateUrl: "views/homeView.html" //before recipesView
            })
                .when("/recipes", {
                    controller: "recipesController",
                    controllerAs: "vm",
                    templateUrl: "views/recipesView.html"
                })
            .when("/search", {
                controller: "findController",
                controllerAs: "vm",
                templateUrl: "views/findView.html"
            });
            $routeProvider.otherwise({ redirectTo: "/" });     
           
        });





})();