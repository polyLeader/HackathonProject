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

    $('input#Street').on('change', function () {

        var findCount = 0;

        var findArray = new Object();

        if ($(this).attr('value').length >= 2) {
            for (var i = 0; i < resource.length; i++) {
                if (resource[i].Name.indexOf($(this).attr('value')) != -1) {
                    findArray[findCount] = resource[i].Name;
                    findCount++;
                }
            }
        }

        if (findCount) {
            var dropList = $('#drop_list');
            var selected = dropList.attr('class');

            $('#drop_element').remove();

            if (selected == null) {
                dropList.attr('class', "0");
                selected = 0;
            }

            for (var j = selected; j < selected + 4; j++) {
                dropList.append('<div id="drop_element">');
                dropList.append(resource[j].Name);
                dropList.append('</div>');
            }
        }
    });
});