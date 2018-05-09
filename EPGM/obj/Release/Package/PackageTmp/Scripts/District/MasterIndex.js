/// <reference path="../Common/_references.js" />
/// <reference path="../Common/helper.js" />

var currentPage = 1;

jQuery(document).ready(function () {
    var grid = jQuery('#Grid');
    grid.jqGrid({
        url: urlContent + 'District/MasterGridData',
        datatype: 'json',
        height: 350,
        autowidth: true,
        rowNum: 1000000,
        mtype: 'GET',
        beforeRequest: function () { },
        colNames: ['#', 'ID', 'District Code', 'District Name', 'PD Name', 'PD Mobile', 'PD Email', 'PD Address'],
        colModel: [
		  		{ name: 'Sno', index: 'Sno', width: 30, align: 'center' },
		  		{ name: 'DistrictID', index: 'Name', width: 180, hidden: true },
		  		{ name: 'DistrictCode', index: 'Name', width: 180, hidden: true },
		  		{ name: 'DistrictName', index: 'Name', width: 180 },
		  		{ name: 'PDName', index: 'PDName', width: 180 },
		  		{ name: 'PDMobile', index: 'PDMobile', width: 180 },
		  		{ name: 'PDEmail', index: 'PDEmail', width: 180 },
		  		{ name: 'PDAddress', index: 'PDAddress', width: 180 }],

        loadError: function (xhr, status, error) {
            var trf = $("#Grid tbody:first tr:first")[0];
            $("#Grid tbody:first").empty().append(trf);
        },

        loadComplete: function (data) { }
    });

    function showBenes(cellvalue, options, rowObject) {
        debugger;
        var encodedString = btoa(rowObject.WHO + "|" + rowObject.DistrictID);
        return "<a href='" + urlContent + "Bene/Index/?V0hPfERpc3RyaWN0SUQ=" + encodedString + "'style='text-decoration: underline'>" + rowObject.Cnt + "</a>";
    }

    callbackFunction = function (status) {
        grid.trigger('reloadGrid');
    }
});
