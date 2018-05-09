
$(document).ready(function () {
    $("#DistrictID").append(new Option("--Select District--", "0"));
    $("#ProjectID").append(new Option("--Select Project--", "0"));
    $("#SectorID").append(new Option("--Select Sector--", "0"));
    $("#CenterID").append(new Option("--Select Center--", "0"));

    $("#btnSubmit").click(function () {
        debugger;

        if ($("#Center").val() == "" || $("#Center").val() == null || $("#Center").val() == "0") {
            alert("Please Enter AWC Name");
            $("#Center").focus();
            return false;
        }
        else if ($("#MobileNo").val() == "" || $("#MobileNo").val() == null) {
            alert("Please Enter Mobile No.");
            $("#MobileNo").focus();
            return false;
        }
        else if ($("#MobileNo").val().substring(0, 1) == 0) {
            alert("Mobile No. should not start with zero");
            $("#MobileNo").focus();
            return false;
        }
        else if ($("#MobileNo").val().length != "10") {
            alert("Please Enter 10 digit Mobile No.");
            $("#MobileNo").focus();
            return false;
        }
        else if ($("#Email").val() == "" || $.trim($("#Email").val()).length == 0) {
            alert('Please enter valid email address');
            $("#Email").focus();
            return false;
        }
        else if (!validateEmail($("#Email").val())) {
            alert('Invalid Email Address');
            $("#Email").focus();
            return false;
        }
        else if ($("#Address").val() == "" || $("#Address").val() == "0" || $("#Address").val() == null) {
            alert('Please Enter Address');
            $("#Address").focus();
            return false;
        }
        else if (($("#AWCType").val() == "0" || $("#AWCType option:selected").text() == "--Select AWC Type--")) {
            alert("Please Select  AWC Type");
            $("#AWCType").focus();
            return false;
        }
        else if (($("#StateID").val() == "0" || $("#StateID option:selected").text() == "--Select State--")) {
            alert("Please Select  State");
            $("#StateID").focus();
            return false;
        }
        else if (($("#DistrictID").val() == "0" || $("#DistrictID").text() == "--Select District--")) {
            alert("Please Select  District");
            $("#DistrictID").focus();
            return false;
        }
        else if (($("#ProjectID").val() == "0" || $("#ProjectID").text() == "--Select Project--")) {
            alert("Please Select  Project");
            $("#ProjectID").focus();
            return false;
        }
        else if (($("#SectorID").val() == "0" || $("#SectorID").text() == "--Select Sector--")) {
            alert("Please Select  Sector");
            $("#SectorID").focus();
            return false;
        }
        else {

            $("#lblawc").text($("#Center").val());
            $("#lblmobile").text($("#MobileNo").val());
            $("#lblmail").text($("#Email").val());
            $("#lbladdress").text($("#Address").val());
            $("#lblawctype").text($("#AWCType option:selected").text());
            $("#lblState").text($("#StateID option:selected").text());
            $("#lblDistrict").text($("#DistrictID option:selected").text());
            $("#lblProject").text($("#ProjectID option:selected").text());
            $("#lblSector").text($("#SectorID option:selected").text());
            $('#conformMRSId').modal('show');
        }
    });

    $("#btnClear").click(function () {
        ClearFields();
    });

});

function validateEmail(email) {
    debugger;
    var regex = /^[\w\-\.\+]+\@[a-zA-Z0-9\.\-]+\.[a-zA-z0-9]{2,4}$/;
    var Result = regex.test(email);
    return Result;
}

function IsNumeric(e) {
    var keyCode = e.which ? e.which : e.keyCode
    var ret = ((keyCode >= 48 && keyCode <= 57));
    return ret;
}


function Submit() {
    debugger;
    var PostedData = {
        Center: $("#Center").val(),
        MobileNo: $("#MobileNo").val(),
        Email: $("#Email").val(),
        Address: $("#Address").val(),
        AWCType: $("#AWCType").val(),
        StateID: $("#StateID").val(),
        DistrictID: $("#DistrictID").val(),
        ProjectID: $("#ProjectID").val(),
        SectorID: $("#SectorID").val()
    }

    $.ajax({
        type: "POST",
        url: '/AWC/CreateAWC',
        data: JSON.stringify(PostedData),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: true,
        success: function (result) {
            debugger;
            if (result.Code == "000") {
                alert(result.Message + result.AWCCode);
                ClearFields();
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