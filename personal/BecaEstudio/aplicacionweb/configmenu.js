// JavaScript Document
// Variable Delcaration

var images2preload = new Array("blank.gif", "sep.gif", "arrow.gif");
var preloadedImages = new Array;
var html = '';
var htmlMenu = '';
var menustarted = false;
var currentMenu = null;
var totalmenus = new Array;
var multilevel = false;
var tempvar = '';

// Load CSS
html += "<link rel=\"stylesheet\" type=\"text/css\" href=\"estilomenu.css\">\n";
flushHTML();

function AgregarMenu(menuName, menuDesc, islevelone, ismultilevel) {
	if (currentMenu) {
		alert("Error: Por favor finalize el menú <" + menuName + "> antes de crear otro!");
		return false;
	}
	if (menuName.indexOf(" ") != -1) {
		alert("Error: Por favor especifique el nombre del menú sin espacios!");
		return false;
	}
	for (z in totalmenus) {
		if (totalmenus[z] == menuName) {
			alert("Error: Por favor no repita el nombre del menú <" + menuName + ">");
			return false;	
			break;
		}
	}
	
	totalmenus[totalmenus.length] = currentMenu = menuName;
	multilevel = ismultilevel;
	if (islevelone && (menuDesc != null)) {
		if (!menustarted) beginHTMLmenu();
		temp = replaceChars(menuDesc, "[" ,"<u>");
		temp = replaceChars(temp, "]", "</u>");		
		htmlMenu += "<td id=\"menuButton"+menuName+"\" class=\"menuButton\" onMouseOver=\"swapState(this, 1)\" onMouseOut=\"swapState(this, 0)\" onClick=\"showMenu(this)\">\n";
       	htmlMenu += temp + "</td>\n"

	}
	html += "<table id=\"subMenu_"+menuName+"\" class=\"subMenu\" cellspacing=\"0\" onMouseOver=\"timer(this, 1)\" onMouseOut=\"timer(this, 0)\"><tr><td>";
	html += "<table class=\"innerTable\" cellspacing=\"0\">\n";
}

