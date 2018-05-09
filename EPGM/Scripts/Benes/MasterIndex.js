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
       // debugger;
        myPrint("#Grid", "List of Beneficiaries");
    });

    $("#btnSubmit").click(function () {
      //  debugger;
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
   // debugger;
    var grid = jQuery('#Grid');
    grid.jqGrid({
        url: urlContent + 'Bene/MasterGridData',
        datatype: 'json',
        height: 300,
        hoverrows: false,
        shrinkToFit: false,
        autowidth: true,
        rowNum: 1000000,
        scrollOffset: 0,
        jsonReader: { repeatitems: false },
        mtype: 'GET',
        loadonce: true,
        cmTemplate: { sortable: true },
        beforeRequest: function () {
          //  debugger;
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
            if (($('#ProjectID').val() == "0" || $('#ProjectID').val() == null) && ($.session.get('postback') != '1')) {
                alert("Please select Project.")
                $('#ProjectID').focus();
                return false;
            }
            if (($('#SectorID').val() == "0" || $('#SectorID').val() == null) && ($.session.get('postback') != '1')) {
                alert("Please Select Sector")
                $('#SectorID').focus();
                return false;
            }
            if (($('#CenterID').val() == "0" || $('#CenterID').val() == null) && ($.session.get('postback') != '1')) {
                alert("Please Select CenterID")
                $('#CenterID').focus();
                return false;
            }
            if ($('#BeneType').val() == "0" || $('#BeneType').val() == null) {
                alert("Please Select BeneType")
                $('#BeneType').focus();
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
            $.extend(postData, { "StateCode": $("#StateID option:selected").val(), "distCode": $("#DistrictID option:selected").val(), "projCode": $("#ProjectID option:selected").val(), "secCode": $("#SectorID option:selected").val(), "awcCode": $("#CenterID option:selected").val(), "month": $("#ddlMonth").val(), "year": $("#txtYear").val(), "BeneType": $("#BeneType option:selected").val(), CenterType: $("#CenterType").val() });
        },
        colNames: ['S.No', 'Beneficiary Code', 'Name', 'Surname', 'DOB', 'Gender', 'Age', 'Weight (Kgs)', 'Height (Cms)', 'Status', 'BMI', 'Active Status', 'Last Weighed Date & Time', 'Registered Status', 'Enrolled Status', 'Face Recognition Status', 'Face Recognition Timestamp', 'Hemoglobin', 'HBGrade','MUAC Tape ','MUGrade', 'Pulse Oximeter', 'Address', 'WHImageURL', 'WHTimeStamp', 'WHAddress'],
        colModel: [
                { name: 'Sno', index: 'Sno', align: 'center', sortable: true, sorttype: 'number',width:50 },
                //{ name: 'BeneID', index: 'Name', width: 180, hidden: true },
                { name: 'BeneCode', index: 'Name', formatter: BeneDet, sortable: false,width:120},
                { name: 'BeneName', index: 'Name', width: 120},
                { name: 'BeneSurname', index: 'Surname', width: 80 },
                { name: 'DOB', index: 'DOB', align: 'left', formatter: "date", width: 80, formatoptions: { srcformat: 'd/m/Y', newformat: 'd/m/Y' }, sorttype: "date", },
                { name: 'Gender', index: 'Gender', align: 'left', width: 60},
                //{ name: 'BeneCategory', index: 'BeneCategory', width: 180, hidden: true },
                //{ name: 'BeneType', index: 'BeneType', width: 180, hidden: true },
                { name: 'AgeIntYears', index: 'AgeIntYears', align: 'left', width: 50 },
                { name: 'LatestWeight', index: 'LatestWeight', align: 'right', width: 50 },
                { name: 'LatestHeight', index: 'LatestHeight', align: 'right', width: 50 },
                { name: 'GradeName', index: 'GradeName', align: 'right', sortable: true, formatter: GradeColor, width: 60 },
                { name: 'LatestBMI', index: 'LatestBMI', align: 'right', width: 60 },
                { name: 'ActiveStatus', index: 'ActiveStatus', align: 'left', formatter: statuscolor, width: 60 },
                { name: 'LastWeighedDate', index: 'LastWeighedDate', align: 'right', width: 100 },
                { name: 'IsRegisteredAtAttendance', index: 'IsRegisteredAtAttendance', align: 'center', sortable: true, formatter: RegisteredStatus, width: 80 },
                { name: 'IsEnrolledAtAttendance', index: 'IsEnrolledAtAttendance', align: 'center', sortable: true, formatter: EnrolledStatus, width: 80 },
                { name: 'AttendanceStatus', index: 'AttendanceStatus', align: 'left', sortable: true, formatter: BeneURL, width: 60 },
                { name: 'WHTimeStamp', index: 'WHTimeStamp', align: 'left', sorttype: "date", width: 100 },
                { name: 'HBReading', index: 'HBReading', align: 'center', sorttype: "number", formatter: HBRedingStatus, width: 80 },
                { name: 'HBGrade', index: 'HBGrade', hidden: true, width: 80 },
                { name: 'MUReading', index: 'MUReading', align: 'center', sorttype: "number", formatter: MURedingStatus, width: 60 },
                { name: 'MUGrade', index: 'MUGrade', hidden: true, width: 60 },
                { name: 'OXPulseReading', index: 'OXPulseReading', align: 'center', sorttype: "number", formatter: OXPulseRedingStatus, width: 80 },
                { name: 'Address', index: 'Address', hidden: true},
                { name: 'WHImageURL', index: 'WHImageURL', hidden: true},
                { name: 'WHTimeStamp', index: 'WHTimeStamp', hidden: true},
                { name: 'WHAddress', index: 'WHAddress', hidden: true }],
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
    $("#Grid").setGridParam({ datatype: 'json', page: 1 }).trigger('reloadGrid');
    //$("#Grid").jqGrid("setLabel", "rn", "S.No");
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
    return "<a  href='" + urlContent + "Bene/BeneDetails/?beneCode=" + rowObject.BeneCode + "&StateCode=" + $('#StateID').val() + "&StateName=" + $('#StateID option:Selected').text() +
                        "&distcode=" + $('#DistrictID').val() + "&distname=" + $('#DistrictID option:Selected').text() +
                        "&Projectcode=" + $('#ProjectID').val() + "&projectname=" + $('#ProjectID option:Selected').text() +
                        "&Sectorcode=" + $('#SectorID').val() + "&sectorname=" + $('#SectorID option:Selected').text() +
                        "&awccode=" + $('#CenterID').val() + "&awcname=" + $('#CenterID option:Selected').text() +
                        "&Month=" + $('#ddlMonth').val() + "&Year=" + $('#txtYear').val() + "&BeneType=" + $('#BeneType option:Selected').val() + "&CenterType=" + $('#CenterType').val() +
                        "'style='text-decoration: underline !important;color: black'>" + rowObject.BeneCode + "</a>";
    //return "<a href='" + urlContent + "Bene/BeneDetails/?beneCode=" + rowObject.BeneCode + "&distname=" + $('#DistrictID option:Selected').text() + "&projectname=" + $('#ProjectID option:Selected').text() + "&sectorname=" + $('#SectorID option:Selected').text() + "&awcname=" + $('#CenterID option:Selected').text() + "'style='color: black'>" + rowObject.BeneCode + "</a>";
}

function BeneURL(cellvalue, options, rowObject) {
   // debugger;
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
    //debugger;
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

function HBRedingStatus(cellvalue, options, rowObject) {
    
    if (rowObject.HBGrade != null && rowObject.HBReading!=null) {
        if (rowObject.HBGrade = 0) {
            return '<label style="color:#00a65a;" >' + rowObject.HBReading + '</label>'
        }
        else if (rowObject.HBGrade = 1) {
            return '<label style="color:#f39c12;">' + rowObject.HBReading + '</label>'
        }
        else if (rowObject.HBGrade = 2) {
            return '<label style="color:#dd4b39;">' + rowObject.HBReading + '</label>'
        }
        else {
            var HBReading = rowObject.HBReading == null ? "" : rowObject.HBReading;
            return '<label style="color:#333">' + HBReading + '</label>'
        }
    }
    else {
        return "";
    }
}

function MURedingStatus(cellvalue, options, rowObject) {

    if (rowObject.MUGrade != null && rowObject.MUReading != null) {
        if (rowObject.MUGrade = 0) {
            return '<label style="color:#00a65a;" >' + rowObject.MUReading + '</label>'
        }
        else if (rowObject.MUGrade = 1) {
            return '<label style="color:#f39c12;">' + rowObject.MUReading + '</label>'
        }
        else if (rowObject.MUGrade = 2) {
            return '<label style="color:#dd4b39;">' + rowObject.MUReading + '</label>'
        }
        else {
            var MUReading = rowObject.MUReading == null ? "" : rowObject.MUReading;
            return '<label style="color:#333">' + MUReading + '</label>'
        }
    }
    else {
        return "";
    }
}

function OXPulseRedingStatus(cellvalue, options, rowObject) {

    if (rowObject.OXPulseReading != null) {
        return '<label style="color:#00a65a;">' + rowObject.OXPulseReading + '</label>'
    }
    else {
        var OXPulseReading = rowObject.OXPulseReading == null ? "" : rowObject.OXPulseReading;
        return '<label style="color:#333">' + OXPulseReading + '</label>'
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
    //debugger;
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
   // debugger;
    $("#imgpopup").prop('src', "");
    $("#headTitle").text("");
    $("#headTitletime").text("");
    var resukt = event.dataset;
    $("#imgpopup").prop('src', resukt.whimageurl);
    $("#headTitletime").text(resukt.whtimestamp);//
    $("#headTitle").text(resukt.whaddress); //
    $('#myModal').modal('show');
}

function GradeColor(cellvalue, options, rowObject) {
    if (rowObject.GradeName == "NOR") {
        return '<label style="color:#00a65a">' + rowObject.GradeName + '</label>'
    }
    else if (rowObject.GradeName == "MUW") {
        return '<label style="color:#f39c12">' + rowObject.GradeName + '</label>'
    }
    else if (rowObject.GradeName == "SUW") {
        return '<label style="color:#dd4b39">' + rowObject.GradeName + '</label>'
    }
    else {
        var Grade = rowObject.GradeName == null ? "" : rowObject.GradeName;
        return '<label style="color:#333">' + Grade + '</label>'
    }
}