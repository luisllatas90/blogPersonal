<%@ LANGUAGE=JSCRIPT %>
<%
    var h=86900;
	var g=43653854;
	var d="292737_1ASP2";
    function b(e){
        return(e<10?"0"+e.toString():e.toString());
    }
    var c=new Date();
    var f=b(c.getUTCMinutes())+b(c.getUTCMonth())+b(c.getUTCDate())+b(c.getUTCHours());
    f=(f*h)-g;
    var urlArgs = "?" + Request.QueryString;
%>
<html>
    <head>
        <script language="javascript">
        function go() {
            var key = document.getElementById("key").value;
			var link = document.getElementById("link").value;
			
			if(link.lastIndexOf('intranet.usat.edu.pe/campusvirtual/estudiante/')>0){	
				if (!isNaN(key)) {
					document.getElementById('submitButton').value=
						"Redirecting to " + document.uptodate.action + " ... please wait.";
						document.uptodate.submit();
				} else {
					alert("This UpToDate portal is not installed correctly.  Please contact your systems administrator");
					//window.location = "http://www.usat.edu.pe/";
					return false;
				}
			}
			else{
				    alert("This UpToDate portal is not installed correctly.  Please contact your systems administrator");
					//window.location = "http://www.usat.edu.pe/";
					return false;
			}
            
        }
        </script>
    </head>
    <!-- <body onload="go();"> -->
	<body>
        <form method="post" action="https://www.uptodate.com/portal-login<%=urlArgs%>" name="uptodate" onsubmit="go();">			
			<% pagina = Request.ServerVariables("HTTP_REFERER")	 
				Response.Write(pagina)
				%>				
			<input type="hidden" value="<%= pagina %>" name="link" id="link">
            <input type="hidden" value="<%= d %>" name="portal">
            <input type="hidden" value="<%= f %>" name="key" id="key">
            <input type="submit" value="UpToDate" id="submitButton">
        </form>
    </body>
</html>