var currentPage = 1;
jQuery(document).ready(function () {

    //$("#SectorID").change(function () {
    //    $('#Grid').trigger('reloadGrid');
    //});

    $("#btnSubmit").click(function () {

        if ($('#StateID').val() == "0" || $('#StateID').val() == "" || $('#StateID').val() == null) {
            alert("Please select State.")
            $('#StateID').focus();
            return false;
        }
        else if ($('#DistrictID').val() == "0" || $('#DistrictID').val() == "" || $('#DistrictID').val() == null) {
            alert("Please select District.")
            $('#DistrictID').focus();
            $('#Grid').jqGrid("clearGridData");
            ClearFields();
            return false;
        }
        else if ($('#ProjectID').val() == "0" || $('#ProjectID').val() == "" || $('#ProjectID').val() == null) {
            alert("Please select Project.")
            $('#ProjectID').focus();
            $('#Grid').jqGrid("clearGridData");
            ClearFields();
            return false;
        }
        else if ($('#SectorID').val() == "0" || $('#SectorID').val() == "" || $('#SectorID').val() == null) {
            alert("Please select Sector.")
            $('#SectorID').focus();
            $('#Grid').jqGrid("clearGridData");
            ClearFields();
            return false;
        }
        else {
            BindGrid();
            $('#Grid').trigger('reloadGrid');
        }

    });

});


function Submit() {

    if ($("#StateID").val() == "" || $("#StateID").val() == "0" || $("#StateID").val() == null) {
        alert("Please Select State.");
        $("#StateID").focus();
        return false;
    }
    else if ($("#DistrictID").val() == "" || $("#DistrictID").val() == "0" || $("#DistrictID").val() == null) {
        alert("Please Select District.");
        $("#DistrictID").focus();
        return false;
    }
    else if ($("#ProjectID").val() == "" || $("#ProjectID").val() == "0" || $("#ProjectID").val() == null) {
        alert("Please Select Project.");
        $("#ProjectID").focus();
        return false;
    }
    else if ($("#SectorID").val() == "" || $("#SectorID").val() == "0" || $("#SectorID").val() == null) {
        alert("Please Select Sector.");
        $("#SectorID").focus();
        return false;
    }
    else if ($("#AWCName").val() == "" || $("#AWCName").val() == "0" || $("#AWCName").val() == null) {
        alert("AWC Name should not be empty");
        $("#AWCID").val("");
        $("#AWCName").focus();
        return false;
    }
    else {
        debugger;
        var PostedData = {
            StateID: $("#StateID").val(),
            DistrictID: $("#DistrictID").val(),
            ProjectID: $("#ProjectID").val(),
            SectorID: $("#SectorID").val(),
            AWCID: $("#AWCID").val(),
            AWCName: $("#AWCName").val()
        }
        showPleaseWait();
        $.post("/AWC/AWCMaster", PostedData, function (result) {
            if (result.Code == "000") {
                hidePleaseWait();
                alert(result.Message);
                ClearFields();
                $('#Grid').trigger('reloadGrid');
            }
            else {
                alert(result.Message);
                ClearFields();
            }
        })

    }
}

