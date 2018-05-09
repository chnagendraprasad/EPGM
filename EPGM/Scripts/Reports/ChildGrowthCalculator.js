
jQuery(document).ready(function () {
});
function EnableDisableWA() {
    debugger;
    var grid = jQuery('#GridWA');
    grid.jqGrid({
        url: urlContent + 'Reports/GetWeightAgeData',
        datatype: 'json',
        height: 300,
        hoverrows: false,
        shrinkToFit: true,
        autowidth: true,
        rowNum: 1000000,
        scrollOffset: 1,
        jsonReader: { repeatitems: false },
        mtype: 'GET',
        cmTemplate: { sortable: false },
        beforeRequest: function () {
            debugger;
            if ($('#Gender').val() == "0" || $('#Gender').val() == null) {
                alert("Please Select Gender")
                $('#Gender').focus();
                return false;
            }
            var postData = grid.jqGrid('getGridParam', "postData");
            $.extend(postData, { "Gender": $("#Gender option:selected").val() });
        },
        colNames: ['Age In Months', 'Normal From', 'Normal To', 'Moderate From', 'Moderate To', 'Severe From', 'Severe To'],
        colModel: [
                { name: 'AgeInMonths', index: 'AgeInMonths', width: '60px' },
                { name: 'NormalFrom', index: 'NormalFrom', width: '100px' },
                { name: 'NormalTo', index: 'NormalTo', width: '100px' },
                { name: 'ModerateFrom', index: 'ModerateFrom', width: '100px' },
                { name: 'ModerateTo', index: 'ModerateTo', width: '100px' },
                { name: 'SevereFrom', index: 'SevereFrom', width: '100px' },
                { name: 'SevereTo', index: 'SevereTo', width: '100px' }],

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
    $("#lihome").addClass("active");
    $("#liprofile").removeClass("active");
    $("#limessages").removeClass("active");
    $("#home").addClass("active");
    $("#profile").removeClass("active");
    $("#messages").removeClass("active");

    $("#home").css('display', 'block');
    $("#home").addClass("tab-pane");
    $("#profile").css('display', 'none');
    $("#messages").css('display', 'none');
    grid.trigger('reloadGrid');
}

function EnableDisableHW() {
    debugger;
    var grid = jQuery('#GridHW');
    grid.jqGrid({
        url: urlContent + 'Reports/GetHeightWeightData',
        datatype: 'json',
        height: 300,
        hoverrows: false,
        shrinkToFit: true,
        autowidth: true,
        rowNum: 1000000,
        scrollOffset: 1,
        jsonReader: { repeatitems: false },
        mtype: 'GET',
        cmTemplate: { sortable: false },
        beforeRequest: function () {
            debugger;
            if ($('#Gender').val() == "0" || $('#Gender').val() == null) {
                alert("Please Select Gender")
                $('#Gender').focus();
                return false;
            }
            var postData = grid.jqGrid('getGridParam', "postData");
            $.extend(postData, { "Gender": $("#Gender option:selected").val() });
        },
        colNames: ['Height (Cm)', 'Normal From', 'Normal To', 'Moderate From', 'Moderate To'],
        colModel: [
                { name: 'HeightInCm', index: 'AgeInMonths', width: '60px' },
                { name: 'NormalFrom', index: 'NormalFrom', width: '100px' },
                { name: 'NormalTo', index: 'NormalTo', width: '100px' },
                { name: 'MAMFrom', index: 'MAMFrom', width: '100px' },
                { name: 'MAMTo', index: 'MAMTo', width: '100px' }],

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
    $("#lihome").removeClass("active");
    $("#liprofile").addClass("active");
    $("#limessages").removeClass("active");
    $("#home").removeClass("active");
    $("#profile").addClass("active");
    $("#messages").removeClass("active");

    $("#profile").css('display', 'block');
    $("#profile").addClass("tab-pane");
    $("#home").css('display', 'none');
    $("#messages").css('display', 'none');
    grid.trigger('reloadGrid');
}

function EnableDisableHA() {
    debugger;
    var grid = jQuery('#GridHA');
    grid.jqGrid({
        url: urlContent + 'Reports/GetHeightAgeData',
        datatype: 'json',
        height: 300,
        hoverrows: false,
        shrinkToFit: true,
        autowidth: true,
        rowNum: 1000000,
        scrollOffset: 1,
        jsonReader: { repeatitems: false },
        mtype: 'GET',
        cmTemplate: { sortable: false },
        beforeRequest: function () {
            debugger;
            if ($('#Gender').val() == "0" || $('#Gender').val() == null) {
                alert("Please Select Gender")
                $('#Gender').focus();
                return false;
            }
            var postData = grid.jqGrid('getGridParam', "postData");
            $.extend(postData, { "Gender": $("#Gender option:selected").val() });
        },
        colNames: ['Age In Months', 'Normal From', 'Normal To', 'Moderate From', 'Moderate To'],
        colModel: [
                { name: 'AgeinMonths', index: 'AgeInMonths', width: '60px' },
                { name: 'NormalFrom', index: 'NormalFrom', width: '100px' },
                { name: 'NormalTo', index: 'NormalTo', width: '100px' },
                { name: 'MAMFrom', index: 'MAMFrom', width: '100px' },
                { name: 'MAMTo', index: 'MAMTo', width: '100px' }],

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

    $("#lihome").removeClass("active");
    $("#liprofile").removeClass("active");
    $("#limessages").addClass("active");
    $("#home").removeClass("active");
    $("#profile").removeClass("active");
    $("#messages").addClass("active");

    $("#messages").css('display', 'block');
    $("#messages").addClass("tab-pane");
    $("#home").css('display', 'none');
    $("#profile").css('display', 'none');
    grid.trigger('reloadGrid');
}
