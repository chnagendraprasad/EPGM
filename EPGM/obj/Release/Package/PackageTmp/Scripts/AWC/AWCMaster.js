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
        alert("Center Name should not be empty");
        $("#AWCID").val("");
        $("#AWCName").focus();
        return false;
    }
    else if ($("#AWWName").val() == "" || $("#AWWName").val() == "0" || $("#AWWName").val() == null) {
        alert("Worker Name should not be empty");
        $("#AWWName").focus();
        return false;
    }
    else if ($("#AWWMobileNo").val() == "" || $("#AWWMobileNo").val() == null) {
        alert("Please Enter Mobile No.");
        $("#AWWMobileNo").focus();
        return false;
    }
    else if ($("#AWWMobileNo").val().substring(0, 1) == 0) {
        alert("Mobile No. should not start with zero");
        $("#AWWMobileNo").focus();
        return false;
    }
    else if ($("#AWWMobileNo").val().length != "10") {
        alert("Please Enter 10 digit Mobile No.");
        $("#AWWMobileNo").focus();
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
            AWCName: $("#AWCName").val(),
            AWWName: $("#AWWName").val(),
            AWWMobileNo: $("#AWWMobileNo").val(),
            CenterType: $("#CenterType").val()
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
                hidePleaseWait();
            }
        })

    }
}





