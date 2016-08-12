var icons = getUrlBase() + '/images/extended-icons3.png';
var activeTankInfoWindows = new HashTable();

function initMap(siteId, latitude, longitude) {
    if (siteId != 'undefined' && latitude != 'undefined' && longitude != 'undefined') {
        var mapOptions = {
            center: { lat: latitude, lng: longitude },
            scrollwheel: false,
            zoom: 13,
            mapTypeId: google.maps.MapTypeId.ROADMAP
        };
        var map = new google.maps.Map(document.getElementById('map'), mapOptions);

        setSite(siteId, map, latitude, longitude);
        //getPonds(siteId, map);
        setTankMarkers(siteId, map);
    }
}

function setSite(siteId, map, latitude, longitude) {
    var location = new google.maps.LatLng(latitude, longitude);

    var infowindow = new google.maps.InfoWindow({
        content: ''
    });

    var marker = new google.maps.Marker({
        position: location,
        map: map,
        title: 'Site'
    });

    //var circle = new google.maps.Circle({
    //    map: map,
    //    radius: 3000, // 10 miles in metres - 186411
    //    fillColor: '#AA0000'
    //});

    //circle.bindTo('center', marker, 'position');
}

function setMakers(map) {

}

function setTankMarkers(siteId, map) {
    $.ajax({
        url: getUrlBase() + "/Customer/Map/GetTanks/",
        type: "GET",
        dataType: "JSON",
        data: { siteId: siteId },
        success: function (data) {
            for (var i = 0; i < data.length; i++) {
                if (data[i].Latitude != null && data[i].Longitude != null) {
                    addTankMarker(map, data[i]);
                }
            }
        },
        error: function (jqXHR, exception) {
            notifiyError('Error - Get Tanks');
        }
    });
}

function addTankMarker(map, tank) {

    var shape = {
        coords: [1, 1, 1, 20, 18, 20, 18, 1],
        type: 'poly'
    };

    var tankImage = {
        url: getUrlBase() + '/images/map_tank.png',
        size: new google.maps.Size(25, 25),
    };

    var marker = new google.maps.Marker({
        position: { lat: Number(tank.Latitude), lng: Number(tank.Longitude) },
        animation: google.maps.Animation.DROP,
        map: map,
        icon: tankImage,
        shape: shape,
        title: tank.Name,
        zIndex: 1
    });

    marker.addListener('click', function () {
        createTankModal(marker, map, tank);
    });
}

function createTankModal(marker, map, tank) {
    if (activeTankInfoWindows.hasItem(tank.Id))
        activeTankInfoWindows.getItem(tank.Id).close();

    $.ajax({
        url: getUrlBase() + "/Customer/Map/GetTanksWithInfo/",
        type: "GET",
        dataType: "JSON",
        data: { tankId: tank.Id },
        success: function (tank) {
            var contentString = '<div class="popup">' +
                                 '<h1 class="title">' + tank.Name + '</h1>' +
                                 '<div class="row">' +

                                 '<div id="bodyContent" class="col-sm-12 col-xs-12 col-md-12 col-lg-12 body">' +
                                 '<div class="row">' +
                                     '<div class="col-sm-6 col-xs-6 col-md-6 col-lg-6">' +
                                        '<b>Water Capacity</b>: ' + tank.WaterVolumeCapacity +
                                     '</div>' +

                                     '<div class="col-sm-6 col-xs-6 col-md-6 col-lg-6">' +
                                         '<div class="row blinkStyle">' +
                                            '<img src="' + tank.UrlImageTankModel + '" style="width: 100px; height: 100px; display: block; margin-left: auto; margin-right: auto;" />' +
                                         '</div>' +
                                     '</div>' +
                                 '</div>' +
                                 '</div>' +

                                 '</div>' +
                                 '</div>';

            var infoWindow = new google.maps.InfoWindow({
                content: contentString
            });

            activeTankInfoWindows.setItem(tank.Id, infoWindow);

            infoWindow.open(map, marker);
        },
        error: function (jqXHR, exception) {
            var contentString = '<div class="popup">' +
           '<h1 class="title">' + tank.Name + '</h1>' +
           '<div id="bodyContent" class="body">' +
           '<p><b>Error loading tank information</b><br>' +
           '</div>' +
           '</div>';

            var infoWindow = new google.maps.InfoWindow({
                content: contentString
            });
            activeTankInfoWindows.setItem(tank.Id, infoWindow);
            infoWindow.open(map, marker);
        }
    });
}

