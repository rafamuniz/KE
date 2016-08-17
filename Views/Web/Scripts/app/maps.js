var icons = getUrlBase() + '/images/extended-icons3.png';

var shape = {
    coords: [1, 1, 1, 20, 18, 20, 18, 1],
    type: 'poly'
};

var tankWaterTemperatures = {};

//'http://www.googlemapsmarkers.com/v1/S/FF0000/',
var siteImage = {
    url: 'http://chart.apis.google.com/chart?cht=d&chdp=mapsapi&chl=pin%27i%5c%27%5bS%27-2%27f%5chv%27a%5c%5dh%5c%5do%5cFF0000%27fC%5c000000%27tC%5c000000%27eC%5cLauto%27f%5c&ext=.png',
    size: new google.maps.Size(25, 25),
};

var tankImage = {
    url: getUrlBase() + '/images/map_tank.png',
    size: new google.maps.Size(25, 25),
};

var alarmLowImage = {
    url: getUrlBase() + '/images/marker_alarm_low.png',
    size: new google.maps.Size(25, 25),
};

var alarmMendiumImage = {
    url: getUrlBase() + '/images/marker_alarm_medium.png',
    size: new google.maps.Size(25, 25),
};

var alarmHighImage = {
    url: getUrlBase() + '/images/marker_alarm_high.png',
    size: new google.maps.Size(25, 25),
};

var pondImage = {
    url: getUrlBase() + '/images/map_pond.png',
    size: new google.maps.Size(50, 50),
};

var sensorImage = {
    url: getUrlBase() + '/images/map_sensor.png',
    size: new google.maps.Size(25, 25),
};

var infoWindow = new google.maps.InfoWindow({
    content: ""
});

function initMap(siteId, latitude, longitude) {
    if (siteId != 'undefined' && latitude != 'undefined' && longitude != 'undefined') {
        var mapOptions = {
            center: { lat: latitude, lng: longitude },
            scrollwheel: false,
            zoom: 13,
            mapTypeId: google.maps.MapTypeId.ROADMAP
        };
        var map = new google.maps.Map(document.getElementById('map'), mapOptions);

        setSite(siteId, map);
        setPondsMarkers(siteId, map);
        setTanksMarkers(siteId, map);
        setSensorsMarkers(siteId, map);
    }
}

/*********/
/* SITE */
/********/
function setSite(siteId, map) {
    $.ajax({
        url: getUrlBase() + "/Customer/Map/GetSite/",
        type: "GET",
        dataType: "JSON",
        data: { siteId: siteId },
        success: function (data) {
            addSiteMarker(map, data);
        },
        error: function (jqXHR, exception) {
            notifiyError('Error - Get Site');
        }
    });

    //var circle = new google.maps.Circle({
    //    map: map,
    //    radius: 3000, // 10 miles in metres - 186411
    //    fillColor: '#AA0000'
    //});

    //circle.bindTo('center', marker, 'position');
}

function addSiteMarker(map, site) {
    var marker = new google.maps.Marker({
        position: { lat: Number(site.Latitude), lng: Number(site.Longitude) },
        animation: google.maps.Animation.DROP,
        map: map,
        title: site.Name,
        zIndex: 1
    });

    marker.addListener('click', function () {
        createSiteModal(marker, map, site);
    });
}

function createSiteModal(marker, map, site) {
    $.ajax({
        url: getUrlBase() + "/Customer/Map/GetSiteWithInfo/",
        type: "GET",
        dataType: "JSON",
        data: { siteId: site.Id },
        success: function (data) {
            openInfoWindow(map, marker, data.Id, siteInfoWindow(data));
        },
        error: function (jqXHR, exception) {
            openInfoWindow(map, marker, site.Id, createInfoWindow('ERROR', '<p>Error loading site information</p>', ''));
        }
    });
}

