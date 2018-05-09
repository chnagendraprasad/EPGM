/// <reference path="../Common/helper.js" />
/// <reference path="../Common/_references.js" />

var currentPage = 1;

jQuery(document).ready(function () {

    $("#rows").bindRecordCount();

    $("#rows").change(function () { callbackFunction(); })

    var grid = jQuery('#Grid');
    grid.jqGrid({
        url: urlContent + 'Bene/GridData',
        datatype: 'json',
        height: 350,
        autowidth: true,
        rowNum: 1000000,
        mtype: 'GET',
        shrinkToFit: true,
        gridview: true,
        hoverrows: false,
        scrollOffset: 0,
        cmTemplate: { sortable: false },
        beforeRequest: function () {
        },
        colNames: ['SNo', 'Beneficiary Code', 'Name', 'Surname', 'Gender', 'DOB', 'Mobile Number', 'Grade', 'Wt.Add'],
        colModel: [
		  		{ name: 'Sno', index: 'Sno', width: 50, align: 'center' },
		  		{ name: 'BeneCode', index: 'BeneCode', width: 150 },
		  		{ name: 'BeneName', index: 'BeneName', width: 240 },
		  		{ name: 'BeneSurname', index: 'BeneSurname', width: 240 },
		  		{ name: 'Gender', index: 'Gender', width: 120 },
		  		{ name: 'DOB', index: 'DOB', width: 120, align: 'right' },
		  		{ name: 'MobileNumber', index: 'MobileNumber', width: 140, hidden: true },
		  		{ name: 'GradeName', index: 'GradeName', width: 190 },
		  		{ name: 'WeightToAdd', index: 'WeightToAdd', width: 120, align: 'right' }],

        loadError: function (xhr, status, error) {
            var trf = $("#Grid tbody:first tr:first")[0];
            $("#Grid tbody:first").empty().append(trf);
        },
        loadComplete: function () {
            var count = grid.getRowData().length
            if (count <= 0) {
                $("#myModal1").modal('show');
                $('#Grid').jqGrid("clearGridData");

            }
        },
        loadonce: true
    });

    function showWHO(cellvalue, options, rowObject) {
        debugger;
        if (rowObject.WHO == "NOR")
            return "Normal";
        if (rowObject.WHO == "MUW")
            return "Moderate";
        if (rowObject.WHO == "SUW")
            return "Severe";
        if (rowObject.WHO == "ABS")
            return "Absent";
    }

    function getDetails(cellvalue, options, rowObject) {
        debugger;
        var encodedString = btoa(rowObject.BeneCode);
        return "<a href='" + urlContent + "Bene/BeneDetails/?beneCode=" + encodedString + "'style='text-decoration: underline'>" + rowObject.Name + "</a>";
    }

    callbackFunction = function (status) {
        grid.trigger('reloadGrid');
    }
});
