
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
        else if ($('#ProjectID').val() == "0" || $('#ProjectID').val() == "" || $('#ProjectID').val() == null) {
            alert("Please select Project.")
            $('#ProjectID').focus();
            $('#Grid').jqGrid("clearGridData");
            ClearFields();
            return false;
        }
        else if ($('#SectorID').val() == "0" || $('#SectorID').val() == "" || $('#SectorID').val() == null) {
            alert("Please select Sector.")
            $('#SectorID').focus();
            $('#Grid').jqGrid("clearGridData");
            ClearFields();
            return false;
        }
        else if ($('#AWCID').val() == "0" || $('#AWCID').val() == "" || $('#AWCID').val() == null) {
            alert("Please select Sector.")
            $('#AWCID').focus();
            $('#Grid').jqGrid("clearGridData");
            ClearFields();
            return false;
        }
        else {
            BindGrid();
            $('#Grid').trigger('reloadGrid');
        }

    });

});


function BindGrid() {

    var grid = jQuery('#Grid');
    jQuery('#Grid').jqGrid({
        url: urlContent + 'Bene/GetBeneDataforEdit',
        datatype: 'json',
        height: 330,
        hoverrows: false,
        shrinkToFit: false,
        autowidth: true,
        rowNum: 1000000,
        //scrollOffset: 0,
        jsonReader: { repeatitems: false },
        mtype: 'GET',
        cmTemplate: { sortable: false },
        beforeRequest: function () {

            if ($('#StateID').val() == "") {
                alert("Please select State.")
                $('#StateID').focus();
                return false;
            }
            if ($('#DistrictID').val() == "") {
                alert("Please select District.")
                $('#DistrictID').focus();
                return false;
            }
            if ($('#ProjectID').val() == "" || $('#ProjectID').val() == null) {
                alert("Please select Project.")
                $('#ProjectID').focus();
                return false;
            }
            if ($('#SectorID').val() == "" || $('#SectorID').val() == null) {
                alert("Please Select Sector")
                $('#SectorID').focus();
                return false;
            }
            if ($('#AWCID').val() == "" || $('#AWCID').val() == null) {
                alert("Please Select AWC")
                $('#AWCID').focus();
                return false;
            }
            if ($('#BeneType').val() == "" || $('#BeneType').val() == null) {
                alert("Please Select BeneType")
                $('#BeneType').focus();
                return false;
            }
            var postData = grid.jqGrid('getGridParam', "postData");
            $.extend(postData, { "StateCode": $("#StateID option:selected").val(), "distCode": $("#DistrictID option:selected").val(), "projCode": $("#ProjectID option:selected").val(), "secCode": $("#SectorID option:selected").val(), "awcCode": $("#AWCID option:selected").val(), "BeneType": $("#BeneType option:selected").val(), "CenterType": $("#CenterType").val() });
        },
        colNames: ['Sno', 'Beneficiary Code', 'Name', 'Surname', 'Mother Name', 'Father Name', 'Gender', 'DOB', 'Aadhaar Type', 'Aadhaar No.', 'Mobile No.', 'RationCard No.', 'Income', 'Category', 'Registered DateTime', 'Resident Type', 'Actions', 'Is Registered at Attendance', 'Registered Attendance Date', 'Is Enrolled at Attendance', 'Enrolled Attendance Date', 'PersonID', 'Persisted FaceID', 'Delete Person face at Attendance', 'Delete Person at Attendance'],
        colModel: [
                { name: 'Sno', index: 'Sno', width: 50, align: 'center' },
                { name: 'BeneCode', index: 'Name', width: 70 },
                { name: 'BeneName', index: 'Name', width: 130 },
                { name: 'BeneSurname', index: 'Surname', width: 130 },
                { name: 'MotherName', index: 'MotherName', width: 130, align: 'left' },
                { name: 'FatherName', index: 'FatherName', width: 130, align: 'left' },
                { name: 'Gender', index: 'Gender', width: 60, align: 'left' },
                { name: 'DOB', index: 'DOB', width: 75, align: 'left' },
                { name: 'AadhaarType', index: 'AadhaarType', width: 60, align: 'left' },
                { name: 'AadhaarNumber', index: 'AadhaarNumber', width: 110, align: 'left' },
                { name: 'MobileNumber', index: 'MobileNumber', width: 90, align: 'left' },
                { name: 'FatherRationCardNumber', index: 'FatherRationCardNumber', width: 90, align: 'left' },
                { name: 'Income', index: 'Income', width: 50, align: 'left' },
                { name: 'BeneCategory', index: 'BeneCategory', width: 65, align: 'left' },
                { name: 'RegisteredDateTime', index: 'RegisteredDateTime', width: 80, align: 'left' },
                { name: 'ResidentType', index: 'ResidentType', width: 80, align: 'left' },
                { name: 'ActionId', index: 'ActionId', width: 70, align: 'center', formatter: actionFormatter },
                { name: 'IsRegisteredAtAttendance', index: 'IsRegisteredAtAttendance', width: 80, formatter: RegisterPerson, align: 'center' },
                { name: 'RegisteredAtAttendanceDate', index: 'RegisteredAtAttendanceDate', width: 160, align: 'center' },
                { name: 'IsEnrolledAtAttendance', index: 'IsEnrolledAtAttendance', formatter: EnrolledAttendance, width: 80, align: 'center' },
                { name: 'EnrolledAtAttendanceDate', index: 'EnrolledAtAttendanceDate', width: 160, align: 'center' },
                { name: 'PersonID', index: 'PersonID', width: 80, hidden: true, align: 'center' },
                { name: 'PersistedFaceID', index: 'PersistedFaceID', width: 80, hidden: true, align: 'center' },
                { name: 'DeletePersonFace', index: 'DeletePersonFace', width: 105, align: 'center', formatter: DeletePersonFace },
                { name: 'DeletePerson', index: 'DeletePerson', width: 80, align: 'center', formatter: DeletePerson }
        ],


        loadError: function (xhr, status, error) {
            var trf = $("#Grid tbody:first tr:first")[0];
            $("#Grid tbody:first").empty().append(trf);
        },
        loadComplete: function () {
             
            var count = grid.getRowData().length
            if (count <= 0) {
                $("#myModal").modal('show');
                $('#Grid').jqGrid("clearGridData");

            }
            DisableColumns();
        }
    });
}