function getPonds(siteId, map) {
    var shape = {
        coords: [1, 1, 1, 20, 18, 20, 18, 1],
        type: 'poly'
    };

    var pondImage = {
        url: getUrlBase() + '/images/map_pond.png',
        size: new google.maps.Size(25, 25),
    };

    $.ajax({
        url: getUrlBase() + "/Customer/Map/GetPonds/",
        type: "GET",
        dataType: "JSON",
        data: { siteId: siteId },
        success: function (data) {
            for (var i = 0; i < data.length; i++) {
                var pond = data[i];

                if (pond.Latitude != null && pond.Longitude != null) {
                    var marker = new google.maps.Marker({
                        position: { lat: Number(pond.Latitude), lng: Number(pond.Longitude) },
                        map: map,
                        icon: pondImage,
                        shape: shape,
                        title: pond.Name,
                        zIndex: i
                    });
                    marker.addListener('click', function () {
                        createPondModal(pond).open(map, marker);
                    });
                }
            }
        },
        error: function (jqXHR, exception) {
            notifiyError('Error - Get Ponds');
        }
    });
}

function createPondModal(pond) {
    var contentString = '<div id="content">' +
 '<div id="siteNotice">' +
 '</div>' +
 '<h1 id="firstHeading" class="firstHeading">' + pond.Name + '</h1>' +
 '<div id="bodyContent">' +
 '<p><b>Water Capacity: ' + pond.WaterVolumeCapacity + '</b><br>' +
 '</div>' +
 '</div>';

    var infowindow = new google.maps.InfoWindow({
        content: contentString
    });
    return infowindow;
}


function getSensors(siteId, map) {
    var shape = {
        coords: [1, 1, 1, 20, 18, 20, 18, 1],
        type: 'poly'
    };

    var sensorImage = {
        url: getUrlBase() + '/images/map_sensor.png',
        size: new google.maps.Size(25, 25),
    };

    $.ajax({
        url: getUrlBase() + "/Customer/Map/GetSensors/",
        type: "GET",
        dataType: "JSON",
        data: { siteId: siteId },
        success: function (data) {
            for (var i = 0; i < data.length; i++) {
                var Sensor = data[i];

                if (Sensor.Latitude != null && Sensor.Longitude != null) {
                    var marker = new google.maps.Marker({
                        position: { lat: Number(Sensor.Latitude), lng: Number(Sensor.Longitude) },
                        map: map,
                        icon: sensorImage,
                        shape: shape,
                        title: Sensor.Name,
                        zIndex: i
                    });
                    marker.addListener('click', function () {
                        createSensorModal(Sensor).open(map, marker);
                    });
                }
            }
        },
        error: function (jqXHR, exception) {
            notifiyError('Error - Get Sensors');
        }
    });
}

function createSensorModal(sensor) {
    var contentString = '<div id="content" class="popup">' +
 '<div id="siteNotice">' +
 '</div>' +
 '<h1 id="firstHeading" class="firstHeading">' + sensor.Name + '</h1>' +
 '<div id="bodyContent">' +
 //'<p><b>Water Capacity: ' + pond.WaterVolumeCapacity + '</b><br>' +
 '</div>' +
 '</div>';

    var infowindow = new google.maps.InfoWindow({
        content: contentString
    });
    return infowindow;
}