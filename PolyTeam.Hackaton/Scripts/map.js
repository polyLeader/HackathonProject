var southWest = new L.LatLng(48.136, 37.483),
    northEast = new L.LatLng(47.837, 38.041),
    donetskBounds = new L.LatLngBounds(southWest, northEast);
var marker = null;

var map = L.map('map', {
    center: new L.LatLng(47.99874, 37.80466),
    zoom: 15
});

L.tileLayer('http://{s}.tile.cloudmade.com/e37d73e201f94dd78191e2470055aec0/997/256/{z}/{x}/{y}.png', {
    attribution: 'Помощь жителям'
}).addTo(map);

map.locate({ setView: true, maxZoom: 18 });

map.setMaxBounds(donetskBounds);

map.on('click', onMapClick);
    //map.on('dragend', onMove);
    
function onMapClick(e) {
    if (marker != null) {
        map.removeLayer(marker);
    }
    sendQuery(e);
};
function onMove(e) {
    sendQuery(e);
};

function sendQuery(e) {
    $.ajax({
        'dataType': 'JSON',
        'type': 'GET',
        'data': { 'lat': e.latlng.lat, 'lon': e.latlng.lng },
        'url': '/Map/GetObject',
        'success': function (response) {
            $('input#House').removeAttr('disabled');
            if (response != null) {
                marker = new L.Marker(e.latlng, { draggable: true });
                map.addLayer(marker);
                map.setView(new L.LatLng(e.latlng.lat, e.latlng.lng), 18);
                $('input#Street').val(response.Road);
                $('input#House').val(response.HouseNumber);
            }
        }
    });
}