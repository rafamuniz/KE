
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

                    $('#imgTankModel').attr('src', getUrlBase() + '/images/tank_models/' + tankModel.ImageFilename);
                    $('#imgTankModel').removeAttr('style');

                    if (tankModel.Geometry.HasHeight) {
                        $('#height').val(tankModel.Height);
                    }
                    else {
                        $('#divHeight').invisible();
                    }

                    if (tankModel.Geometry.HasWidth) {
                        $('#Width').val(tankModel.Width);
                    }
                    else {
                        $('#divWidth').invisible();
                    }

                    if (tankModel.Geometry.HasLength) {
                        $('#length').val(tankModel.Length);
                    }
                    else {
                        $('#divLength').invisible();
                    }

                    if (tankModel.Geometry.HasMinimalDistance) {
                        $('#minDistance').val(tankModel.MinDistance);
                    }
                    else {
                        $('#divMinDistance').invisible();
                    }

                    if (tankModel.Geometry.HasMaximunDistance) {
                        $('#maxDistance').val(tankModel.MaxDistance);
                    }
                    else {
                        $('#divMaxDistance').invisible();
                    }
                }
            },
            error: function (jqXHR, exception) {
                console.log('Error - Get Tank Model' + exception);
            }
        });
    }
};