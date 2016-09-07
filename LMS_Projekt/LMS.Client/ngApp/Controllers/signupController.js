'use strict';
app.controller('signupController', ['$scope', '$http', 'API', '$location', '$timeout', 'authService', function ($scope, $http, API, $location, $timeout, authService) {
    $scope.pageClass = 'page-home';
    $scope.savedSuccessfully = false;
    $scope.message = "";
    $scope._Users;
    $scope._Roles;
    
    $scope.registration = {
        userName: "",
        passWord: "",
        confirmPassword: ""
    };

    $scope.signUp = function () {

        authService.saveRegistration($scope.registration).then(function (response) {

            $scope.savedSuccessfully = true;
            $scope.message = "User has been registered successfully, you will be redicted to login page in 2 seconds.";
            startTimer();

        },
         function (response) {
             var errors = [];
             for (var key in response.data.modelState) {
                 for (var i = 0; i < response.data.modelState[key].length; i++) {
                     errors.push(response.data.modelState[key][i]);
                 }
             }
             $scope.message = "Failed to register user due to:" + errors.join(' ');
         });
    };

    var startTimer = function () {
        var timer = $timeout(function () {
            $timeout.cancel(timer);
            $location.path('/login');
        }, 2000);
    }

    $scope.getUsers = function () {
        API.get('api/admin/GetAllUsers').then(function (data) {
            console.log(data);
            for (var i = 0; i < data.length; i++) {
                data[i].selected = false;
            }
            $scope._Users = data;
        });
    };

    $scope.getRoles = function () {
        API.get('api/admin/GetRoles').then(function (data) {
            console.log(data);
            for (var i = 0; i < data.length; i++) {
                data[i].selected = false;
            }
            $scope._Roles = data;
        });
    };

    $scope.addRoles = function () {
        var roles = $scope._Roles;
        for (var i = 0; i < roles.length; i++) {
            console.log("looping");
            if (roles[i].selectedRole)
                addUserToRole(roles[i].id, $scope.selectedUser);
        }
        $scope.getRoles();

    };

    var addUserToRole = function (r1, u2) {
        console.log("hej");
        API.get('api/admin/AddUserToRole/?rId=' + r1 + '&uId=' + u2).then(function (data) {
            console.log(data);
            $scope._Roles = data;
        });
    };

}]);