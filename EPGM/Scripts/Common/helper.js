var urlContent = (typeof urlContent === 'undefined') ? "/" : urlContent;

function ajaxGet(url, successFunction) {
    $.ajax({
        type: 'get',
        url: url,
        cache: false,
        success: successFunction,
        error: function (xhr, status, error) {

            // var err = eval("(" + xhr.responseText + ")");
            //alert(err.Message);
        }
    });
}

function openModal(url, divContainer, callbackFunction) {
    ajaxGet(url, function (html) {
        $(divContainer).html(html);
        $(divContainer).modal('show');
    });
    //closePopup = function () {
    //    $(divContainer).modal('hide');
    //    $(divContainer).html("");
    //    callbackFunction();
    //}
}

//ajaxGetWithData(url, rowData, function (html) {
//    $(divContainer).html(html);
//});

function openModalWithData(url, divContainer, rowData, callbackFunction) {
    ajaxGetWithData(url, rowData, function (html) {
        $(divContainer).html(html);
    });
    closePopup = function () {
        $(divContainer).modal('hide');
        $(divContainer).html("");
        callbackFunction();
    }
}

closePopup = function () {
    $(divContainer).modal('hide');
    $(divContainer).html("");
    callbackFunction();
}

function GeneratePieChart(chartName, normal, moderate, severe)
{
    debugger;
    var pieChartCanvas = $(chartName).get(0).getContext("2d");
    var pieChart = new Chart(pieChartCanvas);
    var pieOptions = {
        //Boolean - Whether we should show a stroke on each segment
        segmentShowStroke: true,
        //String - The colour of each segment stroke
        segmentStrokeColor: "#fff",
        //Number - The width of each segment stroke
        segmentStrokeWidth: 1,
        //Number - The percentage of the chart that we cut out of the middle
        percentageInnerCutout: 50, // This is 0 for Pie charts
        //Number - Amount of animation steps
        animationSteps: 100,
        //String - Animation easing effect
        animationEasing: "easeOutBounce",
        //Boolean - Whether we animate the rotation of the Doughnut
        animateRotate: true,
        //Boolean - Whether we animate scaling the Doughnut from the centre
        animateScale: false,
        //Boolean - whether to make the chart responsive to window resizing
        responsive: true,
        // Boolean - whether to maintain the starting aspect ratio or not when responsive, if set to false, will take up entire container
        maintainAspectRatio: false,
        //String - A legend template
        legendTemplate: "<ul class=\"<%=name.toLowerCase()%>-legend\"><% for (var i=0; i<segments.length; i++){%><li><span style=\"background-color:<%=segments[i].fillColor%>\"></span><%if(segments[i].label){%><%=segments[i].label%><%}%></li><%}%></ul>",
        //String - A tooltip template
        tooltipTemplate: "<%=value %> <%=label%>"
    };

    var PieData = [
                { value: normal, color: "#00a65a", highlight: "#00a65a", label: "Normal" },
                { value: moderate, color: "#f39c12", highlight: "#f39c12", label: "Moderate" },
                { value: severe, color: "#dd4b39", highlight: "#dd4b39", label: "Severe" }
    ];

    //Create pie or douhnut chart
    //You can switch between pie and douhnut using the method below.
    pieChart.Doughnut(PieData, pieOptions);
}

function ajaxGetWithData(url, data, successFunction) {
    $.ajax({
        type: 'get',
        url: url,
        cache: false,
        data: data,
        success: successFunction,
        error: function (xhr, status, error) {
            var err = eval("(" + xhr.responseText + ")");
            alert(err.Message);
        }
    });
}

function ajaxPostData(url, formdata, callback) {
    debugger;
    var data = formdata;
    var call = callback;
    if (callback == null) {
        data = null;
        call = formdata;
    }
    $.ajax({
        type: 'POST',
        url: url,
        cache: false,
        data: data,
        error: function (xhr, status, error) {
            //var err = eval("(" + xhr.responseText + ")");
            alert(xhr.responseText);
        },
        success: call
    });
}

function ajaxPostForm(url, form, successFunction) {
    var data = new FormData(form); // serializes the form's elements.

    $.ajax({
        type: 'POST',
        url: appRoot + url,
        cache: false,
        data: data,
        success: successFunction,
        error: ajaxError,
        contentType: false,
        processData: false
    });
}

function addOptions(url, container, callback) {
    ajaxGet(urlContent + url, function (data) {
        var sel = $(container);
        sel.empty();
        for (var i = 0; i < data.length; i++) {
            sel.append('<option value="' + data[i].Value + '">' + data[i].Text + '</option>');
        }
        if (data.length > 0)
            sel.val(data[0].Value);
        else
            sel.val(-1);

        if (callback != null)
            callback();
    });
}

