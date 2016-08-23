'use strict';
app.controller('homeController', ['$scope', 'authService', '$http', function ($scope, authService, $http) {
    $scope.pageClass = 'page-home';
    $scope.authentication = authService.authentication;

    $scope.homeInit = function () {
        return $http.get(serviceBase + 'api/home/get').then(function (results) {
            return results;
        });
    };
}]);