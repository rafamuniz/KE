
function showTankModel() {
    var tankModelId = $("#ddlTankModel option:selected").val();

    if (tankModelId != 0 && tankModelId != "") {
        $.ajax({
            url: getUrlBase() + "/Customer/Tank/GetTankModel/",
            type: "GET",
            dataType: "JSON",
            data: { tankModelId: tankModelId },
            success: function (tankModel) {
                if (tankModel != null) {

                    $('#divtankModel').visible();

                    $('#imgTankModel').attr('src', getUrlBase() + '/images/tank_models/' + tankModel.ImageFilename);
                    $('#imgTankModel').removeAttr('style');

                    if (tankModel.Geometry.HasHeight) {
                        $('#height').val(tankModel.Height);
                        $('#divHeight').visible();
                    }
                    else {
                        $('#divHeight').invisible();
                    }

                    if (tankModel.Geometry.HasWidth) {
                        $('#width').val(tankModel.Width);
                        $('#divWidth').visible();
                    }
                    else {
                        $('#divWidth').invisible();
                    }

                    if (tankModel.Geometry.HasLength) {
                        $('#length').val(tankModel.Length);
                        $('#divLength').visible();
                    }
                    else {
                        $('#divLength').invisible();
                    }

                    if (tankModel.Geometry.HasFaceLength) {
                        $('#faceLength').val(tankModel.FaceLength);
                        $('#divFaceLength').visible();
                    }
                    else {
                        $('#divFaceLength').invisible();
                    }

                    if (tankModel.Geometry.HasBottomWidth) {
                        $('#bottomWidth').val(tankModel.BottomWidth);
                        $('#divBottomWidth').visible();
                    }
                    else {
                        $('#divBottomWidth').invisible();
                    }

                    if (tankModel.Geometry.HasDimension1) {
                        $('#dimension1').val(tankModel.Dimension1);
                        $('#divDimension1').visible();
                    }
                    else {
                        $('#divDimension1').invisible();
                    }

                    if (tankModel.Geometry.HasDimension2) {
                        $('#dimension2').val(tankModel.Dimension2);
                        $('#divDimension2').visible();
                    }
                    else {
                        $('#divDimension2').invisible();
                    }

                    if (tankModel.Geometry.HasDimension3) {
                        $('#dimension3').val(tankModel.Dimension3);
                        $('#divDimension3').visible();
                    }
                    else {
                        $('#divDimension3').invisible();
                    }

                    if (tankModel.Geometry.HasMinimumDistance) {
                        $('#minimumDistance').val(tankModel.MinimumDistance);
                        $('#divMinimumDistance').visible();
                    }
                    else {
                        $('#divMinimumDistance').invisible();
                    }

                    if (tankModel.Geometry.HasMaximunDistance) {
                        $('#maximumDistance').val(tankModel.MaximumDistance);
                        $('#divMaximumDistance').visible();
                    }
                    else {
                        $('#divMaximumDistance').invisible();
                    }
                }
            },
            error: function (jqXHR, exception) {
                console.log('Error - Get Tank Model' + exception);
            }
        });
    }
    else {
        $('#divtankModel').invisible();
    }
};

function generateWaterVolumeGraph(tankId) {
    $.ajax({
        url: getUrlBase() + "/Customer/Tank/GetWaterInfo/",
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
}