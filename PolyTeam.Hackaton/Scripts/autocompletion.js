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

    $("input#Flat,input#House").prop("disabled", "true");
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
            if ($(this).attr('value').length == 0) {
                $("input#Flat,input#House").prop("disabled", "true");
            }
            $('#drop_list').html("");
            return;
        }

        if (findCount) {
            var dropList = $('#drop_list');
            var selected = dropList.attr('class');
            $("#drop_list").show();

            dropList.html("");

            if (selected == null) {
                dropList.attr('class', "0");
                selected = 0;
            }

            for (var j = selected; j < selected + 4 && j < findCount; j++) {
                dropList.append('<div class="drop_element">' + findArray[j] + '</div>');
            }
        }
        $(".drop_element:first-child").addClass('active');
    });
    $(document).on('click', '.drop_element', function () {
        $("input#Street").val($(this).text());
        $("#drop_list").hide();
        $('input#House,input#Flat').removeAttr('disabled');
    });
    $(document).on('blur', 'input#Street', function () {
        $(this).val($(".drop_element.active").text());
        $("#drop_list").hide();
        $('input#House,input#Flat').removeAttr('disabled');
    });
    $(document).on('blur', 'input#House', function () {
        if ($('input#House').val().length != 0 && $('input#Street').val().length != 0)
            $.ajax({
                'dataType': 'JSON',
                'type': 'GET',
                'data': { 'street': $('input#Street').val(), 'number': $('input#House').val() },
                'url': '/Map/GetCoordinates',
                'success': function (respon) {
                    if (respon != null) {
                        var marker = L.marker([respon.Lat, respon.Lon]).addTo(map);
                        map.setView(new L.LatLng(respon.Lat, respon.Lon), 18);
                    }
                }
            });
    });
});