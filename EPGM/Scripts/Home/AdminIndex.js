/// <reference path="../Common/helper.js" />
jQuery(document).ready(function () {

    var pieChartCanvas = $("#pieChart").get(0).getContext("2d");
    var pieChart = new Chart(pieChartCanvas);
    //debugger;
    var PieData = [
                    { value: dtnor, color: "#00a65a", highlight: "#00a65a", label: "Normal" },
                    { value: dtmod, color: "#f39c12", highlight: "#f39c12", label: "Moderate" },
                    { value: dtsev, color: "#dd4b39", highlight: "#dd4b39", label: "Severe" },
                    { value: dtabs, color: "#00c0ef", highlight: "#00c0ef", label: "Absent" }
    ];

    var pieOptions = {
        //Boolean - Whether we should show a stroke on each segment
        segmentShowStroke: true,
        //String - The colour of each segment stroke
        segmentStrokeColor: "#fff",
        //Number - The width of each segment stroke
        segmentStrokeWidth: 1,
        //Number - The percentage of the chart that we cut out of the middle
        percentageInnerCutout: 50, // This is 0 for Pie charts
        //Number - Amount of animation steps
        animationSteps: 100,
        //String - Animation easing effect
        animationEasing: "easeOutBounce",
        //Boolean - Whether we animate the rotation of the Doughnut
        animateRotate: true,
        //Boolean - Whether we animate scaling the Doughnut from the centre
        animateScale: false,
        //Boolean - whether to make the chart responsive to window resizing
        responsive: true,
        // Boolean - whether to maintain the starting aspect ratio or not when responsive, if set to false, will take up entire container
        maintainAspectRatio: false,
        //String - A legend template
        legendTemplate: "<ul class=\"<%=name.toLowerCase()%>-legend\"><% for (var i=0; i<segments.length; i++){%><li><span style=\"background-color:<%=segments[i].fillColor%>\"></span><%if(segments[i].label){%><%=segments[i].label%><%}%></li><%}%></ul>",
        //String - A tooltip template
        tooltipTemplate: "<%=value %> <%=label%>"
    };
    //Create pie or douhnut chart
    // You can switch between pie and douhnut using the method below.
    pieChart.Doughnut(PieData, pieOptions);


    var grid = jQuery('#DistGrid');
    grid.jqGrid({
        url: urlContent + 'Home/AdminGridData',
        datatype: 'json',
        height: 257,
        autowidth: true,
        gridview: true,
        mtype: 'GET',
        hoverrows: false,
        beforeRequest: function () { },
        colNames: ['DistrictID', '#', 'District Code', 'District Name', 'Projects Count', 'Sectors Count', 'AWCs Count', 'Details'],
        colModel: [
		        { name: 'DistrictID', index: 'DistrictID', width: 90, hidden: true },
                { name: 'Sno', index: 'Sno', width: 30, align: 'center' },
		  		{ name: 'DistrictCode', index: 'DistrictCode', width: 160, hidden: true },
		  		{ name: 'DistrictName', index: 'DistrictName', width: 120 },
		  		{ name: 'ProjectsCount', index: 'ProjectsCount', width: 80, align: 'center' },
		  		{ name: 'SectorsCount', index: 'SectorsCount', width: 80, align: 'center' },
		  		{ name: 'AWCCount', index: 'AWCCount', width: 80, align: 'center' },
		  		{ name: 'view', index: 'view', width: 80, align: 'center', formatter: showDists }],
        loadError: function (xhr, status, error) {
            var trf = $("#DistGrid tbody:first tr:first")[0];
            $("#DistGrid tbody:first").empty().append(trf);
        },
        loadComplete: function (data) { },
        loadonce: true
    });

    var distGrid = jQuery('#DistDetailsGrid');
    distGrid.jqGrid({
        url: urlContent + 'Home/GetDistrictDetails',
        datatype: 'json',
        height: 257,
        autowidth: true,
        gridview: true,
        mtype: 'GET',
        hoverrows: false,
        beforeRequest: function () { },
        colNames: ['DistrictID', '#', 'District Code', 'District Name', 'Total Beneficiaries', 'No.of Children Weighed', 'Normal', 'Moderate', 'Severe', 'Absent'],
        colModel: [
		        { name: 'DistrictID', index: 'DistrictID', width: 90, hidden: true },
                { name: 'Row', index: 'Row', width: 40, align: 'center' },
		  		{ name: 'DistrictCode', index: 'DistrictCode', width: 160, hidden: true },
		  		{ name: 'DistrictName', index: 'DistrictName', width: 160 },
		  		{ name: 'BeneCount', index: 'BeneCount', width: 120, align: 'center', hidden: true },
		  		{ name: 'Total', index: 'Total', width: 120, align: 'center', hidden: true },
		  		{ name: 'Normal', index: 'Normal', width: 100, align: 'center' },
		  		{ name: 'Moderate', index: 'Moderate', width: 100, align: 'center' },
		  		{ name: 'Severe', index: 'Severe', width: 100, align: 'center' },
		  		{ name: 'Absent', index: 'Absent', width: 100, align: 'center' }],
        loadError: function (xhr, status, error) {
            var trf = $("#DistDetailsGrid tbody:first tr:first")[0];
            $("#DistDetailsGrid tbody:first").empty().append(trf);
        },
        loadComplete: function (data) { },
        loadonce: true
    });

    function showDists(cellvalue, options, rowObject) {
        debugger;
        return "<a href='" + urlContent + "Home/Report/?distCode=" + rowObject.DistrictCode + "'style='text-decoration: underline; color: black;'>View</a>";
    }

    $(".subBut").click(function () {
        debugger;
        var whoType = btoa(this.id);
        window.location.href = urlContent + "District/Index/?dW5kZWZpbmVk=" + whoType;
    });

});



