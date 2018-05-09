
jQuery(document).ready(function () {
    ajaxGetJSON(urlContent + "Home/GetDists", function (retval) {
        debugger;
        var sel = "";
        var totCount = 0;
        for (var i = 0; i < retval.length; i++) {
            sel = sel + "<tr>";
            var dt = (retval[i].Row != null) ? retval[i].Row : '';
            sel = sel.concat('<th scope="row">' + dt + '</th>');
            sel = sel.concat('<td style="text-align: left !important" onClick="ViewReport(' + retval[i].DistrictID + ')">' + retval[i].DistrictName + '</td>');
            sel = sel.concat('<td>' + retval[i].TotalCount + '</td>');
            sel = sel.concat('<td>' + retval[i].Total + '</a></td>');
            sel = sel.concat('<td>' + retval[i].Normal + '</a></td>');
            sel = sel.concat('<td>' + retval[i].Moderate + '</a></td>');
            sel = sel.concat('<td>' + retval[i].Severe + '</a></td>');
            sel = sel.concat('<td>' + retval[i].Absent + '</a></td>');
            sel = sel.concat("</tr>");
        }
        $("#distLevData").html(sel);
    });
});
