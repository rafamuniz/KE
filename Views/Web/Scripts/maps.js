function initMap(siteId, latitude, longitude) {
    if (siteId != 'undefined' && latitude != 'undefined' && longitude != 'undefined') {
        var mapOptions = {
            center: { lat: latitude, lng: longitude },
            scrollwheel: false,
            zoom: 13,
            mapTypeId: google.maps.MapTypeId.ROADMAP
        };
        var map = new google.maps.Map(document.getElementById('map'), mapOptions);

        var location = new google.maps.LatLng(latitude, longitude);

        var infowindow = new google.maps.InfoWindow({
            content: ''
        });

        var marker = new google.maps.Marker({
            position: location,
            map: map,
            title: 'Tate Gallery'
        });

        var circle = new google.maps.Circle({
            map: map,
            radius: 3000, // 10 miles in metres - 186411
            fillColor: '#AA0000'
        });
        circle.bindTo('center', marker, 'position');
        getPonds(siteId, map);
        getTanks(siteId, map);
    }

}

function setMakers(map) {

}

function getTanks(siteId, map) {
    var shape = {
        coords: [1, 1, 1, 20, 18, 20, 18, 1],
        type: 'poly'
    };

    $.ajax({
        url: getUrlBase() + "/Customer/Map/GetTanks/",
        type: "GET",
        dataType: "JSON",
        data: { siteId: siteId },
        success: function (data) {
            for (var i = 0; i < data.length; i++) {
                var tank = data[i];

                if (tank.Latitude != null && tank.Longitude != null) {
                    var marker = new google.maps.Marker({
                        position: { lat: Number(tank.Latitude), lng: Number(tank.Longitude) },
                        map: map,
                        icon: getUrlBase() + '/images/ball-green.png',
                        shape: shape,
                        title: tank.Name//,
                        //zIndex: beach[3]
                    });
                    marker.addListener('click', function () {
                        createTankModal(tank).open(map, marker);
                    });
                }
            }
        },
        error: function (jqXHR, exception) {
            notifiyError('Error - Get Tanks');
        }
    });
}

function createTankModal(tank) {
    var contentString = '<div id="content">' +
 '<div id="siteNotice">' +
 '</div>' +
 '<h1 id="firstHeading" class="firstHeading">' + tank.Name + '</h1>' +
 '<div id="bodyContent">' +
 '<p><b>Water Capacity: ' + tank.WaterVolumeCapacity + '</b><br>' +
 '</div>' +
 '</div>';

    var infowindow = new google.maps.InfoWindow({
        content: contentString
    });
    return infowindow;
}

function getPonds(siteId, map) {
    var shape = {
        coords: [1, 1, 1, 20, 18, 20, 18, 1],
        type: 'poly'
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
                        icon: getUrlBase() + '/images/ball-green.png',
                        shape: shape,
                        title: pond.Name
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