function actionFormatter(cellvalue, options, rowObject) {
     
    if (rowObject.BeneName != null && rowObject.BeneName != "") {
        var BeneName = "'" + rowObject.BeneName + "'";
    }
    else {
        BeneName = "''";
    }
    if (rowObject.BeneSurname != null && rowObject.BeneSurname != "") {
        var SurName = "'" + rowObject.BeneSurname + "'";
    }
    else {
        SurName = "''";
    }
    if (rowObject.MotherName != null && rowObject.MotherName != "") {
        var MotherName = "'" + rowObject.MotherName + "'";
    }
    else {
        MotherName = "''";
    }
    if (rowObject.FatherName != null && rowObject.FatherName != "") {
        var FatherName = "'" + rowObject.FatherName + "'";
    }
    else {
        FatherName = "''";
    }

    if (rowObject.AadhaarNumber != null && rowObject.AadhaarNumber != "") {
        var AadhaarNumber = rowObject.AadhaarNumber;
    }
    else {
        AadhaarNumber = "''";
    }

    if (rowObject.MobileNumber != null && rowObject.MobileNumber != "") {
        var MobileNumber = rowObject.MobileNumber;
    }
    else {
        MobileNumber = "''";
    }

    if (rowObject.FatherRationCardNumber != null && rowObject.FatherRationCardNumber != "") {
        var FatherRationCardNumber = rowObject.FatherRationCardNumber;
    }
    else {
        FatherRationCardNumber = "''";
    }

    if (rowObject.Income != null && rowObject.Income != "") {
        var Income = rowObject.Income;
    }
    else {
        Income = "''";
    }
    var edit = '<input  class="Viewbtn" style="background-color: #5cb85c"  type="button" value="Edit" data-BeneCode=' + rowObject.BeneCode + ' data-BeneName=' + BeneName +
                       ' data-SurName=' + SurName + ' data-MotherName=' + MotherName +
                       ' data-FatherName=' + FatherName + ' data-Gender=' + rowObject.Gender +
                       ' data-DOB=' + rowObject.DOB + ' data-AadhaarType=' + rowObject.AadhaarType +
                       ' data-AadhaarNumber=' + AadhaarNumber + ' data-MobileNumber=' + MobileNumber +
                       ' data-RationNo=' + FatherRationCardNumber + ' data-Income=' + Income +
                       ' data-BeneCategory=' + rowObject.BeneCategory + ' data-Regdate=' + rowObject.RegisteredDateTime +
                       ' data-ResidentType=' + rowObject.ResidentType + ' onclick=\'BindData(this);\'  />';
    return edit;

}

