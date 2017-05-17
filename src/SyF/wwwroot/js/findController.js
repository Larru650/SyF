//findController.js

(function () {

    angular.module("app-recipes")
            .controller("findController", findController);

    function findController($scope, $http) {


        $scope.test = "test";
        
        $scope.searchRecipe = function (ingredient) {
            $http.get("api/recipes/" + ingredient)
                  .then(function (response) {
                      $scope.newRecipe = response.data;

                  });
        };
        $scope.ingredient = "";
    }

})();