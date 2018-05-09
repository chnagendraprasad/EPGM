
$(document).ready(function () {

    var grid = jQuery('#Grid');
    jQuery('#Grid').jqGrid({
        url: urlContent + 'Users/GetInstructions',
        datatype: 'json',
        height: 190,
        hoverrows: false,
        shrinkToFit: false,
        autowidth: true,
        rowNum: 1000000,
        //scrollOffset: 0,
        jsonReader: { repeatitems: false },
        mtype: 'POST',
        cmTemplate: { sortable: false },
        beforeRequest: function () { },
        colNames: ['msgId', 'Message Desc', 'Usertype', 'Start Date', 'End Date', 'Edit', 'Delete'],
        colModel: [
                { name: 'msgId', index: 'msgId', width: 50, align: 'center', hidden: true },
                { name: 'MessageDesc', index: 'MessageDesc', width: 300 },
                { name: 'Usertype', index: 'Usertype', width: 70, hidden: true },
                { name: 'StartDate', index: 'StartDate', width: 130, formatter: 'date', formatoptions: { newformat: 'd/m/Y' } },
                { name: 'EndDate', index: 'EndDate', width: 130, formatter: 'date', formatoptions: { newformat: 'd/m/Y' } },
                { name: 'Edit', index: 'Edit', width: 80, align: 'center', formatter: EditPerson },
                { name: 'Delete', index: 'Delete', width: 80, align: 'center', formatter: DeletePerson }
        ],


        loadError: function (xhr, status, error) {
            var trf = $("#Grid tbody:first tr:first")[0];
            $("#Grid tbody:first").empty().append(trf);
        },
        loadComplete: function () {
            debugger;
            var count = grid.getRowData().length
            if (count <= 0) {
                $("#myModal").modal('show');
                $('#Grid').jqGrid("clearGridData");
            }
        }
    });

    $("#btnSubmit").click(function () {

        var frdate = $("#txtStart").val().substring(0, 2);
        var frmonth = $("#txtStart").val().substring(3, 5);
        var fryear = $("#txtStart").val().substring(6, 10);

        var FromDate = new Date(fryear, frmonth - 1, frdate);

        var tdate = $("#txtEnd").val().substring(0, 2);
        var tmonth = $("#txtEnd").val().substring(3, 5);
        var tyear = $("#txtEnd").val().substring(6, 10);

        var ToDate = new Date(tyear, tmonth - 1, tdate);


        if ($("#txtMessage").val() == "" || $("#txtMessage").val() == null || $("#txtMessage").val() == "0") {
            alert("Please Enter Message");
            $("#txtMessage").focus();
            return false;
        }
        else if ($("#ddlUser").val() == "" || $("#ddlUser").val() == null) {
            alert("Please Select UserType");
            $("#ddlUser").focus();
            return false;
        }
        else if ($("#txtStart").val() == "" || $("#txtStart").val() == null || $("#txtStart").val() == "0") {
            alert("Please Enter Start Date");
            $("#txtStart").focus();
            return false;
        }
        else if ($("#txtEnd").val() == "" || $("#txtEnd").val() == null || $("#txtEnd").val() == "0") {
            alert("Please Enter End Date");
            $("#txtEnd").focus();
            return false;
        }
        else if (FromDate > ToDate) {
            alert("Start Date should not be greater than End Date");
            $("#txtStart").focus();
            return false;
        }
        else {

            var obj = []
            var items = '';
            $('.SlectBox option:selected').each(function (i) {
                obj.push($(this).val());
                $('.SlectBox')[0].sumo.unSelectItem(i);
            });
            for (var i = 0; i < obj.length; i++) {
                items += ',' + obj[i]
            };
            //alert(items);

            var PostedData = {
                Msgid:$("#msgid").val(),
                Message: $("#txtMessage").val(),
                Role: items,
                StartDate: $("#txtStart").val(),
                EndDate: $("#txtEnd").val(),
                btnText: $("#btnSubmit").val()
            }

            showPleaseWait();
            $.ajax({
                type: "POST",
                url: '/Users/InstructionUpdate',
                data: JSON.stringify(PostedData),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: true,
                success: function (result) {
                    if (result.code = "000") {
                        hidePleaseWait();
                        alert(result.message);
                        ClearFields();
                        location.href = "/Users/InstructionsModule";
                    }
                    else {
                        alert(result.message);
                        ClearFields();
                        hidePleaseWait();
                    }
                },
                error: function (result) {
                    alert(result.message);
                    hidePleaseWait();
                }

            });
        }

    });

    $("#btnClear").click(function () {
        ClearFields();
    });

});

function DeletePerson(cellvalue, options, rowObject) {
    var Message = "'" + rowObject.MessageDesc + "'";
    var edit = '<input  class="Viewbtn"  style="background-color:#d9534f"  type="button" value="Delete" data-msgid=' + rowObject.msgId +
                                        ' data-Message=' + Message + ' data-StartDate=' + rowObject.StartDate +
                                        ' data-EndDate=' + rowObject.EndDate + ' onclick=\'DeleteMessage(this);\'  />';
    return edit;

}

function ClearFields() {
    $("#txtMessage").val("");
    $("#ddlUser").prop('selectedindex', 0);
    $("#txtStart").val("");
    $("#txtEnd").val("");
    $("#btnSubmit").attr('value', 'Submit');
}

function DeleteMessage(event) {
    if (confirm("do you want to delete this Instruction?")) {
        debugger;
        var resukt = event.dataset;
        showPleaseWait();

        var PostedData = {
            msgid: resukt.msgid,
            Message: resukt.message
        };
        $.post("/Users/DeleteMessage?msgid=" + resukt.msgid + "&Message=" + resukt.message, "", function (result) {
            debugger;
            if (result.Code == "000") {
                hidePleaseWait();
                alert(result.Message);
                ClearFields();
                $('#Grid').trigger('reloadGrid');
            }
            else {
                hidePleaseWait();
                alert(result.Message);
                ClearFields();
            }
        })
    }
}

function EditPerson(cellvalue, options, rowObject) {
    var Message = "'" + rowObject.MessageDesc + "'";
    var StartDate = "'" + rowObject.StartDate + "'";
    var EndDate = "'" + rowObject.EndDate + "'";
    var edit = '<input  class="Viewbtn"  style="background-color:#d9534f"  type="button" value="Edit" data-msgid=' + rowObject.msgId +
                                        ' data-Message=' + Message + ' data-StartDate=' + StartDate +
                                        ' data-EndDate=' + EndDate + ' data-userType=' + rowObject.Usertype + ' onclick=\'EditMessage(this);\'  />';
    return edit;
}


function EditMessage(event) {
    debugger;
    var resukt = event.dataset;
    var sdate = moment(resukt.startdate).format("DD/MM/YYYY");
    var edate = moment(resukt.enddate).format("DD/MM/YYYY");

    $("#txtMessage").val(resukt.message);
    $("#ddlUser").prop('selectedindex', 0);
    $("#txtStart").val(sdate);
    $("#txtEnd").val(edate);

    $("#msgid").val(resukt.msgid)

    $("#btnSubmit").attr('value', 'Update');
    //$("btnSubmit").text="Update";
}