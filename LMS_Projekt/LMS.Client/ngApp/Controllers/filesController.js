'use strict';
app.controller('filesController', ['$scope', 'authService', '$http', 'Download', 'API','$timeout', function ($scope, authService, $http, Download, API, $timeout) {

    $scope.currentFolder;

    $scope.initFiles = function () {
        API.get('api/file/getfolders').then(function (data) {
            $scope.currentFolder = data;
        });
    };
    $scope.downloadFile = function (file) {
        API.getFile('stuff', 'txt');
    };
    $scope.test = function () {
        console.log($scope.currentFolder);
    }
}]);