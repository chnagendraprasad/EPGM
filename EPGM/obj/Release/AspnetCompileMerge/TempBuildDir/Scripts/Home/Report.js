
$(document).ready(function () {
    debugger;
    if ('<%Session["UserTypeID"]%>' == "1") { 
        $("#DistrictID").val('<%Session["distcode"]%>');
        $("#DistrictID").prop("disabled", false);
        BindJsonDropDown(0, $('#DistrictID').val(), 'ProjectID')
        BindJsonDropDown(1, $('#ProjectID').val(), 'SectorID')
        BindJsonDropDown(2, $('#SectorID').val(), 'CenterID')
    }
    else if ('@Session["UserTypeID"]' == "2") {
        $("#DistrictID").val('@Session["distcode"].ToString()');
        $("#DistrictID").prop("disabled", true);
        BindJsonDropDown(0, $('#DistrictID').val(), 'ProjectID')
        BindJsonDropDown(1, $('#ProjectID').val(), 'SectorID')
        BindJsonDropDown(2, $('#SectorID').val(), 'CenterID')
    }
    else if ('@Session["UserTypeID"]' == "3") {
        $("#DistrictID").val('@Session["distcode"].ToString()');
        $("#DistrictID").prop("disabled", true);
        $("#ProjectID").val('@Session["projcode"].ToString()');
        $("#ProjectID").prop("disabled", true);
        BindJsonDropDown(1, $('#ProjectID').val(), 'SectorID')
        BindJsonDropDown(2, $('#SectorID').val(), 'CenterID')
    }
    else if ('@Session["UserTypeID"]' == "4") {
        $("#DistrictID").val('@Session["distcode"].ToString()');
        $("#DistrictID").prop("disabled", true);
        $("#ProjectID").val('@Session["projcode"].ToString()');
        $("#ProjectID").prop("disabled", true);
        $("#SectorID").val('@Session["projcode"].ToString()');
        $("#SectorID").prop("disabled", true);
        BindJsonDropDown(2, $('#SectorID').val(), 'CenterID')
    }


    var url = "/Report/MonthWiseDataComparison.aspx?statecode=" +@ViewBag.statecode + "&distcode=" + $("#DistrictID").val() + "&projectcode=" + " " + "&sectorcode=" + " " + "&centercode=" + " ";
    $('iframe#ifr').attr('src', url);

    $("#btnSubmit").click(function () {
        var url = "/Report/MonthWiseDataComparison.aspx?statecode=" +@ViewBag.statecode + "&distcode=" + $("#DistrictID").val() + "&projectcode=" + $("#ProjectID").val() + "&sectorcode=" + $("#SectorID").val() + "&centercode=" + $("#CenterID").val();
        $('iframe#ifr').attr('src', url);
    });
});

function BindJsonDropDown(typeID, ID, name) {
    debugger;

    if (typeID == 0) {
        BindJsonDropDown(1, $('#ProjectID').val(), 'SectorID')
        BindJsonDropDown(2, $('#SectorID').val(), 'CenterID')
        url = '@Url.Action("GetProjects", "Login")?DistCd=' + ID
    }
    if (typeID == 1) {
        // url = '/Registration/OnChangeBindDistrict?ID=' + ID;
        BindJsonDropDown(2, $('#SectorID').val(), 'CenterID')
        url = '@Url.Action("GetSector", "Login")?Distcode= ' + $('#DistrictID').val() + '&Project=' + ID + '&StateCode=52'
    }
    if (typeID == 2) {
        // url = '/Registration/OnChangeBindDistrict?ID=' + ID;
        url = '@Url.Action("GetCenters", "Login")?Seccode= ' + $('#SectorID').val() + '&Project=' + $('#ProjectID').val() + '&StateCode=52'
    }
    $.ajax({

        type: "GET",

        contentType: "application/json; charset=utf-8",

        url: url,

        data: "{}",

        dataType: "json",

        success: function (Result) {
            $("#" + name + "").empty()
            debugger;
            var opt = new Option("All", " ");
            $("#" + name + "").append(opt)
            for (var i = 0; i < Result.length; i++) {
                opt = new Option(Result[i].Text, Result[i].Value); $("#" + name + "").append(opt);
            }

        },

        error: function (Result) {

            alert("Error");

        }

    });
}