function ajaxGetJSON(url, successFunction) {
    $.ajax({
        type: 'get',
        url: url,
        dataType: 'json',
        cache: false,
        success: successFunction,
        error: function (xhr, status, error) {
            var err = eval("(" + xhr.responseText + ")");
            alert(err.Message);
        }
    });
}

function myPrint(id, ReportFor) {
    debugger;
    var dat = new Date();
    var month = (dat.getMonth() + 1) < 10 ? '0' + (dat.getMonth() + 1) : '' + (dat.getMonth() + 1);
    var day = (dat.getDate()) < 10 ? '0' + (dat.getDate()) : '' + (dat.getDate());
    var today = day + "-" + month + "-" + dat.getFullYear();
    var nowTime = dat.getHours() + ":" + dat.getMinutes();

    var divContents = $(id).html();
    var columnNames = $(id).jqGrid('getGridParam', 'colNames');

    var html = "Sno";
    for (var k = 1; k < columnNames.length; k++) {
        if (k == 0)
            continue;
        html = html + "<th>" + columnNames[k] + "</th>";
    }

    var printWindow = window.open('', 'Print Data', 'height=500, width=1200');
    printWindow.document.write('<html><head><title></title>');
    printWindow.document.write('</head><body><table border="0" style="margin-left: 150px;"><tr><td align="center" style="font-size:22px"><b><font color="Olive">GOVERNMENT OF ANDHRA PRADESH</font></b></td></tr><tr><td align="center" style="font-size:18px"><b><font color="Olive">Growth Monitoring System</font></b></td></tr><tr><td align="center" style="font-size:16px"><font color="Olive">' + ReportFor + '</font></td></tr></table><table border="0" align="center" style="margin-left:420px;"><tr style="font-size:14px"><td><font color="Olive">Report Generated Time: ' + today + "  " + nowTime + '</font></td></tr></table><br />');
    printWindow.document.write('<table id="tblPrint" border="1" style=" margin-left: 2cm; margin-right: 2cm; border-collapse: collapse;"><tr><th>' + html + '</th></tr>' + divContents + '</table></div></body>');
    $(id).trigger('reloadGrid');
    printWindow.print();
    printWindow.close();
}


/*  Control toolkit */
$.fn.extend({
    bindDatePicker: function () {

        if ($(this).hasClass("hfreeze"))
            return;
        this.datepicker({
            format: "dd-mm-yyyy"
        }).on('changeDate', function (e) {
            $(this).datepicker('hide');
        });

    },
    bindMonthPicker: function (readonly) {
        if ($(this).hasClass("hfreeze"))
            return;
        this.datepicker({
            format: "mmm-yyyy",
            viewMode: "months",
            minViewMode: "months"
        }).on('changeDate', function () {
            $(this).datepicker('hide');
        });

    },
    bindCheckbox: function () {
        this.addClass("prettyCheckable");
        if (this.data("label") == null)
            this.data("label", "");
        this.prettyCheckable();
    },
    bindRadio: function () {
        this.addClass("prettyCheckable");
        if (this.data("label") == null)
            this.data("label", "");
        this.prettyCheckable();
    },
    bindRecordCount: function () {
        this.empty();
        for (var i = 1; i <= 10; i++) {
            this.append('<option value="' + i * 10000 + '">' + i * 10000 + '</option>');
        }
    },
    clear: function () {     //  to extend to all controls including form and div
        var container = $(this);
        container.find("input[type=text],input[type=hidden],input[type=checkbox], textarea").val("");
        container.find('select').each(function () {
            $(this).find('option:first').prop('selected', 'selected');
        });
        container.find(".prettyCheckable").each(function () {
            $(this).prettyCheckable("uncheck");
        });
        container.find(".date-picker").each(function () {
            $(this).val(moment().format("DD-MM-YYYY"));
        });

    },
    bindTabs: function () {
        this.addClass("tabs");
        this.tabs();
    },
    readonly: function () {        //  to extend to all the controls including form and div
        if (this.is('input')) {
            $(this).prop("readonly", true);
        }
        if (this.is('select')) {
            //  save current value
            var val = this.val();
            //  remove any events that fire on change
            this.unbind("change");
            //reset the value when the select change
            this.change(function () {
                $(this).val(val);
            });
        }
    },
    addOptions: function (url, successFunction, appendOnly) {
        if (!this.is('select'))
            return;
        if (!(appendOnly === false))
            this.empty();
        ajaxGet(url, function (data) {
            for (var i = 0; i < data.length; i++) {
                this.append('<option value="' + data[i].Value + '">' + data[i].Text + '</option>');
            }
            if (successFunction != null)
                successFunction();
        });
    }
});

/*  Search toolkit */
function customRules(rules) {
    return "{\"CustomRules\": [" + rules.join() + "]}";
}
function rule(field, op, data) {
    return "{ \"field\": \"" + field + "\", \"op\": \"" + op + "\", \"data\": \"" + data + "\" }";
}
/*  end of Search toolkit    */
