/// <reference path="../Common/helper.js" />
/// <reference path="../Common/_references.js" />

var currentPage = 1;

jQuery(document).ready(function () {
    $('#myModal').modal('hide');


    //$("#CenterID").change(function () {
    //    debugger;
    //    grid.trigger('reloadGrid');
    //});

    $("#btnprint").click(function () {
        debugger;
        myPrint("#Grid", "List of Beneficiaries");
    });

    $("#btnSubmit").click(function () {
        debugger;
        var date = new Date();
        if ($('#StateID').val() == " " || $('#StateID').val() == null) {
            alert("Please Select State")
            $('#StateID').focus();
            return false;
        }
        else if ($('#DistrictID').val() == " " || $('#DistrictID').val() == null) {
            alert("Please Select District")
            $('#DistrictID').focus();
            return false;
        }
        else if ($('#ProjectID').val() == " " || $('#ProjectID').val() == null) {
            alert("Please select Project")
            $('#ProjectID').focus();
            return false;
        }
        else if ($('#SectorID').val() == " " || $('#SectorID').val() == null) {
            alert("Please Select Sector")
            $('#SectorID').focus();
            return false;
        }
        else if ($('#CenterID').val() == " " || $('#CenterID').val() == null) {
            alert("Please Select AWC")
            $('#CenterID').focus();
            return false;
        }
        else if ($('#ddlMonth').val() == "0" || $('#ddlMonth').val() == null) {
            alert("Please Select Month")
            $('#ddlMonth').focus();
            return false;
        }
        else if (($('#ddlMonth').val() > date.getMonth() + 1) && ($('#txtYear').val() >= date.getFullYear())) {
            alert("Month Should not be greater than the Current Month");
            $('#ddlMonth').focus();
            return false;
        }
        else if ($('#txtYear').val() == "" || $('#txtYear').val() == null) {
            alert("Please Enter Year")
            $('#txtYear').focus();
            return false;
        }
        else if ($('#txtYear').val().length != "4") {
            alert("Please Enter Correct Year");
            $('#txtYear').focus();
            return false;
        }
        else if ($('#txtYear').val() > date.getFullYear()) {
            alert("Year Should not be greater than the Current Year");
            $('#txtYear').focus();
            return false;
        }
        else {
            BindGridData();
            var grid = jQuery('#Grid');
            grid.trigger('reloadGrid');
        }
    });

    $("#btnClear").click(function () {
        ClearFields();
    });

    function GetDataonChange() {
        var date = new Date();

        if ($('#DistrictID').val() == "") {
            alert("Please select upto center level.")
            $('#DistrictID').focus();
            return false;
        }
        else if ($('#ProjectID').val() == " " || $('#ProjectID').val() == null) {
            alert("Please select upto center level.")
            //alert("Please Select Project ")
            $('#ProjectID').focus();
            return false;
        }
        else if ($('#SectorID').val() == " " || $('#SectorID').val() == null) {
            alert("Please Select Sector")
            $('#SectorID').focus();
            return false;
        }
        else if ($('#CenterID').val() == " " || $('#CenterID').val() == null) {
            alert("Please Select Center")
            $('#CenterID').focus();
            return false;
        }
        else if ($('#ddlMonth').val() == "0" || $('#ddlMonth').val() == null) {
            alert("Please Select Month")
            $('#ddlMonth').focus();
            return false;
        }
        else if (($('#ddlMonth').val() > date.getMonth() + 1) && ($("#txtYear").val() >= date.getFullYear())) {
            alert("Month Should not be greater than the Current Month");
            $('#ddlMonth').focus();
            return false;
        }
        else if ($('#txtYear').val() == " " || $('#txtYear').val() == null) {
            alert("Please Enter Year")
            $('#txtYear').focus();
            return false;
        } else if ($("#txtYear").val().length != "4") {
            alert("Please Enter Correct Year");
            $("#txtYear").focus();
            return false;
        }
        else if ($("#txtYear").val() > date.getFullYear()) {
            alert("Year Should not be greater than the Current Year");
            $("#txtYear").focus();
            return false;
        }
        else {
            grid.trigger('reloadGrid');
        }
    }

    //$("#ddlMonth").change(function () {
    //    GetDataonChange();
    //});

    //$("#txtYear").change(function () {
    //    GetDataonChange();
    //});


    callbackFunction = function (status) {
        grid.trigger('reloadGrid');
    }

    $("#btnRefresh").click(function () {
        grid.trigger('reloadGrid');
    });

    $("#closbtn").click(function () {
        $('#myModal').modal('hide');
    });
});


