// JavaScript Document
var menuBarElement, menuBarBgElement;
var menuButtonClicked = new Array();
var menuButtonElements = new Array();
var subMenuElements = new Array();
var subMenuGfx = new Array();
var subMenuTimeOuts = new Array();
var subMenuTimeOutsElement = new Array();
var cursorOnMenu = false;

var ie = document.all;

function init() {

	// Colour In MenuBar
	menuBarElement = document.getElementById("menuBar");
	menuBarBgElement = document.getElementById("menuBarBg");
	
	menuBarElement.style.backgroundColor = "transparent"; 
	menuBarBgElement.style.backgroundColor = menuBarBgColor;
			
	// Colour In MenuButtons
	TD_Elements = document.getElementsByTagName("TD");
	for (i=0;i<TD_Elements.length;i++) {
		if (TD_Elements[i].id.substring(0, 10) == "menuButton") menuButtonElements[menuButtonElements.length] = TD_Elements[i];
	}
	
	for (i=0;i<menuButtonElements.length;i++) {
		swapState(menuButtonElements[i], 0);
	}
	
	// Colour In SubMenus
	Table_Elements = document.getElementsByTagName("TABLE");
	for (i=0;i<Table_Elements.length;i++) {
		if (Table_Elements[i].id.substring(0, 7) == "subMenu") subMenuElements[subMenuElements.length] = Table_Elements[i];
	}
	
	for (i=0;i<subMenuElements.length;i++) {
		subMenuElements[i].style.borderColor = subMenuBdrColor;
		subMenuElements[i].style.backgroundColor = subMenuBgColor[0];		
	}

	// Color In SubMenu Gfx
	for (i=0;i<TD_Elements.length;i++) {
		if (TD_Elements[i].className == "subMenuGfx") {
			subMenuGfx[subMenuGfx.length] = TD_Elements[i];
		}
	}

	for (i=0;i<subMenuGfx.length;i++) {
		subMenuGfx[i].style.borderColor = subMenuGfxBgColor;
		subMenuGfx[i].style.backgroundColor = subMenuGfxBgColor;
	}
	
	for (i=0;i<TD_Elements.length;i++) {
		if (TD_Elements[i].className == "leftSide") TD_Elements[i].style.backgroundColor = subMenuGfxBgColor;
	}
	
	TR_Sections = document.getElementsByTagName("TR");
	for (j=0;j<TR_Sections.length;j++) {
		TD_Sections = TR_Sections[j].getElementsByTagName("TD");
		if (TD_Sections[0].className == "subMenuGfx") {
			TD_Sections[0].style.borderRightWidth = "0px";

			if (TD_Sections.length == 2) {
				TD_Sections[1].style.borderLeftWidth = "0px";
			} else {
				TD_Sections[1].style.borderLeftWidth = "0px";
				TD_Sections[1].style.borderRightWidth = "0px";
				TD_Sections[2].style.borderLeftWidth = "0px";
			}
		}
	}
	
	//Keep Main MenuBar On Top of Screen
	if (keepMenuBarOnTop) setInterval("keepOnTop()", 1);
	bodytag = document.getElementById("BODY");

}

function swapState(element, state) {
	cursorOnMenu = state;
	y = (ie)?document.body.scrollTop:window.pageYOffset;
	nowY = findPosY(menuBarElement);
		
	if (nowY != y && state) {
		return false;
	}

	if (menuButtonClicked[element.id]) {

		shortName = element.id.substring(10, element.id.length);
		menuName = "subMenu_" + shortName;

		timer(document.getElementById(menuName), state);
		return false;
	}

	for (j in menuButtonClicked) if (menuButtonClicked[j]) {
		element.style.borderWidth = "1px";
		element.style.padding = "3px 7px 2px";
		menuButtonClicked[element.id] = false;
		subMenuTimeOutsElement[1] = null;

		hideMenus();
		showMenu(element);

		return;
	}
	
	element.style.backgroundColor = bgColorArray[state];
	element.style.borderColor = bdrColorArray[state];
}

