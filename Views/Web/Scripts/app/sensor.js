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
                $("#" + element).html(markup).show();
                $("#" + element).removeAttr("disabled");
            },
            error: function (reponse) {
                notifiyError("error : " + reponse);
            }
        });
    }
};

function getsItemBySensorTypeId(items) {
    var val = $("#ddlSensorType").val();

    if (val != 0 && val != "") {
        $.ajax({
            url: getUrlBase() + "/Customer/Sensor/GetsItemBySensorTypeId/",
            type: "GET",
            dataType: "JSON",
            data: { sensorTypeId: val },
            success: function (data) {
                $('#divCheckboxlist').empty();
                showItems(items, data);
            },
            error: function (jqXHR, exception) {
                notifiyError('Error - Get Item By SensorType');
            }
        });
    }
    else {
        //$('#divtankModel').invisible();
    }
};

function showItems(itemsSelected, items) {
    $.each(items, function (i, v) {
        var html = '<div class="row"><div class="col-md-6 col-lg-6">{HIDDEN_ITEM_ID}{CHECKBOX}{HIDDEN_ITEM_NAME}</div><div class="col-md-6 col-lg-6">{DROPDOWNLIST}</div></div>';

        var hidden_item_id = '<input name="Items[' + i + '].Id" type="hidden" value="' + v.Id + '">';
        var hidden_item_name = '<input name="Items[' + i + '].Name" type="hidden" value="' + v.Name + '">';
        var hidden_item_isselected = '<input name="Items[' + i + '].IsSelected" type="hidden" value="' + v.IsSelected + '">';

        var checked = "";
        var foundItem = $.map(itemsSelected, function (val) {
            if (val.Id == v.Id && val.IsSelected == true)
                checked = "checked";
        });

        var checkbox = '<input type="checkbox" id="chk_' + v.Id + '" style="vertical-align: 3px;" name="Items[' + i + '].IsSelected" ' + checked + ' value="true" /><label for="chk_' + v.Id + '">' + v.Name + '</label>';

        var dropdownlist = "<select class='form-control selectpicker' data-val='true' id='ddlUnit_" + v.Id + "' name='Items[" + i + "].UnitSelected'>";
        dropdownlist += '<option value="">-- Please select an Unit --</option>';

        $.each(v.Units, function (iu, vu) {
            var selected = "";

            var foundUnit = $.map(itemsSelected, function (val) {
                if (val.UnitSelected != null && val.UnitSelected == vu.Id && val.Id == v.Id)
                    selected = "selected";
            })

            dropdownlist += '<option value="' + vu.Id + '"' + selected + '>' + vu.Name + '</option>';
        });

        dropdownlist += '</select>';

        html = ReplaceAll(html, "{HIDDEN_ITEM_ID}", hidden_item_id);
        html = ReplaceAll(html, "{HIDDEN_ITEM_NAME}", hidden_item_name);
        html = ReplaceAll(html, "{HIDDEN_ITEM_ISSELECTED}", hidden_item_isselected);
        html = ReplaceAll(html, "{CHECKBOX}", checkbox);
        html = ReplaceAll(html, "{DROPDOWNLIST}", dropdownlist);

        $('#divCheckboxlist').append(html);
    });
}

function showTankModel(tankModel, tank) {
    if (tankModel != null) {

        $('#divtankModel').visible();

        var image_url = getUrlBase() + '/images/tankmodels/' + tankModel.Id + '/' + ReplaceAll(tankModel.ImageFilename, "{info}", "measure");
        $('#imgTankModel').attr('src', image_url);
        $('#imgTankModel').removeAttr('style');

        if (tankModel.Geometry.HasHeight) {
            $('#height').val(tank != null && tank.Height != null ? tank.Height : tankModel.Height);
            $('#divHeight').visible();
        }
        else {
            $('#divHeight').invisible();
        }

        if (tankModel.Geometry.HasWidth) {
            $('#width').val(tank != null && tank.Width != null ? tank.Width : tankModel.Width);
            $('#divWidth').visible();
        }
        else {
            $('#divWidth').invisible();
        }

        if (tankModel.Geometry.HasLength) {
            $('#length').val(tank != null && tank.Length != null ? tank.Length : tankModel.Length);
            $('#divLength').visible();
        }
        else {
            $('#divLength').invisible();
        }

        if (tankModel.Geometry.HasFaceLength) {
            $('#faceLength').val(tank != null && tank.FaceLength != null ? tank.FaceLength : tankModel.FaceLength);
            $('#divFaceLength').visible();
        }
        else {
            $('#divFaceLength').invisible();
        }

        if (tankModel.Geometry.HasBottomWidth) {
            $('#bottomWidth').val(tank != null && tank.BottomWidth != null ? tank.BottomWidth : tankModel.BottomWidth);
            $('#divBottomWidth').visible();
        }
        else {
            $('#divBottomWidth').invisible();
        }

        if (tankModel.Geometry.HasDimension1) {
            $('#dimension1').val(tank != null && tank.Dimension1 != null ? tank.Dimension1 : tankModel.Dimension1);
            $('#divDimension1').visible();
        }
        else {
            $('#divDimension1').invisible();
        }

        if (tankModel.Geometry.HasDimension2) {
            $('#dimension2').val(tank != null && tank.Dimension2 != null ? tank.Dimension2 : tankModel.Dimension2);
            $('#divDimension2').visible();
        }
        else {
            $('#divDimension2').invisible();
        }

        if (tankModel.Geometry.HasDimension3) {
            $('#dimension3').val(tank != null && tank.Dimension3 != null ? tank.Dimension3 : tankModel.Dimension3);
            $('#divDimension3').visible();
        }
        else {
            $('#divDimension3').invisible();
        }

        if (tankModel.Geometry.HasMinimumDistance) {
            $('#minimumDistance').val(tank != null && tank.MinimumDistance != null ? tank.MinimumDistance : tankModel.MinimumDistance);
            $('#divMinimumDistance').visible();
        }
        else {
            $('#divMinimumDistance').invisible();
        }

        if (tankModel.Geometry.HasMaximunDistance) {
            $('#maximumDistance').val(tank != null && tank.MaximumDistance != null ? tank.MaximumDistance : tankModel.MaximumDistance);
            $('#divMaximumDistance').visible();
        }
        else {
            $('#divMaximumDistance').invisible();
        }

        $('#waterVolumeCapacity').val(tank != null && tank.WaterVolumeCapacity != null ? tank.WaterVolumeCapacity : tankModel.WaterVolumeCapacity);
    }
}