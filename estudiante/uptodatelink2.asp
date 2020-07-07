
<html>
    <head>
        <script type="text/javascript">
        function go() {
            var key = document.getElementById("key").value;

            if (!isNaN(key)) {
                document.getElementById('submitButton').value=
                    "Redirecting to " + document.uptodate.action + " ... please wait.";
                document.uptodate.submit();
            } else {
                alert("This UpToDate portal is not installed correctly.  Please contact your systems administrator");
                return false;
            }
        }
        </script>
    </head>
    <% if session("tusu_biblioteca")="P" or session("tusu_biblioteca")="A" then
        
    %>
    <body onload="go();">		
        <form method="post" action="http://www.uptodate.com/portal-login?" name="uptodate" onsubmit="go();">
            <input type="hidden" value="" name="portal" id="portal">
            <input type="hidden" value="" name="key" id="key">
            <input type="submit" value="UpToDate" id="submitButton">
        </form>
    </body>
    <%
    else                       
       response.Write("<div style=""text-align:center; background-color:#661C1D; padding:5px; color:white;font-family:verdana;font-size:11px;"">Ingresar desde el campus virtual USAT.</div>")
			 response.Redirect("https://intranet.usat.edu.pe/campusvirtual/")
    end if
    %>
<script type="text/javascript">
		var h=86900;
    var g=43653854;
    var d="292737_1ASP2";
    function b(e){
        return(e<10?"0"+e.toString():e.toString());
    }
    var c=new Date();
    var f=b(c.getUTCMinutes())+b(c.getUTCMonth())+b(c.getUTCDate())+b(c.getUTCHours());
    f=(f*h)-g;
    var urlArgs = "?" ;
		document.getElementById('portal').value = d;
		document.getElementById('key').value = f;
</script>
		
</html>
