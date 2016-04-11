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
    $.fn.hide = function () {
        return this.each(function () {
            $(this).css("visibility", "hidden");
        });
    };

    $.fn.show = function () {
        return this.each(function () {
            $(this).css("visibility", "visible");
        });
    };

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