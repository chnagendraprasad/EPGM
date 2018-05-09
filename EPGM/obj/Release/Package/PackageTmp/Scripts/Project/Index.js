
var currentPage = 1;

jQuery(document).ready(function () {

    var grid = jQuery('#Grid');
    grid.jqGrid({
        url: urlContent + 'Project/GridData',
        datatype: 'json',
        height: 340,
        autowidth: true,
        rowNum: 1000000,
        mtype: 'GET',
        hoverrows: false,
        cmTemplate: { sortable: false },
        beforeRequest: function () {},
        colNames: ['ProjectID', 'SNo', 'District Code', 'Project Code', 'Project Name', 'No.of Beneficiaries'],
        colModel: [
		        { name: 'ProjectID', index: 'ProjectID', width: 90, hidden: true },
		  		{ name: 'Sno', index: 'Sno', width: 20, align: 'center' },
		  		{ name: 'DistrictCode', index: 'DistrictCode', width: 80, align: 'center', hidden: true },
		  		{ name: 'ProjectCode', index: 'ProjectCode', width: 80, align: 'center', hidden: true },
		  		{ name: 'ProjectName', index: 'ProjectName', width: 160 },
		  		{ name: 'Cnt', index: 'Cnt', width: 110, align: 'center', formatter: showBenes }],

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

    function showSectors(cellvalue, options, rowObject) {
        debugger;
        return "<a href='" + urlContent + "Sector/Index/?distCode=" + rowObject.DistrictCode + "&projCode=" + rowObject.ProjectCode + "' style='text-decoration: underline'>" + rowObject.SectorsCount + "</a>";
    }

    callbackFunction = function (status) {
        grid.trigger('reloadGrid');
    }

    function showBenes(cellvalue, options, rowObject) {
        debugger;
        var encodedString = btoa('Total' + '|' + rowObject.DistrictCode + '|' + rowObject.ProjectCode + '|' + '' + '|' + '' + '|' + $("#BeneType").val());
        return "<a title='' href='" + urlContent + "Bene/Index/?V0hPfERpc3RyaWN0SUQ=" + encodedString + "'style='text-decoration: underline !important;color: black'>" + rowObject.Cnt + "</a>";
    }
});





