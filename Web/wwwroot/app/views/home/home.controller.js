app.controller('homeController', [
    '$scope',
    'itemService',
    function ($scope, itemService) {
        itemService.get(1);
    }
]);
