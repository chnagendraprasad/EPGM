/// <reference path="../Common/helper.js" />
jQuery(document).ready(function () {

    function GetDashStatCount() {
        ajaxGet(urlContent + 'Home/GetDashStats', function (retval) {
            //debugger;
            var data = retval;
            $("#totben").html(data.BeneCount);
            $("#Total").html(data.Count);
            $("#NOR").html(data.NormalCount);
            $("#MUW").html(data.ModerateCount);
            $("#SUW").html(data.SevereCount);
            $("#norpe").html(data.NorPer + '%');
            $("#modpe").html(data.ModPer + '%');
            $("#sevpe").html(data.SevPer + '%');
            $("#abspe").html(data.AbsPer);
            dtnor = data.NormalCount;
            dtmod = data.ModerateCount;
            dtsev = data.SevereCount;
            dtabs = data.AbsentCount;
        });
        //$("#Grid").load();
        $("#Grid").jqGrid("setGridParam", { datatype: "json" })
             .trigger("reloadGrid", [{ current: true }]);
    }

    setInterval(GetDashStatCount, 30000);

    GeneratePieChart("#pieChartWA", dtnor, dtmod, dtsev);
    GeneratePieChart("#pieChartHW", hwnor, hwmod, hwsev);
    GeneratePieChart("#pieChartHA", hanor, hamod, hasev);

    var grid = jQuery('#Grid');
    grid.jqGrid({
        url: urlContent + 'Home/GetDistrictDetails',
        datatype: 'json',
        height: 353,
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
        beforeRequest: function () { },
        colNames: ['DistrictID', 'S.No', 'District Code', 'District Name', 'Total Beneficiaries', 'No.of Children Weighed', 'Attendance Count', 'Normal', 'Moderate', 'Severe', 'Normal', 'MAM', 'SAM', 'Normal', 'Moderate', 'Severe', 'Absent'],
        colModel: [
		        { name: 'DistrictID', index: 'DistrictID', width: 90, hidden: true },
                { name: 'Row', index: 'Row', width: 50, align: 'center', sortable: false, hidden: true },
		  		{ name: 'DistrictCode', index: 'DistrictCode', width: 160, hidden: true },
		  		{ name: 'DistrictName', index: 'DistrictName', width: 140, formatter: showDistricts, sortable: true },
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
        },
        loadonce: true
    });

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
        return "<a href='" + urlContent + "Home/Report/?distCode=" + rowObject.DistrictCode + "'style='text-decoration: underline;color:red'>" + rowObject.DistrictName + "</a>";
    }

    $(".subBut").click(function () {
        debugger;
        var whoType = btoa(this.id);
        window.location.href = urlContent + "District/Index/?dW5kZWZpbmVk=" + whoType;
    });

    $(window).on("resize", function () {
        var $grid = $("#list"),
            newWidth = $grid.closest(".ui-jqgrid").parent().width();
        $grid.jqGrid("setGridWidth", newWidth, true);
    });

});



//ajaxGetJSON(urlContent + "Home/GetDistrictDetails", function (retval) {
//    debugger;
//    var sel = "";
//    var totCount = 0;
//    for (var i = 0; i < retval.length; i++) {
//        sel = sel + "<tr>";
//        var dt = (retval[i].Row != null) ? retval[i].Row : '';
//        var dttot = "" + retval[i].DistrictID + "_TOT";
//        $(document).on('click', '#' + dttot, function () {
//            debugger;
//            //alert('#' + dttot);
//        });
//        var dtnor = "" + retval[i].DistrictID + "_NOR";
//        $(document).on('click', '#' + dtnor, function () {
//            debugger;
//            //alert('#' + dtnor);
//        });
//        var dtmuw = "" + retval[i].DistrictID + "_MUW";
//        $(document).on('click', '#' + dtmuw, function () {
//            debugger;
//            //alert('#' + dtmuw);
//        });
//        var dtsuw = "" + retval[i].DistrictID + "_SUW";
//        $(document).on('click', '#' + dtsuw, function () {
//            debugger;
//            //alert('#' + dtsuw);
//        });
//        var dtabs = "" + retval[i].DistrictID + "_ABS";
//        $(document).on('click', '#' + dtabs, function () {
//            debugger;
//            //alert('#' + dtabs);
//        });
//        sel = sel.concat('<th scope="row">' + dt + '</th>');
//        sel = sel.concat('<td style="text-align: left !important" id="' + retval[i].DistrictID + '">' + retval[i].DistrictName + '</td>');
//        sel = sel.concat('<td>' + retval[i].TotalCount + '</td>');
//        sel = sel.concat('<td><a href="#" id="' + dttot + '">' + retval[i].Total + '</a></td>');
//        sel = sel.concat('<td><a href="#" id="' + dtnor + '">' + retval[i].Normal + '</a></td>');
//        sel = sel.concat('<td><a href="#" id="' + dtmuw + '">' + retval[i].Moderate + '</a></td>');
//        sel = sel.concat('<td><a href="#" id="' + dtsuw + '">' + retval[i].Severe + '</a></td>');
//        sel = sel.concat('<td><a href="#" id="' + dtabs + '">' + retval[i].Absent + '</a></td>');
//        sel = sel.concat("</tr>");
//    }
//    totCount += parseInt(retval[(retval.length - 1)].TotalCount);
//    $("#distLevData").html(sel);
//    $("#All").prepend(totCount);
//});
