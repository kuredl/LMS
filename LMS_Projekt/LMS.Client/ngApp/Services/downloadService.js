app.service('Download', function Download(Restangular) {

    this.get = function (name, fileExtension) {
        return Restangular.one('api/file/getfile').withHttpConfig({ responseType: 'arraybuffer' }).get().then(function (data) {
            console.log(data)
            var blob = new Blob([data], {
            });
            //saveAs provided by FileSaver.js
            saveAs(blob, name +'.'+ fileExtension);
        })
    }
});