function BindGrid() {
    var grid = jQuery('#Grid');
    grid.jqGrid({
        url: '/AWC/GetGridData',
        datatype: 'json',
        height: 270,
        hoverrows: false,
        shrinkToFit: true,
        autowidth: true,
        rowNum: 1000000,
        scrollOffset: 0,
        jsonReader: { repeatitems: false },
        mtype: 'GET',
        cmTemplate: { sortable: false },
        beforeRequest: function () {

            $('#Grid').jqGrid("clearGridData");
            if ($('#StateID').val() == "0") {
                alert("Please select State.")
                $('#StateID').focus();
                return false;
            }
            if ($('#DistrictID').val() == "0") {
                alert("Please select District.")
                $('#DistrictID').focus();
                $('#Grid').jqGrid("clearGridData");
                ClearFields();
                return false;
            }
            if ($('#ProjectID').val() == "0") {
                alert("Please select Project.")
                $('#ProjectID').focus();
                $('#Grid').jqGrid("clearGridData");
                ClearFields();
                return false;
            }
            if ($('#SectorID').val() == "0") {
                alert("Please select Sector.")
                $('#SectorID').focus();
                $('#Grid').jqGrid("clearGridData");
                ClearFields();
                return false;
            }
            var postData = grid.jqGrid('getGridParam', "postData");
            $.extend(postData, { "StateCode": $("#StateID option:selected").val(), "DistrictCode": $("#DistrictID option:selected").val(), "ProjectCode": $("#ProjectID option:selected").val(), "SectorCode": $("#SectorID option:selected").val() });
        },
        colNames: ['Sno', 'AWC ID', 'AWC Code', 'AWC Name', 'Actions', ' Is Hierarchy Created', 'Hierarchy Created Date', ' Is Hierarchy Actived', ' Hierarchy Activated Date', 'Delete Person Group'],
        colModel: [
                { name: 'Sno', index: 'Sno', width: 50, align: 'center' },
                { name: 'AWCID', hidden: true, index: 'SectorID', width: 190 },
                { name: 'AWCCode', hidden: true, index: 'SectorCode', width: 180 },
                { name: 'AWCName', index: 'SectorName', width: 180 },
                { name: 'ActionId', width: 100, index: 'ActionId', formatter: actionFormatter, align: 'center' },
                { name: 'IsHierarchyCreated', width: 100, index: 'IsHierarchyCreated', align: 'center', formatter: CreateHierarchyFormatter },
                { name: 'HierarchyActivatedDate', index: 'HierarchyActivatedDate', align: 'center', formatter: 'datetime' },
                { name: 'IsHierarchyActived', width: 100, index: 'IsHierarchyActived', align: 'center', formatter: HierarchyActivedFormatter },
                { name: 'HierarchyActivatedDate', index: 'HierarchyActivatedDate', align: 'center', formatter: 'datetime' },
                { name: 'DeletePersonGroup', index: 'DeletePersonGroup', align: 'center', formatter: DeletePersonGroup1 }],
        loadComplete: function () {
            debugger;
            var count = grid.getRowData().length
            if (count <= 0) {
                //alert("No Data found");
                $("#myModal").modal('show');
                //$('#Grid tbody').html("<div style='padding:6px;background:#D8D8D8;font-weight:bold'>No Data found</div>")
                $('#Grid').jqGrid("clearGridData");

            }
        }
    });
}

function actionFormatter(cellvalue, options, rowObject) {
    var AWCName = "'" + rowObject.AWCName + "'";
    var edit = '<input  class="Viewbtn" style="background-color:#5cb85c" type="button" value="Edit" data-AWCID=' + rowObject.AWCID + ' data-AWCCode=' + rowObject.AWCCode +
               ' data-AWCName=' + AWCName + ' onclick=\'BindData(this);\'  />';
    return edit;

}

function CreateHierarchyFormatter(cellvalue, options, rowObject) {
    if (rowObject.IsHierarchyCreated == "N") {
        var AWCName = "'" + rowObject.AWCName + "'";
        var edit = '<input  class="Viewbtn" style="background-color: #337ab7"  type="button" value="Create" data-AWCID=' + rowObject.AWCID + ' data-AWCCode=' + rowObject.AWCCode +
               ' data-AWCName=' + AWCName + ' onclick=\'CreateHierarchy(this);\'  />';
        return edit;
    }
    else {
        return "Yes";
    }

}