function swapSubMenuItem(element, state, newImg) {
	TD_Sections = element.getElementsByTagName("TD");
	for (j=0;j<TD_Sections.length;j++) {
		if (state == 0 && j == 0) TD_Sections[j].style.backgroundColor = subMenuGfxBgColor;
		else TD_Sections[j].style.backgroundColor = subMenuBgColor[state];
		
		if (state) {
			if (j==(TD_Sections.length)-1) TD_Sections[j].style.borderColor = subMenuItemSelectedBdrColor[2];
			else TD_Sections[j].style.borderColor = subMenuItemSelectedBdrColor[j];
		} else {
			TD_Sections[j].style.borderColor = subMenuItemBdrColor[j];
		}
	}
	Img_Sections = element.getElementsByTagName("IMG");
	if ((Img_Sections[0].src != null) && (newImg != null)) {
		Img_Sections[0].src = imagePath + newImg;
	}

	cursorOnMenu = state;	
}

function showMenu(element) {
	if (menuButtonClicked[element.id]) {
		hideMenus();
		swapState(element, 1);
		return false;
	}

	for (i=0;i<menuButtonElements.length;i++) {
		if (menuButtonElements[i].id != element.id) {
			menuButtonElements[i].style.borderWidth = "0px";
			menuButtonElements[i].style.padding = "4px 8px 3px";
			menuButtonElements[i].style.backgroundColor = "transparent";			
		}
	}
	
	swapState(element, 2);
	menuButtonClicked[element.id] = true;
	
	menuName = element.id.substring(10, element.id.length); 
	menuElement = document.getElementById("subMenu_" + menuName);	
	if (document.all) {
		menuElement.filters.alpha.opacity = 0;			
	} else {
		menuElement.style.MozOpacity = "0%";
	}	

	menuElement.style.top  = findPosY(document.getElementById("menuButton"+menuName)) + 19;

	menuElement.style.left = findPosX(document.getElementById("menuButton"+menuName));
	menuElement.style.visibility = "visible";

    timer(menuElement, 1);
	fadeIn(menuElement.id);
}

function fadeIn(id) {
	if (document.all) {
		document.getElementById(id).filters.alpha.opacity += 5;
		divOpacity = document.getElementById(id).filters.alpha.opacity;		
		if (divOpacity > 100) document.getElementById(id).filters.alpha.opacity = 100;
	} else {
		document.getElementById(id).style.MozOpacity = ((document.getElementById(id).style.MozOpacity.replace("%", "") - 0) + 5) + "%";
		divOpacity = (document.getElementById(id).style.MozOpacity.replace("%", "") - 0);
		if (divOpacity > 100) document.getElementById(id).style.MozOpacity = "100%";				
	}
	
	if (divOpacity < 100) setTimeout('fadeIn("'+id+'");','5');	
}

function hideMenus(id) {
	if (id != null) {
		
		buttonElement = document.getElementById(id);
		shortName = buttonElement.id.substring(10, buttonElement.id.length);
		menuName = "subMenu_" +  shortName;
		
		if (document.getElementById(menuName).style.visibility == "hidden") return false;
		
		menuButtonClicked[id] = false;
		subMenuTimeOutsElement[1] = null;	
		swapState(buttonElement, 0);	
		document.getElementById(menuName).style.visibility = "hidden";
	} else {

		for (j in menuButtonClicked) menuButtonClicked[j] = false;
		for (i=0;i<menuButtonElements.length;i++) {
			swapState(menuButtonElements[i], 0);
		}
		
		for (i=0;i<subMenuElements.length;i++) {
			subMenuElements[i].style.visibility = "hidden";
		}
		
		for (j in subMenuTimeOuts.length) {
			window.clearTimeout(subMenuTimeOuts[j]);
		}
	}
	
	for (i=0;i<menuButtonElements.length;i++) {
		if (!menuButtonClicked[menuButtonElements.id]) {
			menuButtonElements[i].style.borderWidth = "1px";
			menuButtonElements[i].style.padding = "3px 7px 2px";
		}	
	}
}

function hidemenu(id) {
		document.getElementById(id).style.visibility = "hidden";
}

