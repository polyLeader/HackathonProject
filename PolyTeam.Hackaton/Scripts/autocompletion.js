$(document).ready(function () {

    var resource;
    $.ajax({
        'dataType': "JSON",
        'type': 'GET',
        'url': '/Map/GetAllStreets',
        'success': function (res) {
            resource = res;
        }
    });
    $('input#Street').change(function () {
        if (this.length > 2) {
            for (var i = 0; i < resource.length; i++) {
                if (resource.Lang == "ru") {
                    
                }
            }
        }
    });
});