function HierarchyActivedFormatter(cellvalue, options, rowObject) {
    debugger;
    if (rowObject.IsHierarchyCreated == "Y" && rowObject.IsHierarchyActived.trim() == "N") {
        var AWCName = "'" + rowObject.AWCName + "'";
        var edit1 = '<input  class="Viewbtn" style="background-color: #337ab7"  type="button" value="Activate" data-AWCID=' + rowObject.AWCID + ' data-AWCCode=' + rowObject.AWCCode +
               ' data-AWCName=' + AWCName + ' onclick=\'HierarchyActived(this);\'  />';
        return edit1;
    }
    else if (rowObject.IsHierarchyCreated == "Y" && rowObject.IsHierarchyActived.trim() == "Y") {
        return "Yes";
    }
    else {
        return "No";
    }

}

function ClearFields() {
    $("#AWCName").val("");
    $("#AWCID").val("");
    $("#btnProceeds").prop('disabled', true);
    $('#conformMRSId').modal('hide');
}

function BindData(event) {
    var resukt = event.dataset;

    $("#lblState").text($("#StateID option:selected").text());
    $("#lblDistrict").text($("#DistrictID option:selected").text());
    $("#lblProject").text($("#ProjectID option:selected").text());
    $("#lblSector").text($("#SectorID option:selected").text());
    $("#AWCName").val(resukt.awcname);
    $("#AWCID").val(resukt.awccode);
    //$('#conformMRSId').modal('show');
    $("#conformMRSId").modal({
        backdrop: 'static'
    });

    if ($("#AWCName").val() == "") {
        $("#btnProceeds").prop('disabled', true);
    }
    else {
        $("#btnProceeds").prop('disabled', false);
    }
}

function CreateHierarchy(event) {
    var resukt = event.dataset;
    showPleaseWait();
    $.post("/AWC/CreateHierarchy?AWCCode=" + resukt.awccode + "&AWCName=" + resukt.awcname + "&StateCode=" + $("#StateID option:selected").val(), "", function (result) {
        debugger;
        if (result.statusCode == "000") {
            hidePleaseWait();
            alert(result.message);
            ClearFields();
            $('#Grid').trigger('reloadGrid');
        }
        else {
            hidePleaseWait();
            alert(result.message);
            ClearFields();
        }
    })
}

function HierarchyActived(event) {
    var resukt = event.dataset;
    showPleaseWait();
    $.post("/AWC/ActivateHierarchy?AWCCode=" + resukt.awccode + "&StateCode=" + $("#StateID option:selected").val(), "", function (result) {
        debugger;
        if (result.statusCode == "000") {
            hidePleaseWait();
            alert(result.message);
            ClearFields();
            $('#Grid').trigger('reloadGrid');
        }
        else {
            hidePleaseWait();
            alert(result.message);
            ClearFields();
        }
    })
}


function DeletePersonGroup1(cellvalue, options, rowObject) {
    if (rowObject.IsHierarchyCreated == "Y" && rowObject.IsHierarchyActived.trim() == "Y") {
        var AWCName = "'" + rowObject.AWCName + "'";
        var edit = '<input  class="Viewbtn" style="background-color:#d9534f"  type="button" value="Delete" data-AWCID=' + rowObject.AWCID + ' data-AWCCode=' + rowObject.AWCCode +
               ' data-AWCName=' + AWCName + ' onclick=\'DeletePersonGroupReq(this);\'  />';
        return edit;
    }
    else {
        return "";
    }

}

function DeletePersonGroupReq(event) {
    var resukt = event.dataset;
    showPleaseWait();

    var PostedData = {
        StateCode: $("#StateID option:selected").val(),
        DistrictCode: $('#DistrictID option:selected').val(),
        ProjectCode: $('#ProjectID option:selected').val(),
        SectorCode: $('#SectorID option:selected').val(),
        AWCCode: resukt.awccode
    }
    $.post("/AWC/DeleteHierarchy", PostedData, function (result) {
        debugger;
        if (result.statusCode == "000") {
            hidePleaseWait();
            alert(result.message);
            ClearFields();
            $('#Grid').trigger('reloadGrid');
        }
        else {
            hidePleaseWait();
            alert(result.message);
            ClearFields();
        }
    })
}

