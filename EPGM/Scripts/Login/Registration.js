



//function BindJsonDropDown(typeID, ID, name) {
//    debugger;

//    if (typeID == 0) {
//        url = '/Registration/OnchangeBindState?ID=' + ID;
//    }
//    else {
//        url = '/Registration/OnChangeBindDistrict?ID=' + ID;
//    }
//    $.ajax({

//        type: "GET",

//        contentType: "application/json; charset=utf-8",

//        url: url,

//        //data: "{}",

//        dataType: "json",

//        success: function (Result) {
//            $("#" + name + "").empty()
//            debugger;
//            for (var i = 0; i < Result.length; i++)
//            { var opt = new Option(Result[i].Text, Result[i].Value); $("#" + name + "").append(opt); }
//            //$.each(Result, function (key, value) {
//            //    Result = Result.d;
//            //    $("#" + name + "").append($("<option></option>").val(value.Value).html(value.Text));

//            //});

//        },

//        error: function (Result) {

//            alert("Error");

//        }

//    });

//}


function validateEmail(email) {
    var re = /^(([^<>()[\]\\.,;:\s@"]+(\.[^<>()[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
    return re.test(email);
}

function validateFreeEmail(emails) {
    debugger;
   var email= $('#' + emails + '').val();
   var reg = /^(([^<>()[\]\\.,;:\s@"]+(\.[^<>()[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
    if (reg.test(email)) {
        return true;
    }
    else {
        alert('Please Enter Valid Email Id ');
        $('#' + emails + '').val('');
        return false;
       
    }
}