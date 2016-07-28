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

Date.prototype.addHours = function (h) {
    this.setTime(this.getTime() + (h * 60 * 60 * 1000));
    return this;
}

Date.prototype.subtractHours = function (h) {
    this.setTime(this.getTime() - (h * 60 * 60 * 1000));
    return this;
}

/* DATE & TIME */