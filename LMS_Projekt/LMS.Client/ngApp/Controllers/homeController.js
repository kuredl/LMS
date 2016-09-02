'use strict';
app.controller('homeController', ['$scope', '$http', 'API', function ($scope, $http, API) {
    $scope.pageClass = 'page-home';

    $scope.homeInit = function () {
        API.get('api/admin/GetAllCourses').then(function (data) {
            console.log(data);

        });
    };
}]);