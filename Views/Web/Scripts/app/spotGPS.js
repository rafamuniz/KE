//<message clientUnixTime="0">
//				<id>4937060</id>
//				<messengerId>0-8356068</messengerId>
//				<messengerName>Spot2</messengerName>
//				<unixTime>1364908774</unixTime>
//				<messageType>CUSTOM</messageType>
//				<latitude>45.42249</latitude>
//				<longitude>-111.68832</longitude>
//				<modelId>SPOT2</modelId>
//				<showCustomMsg>Y</showCustomMsg>
//				<dateTime>2013-04-02T06:19:34-0700</dateTime>
//				<hidden>0</hidden>
//				<messageContent>This is a custom message</messageContent>
//			</message>
function getGPSInfo() {
    var spotId = $("txtSporId");
    var url = $("hfUrl");
    url = ReplaceAll(url, "[SPOTID]", spotId);

    $.getJSON(url, function (data) {

        var message = JSON.parse(data);

        if (message != 'undefined') {
            $("txtSpotName").val(message.messengerName);
            //$("txtBattery").val();
            //$("txtLastContact").val();
            //$("txtLastLocator").val();
            $("txtModel").val(message.modelId);

            $("txtLatitude").val(message.latitude);
            $("txtLongitude").val(message.longitude);
        }
    });
}