//ajaxGetJSON(urlContent + "Home/GetDistrictDetails", function (retval) {
//    debugger;
//    var sel = "";
//    var totCount = 0;
//    for (var i = 0; i < retval.length; i++) {
//        sel = sel + "<tr>";
//        var dt = (retval[i].Row != null) ? retval[i].Row : '';
//        var dttot = "" + retval[i].DistrictID + "_TOT";
//        $(document).on('click', '#' + dttot, function () {
//            debugger;
//            //alert('#' + dttot);
//        });
//        var dtnor = "" + retval[i].DistrictID + "_NOR";
//        $(document).on('click', '#' + dtnor, function () {
//            debugger;
//            //alert('#' + dtnor);
//        });
//        var dtmuw = "" + retval[i].DistrictID + "_MUW";
//        $(document).on('click', '#' + dtmuw, function () {
//            debugger;
//            //alert('#' + dtmuw);
//        });
//        var dtsuw = "" + retval[i].DistrictID + "_SUW";
//        $(document).on('click', '#' + dtsuw, function () {
//            debugger;
//            //alert('#' + dtsuw);
//        });
//        var dtabs = "" + retval[i].DistrictID + "_ABS";
//        $(document).on('click', '#' + dtabs, function () {
//            debugger;
//            //alert('#' + dtabs);
//        });
//        sel = sel.concat('<th scope="row">' + dt + '</th>');
//        sel = sel.concat('<td style="text-align: left !important" id="' + retval[i].DistrictID + '">' + retval[i].DistrictName + '</td>');
//        sel = sel.concat('<td>' + retval[i].TotalCount + '</td>');
//        sel = sel.concat('<td><a href="#" id="' + dttot + '">' + retval[i].Total + '</a></td>');
//        sel = sel.concat('<td><a href="#" id="' + dtnor + '">' + retval[i].Normal + '</a></td>');
//        sel = sel.concat('<td><a href="#" id="' + dtmuw + '">' + retval[i].Moderate + '</a></td>');
//        sel = sel.concat('<td><a href="#" id="' + dtsuw + '">' + retval[i].Severe + '</a></td>');
//        sel = sel.concat('<td><a href="#" id="' + dtabs + '">' + retval[i].Absent + '</a></td>');
//        sel = sel.concat("</tr>");
//    }
//    totCount += parseInt(retval[(retval.length - 1)].TotalCount);
//    $("#distLevData").html(sel);
//    $("#All").prepend(totCount);
//});
