$.ajaxjson = function (url, dataMap, fnSuccess) {
    $.ajax({
        type: "POST",
        url: url,
        data: dataMap,
        dataType: "json",
        beforeSend: function () { top.$.hLoading.show(); },
        complete: function () { top.$.hLoading.hide(); },
        success: fnSuccess
    });
}
$.ajaxtext = function (url, dataMap, fnSuccess) {
    $.ajax({
        type: "POST",
        url: url,
        data: dataMap,
        beforeSend: function () { top.$.hLoading.show(); },
        complete: function () { top.$.hLoading.hide(); },
        success: fnSuccess
    });
}


function autoResize(options) {
    var defaults = {
        width: 10,
        height: 10,
        gridType: 'datagrid'
    };
    options = $.extend(defaults, options);

    // 第一次调用
    var wsize = getWidthAndHeigh();
    if ($.isFunction(options.callback)) {
        options.callback(wsize);
    }

    // 窗口大小改变的时候
    $(window).resize(function () {
        var size = getWidthAndHeigh(true);
        switch (options.gridType) {
            case "datagrid":
                $(options.dataGrid).datagrid('resize', { width: size.width, height: size.height });
                break;
            case "treegrid":
                $(options.dataGrid).treegrid('resize', { width: size.width, height: size.height });
                break;
            case "jqgrid":
                $(options.dataGrid).jqGrid('setGridHeight', size.height).jqGrid('setGridWidth', wsize.width);
                break;
        }
    });

    // 获取iframe大小
    function getWidthAndHeigh(resize) {
        var windowHeight = 0;
        var widowWidth = 0;
        if (typeof (window.innerHeight) == 'number') {
            windowHeight = window.innerHeight;
            widowWidth = window.innerWidth;
        }
        else {
            if (document.documentElement && document.documentElement.clientHeight) {
                windowHeight = document.documentElement.clientHeight;
                widowWidth = document.documentElement.clientWidth;
            }
            else {
                if (document.body && document.body.clientHeight) {
                    windowHeight = document.body.clientHeight;
                    widowWidth = document.body.clientWidth;
                }
            }
        }


        widowWidth -= options.width;
        windowHeight -= options.height;


        return { width: widowWidth, height: windowHeight };
    }
}



function _StringFormatInline() {
    var txt = this;
    for (var i = 0; i < arguments.length; i++) {
        var exp = new RegExp('\\{' + (i) + '\\}', 'gm');
        txt = txt.replace(exp, arguments[i]);
    }
    return txt;
}

function _StringFormatStatic() {
    for (var i = 1; i < arguments.length; i++) {
        var exp = new RegExp('\\{' + (i - 1) + '\\}', 'gm');
        arguments[0] = arguments[0].replace(exp, arguments[i]);
    }
    return arguments[0];
}

if (!String.prototype.format) {
    String.prototype.format = _StringFormatInline;
}

if (!String.format) {
    String.format = _StringFormatStatic;
}

//主要是推荐这个函数。它将jquery系列化后的值转为name:value的形式。
function convertArray(o) {
    var v = {};
    for (var i in o) {
        if (o[i].name != '__VIEWSTATE') {
            if (typeof (v[o[i].name]) == 'undefined')
                v[o[i].name] = o[i].value;
            else
                v[o[i].name] += "," + o[i].value;
        }
    }
    return v;
}

/*
随机字符串 
length : 字符串长度
*/
function randomString(length) {
    var chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
    var size = length || 8;
    var i = 1;
    var ret = "";
    while (i <= size) {
        var max = chars.length - 1;
        var num = Math.floor(Math.random() * max);
        var temp = chars.substr(num, 1);
        ret += temp;
        i++;
    }
    return ret;
}


function MessageOrRedirect(d) {
    if (d) {
        if (d.Data == "-99")
            top.$.hLoading.show({
                type: 'hits',
                msg: d.Message,
                onAfterHide: function () {
                    top.location.href = "/login.html";
                },
                timeout: 1000
            });

        else {
            top.$.messager.alert('系统提示', d.Message, 'warning');
        }
    }
}


function ExporterExcel(grid) {
    //获取Datagride的列
    var rows = $('#' + grid).datagrid('getRows');
    var columns = $("#" + grid).datagrid("options").columns[0];
    var oXL = new ActiveXObject("Excel.Application"); //创建AX对象excel
    var oWB = oXL.Workbooks.Add(); //获取workbook对象
    var oSheet = oWB.ActiveSheet; //激活当前sheet
    //设置工作薄名称
    oSheet.name = "导出Excel报表";
    //设置表头
    for (var i = 0; i < columns.length; i++) {
        oSheet.Cells(1, i + 1).value = columns[i].title;
    }
    //设置内容部分
    for (var i = 0; i < rows.length; i++) {
        //动态获取每一行每一列的数据值
        for (var j = 0; j < columns.length; j++) {
            oSheet.Cells(i + 2, j + 1).value = rows[i][columns[j].field];
        }
    }
    oXL.Visible = true; //设置excel可见属性
}

function fmoney(s, n) {

    var tm = n;
    if (s == "" || s == null) return "0";
    n = n > 0 && n <= 20 ? n : 2;
    s = parseFloat((s + "").replace(/[^\d\.-]/g, "")).toFixed(n) + ""; //更改这里n数也可确定要保留的小数位  
    var l = s.split(".")[0].split("").reverse(),
           r = s.split(".")[1];
    t = "";
    for (i = 0; i < l.length; i++) {
        t += l[i] + ((i + 1) % 3 == 0 && (i + 1) != l.length ? "," : "");
    }
    if (tm > 0) {
        return t.split("").reverse().join("") + "." + r.substring(0, 2); //保留2位小数  如果要改动 把substring 最后一位数改动就可  
    }
    else {
        return t.split("").reverse().join("");
    }
}

function statuscolor(status, text) {
    if (status == 2) {//待审核
        return '<font color="blue">' + text + '</font>';
    }
    else if (status == 3) {//审核通过
        return '<font color="Green">' + text + '</font>';
    }
    else if (status == 4) {//审核失败
        return '<font color="red">' + text + '</font>';
    }
    else if (status == 5) {//单据关闭
        return '<font color="Gray">' + text + '</font>';
    }
    else {
        return text;
    }
}

function request(paras) {
    var url = location.href;
    var paraString = url.substring(url.indexOf("?") + 1, url.length).split("&");
    var paraObj = {}
    for (i = 0; j = paraString[i]; i++) {
        paraObj[j.substring(0, j.indexOf("=")).toLowerCase()] = j.substring(j.indexOf("=") + 1, j.length);
    }
    var returnValue = paraObj[paras.toLowerCase()];
    if (typeof (returnValue) == "undefined") {
        return "";
    } else {
        return returnValue;
    }
}
