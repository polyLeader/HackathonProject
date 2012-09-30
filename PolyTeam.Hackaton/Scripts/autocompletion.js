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

    $('input#Street').on('input', function () {
        var findArray = new Object();
        var findCount = 0;
        var i;

        if ($(this).attr('value').length >= 2) {
            for (i = 0; i < resource.length; i++) {
                if (resource[i].Name.toLowerCase().indexOf($(this).attr('value').toLowerCase()) != -1 && resource[i].Lang == "uk") {
                    findArray["" + findCount] = resource[i].Name;
                    findCount++;
                }
            }

            if (!findCount) {
                for (i = 0; i < resource.length; i++) {
                    if (resource[i].Name.toLowerCase().indexOf($(this).attr('value').toLowerCase()) != -1 && resource[i].Lang == "ru") {
                        findArray["" + findCount] = resource[i].Name;
                        findCount++;
                    }
                }

                if (!findCount) {
                    for (i = 0; i < resource.length; i++) {
                        if (resource[i].Name.toLowerCase().indexOf($(this).attr('value').toLowerCase()) != -1 && resource[i].Lang == "en") {
                            findArray["" + findCount] = resource[i].Name;
                            findCount++;
                        }
                    }
                }
                
                $('#drop_list').html("");
            }
        } else {
            $('#drop_list').html("");
            return;
        }

        if (findCount) {
            var dropList = $('#drop_list');
            var selected = dropList.attr('class');

            dropList.html("");

            if (selected == null) {
                dropList.attr('class', "0");
                selected = 0;
            }

            for (var j = selected; j < selected + 4 && j < findCount; j++) {
                dropList.append('<div id="drop_element">' + findArray[j] + '</div>');
            }
        }
    });
});