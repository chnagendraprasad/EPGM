
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
        else {
            BindGird();
            $('#Grid').trigger('reloadGrid');
        }

    });
});


function Submit() {
    debugger;
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
    else if ($("#ProjectName").val() == "" || $("#ProjectName").val() == "0" || $("#ProjectName").val() == null) {
        alert("Project Name should not be empty");
        $("#ProjectID").val("");
        $("#ProjectName").focus();
        return false;
    }
    else {
        debugger;
        var PostedData = {
            StateID: $("#StateID").val(),
            DistrictID: $("#DistrictID").val(),
            ProjectID: $("#ProjectID").val(),
            ProjectName: $("#ProjectName").val()
        }
        showPleaseWait();
        $.post("/Project/ProjectMaster", PostedData, function (result) {
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

function BindGird() {
    debugger;
    var grid = jQuery('#Grid');
    grid.jqGrid({
        url: '/Project/BindDataGrid',
        datatype: 'json',
        height: 320,
        hoverrows: false,
        shrinkToFit: true,
        autowidth: true,
        rowNum: 1000000,
        scrollOffset: 0,
        jsonReader: { repeatitems: false },
        mtype: 'GET',
        cmTemplate: { sortable: false },
        beforeRequest: function () {
            debugger;
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

            var postData = grid.jqGrid('getGridParam', "postData");
            $.extend(postData, { "StateCode": $("#StateID option:selected").val(), "DistrictCode": $("#DistrictID option:selected").val() });
        },
        colNames: ['Sno', 'Project ID', 'Project Code', 'Project Name', 'Actions'],
        colModel: [
                { name: 'Sno', index: 'Sno', width: 50, align: 'center' },
                { name: 'ProjectID', hidden: true, index: 'ProjectID', width: 150 },
                { name: 'ProjectCode', hidden: true, index: 'ProjectCode', width: 180 },
                { name: 'ProjectName', index: 'ProjectName', width: 180 },
                { name: 'ActionId', index: 'ActionId', formatter: actionFormatter, width: 100, align: 'center' }],
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
    var ProjectName = "'" + rowObject.ProjectName + "'";
    var edit = '<input  class="Viewbtn"  type="button" value="Edit" data-ProjectID=' + rowObject.ProjectID + ' data-ProjectCode=' + rowObject.ProjectCode +
               ' data-ProjectName=' + ProjectName + ' onclick=\'BindData(this);\'  />';
    return edit;

}

function ClearFields() {
    $("#ProjectName").val("");
    $("#ProjectID").val("");
    $("#btnProceeds").prop('disabled', true);
    $('#conformMRSId').modal('hide');

}

function BindData(event) {
    debugger;
    var resukt = event.dataset;

    $("#lblState").text($("#StateID option:selected").text());
    $("#lblDistrict").text($("#DistrictID option:selected").text());
    $("#ProjectName").val(resukt.projectname);
    $("#ProjectID").val(resukt.projectcode);
    // $('#conformMRSId').modal('show');
    $("#conformMRSId").modal({
        backdrop: 'static',
        keyboard: false
    });

    if ($("#ProjectName").val() == "") {
        $("#btnProceeds").prop('disabled', true);
    }
    else {
        $("#btnProceeds").prop('disabled', false);
    }
}