function siteInfoWindow(site) {
    var content = '<div class="row">' +
                        '<div class="col-sm-6 col-xs-6 col-md-6 col-lg-6">' +
                            '<b>IP</b>:<br/>' + site.IPAddress +
                        '</div>' +
                        '<div class="col-sm-6 col-xs-6 col-md-6 col-lg-6">' +
                        '</div>' +
                    '</div>';

    return createInfoWindow(site.Name, content, '')
}
/*********/
/* SITE */
/********/

/*********/
/* POND  */
/********/
function setPondsMarkers(siteId, map) {
    $.ajax({
        url: getUrlBase() + "/Customer/Map/GetsPond/",
        type: "GET",
        dataType: "JSON",
        data: { siteId: siteId },
        success: function (data) {
            for (var i = 0; i < data.length; i++) {
                if (data[i].Latitude != null && data[i].Longitude != null) {
                    addPondMarker(map, data[i]);
                }
            }
        },
        error: function (jqXHR, exception) {
            notifiyError('Error - Get Ponds');
        }
    });
}

function addPondMarker(map, pond) {
    var marker = new google.maps.Marker({
        position: { lat: Number(pond.Latitude), lng: Number(pond.Longitude) },
        animation: google.maps.Animation.DROP,
        map: map,
        icon: pondImage,
        shape: shape,
        title: pond.Name,
        zIndex: 1
    });

    marker.addListener('click', function () {
        createPondModal(marker, map, pond);
    });
}

function createPondModal(marker, map, pond) {
    $.ajax({
        url: getUrlBase() + "/Customer/Map/GetPondWithInfo/",
        type: "GET",
        dataType: "JSON",
        data: { pondId: pond.Id },
        success: function (data) {
            openInfoWindow(map, marker, data.Id, pondInfoWindow(data));
        },
        error: function (jqXHR, exception) {
            openInfoWindow(map, marker, pond.Id, createInfoWindow('ERROR', '<p>Error loading pond information</p>', ''));
        }
    });
}

function pondInfoWindow(pond) {
    var content = '<div class="row">' +
                    '<div class="col-sm-6 col-xs-6 col-md-6 col-lg-6">';

    // Alarm
    if (pond.Alarms.length == 0) {

    } else if (pond.Alarms.length > 0 && (pond.HasAlarmHigh == true || pond.HasAlarmCritical == true)) {
        content += '<a href="/Customer/Monitoring/AlarmByPond/?pondId=' + pond.Id + '"><i class="fa fa-warning text-red"><span id="totalAlarms" class="label label-primary">' + pond.Alarms.length + '</span></i></a><br/>';
    } else {
        content += '<a href="/Customer/Monitoring/AlarmByPond/?pondId=' + pond.Id + '"><i class="fa fa-warning text-white"><span id="totalAlarms" class="label label-primary">' + pond.Alarms.length + '</span></i></a><br/>';
    }

    content += '<b>Water Capacity</b>:<br/>' + pond.WaterVolumeCapacity;
    if (pond.WaterVolumeCapacityUnit != null) {
        content += ' ' + pond.WaterVolumeCapacityUnit;
    } else {
        content += ' gal';
    }

    if (pond.WaterVolumeLastValue != null) {
        content += '<br><b>Water Remaining</b>:<br/>' + pond.WaterVolumeLastValue
    }

    content += '<div class="iw-subTitle"></div>' +
                '<p><br></p>' +
                '</div>' +
                '<div class="col-sm-6 col-xs-6 col-md-6 col-lg-6">' +
                '<div class="row iw-image-center">' +
                '<img src="' + pond.UrlImage + '" alt="pond" height="90" width="130">' +
                '</div>' +
                '<div class="row text-center">';

    if (pond.WaterVolumePercentage != null) {
        content += '<b>' + pond.WaterVolumePercentage + ' %</b>';
    }

    content += '</div>' +
                '</div>' +
                '</div>' +
                '</div>' +
                '</div>';

    return createInfoWindow(pond.Name, content, '');
}
/*********/
/* POND  */
/********/

