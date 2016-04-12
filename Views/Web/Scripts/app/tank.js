
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