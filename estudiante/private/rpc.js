function createobject(handler)
{ 
var objXMLHttp=null
if (window.XMLHttpRequest)
{
objXMLHttp=new XMLHttpRequest()
}
else if (window.ActiveXObject)
{
objXMLHttp=new ActiveXObject("Microsoft.XMLHTTP")
}
return objXMLHttp
}

function getrpc(pagename,procedure)
{
	rst=createobject();
	parameters=pagename + "?procedure=" + procedure + "&uniqueid=" + Date().toString();
	
	if (rst==null)
	{
		alert ("Browser does not support HTTP Request");
		return 0;
	}
	//alert(parameters);
	rst.open("GET",parameters,false);
	rst.setRequestHeader('Content-Type','application/x-www-form-urlencoded; charset=utf-8');
  	rst.send(null);
  	var xx= rst.responseText;
	//alert(xx);
  	return xx;
}
function clsrpc()
{
	this.responsetext="";
	this.pagename = "datos.aspx"; //rpc.asp
		clsrpc.prototype.exec=function(procedure)
		{
			this.responsetext=getrpc(this.pagename,escape(procedure));
			return this.responsetext;
			
		}

}
var rpc=new clsrpc();

		if(window.event) var e=window.event;
		var a=e.srcElement? e.srcElement : e.target;
