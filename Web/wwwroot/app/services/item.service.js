app.service("itemService",
    function ($http, $q, baseService) {
        var url = '/api/item/';
        var request = {};

        save = function (item) {
            request = {};
            if (!item.id || item.id == 0)
                request = $http({ method: 'POST', url: url, params: item });
            else
                request = $http({ method: 'PUT', url: url, params: item });
            return (request.then(baseService.handleSuccess, baseService.handleError));
        }

        search = function (item) {
            request = $http({ method: 'GET', url: url, params: item });
            return (request.then(baseService.handleSuccess, baseService.handleError));
        }

        get = function (id) {
            request = $http({ method: 'GET', url: url, params: { id: id } });
            return (request.then(baseService.handleSuccess, baseService.handleError));
        }

        return ({
            save: save,
            search: search,
            get: get,
        });
    }
);