
function fillTankWater(elementId, tankId, waterVolumePercentage) {
    var height = $('#Tank_' + tankId).height();
    var heightFill = ((waterVolumePercentage * 100) * height) / 100;
    $('#TankFill_' + tankId).height(heightFill);
    $('#TankFill_' + tankId).css('background', '#72AFD2');
};

function generateFlowMeter(id, rateFlow, totalizer) {    
    var flowMeters = [];
    flowMeters.push(rateFlow);

    var chart = new Highcharts.Chart({
        chart: {
            renderTo: 'FlowMeter_' + id,
            type: 'gauge',
            backgroundColor: 'rgba(0,0,0,0)',
            plotBackgroundColor: null,
            plotBackgroundImage: null,
            plotBorderWidth: 0,
            plotShadow: false,       
            width: 200,
            height: 200
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
    });
}