function BindData(event) {
    debugger;
    var resukt = event.dataset;
    //date Picker
    var dobDate1 = "";
    if (resukt.dob == null || resukt.dob == "") {
        dobDate1 = "";
    }
    else {
        var dobDate = resukt.dob.split('-');
        var Day = dobDate[0];
        var Month = dobDate[1];
        var Year = dobDate[2];
        dobDate1 = Day + '/' + Month + '/' + Year;
    }
    //date Picker
    var RegisDate1 = "";
    if (resukt.regdate == null || resukt.regdate == "") {
        RegisDate1 = "";
    }
    else {
        var RegisDate = resukt.regdate.split('-');
        var RegisDay = RegisDate[0];
        var RegisMonth = RegisDate[1];
        var RegisYear = RegisDate[2];
        RegisDate1 = RegisDay + '/' + RegisMonth + '/' + RegisYear;
    }

    $("#BeneCode").val(resukt.benecode);
    $("#BeneName").val(resukt.benename);
    $("#BeneSurname").val(resukt.surname);
    $("#MotherName").val(resukt.mothername);
    $("#FatherName").val(resukt.fathername);
    $("#AadhaarNumber").val(resukt.aadhaarnumber);
    $("#MobileNumber").val(resukt.mobilenumber);
    $("#DOB").val(dobDate1);
    $("#FatherRationCardNumber").val(resukt.rationno);
    $("#Income").val(resukt.income);
    $("#RegisteredDateTime").val(RegisDate1);

    if (resukt.gender != "null" && resukt.gender != "") {
        var Gen = GetGender(resukt.gender);
        $("#Gender").val(Gen).attr("selected", "selected");
    }
    if (resukt.aadhaartype != "null" && resukt.aadhaartype != "") {
        var Aadhaar = GetAadhaar(resukt.aadhaartype);
        $("#AadhaarType").val(Aadhaar).attr("selected", "selected");
    }
    else {
        $("#AadhaarType").val("0").attr("selected", "selected");
    }
    if (resukt.benecategory != "null" && resukt.benecategory != "") {
        var Category = GetCategory(resukt.benecategory);
        $("#BeneCategory").val(Category).attr("selected", "selected");
    }
    else {
        $("#BeneCategory").val("0").attr("selected", "selected");
    }
    if (resukt.residenttype != "null" && resukt.residenttype != "") {
        var Resident = GetResident(resukt.residenttype);
        $("#ResidentType").val(Resident).attr("selected", "selected");
    }
    else {
        $("#ResidentType").val("0").attr("selected", "selected");
    }


    if ($("#BeneType").val() == "2" || $("#BeneType").val() == "3") {
        $("#lblFName").text("Name of Husband");
        $("#Gender").val('F');
        $("#Gender").prop("disabled", true);
        $("#redstar").css("display", "block");
        $("#divbirth").css("display", "none");
        $("#divedd").css("display", "block");
    }
    else {
        $("#lblFName").text("Name of Father");
        $("#redstar").css("display", "none");
        $("#Gender").prop("disabled", false);
        $("#divbirth").css("display", "block");
        $("#divedd").css("display", "none");
    }

    $('#conformMRSId').modal('show');

}

function GetGender(Gender) {
    if (Gender.toUpperCase() == "MALE")
    { return "M"; }
    else if (Gender.toUpperCase() == "FEMALE") {
        return "F";
    }
    else {
        return "0";
    }
}

function GetAadhaar(Aadhaar) {
    if (Aadhaar.toUpperCase() == "CHILD")
    { return "C"; }
    else if (Aadhaar.toUpperCase() == "FATHER") {
        return "F";
    }
    else if (Aadhaar.toUpperCase() == "MOTHER") {
        return "M";
    }
    else {
        return "0";
    }
}

