

$(document).ready(function () {
    $("#btnSubmit").click(function () {
        debugger;
        if ($("#StaffCode").val() == "" || $("#StaffCode").val() == null || $("#StaffCode").val() == "0") {
            alert("Please Enter User Name");
            $("#StaffCode").focus();
            return false;
        }

        if ($("#StateId").val() == "" || $("#StateId").val() == null || $("#StateId").val() == "0") {
            alert("Please Select State");
            $("#StateId").focus();
            return false;
        }

        if ($("#Qualification").val() == "" || $("#Qualification").val() == null || $("#Qualification").val() == "0") {
            alert("Please Enter Qualification");
            $("#Qualification").focus();
            return false;
        }

        if ($("#Name").val() == "" || $("#Name").val() == null) {
            alert("Please Enter First Name");
            $("#Name").focus();
            return false;
        }
        if ($("#MiddleName").val() == "" || $("#MiddleName").val() == null) {
            alert("Please Enter Middle Name");
            $("#MiddleName").focus();
            return false;
        }
        if ($("#SurName").val() == "" || $("#SurName").val() == null) {
            alert("Please Enter Sur Name");
            $("#SurName").focus();
            return false;
        }
        else if ($("#DOB").val() == "" || $("#DOB").val() == null) {
            alert("Please Enter Date of Birth");
            $("#DOB").focus();
            return false;
        }
        else if ($("#Gender").val() == "0" || $("#Gender").val() == "" || $("#Gender").val() == null) {
            alert("Please Select Gender");
            $("#Gender").focus();
            return false;
        }
        else if ($("#DOJ").val() == "" || $("#DOJ").val() == null) {
            alert("Please Enter Date of Joining");
            $("#DOJ").focus();
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
        else if ($("#Email").val() != "" || $.trim($("#Email").val()).length > 0) {

            alert($("#Email").val());
            alert($("#Email").val().length);
            
            
         if (!validateEmail($("#Email").val())) {
            alert('Invalid Email Address');
            $("#Email").focus();
            return false;
        }
            }
        else if ($("#Address").val() == "" || $("#Address").val() == null) {
            alert("Please Enter Address");
            $("#Address").focus();
            return false;
        }
        else {
            $("#lblStaffCode").text($("#StaffCode").val());
            $("#lblFirstrName").text($("#Name").val().toUpperCase());
            $("#lblMiddleName").text($("#MiddleName").val());
            $("#lblGender").text($("#Gender option:selected").text());
            $("#lblSurName").text($("#SurName").val().toUpperCase());
            $("#lblDob").text($("#DOB").val());
            $("#lblDoj").text($("#DOJ").val());
            $("#lblMobileNo").text($("#MobileNo").val());
            $("#lblEmail").text($("#Email").val());
            $("#lblAddress").text($("#Address").val());
            $('#conformMRSId').modal('show');
        }

    });


    $("#btnClear").click(function () {
        ClearFields();
    });
});

//Checking User Exist Or Not
function CheckUser() {
    
    if ($("#StaffCode").val() == "" || $("#StaffCode").val() == "0" || $("#StaffCode").val() == null) {
        alert("Please Enter UserName");
        $("#StaffCode").focus();
        return false;
    }
    else {
        $.get("/Login/CheckUserFF", { UserName: $("#StaffCode").val() }, function (result) {
            if (result == "1") {
                alert("User already existed with this UserName");
                $("#StaffCode").val("");
                $("#StaffCode").focus();
                return false;
            }
            else {
                alert("UserName Available for Creation");
            }
        });
    }
}

function readPhoto(input) {

    var filesize = document.getElementById('UploadPhoto').files[0].size;

    if (filesize < 1000000) {
        if (input.files && input.files[0]) {

            var reader = new FileReader();
            reader.onload = function (e) {
                //   alert(e.target.result);
                $('#imgPhoto').attr('src', e.target.result);
            }
            reader.readAsDataURL(input.files[0]);
        }
    }
}

function Submit() {
    debugger;
    var PostedData = {
        StaffCode: $("#StaffCode").val(),
        Name: $("#Name").val(),
        SurName: $("#SurName").val(),
        MiddleName: $("#MiddleName").val(),
        DOB: $("#DOB").val(),
        Gender: $("#Gender").val(),
        DOJ: $("#DOJ").val(),
        MobileNo: $("#MobileNo").val(),
        Email: $("#Email").val(),
        Address: $("#Address").val()
    }

    showPleaseWait();

    var form = $('#form')[0];

    var data = new FormData(form);

    $.ajax({
        type: "POST",
        url: '/FieldForce/InsertFieldForceStaff',
        //data: JSON.stringify(PostedData),
        data : data,
        enctype: 'multipart/form-data',
        processData: false,  // Important!
        contentType: false,
        cache: false,
        success: function (result) {
            debugger;
            if (result.statusCode == "000") {
                hidePleaseWait();
                alert(result.message);
                ClearFields();
            }
            else {
                hidePleaseWait();
                alert(result.message);
                ClearFields();
            }
        },
        error: function (result) { }


    });
}