function timer(element, state) {
	id = element.id; 
	menuName = id.substring((id.indexOf('_')+1), id.length);
	window.clearTimeout(subMenuTimeOuts[id]);
	window.status = menuName+": "+state;
	if (!state) {
		if ((menuName.indexOf('_')) == -1) {
			subMenuTimeOuts[id] = window.setTimeout("hideMenus('menuButton"+menuName+"')", timeOut);
		} else subMenuTimeOuts[id] = window.setTimeout("hidemenu('"+id+"')", timeOut);	
	}

	var matches = id.match(/_/g); 
	if (matches.length > 1) {
		lastPos = id.lastIndexOf("_");
		eleName = id.substring(0, lastPos);
		timer(document.getElementById(eleName), state);
	}
}

function openMenuItem(action, element) {
	if (action != null) {
		if (action.substring(0, 9) == "open_menu") {
			if (!element) {
				alert("Warning: No Menu Specified!\n\tUnable to launch submenu.");
				return false;
			}
			
			childTDs = element.getElementsByTagName("TD");
			subMenuName = childTDs[1].id.substring(childTDs[1].id.indexOf('_')+1, childTDs[1].id.length);
		
			posX = findPosX(childTDs[2].getElementsByTagName("IMG")[0]);
			posY = findPosY(element);
		
			popMenuElement = document.getElementById("subMenu_"+subMenuName);
			if (!popMenuElement) {
				alert("Warning: Specified Menu Not Found!!\n\tUnable to launch submenu.");
				return false;
			}

			if (popMenuElement.style.visibility != "visible") {
				
			if (document.all) {
				popMenuElement.filters.alpha.opacity = 0;			
			} else {
				popMenuElement.style.MozOpacity = "0%";
			}	
			
			if (!ie) posY += 1;
			popMenuElement.style.top  = posY - 2;
			popMenuElement.style.left = posX + 15;
			popMenuElement.style.visibility = "visible";
		
			fadeIn(popMenuElement.id);

			timer(popMenuElement, 0);
			timer(popMenuElement, 1);
			}
		} else {
			hideMenus();
			for (j=1;j<subMenuTimeOuts.length;j++) {
				window.clearTimeout(subMenuTimeOuts[j]);
				subMenuTimeOutsElement[j] = null;
			}
		}
	} else {
			hideMenus();
			for (j=1;j<subMenuTimeOuts.length;j++) {
				window.clearTimeout(subMenuTimeOuts[j]);
				subMenuTimeOutsElement[j] = null;
			}
	
	}
}

function closeMenuItem(element) {
	childTDs = element.getElementsByTagName("TD");
	subMenuName = childTDs[1].id.substring(childTDs[1].id.indexOf('_')+1, childTDs[1].id.length);

	popMenuElement = document.getElementById("subMenu_"+subMenuName);
	timer(popMenuElement, 0);
}
// Key Functions

function findPosX(obj)
{

	var curleft = 0;
	if ((document.getElementById || ie) && (obj.offsetParent != null))
	{
		while (obj.offsetParent)
		{
			curleft += obj.offsetLeft
			obj = obj.offsetParent;
		}
	}
	else if (document.layers) curleft += obj.x;
	return curleft;
}

function findPosY(obj)
{
	var curtop = 0;
	if ((document.getElementById || ie) && (obj.offsetParent != null))
	{
		while (obj.offsetParent)
		{
			curtop += obj.offsetTop
			obj = obj.offsetParent;
		}
	}
	else if (document.layers) curtop += obj.y;
	return curtop;
}

function keepOnTop() {
	if (keepMenuBarOnTop) {
		y = (ie)?document.body.scrollTop:window.pageYOffset;
		nowY = findPosY(menuBarElement); 
		
		newY = nowY;
		if (nowY != y) {
			if (nowY > y) {
				if ((nowY - y) > screen.height) newY = y - 70;
				else newY -= 1;
			}
			else {
				if ((y - nowY) > 70) newY += 70;
				else newY += 1;
			}
			hideMenus();
		} else diffY = 0;
		
		menuBarBgElement.style.left = menuBarElement.style.left = 0 + "px";
		menuBarBgElement.style.top  = menuBarElement.style.top  = newY + "px";
				
	}
}

function passEvent(e) {
	if (!cursorOnMenu) {
		hideMenus();
	}
}

onload = init;
document.onclick = passEvent;
