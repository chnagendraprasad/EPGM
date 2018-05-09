jQuery(document).ready(function () {
    debugger;
    var today = new Date();
    var curmon = today.getMonth() + 1;
    $('#ddlMonth').prop('selectedIndex', curmon);

    $('#StateID').val("52").attr("selected", "selected");
    BindData();
    BindGrid();
    $('#Grid').trigger('reloadGrid');
    //function GetDashStatCount() {
    //    ajaxGet(urlContent + 'Home/GetDashStats?StateCode=' + $("#StateID option:selected").val(), function (retval) {
    //        //debugger;
    //        var data = retval;
    //        $("#totben").html(data.BeneCount);
    //        $("#Total").html(data.Count);
    //        $("#NOR").html(data.NormalCount);
    //        $("#MUW").html(data.ModerateCount);
    //        $("#SUW").html(data.SevereCount);
    //        $("#norpe").html(data.NorPer + '%');
    //        $("#modpe").html(data.ModPer + '%');
    //        $("#sevpe").html(data.SevPer + '%');
    //        $("#abspe").html(data.AbsPer);
    //        dtnor = data.NormalCount;
    //        dtmod = data.ModerateCount;
    //        dtsev = data.SevereCount;
    //        dtabs = data.AbsentCount;
    //    });
    //    //$("#Grid").load();
    //    $("#Grid").jqGrid("setGridParam", { datatype: "json" })
    //         .trigger("reloadGrid", [{ current: true }]);
    //}

    //// setInterval(GetDashStatCount, 30000);

    //GeneratePieChart("#pieChartWA", dtnor, dtmod, dtsev);
    //GeneratePieChart("#pieChartHW", hwnor, hwmod, hwsev);
    //GeneratePieChart("#pieChartHA", hanor, hamod, hasev);


    $("#btnSubmit").click(function () {
    });

    $(".subBut").click(function () {
        debugger;
        var whoType = btoa(this.id);
        window.location.href = urlContent + "District/Index/?dW5kZWZpbmVk=" + whoType + "&StateCode=" + $("#StateID option:selected").val() + "&BeneType=" + $("#BeneType option:selected").val() + "&Month=" + $("#ddlMonth option:selected").val() + "&Year=" + $("#txtYear").val() + '&CenterType=' + $("#CenterType").val();
    });
});

function BindDashBoard() {
    var date = new Date();
    if ($('#ddlMonth').val() == "0" || $('#ddlMonth').val() == "" || $('#ddlMonth').val() == null) {
        alert("Please Select Month");
        $('#ddlMonth').focus();
        return false;
    }
    else if (($('#ddlMonth').val() > date.getMonth() + 1) && ($("#txtYear").val() >= date.getFullYear())) {
        alert("Month Should not be greater than the Current Month");
        $('#ddlMonth').focus();
        return false;
    }
    else if ($("#txtYear").val() == "" || $("#txtYear").val() == null) {
        alert("Please Enter Year");
        $('#txtYear').focus();
        return false;
    }
    else if ($("#txtYear").val().length != "4") {
        alert("Please Enter Correct Year");
        $("#txtYear").focus();
        return false;
    }
    else if ($("#txtYear").val() > date.getFullYear()) {
        alert("Year Should not be greater than the Current Year");
        $("#txtYear").focus();
        return false;
    }
    else {
        BindData();
        BindGrid();
        $('#Grid').trigger('reloadGrid');
    }
}

function DropDownChange() {
    $("#name1").text($("#StateID option:selected").text());
    $('#BeneType').val("1").attr("selected", "selected");
    BindData();
    BindGrid();
    $('#Grid').trigger('reloadGrid');
}

function BeneTypeChange() {
    BindData();
    BindGrid();
    $('#Grid').trigger('reloadGrid');

    if ($("#BeneType option:selected").val() == "2" || $("#BeneType option:selected").val() == "3") {
        $("#chartdata").css("display", "none");
    } else {

        $("#chartdata").css("display", "block");
    }
}