function BindGrid() {


    var grid = jQuery('#Grid');
    grid.jqGrid({
        url: '/AWC/GetGridData',
        datatype: 'json',
        height: 350,
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
            $.extend(postData, { "StateCode": $("#StateID option:selected").val(), "DistrictCode": $("#DistrictID option:selected").val(), "ProjectCode": $("#ProjectID option:selected").val(), "SectorCode": $("#SectorID option:selected").val(), "CenterType": $("#CenterType").val() });
        },
        colNames: ['Sno', 'AWC ID', 'AWC Code', 'AWC Name', 'AWC Image', 'Worker Name', 'Worker Mobile No.', 'Actions', ' Is Hierarchy Created', 'Hierarchy Created Date', ' Is Hierarchy Actived', ' Hierarchy Activated Date', 'Delete Hierarchy','Staff Name','Created Date','Assign','Staff Id','Staff Mobile'],
        colModel: [
                { name: 'Sno', index: 'Sno', width: 40, align: 'center' },
                { name: 'AWCID', hidden: true, index: 'SectorID', width: 190 },
                { name: 'AWCCode', hidden: true, index: 'SectorCode', width: 180 },
                { name: 'AWCName', index: 'AWCName', width: 180 },
                { name: 'AWCImagePath', index: 'AWCImagePath', width: 100, formatter: AWCImage, align: 'center' },
                { name: 'AWWName', index: 'AWWName', width: 150 },
                { name: 'AWWMobile', index: 'AWWMobile' },
                { name: 'ActionId', width: 80, index: 'ActionId', formatter: actionFormatter, align: 'center' },
                { name: 'IsHierarchyCreated', index: 'IsHierarchyCreated', align: 'center', formatter: CreateHierarchyFormatter },
                { name: 'HierarchyCreatedDate', index: 'HierarchyCreatedDate', align: 'center', formatter: 'datetime' },
                { name: 'IsHierarchyActived', index: 'IsHierarchyActived', align: 'center', formatter: HierarchyActivedFormatter },
                { name: 'HierarchyActivatedDate', index: 'HierarchyActivatedDate', align: 'center', formatter: 'datetime' },
                { name: 'DeletePersonGroup', index: 'DeletePersonGroup', align: 'center', width: 100, formatter: DeletePersonGroup1 },
        { name: 'FieldForceName', index: 'FieldForceName', width: 150 },
        { name: 'FiledForceStaffCreatedDateTime', index: 'FiledForceStaffCreatedDateTime', width: 150, align: 'center', formatter: 'datetime' },
        { name: 'Sno', index: 'Sno', width: 80, formatter: AssignFormatter },
          { name: 'StaffID', hidden: true, index: 'StaffID', width: 190 },
          { name: 'FiledForceStaffMobileNo', hidden: true, index: 'FiledForceStaffMobileNo', width: 190 }, ],
        loadComplete: function () {
            debugger;
            //DisableColumns();
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
    var AWWName = "'" + rowObject.AWWName + "'";
    var edit = '<input  class="Viewbtn" style="background-color:#5cb85c" type="button" value="Edit" data-AWCID=' + rowObject.AWCID +
                                               ' data-AWCCode=' + rowObject.AWCCode + ' data-AWCName=' + AWCName +
                                               ' data-AWWName=' + AWWName + ' data-MobileNo=' + rowObject.AWWMobile + ' onclick=\'BindData(this);\'  />';
    return edit;

}

function AssignFormatter(cellvalue, options, rowObject) {
    var AWCName = "'" + rowObject.AWCName + "'";
    var AWWName = "'" + rowObject.AWWName + "'";
    var StaffName = "'" + rowObject.FieldForceName + "'";
    var edit = '<input  class="Viewbtn" style="background-color:#5cb85c" type="button" value="Assign" data-AWCID=' + rowObject.AWCID + 
                                               ' data-AWCCode=' + rowObject.AWCCode +
                                               ' data-AWCName=' + AWCName +
                                               ' data-AWWName=' + AWWName +
                                               ' data-MobileNo=' + rowObject.AWWMobile +
                                               ' data-StaffID=' + rowObject.StaffID +
                                               ' data-FiledForceStaffMobileNo=' + rowObject.FiledForceStaffMobileNo +
                                               ' data-FieldForceName=' + StaffName + '  onclick=\'AssingBindData(this);\'  />';
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
    debugger;
    var resukt = event.dataset;

    $("#lblState").text($("#StateID option:selected").text());
    $("#lblDistrict").text($("#DistrictID option:selected").text());
    $("#lblProject").text($("#ProjectID option:selected").text());
    $("#lblSector").text($("#SectorID option:selected").text());
    $("#AWCName").val(resukt.awcname);
    $("#AWCID").val(resukt.awccode);
    $("#AWWName").val(resukt.awwname);
    $("#AWWMobileNo").val(resukt.mobileno);
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

//Modal Open
function AssingBindData(event) {
    debugger;
    var resukt = event.dataset;

    
    if ($("#CenterType").val() != "1")
    {
        alert("Field Force Staff are allowed to assign only Angawadi Centers");
        return false;
    }

    $.get("/AWC/GetStaff", { State: $("#StateID").val() }, function (Result) {

        $("#StaffID").empty()
            debugger;
            var opt = new Option("--Select Staff--", "0");

            $("#StaffID").append(opt)
            
            for (var i = 0; i < Result.length; i++) {
                opt = new Option(Result[i].StaffCode, Result[i].StaffID);
                $("#StaffID").append(opt);
            }
            $("#StaffID").val(resukt.staffid);
        
        });
    //alert(resukt.staffid + " " + resukt.fieldforcename);

    $("#lblUserState").text($("#StateID option:selected").text());
    $("#lblUserDistrict").text($("#DistrictID option:selected").text());
    $("#lblUserProject").text($("#ProjectID option:selected").text());
    $("#lblUserSector").text($("#SectorID option:selected").text());
    $("#lblAwcNmae").text(resukt.awcname);
    $("#lblWorkerid").text(resukt.awccode);
    $("#lblWorkerNmae").text(resukt.awwname);
    $("#lblWorkerMobileNo").text(resukt.mobileno);

    if (resukt.staffid > 0)
    {
        $("#lblStaffName").text(resukt.fieldforcename);
        $("#lblStaffNobileNo").text(resukt.filedforcestaffmobileno);
    }
    else
    {
        $("#lblStaffName").text("");
        $("#lblStaffNobileNo").text("");

    }
    


    


    //$('#conformMRSId').modal('show');
    $("#conformAssign").modal({
        backdrop: 'static'
    });

    
}

function BindStaffDet() {

    if ($("#StaffID").val() > 0)
    {
        $.get("/AWC/GetStaffDetails", { StaffCode: $("#StaffID option:selected").text() }, function (result) {

            $("#lblStaffName").text(result.Name);
            $("#lblStaffNobileNo").text(result.MobileNumber);
        })
        }
        else
    {
        $("#lblStaffName").text("");
        $("#lblStaffNobileNo").text("");
    }

       

   

}

function SubmitAssign() {

    if ($("#StaffID").val() == "0" || $("#StaffID").val() == null) {
        alert("Please Select Staff");
        $("#StaffID").focus();
        return false();
    }

    $('#conformAssign').modal('hide');

    conformAssign
    showPleaseWait();

    $.get("/AWC/AssignStaff", { StateID: $("#StateID").val(), DistrictID: $("#DistrictID").val(), ProjectID: $("#ProjectID").val(), SectorID: $("#SectorID").val(), AwcCode: $("#lblWorkerid").text(), StaffId: $("#StaffID").val() }, function (result) {

        if (result.statusCode == "000") {
            hidePleaseWait();
            alert(result.message);
            ClearFields();
            $('#Grid').trigger('reloadGrid');
        }
        else {
            alert(result.message);
            ClearFields();
            hidePleaseWait();
        }
    })


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
    if (confirm("do you want to delete Hierarchy from Attendance?")) {
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
}

function AWCImage(cellvalue, options, rowObject) {
    debugger;
    if (rowObject.AWCImagePath != null && rowObject.AWCImagePath != "") {
        var Address = "'" + rowObject.Address + "'";
        var AWCDateTime = "'" + rowObject.AWCImageCreatedDateTime + "'";
        var edit = '<input  class="Viewbtn" style="background-color:#f0ad4e"  type="button" value="Click"  data-AWCImageURL=' + rowObject.AWCImagePath + ' data-Address=' + Address + ' data-AWCDateTime=' + AWCDateTime + ' onclick=\'ShowAWCImage(this);\'  />';
        return edit;
    }
    else {
        return "";
    }
}

function ShowAWCImage(event) {
    debugger;
    $("#imgpopup").prop('src', "");
    $("#headTitle").text("");
    $("#headTitletime").text("");
    var resukt = event.dataset;
    $("#imgpopup").prop('src', resukt.awcimageurl);
    $("#headTitletime").text(resukt.awcdatetime);
    $("#headTitle").text(resukt.address);
    $('#myModal1').modal('show');
}

