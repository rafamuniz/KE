function ReplaceAll(string, Find, Replace) {
    try {
        return string.replace(new RegExp(Find, "gi"), Replace);
    } catch (ex) {
        return string;
    }
}

String.prototype.fmt = function (hash) {
    var string = this, key; for (key in hash) string = string.replace(new RegExp('\\{' + key + '\\}', 'gm'), hash[key]); return string
}

function RedirectToUrl(url) {
    window.location.href = url;
}

function RedirectToUrl(action, controller) {
    window.location.href = "/" + controller + "/" + action;
}

function showLoading() {
    $('#divLoading').show();
}

function showLoading(element) {
    var e = '#' + element;
    var loading = $('divLoading');
    $(e).append(loading);
    $('#divLoading').show();
}

function hideLoading() {
    $('#divLoading').hide();
}

function notifiyError(message) {
    $.notify({
        message: message
    }, {
        type: 'danger'
    });
}

function notifiySuccess(message) {
    $.notify({
        message: message
    }, {
        type: 'success'
    });
}

function getUrlBase() {
    var url = '';

    if (typeof location.origin === 'undefined')
        url = location.protocol + '/' + location.host;
    else
        url = location.origin + '/';

    return url;
}

// DIALOG
function openDialog(action, title, dialogClass, width, height, oncomplete) {
    if (typeof (width) === 'undefined') width = 500;
    if (typeof (height) === 'undefined') height = 300;

    $("#dialog-box").load(action, function (responseText, textStatus, XMLHttpRequest) {
        var dialog = $("#dialog-box").dialog({
            autoOpen: false,
            title: title,
            width: width,
            height: height,
            modal: true,
            dialogClass: dialogClass,
            open: oncomplete
        });

        $("#dialog-box").dialog("open");

        return dialog;
    });
};

function populateDropdown(select, data) {
    select.html('');
    $.each(data, function (id, option) {
        select.append($('<option></option>').val(option.value).html(option.name));
    });
}

(function ($) {
    //$.fn.hide = function () {
    //    return this.each(function () {
    //        $(this).css("visibility", "hidden");
    //    });
    //};

    //$.fn.show = function () {
    //    return this.each(function () {
    //        $(this).css("visibility", "visible");
    //    });
    //};

    $.fn.invisible = function () {
        return this.each(function () {
            $(this).css("display", "none");
        });
    };

    $.fn.visible = function () {
        return this.each(function () {
            $(this).css("display", "");
        });
    };
}(jQuery));

function getGeoLocation(success, error) {
    var options = {
        enableHighAccuracy: true,
        timeout: 5000,
        maximumAge: 0
    };

    if (navigator.geolocation) {
        navigator.geolocation.getCurrentPosition(success, error, options)
    } else {
        showNotify("Geolocation is not supported by this browser.");
    }
};

function geoLocationSuccess(pos) {
    var crd = pos.coords;

    console.log('Your position:');
    console.log('Latitude: ' + crd.latitude);
    console.log('Longitude: ' + crd.longitude);
    console.log('Accuracy: ' + crd.accuracy + ' meters.');

    return crd;
};

function geoLocationError(err) {
    switch (error.code) {
        case error.PERMISSION_DENIED:
            x.innerHTML = "User denied the request for Geolocation."
            break;
        case error.POSITION_UNAVAILABLE:
            x.innerHTML = "Location information is unavailable."
            break;
        case error.TIMEOUT:
            x.innerHTML = "The request to get user location timed out."
            break;
        case error.UNKNOWN_ERROR:
            x.innerHTML = "An unknown error occurred."
            break;
    }
};


function blink(element, fadeout, fadin) {
    fadeout = typeof fadeout !== 'undefined' ? fadeout : 100;
    fadin = typeof fadin !== 'undefined' ? fadin : 100;

    $(element).fadeOut(fadeout).fadeIn(fadin, blink);
};