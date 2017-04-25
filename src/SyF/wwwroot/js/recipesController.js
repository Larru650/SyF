//recipesController.js
(function () {

    "use strict";
    angular.module("app-recipes")
        .controller("recipesController", recipesController);

    function recipesController($http) //we call the http service(similarly to how we call it injecting it in the ctor in asp.net
    {
        var vm = this;
        vm.recipes = [];
        vm.newRecipe = {};
        vm.errorMessage = ""; //we can create members in the vm that represent non functional data
        vm.isBusy = true;
        $http.get("api/recipes")
            .then(function (response) {
                
                angular.copy(response.data, vm.recipes); // it has been already converted from json to an object graph for us
               
            }, function (error) {

                vm.errorMessage = "Failed to load data: " + error;


            }).finally(function () {
                vm.isBusy = false;
                
            });
             
          //this returns a promise object, 1st success,2nd failure     

        vm.addRecipe = function () {
           

            vm.isBusy = true;
            vm.errorMessage = "";
            //posting new recipe to the server
            $http.post("api/recipes", vm.newRecipe)
                .then(function(response){
                
                    vm.recipes.push(response.data);
                    vm.newRecipe={};
                }, function(error){
                
                    vm.errorMessage = "Failed to save new trip";
                }).finally(function(){

                    vm.isBusy=false;
                });
            };

    }















})();