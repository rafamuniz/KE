// TANK
function generateWaterVolumeGraph(tankId) {
    $.ajax({
        url: getUrlBase() + "/Customer/Dashboard/GetsWaterVolume/",
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
                    height: '220',
                    width: '500'
                },
                title: {
                    //text: 'Water Volume'
                    text: ''
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
                            return '';
                            //return Highcharts.dateFormat('%m/%d/%Y ', this.value) + '<BR>' +
                            //       Highcharts.dateFormat('%H:%M:%S', this.value);
                        }
                    }
                },
                yAxis: {
                    title: {
                        //text: 'Water'
                        text: ''
                    },
                    opposite: true,
                    labels: {
                        align: 'right'
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
                }]
            });
        },
        error: function (jqXHR, exception) {
            notifiyError("Error - Generate Graph");
        }
    });
}

function generateTemperatureGraph(tankId, waterTemperature) {
    if (waterTemperature != "" && waterTemperature != undefined) {
        var majorTicks = { size: '10%', interval: 10 },
            minorTicks = { size: '5%', interval: 2.5, style: { 'stroke-width': 1, stroke: '#AAAAAA' } },
            labels = { interval: 10, position: 'far' };
        var minTemp = waterTemperature > 0 ? 0 : 10;
        var maxTemp = parseInt(waterTemperature * 1.2);

        $('#graph-temperature-' + tankId).jqxLinearGauge({
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
// TANK

// Tank - GAUGE

function generateLastFlowMeter(tankId) {
    $.ajax({
        url: getUrlBase() + "/Customer/Dashboard/GetLastFlowMeter/",
        type: "GET",
        dataType: "JSON",
        data: { TankId: tankId },
        success: function (data) {

            var flowMeters = [];
            flowMeters.push(data.RateFlow);

            var totalizer = data.Totalizer;

            var chart = new Highcharts.Chart({
                chart: {
                    renderTo: 'gauge_' + tankId,
                    type: 'gauge',
                    plotBackgroundColor: null,
                    plotBackgroundImage: null,
                    plotBorderWidth: 0,
                    plotShadow: false
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
                            return totalizer;
                        },
                    }
                }]

                //}, function (chart) {
                //    if (!chart.renderer.forExport) {
                //        setInterval(function () {
                //            var point = chart.series[0].points[0],
                //                newVal,
                //                inc = Math.round((Math.random() - 0.5) * 20);

                //            newVal = point.y + inc;
                //            if (newVal < 0 || newVal > 200) {
                //                newVal = point.y - inc;
                //            }

                //            point.update(newVal);

                //        }, 3000);
                //    }
            });

            $("#flowmeter_tube").html("<img src='/images/water-flow.gif' />");
        },
        error: function (jqXHR, exception) {
            notifiyError("Error - Generate Gauge");
        }
    });
}

function generateWaterVolumeGraphByPeriod(tankId, option) {

    $.ajax({
        url: getUrlBase() + "/Customer/Dashboard/GetsWaterVolumeByOption/",
        type: "GET",
        dataType: "JSON",
        data: { TankId: tankId, Option: option },
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
                    renderTo: 'tankVolumeGraph_' + tankId,
                    zoomType: 'x',
                    height: '220'
                },
                title: {
                    //text: 'Water Volume'
                    text: ''
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
                            return '';
                            //return Highcharts.dateFormat('%m/%d/%Y ', this.value) + '<BR>' +
                            //       Highcharts.dateFormat('%H:%M:%S', this.value);
                        }
                    }
                },
                yAxis: {
                    title: {
                        text: 'Water Volume'
                    },
                    labels: {
                        align: 'right'
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
                }]
            });
        },
        error: function (jqXHR, exception) {
            notifiyError("Error - Generate Graph");
        }
    });
}

