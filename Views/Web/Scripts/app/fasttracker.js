function generateFlowMeter(id, rateFlowValue, totalizerValue) {
    var flowMeters = [];

    flowMeters.push(rateFlowValue);

    var chart = new Highcharts.Chart({
        chart: {
            renderTo: 'flowmeter_' + id,
            type: 'gauge',
            plotBackgroundColor: null,
            plotBackgroundImage: null,
            plotBorderWidth: 0,
            plotShadow: false,
            backgroundColor: 'rgba(255, 255, 255, 0.1)',
            height: 250
        },
        title: {
            text: ''
        },
        credits: {
            enabled: false
        },
        pane: {
            startAngle: -150,
            endAngle: 150,
            background: [{
                backgroundColor: {
                    linearGradient: { x1: 0, y1: 0, x2: 0, y2: 1 },
                    stops: [
                        [0, '#FFF'],
                        [1, '#333']
                    ]
                },
                borderWidth: 0,
                outerRadius: '109%'
            }, {
                backgroundColor: {
                    linearGradient: { x1: 0, y1: 0, x2: 0, y2: 1 },
                    stops: [
                        [0, '#333'],
                        [1, '#FFF']
                    ]
                },
                borderWidth: 1,
                outerRadius: '107%'
            }, {
                // default background
            }, {
                backgroundColor: '#DDD',
                borderWidth: 0,
                outerRadius: '105%',
                innerRadius: '103%'
            }]
        },

        // the value axis
        yAxis: {
            min: 0,
            max: 140,
            minorTickInterval: 'auto',
            minorTickWidth: 1,
            minorTickLength: 10,
            minorTickPosition: 'inside',
            minorTickColor: '#666',

            tickPixelInterval: 30,
            tickWidth: 2,
            tickPosition: 'inside',
            tickLength: 10,
            tickColor: '#666',
            labels: {
                step: 2,
                rotation: 'auto'
            },
            dataLabels: false,
            title: {
                text: ''
            },
            plotBands: [{
                from: 0,
                to: 60,
                color: '#55BF3B' // green
            }, {
                from: 60,
                to: 100,
                color: '#DDDF0D' // yellow
            }, {
                from: 100,
                to: 140,
                color: '#DF5353' // red
            }]
        },

        series: [{
            name: 'Speed',
            data: flowMeters,
            tooltip: {
                valueSuffix: '',
                pointFormatter: function () {
                    return this.y;
                }
            },
            dataLabels: {
                formatter: function () {
                    return totalizerValue;
                },
            }
        }]
    });
}

function addSensorAlarm() {
    var description = $('#txtDescription').val();
    var severityId = $("#ddlSeverity option:selected").val();
    var limit = $('#txtLimit').val();
    var email = $('#txtEmail').val();

    var sensorAlarm = {
        Description: description,
        SeverityId: severityId,
        SensorId: 2,
        Limit: limit,
        Email: email
    };

    $.ajax({
        url: getUrlBase() + "api/v1/json/customer/sensoralarm/add/",
        type: "POST",
        dataType: "json",
        cache: false,
        contentType: 'application/json; charset=utf-8',
        data: JSON.stringify(sensorAlarm),
        success: function (data) {
            notifiySuccess("Alarm is added")
        },
        error: function (jqXHR, exception) {
            notifiyError("Error - Add Alarm");
        }
    });
};

function getData() {
    showLoading();
    var tankId = $('#ddlTank').val();
    getTankInfo(tankId);
    getTankWaterVolume(tankId);
    getTankTemperature(tankId);
    getTankSensorVoltage(tankId);
    hideLoading();
}

function getTankInfo(tankId) {
    $.ajax({
        url: '/Customer/Tank/GetTankInfo',
        type: "GET",
        dataType: "JSON",
        data: { TankId: tankId },
        success: function (data) {
            if (data != null) {
                var waterVolumePerc = (data.WaterVolume / data.Sensor.Tank.WaterVolumeCapacity) * 100;

                $('#tankName h3').text(data.Sensor.Tank.Name);
                $('#tankWaterVolume h3').text(data.WaterVolume);
                $('#tankWaterVolumePerc h3').text(waterVolumePerc + ' %');

                var date = $.datepicker.formatDate('dd/MM/yyyy', data.CreatedDate);
                var time = $.datepicker.formatDate('hh:mm:ss', data.CreatedDate)

                $('#tankWaterLastMeasurement p').text(date);
            }
        }
    });
}

function getTankWaterVolume(tankId) {
    $.ajax({
        url: '/Customer/Tank/GetTankWaterVolume',
        type: "GET",
        dataType: "JSON",
        data: { TankId: tankId },
        success: function (data) {

        }
    });
}

function getTankTemperature(tankId) {
    $.ajax({
        url: '/Customer/Tank/GetTankTemperature',
        type: "GET",
        dataType: "JSON",
        data: { TankId: tankId },
        success: function (data) {

        }
    });
}