function BindGridData() {
    debugger;
    var grid = jQuery('#Grid');
    grid.jqGrid({
        url: urlContent + 'Bene/MasterGridData',
        datatype: 'json',
        height: 330,
        hoverrows: false,
        shrinkToFit: false,
        autowidth: true,
        rowNum: 1000000,
        scrollOffset: 0,
        jsonReader: { repeatitems: false },
        mtype: 'GET',
        cmTemplate: { sortable: false },
        beforeRequest: function () {
            debugger;
            if ($('#StateID').val() == "0" || $('#StateID').val() == null) {
                alert("Please Select State")
                $('#StateID').focus();
                return false;
            }
            if ($('#DistrictID').val() == "0" || $('#DistrictID').val() == null) {
                alert("Please select District.")
                $('#DistrictID').focus();
                return false;
            }
            if ($('#ProjectID').val() == "0" || $('#ProjectID').val() == null) {
                alert("Please select Project.")
                $('#ProjectID').focus();
                return false;
            }
            if ($('#SectorID').val() == "0" || $('#SectorID').val() == null) {
                alert("Please Select Sector")
                $('#SectorID').focus();
                return false;
            }
            if ($('#CenterID').val() == "0" || $('#CenterID').val() == null) {
                alert("Please Select CenterID")
                $('#CenterID').focus();
                return false;
            }
            if ($('#ddlMonth').val() == "0" || $('#ddlMonth').val() == null) {
                alert("Please Select Month")
                $('#ddlMonth').focus();
                return false;
            }
            if ($('#txtYear').val() == "" || $('#txtYear').val() == null) {
                alert("Please Enter Year")
                $('#txtYear').focus();
                return false;
            }
            var postData = grid.jqGrid('getGridParam', "postData");
            $.extend(postData, { "StateCode": $("#StateID option:selected").val(), "distCode": $("#DistrictID option:selected").val(), "projCode": $("#ProjectID option:selected").val(), "secCode": $("#SectorID option:selected").val(), "awcCode": $("#CenterID option:selected").val(), "month": $("#ddlMonth").val(), "year": $("#txtYear").val() });
        },
        colNames: ['S.No', 'Beneficiary Code', 'Name', 'Surname', 'DOB', 'Gender', 'Age', 'Weight (Kgs)', 'Height (Cms)', 'Status', 'BMI', 'Active Status', 'Last Weighed Date & Time', 'Registered Status', 'Enrolled Status', 'Attendance Status', 'Attendance Timestamp', 'Address', 'WHImageURL', 'WHTimeStamp', 'WHAddress'],
        colModel: [
                { name: 'Sno', index: 'Sno', width: 40, align: 'center' },
                //{ name: 'BeneID', index: 'Name', width: 180, hidden: true },
                { name: 'BeneCode', index: 'Name', width: 130, formatter: BeneDet },
                { name: 'BeneName', index: 'Name', width: 160 },
                { name: 'BeneSurname', index: 'Surname', width: 140 },
                { name: 'DOB', index: 'DOB', width: 80, align: 'left' },
                { name: 'Gender', index: 'Gender', width: 60, align: 'left' },
                //{ name: 'BeneCategory', index: 'BeneCategory', width: 180, hidden: true },
                //{ name: 'BeneType', index: 'BeneType', width: 180, hidden: true },
                { name: 'AgeIntYears', index: 'AgeIntYears', width: 55, align: 'left' },
                { name: 'LatestWeight', index: 'LatestWeight', width: 55, align: 'right' },
                { name: 'LatestHeight', index: 'LatestHeight', width: 60, align: 'right' },
                { name: 'GradeName', index: 'GradeName', width: 60, align: 'right' },
                { name: 'LatestBMI', index: 'LatestBMI', width: 50, align: 'right' },
                { name: 'ActiveStatus', index: 'ActiveStatus', width: 60, align: 'left', formatter: statuscolor },
                { name: 'LastWeighedDate', index: 'LastWeighedDate', width: 160, align: 'right' },
                { name: 'IsRegisteredAtAttendance', index: 'IsRegisteredAtAttendance', width: 80, align: 'center', formatter: RegisteredStatus },
                { name: 'IsEnrolledAtAttendance', index: 'IsEnrolledAtAttendance', width: 60, align: 'center', formatter: EnrolledStatus },
                { name: 'AttendanceStatus', index: 'AttendanceStatus', width: 80, align: 'left', formatter: BeneURL },
                { name: 'WHTimeStamp', index: 'WHTimeStamp', width: 160, align: 'left', },
                { name: 'Address', index: 'Address', hidden: true, width: 100 },
                { name: 'WHImageURL', index: 'WHImageURL', hidden: true, width: 100 },
                { name: 'WHTimeStamp', index: 'WHTimeStamp', hidden: true, width: 100 },
                { name: 'WHAddress', index: 'WHAddress', hidden: true, width: 100 }],
        //{ name: 'WeightToAdd', index: 'WeightToAdd', width: 180, align: 'right', hidden: true },
        //{ name: 'Remarks', index: 'Remarks', width: 180, hidden: true }],

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
}

function showWHO(cellvalue, options, rowObject) {
    if (rowObject.LatestGrade == "NOR")
        return "Normal";
    if (rowObject.LatestGrade == "MUW")
        return "Moderate";
    if (rowObject.LatestGrade == "SUW")
        return "Severe";
    if (rowObject.LatestGrade == "ABS")
        return "Absent";
}

function statuscolor(cellvalue, options, rowObject) {
    if (rowObject.ActiveStatus == "InActive") {
        return '<span style="color:red">' + rowObject.ActiveStatus + '</span>';
    }
    else {
        return rowObject.ActiveStatus;
    }
}

function showGender(cellvalue, options, rowObject) {
    if (rowObject.Gender == "F")
        return "Female";
    if (rowObject.Gender == "M")
        return "Male";
}

function BeneDet(cellvalue, options, rowObject) {
    return "<a  href='" + urlContent + "Bene/BeneDetails/?beneCode=" + rowObject.BeneCode + "&distcode=" + $('#DistrictID').val() + "&distname=" + $('#DistrictID option:Selected').text() + "&Projectcode=" + $('#ProjectID').val() + "&projectname=" + $('#ProjectID option:Selected').text() + "&Sectorcode=" + $('#SectorID').val() + "&sectorname=" + $('#SectorID option:Selected').text() + "&awccode=" + $('#CenterID').val() + "&awcname=" + $('#CenterID option:Selected').text() + "&Month=" + $('#ddlMonth').val() + "&Year=" + $('#txtYear').val() + "'style='text-decoration: underline !important;color: black'>" + rowObject.BeneCode + "</a>";
    //return "<a href='" + urlContent + "Bene/BeneDetails/?beneCode=" + rowObject.BeneCode + "&distname=" + $('#DistrictID option:Selected').text() + "&projectname=" + $('#ProjectID option:Selected').text() + "&sectorname=" + $('#SectorID option:Selected').text() + "&awcname=" + $('#CenterID option:Selected').text() + "'style='color: black'>" + rowObject.BeneCode + "</a>";
}

function BeneURL(cellvalue, options, rowObject) {
    debugger;
    if (rowObject.AttendanceStatus == "Present") {
        //return "<a  href='" + rowObject.AttendanceImageURL + "'target='_blank' style='text-decoration: underline !important;color: black'>" + rowObject.AttendanceStatus + "</a>";
        //var Address = '"' + rowObject.Address + '"';
        //var AttendanceTimeStamp = '"' + rowObject.AttendanceTimeStamp + '"';
        var WHAddress = '"' + rowObject.WHAddress + '"';
        var WHAttendanceTimeStamp = '"' + rowObject.WHTimeStamp + '"';

        var edit = '<input  class="Viewbtn"  type="button" value=' + rowObject.AttendanceStatus + ' data-AttendanceImageURL=' + rowObject.WHImageURL
                                                                   + ' data-AttendanceTimeStamp=' + WHAttendanceTimeStamp
                                                                   + ' data-Address=' + WHAddress + ' onclick=\'BindData(this);\'  />';
        return edit;
    }
    else {
        return rowObject.AttendanceStatus;
    }
}

function BeneWHURL(cellvalue, options, rowObject) {
    debugger;
    if (rowObject.LastWeighedDate != "" && rowObject.LastWeighedDate !== null && rowObject.WHImageURL != "" && rowObject.WHImageURL !== null) {
        var WHAddress = '"' + rowObject.WHAddress + '"';
        var WHAttendanceTimeStamp = '"' + rowObject.WHTimeStamp + '"';
        var edit1 = "<a href='#' Style='text-decoration: underline !important;color: black' value=" + rowObject.LastWeighedDate
                                  + ' data-WHTimeStamp=' + WHAttendanceTimeStamp
                                  + ' data-WHImageURL=' + rowObject.WHImageURL
                                  + ' data-WHAddress=' + WHAddress + " onclick=\'BindDataWH(this);\'>" + rowObject.LastWeighedDate + "</a>"
        return edit1;
    }
    else {
        return rowObject.LastWeighedDate;
    }
}

function BeneAddress(cellvalue, options, rowObject) {
    return "<div title='" + rowObject.Address + "'/div>" + rowObject.EnrolledStatus;
}

function RegisteredStatus(cellvalue, options, rowObject) {
    if (rowObject.IsRegisteredAtAttendance == "Y") {
        return '<label style="color:green">Yes</label>'
    }
    else {
        return '<label style="color:red">No</label>'
    }
}
function EnrolledStatus(cellvalue, options, rowObject) {
    if (rowObject.IsEnrolledAtAttendance == "Y") {
        return '<label style="color:green">Yes</label>'
    }
    else {
        return '<label style="color:red">No</label>'
    }
}

function BindData(event) {
    debugger;
    $("#imgpopup").prop('src', "");
    $("#headTitle").text("");
    $("#headTitletime").text("");
    var resukt = event.dataset;
    $("#imgpopup").prop('src', resukt.attendanceimageurl);
    $("#headTitletime").text(resukt.attendancetimestamp);
    $("#headTitle").text(resukt.address);
    $('#myModal').modal('show');
}


function BindDataWH(event) {
    debugger;
    $("#imgpopup").prop('src', "");
    $("#headTitle").text("");
    $("#headTitletime").text("");
    var resukt = event.dataset;
    $("#imgpopup").prop('src', resukt.whimageurl);
    $("#headTitletime").text(resukt.whtimestamp);//
    $("#headTitle").text(resukt.whaddress); //
    $('#myModal').modal('show');
}