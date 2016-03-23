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

/* DATE & TIME */
function toDateTime(secs) {
    var t = new Date(1970, 0, 1);
    t.setSeconds(secs);
    return t;
}

// Format: 15:06:38 PM
function displayTime() {
    var str = "";

    var currentTime = new Date()
    var hours = currentTime.getHours()
    var minutes = currentTime.getMinutes()
    var seconds = currentTime.getSeconds()

    if (minutes < 10) {
        minutes = "0" + minutes
    }
    if (seconds < 10) {
        seconds = "0" + seconds
    }
    str += hours + ":" + minutes + ":" + seconds + " ";
    if (hours > 11) {
        str += "PM"
    } else {
        str += "AM"
    }
    return str;
}

// Format: mm/dd/yy hh:mi:ss
function getTimeStamp() {
    var now = new Date();
    return ((now.getMonth() + 1) + '/' +
            (now.getDate()) + '/' +
             now.getFullYear() + " " +
             now.getHours() + ':' +
             ((now.getMinutes() < 10)
                 ? ("0" + now.getMinutes())
                 : (now.getMinutes())) + ':' +
             ((now.getSeconds() < 10)
                 ? ("0" + now.getSeconds())
                 : (now.getSeconds())));
}

Date.prototype.toUTC = function () {
    var self = this;
    return new Date(self.getUTCFullYear(), self.getUTCMonth(), self.getUTCDate(), self.getUTCHours(), self.getUTCMinutes());
};

Date.prototype.getUTCUnixTime = function () {
    return Math.floor(new Date(
      this.getUTCFullYear(),
      this.getUTCMonth(),
      this.getUTCDate(),
      this.getUTCHours(),
      this.getUTCMinutes(),
      this.getUTCSeconds()
    ).getTime() / 1000);
}

Date.prototype.toUTCSeconds = function () {
    var self = this;
    var dateUTC = new Date(self.getUTCFullYear(), self.getUTCMonth(), self.getUTCDate(), self.getUTCHours(), self.getUTCMinutes());
    return dateUTC.getSeconds();
};

Date.prototype.get12Hour = function () {
    var h = this.getHours();
    if (h > 12) { h -= 12; }
    if (h == 0) { h = 12; }
    return h;
};

Date.prototype.getAMPM = function () {
    return (this.getHours() < 12) ? "AM" : "PM";
};

Date.prototype.toShortTime = function () {
    return this.get12Hour() + ":" + this.getMinutes() + " " + this.getAMPM();
};

/* DATE & TIME */