function getTankSensorVoltage(tankId) {
    $.ajax({
        url: '/Customer/Tank/GetTankSensorVoltage',
        type: "GET",
        dataType: "JSON",
        data: { TankId: tankId },
        success: function (data) {
            var d1 = [];
            for (var i = 0; i < 14; i += 0.5) {
                d1.push([i, Math.sin(i)]);
            }

            var d2 = [[0, 3], [4, 8], [8, 5], [9, 13]];
            // A null signifies separate line segments
            var d3 = [[0, 12], [7, 12], null, [7, 2.5], [12, 2.5]];
            $.plot("#voltage-graph", [d1, d2, d3]);
        }
    });
}

function FillTank() {
    var siteId = $('#ddlSite').val();

    $.ajax({
        url: '/Customer/Tank/FillTank',
        type: "GET",
        dataType: "JSON",
        data: { SiteId: siteId },
        success: function (tanks) {
            $("#ddlTank").html("");
            $("#ddlTank").append(
                   $('<option></option>').val("").html("-- Please select a Tank --"));

            $.each(tanks, function (i, tank) {
                $("#ddlTank").append(
                    $('<option></option>').val(tank.Id).html(tank.Name));
            });
        }
    });
}

function selectedSite() {
    var siteId = $('#ddlSite').val();

    if (siteId != "") {
        $.ajax({
            url: '/Customer/FastTracker/GetsTankBySiteId',
            type: "GET",
            dataType: "JSON",
            data: { SiteId: siteId },
            success: function (tanks) {
                $.get('/Customer/FastTracker/GetTankHTML', function (html) {
                    $.each(tanks, function (i, tank) {
                        var html_tank = html;

                        var dateString = tank.EventDate.substr(6);
                        var currentDate = new Date(parseInt(dateString));
                        var month = currentDate.getMonth() + 1;
                        var day = currentDate.getDate();
                        var year = currentDate.getFullYear();
                        var date = day + "/" + month + "/" + year;

                        var html_tank = ReplaceAll(html_tank, "{{Count}}", i + 1);
                        var html_tank = ReplaceAll(html_tank, "{{ImageTankModel}}", "~/images/tank_models/" + tank.UrlImageTankModel);
                        var html_tank = ReplaceAll(html_tank, "{{MeasurementTime}}", date);
                        var html_tank = ReplaceAll(html_tank, "{{MeasurementDate}}", tank.EventDate);
                        var html_tank = ReplaceAll(html_tank, "{{WaterVolumePerc}}", tank.WaterVolumePerc);
                        var html_tank = ReplaceAll(html_tank, "{{WaterVolume}}", tank.WaterVolume);

                        $('#tanks').append(html_tank);
                    });
                });
            },
            error: function (jqXHR, exception) {
                notifiyError("Error - Get Tanks");
            }
        });
    }
}

function generateWaterVolumeGraph(tankId) {
    $.ajax({
        url: getUrlBase() + "/Customer/FastTracker/GetsWaterVolume",
        type: "GET",
        dataType: "JSON",
        data: { TankId: tankId },
        success: function (data) {
            var dataX = [];
            var dataY = [];
            var minY = 0;
            var maxY = data.WaterVolumeCapacity;

            $.each(data.WaterVolumes, function (i, d) {
                var momentDate = moment.utc(d.EventDate);
                dataX.push(momentDate);
                dataY.push(d.WaterVolume);
            });

            var chart = new Highcharts.Chart({
                chart: {
                    renderTo: 'graph-' + tankId,
                    zoomType: 'x',
                    height: '220'
                },
                title: {
                    text: 'Water Volume'
                },
                tooltip: {
                    useHTML: true,
                    formatter: function () {
                        return Highcharts.dateFormat('%m/%d/%Y ', this.x) + '<BR>' +
                               Highcharts.dateFormat('%H:%M:%S', this.x) + '<BR>' +
                               'WV: ' + this.y;
                    }
                },
                loading: {
                    hideDuration: 100,
                    labelStyle: { "fontWeight": "bold", "position": "relative", "top": "45%" },
                    showDuration: 0
                },
                credits: {
                    enabled: false
                },
                xAxis: {
                    type: 'datetime',
                    categories: dataX,
                    labels: {
                        useHTML: true,
                        align: "center",
                        enabled: true,
                        formatter: function () {
                            return Highcharts.dateFormat('%m/%d/%Y ', this.value) + '<BR>' +
                                   Highcharts.dateFormat('%H:%M:%S', this.value);
                        }
                    }
                },
                yAxis: {
                    title: {
                        text: 'Water'
                    },
                    min: minY,
                    max: maxY,
                },
                legend: {
                    enabled: false
                },
                series: [{
                    type: 'column',
                    data: dataY
                },
                {
                    type: 'spline',
                    data: dataY
                },
                {
                    type: 'area',
                    data: dataY
                }]
            });
        },
        error: function (jqXHR, exception) {
            notifiyError("Error - Generate Graph");
        }
    });
}


function generateTemperatureGraph(elementId, waterTemperature) {
    if (waterTemperature != "" && waterTemperature != undefined) {
        var majorTicks = { size: '10%', interval: 10 },
            minorTicks = { size: '5%', interval: 2.5, style: { 'stroke-width': 1, stroke: '#AAAAAA' } },
            labels = { interval: 10, position: 'far' };
        var minTemp = waterTemperature > 0 ? 0 : 10;
        var maxTemp = parseInt(waterTemperature * 1.2);

        $('#' + elementId).jqxLinearGauge({
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