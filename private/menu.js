var Menuitem=new Array();
var MenuNoitems=0;
var Menu;
var Menucell;

/*
<script language="JavaScript">
agregarmenu("../inicio.gif","Back","javascript","history.back()");
agregarmenu("forward.gif","Forward","javascript","history.forward()");
agregarmenu("-");
agregarmenu("mail.gif","Email","mailto:whoever@whereever.com");
agregarmenu("-");
agregarmenu("webel.gif","Webelectrix.com","http://www.webelectrix.com","_blank");
agregarmenu("-");
agregarmenu("print.gif","Print","javascript","window.print()");
construirmenu();

document.oncontextmenu=mostrarmenu;
document.body.onclick=ocultarmenu;

</script>
*/

function construirmenu()
{
	Menu='<div onmouseover="Menuover()" onmouseout="Menuout()" onclick="seleccionarMenu()" id="Menucontainer" class="Menucontainer"><table class="Menu">';
	for (i=0;i<MenuNoitems;i++) 
	{
		Menu=Menu+Menuitem[i];
	}
	Menu=Menu+'</table></div>';
	
	document.write(Menu);
}

//Create Items.

function agregarmenu(type,text,link,target)
{
	if (type=="-")
	{
	//Horezontal Rule.
		Menuitem[MenuNoitems]='<TR><TD><HR></TD></TR>';
	}
	else
	{
	//Custom HTML.
		if (type=="custom")
		{
			Menuitem[MenuNoitems]='<TR><TD>'+text+'</TD></TR>';
		}
		else
	//Standard.
		{
			if (type=="")
			{
			Menuitem[MenuNoitems]='<TR><TD>';
			Menuitem[MenuNoitems]=Menuitem[MenuNoitems]+'<table cellSpacing="0" class=menuitems width="100%"><tr><td width=18 height=18 type="text">';
			Menuitem[MenuNoitems]=Menuitem[MenuNoitems]+'</td><td type="text" url="'+link+'" target="'+target+'">';
			Menuitem[MenuNoitems]=Menuitem[MenuNoitems]+text+'</td></tr></table>';
			Menuitem[MenuNoitems]=Menuitem[MenuNoitems]+'</TD></TR>';			
			}
			else
			{
			if (type=="disable")
			{
			Menuitem[MenuNoitems]='<TR><TD>';
			Menuitem[MenuNoitems]=Menuitem[MenuNoitems]+'<table cellSpacing="0" style="color:inactivecaption" class=menuitems width="100%"><tr><td width=18 height=18>';
			Menuitem[MenuNoitems]=Menuitem[MenuNoitems]+'</td><td>';
			Menuitem[MenuNoitems]=Menuitem[MenuNoitems]+text+'</td></tr></table>';
			Menuitem[MenuNoitems]=Menuitem[MenuNoitems]+'</TD></TR>';			
			}
			else
			{
			Menuitem[MenuNoitems]='<TR><TD>';
			Menuitem[MenuNoitems]=Menuitem[MenuNoitems]+'<table cellSpacing="0" class=menuitems width="100%"><tr><td width=16 height=16 style="BORDER: 1px solid buttonface;" type="img">';
			Menuitem[MenuNoitems]=Menuitem[MenuNoitems]+'<IMG height=16 src="'+type+'" width=16></td><td type="img" url="'+link+'" target="'+target+'">';
			Menuitem[MenuNoitems]=Menuitem[MenuNoitems]+text+'</td></tr></table>';
			Menuitem[MenuNoitems]=Menuitem[MenuNoitems]+'</TD></TR>';
			}
			}
		}
	}
	MenuNoitems++;
}

function mostrarmenu(){
	var rightedge=document.body.clientWidth-event.clientX
	var bottomedge=document.body.clientHeight-event.clientY
	if (rightedge<Menucontainer.offsetWidth)
		Menucontainer.style.left=document.body.scrollLeft+event.clientX-Menucontainer.offsetWidth
	else
		Menucontainer.style.left=document.body.scrollLeft+event.clientX
	if (bottomedge<Menucontainer.offsetHeight)
		Menucontainer.style.top=document.body.scrollTop+event.clientY-Menucontainer.offsetHeight
	else
		Menucontainer.style.top=document.body.scrollTop+event.clientY
	Menucontainer.style.visibility="visible"
	return false
}
function ocultarmenu(){
	Menucontainer.style.visibility="hidden"
}

function Menuover()
{
	Menucell=event.srcElement;
	if (Menucell.type=="text")
	{
		Menucell.parentElement.children(0).style.backgroundColor="highlight";
		Menucell.parentElement.children(1).style.backgroundColor="highlight";
		Menucell.parentElement.children(1).style.color="highlighttext";
	}
	if (Menucell.type=="img")
	{
		Menucell.parentElement.children(0).style.border="1px outset";
		Menucell.parentElement.children(1).style.backgroundColor="highlight";
		Menucell.parentElement.children(1).style.color="highlighttext";
	}	
	Menucell=Menucell.parentElement
	if (Menucell.type=="img")
	{
		Menucell.parentElement.children(0).style.border="1px outset";
		Menucell.parentElement.children(1).style.backgroundColor="highlight";
		Menucell.parentElement.children(1).style.color="highlighttext";
	}	
}

function Menuout()
{
	Menucell=event.srcElement;
	if (Menucell.type=="text")
	{
		Menucell.parentElement.children(0).style.backgroundColor="";
		Menucell.parentElement.children(1).style.backgroundColor="";
		Menucell.parentElement.children(1).style.color="menutext";
	}
	if (Menucell.type=="img")
	{
		Menucell.parentElement.children(0).style.border="1px solid buttonface";
		Menucell.parentElement.children(1).style.backgroundColor="";
		Menucell.parentElement.children(1).style.color="menutext";
	}
	Menucell=Menucell.parentElement
	if (Menucell.type=="img")
	{
		Menucell.parentElement.children(0).style.border="1px solid buttonface";
		Menucell.parentElement.children(1).style.backgroundColor="";
		Menucell.parentElement.children(1).style.color="menutext";
	}
}

function seleccionarMenu()
{
	Menucell=event.srcElement;
	Menucontainer.style.visibility="hidden";
	if (Menucell.type=="text"|Menucell.type=="img")
	{
		if (Menucell.parentElement.children(1).url=="javascript")
		{
			eval(Menucell.parentElement.children(1).target);
		}
		else
		{
			if (Menucell.parentElement.children(1).url!= "undefined"){
				if (Menucell.parentElement.children(1).target!="undefined")
					window.open(Menucell.parentElement.children(1).url,Menucell.parentElement.children(1).target);
				else
					window.location=Menucell.parentElement.children(1).url;
			}			
		}
	}
	Menucell=Menucell.parentElement
	if (Menucell.type=="img")
	{
		if (Menucell.parentElement.children(1).url=="javascript")
		{
			eval(Menucell.parentElement.children(1).target);
		}
		else
		{
			if (Menucell.parentElement.children(1).url!= "undefined"){
				if (Menucell.parentElement.children(1).target!="undefined")
					window.open(Menucell.parentElement.children(1).url,Menucell.parentElement.children(1).target);
				else
					window.location=Menucell.parentElement.children(1).url;
			}			
		}
	}	
}