function LoadMonthDropDownListValue() {
    var value = "";

    value += "<option value='-1'>全部</option>";
    for (var i = 1; i < 13; i++)
        value += "<option value ='" + i + "'>" + i + " 月</option>";

    return value;
}

function LoadHourDropDownListValue(type) {
    var value = "";
    var i;

    if (type == 3) {
        for (i = 0; i <= 23; i++) {
            if (i - 12 <= 0)
                value += " <option value='" + i + "'>" + i + " AM</option>";
            else
                value += " <option value='" + i + "'>" + (i - 12) + " PM</option>";
        }
    }
    else {
        for (i = 9; i <= 18; i++) {
            if (i - 12 <= 0)
                value += " <option value='" + i + "'>" + i + " AM</option>";
            else
                value += " <option value='" + i + "'>" + (i - 12) + " PM</option>";
        }
    }

    return value;
}

function LoadDropDownListValue(type) {
    var value = "<option value='-1'>全部</option>";
    $.ajax({
        type: "POST",
        url: "WebService.asmx/GetDropDownListValue",
        data: "{type:'" + type + "'}",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        async:false,
        success: function (Msg) {
            var json = eval(Msg.d);
            for (var i = 0; i < json.length; i++) {
                 value += "<option value = '" + json[i].Value + "'>" + json[i].Text + "</option>";
            }
        },
        error: function (xhr, status, error) {
            alert(xhr.responseText);
        }
    });
    return value;
}

function LoadYearDropDownListValue(data) {
    var value = "";

    value += "<option value='-1'>全部</option>";
    for (var i = 0; i < data.length; i++)
        value += "<option value ='" + data[i].Year + "'>" + data[i].Year + " 年</option>";

    return value;
}

function LoadTeamDropDownListValue(data) {
    var value = "";

    value += "<option value='-1'>全部</option>";
    for (var i = 0; i < data.length; i++)
        value += "<option value ='" + data[i].TeamID + "'>" + data[i].TeamName + "</option>";

    return value;
}

function LoadMemberDropDownListValue(data) {
    var value = "";

    value += "<option value='-1'>全部</option>";
    for (var i = 0; i < data.length; i++)
        value += "<option value ='" + data[i].EmployeeID + "'>" + data[i].EmployeeEName + "</option>";

    return value;
}

function LoadAllMemberDropDownListValue() {
    var value = "";
    value += "<option value='-1'>全部</option>";

    $.ajax({
        type: "POST",
        url: "WebService.asmx/GetAllEmployeeNameValue",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        async: false,
        success: function (Msg) {
            var json = eval(Msg.d);
            for (var i = 0; i < json.length; i++) {
                value += "<option value ='" + json[i].EmployeeID + "'>" + json[i].EmployeeEName + "</option>";
            }
        },
        error: function (xhr, status, error) {
            alert(xhr.responseText);
        }
    });
    return value;
}

function DateValidate(StartDate, EndDate, StartHour, EndHour) {
    var DateCheck = false;

    var DateFrom = new Date(StartDate + " " + StartHour + ":00");
    var DateTo = new Date(EndDate + " " + EndHour + ":00");

    if (StartDate != "" && DateFrom < DateTo)
        DateCheck = true;

    if (DateCheck)
        $('#DateWarning').html("<span style='color:black;font-weight:bold;'>" + HoursCalculate() + "小時</span>");
    else if (StartDate == "")
        $('#DateWarning').text("請選擇日期");
    else if ((new Date(StartDate)).getMonth() != (new Date(EndDate)).getMonth())
        $('#DateWarning').text("日期時段不能跨月");
    else if ((new Date(StartDate)).getFullYear() != (new Date(EndDate)).getFullYear())
        $('#DateWarning').text("日期時段不能跨年");
    else
        $('#DateWarning').text("日期時段錯誤");

    return DateCheck;
}

function TypeValidate(VacationType, WorkPosition) {
    if (VacationType < 0)
        return false;
    else if (VacationType == 3 && WorkPosition < 0)
        return false;
    else
        return true;
}

function HoursValidate(DateCheck) {
    var HoursCheck = false;

    if ($('#VacationType').val() == 0 || $('#VacationType').val() == 1) {
        if (parseInt($('#TypeWarning').text().split(" ")[2], 10) >= parseInt($('#DateWarning').text(), 10))
            HoursCheck = true;
    }
    else
        HoursCheck = true;

    if (DateCheck && !HoursCheck)
        $('#HoursWarning').text("超過可休假時數");
    else
        $('#HoursWarning').text("");

    return HoursCheck;
}

function NoteValidate(Note) {
    if (Note.replace(/(^\s*)|(\s*$)/g, "").length < 5 || Note.replace(/(^\s*)|(\s*$)/g, "").length >= 400)
        return false;
    else
        return true;
}

function IsTel(data) {
    var reg = /^([0-9]|[\-])+$/g;

    if (data.length < 7 || data.length > 10)
        return false;
    else
        return reg.test(data);
}

function ReplaceAll(strOrg,strFind,strReplace)
{
    return strOrg.replace(new RegExp(strFind,"g"),strReplace);
}

function HoursCalculate() {
    var hours = 0;

    var VacationType = $('#VacationType').val();
    if (VacationType == null)
        VacationType = 0;

    var Information = "";

    Information += "{";
    Information += "DateFrom:'" + $('#StartDate').val() + " " + $('#StartHour').val() + ":00',";
    Information += "DateTo:'" + $('#EndDate').val() + " " + $('#EndHour').val() + ":00',";
    Information += "VacationTypeID:" + VacationType;
    Information += "}";

    $.ajax({
        type: "POST",
        url: "WebService.asmx/CalculateWorkingHours",
        data: Information,
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        async: false,
        success: function (Msg) {
            hours = parseInt(Msg.d);
        },
        error: function (xhr, status, error) {
            alert(xhr.responseText);
        }
    });

    return hours;
}