function GetCategory(Category) {
    if (Category.toUpperCase() == "SC")
    { return "1"; }
    else if (Category.toUpperCase() == "ST") {
        return "2";
    }
    else if (Category.toUpperCase() == "BC") {
        return "3";
    }
    else if (Category.toUpperCase() == "OC") {
        return "4";
    }
    else if (Category.toUpperCase() == "Minority") {
        return "5";
    }
    else {
        return "0";
    }
}

function GetResident(ResType) {
    if (ResType.toUpperCase() == "TEMPORARY")
    { return "T"; }
    else if (ResType.toUpperCase() == "PERMANENT") {
        return "P";
    }
    else {
        return "0";
    }
}

function Submit() {
    debugger;
    if ($("#BeneName").val() == "" || $("#BeneName").val() == null) {
        alert("Please Enter Beneficiary Name");
        $("#BeneName").focus();
        return false;
    }
    //if ($("#BeneSurname").val() == "" || $("#BeneSurname").val() == null) {
    //    alert("Please Enter SurName");
    //    $("#BeneSurname").focus();
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
    else if ($("#AadhaarType").val() == "0" || $("#AadhaarType").val() == "" || $("#AadhaarType").val() == null) {
        alert("Please Select Aadhaar Holder");
        $("#AadhaarType").focus();
        return false;
    }
    else if ($("#AadhaarNumber").val() == "" || $("#AadhaarNumber").val() == null) {
        alert("Please Enter Aadhaar Card No.");
        $("#AadhaarNumber").focus();
        return false;
    }
    else if ($("#AadhaarNumber").val().length != "12") {
        alert("Please Enter 12 digit Aadhaar Card No.");
        $("#AadhaarNumber").focus();
        return false;
    }
    else if ($("#MobileNumber").val() == "" || $("#MobileNumber").val() == null) {
        alert("Please Enter Mobile No.");
        $("#ContactNo").focus();
        return false;
    }
    else if ($("#MobileNumber").val().substring(0, 1) == 0) {
        alert("Mobile No. should not start with zero");
        $("#MobileNumber").focus();
        return false;
    }

    else if ($("#MobileNumber").val().length != "10") {
        alert("Please Enter 10 digit Mobile No.");
        $("#MobileNumber").focus();
        return false;
    }
    else if ($("#BeneCategory").val() == "0" || $("#BeneCategory").val() == "" || $("#BeneCategory").val() == null) {
        alert("Please Select Category");
        $("#BeneCategory").focus();
        return false;
    }
    else if ($("#RegisteredDateTime").val() == "" || $("#RegisteredDateTime").val() == null) {
        alert("Please Enter Registration Date");
        $("#RegisteredDateTime").focus();
        return false;
    }
    else if ($("#ResidentType").val() == "0" || $("#ResidentType").val() == null) {
        alert("Please Select  Resident Type");
        $("#ResidentType").focus();
        return false;
    }
    else if ($("#DistrictID").val() == " " || $("#DistrictID").text() == "--Select District--") {
        alert("Please Select  District");
        $("#DistrictID").focus();
        return false;
    }
    else if ($("#ProjectID").val() == " " || $("#ProjectID").text() == "--Select Project--") {
        alert("Please Select  Project");
        $("#ProjectID").focus();
        return false;
    }
    else if ($("#SectorID").val() == " " || $("#SectorID").text() == "--Select Sector--") {
        alert("Please Select  Sector");
        $("#SectorID").focus();
        return false;
    }
    else if ($("#CenterID").val() == " " || $("#CenterID").text() == "--Select Center--") {
        alert("Please Select  Center");
        $("#CenterID").focus();
        return false;
    }
    else {
        var PostedData = {
            BeneCode: $("#BeneCode").val(),
            BeneName: $("#BeneName").val(),
            BeneSurname: $("#BeneSurname").val(),
            MotherName: $("#MotherName").val(),
            FatherName: $("#FatherName").val(),
            AadhaarType: $("#AadhaarType").val(),
            AadhaarNumber: $("#AadhaarNumber").val(),
            FatherRationCardNumber: $("#FatherRationCardNumber").val(),
            Income: $("#Income").val(),
            MobileNumber: $("#MobileNumber").val(),
            DOB: $("#DOB").val(),
            Gender: $("#Gender").val(),
            BeneCategory: $("#BeneCategory").val(),
            RegisteredDateTime: $("#RegisteredDateTime").val(),
            ResidentType: $("#ResidentType").val(),
            StateID: $("#StateID").val(),
            DistrictID: $("#DistrictID").val(),
            ProjectID: $("#ProjectID").val(),
            SectorID: $("#SectorID").val(),
            AWCID: $("#AWCID").val(),
            DeptUID: $("#DeptUID").val(),
            BirthWeight: $("#BirthWeight").val(),
            EDD: $("#EDD").val(),
            CenterType: $("#CenterType").val()
        }

        showPleaseWait();
        $.ajax({
            type: "POST",
            url: '/Bene/BeneEdit',
            data: JSON.stringify(PostedData),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            async: true,
            success: function (result) {

                if (result.Code == "000") {
                    hidePleaseWait();
                    alert(result.Message);
                    $('#conformMRSId').modal('hide');
                    ClearFields();
                    $('#Grid').trigger('reloadGrid');
                }
                else {
                    alert(result.Message);
                    hidePleaseWait();
                    $('#conformMRSId').modal('hide');
                    ClearFields();
                }
            },
            error: function (result) {
                $('#conformMRSId').modal('hide');
                ClearFields();
            }


        });
    }
}