function generateWaterAndAmbientTemperatureGraph(tankId) {
    $.ajax({
        url: getUrlBase() + "/Customer/Dashboard/GetsWaterAndWeatherTemperature",
        type: "GET",
        dataType: "JSON",
        data: { TankId: tankId },
        success: function (data) {
            var dataX = [];
            var dataWaterTempY = [];
            var dataWeatherTempY = [];
            var minY = 0;
            var maxY = 0;

            $.each(data.WaterTempertatures, function (i, d) {
                var momentDate = moment.utc(d.EventDate);
                dataX.push(momentDate);

                var value = parseFloat(d.Value);
                if (maxY < value)
                    maxY = value;

                dataWaterTempY.push(value);
            });

            $.each(data.WeatherTempertatures, function (i, d) {
                var momentDate = moment.utc(d.EventDate);
                dataX.push(momentDate);

                var value = parseFloat(d.Value);
                if (maxY < value)
                    maxY = value;

                dataWeatherTempY.push(value);
            });

            var chart = new Highcharts.Chart({
                chart: {
                    renderTo: 'temperature_graph_' + tankId,
                    type: 'spline',
                    zoomType: 'x',
                    height: '300'
                },
                title: {
                    text: ''
                },
                tooltip: {
                    useHTML: true,
                    formatter: function () {
                        var text = Highcharts.dateFormat('%m/%d/%Y ', this.x) + '<BR>' +
                                    Highcharts.dateFormat('%H:%M:%S', this.x) + '<BR>';

                        if (this.series.name == 'Water Temperature')
                            text += 'Water: ' + this.y + ' °F';

                        if (this.series.name == 'Weather Temperature')
                            text += 'Weather: ' + this.y + ' °F';

                        return text;
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
                plotOptions: {
                    spline: {
                        marker: {
                            radius: 4,
                            lineColor: '#666666',
                            lineWidth: 1
                        }
                    }
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
                        text: '°F'
                    },
                    min: minY,
                    max: maxY,
                },
                legend: {
                    enabled: true
                },
                series: [{
                    name: 'Water Temperature',
                    marker: {
                        symbol: 'square'
                    },
                    title: {
                        text: 'Water Temp (°F)'
                    },
                    data: dataWaterTempY
                }, {
                    name: 'Weather Temperature',
                    marker: {
                        symbol: 'diamond'
                    },
                    title: {
                        text: 'Weather Temp (°F)'
                    },
                    color: '#FFFF00',
                    data: dataWeatherTempY
                }]
            });
        },
        error: function (jqXHR, exception) {
            notifiyError("Error - Generate Temperature Graph");
        }
    });
}

function generateVoltageGraph(tankId) {
    $.ajax({
        url: getUrlBase() + "/Customer/Dashboard/GetsVoltage/",
        type: "GET",
        dataType: "JSON",
        data: { TankId: tankId },
        success: function (data) {
            var dataX = [];
            var dataY = [];
            var minY = 0;
            var maxY = 0;

            $.each(data.Voltages, function (i, d) {
                var momentDate = moment.utc(d.EventDate);
                dataX.push(momentDate);

                var value = parseFloat(d.Value);
                if (maxY < value)
                    maxY = value;

                dataY.push(value);
            });

            var chart = new Highcharts.Chart({
                chart: {
                    renderTo: 'voltage_graph_' + tankId,
                    zoomType: 'x',
                    height: '300'
                },
                title: {
                    text: ''
                },
                tooltip: {
                    useHTML: true,
                    formatter: function () {
                        return Highcharts.dateFormat('%m/%d/%Y ', this.x) + '<BR>' +
                               Highcharts.dateFormat('%H:%M:%S', this.x) + '<BR>' +
                               'Volts: ' + this.y;
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
                        text: 'mV'
                    },
                    min: minY,
                    max: maxY,
                },
                legend: {
                    enabled: false
                },
                series: [{
                    type: 'line',
                    data: dataY
                }],
                navigation: {
                    menuItemStyle: {
                        fontSize: '10px'
                    }
                }
            });
        },
        error: function (jqXHR, exception) {
            notifiyError("Error - Generate Voltage Graph");
        }
    });
}

// Tank - GAUGE

// SITE
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
            backgroundColor:'rgba(255, 255, 255, 0.1)',
            height: 250,
            width: 250
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
// SITE