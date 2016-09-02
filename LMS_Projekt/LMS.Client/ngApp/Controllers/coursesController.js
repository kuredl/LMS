'use strict';
app.controller('coursesController', ['$scope', '$http', 'API', function ($scope, $http, API) {
    $scope.pageClass = 'page-home';
    $scope._Courses;
    $scope._Users;
    //@$scope._CourseAttendants;

    $scope.getAllCourses = function () {
        API.get('api/admin/GetAllCourses').then(function (data) {
            console.log(data);
            $scope._Courses = data;
        });
    };

    $scope.getAllUsers = function () {
        API.get('api/admin/GetAllUsers').then(function (data) {
            console.log(data);
            for (var i = 0; i < data.length; i++) {
                data[i].selected = false;
            }
            $scope._Users = data;
        });
    };

    $scope.addUsers = function () {
        var users = $scope._Users;
        for (var i = 0; i < users.length; i++) {
            if (users[i].selected)
                addUserToCourse($scope.selectedCourse, users[i].id);
        }
        $scope.getAllUsers(); 

    }
    var addUserToCourse = function (c1, u2){
        API.get('api/admin/AddUserToCourse/?cId=' + c1 + '&uId=' + u2).then(function (data) {
            console.log(data);
            $scope._CourseAttendants = data;
       });
    };

    $scope.addNewCourse = function (c1) {
        API.post('api/admin/AddNewCourse/',c1).then(function (data) {
            console.log(data);
            $scope._Courses = data;
        });
    };

    $scope.deleteCourse = function (del) {
        API.post('api/admin/DeleteCourse/', del).then(function (data) {
            console.log(data);
            $scope._Courses = data;
        });
    };
}]);