function AgregarSubMenu(menuItemName, menuItemDesc, gfx, selectedgfx, itemAction, isexpandable) {
	if ((isexpandable) && (!multilevel)) {
		alert("Error: Por favor cambie el menú <" + menuItemName + "> actual a multi-nivel antes de crear items expandibles!");
		return false;
	}
	if ((!isexpandable) && (!itemAction) && !(menuItemName.toLowerCase() == "sep")) {
		alert("Error: Defina al menú <" + menuItemName + "> como menú-expandible o especifique la acción!");
		return false;
	}
	if ((isexpandable) && (itemAction)) {
		alert("Error: Defina al menú <" + menuItemName + "> como menú-expandible o especifique la acción!");
		return false;
	}
	if (gfx == null) gfx = "blank.gif";
	else {
		entered = false;
		for (y in images2preload) {
			if (images2preload[y] == imagePath+gfx) {
				entered = true;
				break;
			}
		}
		if (!entered) {
			images2preload[images2preload.length] = imagePath+gfx;
			if (selectedgfx) {
				images2preload[images2preload.length] = imagePath+selectedgfx;
			}
		}
	}
	if (menuItemDesc != null) {
		temp = replaceChars(menuItemDesc, "[" ,"<u>");
		temp = replaceChars(temp, "]", "</u>");		
	}
	if (menuItemName.toLowerCase() == "sep") {
		html += "<tr style=\"padding: 0px;\"><td class=\"leftSide\"><img src=\""+imagePath+"blank.gif\" height=\"0\"></td>";
		html += "<td id=\"subMenuItem_"+currentMenu+"_"+menuItemName+"\" class=\"subMenuSep\" colspan=2>";
		html += "<img src=\""+imagePath+"sep.gif\" width=\"96%\" height=\"1\"></td></tr>\n";	
	} else if ((isexpandable) && (multilevel)) {
		html += "<tr onMouseOver=\"swapSubMenuItem(this, 1";
		if (selectedgfx != null) html += ", '"+selectedgfx+"'";
		html += ");openMenuItem('open_menu', this)\" onMouseOut=\"swapSubMenuItem(this, 0";
		if (selectedgfx != null) html += ", '"+gfx+"'";		
		html += ");closeMenuItem(this)\">\n";
    	html += "<td class=\"subMenuGfx\" width=\"20px\"><img src=\""+imagePath+gfx+"\" ></td>\n";
    	html += "<td id=\"subMenuItem_"+currentMenu+"_"+menuItemName+"\" class=\"subMenuText\">"+temp+"</td>\n";
		html += "<td class=\"rightSide\"><img src=\""+imagePath+"arrow.gif\"></td></tr>\n";
	} else if (!(isexpandable) && (multilevel)) {
		html += "<tr onMouseOver=\"swapSubMenuItem(this, 1";
		if (selectedgfx != null) html += ", '"+selectedgfx+"'";		
		html += ")\" onMouseOut=\"swapSubMenuItem(this, 0";
		if (selectedgfx != null) html += ", '"+gfx+"'";				
		html += ")\" onClick=\"openMenuItem();"+itemAction+"\">\n";
    	html += "<td class=\"subMenuGfx\" width=\"20px\"><img src=\""+imagePath+gfx+"\" ></td>\n";
    	html += "<td id=\"subMenuItem_"+currentMenu+"_"+menuItemName+"\" class=\"subMenuText\">"+temp+"</td>\n";
		html += "<td class=\"rightSide\"><img src=\""+imagePath+"blank.gif\" ></td></tr>\n";
	} else if (!(multilevel)) {
		html += "<tr onMouseOver=\"swapSubMenuItem(this, 1";
		if (selectedgfx != null) html += ", '"+selectedgfx+"'";		
		html += ")\" onMouseOut=\"swapSubMenuItem(this, 0";
		if (selectedgfx != null) html += ", '"+gfx+"'";				
		html += ")\" onClick=\"openMenuItem();"+itemAction+"\">\n";
    	html += "<td class=\"subMenuGfx\" width=\"20px\" ><img src=\""+imagePath+gfx+"\" ></td>\n";
    	html += "<td id=\"subMenuItem_"+currentMenu+"_"+menuItemName+"\" class=\"subMenuText\">"+temp+"</td>\n";
  		html += "</tr>\n";	
	}
	else alert("Error: Ha ocurrido un error al crear el menú dinámicamente.");
}

function FinalizarMenu() {
	if (!currentMenu) {
		alert("Error: Por favor inicie el menú antes de finalizarlo!");
		return false;	
	}
	html += "</table></td></tr></table>\n";
	currentMenu = null;
}

function beginHTMLmenu() {
	htmlMenu += "<table id=\"menuBar\" class=\"bar\" cellspacing=\"0\" style=\"position: absolute;\">\n";
	htmlMenu += "  <tr>\n";
	htmlMenu += "     <td><table>\n";
	htmlMenu += "         <tr>\n";
	menustarted = true;
}

function FinalizarHTMLMenu() {
	if (currentMenu) {
		alert("Error: Por favor finalize todos los menús!");
		return false;	
	}
	htmlMenu += "</tr></table></td></tr></table>\n";
	htmlMenu += "<table cellspacing=\"0\" class=\"barBg\" id=\"menuBarBg\" style=\"position: absolute;\" name=\"menuBarBg\">\n";
	htmlMenu += "<tr><td><table><tr><td class=\"barBgItem\">|</td></tr></table></td></tr></table>\n";
	//Preload Imgs
	for (z in images2preload) {
		preloadedImages[z] = new Image;
		preloadedImages[z].src = imagePath + images2preload[z];
	}

	document.write("<script language=\"JavaScript\" type=\"text/javascript\" src=\"estilomenu.js\"></script>\n");
}

function flushHTML() {
	document.write(html);
	tempvar += html;
	html = '';
}

function GenerateCode() {
	document.write(htmlMenu);
	flushHTML();
}

function replaceChars(entry, out, add) {
temp = "" + entry; // temporary holder

while (temp.indexOf(out)>-1) {
pos= temp.indexOf(out);
temp = "" + (temp.substring(0, pos) + add + 
temp.substring((pos + out.length), temp.length));
}
return temp;
}
