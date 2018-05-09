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
        rowNum: -1,
        mtype: 'GET',
        shrinkToFit: false,
        gridview: true,
        beforeRequest: function () {
            //var postData = grid.jqGrid('getGridParam', "postData");
            //postData.page = currentPage;
            //postData.rows = $("#rows").val();
            //var AndRules = Array();
            //var OrRules = Array();
            //$.extend(postData, { "AndRule": customRules(AndRules), "OrRule": customRules(OrRules) });
        },
        colNames: ['#', 'BeneCode', 'Name', 'Surname', 'Gender', 'DOB', 'Mobile Number'
            //, 'Weight', 'Height', 'BMI'
            , 'Grade', 'Wt.Add'],
        colModel: [
		  		{ name: 'Sno', index: 'Sno', width: 50, align: 'center' },
		  		{ name: 'BeneCode', index: 'BeneCode', width: 240 },
		  		{ name: 'BeneName', index: 'BeneName', width: 240 },
		  		{ name: 'BeneSurname', index: 'BeneSurname', width: 240 },
		  		{ name: 'Gender', index: 'Gender', width: 140 },
		  		{ name: 'DOB', index: 'DOB', width: 140, align: 'right', formatter: "date", formatoptions: { srcformat: "ISO8601Long", newformat: "d-m-Y" } },
		  		{ name: 'MobileNumber', index: 'MobileNumber', width: 140 },
		  		//{ name: 'LatestWeight', index: 'LatestWeight', width: 140, align: 'right' },
		  		//{ name: 'LatestHeight', index: 'LatestHeight', width: 140, align: 'right' },
		  		//{ name: 'LatestBMI', index: 'LatestBMI', width: 140, align: 'right' },
		  		{ name: 'GradeName', index: 'GradeName', width: 140 },
		  		{ name: 'WeightToAdd', index: 'WeightToAdd', width: 140, align: 'right' }],

        loadError: function (xhr, status, error) {
            var trf = $("#Grid tbody:first tr:first")[0];
            $("#Grid tbody:first").empty().append(trf);
        },
        loadonce:true,
        loadComplete: function (data) {
            //$('#paginationholder').html('');
            //$('#paginationholder').html('<ul id="pagination" class="pagination-sm"></ul>');
            //$('#pagination').twbsPagination({
            //    startPage: data.page,
            //    first: false,
            //    last: false,
            //    totalPages: data.total,
            //    visiblePages: 5,
            //    onPageClick: function (event, page) {
            //        currentPage = page;
            //        //callbackFunction();
            //    }
            //});
        }
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
