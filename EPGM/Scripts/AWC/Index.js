/// <reference path="../../Common/helper.js" />

var currentPage = 1;

jQuery(document).ready(function () {

    var grid = jQuery('#Grid');
    grid.jqGrid({
        url: urlContent + 'AWC/GridData',
        datatype: 'json',
        height: 350,
        autowidth: true,
        rowNum: 1000000,
        hoverrows: false,
        mtype: 'GET',
        cmTemplate: { sortable: false },
        beforeRequest: function () {
        },
        colNames: ['AWCID', 'Sno', 'District Code', 'Project Code', 'Sector Code', 'AWC Code', 'AWC Name', 'No.of Beneficiaries'],
        colModel: [
		        { name: 'AWCID', index: 'AWCID', width: 90, hidden: true },
		  		{ name: 'Sno', index: 'Sno', width: 50, align: 'center' },
		  		{ name: 'DistrictCode', index: 'DistrictCode', width: 60, align: 'center', hidden: true },
		  		{ name: 'ProjectCode', index: 'ProjectCode', width: 60, align: 'center', hidden: true },
		  		{ name: 'SectorCode', index: 'SectorCode', width: 60, align: 'center', hidden: true },
		  		{ name: 'AWCCode', index: 'AWCCode', width: 60, align: 'center', hidden: true },
		  		{ name: 'AWCName', index: 'AWCName', width: 150 },
		  		{ name: 'Cnt', index: 'Cnt', width: 150, align: 'center', formatter: showBenes }],

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

    function showImage(cellvalue, options, rowObject) {
        debugger;
        //alert(rowObject.ImageUploaded);
        if (rowObject.ImageUploaded == true)
            return "<a href='" + urlContent + "Geo/Index/?awcCode=" + rowObject.AWCCode + "' style='text-decoration: underline'><img src='../../Content/images/Avail.png'></img></a>";
        else
            return "<a href='" + urlContent + "Geo/Index/?awcCode=" + rowObject.AWCCode + "' style='text-decoration: underline'><img src='../../Content/images/NotAvail.png'></img></a>";
    }

    callbackFunction = function (status) {
        grid.trigger('reloadGrid');
    }

    function showBenes(cellvalue, options, rowObject) {
        debugger;
        var encodedString = btoa('Total' + '|' + rowObject.DistrictCode + '|' + rowObject.ProjectCode + '|' + rowObject.SectorCode + '|' + rowObject.AWCCode + '|' + $("#BeneType").val());
        return "<a title='' href='" + urlContent + "Bene/Index/?V0hPfERpc3RyaWN0SUQ=" + encodedString + "'style='text-decoration: underline !important;color: black'>" + rowObject.Cnt + "</a>";
    }
});
































//$("#Add").click(function () {
//    openPopupWindow(urlContent + "Admin/AWC/Edit", "#Popup", callbackFunction);
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
//    openPopupWindow(urlContent + "Admin/AWC/Edit/?ID=" + rowData.AWCID, "#Popup", callbackFunction);
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
//    ajaxPostData("Admin/AWC/ToggleActive/?ID=" + rowData.AWCID, callbackFunction);
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
//    ajaxPostData("Admin/AWC/Delete/?ID=" + rowData.AWCID, callbackFunction);
//});