function BindGrid() {
    var grid = jQuery('#Grid');
    grid.jqGrid({
        url: urlContent + 'Home/GetAdminDistrictDetails',
        datatype: 'json',
        height: 330,
        autowidth: true,
        width: 'auto',
        gridview: true,
        mtype: 'GET',
        hoverrows: false,
        scroll: true,
        scrollOffset: 0,
        footerrow: true,
        userDataOnFooter: true,
        shrinkToFit: true,
        rownumbers: true,
        beforeRequest: function () {
            var postData = grid.jqGrid('getGridParam', "postData");
            $.extend(postData, { "StateCode": $("#StateID option:selected").val(), "BeneType": $("#BeneType option:selected").val(), "Month": $("#ddlMonth option:selected").val(), "Year": $("#txtYear").val(), "CenterType": $("#CenterType").val() });
        },
        colNames: ['DistrictID', 'S.No', 'District Code', 'District Name', 'Total Beneficiaries', 'No.of Children Weighed', 'Attendance Count', 'Normal', 'Moderate', 'Severe', 'Normal', 'MAM', 'SAM', 'Normal', 'Moderate', 'Severe', 'Absent'],
        colModel: [
		        { name: 'DistrictID', index: 'DistrictID', width: 90, hidden: true },
                { name: 'Row', index: 'Row', width: 50, align: 'center', sortable: false, hidden: true },
		  		{ name: 'DistrictCode', index: 'DistrictCode', width: 160, hidden: true },
		  		{ name: 'DistrictName', index: 'DistrictName', width: 140, formatter: showDistricts, sorttype: 'text', sortable: true },
		  		{ name: 'BeneCount', index: 'BeneCount', width: 130, align: 'center', sortable: true, sorttype: 'number' },
                { name: 'Total', index: 'Total', width: 150, align: 'center', sortable: true, sorttype: 'number' },
                { name: 'TotalAttendance', index: 'TotalAttendance', width: 120, align: 'center', sortable: true, sorttype: 'number' },
		  		{ name: 'Normal', index: 'Normal', width: 100, align: 'center', sortable: true, sorttype: 'number' },
		  		{ name: 'Moderate', index: 'Moderate', width: 110, align: 'center', sortable: true, sorttype: 'number' },
		  		{ name: 'Severe', index: 'Severe', width: 90, align: 'center', sortable: true, sorttype: 'number' },

                { name: 'HWNormal', index: 'HWNormal', width: 100, align: 'center', sortable: true, sorttype: 'number' },
		  		{ name: 'HWModerate', index: 'HWModerate', width: 110, align: 'center', sortable: true, sorttype: 'number' },
		  		{ name: 'HWSevere', index: 'HWSevere', width: 90, align: 'center', sortable: true, sorttype: 'number' },
                { name: 'HANormal', index: 'HANormal', width: 100, align: 'center', sortable: true, sorttype: 'number' },
		  		{ name: 'HAModerate', index: 'HAModerate', width: 110, align: 'center', sortable: true, sorttype: 'number' },
		  		{ name: 'HASevere', index: 'HASevere', width: 90, align: 'center', sortable: true, sorttype: 'number' },

		  		{ name: 'Absent', index: 'Absent', width: 100, align: 'center', sortable: true, sorttype: 'number' }],
        loadError: function (xhr, status, error) {
            var trf = $("#Grid tbody:first tr:first")[0];
            $("#Grid tbody:first").empty().append(trf);
        },
        loadComplete: function () {
            debugger;
            var $grid = $("#Grid");
            $grid.jqGrid('footerData', 'set', { DistrictName: 'Total:' });

            var BeneCountSum = $grid.jqGrid('getCol', 'BeneCount', false, 'sum');
            $grid.jqGrid('footerData', 'set', { BeneCount: BeneCountSum });

            var TotalSum = $grid.jqGrid('getCol', 'Total', false, 'sum');
            $grid.jqGrid('footerData', 'set', { Total: TotalSum });

            var TotalAttendance = $grid.jqGrid('getCol', 'TotalAttendance', false, 'sum');
            $grid.jqGrid('footerData', 'set', { TotalAttendance: TotalAttendance });

            var NormalSum = $grid.jqGrid('getCol', 'Normal', false, 'sum');
            $grid.jqGrid('footerData', 'set', { Normal: NormalSum });

            var ModerateSum = $grid.jqGrid('getCol', 'Moderate', false, 'sum');
            $grid.jqGrid('footerData', 'set', { Moderate: ModerateSum });

            var SevereSum = $grid.jqGrid('getCol', 'Severe', false, 'sum');
            $grid.jqGrid('footerData', 'set', { Severe: SevereSum });

            var AbsentSum = $grid.jqGrid('getCol', 'Absent', false, 'sum');
            $grid.jqGrid('footerData', 'set', { Absent: AbsentSum });

            var HWNormalSum = $grid.jqGrid('getCol', 'HWNormal', false, 'sum');
            $grid.jqGrid('footerData', 'set', { HWNormal: HWNormalSum });

            var HWModerateSum = $grid.jqGrid('getCol', 'HWModerate', false, 'sum');
            $grid.jqGrid('footerData', 'set', { HWModerate: HWModerateSum });

            var HWSevereSum = $grid.jqGrid('getCol', 'HWSevere', false, 'sum');
            $grid.jqGrid('footerData', 'set', { HWSevere: HWSevereSum });

            var HANormalSum = $grid.jqGrid('getCol', 'HANormal', false, 'sum');
            $grid.jqGrid('footerData', 'set', { HANormal: HANormalSum });

            var HAModerateSum = $grid.jqGrid('getCol', 'HAModerate', false, 'sum');
            $grid.jqGrid('footerData', 'set', { HAModerate: HAModerateSum });

            var HASevereSum = $grid.jqGrid('getCol', 'HASevere', false, 'sum');
            $grid.jqGrid('footerData', 'set', { HASevere: HASevereSum });

        }
        , loadonce: true,
        navOptions: { reloadGridOptions: { fromServer: true } }
    });


    $("#Grid").setGridParam({ datatype: 'json', page: 1 }).trigger('reloadGrid');

    jQuery('#Grid').jqGrid('destroyGroupHeader');

    jQuery("#Grid").jqGrid('setGroupHeaders', {
        useColSpanStyle: true,
        groupHeaders: [
            { startColumnName: 'Normal', numberOfColumns: 3, titleText: '<em>Weight/Age Criteria</em>', align: 'center' },
            { startColumnName: 'HWNormal', numberOfColumns: 3, titleText: '<em>Height/Weight Wasted</em>', align: 'center' },
            { startColumnName: 'HANormal', numberOfColumns: 3, titleText: '<em>Height/Age Stunted</em>', align: 'center' },
        ]
    });
    $("#jqgh_Grid_rn").text("S.N");
    $("#Grid_Normal").addClass('GNor');
    $("#Grid_Moderate").addClass('GMod');
    $("#Grid_Severe").addClass('GSev');
    $("#Grid_Absent").addClass('GAbs');

    $("#Grid_HWNormal").addClass('GNor');
    $("#Grid_HWModerate").addClass('GMod');
    $("#Grid_HWSevere").addClass('GSev');

    $("#Grid_HANormal").addClass('GNor');
    $("#Grid_HAModerate").addClass('GMod');
    $("#Grid_HASevere").addClass('GSev');

    callbackFunction = function (status) {
        grid.trigger('reloadGrid');
    }

}

