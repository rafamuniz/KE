
function getTanks() {
    var customerId = $('#hfCustomerId').val();

    $.ajax({
        url: "api/v1/json/customer/tank/gets/" + customerId,
        type: "GET",
        dataType: "json",
        success: function (data) {
            var html = "";
            $.get('@Url.Action("GetTankHTML", "Tank", new { area = "Customer" } )', function (data) {
                html = data;
            });

            var item_name = ReplaceAll(data.Name, "{NAME}", html);

        },
        error: function (jqXHR, exception) {
            console.log('Error - Get Tanks');
        }
    });
};