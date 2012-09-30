var southWest = new L.LatLng(48.136, 37.483),
    northEast = new L.LatLng(47.837, 38.041),
    donetskBounds = new L.LatLngBounds(southWest, northEast);

var map = L.map('map', {
    center: new L.LatLng(47.99874, 37.80466),
    zoom: 15
});

L.tileLayer('http://{s}.tile.cloudmade.com/e37d73e201f94dd78191e2470055aec0/997/256/{z}/{x}/{y}.png', {
    attribution: 'Помощь жителям'
}).addTo(map);

map.locate({ setView: true, maxZoom: 18 });

map.setMaxBounds(donetskBounds);