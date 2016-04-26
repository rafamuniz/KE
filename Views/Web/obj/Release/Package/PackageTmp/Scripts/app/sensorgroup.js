function GetsTankBySiteId(siteId, element, url) {
    if (siteId != "" || siteId != "0") {
        var procemessage = "<option value='0'> Please wait...</option>";
        $("#" + element).html(procemessage).show();
        //var url = url;

        $.ajax({
            url: url,
            data: { siteId: siteId },
            cache: false,
            type: "POST",
            success: function (data) {
                var markup = "<option value='0'>-- Please select a Tank -- </option>";
                for (var x = 0; x < data.length; x++) {
                    markup += "<option value=" + data[x].Value + ">" + data[x].Text + "</option>";
                }
                $("#" + element).html(markup).show();
            },
            error: function (reponse) {
                showNotify("error : " + reponse);
            }
        });
    }
};

function GetsSensorByTankId(tankId, element, url) {
    if (tankId != "" || tankId != "0") {
        var procemessage = "<option value='0'> Please wait...</option>";
        $("#" + element).html(procemessage).show();

        $.ajax({
            url: url,
            data: { tankId: tankId },
            cache: false,
            type: "POST",
            success: function (data) {
                var markup = "<option value='0'>-- Please select a Sensor -- </option>";
                for (var x = 0; x < data.length; x++) {
                    markup += "<option value=" + data[x].Value + ">" + data[x].Text + "</option>";
                }
                $("#" + element).html(markup).show();
            },
            error: function (reponse) {
                showNotify("error : " + reponse);
            }
        });
    }
};