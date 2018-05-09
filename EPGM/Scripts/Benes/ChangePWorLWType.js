$(document).ready(function () {

    $("#btnSubmit").click(function () {
        BindGridData();
    });

    $("#btnClear").click(function () {
        ClearFields();
    });
});

function BindGridData() {
    debugger;
    var grid = jQuery('#Grid');
    grid.jqGrid({
        url: urlContent + 'Bene/GetPWWomenData',
        datatype: 'json',
        height: 300,
        hoverrows: false,
        shrinkToFit: true,
        autowidth: true,
        rowNum: 1000000,
        scrollOffset: 1,
        jsonReader: { repeatitems: false },
        mtype: 'GET',
        loadonce: true,
        cmTemplate: { sortable: false },
        beforeRequest: function () {
            debugger;
            if ($('#StateID').val() == "0" || $('#StateID').val() == null) {
                alert("Please Select State")
                $('#StateID').focus();
                return false;
            }
            if (($('#DistrictID').val() == "0" || $('#DistrictID').val() == null) && ($.session.get('postback') != '1')) {
                alert("Please select District.")
                $('#DistrictID').focus();
                return false;
            }
            if (($('#ProjectID').val() == "0" || $('#ProjectID').val() == null)) {
                alert("Please select Project.")
                $('#ProjectID').focus();
                return false;
            }
            if (($('#SectorID').val() == "0" || $('#SectorID').val() == null)) {
                alert("Please Select Sector")
                $('#SectorID').focus();
                return false;
            }
            if (($('#AWCID').val() == "0" || $('#AWCID').val() == null)) {
                alert("Please Select CenterID")
                $('#AWCID').focus();
                return false;
            }
            if ($('#BeneType').val() == "0" || $('#BeneType').val() == null) {
                alert("Please Select BeneType")
                $('#BeneType').focus();
                return false;
            }
            var postData = grid.jqGrid('getGridParam', "postData");
            $.extend(postData, { "StateCode": $("#StateID option:selected").val(), "distCode": $("#DistrictID option:selected").val(), "projCode": $("#ProjectID option:selected").val(), "secCode": $("#SectorID option:selected").val(), "awcCode": $("#AWCID option:selected").val(), "BeneType": $("#BeneType option:selected").val(), "CenterType": $("#CenterType").val() });
        },
        colNames: ['S.No', 'BeneID', 'Beneficiary Code', 'Beneficiary Name', 'Husband Name', 'Mobile No', 'BeneType', 'Action'],
        colModel: [
                { name: 'Sno', index: 'Sno', width: 40, align: 'center', },
                { name: 'BeneID', index: 'Name', width: 180, hidden: true },
                { name: 'BeneCode', index: 'Name', width: 130, sortable: false },
                { name: 'Name', index: 'Name', width: 260 },
                { name: 'HusbandName', index: 'HusbandName', width: 120 },
                { name: 'MobileNo', index: 'MobileNo', width: 100, align: 'left' },
                { name: 'BeneType', index: 'BeneType', width: 100, align: 'left', hidden: true },
                { name: 'UpdateType', index: 'UpdateType', width: 80, align: 'left', formatter: UpdateType }],

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
        }
    });
    $("#Grid").setGridParam({ datatype: 'json', page: 1 }).trigger('reloadGrid');
    //$("#Grid").jqGrid("setLabel", "rn", "S.No");
}

function UpdateType(cellvalue, options, rowObject) {
    debugger;

    var WHAddress = '"' + rowObject.WHAddress + '"';
    var WHAttendanceTimeStamp = '"' + rowObject.WHTimeStamp + '"';

    var edit = '<input  class="Viewbtn"  type="button" value="Update"'
                                         + ' data-benId=' + rowObject.BeneID + ' data-benetype=' + rowObject.BeneType
                                         + ' data-BeneCode=' + rowObject.BeneCode
                                         + ' onclick=\'UpdatePWType(this);\'  />';
    return edit;

}

function UpdatePWType(event) {
    var resukt = event.dataset;
    $.ajax({
        type: "POST",
        url: '/Bene/UpdatePWType',
        data: JSON.stringify({
            'BeneID': resukt.benid,
            'BeneCode': resukt.benecode,
            'BeneType': resukt.benetype
        }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: true,
        success: function (result) {
            alert(result.message);
            BindGridData();
        },
        error: function (result) {
            alert(result.message);
        }


    });
}