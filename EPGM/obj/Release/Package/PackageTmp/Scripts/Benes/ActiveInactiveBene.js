﻿var currentPage = 1;

jQuery(document).ready(function () {

    //$("#CenterID").change(function () {
    //    debugger;
    //    grid.trigger('reloadGrid');
    //});


    $("#btnSubmit").click(function () {
        debugger;
        var date = new Date();
        if ($('#StateID').val() == " " || $('#StateID').val() == null) {
            alert("Please Select State")
            $('#StateID').focus();
            return false;
        }
        else if ($('#DistrictID').val() == " " || $('#DistrictID').val() == null) {
            alert("Please Select District")
            $('#DistrictID').focus();
            return false;
        }
        else if ($('#ProjectID').val() == " " || $('#ProjectID').val() == null) {
            alert("Please select Project")
            $('#ProjectID').focus();
            return false;
        }
        else if ($('#SectorID').val() == " " || $('#SectorID').val() == null) {
            alert("Please Select Sector")
            $('#SectorID').focus();
            return false;
        }
        else if ($('#CenterID').val() == " " || $('#CenterID').val() == null) {
            alert("Please Select Center")
            $('#CenterID').focus();
            return false;
        }
        else if ($('#BeneType').val() == " " || $('#BeneType').val() == null) {
            alert("Please Select BeneType")
            $('#BeneType').focus();
            return false;
        }
        else {
            BindGridData();
            jQuery('#Grid').trigger('reloadGrid');
        }
    });

    $("#btnClear").click(function () {
        ClearFields();
    });

    callbackFunction = function (status) {
        grid.trigger('reloadGrid');
    }

    $("#btnRefresh").click(function () {
        grid.trigger('reloadGrid');
    });

    $("#EDScreen").click(function () {

        debugger;
        var count = $("#Grid").getGridParam("reccount");

        if (count == 0) {
            alert("There is no records in grid");
            return false;
        }

        var selRowIds = $("#Grid").jqGrid('getGridParam', 'selarrrow');

        if (selRowIds.length == 0) {
            alert("Please Select Beneficiary/Beneficiaries to Enable/Disable");
            return false;
        }
        var j = selRowIds.length;
        for (var i = 0; i < selRowIds.length; i++) {

            var rowId = $("#Grid").jqGrid('getGridParam', 'selrow');

            var rowData = $("#Grid").jqGrid('getRowData', selRowIds[i]);

            $.ajax({
                type: "POST",
                url: '/Bene/BenestatusChange',
                data: JSON.stringify({
                    'BeneCode': rowData.BeneCode,
                    'Status': rowData.IsActive,
                    StateCode: $('#StateID').val(),
                    distCode: $('#DistrictID').val(),
                    projCode: $('#ProjectID').val(),
                    secCode: $('#SectorID').val(),
                    awcCode: $('#CenterID').val(),
                    centertype: $("#CenterType").val()
                }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: true,
                success: function (result) {
                    if (result.BeneCode != null && result.BeneCode != "" && result.AWCCode != null && result.AWCCode != "") {
                        debugger;
                        j = j - 1;
                        if (j == 0) {
                            if (selRowIds.length == 1) {
                                debugger;
                                alert("Selected Beneficiary Successfully Enabled/Disabled.");
                                //$("#SectorID").val('<%=Session["BSector"] %>');
                                //BindDropDownCenter()
                                //$("#CenterID").val('<%=Session["BAWC"] %>');
                                $("#Grid").trigger("reloadGrid", [{ current: false }]);
                            }
                            else if (selRowIds.length > 1) {
                                debugger;
                                alert("Selected Beneficiaries Successfully Enabled/Disabled.");
                                $("#Grid").trigger("reloadGrid", [{ current: false }]);

                                //location.href = $("#RedirectTo").val();
                            }

                        }
                    }
                    else {
                        alert("Failed to Update Data");
                    }
                },
                error: function (result) { }


            });

        }

    });


});



function BindGridData() {
    var grid = jQuery('#Grid');
    grid.jqGrid({
        url: urlContent + 'Bene/GetBenesofCenter',
        datatype: 'json',
        height: 330,
        hoverrows: false,
        shrinkToFit: false,
        autowidth: true,
        multiselect: true,
        rowNum: 1000000,
        scrollOffset: 0,
        mtype: 'GET',
        cmTemplate: { sortable: false },
        beforeRequest: function () {
            if ($('#StateID').val() == "0") {
                alert("Please select State.")
                $('#StateID').focus();
                return false;
            }
            if ($('#DistrictID').val() == "0") {
                alert("Please select District.")
                $('#DistrictID').focus();
                return false;
            }
            if ($('#ProjectID').val() == "0" || $('#ProjectID').val() == null) {
                alert("Please select Project.")
                $('#ProjectID').focus();
                return false;
            }
            if ($('#SectorID').val() == "0" || $('#SectorID').val() == null) {
                alert("Please Select Sector")
                $('#SectorID').focus();
                return false;
            }
            if ($('#CenterID').val() == "0" || $('#CenterID').val() == null) {
                alert("Please Select Center")
                $('#CenterID').focus();
                return false;
            }
            if ($('#BeneType').val() == "0" || $('#BeneType').val() == null) {
                alert("Please Select BeneType")
                $('#BeneType').focus();
                return false;
            }
            var postData = grid.jqGrid('getGridParam', "postData");
            $.extend(postData, { "StateCode": $("#StateID option:selected").val(), "distCode": $("#DistrictID option:selected").val(), "projCode": $("#ProjectID option:selected").val(), "secCode": $("#SectorID option:selected").val(), "awcCode": $("#CenterID option:selected").val(), "BeneType": $("#BeneType option:selected").val(), "CenterType": $("#CenterType").val() });
        },

        colNames: ['Sno', 'AWCCode', 'Beneficiary Code', 'Name', 'Surname', 'Gender', 'DOB', 'Age', 'Status', 'Mother Name', 'Father Name', 'Aadhaar No', 'Mobile No', 'Registered Date & Time'],
        colModel: [
                { name: 'Sno', index: 'Sno', width: 50, align: 'center', hidden: true },
                { name: 'AWCCode', index: 'AWCCode', width: 165, hidden: true },
                { name: 'BeneCode', index: 'BeneCode', width: 80 },
                { name: 'BeneName', index: 'Name', width: 150 },
                { name: 'BeneSurname', index: 'BeneSurname', width: 150 },
                { name: 'Gender', index: 'Gender', width: 55, align: 'center' },
                { name: 'DOB', index: 'DOB', width: 90, align: 'left' },
                { name: 'Age', index: 'Age', width: 60, align: 'center' },
                { name: 'IsActive', index: 'IsActive', width: 60, edittype: 'checkbox', align: 'center', editoptions: { value: "True:False" }, formatter: "checkbox", formatoptions: { disabled: true } },
                { name: 'MotherName', index: 'MotherName', width: 130, align: 'left' },
                { name: 'FatherName', index: 'FatherName', width: 130, align: 'left' },
                { name: 'AadhaarNumber', index: 'AadhaarNumber', width: 100, align: 'left' },
                { name: 'MobileNumber', index: 'MobileNumber', width: 90, align: 'left' },
                { name: 'RegisteredDateTime', index: 'RegisteredDateTime', width: 160, align: 'left' }],

        loadComplete: function () {
            debugger;
            $("#EDScreen").show();
            var count = grid.getRowData().length
            if (count <= 0) {
                $("#myModal").modal('show');
                $("#EDScreen").hide();
            }
        },
        loadError: function (xhr, status, error) {
            var trf = $("#Grid tbody:first tr:first")[0];
            $("#Grid tbody:first").empty().append(trf);
        }
    });

}

function showWHO(cellvalue, options, rowObject) {
    debugger;
    if (rowObject.LatestGrade == "NOR")
        return "Normal";
    if (rowObject.LatestGrade == "MUW")
        return "Moderate";
    if (rowObject.LatestGrade == "SUW")
        return "Severe";
    if (rowObject.LatestGrade == "ABS")
        return "Absent";
}

function showGender(cellvalue, options, rowObject) {
    debugger;
    if (rowObject.Gender == "F")
        return "Female";
    if (rowObject.Gender == "M")
        return "Male";
}

function BeneDet(cellvalue, options, rowObject) {
    debugger;
    return "<a href='" + urlContent + "Bene/BeneDetails/?beneCode=" + rowObject.BeneCode + "&distname=" + $('#DistrictID option:Selected').text() + "&projectname=" + $('#ProjectID option:Selected').text() + "&sectorname=" + $('#SectorID option:Selected').text() + "&awcname=" + $('#CenterID option:Selected').text() + "'style='color: black'>" + rowObject.BeneCode + "</a>";
    //return "<a href='" + urlContent + "Bene/BeneDetails/?beneCode=" + rowObject.BeneCode + "&distname=" + $('#DistrictID option:Selected').text() + "&projectname=" + $('#ProjectID option:Selected').text() + "&sectorname=" + $('#SectorID option:Selected').text() + "&awcname=" + $('#CenterID option:Selected').text() + "'style='color: black'>" + rowObject.BeneCode + "</a>";
}