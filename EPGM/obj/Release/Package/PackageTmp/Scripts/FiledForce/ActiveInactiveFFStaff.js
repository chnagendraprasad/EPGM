var currentPage = 1;

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
                url: '/FieldForce/FielForceStatusChange',
                data: JSON.stringify({
                    'StaffId': rowData.StaffID,
                    'Status': rowData.IsActive,
                    StateCode: $('#StateID').val(),
                   
                }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: true,
                success: function (result) {
                    if (result.StaffCode != null && result.StaffID != null && result.Status != null) {
                        debugger;
                        j = j - 1;
                        if (j == 0) {
                            if (selRowIds.length == 1) {
                                debugger;
                                alert("Selected Field Force Staff Successfully Enabled/Disabled.");
                                //$("#SectorID").val('<%=Session["BSector"] %>');
                                //BindDropDownCenter()
                                //$("#CenterID").val('<%=Session["BAWC"] %>');
                                $("#Grid").trigger("reloadGrid", [{ current: false }]);
                            }
                            else if (selRowIds.length > 1) {
                                debugger;
                                alert("Selected Field Force Staff Successfully Enabled/Disabled.");
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
        url: urlContent + 'FieldForce/GetFieldForceStaff',
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
            
            var postData = grid.jqGrid('getGridParam', "postData");
            $.extend(postData, { "StateCode": $("#StateID option:selected").val()});
        },

        colNames: ['Sno', 'StaffId', 'Staff Code', 'Name', 'Gender', 'DOB', 'Qualification', 'DOJ','Status', 'Mobile No'],
        colModel: [
                { name: 'RowNo', index: 'RowNo', align: 'center',width:50 },
                { name: 'StaffID', index: 'StaffID', hidden: true },
                { name: 'StaffCode', index: 'StaffCode' },
                { name: 'Name', index: 'Name',width:150 },
                
                { name: 'Gender', index: 'Gender', align: 'center' },
               
                { name: 'C_DOB_', index: 'C_DOB_', align: 'left' },
                 { name: 'Qualification', index: 'Qualification', align: 'center' },
                 { name: 'C_DOJ_', index: 'C_DOJ_', align: 'left' },

                
                { name: 'IsActive', index: 'IsActive', edittype: 'checkbox', align: 'center', editoptions: { value: "True:False" }, formatter: "checkbox", formatoptions: { disabled: true } },
                
                { name: 'MobileNumber', index: 'MobileNumber', align: 'left' },
                ],

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