function generateVoltageGraph(tankId) {
    $.ajax({
        url: getUrlBase() + "/Customer/Tank/GetsVoltage/",
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

function generateTemperatureGraph(tankId) {
    $.ajax({
        url: getUrlBase() + "/Customer/Tank/GetsWaterAndWeatherTemperature",
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
        url: getUrlBase() + "/Customer/Tank/GetsVoltage/",
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