function GetRemainHours(VacationTypeID, Year) {
    var hours;
    $.ajax({
        type: "POST",
        url: "WebService.asmx/GetRemainHours",
        data: "{VacationTypeID:" + VacationTypeID + ", Year:" + Year + "}",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        async: false,
        success: function (Msg) {
            var json = eval(Msg.d);
            hours = json[0].Hours;
        },
        error: function (xhr, status, error) {
            alert(xhr.responseText);
        }
    });

    return hours;
}

function DateDeserialize(dateStr) {
    return eval('new' + dateStr.replace(/\//g, ' '));
} 

function ToDateString(dateStr) {
    var DateTime = new Date(DateDeserialize(dateStr));
    var Month = DateTime.getMonth() + 1;
    var Day = DateTime.getDate();
    var Hours = DateTime.getHours();
    var Minutes = DateTime.getMinutes();
    var Seconds = DateTime.getSeconds();
    if (Month < 10)
        Month = "0" + Month;
    if (Day < 10)
        Day = "0" + Day;
    if (Hours < 13) {
        if (Hours < 10)
            Hours = "0" + Hours;
        Hours = " am " + Hours;
    }
    else {
        Hours -= 12
        if (Hours < 10)
            Hours = "0" + Hours;
        Hours = " pm " + Hours;
    }
    
    if (Minutes < 10)
        Minutes = "0" + Minutes;
    if (Seconds < 10)
        Seconds = "0" + Seconds;
    return DateTime.getFullYear() + "-" + Month + "-" + Day + Hours + ":" + Minutes + ":" + Seconds;
}

isNumber = function (e) {
  //  alert(e.keyCode);
  //  alert(e.which);
    if ($.browser.msie) {
        if (((event.keyCode > 47) && (event.keyCode < 58)) || (event.keyCode == 8) || (event.keyCode == 45) || (event.keyCode == 46))
            return true;
        else
            return false;
    }
    else {
        if (((e.which > 47) && (e.which < 58)) || (e.which == 8) || (e.which == 45) || (e.which == 46))
            return true;
        else
            return false;

    }
}

var ArrayList = function () {
    var args = ArrayList.arguments;
    var initialCapacity = 10;

    if (args != null && args.length > 0) {
        initialCapacity = args[0];
    }

    var elementData = new Array(initialCapacity);
    var elementCount = 0;

    this.size = function () {
        return elementCount;
    };

    this.add = function (element) {
        //alert("add");
        ensureCapacity(elementCount + 1);
        elementData[elementCount++] = element;
        return true;
    };

    this.addElementAt = function (index, element) {
        //alert("addElementAt");
        if (index > elementCount || index < 0) {
            alert("IndexOutOfBoundsException, Index: " + index + ", Size: " + elementCount);
            return;
            //throw (new Error(-1,"IndexOutOfBoundsException, Index: "+index+", Size: " + elementCount));
        }
        ensureCapacity(elementCount + 1);
        for (var i = elementCount + 1; i > index; i--) {
            elementData[i] = elementData[i - 1];
        }
        elementData[index] = element;
        elementCount++;
    };

    this.setElementAt = function (index, element) {
        //alert("setElementAt");
        if (index > elementCount || index < 0) {
            alert("IndexOutOfBoundsException, Index: " + index + ", Size: " + elementCount);
            return;
            //throw (new Error(-1,"IndexOutOfBoundsException, Index: "+index+", Size: " + elementCount));
        }
        elementData[index] = element;
    };

    this.toString = function () {
        //alert("toString()");
        var str = "{";
        for (var i = 0; i < elementCount; i++) {
            if (i > 0) {
                str += ",";
            }
            str += elementData[i];
        }
        str += "}";
        return str;
    };

    this.get = function (index) {
        //alert("elementAt");
        if (index >= elementCount) {
            alert("ArrayIndexOutOfBoundsException, " + index + " >= " + elementCount);
            return;
            //throw ( new Error( -1,"ArrayIndexOutOfBoundsException, " + index + " >= " + elementCount ) );
        }
        return elementData[index];
    };

    this.remove = function (index) {
        if (index >= elementCount) {
            alert("ArrayIndexOutOfBoundsException, " + index + " >= " + elementCount);
            //return;
            throw (new Error(-1, "ArrayIndexOutOfBoundsException, " + index + " >= " + elementCount));
        }
        var oldData = elementData[index];
        for (var i = index; i < elementCount - 1; i++) {
            elementData[i] = elementData[i + 1];
        }
        elementData[elementCount - 1] = null;
        elementCount--;
        return oldData;
    };

    this.isEmpty = function () {
        return elementCount == 0;
    };

    this.indexOf = function (elem) {
        //alert("indexOf");
        for (var i = 0; i < elementCount; i++) {
            if (elementData[i] == elem) {
                return i;
            }
        }
        return -1;
    };

    this.lastIndexOf = function (elem) {
        for (var i = elementCount - 1; i >= 0; i--) {
            if (elementData[i] == elem) {
                return i;
            }
        }
        return -1;
    };

    this.contains = function (elem) {
        return this.indexOf(elem) >= 0;
    };

    function ensureCapacity(minCapacity) {
        var oldCapacity = elementData.length;
        if (minCapacity > oldCapacity) {
            var oldData = elementData;
            var newCapacity = parseInt((oldCapacity * 3) / 2 + 1);
            if (newCapacity < minCapacity) {
                newCapacity = minCapacity;
            }
            elementData = new Array(newCapacity);
            for (var i = 0; i < oldCapacity; i++) {
                elementData[i] = oldData[i];
            }
        }
    }
};