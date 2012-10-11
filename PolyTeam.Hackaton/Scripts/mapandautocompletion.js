$(document).ready(function () {
    var southWest = new L.LatLng(48.136, 37.483);
    var northEast = new L.LatLng(47.837, 38.041);

    var donetskBounds = new L.LatLngBounds(southWest, northEast);

    var map = L.map('map', {
        center: new L.LatLng(47.99874, 37.80466),
        zoom: 14
    });

    var marker = new L.Marker(map.locate(), { draggable: true });
    map.addLayer(marker);

    L.tileLayer('http://{s}.tile.cloudmade.com/e37d73e201f94dd78191e2470055aec0/997/256/{z}/{x}/{y}.png', {
        attribution: 'Допомога ЖКГ'
    }).addTo(map);

    map.locate({ setView: true, maxZoom: 18 });

    map.setMaxBounds(donetskBounds);

    map.on('click', onClickAndDragMarker);
    marker.on('dragend', onClickAndDragMarker);

    function onClickAndDragMarker(event) {
        var lat = 0.0;
        var lng = 0.0;

        if (event.type == 'click') {
            lat = event.latlng.lat;
            lng = event.latlng.lng;
        } else if (event.type == 'dragend') {
            lat = event.target._latlng.lat;
            lng = event.target._latlng.lng;
        }

        $.ajax({
            'dataType': 'JSON',
            'type': 'GET',
            'data': { 'lat': lat, 'lon': lng },
            'url': '/Map/GetObject',
            'success': function (response) {
                if (response != null) {
                    $('input#House, input#Flat, select#ProblemId').removeAttr('disabled');

                    marker.setLatLng([lat, lng]);
                    map.setView(new L.LatLng(lat, lng), 18);

                    $('input#Street').val(response.Road);
                    if (response.HouseNumber != null) {
                        $('input#House').val(response.HouseNumber);
                        $('input#Flat').focus();
                    } else {
                        $('input#House').val("");
                        $('input#House').focus();
                    }
                }
            }
        });
    }

    // End map------------

    var resource;

    $.ajax({
        'dataType': "JSON",
        'type': 'GET',
        'url': '/Map/GetAllStreets',
        'success': function (res) {
            resource = res;
        }
    });

    var elementsNum = 10;
    var menuClicked = false;
    var streetFound = false;

    $('input#Street').attr('autocomplete', 'off').focus();
    $('input#House, input#Flat, select#ProblemId').attr('disabled', 'disabled');

    $('input#Street').on('input', function () {
        var findArray = new Object();

        streetFound = false;

        var findCount = 0;
        var i;

        var dropList = $('#drop_list');

        if ($(this).attr('value').length > 0) {

            for (i = 0; i < resource.length; i++) {
                if (resource[i].Name.toLowerCase().indexOf($(this).attr('value').toLowerCase()) != -1 && resource[i].Lang == "uk") {
                    findArray["" + findCount] = resource[i].Name;
                    findCount++;
                }
            }

            if (findCount == 0) {
                for (i = 0; i < resource.length; i++) {
                    if (resource[i].Name.toLowerCase().indexOf($(this).attr('value').toLowerCase()) != -1 && resource[i].Lang == "ru") {
                        findArray["" + findCount] = resource[i].Name;
                        findCount++;
                    }
                }

                if (findCount == 0) {
                    for (i = 0; i < resource.length; i++) {
                        if (resource[i].Name.toLowerCase().indexOf($(this).attr('value').toLowerCase()) != -1 && resource[i].Lang == "en") {
                            findArray["" + findCount] = resource[i].Name;
                            findCount++;
                        }
                    }
                }
            }

            dropList.html("");
        }
        else {
            $('input#House, input#Flat, select#ProblemId').attr('disabled', 'disabled');
            dropList.html('');
            return;
        }

        if (findCount) {
            for (var j = 0; j < elementsNum && j < findCount; j++) {
                dropList.append('<div class="drop_element">' + findArray[j] + '</div>');
                menuClicked = false;
            }
            streetFound = true;
        }
    });

    $('input#Street').on('keydown', function (e) {
        if ($('.drop_element').index() == -1) {
            return;
        }

        if (e.which == 38 || e.which == 40) {
            var iterator = e.which - 40 + 1;

            var current = $('.drop_element.active');

            var idx;

            var nums = $('.drop_element:last').index();

            idx = current.index();

            if (idx + iterator > nums || idx + iterator < 0) return;

            current.removeClass('active');

            if (iterator == -1 && idx < 0) {
                idx = nums;
            }
            else {
                idx += iterator;
            }

            var next = $('.drop_element:eq(' + idx + ')').addClass('active');

            $(this).val(next.text());

            if (iterator == -1) {
                e.preventDefault();
            }
        }
    });

    $('form').submit(function () {
        if ($('#drop_list:visible') != -1) {
            $('#drop_list').html('');
            $('input#House, input#Flat, select#ProblemId').removeAttr('disabled');
            menuClicked = true;
            $('input#House').focus();
            return false;
        }
        return true;
    });

    $('input#Street').on('blur', function () {
        if ($('.drop_element:hover').index() == -1) {
            var element = $('.drop_element.active');

            if (element.index() != -1) {
                $('#drop_list').html('');
                $('input#House, input#Flat, select#ProblemId').removeAttr('disabled');
                $('input#House').focus();
            } else if (!streetFound | !menuClicked) {
                $('input#House, input#Flat, select#ProblemId').attr('disabled', 'disabled');
            }
            
            $('#drop_list').hide();
        }
    });

    $('input#Street').on('focus', function () {
        $('#drop_list').show();
    });

    $('#drop_list').on('click', function () {
        var street = $('.drop_element:hover').text();
        $('input#Street').val(street);
        $('#drop_list').html('');
        $('input#House, input#Flat, select#ProblemId').removeAttr('disabled');
        $('input#House').focus();
    });

    $('#map').on('click', function () {
        $('#drop_list').html('');
    });

    $('input#House').on('blur', function () {
        if ($('input#House').val().length != 0 && $('input#Street').val().length != 0) {
            $.ajax({
                'dataType': 'JSON',
                'type': 'GET',
                'data': { 'street': $('input#Street').val(), 'number': $('input#House').val() },
                'url': '/Map/GetCoordinates',
                'success': function (response) {
                    if (response != null) {
                        marker.setLatLng([response.Lat, response.Lon]);
                        map.setView(new L.LatLng(response.Lat, response.Lon), 18);
                    }
                }
            });
        }
    });
});