/*********/
/* TANK */
/********/
function setTanksMarkers(siteId, map) {
    $.ajax({
        url: getUrlBase() + "/Customer/Map/GetsTank/",
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
    var image;
    var animation;

    if (tank.Alarms.length == 0) {
        image = tankImage;
        animation = google.maps.Animation.DROP;
    } else if (tank.Alarms.length > 0 && (tank.HasAlarmHigh == true || tank.HasAlarmCritical == true)) {
        image = alarmHighImage;
        animation = google.maps.Animation.BOUNCE;
    } else {
        image = alarmLowImage;
        animation = google.maps.Animation.BOUNCE;
    }

    var marker = new google.maps.Marker({
        position: { lat: Number(tank.Latitude), lng: Number(tank.Longitude) },
        animation: animation,
        map: map,
        icon: image,
        shape: shape,
        title: tank.Name,
        zIndex: 1
    });

    //window.setTimeout(function () {
    //    markers.push(new google.maps.Marker({
    //        position: position,
    //        map: map,
    //        animation: google.maps.Animation.DROP
    //    }));
    //}, timeout);

    marker.addListener('click', function () {
        createTankModal(marker, map, tank);
    });
}

function createTankModal(marker, map, tank) {
    $.ajax({
        url: getUrlBase() + "/Customer/Map/GetTankWithInfo/",
        type: "GET",
        dataType: "JSON",
        data: { tankId: tank.Id },
        success: function (data) {
            openInfoWindow(map, marker, data.Id, tankInfoWindow(data));
            for (var key in tankWaterTemperatures) {
                var value = tankWaterTemperatures[key];
                generateWaterTemperatureGraph(key, value);
            }
        },
        error: function (jqXHR, exception) {
            openInfoWindow(map, marker, tank.Id, createInfoWindow('ERROR', '<p>Error loading tank information</p>', ''));
        }
    });
}

function tankInfoWindow(tank) {
    var content = '<div id="iw-container">' +
            '<div class="iw-title">' + tank.Name + '</div>' +
            '<div class="row">' +
            '<div class="iw-content">' +
            '<div class="row">' +
                '<div class="col-sm-6 col-xs-6 col-md-6 col-lg-6">';

    // Alarm
    if (tank.Alarms.length == 0) {

    } else if (tank.Alarms.length > 0 && (tank.HasAlarmHigh == true || tank.HasAlarmCritical == true)) {
        content += '<a href="/Customer/Monitoring/AlarmByTank/?tankId=' + tank.Id + '"><i class="fa fa-warning text-red"><span id="totalAlarms" class="label label-primary">' + tank.Alarms.length + '</span></i></a><br/>';
    } else {
        content += '<a href="/Customer/Monitoring/AlarmByTank/?tankId=' + tank.Id + '"><i class="fa fa-warning text-white"><span id="totalAlarms" class="label label-primary">' + tank.Alarms.length + '</span></i></a><br/>';
    }

    content += '<b>Water Capacity</b>:<br/>' + tank.WaterVolumeCapacity;
    if (tank.WaterVolumeCapacityUnit != null) {
        content += ' ' + tank.WaterVolumeCapacityUnit;
    } else {
        content += ' gal';
    }

    if (tank.WaterVolumeLastValue != null) {
        content += '<br><b>Water Remaining</b>:<br/>' + tank.WaterVolumeLastValue + ' ' + tank.WaterVolumeLastUnit;
    }

    if (tank.WaterTemperatureLastEventValue != null) {
        content += '<br><b>Water Temperature</b>:<br/>' + tank.WaterTemperatureLastEventValue + ' ' + tank.WaterTemperatureLastEventUnit;
    }

    content += '</div>';

    content += '<div class="col-sm-6 col-xs-6 col-md-6 col-lg-6">' +
                    '<div class="row iw-image-center">' +
                        '<img src="' + tank.UrlImageTankModel + '" alt="tank" height="115" width="100"></div>';

    if (tank.WaterTemperatureLastEventValue != null) {
        var waterTemperatureId = "divWaterTemperature_" + tank.WaterTemperatureLastEventId;
        tankWaterTemperatures[waterTemperatureId] = tank.WaterTemperatureLastEventValue;
        content += '<div class="row iw-image-center"><div id="' + waterTemperatureId + '"></div></div>';
    }

    content += '</div>';

    content += '</div>' +
                '</div>' +
            '</div>' +
            '<div class="iw-bottom-gradient">';

    content += '</div>' +
    '</div>';

    return content;
}

function generateWaterTemperatureGraph(elementId, waterTemperature) {
    if (waterTemperature != "" && waterTemperature != undefined) {
        var majorTicks = { size: '10%', interval: 10 },
            minorTicks = { size: '5%', interval: 2.5, style: { 'stroke-width': 1, stroke: '#AAAAAA' } },
            labels = { interval: 10, position: 'far' };
        var minTemp = waterTemperature > 0 ? 0 : 10;
        var maxTemp = parseInt(waterTemperature * 1.2);

        $('#' + elementId).jqxLinearGauge({
            height: '200px',
            orientation: 'vertical',
            labels: labels,
            ticksMajor: majorTicks,
            ticksMinor: minorTicks,
            min: minTemp,
            max: maxTemp,
            value: waterTemperature,
            pointer: { size: '6%' },
            colorScheme: 'scheme05',
            ranges: [
            { startValue: -10, endValue: 10, style: { fill: '#FFF157', stroke: '#FFF157' } },
            { startValue: 10, endValue: 35, style: { fill: '#FFA200', stroke: '#FFA200' } },
            { startValue: 35, endValue: maxTemp, style: { fill: '#FF4800', stroke: '#FF4800' } }]
        });
    }
}
/*********/
/* TANK */
/********/

/***********/
/* SENSOR */
/**********/
function setSensorsMarkers(siteId, map) {
    $.ajax({
        url: getUrlBase() + "/Customer/Map/GetsSensor/",
        type: "GET",
        dataType: "JSON",
        data: { siteId: siteId },
        success: function (data) {
            for (var i = 0; i < data.length; i++) {
                if (data[i].Latitude != null && data[i].Longitude != null) {
                    addSensorMarker(map, data[i]);
                }
            }
        },
        error: function (jqXHR, exception) {
            notifiyError('Error - Get Sensor');
        }
    });
}

function addSensorMarker(map, sensor) {
    var marker = new google.maps.Marker({
        position: { lat: Number(sensor.Latitude), lng: Number(sensor.Longitude) },
        animation: google.maps.Animation.DROP,
        map: map,
        icon: sensorImage,
        shape: shape,
        title: sensor.Name,
        zIndex: 1
    });

    marker.addListener('click', function () {
        createSensorModal(marker, map, sensor);
    });
}

function createSensorModal(marker, map, sensor) {
    $.ajax({
        url: getUrlBase() + "/Customer/Map/GetSensorWithInfo/",
        type: "GET",
        dataType: "JSON",
        data: { sensorId: sensor.Id },
        success: function (data) {
            openInfoWindow(map, marker, data.Id, sensorInfoWindow(data));
        },
        error: function (jqXHR, exception) {
            openInfoWindow(map, marker, sensor.Id, createInfoWindow('ERROR', '<p>Error loading sensor information</p>', ''));
        }
    });
}

function sensorInfoWindow(sensor) {
    var content = '<div class="row">' +
                    '<div class="col-sm-6 col-xs-6 col-md-6 col-lg-6">';

    // Alarm
    if (sensor.Alarms.length == 0) {

    } else if (sensor.Alarms.length > 0 && (sensor.HasAlarmHigh == true || sensor.HasAlarmCritical == true)) {
        content += '<a href="/Customer/Monitoring/AlarmBySensor/?sensorId=' + sensor.Id + '"><i class="fa fa-warning text-red"><span id="totalAlarms" class="label label-primary">' + sensor.Alarms.length + '</span></i></a><br/>';
    } else {
        content += '<a href="/Customer/Monitoring/AlarmBySensor/?sensorId=' + sensor.Id + '"><i class="fa fa-warning text-white"><span id="totalAlarms" class="label label-primary">' + sensor.Alarms.length + '</span></i></a><br/>';
    }

    content += '</div>' +
                    '<div class="col-sm-6 col-xs-6 col-md-6 col-lg-6">' +
                    '</div>' +
                '</div>';

    return createInfoWindow(sensor.Name, content, '')
}
/***********/
/* SENSOR */
/**********/

/*********/
/* UTIL  */
/********/
function openInfoWindow(map, marker, id, content) {
    infoWindow.close();
    infoWindow = new google.maps.InfoWindow({
        content: content
    });

    infoWindowEvents(infoWindow);
    infoWindow.open(map, marker);
}

function infoWindowMessage(title, message) {
    return '<div id="iw-container">' +
                '<div class="iw-title">' + title + '</div>' +
                '<div class="row">' +
                    '<div class="iw-content">' +
                        '<div class="row">' +
                            '<div class="col-sm-12 col-xs-12 col-md-12 col-lg-12">' +
                                '<div class="iw-subTitle"></div>' +
                                '<p>' + message + '</p>' +
                            '</div>' +
                        '</div>' +
                    '</div>' +
                '</div>' +
                '<div class="iw-bottom-gradient">' +
                '</div>' +
            '</div>';
}

function createInfoWindow(title, content, bottom) {
    var window = '<div id="iw-container">' +
                 '<div class="iw-title">' + title + '</div>' +
                 '<div class="row">' +
                 '<div class="iw-content">' + content + '</div>' +
                 '<div class="iw-bottom-gradient">' + bottom + '</div></div></div>';

    return window;
}

function infoWindowEvents(infoWindow) {
    google.maps.event.addListener(infoWindow, 'domready', function () {
        infoWindowCustomize();
    });
}

function infoWindowCustomize() {
    var iwOuter = $('.gm-style-iw');

    /* Since this div is in a position prior to .gm-div style-iw.
     * We use jQuery and create a iwBackground variable,
     * and took advantage of the existing reference .gm-style-iw for the previous div with .prev().
    */
    var iwBackground = iwOuter.prev();

    // Removes background shadow DIV
    iwBackground.children(':nth-child(2)').css({ 'display': 'none' });

    // Removes white background DIV
    iwBackground.children(':nth-child(4)').css({ 'display': 'none' });

    // Moves the infowindow 115px to the right.
    iwOuter.parent().parent().css({ left: '115px' });

    // Moves the shadow of the arrow 76px to the left margin.
    iwBackground.children(':nth-child(1)').attr('style', function (i, s) { return s + 'left: 76px !important;' });

    // Moves the arrow 76px to the left margin.
    iwBackground.children(':nth-child(3)').attr('style', function (i, s) { return s + 'left: 76px !important;' });

    // Changes the desired tail shadow color.
    iwBackground.children(':nth-child(3)').find('div').children().css({ 'box-shadow': 'rgba(72, 181, 233, 0.6) 0px 1px 6px', 'z-index': '1' });

    // Reference to the div that groups the close button elements.
    var iwCloseBtn = iwOuter.next();

    // Apply the desired effect to the close button
    iwCloseBtn.css({ opacity: '1', width: '28px', height: '28px', right: '38px', top: '3px', border: '7px solid #00ADBD', 'border-radius': '13px', 'box-shadow': '0 0 5px #00ADBD' });

    // If the content of infowindow not exceed the set maximum height, then the gradient is removed.
    if ($('.iw-content').height() < 140) {
        $('.iw-bottom-gradient').css({ display: 'none' });
    }

    // The API automatically applies 0.7 opacity to the button after the mouseout event. This function reverses this event to the desired value.
    iwCloseBtn.mouseout(function () {
        $(this).css({ opacity: '1' });
    });

    //var iwContainer = $('#iw-container');
    //iwContainer.parent().css({ 'overflow': 'hidden' });
}
/*********/
/* UTIL  */
/********/