
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

function generateGraph(tankId) {
    $.ajax({
        url: getUrlBase() + "/Customer/FastTracker/GetWaterInfo/",
        type: "GET",
        dataType: "JSON",
        data: { TankId: tankId },
        success: function (data) {
            var chartData = [];
            var dataX = [];
            var dataY = [];
            var minX = null;
            var maxX = null;
            var minY = 0;
            var maxY = data.WaterVolumeCapacity;

            $.each(data.WaterVolumeData, function (i, d) {
                //var water = {
                //    value: d.WaterVolume
                //};

                var momentDate = moment.utc(d.EventDate);
                var eventDate = momentDate.format('MM/DD/YYYY hh:mm A');
                //moment.utc(moment(d.EventDate));
                //.format('MM/DD/YYYY hh:mm A')).toDate();

                var dates = {
                    value: eventDate
                };

                var point = [];
                point.push(eventDate);
                point.push(d.WaterVolume);
                chartData.push(point);

                //dataY.push(water);
                dataX.push(dates);
            });

            var chart = new Highcharts.Chart({
                chart: {
                    renderTo: 'graph-' + data.TankId,
                    zoomType: 'x',
                    height: '220'//,
                    //width: '100%'
                },
                title: {
                    text: 'Water'
                },
                loading: {
                    hideDuration: 100,
                    labelStyle: { "fontWeight": "bold", "position": "relative", "top": "45%" },
                    showDuration: 0
                },
                //subtitle: {
                //    text: document.ontouchstart === undefined ?
                //            'Click and drag in the plot area to zoom in' : 'Pinch the chart to zoom in'
                //},
                xAxis: {
                    type: 'time',
                    dateTimeLabelFormats: {
                        millisecond: '%H:%M:%S.%L',
                        second: '%H:%M:%S',
                        minute: '%H:%M',
                        hour: '%H:%M',
                        day: '%e. %b',
                        week: '%e. %b',
                        month: '%b \'%y',
                        year: '%Y'
                    },
                    min: minX,
                    max: maxX,
                    categories: dataX,
                    labels: {
                        formatter: function () {
                            return Highcharts.dateFormat('%a %d %b', this.value);
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
                plotOptions: {
                    area: {
                        fillColor: {
                            linearGradient: {
                                x1: 0,
                                y1: 0,
                                x2: 0,
                                y2: 1
                            },
                            stops: [
                                [0, Highcharts.getOptions().colors[0]],
                                [1, Highcharts.Color(Highcharts.getOptions().colors[0]).setOpacity(0).get('rgba')]
                            ]
                        },
                        marker: {
                            radius: 2
                        },
                        lineWidth: 1,
                        states: {
                            hover: {
                                lineWidth: 1
                            }
                        },
                        threshold: null
                    }
                },
                series: [{
                    type: 'column',
                    //type: 'area',
                    name: '',
                    data: chartData//,
                    //yAxis: {
                    //    min: 0,
                    //    max: data.WaterVolumeCapacity
                    //}
                }]
            });
        },
        error: function (jqXHR, exception) {
            notifiyError("Error - Generate Graph");
        }
    });
};


