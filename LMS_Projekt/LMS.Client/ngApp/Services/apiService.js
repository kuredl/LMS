app.service('API', function API(Restangular, ngAuthSettings, $http, $q) {

    this.get = function (uri) {
        var deferred = $q.defer();
        $http({
            method: 'GET',
            url: ngAuthSettings.apiServiceBaseUri + uri
        }).success( function (data) {
            deferred.resolve(data);
        }).error(deferred.reject);
        return deferred.promise;        
    }
    this.post = function (uri) {
        Restangular.one(uri).post()
        .then(function (data) {
            return data;
        })
    }
    this.getFile = function (name, fileExtension) {
        return Restangular.one('api/file/getfile').withHttpConfig({ responseType: 'arraybuffer' }).get().then(function (response) {
            var blob = new Blob([response.data], {
            });
            console.log();
            var filename = response.headers('Content-Disposition').split(';')[1].trim().split('=')[1];
            //saveAs provided by FileSaver.js
            saveAs(blob, filename);
        })
    }
});