function ClearFields() {

    $('#conformMRSId').modal('hide');
    $("#BeneName").val("");
    $("#BeneSurname").val("");
    $("#MotherName").val("");
    $("#FatherName").val("");
    $("#AadhaarType").prop('selectedIndex', 0);
    $("#AadhaarNumber").val("");
    $("#FatherRationCardNumber").val("");
    $("#Income").val("");
    $("#MobileNumber").val("");
    $("#DOB").val("");
    $("#Gender").prop('selectedIndex', 0);
    $("#BeneCategory").prop('selectedIndex', 0);
    $("#RegisDate").val("");
    $("#ResidentType").prop('selectedIndex', 0);
    $('#Grid').trigger('reloadGrid');
}

function RegisterPerson(cellvalue, options, rowObject) {
    if (rowObject.IsRegisteredAtAttendance == "N") {
        var BeneName = "'" + rowObject.BeneName + "'";
        var MotherName = "'" + rowObject.MotherName + "'";
        var edit = '<input  class="Viewbtn"  style="background-color:#337ab7" type="button" value="Create" data-BeneCode=' + rowObject.BeneCode + ' data-BeneName=' + BeneName +
               ' data-MName=' + MotherName + ' onclick=\'RegisterBene(this);\'  />';
        return edit;
    }
    else {
        return "Yes";
    }

}

function RegisterBene(event) {
     
    var resukt = event.dataset;
    showPleaseWait();

    var PostedData = {
        BeneCode: resukt.benecode,
        BeneName: resukt.benename,
        MotherName: resukt.mname,
        StateCode: $("#StateID option:selected").val(),
        DistrictCode: $("#DistrictID option:selected").val(),
        ProjectCode: $("#ProjectID option:selected").val(),
        SectorCode: $("#SectorID option:selected").val(),
        AWCCode: $("#AWCID option:selected").val()
    };
    $.post("/Bene/RegisterPerson", PostedData, function (result) {
         
        if (result.statusCode == 1) {
            hidePleaseWait();
            alert(result.message);
            ClearFields();
            $('#Grid').trigger('reloadGrid');
        }
        else {
            hidePleaseWait();
            alert(result.message);
            ClearFields();
        }
    })
}

function EnrolledAttendance(cellvalue, options, rowObject) {
    if (rowObject.IsRegisteredAtAttendance == "Y" && rowObject.IsEnrolledAtAttendance.trim() == "N") {
        //var BeneName = "'" + rowObject.BeneName + "'";
        //var MotherName = "'" + rowObject.MotherName + "'";
        //var edit = '<input  class="Viewbtn"  style="background-color:#337ab7" type="button" value="Create" data-BeneCode=' + rowObject.BeneCode + ' data-PersonID=' + rowObject.PersonID + ' onclick=\'EnrolledFace(this);\'  />';
        //return edit;
        return "No";
    }
    else if (rowObject.IsRegisteredAtAttendance == "N" && rowObject.IsEnrolledAtAttendance.trim() == "N") {
        return "No";
    }
    else {
        return "Yes";
    }

}

