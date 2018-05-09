$(document).ready(function () {
    $("#btnSubmit").click(function () {
        if ($('#StateID').val() == "0" || $('#StateID').val() == "" || $('#StateID').val() == null) {
            alert("Please Select State");
            $('#StateID').focus();
            return false;
        }
        else if ($('#DistrictID').val() == "0" || $('#DistrictID').val() == null) {
            alert("Please Select District");
            $('#DistrictID').focus();
            return false;
        }
        else if ($('#ProjectID').val() == "0" || $('#ProjectID').val() == null) {
            alert("Please Select Project");
            $('#ProjectID').focus();
            return false;
        }
        else if ($('#SectorID').val() == "0" || $('#SectorID').val() == null) {
            alert("Please Select Sector");
            $('#SectorID').focus();
            return false;
        }
        else if ($('#CenterID').val() == "0" || $('#CenterID').val() == null) {
            alert("Please Select Center");
            $('#CenterID').focus();
            return false;
        }
        else {
            BindMenuList();
        }
    });
});

function BindMenuList() {
    debugger;
    //url = '@Url.Action("GetMobileAppMenus", "Users")';
    var grid = jQuery('#Grid');
    grid.jqGrid({
        url: "/Users/GetMobileAppMenus",
        datatype: 'json',
        height: 300,
        hoverrows: false,
        shrinkToFit: true,
        autowidth: true,
        rowNum: 1000000,
        scrollOffset: 0,
        jsonReader: { repeatitems: false },
        mtype: 'GET',
        loadonce: true,
        cmTemplate: { sortable: false },
        beforeRequest: function () {
            var postData = grid.jqGrid('getGridParam', "postData");
            $.extend(postData, { "StateCode": $("#StateID option:selected").val(), "distCode": $("#DistrictID option:selected").val(), "projCode": $("#ProjectID option:selected").val(), "secCode": $("#SectorID option:selected").val(), "awcCode": $("#CenterID option:selected").val(), "CenterType": $("#CenterType").val() });
        },
        colNames: ['Menu Id', 'Menu(Screen) Name', 'Full Access', 'No Access'],
        colModel: [
             { name: 'AppMenuID', index: 'AppMenuID', align: 'center', hidden: true },
                { name: 'Menu_Name', index: 'Menu_Name', align: 'left' },
                { name: 'Enabled', index: 'Enabled', width: 130, formatter: FullAccess, align: 'center' },
                { name: 'Enabled', index: 'Enabled', width: 130, formatter: NoAccess, align: 'center' }, ],

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
}

function FullAccess(cellvalue, options, rowObject) {
    debugger;
    if (rowObject.Enabled == true) {
        var Radio = '<input type="radio" name="F' + rowObject.AppMenuID + '"   Value=' + rowObject.AppMenuID + ' onchange="ChngeRadio(this);" checked />';
    }
    else {
        var Radio = '<input type="radio" name="F' + rowObject.AppMenuID + '"  Value=' + rowObject.AppMenuID + ' onchange="ChngeRadio(this);"  />';
    }
    return Radio;
}

function NoAccess(cellvalue, options, rowObject) {
    debugger;
    if (rowObject.Enabled == true) {
        var Radio1 = '<input type="radio" name="N' + rowObject.AppMenuID + '"   Value=' + rowObject.AppMenuID + ' onchange="ChngeRadio(this);"  />';
    }
    else {
        var Radio1 = '<input type="radio" name="N' + rowObject.AppMenuID + '"  Value=' + rowObject.AppMenuID + ' onchange="ChngeRadio(this);" checked  />';
    }
    return Radio1;
}

function ChngeRadio(e) {

    debugger;
    showPleaseWait();
    $.get("/Users/UpdateAppMenuRole", {
        "StateCode": $("#StateID option:selected").val(), "distCode": $("#DistrictID option:selected").val(),
        "projCode": $("#ProjectID option:selected").val(), "secCode": $("#SectorID option:selected").val(),
        "awcCode": $("#CenterID option:selected").val(), "MenuID": e.value, "rName": e.name,"CenterType" :$("#CenterType").val()
    }, function (result) {
        if (result.code == "000") {
            hidePleaseWait();
            alert(result.message);
            BindMenuList();
        }
        else {
            hidePleaseWait();
            alert(result.message);
        }
    });

}