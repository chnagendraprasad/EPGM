/// <reference path="../../Common/helper.js" />

var currentPage = 1;

jQuery(document).ready(function () {
    var grid = jQuery('#Grid');
    grid.jqGrid({
        url: urlContent + 'Sector/GridData',
        datatype: 'json',
        height: 350,
        autowidth: true,
        rowNum: 1000000,
        mtype: 'GET',
        hoverrows: false,
        cmTemplate: { sortable: false },
        beforeRequest: function () {
        },
        colNames: ['SectorID', 'SNo', 'Sector Code', 'Sector Name', 'No.of Beneficiaries', 'Project Name', 'District Name'],
        colModel: [
		        { name: 'SectorID', index: 'SectorID', width: 90, hidden: true },
		        { name: 'Sno', index: 'Sno', width: 15, align: 'center' },
		  		{ name: 'SectorCode', index: 'SectorCode', width: 110, align: 'center', hidden: true },
		  		{ name: 'SectorName', index: 'SectorName', width: 150 },
		  		{ name: 'Cnt', index: 'Cnt', width: 150, align: 'center', formatter: showBenes },
		  		{ name: 'ProjectCode', index: 'ProjectCode', width: 110, hidden: true },
		  		{ name: 'DistrictCode', index: 'DistrictCode', width: 110, hidden: true }],

        loadError: function (xhr, status, error) {
            var trf = $("#Grid tbody:first tr:first")[0];
            $("#Grid tbody:first").empty().append(trf);
        },

        loadComplete: function (data) {
            var count = grid.getRowData().length
            if (count <= 0) {
                $("#myModal").modal('show');
                $('#Grid').jqGrid("clearGridData");

            }
        }
    });

    function showAWCs(cellvalue, options, rowObject) {
        debugger;
        return "<a href='" + urlContent + "AWC/Index/?secCode=" + rowObject.SectorCode + "'style='text-decoration: underline'>" + rowObject.AWCCount + "</a>";
    }

    callbackFunction = function (status) {
        grid.trigger('reloadGrid');
    }

    function showBenes(cellvalue, options, rowObject) {
        debugger;
        var encodedString = btoa('Total' + '|' + rowObject.DistrictCode + '|' + rowObject.ProjectCode + '|' + rowObject.SectorCode + '|' + '');
        return "<a title='' href='" + urlContent + "Bene/Index/?V0hPfERpc3RyaWN0SUQ=" + encodedString + "'style='text-decoration: underline !important;color: black'>" + rowObject.Cnt + "</a>";
    }
});




















//$("#Add").click(function () {
//    openPopupWindow(urlContent + "Admin/Sector/Edit", "#Popup", callbackFunction);
//});

//$("#Edit").click(function () {
//    debugger;
//    var selRowIds = grid.jqGrid('getGridParam', 'selarrrow');
//    if (selRowIds.length != 1) {
//        hAlert("Please select a row to edit");
//        return false;
//    }
//    var rowId = grid.jqGrid('getGridParam', 'selrow');
//    var rowData = grid.jqGrid('getRowData', rowId);
//    openPopupWindow(urlContent + "Admin/Sector/Edit/?ID=" + rowData.SectorID, "#Popup", callbackFunction);
//});

//$("#ToggleActive").click(function () {
//    var selRowIds = grid.jqGrid('getGridParam', 'selarrrow');
//    if (selRowIds.length != 1) {
//        hAlert("Please select a row to toggle Active");
//        return false;
//    }
//    var response = hConfirm("Do you want to toggle active state of the selected record?");
//    if (response == false)
//        return;
//    var rowId = grid.jqGrid('getGridParam', 'selrow');
//    var rowData = grid.jqGrid('getRowData', rowId);
//    ajaxPostData("Admin/Sector/ToggleActive/?ID=" + rowData.SectorID, callbackFunction);
//});

//$("#Delete").click(function () {
//    var selRowIds = grid.jqGrid('getGridParam', 'selarrrow');
//    if (selRowIds.length != 1) {
//        hAlert("Please select a row to delete");
//        return false;
//    }
//    var response = hConfirm("Do you want to delete the selected record?");
//    if (response == false)
//        return;

//    var rowId = grid.jqGrid('getGridParam', 'selrow');
//    var rowData = grid.jqGrid('getRowData', rowId);
//    ajaxPostData("Admin/Sector/Delete/?ID=" + rowData.SectorID, callbackFunction);
//});
