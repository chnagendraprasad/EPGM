
var currentPage = 1;
jQuery(document).ready(function () {

    $("#btnSubmit").click(function () {

        if ($("#StateID").val() == "" || $("#StateID").val() == "0" || $("#StateID").val() == null) {
            alert("Please Select State");
            $("#StateID").focus();
            $('#Grid').jqGrid("clearGridData");
            return false;
        }
        else {
            BindGird();
            $('#Grid').trigger('reloadGrid');
        }
    });

});

function Submit() {
    if ($("#DistrictName").val() == "" || $("#DistrictName").val() == "0" || $("#DistrictName").val() == null) {
        alert("District Name should not be empty");
        $("#DistrictCode").val("");
        $("#DistrictName").focus();
        return false;
    }
    else {
        var PostedData = {
            DistrictCode: $("#DistrictCode").val(),
            DistrictName: $("#DistrictName").val(),
            StateID: $("#StateID").val()
        }

        showPleaseWait();
        $.ajax({
            type: "POST",
            url: '/District/DistrictMaster',
            data: JSON.stringify(PostedData),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            async: true,
            success: function (result) {
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
            },
            error: function (result) {
                alert(result.Message);
            }

        });
    }
}

function BindGird() {
    var grid = jQuery('#Grid');
    grid.jqGrid({
        url: '/District/MasterGridData',
        datatype: 'json',
        height: 330,
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
            $('#Grid').jqGrid("clearGridData");
            if ($('#StateID').val() == "0") {
                alert("Please select State.")
                $('#StateID').focus();
                $('#Grid').jqGrid("clearGridData");
                ClearFields();
                return false;
            }
            var postData = grid.jqGrid('getGridParam', "postData");
            $.extend(postData, { "StateCode": $("#StateID option:selected").val() });
        },
        colNames: ['Sno', 'District ID', 'District Code', 'District Name', 'Actions'],
        colModel: [
                { name: 'Sno', index: 'Sno', width: 50, align: 'center' },
                { name: 'DistrictID', hidden: true, index: 'DistrictID', width: 190 },
                { name: 'DistrictCode', hidden: true, index: 'DistrictCode', width: 180 },
                { name: 'DistrictName', index: 'DistrictName', width: 180 },
                { name: 'ActionId', index: 'ActionId', formatter: actionFormatter, align: 'center' }],
        loadComplete: function () {
            debugger;
            var count = grid.getRowData().length
            if (count <= 0) {
                //alert("No Data found");
                $("#myModal").modal('show');
                //$('#Grid').jqGrid("clearGridData");
                //$('#Grid tbody').html("<div style='padding:6px;background:#D8D8D8;font-weight:bold'>No Data found</div>")
            }
        }
    });

}

function actionFormatter(cellvalue, options, rowObject) {
    var DistName = "'" + rowObject.DistrictName + "'";
    var edit = '<input  class="Viewbtn"  type="button" value="Edit" data-DistrictID=' + rowObject.DistrictID + ' data-DistrictCode=' + rowObject.DistrictCode +
               ' data-DistrictName=' + DistName + ' onclick=\'BindData(this);\'  />';
    return edit;

}

function ClearFields() {
    debugger;
    $("#DistrictName").val("");
    $("#DistrictCode").val("");
    $("#btnProceeds").prop('disabled', true);
    $('#conformMRSId').modal('hide');
}

function BindData(event) {

    var resukt = event.dataset;
    $("#lblState").text($("#StateID option:selected").text());
    $("#DistrictName").val(resukt.districtname);
    $("#DistrictCode").val(resukt.districtcode);

    //$("#conformMRSId").modal('show');
    $("#conformMRSId").modal({
        backdrop: 'static',
        keyboard: false
    });
    if ($("#DistrictName").val() == "") {
        $("#btnProceeds").prop('disabled', true);
    }
    else {
        $("#btnProceeds").prop('disabled', false);
    }
}