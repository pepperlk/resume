var app = angular.module('me', ['ui.router', 'ngRoute']);
app.config(function ($stateProvider) {
    $stateProvider
      .state('index', {
          url: "",
          views: {
              "root": { templateUrl: "/App/root.html" },
              
          }
      });
    
});

app.filter('iif', function () {
    return function (input, trueValue, falseValue) {
        return input ? trueValue : falseValue;
    };
});

app.controller('rootCtrl', function ($scope, $http) {
    $http.get('/api/resume').success(function (data) {
        $scope.resume = data;
    })
});


app.directive("stars", function () {
    var template = '<span class="stars">' +
            '' +
          '</span>';
    return {
        restrict: 'E',
        transclude: true,
        scope: {},
        controller: function ($scope, $element) {
           
        },
        link: function (scope, element, attrs) {
            var output = '';

            var total = attrs.total;
            var val = attrs.val;
            for (var i = 0; i < total; i++) {
                if (i < val) {
                    output += '<i class="fa fa-star"></i>';
                }
                else {
                    output += '<i class="fa fa-star-o"></i>';
                }


            }

            element.append(output);

        },
        template: template,
        replace: true
    };
});