function EnrolledFace(event) {
     
    var resukt = event.dataset;
    showPleaseWait();

    var PostedData = {
        BeneCode: resukt.benecode,
        PersonID: resukt.personid,
        StateCode: $("#StateID option:selected").val(),
        DistrictCode: $("#DistrictID option:selected").val(),
        ProjectCode: $("#ProjectID option:selected").val(),
        SectorCode: $("#SectorID option:selected").val(),
        AWCCode: $("#AWCID option:selected").val()
    };
    $.post("/Bene/ErollPerson", PostedData, function (result) {
         
        if (result.statusCode == 1) {
            hidePleaseWait();
            alert(result.message);
            ClearFields();
            $('#Grid').trigger('reloadGrid');
        }
        else {
            hidePleaseWait();
            alert(result.message);
            ClearFields();
        }
    })
}

function DeletePersonFace(cellvalue, options, rowObject) {
    if (rowObject.IsRegisteredAtAttendance == "Y" && rowObject.IsEnrolledAtAttendance == "Y") {
        var PersonID = "'" + rowObject.PersonID + "'";
        var FaceID = "'" + rowObject.PersistedFaceID + "'";
        var edit = '<input  class="Viewbtn"  style="background-color:#d9534f"  type="button" value="Delete" data-BeneCode=' + rowObject.BeneCode + ' data-PersonID=' + rowObject.PersonID + ' data-FaceID=' + FaceID + ' onclick=\'DeleteBeneFace(this);\'  />';
        return edit;
    }
    else {
        return "";
    }

}

function DeleteBeneFace(event) {
     
    if (confirm("do you want to delete Person face from Attendance?")) {
        var resukt = event.dataset;
        showPleaseWait();

        var PostedData = {
            PersonID: resukt.personid,
            PersistedFaceID: resukt.faceid,
            BeneCode: resukt.benecode,
            StateCode: $("#StateID option:selected").val(),
            DistrictCode: $("#DistrictID option:selected").val(),
            ProjectCode: $("#ProjectID option:selected").val(),
            SectorCode: $("#SectorID option:selected").val(),
            AWCCode: $("#AWCID option:selected").val(),
        };
        $.post("/Bene/DeletePersonFace", PostedData, function (result) {
             
            if (result.statusCode == "000") {
                hidePleaseWait();
                alert(result.message);
                ClearFields();
                $('#Grid').trigger('reloadGrid');
            }
            else {
                hidePleaseWait();
                alert(result.message);
                ClearFields();
                $('#Grid').trigger('reloadGrid');
            }
        })
    }
}

function DeletePerson(cellvalue, options, rowObject) {
    if (rowObject.IsRegisteredAtAttendance == "Y" && rowObject.IsEnrolledAtAttendance == "Y") {
        var PersonID = "'" + rowObject.PersonID + "'";
        var FaceID = "'" + rowObject.PersistedFaceID + "'";
        var edit = '<input  class="Viewbtn"  style="background-color:#d9534f"  type="button" value="Delete" data-BeneCode=' + rowObject.BeneCode + ' data-PersonID=' + rowObject.PersonID + ' onclick=\'DeleteBeneSamrtAttendance(this);\'  />';
        return edit;
    }
    else {
        return "";
    }

}

function DeleteBeneSamrtAttendance(event) {
    if (confirm("do you want to delete Person from Attendance?")) {
         
        var resukt = event.dataset;
        showPleaseWait();

        var PostedData = {
            PersonID: resukt.personid,
            BeneCode: resukt.benecode,
            StateCode: $("#StateID option:selected").val(),
            DistrictCode: $("#DistrictID option:selected").val(),
            ProjectCode: $("#ProjectID option:selected").val(),
            SectorCode: $("#SectorID option:selected").val(),
            AWCCode: $("#AWCID option:selected").val(),
        };
        $.post("/Bene/DeletePerson", PostedData, function (result) {
             
            if (result.statusCode == "000") {
                hidePleaseWait();
                alert(result.message);
                ClearFields();
                $('#Grid').trigger('reloadGrid');
            }
            else {
                hidePleaseWait();
                alert(result.message);
                ClearFields();
            }
        })
    }
}