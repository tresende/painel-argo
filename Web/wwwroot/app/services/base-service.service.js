
app.factory('baseService', function ($http, $q, $rootScope) {
    handleError = function (response) {
        var unknownError = 'unknownError';
        // showModal($translate.instant('commonResource.caution'), response.data.exceptionMessage);
        if (!angular.isObject(response.data) || !response.data.message)
            return ($q.reject(unknownError));
        return ($q.reject(response.data.message));
    }

    handleSuccess = function (response) {
        return response.data;
    }

    return ({
        handleError: handleError,
        handleSuccess: handleSuccess
    });
});