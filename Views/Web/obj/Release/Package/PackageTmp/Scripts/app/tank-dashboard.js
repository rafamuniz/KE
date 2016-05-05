function generateWaterVolumeGraph(tankId) {
    $.ajax({
        url: getUrlBase() + "/Customer/Tank/GetsWaterVolume/",
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