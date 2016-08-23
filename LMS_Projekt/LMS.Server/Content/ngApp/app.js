var app = angular.module('LMSClient', ['ngRoute', 'LocalStorageModule', 'angular-loading-bar','ngAnimate']);

app.config(function ($routeProvider) {
    $routeProvider.when('/login', {
        controller: 'loginController',
        templateUrl: 'ngApp/views/login.html'
    });
    $routeProvider.when('/home', {
        controller: 'homeController',
        templateUrl: 'ngApp/views/home.html'
    });
    /*
    $routeProvider.when('/signup', {
        controller: 'signupController',
        templateUrl: 'app/views/signup.html'
    });
    $routeProvider.when('/orders', {
        controller: 'ordersController',
        templateUrl: 'app/views/orders.html'
    });
    */
    $routeProvider.otherwise({ redirectTo: '/login' });
});

//var serviceBase = 'http://localhost:8080/';
var serviceBase = 'http://' + location.host + '/';
console.log(serviceBase);

app.constant('ngAuthSettings', {
    apiServiceBaseUri: serviceBase,
    clientId: 'ngAuthApp'
});
app.config(function ($httpProvider) {
    $httpProvider.interceptors.push('authInterceptorService');
});

app.run(['authService', function (authService) {
    authService.fillAuthData();
}]);

