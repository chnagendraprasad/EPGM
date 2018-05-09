/// <reference path="../Common/_references.js" />
/// <reference path="../Common/helper.js" />

var currentPage = 1;

jQuery(document).ready(function () {
    var grid = jQuery('#Grid');
    grid.jqGrid({
        url: urlContent + 'District/GridData',
        datatype: 'json',
        height: 340,
        autowidth: true,
        rowNum: 1000000,
        mtype: 'GET',
        hoverrows: false,
        cmTemplate: { sortable: false },
        beforeRequest: function () { },
        colNames: ['DistrictID', 'Sno', 'District Name', 'District Code', 'No.of Beneficiaries'],
        colModel: [
		        { name: 'DistrictID', index: 'DistrictID', width: 90, hidden: true },
                { name: 'Sno', index: 'Sno', width: 15, align: 'center' },
		  		{ name: 'DistrictName', index: 'DistrictName', width: 140 },
		  		{ name: 'DistrictCode', index: 'DistrictCode', width: 140, hidden: true },
                { name: 'Cnt', index: 'Cnt', width: 110, align: 'center', formatter: showBenes }],

        loadError: function (xhr, status, error) {
            var trf = $("#Grid tbody:first tr:first")[0];
            $("#Grid tbody:first").empty().append(trf);
        },
        loadComplete: function (data) {
            debugger;
            var count = grid.getRowData().length
            if (count <= 0) {
                $("#myModal").modal('show');
                $('#Grid').jqGrid("clearGridData");
            }
        }
    });

    function showBenes(cellvalue, options, rowObject) {
        debugger; 
        var encodedString = btoa('Total' + '|' + rowObject.DistrictCode + '|' + '' + '|' + '' + '|' + '' + '|' + $("#BeneType").val());
        return "<a title='' href='" + urlContent + "Bene/Index/?V0hPfERpc3RyaWN0SUQ=" + encodedString + " 'style='text-decoration:underline !important;color: black''>" + rowObject.Cnt + "</a>";
    }

    callbackFunction = function (status) {
        grid.trigger('reloadGrid');
    }
});

