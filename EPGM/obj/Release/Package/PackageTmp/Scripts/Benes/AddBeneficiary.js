

$(document).ready(function () {


    $("#btnSubmit").click(function () {
        debugger;
        if ($("#BeneType").val() == "" || $("#BeneType").val() == null || $("#BeneType").val() == "0") {
            alert("Please Select BeneType");
            $("#BeneType").focus();
            return false;
        }
        if ($("#ChildName").val() == "" || $("#ChildName").val() == null) {
            alert("Please Enter Beneficiary Name");
            $("#ChildName").focus();
            return false;
        }
        if ($("#ConfirmChildName").val() == "" || $("#ConfirmChildName").val() == null) {
            alert("Please Enter SurName");
            $("#ConfirmChildName").focus();
            return false;
        }
            //else if ($("#ConfirmChildName").val() == "" || $("#ConfirmChildName").val() == null) {
            //    alert("Please Enter Confirm Child Name");
            //    $("#ConfirmChildName").focus();
            //    return false;
            //}
            //else if ($("#ChildName").val().toUpperCase() != $("#ConfirmChildName").val().toUpperCase()) {
            //    alert("ChildName and Confirm ChildName Should be Same");
            //    $("#ConfirmChildName").focus();
            //    return false;
            //}
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
        else if ($("#MotherName").val() == "" || $("#MotherName").val() == null) {
            alert("Please Enter Mother Name");
            $("#MotherName").focus();
            return false;
        }
        else if (($("#FatherName").val() == "" || $("#FatherName").val() == null) && $("#BeneType").val() != "1") {
            alert("Please Enter Husband Name");
            $("#FatherName").focus();
            return false;
        }
        else if ($("#AadhaarCardType").val() == "0" || $("#AadhaarCardType").val() == "" || $("#AadhaarCardType").val() == null) {
            alert("Please Select Aadhaar Holder");
            $("#AadhaarCardType").focus();
            return false;
        }
        else if ($("#AadhaarNo").val() == "" || $("#AadhaarNo").val() == null) {
            alert("Please Enter Aadhaar Card No.");
            $("#AadhaarNo").focus();
            return false;
        }
        else if ($("#AadhaarNo").val().length != "12") {
            alert("Please Enter 12 digit Aadhaar Card No.");
            $("#AadhaarNo").focus();
            return false;
        }
            //else if ($("#RationCardNo").val() == "" || $("#RationCardNo").val() == null) {
            //    alert("Please Enter Ration Card No.");
            //    $("#RationCardNo").focus();
            //    return false;
            //}
            //else if ($("#RationCardNo").val().length!="20") {
            //    alert("Please Enter Ration Card No.");
            //    $("#RationCardNo").focus();
            //    return false;
            //}
        else if ($("#ContactNo").val() == "" || $("#ContactNo").val() == null) {
            alert("Please Enter Mobile No.");
            $("#ContactNo").focus();
            return false;
        }
        else if ($("#ContactNo").val().substring(0, 1) == 0) {
            alert("Mobile No. should not start with zero");
            $("#ContactNo").focus();
            return false;
        }

        else if ($("#ContactNo").val().length != "10") {
            alert("Please Enter 10 digit Mobile No.");
            $("#ContactNo").focus();
            return false;
        }
        else if ($("#Category").val() == "0" || $("#Category").val() == "" || $("#Category").val() == null) {
            alert("Please Select Category");
            $("#Category").focus();
            return false;
        }
        else if ($("#RegisDate").val() == "" || $("#RegisDate").val() == null) {
            alert("Please Enter Registration Date");
            $("#RegisDate").focus();
            return false;
        }
        else if ($("#ResidentType").val() == "0" || $("#ResidentType").val() == null) {
            alert("Please Select  Resident Type");
            $("#ResidentType").focus();
            return false;
        }
        else if ($("#StateID").val() == "0" || $("#StateID option:selected").text() == "--Select District--") {
            alert("Please Select  State");
            $("#StateID").focus();
            return false;
        }
        else if ($("#DistrictID").val() == "0" || $("#DistrictID option:selected").text() == "--Select District--") {
            alert("Please Select  District");
            $("#DistrictID").focus();
            return false;
        }
        else if ($("#ProjectID").val() == "0" || $("#ProjectID option:selected").text() == "--Select Project--") {
            alert("Please Select  Project");
            $("#ProjectID").focus();
            return false;
        }
        else if ($("#SectorID").val() == "0" || $("#SectorID option:selected").text() == "--Select Sector--") {
            alert("Please Select  Sector");
            $("#SectorID").focus();
            return false;
        }
        else if ($("#CenterID").val() == "0" || $("#CenterID option:selected").text() == "--Select AWC--") {
            alert("Please Select  Center");
            $("#CenterID").focus();
            return false;
        }
        else {
            //ajaxPostData("/Bene/InsertBeneficiary", "#BeneSave", function () { })
            $("#lblBenetype").text($("#BeneType option:selected").text());
            $("#lblChild").text($("#ChildName").val().toUpperCase());
            $("#lblSurName").text($("#ConfirmChildName").val().toUpperCase());
            $("#lblDOB").text($("#DOB").val());
            $("#lblGender").text($("#Gender option:selected").text());
            $("#lblMother").text($("#MotherName").val().toUpperCase());
            $("#lblFather").text($("#FatherName").val().toUpperCase());
            $("#lblAadhaarHolder").text($("#AadhaarCardType option:selected").text());
            $("#lblAadhaarNo").text($("#AadhaarNo").val());
            $("#lblRationCardNo").text($("#RationCardNo").val());
            $("#lblIncome").text($("#IncomeRange").val());
            $("#lblMobile").text($("#ContactNo").val());
            $("#lblCategory").text($("#Category option:selected").text());
            $("#lblRegDate").text($("#RegisDate").val());
            $("#lblResident").text($("#ResidentType option:selected").text());
            $("#lbluid").text($("#DeptUID").val());
            $("#lblweight").text($("#BirthWeight").val());
            $("#lblDDate").text($("#EDD").val());
            $("#lblState").text($("#StateID option:selected").text());
            $("#lblDistrict").text($("#DistrictID option:selected").text());
            $("#lblProject").text($("#ProjectID option:selected").text());
            $("#lblSector").text($("#SectorID option:selected").text());
            
            $("#lblCenter").text($("#CenterID option:selected").text());
            $("#lblRegType").text($("#CenterType option:selected").text());
            $('#conformMRSId').modal('show');
        }

    });


    $("#btnClear").click(function () {
        ClearFields();
    });
});


