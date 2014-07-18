angular.module('app')
.controller('ListController', function ($scope, $http) {
    $scope.tasks = [];
    $scope.newTask = {
        description: ''
    };

    $http.get('/tasks')
    .then(function (data) {
        $scope.tasks = data.data;
    });
    
    $scope.createTask = function () {
        if ($scope.newTask.description === '')
            return;

        $http.post('/tasks', JSON.stringify({ description: $scope.newTask.description } ))
        .then(function (data) {
            $scope.tasks.push(data.data);
        });
    };
});