angular.module('app')
.controller('ListController', function ($scope, $http) {
    $scope.tasks = [];
    $scope.newTask = {
        description: ''
    };

    $http.get('/tasks')
    .then(function (data) {
        $scope.tasks = data.data;
    })
    .catch(function (e) {
        alert('Error: ' + e);
    });
    
    $scope.createTask = function () {
        if ($scope.newTask.description === '')
            return;

        $http.post('/tasks', { description: $scope.newTask.description })
        .then(function (data) {
            $scope.tasks.push(data.data);
            $scope.newTask.description = '';
        })
        .catch(function (e) {
            alert('Could not create task: ' + e);
        });
    };
});