function Submit() {
    debugger;
    var ContactNo = $("#ContactNo").val();
    var PostedData = {
        BeneType: $("#BeneType").val(),
        ChildName: $("#ChildName").val(),
        ConfirmChildName: $("#ConfirmChildName").val(),
        MotherName: $("#MotherName").val(),
        FatherName: $("#FatherName").val(),
        AadhaarCardType: $("#AadhaarCardType").val(),
        AadhaarNo: $("#AadhaarNo").val(),
        RationCardNo: $("#RationCardNo").val(),
        IncomeRange: $("#IncomeRange").val(),
        ContactNo: $("#ContactNo").val(),
        DOB: $("#DOB").val(),
        Gender: $("#Gender").val(),
        Category: $("#Category").val(),
        RegisDate: $("#RegisDate").val(),
        ResidentType: $("#ResidentType").val(),
        StateID: $("#StateID").val(),
        DistrictID: $("#DistrictID").val(),
        ProjectID: $("#ProjectID").val(),
        SectorID: $("#SectorID").val(),
        CenterID: $("#CenterID").val(),
        CenterType: $("#CenterType").val()
    }

    showPleaseWait();
    $.ajax({
        type: "POST",
        url: '/Bene/InsertBeneficiary',
        data: JSON.stringify(PostedData),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: true,
        success: function (result) {
            debugger;
            if (result.statusCode == "000") {
                hidePleaseWait();
                alert(result.message);
                ClearFields();
            }
            else {
                alert(result.message);
                ClearFields();
            }
        },
        error: function (result) { }


    });
}