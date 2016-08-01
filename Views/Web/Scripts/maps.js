function initMap(latitude, longitude) {
    if (latitude != 'undefined' && longitude != 'undefined') {
        var mapOptions = {
            center: { lat: latitude, lng: longitude },
            scrollwheel: false,
            zoom: 13,
            mapTypeId: google.maps.MapTypeId.ROADMAP
        };
        var map = new google.maps.Map(document.getElementById('map'), mapOptions);

        var location = new google.maps.LatLng(latitude, longitude);

        var infowindow = new google.maps.InfoWindow({
            content: ''
        });
        
        var marker = new google.maps.Marker({
            position: location,
            map: map,
            title: 'Tate Gallery'
        });

        var circle = new google.maps.Circle({            
            map: map,
            radius: 3000, // 10 miles in metres - 186411
            fillColor: '#AA0000'
        });
        circle.bindTo('center', marker, 'position');     
    }
}