function BindData() {
    debugger;
    
    var StateCode = $("#StateID option:selected").val();
    $.post("/Home/GetDashStats?StateCode=" + $("#StateID option:selected").val() + "&BeneType=" + $("#BeneType option:selected").val() + "&Month=" + $("#ddlMonth option:selected").val() + "&Year=" + $("#txtYear").val() + "&CenterType=" + $("#CenterType").val(), "", function (result) {

        $("#totben").text(result.BeneCount);
        $("#PW").text(result.PregnantWomen);
        $("#LW").text(result.LacatingWomen);
        $("#child").text(result.TotalChildren);
        $("#Child0-3").text(result.Child0to3);
        $("#Child3-6").text(result.Child3to6);

        $("#Total").text(result.Count);
        $("#NOR").text(result.WANormalCount);
        $("#MUW").text(result.WAModerateCount);
        $("#SUW").text(result.WASevereCount);
        $("#Absben").text(result.AbsentCount);

        $("#WANorPer").text(result.WANorPer + " %");
        $("#WAModPer").text(result.WAModPer + " %");
        $("#WASevPer").text(result.WASevPer + " %");

        $("#HWNorPer").text(result.HWNorPer + " %");
        $("#HWModPer").text(result.HWModPer + " %");
        $("#HWSevPer").text(result.HWSevPer + " %");

        $("#HANorPer").text(result.HANorPer + " %");
        $("#HAModPer").text(result.HAModPer + " %");
        $("#HASevPer").text(result.HASevPer + " %");

        var dtnor = result.Normal
        var dtmod = result.Moderate
        var dtsev = result.Severe
        var dtabs = result.Absent
        var hwnor = result.HWNor
        var hwmod = result.HWMod
        var hwsev = result.HWSev
        var hanor = result.HANor
        var hamod = result.HAMod
        var hasev = result.HASev
        if ($("#BeneType option:selected").val() == "1") {
            GeneratePieChart("#pieChartWA", dtnor, dtmod, dtsev);
            GeneratePieChart("#pieChartHW", hwnor, hwmod, hwsev);
            GeneratePieChart("#pieChartHA", hanor, hamod, hasev);
        }
    });
}

