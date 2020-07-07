<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmLineaTiempo.aspx.vb" Inherits="PlanProyecto_frmLineaTiempo" %>

<%@ Register Assembly="TimelineNet" Namespace="TimelineNet" TagPrefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <!-- <script src="timeline_js/timeline-api.js?bundle=true" type="text/javascript"></script>  -->
    <script src="v2.3/timeline-api.js?bundle=true" type="text/javascript"></script>  
    <script src="timeline_js/local_data.js" type="text/javascript"></script>
    <script src="jquery/jquery-1.5.2.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        var tl;
        function onLoad() {            
            var tl_el = document.getElementById("tl");
            var eventSource1 = new Timeline.DefaultEventSource();
            var theme1 = Timeline.ClassicTheme.create();
            theme1.autoWidth = true; // Set the Timeline's "width" automatically.
            // Set autoWidth on the Timeline's first band's theme,
            // will affect all bands.
            theme1.timeline_start = new Date(Date.UTC(2005, 0, 1));
            theme1.timeline_stop = new Date(Date.UTC(2100, 0, 1));

            var d = Timeline.DateTime.parseGregorianDateTime("2010")
            var bandInfos = [
                Timeline.createBandInfo({
                    width: 100, // set to a minimum, autoWidth will then adjust
                    intervalUnit: Timeline.DateTime.YEAR,
                    intervalPixels: 300,
                    eventSource: eventSource1,
                    date: d,
                    theme: theme1,
                    layout: 'original'  // original, overview, detailed
                })                
            ];

            // create the Timeline
            tl = Timeline.create(tl_el, bandInfos, Timeline.HORIZONTAL);

            var url = '.'; // The base url for image, icon and background image
            // references in the data
            eventSource1.loadJSON(timeline_data, url); // The data was stored into the 
            // timeline_data variable.
            tl.layout(); // display the Timeline
        }

        var resizeTimerID = null;
        function onResize() {
            if (resizeTimerID == null) {
                resizeTimerID = window.setTimeout(function() {
                    resizeTimerID = null;
                    tl.layout();
                }, 500);
            }
        }
   </script>
</head>
<body onload="onLoad();" onresize="onResize();">
<div id="doc3" class="yui-t7">   
   <div id="bd" role="main">
	   <div class="yui-g">
	     <div id='tl'></div>	     
	   </div>
	 </div>
</div>
    <!-- <cc1:Timeline runat="server" ID="TimeLine1">
    </cc1:Timeline> -->

</body>
</html>
