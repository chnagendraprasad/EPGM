var currentPage = 1;
jQuery(document).ready(function () {

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
        alert("Please Select State.");
        $("#DistrictID").focus();
        return false;
    }
    else if ($("#ProjectID").val() == "" || $("#ProjectID").val() == "0" || $("#ProjectID").val() == null) {
        alert("Please Select State.");
        $("#ProjectID").focus();
        return false;
    }
    else if ($("#SectorName").val() == "" || $("#SectorName").val() == "0" || $("#SectorName").val() == null) {
        alert("Project Name should not be empty");
        $("#SectorID").val("");
        $("#SectorName").focus();
        return false;
    }
    else {
        debugger;
        var PostedData = {
            StateID: $("#StateID").val(),
            DistrictID: $("#DistrictID").val(),
            ProjectID: $("#ProjectID").val(),
            SectorID: $("#SectorID").val(),
            SectorName: $("#SectorName").val(),
            CenterType: $("#CenterType").val()
        }
        showPleaseWait();
        $.post("/Sector/SectorMaster", PostedData, function (result) {
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
    debugger;
    var grid = jQuery('#Grid');
    grid.jqGrid({
        url: '/Sector/GetGridData',
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
            var postData = grid.jqGrid('getGridParam', "postData");
            $.extend(postData, { "StateCode": $("#StateID option:selected").val(), "DistrictCode": $("#DistrictID option:selected").val(), "ProjectCode": $("#ProjectID option:selected").val(), "CenterType": $("#CenterType").val() });
        },
        colNames: ['Sno', 'Sectro ID', 'Sector Code', 'Sector Name', 'Actions'],
        colModel: [
                { name: 'Sno', index: 'Sno', width: 50, align: 'center' },
                { name: 'SectorID', hidden: true, index: 'SectorID', width: 190 },
                { name: 'SectorCode', hidden: true, index: 'SectorCode', width: 180 },
                { name: 'SectorName', index: 'SectorName', width: 180 },
                { name: 'ActionId', index: 'ActionId', formatter: actionFormatter, align: 'center' }],
        loadComplete: function () {
            debugger;
            var count = grid.getRowData().length
            if (count <= 0) {
                $("#myModal").modal('show');
            }
        }
    });

}

function actionFormatter(cellvalue, options, rowObject) {
    var SectorName = "'" + rowObject.SectorName + "'";
    var edit = '<input  class="Viewbtn"  type="button" value="Edit" data-SectorID=' + rowObject.SectorID + ' data-SectorCode=' + rowObject.SectorCode +
               ' data-SectorName=' + SectorName + ' onclick=\'BindData(this);\'  />';
    return edit;
    //return '<input type="button" data-CollAmount=' + rowObject[1] + ' data-CollAmount1=' + rowObject[2] + ' onclick=\'PrintReceipt(this);\'>Update</button>';

}

function ClearFields() {
    $("#SectorName").val("");
    $("#SectorID").val("");
    $("#btnProceeds").prop('disabled', true);
    $('#conformMRSId').modal('hide');
}

function BindData(event) {
    var resukt = event.dataset;
    $("#lblState").text($("#StateID option:selected").text());
    $("#lblDistrict").text($("#DistrictID option:selected").text());
    $("#lblProject").text($("#ProjectID option:selected").text());
    $("#SectorName").val(resukt.sectorname);
    $("#SectorID").val(resukt.sectorcode);
    // $('#conformMRSId').modal('show');
    $("#conformMRSId").modal({
        backdrop: 'static'
    });

    if ($("#SectorName").val() == "") {
        $("#btnProceeds").prop('disabled', true);
    }
    else {
        $("#btnProceeds").prop('disabled', false);
    }
}