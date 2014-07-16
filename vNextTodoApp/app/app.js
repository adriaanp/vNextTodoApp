var app = angular.module('app', ['ngRoute'])

.config(function ($routeProvider) {
    $routeProvider
    .when('/', {
        controller: 'ListController',
        templateUrl: 'list.html'
    });
});