
function getTanks() {
    var customerId = $('#hfCustomerId').val();

    $.ajax({
        url: getUrlBase() + "api/v1/json/customer/tank/gets/" + customerId,
        type: "GET",
        dataType: "json",
        success: function (data) {
            var tanks = data;

            $.ajax({
                async: false,
                type: 'GET',
                url: getUrlBase() + "/Customer/Tank/GetTankHTML",
                success: function (data) {
                    if (tanks.length > 0) {
                        var html = data;
                        $.each(tanks, function (index, value) {
                            var html_tank = "<div class='col-lg-6 col-sm-12 col-xs-12'>" + html;
                            var html_tank = ReplaceAll(html_tank, "{NAME}", value.Name);
                            var html_tank = ReplaceAll(html_tank, "{VOLUME}", value.WaterVolumeCapacity);
                            html_tank += "</div>";
                            $('#tank-dashboard').append(html_tank);
                        });
                    }
                }
            });
        },
        error: function (jqXHR, exception) {
            console.log('Error - Get Tanks');
        }
    });
};