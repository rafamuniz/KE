
function getsItemBySensorTypeId(element) {
    if (element.value != 0 && element.value != "") {
        $.ajax({
            url: getUrlBase() + "/Customer/Sensor/GetsItemBySensorTypeId/",
            type: "GET",
            dataType: "JSON",
            data: { sensorTypeId: element.value },
            success: function (data) {
                $('#divCheckboxlist').empty();
                showItems(data);
            },
            error: function (jqXHR, exception) {
                notifiyError('Error - Get Item By SensorType' + exception);
            }
        });
    }
    else {
        //$('#divtankModel').invisible();
    }
};

function showItems(items) {
    $.each(items, function (i, v) {
        var html = '<div class="row"><div class="col-md-6 col-lg-6">{HIDDEN}{CHECKBOX}{HIDDEN_CHECKBOX}</div><div class="col-md-6 col-lg-6">{DROPDOWNLIST}</div></div>';

        var hidden = '<input name="Items[' + i + '].Id" type="hidden" value="' + v.Id + '">';
        var checkbox = '<input type="checkbox" style="vertical-align: 3px;" name="Items[' + i + '].IsSelected" value="true" /><label>' + v.Name + '</label>';
        var hidden_checkbox = '<input name="Items[' + i + '].IsSelected" type="hidden" value="false">';

        var dropdownlist = '<select class="form-control selectpicker" data-val="true" id="ddlUnit_"' + v.Id + '"name=Items["' + i + '"].UnitSelected">';
        dropdownlist += '<option value="">-- Please select an Unit --</option>';

        $.each(v.Units, function (iu, vu) {
            dropdownlist += '<option value="' + vu.Id + '">' + vu.Name + '</option>';
        });

        dropdownlist += '</select>';

        html = ReplaceAll(html, "{HIDDEN}", hidden);
        html = ReplaceAll(html, "{CHECKBOX}", checkbox);
        html = ReplaceAll(html, "{HIDDEN_CHECKBOX}", hidden_checkbox);
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