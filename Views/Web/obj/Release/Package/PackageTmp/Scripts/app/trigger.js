function GetsPondAndTankSensorBySite(siteId, elementSite, elementPond, elementTank, elementSensor) {
    GetsPondBySite(siteId, elementPond, '/Trigger/GetsPondBySite');
    GetsTankBySite(siteId, elementTank, '/Trigger/GetsTankBySite');
    GetsSiteSensorBySite(siteId, elementSensor, '/Trigger/GetsSiteSensorBySite');
};

function GetsSiteSensorBySite(siteId, element, url) {
    if (siteId != "" && siteId != "0") {
        var procemessage = "<option value='0'> Please wait...</option>";
        $("#" + element).html(procemessage).show();

        $.ajax({
            url: url,
            data: { siteId: siteId },
            cache: false,
            type: "GET",
            success: function (data) {
                var markup = "<option value=''>-- Please select a Sensor -- </option>";
                for (var x = 0; x < data.length; x++) {
                    markup += "<option value=" + data[x].Value + ">" + data[x].Text + "</option>";
                }
                $("#" + element).html(markup).show();
                $("#" + element).removeAttr("disabled");
            },
            error: function (reponse) {
                notifiyError("error : " + reponse);
            }
        });
    }
};

function GetsPondBySite(siteId, element, url) {
    if (siteId != "" && siteId != "0") {
        var procemessage = "<option value='0'> Please wait...</option>";
        $("#" + element).html(procemessage).show();

        $.ajax({
            url: url,
            data: { siteId: siteId },
            cache: false,
            type: "GET",
            success: function (data) {
                var markup = "<option value=''>-- Please select a Pond -- </option>";
                for (var x = 0; x < data.length; x++) {
                    markup += "<option value=" + data[x].Value + ">" + data[x].Text + "</option>";
                }

                if (data.length > 0)
                    $("#" + element).empty();

                $("#" + element).html(markup).show();
                $("#" + element).removeAttr("disabled");
            },
            error: function (reponse) {
                notifiyError("error : " + reponse);
            }
        });
    }
};

function GetsTankBySite(siteId, element, url) {
    if (siteId != "" && siteId != "0") {
        var procemessage = "<option value='0'> Please wait...</option>";
        $("#" + element).html(procemessage).show();

        $.ajax({
            url: url,
            data: { siteId: siteId },
            cache: false,
            type: "GET",
            success: function (data) {
                var markup = "<option value=''>-- Please select a Tank -- </option>";
                for (var x = 0; x < data.length; x++) {
                    markup += "<option value=" + data[x].Value + ">" + data[x].Text + "</option>";
                }

                if (data.length > 0)
                    $("#" + element).empty();

                $("#" + element).html(markup).show();
                $("#" + element).removeAttr("disabled");
            },
            error: function (reponse) {
                notifiyError("error : " + reponse);
            }
        });
    }
};

function GetsSensorByPond(pondId, element) {
    if (pondId != "" && pondId != "0") {
        var procemessage = "<option value='0'> Please wait...</option>";
        $("#" + element).html(procemessage).show();

        $.ajax({
            url: '/Trigger/GetsPondSensorByPond',
            data: { pondId: pondId },
            cache: false,
            type: "GET",
            success: function (data) {
                var markup = "<option value=''>-- Please select a Sensor -- </option>";
                for (var x = 0; x < data.length; x++) {
                    markup += "<option value=" + data[x].Value + ">" + data[x].Text + "</option>";
                }

                if (data.length > 0)
                    $("#" + element).empty();

                $("#" + element).html(markup).show();
                $("#" + element).removeAttr("disabled");
            },
            error: function (reponse) {
                notifiyError("error : " + reponse);
            }
        });
    }
};

function GetsSensorByTank(tankId, element) {
    if (tankId != "" && tankId != "0") {
        var procemessage = "<option value='0'> Please wait...</option>";
        $("#" + element).html(procemessage).show();

        $.ajax({
            url: '/Trigger/GetsTankSensorByTank',
            data: { tankId: tankId },
            cache: false,
            type: "GET",
            success: function (data) {
                var markup = "<option value=''>-- Please select a Sensor -- </option>";
                for (var x = 0; x < data.length; x++) {
                    markup += "<option value=" + data[x].Value + ">" + data[x].Text + "</option>";
                }

                if (data.length > 0)
                    $("#" + element).empty();

                $("#" + element).html(markup).show();
                $("#" + element).removeAttr("disabled");
            },
            error: function (reponse) {
                notifiyError("error : " + reponse);
            }
        });
    }
};

// When select a sensor, fill SensorItem values
function GetsSensorItemBySensor(sensorId, element) {
    if (sensorId != "" && sensorId != "0") {
        var procemessage = "<option value='0'> Please wait...</option>";
        $("#" + element).html(procemessage).show();

        $.ajax({
            url: '/Trigger/GetsSensorItemBySensor',
            data: { sensorId: sensorId },
            cache: false,
            type: "GET",
            success: function (data) {
                var markup = "<option value=''>-- Please select an Item -- </option>";
                for (var x = 0; x < data.length; x++) {
                    markup += "<option value=" + data[x].Value + ">" + data[x].Text + "</option>";
                }

                if (data.length > 0)
                    $("#" + element).empty();

                $("#" + element).html(markup).show();
                $("#" + element).removeAttr("disabled");
            },
            error: function (reponse) {
                notifiyError("error : " + reponse);
            }
        });
    }
};