function showTotal(cellvalue, options, rowObject) {
    var encodedString = btoa("Total|" + rowObject.DistrictCode);
    return "<a href='" + urlContent + "Bene/Index/?V0hPfERpc3RyaWN0SUQ=" + encodedString + "'style='text-decoration: underline'>" + rowObject.Total + "</a>";
}

function showNormal(cellvalue, options, rowObject) {
    var encodedString = btoa("NOR|" + rowObject.DistrictCode);
    return "<a href='" + urlContent + "Bene/Index/?V0hPfERpc3RyaWN0SUQ=" + encodedString + "'style='text-decoration: underline'>" + rowObject.Normal + "</a>";
}

function showModerate(cellvalue, options, rowObject) {
    var encodedString = btoa("MUW|" + rowObject.DistrictCode);
    return "<a href='" + urlContent + "Bene/Index/?V0hPfERpc3RyaWN0SUQ=" + encodedString + "'style='text-decoration: underline'>" + rowObject.Moderate + "</a>";
}

function showSevere(cellvalue, options, rowObject) {
    var encodedString = btoa("SUW|" + rowObject.DistrictCode);
    return "<a href='" + urlContent + "Bene/Index/?V0hPfERpc3RyaWN0SUQ=" + encodedString + "'style='text-decoration: underline'>" + rowObject.Severe + "</a>";
}

function showAbsent(cellvalue, options, rowObject) {
    var encodedString = btoa("ABS|" + rowObject.DistrictCode);
    return "<a href='" + urlContent + "Bene/Index/?V0hPfERpc3RyaWN0SUQ=" + encodedString + "'style='text-decoration: underline'>" + rowObject.Absent + "</a>";
}

function showDistricts(cellvalue, options, rowObject) {
    var encodedString = btoa(rowObject.DistrictCode);
    return "<a href='" + urlContent + "Home/ReportAdmin/?statecode=" + $("#StateID option:selected").val() + "&distCode=" + rowObject.DistrictCode + "&CenterType=" + $("#CenterType").val() + "'style='text-decoration: underline;color:red'>" + rowObject.DistrictName + "</a>";
}

function Checkyear() {
    var date = new Date();
    if ($('#ddlMonth').val() == "0" || $('#ddlMonth').val() == "" || $('#ddlMonth').val() == null) {
        alert("Please Select Month");
        $('#ddlMonth').focus();
        return false;
    }
    else if (($('#ddlMonth').val() > date.getMonth() + 1) && ($("#txtYear").val() >= date.getFullYear())) {
        alert("Month Should not be greater than the Current Month");
        $('#ddlMonth').focus();
        return false;
    }
    else if ($("#txtYear").val() == "" || $("#txtYear").val() == null) {
        alert("Please Enter Year");
        $('#txtYear').focus();
        return false;
    }
    else if ($("#txtYear").val().length != "4") {
        alert("Please Enter Correct Year");
        $("#txtYear").focus();
        return false;
    }
    else if ($("#txtYear").val() > date.getFullYear()) {
        alert("Year Should not be greater than the Current Year");
        $("#txtYear").focus();
        return false;
    }
    else {
        DropDownChange();
    }
}

function ChangeCenterType()
{
   
        var date = new Date();
        if ($('#ddlMonth').val() == "0" || $('#ddlMonth').val() == "" || $('#ddlMonth').val() == null) {
            alert("Please Select Month");
            $('#ddlMonth').focus();
            return false;
        }
        else if (($('#ddlMonth').val() > date.getMonth() + 1) && ($("#txtYear").val() >= date.getFullYear())) {
            alert("Month Should not be greater than the Current Month");
            $('#ddlMonth').focus();
            return false;
        }
        else if ($("#txtYear").val() == "" || $("#txtYear").val() == null) {
            alert("Please Enter Year");
            $('#txtYear').focus();
            return false;
        }
        else if ($("#txtYear").val().length != "4") {
            alert("Please Enter Correct Year");
            $("#txtYear").focus();
            return false;
        }
        else if ($("#txtYear").val() > date.getFullYear()) {
            alert("Year Should not be greater than the Current Year");
            $("#txtYear").focus();
            return false;
        }
        else {
            DropDownChange();
        }
  
}

$(window).on("resize", function () {
    var $grid = $("#list"),
        newWidth = $grid.closest(".ui-jqgrid").parent().width();
    $grid.jqGrid("setGridWidth", newWidth, true);
});