
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

function initView() {
    //var voltage_y = [];
    //var voltage_x = [];

    //for (var i = 0; i < 5; i++) {
    //    voltage_x.push([new Date().subtractHours(i), Math.sin(i)]);
    //}

    //for (var i = 0; i < 6000; i += 2000) {
    //    voltage_y.push([i, Math.sin(i)]);
    //}

    ////var volts = [[(new Date()).getTime(), 2000], [(new Date()).getTime(), 4000]];
    ////$.plot("#voltage-graph", [voltage_y, volts]);

    ////$.plot($("#voltage-graph"), []);

    //$.plot($("#voltage-graph"), [{ data: voltage_y, lines: { show: true }, label: "Volts" },
    //    { data: voltage_x, lines: { show: true }, label: "" }],
    //    { yaxis: { label: "mV" } },
    //    {
    //        xaxis:
    //           {
    //               mode: "time",
    //               timeformat: "%M:%S"
    //           }
    //    });

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