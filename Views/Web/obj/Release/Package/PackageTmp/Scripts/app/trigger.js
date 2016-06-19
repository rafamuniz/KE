function GetsTankAndSiteSensorBySite(siteId, elementTank, elementSensor) {
    GetsTankBySiteId(siteId, elementTank, '/Trigger/GetsTankBySite');
    GetsSiteSensorBySiteId(siteId, elementSensor, '/Trigger/GetsSiteSensorBySite');
};

function GetsTankBySiteId(siteId, element, url) {
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
                $("#" + element).html(markup).show();
                $("#" + element).removeAttr("disabled");
            },
            error: function (reponse) {
                notifiyError("error : " + reponse);
            }
        });
    }
};

function GetsSiteSensorBySiteId(siteId, element, url) {
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

function GetsSensorByTank(tankId, element, url) {
    if (tankId != "" && tankId != "0") {
        var procemessage = "<option value='0'> Please wait...</option>";
        $("#" + element).html(procemessage).show();

        $.ajax({
            url: url,
            data: { tankId: tankId },
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

function GetsSensorItemBySensor(sensorId, element, url) {
    if (sensorId != "" && sensorId != "0") {
        var procemessage = "<option value='0'> Please wait...</option>";
        $("#" + element).html(procemessage).show();

        $.ajax({
            url: url,
            data: { sensorId: sensorId },
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