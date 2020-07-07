

var ChartToolbar = {

popupMenus : new Array(),
btnElement : null,
suppressCloseMenu: false,

loadingContent :  "<div class=\"ds_mnu\" style=\"border: black 1px solid;\">&nbsp;Loading...&nbsp;</div>",

callDrop : function( e, elm, chartID, params)
{
    var context = ChartCB.context(chartID);
    if ( elm && elm.tagName && elm.tagName == "IMG")
    {
        elm = elm.parentNode;
    }
	var r = DundasChart.getElmPosition( elm);
    r.left = r.x + "px";
    r.top  = (r.y + r.h) + "px";    
	context.pos = r;
	context.element = elm;
	context.callback = ChartToolbar.callDropCallBack;
    var result = ChartCB.call(e, chartID, params, null, context); 	
	return result;
},

callSub : function( e, elm, chartID, params)
{
    var context = ChartCB.context(chartID);
    if ( elm && elm.tagName && elm.tagName == "IMG")
    {
        elm = elm.parentNode;
    }
	var r = DundasChart.getElmPosition( elm);
    r.left = (r.x + r.w)+ "px";
    r.top  = (r.y) + "px";    
	context.pos = r;
	context.element = elm;
	context.callback = ChartToolbar.callDropCallBack;
        
	ChartToolbar.suppressCloseMenu = true;
    var result = ChartCB.call(e, chartID, params, null, context); 	

	ChartToolbar.suppressCloseMenu = true;
	elm.style.cursor = "wait";
	
	return result;
},

callCntx : function( e, chartID, params)
{
    e = DundasChart.fixEvent( e)
    var context = ChartCB.context(chartID);
	var scroll = DundasChart.getScrollPos();
	var r = {x:0,y:0,w:0,h:0};
	r.left = (e.clientX + scroll.left) + "px";
	r.top  = (e.clientY + scroll.top) + "px";
	context.pos = r;
	context.element = null;
    context.floatMode = true;	
	context.callback = ChartToolbar.callDropCallBack;
    var result = ChartCB.call(e, chartID, params, null, context); 	
	return result;
},

callDropCallBack : function ( context, content )
{
	ChartToolbar.closeMenu();
	var fr = document.createElement("DIV");
	fr.style.position = "absolute";
	fr.style.zIndex = 1000;
    fr.style.backgroundColor = "white";	
	fr.style.left = context.pos.left;
	fr.style.top  = context.pos.top;
	
    // bug fix #7933
    if (document.location.protocol.toLowerCase() == "https:" && DundasChart.msieFlag)
    {
	    content = content.replace(/url\(/gi, "url(https://" + document.location.hostname);
    }
    ///
    
	fr.innerHTML = content;
	fr.style.display = "block";
	if ( context.element )
	{
		if ( context.element && context.element.style.cursor == "wait")
		{
		   context.element.style.cursor = "default";
		}
		fr.parentMenu = DundasChart.getParentNodeByTag(context.element, "TABLE");
		if ( fr.parentMenu && fr.parentMenu.childMenu )
		{
			ChartToolbar.closeMenu(fr.parentMenu.childMenu );
		}
		if (fr.parentMenu)
		{
		    fr.parentMenu.childMenu = fr;
		}
	}
	ChartToolbar.btnElement = context.element;
	ChartToolbar.outButton(true);
	document.body.appendChild( fr);
	ChartToolbar.pushMenu(fr);
    if (context.floatMode && DundasChart) {
	    DundasChart.adjustPosition( fr.style.left, fr.style.top, fr);
	}
	window.setTimeout(function() { ChartToolbar.setCloseMenu() }, 100)	
	return false;
},

menuHandlerAttached : false,

cancelCloseMenu : function ()
{
	if ( ChartToolbar.menuHandlerAttached )
	{
		DundasChart.detachEvent(document, "click", ChartToolbar.closeAllMenus);
		ChartToolbar.menuHandlerAttached = false;
	}
},

setCloseMenu : function ()

{
	if ( !ChartToolbar.menuHandlerAttached )
	{
		DundasChart.attachEvent(document, "click", ChartToolbar.closeAllMenus);
		ChartToolbar.menuHandlerAttached = true; 
	}
},

closeMenuItem : function ( activeMenu)
{
	if ( activeMenu )
	{
	    document.body.removeChild( activeMenu);
	    ChartToolbar.activeMenu = null;
	    ChartToolbar.outButton(false);
	    ChartToolbar.btnElement = null;
	}
	if ( !ChartToolbar.topMenu() )
	{
	    ChartToolbar.cancelCloseMenu();
	}
},

closeMenu : function ( item)
{
	
	if ( ChartToolbar.suppressCloseMenu )
	{
	    ChartToolbar.suppressCloseMenu = false;
	    return void(0); 
	}
	if ( item )
	{
		var removed = false;
		do
		{
			var activeMenu =  ChartToolbar.popMenu();
			if ( activeMenu )
			{
				ChartToolbar.closeMenuItem( activeMenu );
				if (activeMenu == item )
				{
					removed = true;
				}
			}
			else
			{
				removed = true;
			}
		}
		while( !removed )
	}
	else
	{
		ChartToolbar.closeMenuItem(ChartToolbar.popMenu()); 	
	}	
},


closeAllMenus : function ()
{
	if ( ChartToolbar.suppressCloseMenu )
	{
	    ChartToolbar.suppressCloseMenu = false;
	    return void(0); 
	}
	
	while(ChartToolbar.topMenu())
	{
	    ChartToolbar.closeMenu();
	}
},

outButton : function( over)
{
	if ( ChartToolbar.btnElement )
	{
	    
	    if (ChartToolbar.btnElement.className.indexOf("_m") != -1 )
	    {
	        if ( over )
	        {
	            ChartToolbar.tbOverMnu( ChartToolbar.btnElement);
	        }
	        else
	        {
	            ChartToolbar.tbOutMnu( ChartToolbar.btnElement);
	        }
	    }
	    else
	    {
	        if ( over )
	        {
	            ChartToolbar.tbOver( ChartToolbar.btnElement);
	        }
	        else
	        {
	            ChartToolbar.tbOut( ChartToolbar.btnElement);
	        }
	            
	    }
	}
},

pushMenu : function ( item )
{
    var menus = ChartToolbar.popupMenus;
    menus[menus.length] = item;
},

popMenu : function ()
{
    var menus = ChartToolbar.popupMenus;
    if ( menus.length > 0 )
    {
        var result = menus[menus.length - 1];
        menus.length = menus.length - 1;
        return result;
    }
    return null;
},

popMenuItem : function (item)
{
    if ( !item )
    {
		return;
    }
    var menus = ChartToolbar.popupMenus;
    for( var i = 0; i < menus.length; i++)
    {
		if ( menus[i] == item )
		{
			if ( menus[i].childMenu )
			{
				DundasToolbar.popMenuItem(menus[i].childMenu);
			}
		}
		
    }
    
    if ( menus.length > 0 )
    {
        
        var result = menus[menus.length - 1];
        menus.length = menus.length - 1;
        return result;
    }
    return null;
},

topMenu : function ()
{
    var menus = ChartToolbar.popupMenus;
    if ( menus.length > 0 )
    {
        return menus[menus.length - 1];
    }
    return null;
},

tbOverMnu : function ( el )
{
    if ( el )
    {
		if ( typeof(el.togled) == 'undefined')
		{
		    el.togled = el.className.indexOf("toggled") != -1;
		}
		var table = DundasChart.getParentNodeByTag(el, "TABLE");
		if ( table && !table.widthFixed)
		{
			table.style.width  = (table.offsetWidth) + "px";
			table.widthFixed = true;
		}
		el.className = "ds_mnover";
	}
},

tbOutMnu: function ( el )
{
    if ( el )
    {
		if ( this.isTogled(el))
		{
			var attr = el.getAttribute("tgstyle");
		    el.className = attr ? attr : "ds_mntoggled";
		}
		else
		{
		    el.style.padding = "";
		    el.className = "ds_mn";
		}
	}
},

tbOver : function ( el )
{
	if ( el && el.onclick)
	{
		if ( typeof(el.togled) == 'undefined')
		{
		    el.togled = el.className.indexOf("toggled") != -1;
		}
		el.className = "ds_tbover";
	}
},

tbOut : function ( el )
{
	if ( el && el.onclick)
	{
		if ( this.isTogled(el))
		{
		    el.className = "ds_tbtoggled";
		}
		else
		{
		    el.className = "ds_tbbtn";
		}
	}
},

setTogled : function (el, state)
{
    if ( typeof(el) == "string")
    {
        el = document.getElementById(el);
    }
    if ( el )
    {
        el.togled = state;
        if ( el.className.indexOf("_mn") != -1)
        {
            el.className = state ? "ds_mntoggled" : "ds_mn";
        }
        else
        {
            el.className = state ? "ds_tbtoggled" : "ds_tbbtn";
        }
    }
},

isTogled : function (el)
{
    if ( el )
    {
        return typeof(el.togled) == "undefined" ? false : el.togled;
    }
    return false;
},

compileEvent : function ( clickEvent )
{
	var div = document.createElement("div");
	div.innerHTML = "<div onclick=\"" + clickEvent + "\" />";
	return div.firstChild.onclick;
},

setEnabled: function ( el, enabled, clickEvent, toolbarID)
{
    var idd = el.id;
    if(typeof(el.style.filter) != "undefined")
    {
        el.style.filter = enabled ? "" : "alpha(opacity=50)";
    }
    if(typeof(el.style.MozOpacity) != "undefined")
    {
        el.style.MozOpacity = enabled ? "" : "0.5";
    }
    if(typeof(el.style.opacity) != "undefined")
    {
		el.style.opacity = enabled ? "" : "0.5";
		if ( toolbarID )
		{
			var tb = document.getElementById(toolbarID);
			if ( tb )
			{
				tb.innerHTML = tb.innerHTML;
			}
		}
	}
	var el = document.getElementById(idd);
	if ( el )
	{
		if ( enabled )
		{
			el.onclick = ChartToolbar.compileEvent(clickEvent);
		}
		else
		{
			el.onclick = null;
		}
    }	
},

setState : function (el, togled, enabled, clickEvent, toolbarID )
{
    if ( typeof(el) == "string")
    {
        el = document.getElementById(el);
    }
    if ( el )
    {
		this.setTogled(el, togled);
		this.setEnabled(el, enabled, clickEvent, toolbarID);
    }
},



doPostBack : function( eventTarget, eventArgument, targetSite, actionUrl, encType )
{
    if ( theForm  && __doPostBack)
    {
        
        var saveActionUrl = theForm.action;
        var saveTargetSite = theForm.target;
		var saveEnctype = theForm.enctype;
		
        try 
        {
			
			if ( targetSite == "nonamePrintFrame")
			{
				targetSite = "_ds__print_id__";
				var frame = document.getElementById(targetSite);
				if ( !frame )
				{
					var div = document.createElement("DIV");
					div.style.width = "30px";
					div.style.height = "30px";
					div.style.position = "absolute";
					div.style.top = "-1000px";
					div.style.left = "-10000px";

                    if (document.location.protocol.toLowerCase() == "https:" && DundasChart.msieFlag)
                    {
                        // bug fix #7933
					    div.innerHTML = '<iframe src="." id="_ds__print_id__" name="_ds__print_id__" frameborder="0" width="30px" height="30px" ></iframe>';
					    /////
                    }
                    else
                    {
					    div.innerHTML = '<iframe src="about:blank" id="_ds__print_id__" name="_ds__print_id__" frameborder="0" width="30px" height="30px" ></iframe>';
                    }					    
					
					document.body.appendChild(div);
				}
			}
			
            if ((typeof(encType) != "undefined") && (encType != null) && (encType.length > 0)) {
				if ( typeof(theForm.encoding) != 'undefined' ) { theForm.encoding = encType; } else { theForm.enctype = encType; };	
            }
            if ((typeof(actionUrl) != "undefined") && (actionUrl != null) && (actionUrl.length > 0)) {
                theForm.action = actionUrl;
            }
            if ((typeof(targetSite) != "undefined") && (targetSite != null) && (targetSite.length > 0)) {
                theForm.target = targetSite;
            }

            theForm.__EVENTTARGET.value = eventTarget;
            theForm.__EVENTARGUMENT.value = eventArgument;
            
            theForm.submit();
            
            
            theForm.__EVENTTARGET.value = "";
            theForm.__EVENTARGUMENT.value = "";

            theForm.action = saveActionUrl;
            theForm.target = saveTargetSite;
			if ( typeof(theForm.encoding) != 'undefined' ) { theForm.encoding = saveEnctype; } else { theForm.enctype = saveEnctype; };	
        }
        catch( e )
        {
            theForm.__EVENTTARGET.value = "";
            theForm.__EVENTARGUMENT.value = "";
            theForm.action = saveActionUrl;
            theForm.target = saveTargetSite;
			if ( typeof(theForm.encoding) != 'undefined' ) { theForm.encoding = saveEnctype; } else { theForm.enctype = saveEnctype; };	
            alert( e.message );        
        }
    }
},

printChart : function ( chartUrl )
{
    if ( chartUrl.indexOf("&preview=yes") == -1)
    {
        var frame = document.getElementById("__print_id");
        if ( !frame )
        {
            var fr = document.createElement("IFRAME");
            fr.style.width  = "0px";
            fr.style.height = "0px";
            fr.style.top = "-10px";
            fr.id = "__print_id";
            fr.src = chartUrl;
            document.body.appendChild(fr);
        }
        else
        {
            frame.src = chartUrl;    
        }
    }
    else
    {
       window.open( chartUrl.replace("&preview=yes",""), "_blank");//, "location=no, menubar=no, toolbar=yes");   
    }
    
},

updateCmdControl : function( element, controlID, commandName)
{
	ChartToolbar.closeMenu();
	var control = document.getElementById( controlID);
	var imagesSrc = element.getElementsByTagName("IMG");
	var imagesDst = control.getElementsByTagName("IMG");
	imagesDst[0].src = imagesSrc[0].src;
	var cmdNames = commandName.split("@");
	control.getElementsByTagName("INPUT")[0].value = cmdNames[0];
	control.getElementsByTagName("SPAN")[0].innerHTML = cmdNames[1];
},

copyClpBoard : function ( content)
{
	try
	{
	var div = document.createElement("DIV");
		div.innerHTML = content;
		div.style.position = "absolute";
		div.style.top = "-1000px";
		div.style.left = "-1000px";
		document.body.appendChild( div );
		div.contentEditable = 'true';
		if (document.body.createControlRange) {
			var controlRange = document.body.createControlRange();
			if (div.childNodes.length == 1)
			{
				controlRange.addElement(div.firstChild);
			}
			else
			{
				controlRange.addElement(div);
			}
			window.setTimeout( function() { controlRange.execCommand('Copy'); }, 100);
		}
		
		window.setTimeout( function() { div.contentEditable = 'false'; document.body.removeChild( div );}, 1000);
	}
	catch (e) { alert( e.message ) }
},

loadChartComplete : function ( e, chartId, params)
{
	DundasChart.topDialog().close();
	ChartCB.call(e, chartId, params);
}

}

var Dundas_PP = {

	btnOver : function (item, flag)
	{
		if ( item.className.indexOf("ds_ppbtn_selected") != -1 )
		{
			return;
		}
		if ( !flag )
		{
			item.className = item.className.replace(/ds_ppbtn_over/g, "ds_ppbtn_normal");
		}
		else
		{
			item.className = item.className.replace(/ds_ppbtn_normal/g, "ds_ppbtn_over");
		}
	},
	
	selectPage: function( item )
	{		
			if ( item.className == "ds_ppbtn_selected" )
			{
				return;
			}
			var cell = item;
			while( cell.previousSibling ) { cell = cell.previousSibling; }
			while( cell ) { 
				if ( cell.className == "ds_ppbtn_selected")
				{
					cell.className = "ds_ppbtn_normal"; 
					Dundas_PP.displayPanel( cell, "none");
				}
				cell = cell.nextSibling; 
			}
			item.className = "ds_ppbtn_selected";
			Dundas_PP.displayPanel( item, "block");
	},
	
	displayPanel : function (item, displayStyle )
	{
		var panel = document.getElementById(item.getAttribute("pagename"));
		if ( panel )
		{
			panel.style.display = displayStyle;
		}
	},

	close : function( button )
	{
		DundasChart.topDialog().close();
	},
	
	pick  : function( button)
	{
		alert( button.value);
	},
	
	nEditMarkChanged : function (validator)
	{
		var element = document.getElementById(validator.elementName + "Tag");
		if ( element )
		{
			element.value = "Y";
		}
	},
	
	nEditCheck : function (validator)
	{
		var element = document.getElementById(validator.elementName);
		var value = parseInt(element.value);
		if ( isNaN(value))
		{
			alert("Invalid value!");
			element.select();
			return false;
		}
		if ( !isNaN(validator.max) && value > validator.max)
		{
			alert("Invalid value!");
			element.select();
			return false;
		}
		if ( !isNaN(validator.min) && value < validator.min)
		{
			alert("Invalid value!");
			element.select();
			return false;
		}
		return true;
	},
	
	nEditKey : function ( e, validator )
	{
	var code;
		Dundas_PP.nEditMarkChanged( validator);
		if (!e) var e = window.event;
		if (e.keyCode) code = e.keyCode;
		else if (e.which) code = e.which;
		if ( "8,37,38,39,40".indexOf(code.toString()) != -1)
		{
			return true;
		}
		var character = String.fromCharCode(code);
		return (validator.delimiter + "0123456789-").indexOf(character) != -1;
	},
	
	nEditInc : function (validator, increment)
	{
		var element = document.getElementById(validator.elementName);
		if ( element.disabled )
		{
			return void(0);
		}
		var value = parseInt(element.value);
		if ( !isNaN(	value ))
		{
			if ( increment )
			{
				if ( value <= (validator.max - validator.increment))
				{
					value += validator.increment;
				}
			}
			else
			{
				if ( value >= (validator.min + validator.increment))
				{
					value -= validator.increment;
				}
			}
			element.value = value;
			Dundas_PP.nEditMarkChanged( validator);	
			if ( element.onchange )
			{
				element.onchange(element);
			}			
		}	
	},
	
	panelSw : function ( element )
	{
		var panel = element.nextSibling;
		if ( panel.nodeType == 3 )
		{
			panel = panel.nextSibling
		}
		panel.style.display = panel.style.display == "block" ? "none" : "block";
		element.getElementsByTagName("SPAN")[0].innerHTML = panel.style.display == "block" ? "[--]" : "[+]";
		return void(0);
	},

	disableCnt : function ( tagName, contr, disable, uelms)
	{
		var index = 0;
		var update = true;
		var inputs = contr.getElementsByTagName(tagName);
		for( index = 0; index < inputs.length; index ++)
		{
			update = true;
			var elm = inputs[index];
			for( var ind1 = 0; ind1 < uelms.length; ind1 ++)
			{
				if ( elm == uelms[ind1] )
				{
					update = false;
				}
			}
			if ( update && typeof(elm.disabled) != 'undefined')
			{
				var cntElement = elm.parentNode;
				elm.disabled = disable;
				if ( cntElement && cntElement.tagName && cntElement.tagName.toLowerCase() == "span")
				{
					cntElement.disabled = disable;
				}
			}
		}
	},		
		
	disableElm : function (containerName, disable, unchControlsName)
	{
		var contrs = containerName.split(",");
		var ucontrs = new Array();
		if ( unchControlsName )
		{
			ucontrs = unchControlsName.split(",");
		}
		var uelms = new Array();
		for( var indx = 0; indx < ucontrs.length; indx ++ )
		{
			uelms[uelms.length] = document.getElementById(ucontrs[indx]);
		}
		for( var indx = 0; indx < contrs.length; indx ++ )
		{
			var contr = document.getElementById(contrs[indx]);
			if ( contr )
			{
				Dundas_PP.disableCnt("INPUT", contr, disable, uelms);
				Dundas_PP.disableCnt("SELECT", contr, disable, uelms);
			}
		}
	},

	callClrDlg : function( e, chartName, cmd, ctrlName )
	{
		return ChartCB.call(e, chartName, cmd + ctrlName + "/" + document.getElementById(ctrlName+"Value").value);
	},

	updateGC : function( e, chartName, cmd, ctrlNameColors, ctrlNameLists )
	{
		var aCtrlNameColors = ctrlNameColors.split(",");
		var aCtrlNameLists  = ctrlNameLists.split(",");
		var colors = document.getElementById(aCtrlNameColors[0] + "Value").value + ";" +
					 document.getElementById(aCtrlNameColors[1] + "Value").value;
		for( var ind = 0; ind < aCtrlNameLists.length; ind ++)
		{
			var elmName = aCtrlNameLists[ind].split("!")[0];					 
			aCtrlNameLists[ind] += "!" + document.getElementById(elmName + "Value").value + 
								   "!" + document.getElementById(elmName).offsetWidth;			 
		} 
		
		return ChartCB.call(e, chartName, cmd + aCtrlNameLists.join(",") + "/" + colors);
	},
	
	getParentByTag : function ( o, tagName)	
	{
		if ( o == null )
		{
			return null;
		}	
        var parent = o.parentNode;
        var upperTagName = tagName.toUpperCase();
        while (parent && parent.tagName && (parent.tagName.toUpperCase() != upperTagName)) {
            parent = parent.parentNode ? parent.parentNode : parent.parentElement;
        }
        return parent;
	},
		
	selectErrorBar : function( element, index )
	{
		var tr = Dundas_PP.getParentByTag(element, "TABLE");
		if ( tr )
		{
			var inps = tr.getElementsByTagName("INPUT");
			if ( inps.length > index )
			{
				inps[index].checked = !inps[index].checked;
			}
		} 	
	},
	
	disableError : function(element)
	{
		var inpElement = null;
		var tr = Dundas_PP.getParentByTag(element, "TR");
		if ( tr )
		{
			var inps1 = tr.getElementsByTagName("INPUT");
			if ( inps1.length > 1 )
			{
				inpElement = inps1[1];
				var table = Dundas_PP.getParentByTag(element, "TABLE");
				var inps2 = table.getElementsByTagName("INPUT");
				for( var i = 0; i < inps2.length; i++)
				{
					if ( inps2[i].type.toLowerCase() == "text")
					{
						inps2[i].disabled = inpElement != inps2[i];
					}
				}
			}
		}		
	},
	
	disableContrById : function( elementNames, flag )
	{
		var elements = elementNames.split(",");
		for( var idx = 0; idx < elements.length; idx ++)
		{
			var el = document.getElementById(elements[idx]);
			if ( el )
			{
				el.disabled = flag;
			}
		}
	},
	
	processFmls : function( element )
	{
		var table = Dundas_PP.getParentByTag(element, "TABLE");
		var inputs = table.getAttribute("editors");
		if ( inputs )
		{
			Dundas_PP.disableContrById( inputs, true);
			inputs = inputs.split(",");
			if ("None,TypicalPrice,MedianPrice,WeightedClose".indexOf(element.value) == -1 )
			{
				Dundas_PP.disableContrById( inputs[0], false);
			}
			if (element.value == "Envelopes")
			{
				Dundas_PP.disableContrById( inputs[2], false);
			}
			if (element.value == "Forecasting")
			{
				Dundas_PP.disableContrById( inputs[1], false);
			}
			if (element.value.indexOf("Bands") != -1)
			{
				Dundas_PP.disableContrById( inputs[3], false);
			}
		}
		var seriesData = table.getAttribute("series");
		if (seriesData)
		{	
			seriesData = seriesData.split(",");
			var checkBox = document.getElementById(seriesData[1]);
			if (checkBox && checkBox.checked )
			{
				var text = element.getAttribute("legendText") + " (" + seriesData[0] + ")";;
				if ( element.getAttribute("legendText") == null )
				{
					text = "";
				}
				if (checkBox.checked)
				{
					document.getElementById(seriesData[2]).value = text;
				}
			}
		}
	},
	
	processFmlsCheck : function( element, tableID, legendTextID)
	{
		document.getElementById(legendTextID).disabled = element.checked;
		var elements = document.getElementById(tableID).getElementsByTagName("INPUT");
		if ( element.checked )
		{
			for( var idx = 0; idx < elements.length; idx ++)
			{
				if(elements[idx].checked)
				{
					Dundas_PP.processFmls(elements[idx]);
					return;
				}
			}
		}
	}		
}     

var DundasListBox = {
    
		currentList : null,
		installed : false,		
		
		close : function (e)
		{
			if ( DundasListBox.currentList != null )
			{
				DundasListBox.pullDown( e, DundasListBox.currentList );
			}
		},
		
		cancelEvent : function (e)
		{
			if (!e) var e = window.event
			if ( e )
			{
				e.cancelBubble = true;
				if (e.stopPropagation) e.stopPropagation();    
			}
			return false;
		},		
		
		pullDown : function (e, element)
		{
			if ( element.parentNode && (element.parentNode.disabled || element.parentNode.getAttribute("disabled") == "disabled"))
			{
				return;
			}			
			DundasListBox.cancelEvent(e);
			var table = element.nextSibling;
			if ( table.nodeType == 3 )
			{
				table = table.nextSibling
			}
			if ( table.style.display == "block" )
			{
				table.style.display = "none";
				DundasListBox.currentList = null;
			}
			else
			{
				DundasListBox.close(e);
				table.style.display = "block";
				DundasListBox.currentList = element;
				if ( !DundasListBox.installed )
				{
					DundasChart.attachEvent(document, "click", DundasListBox.close);
					DundasListBox.installed = true;
				}
			}
		},
	     
		over : function (element)
		{
			element.className = "ds_list_div_o";
		},
	     
		out : function (element)
		{
			element.className = "ds_list_div";
		},
	     
		click : function (element)
		{
			var list = element.parentNode;    
			var control = list.parentNode;    
			control.getElementsByTagName("INPUT")[0].value = element.getElementsByTagName("SPAN")[0].innerHTML;
			control.getElementsByTagName("INPUT")[1].value = "Y";
			control.getElementsByTagName("DIV")[0].innerHTML = element.getElementsByTagName("SPAN")[1].innerHTML;
			control.getElementsByTagName("IMG")[0].src = element.getElementsByTagName("IMG")[0].src;
			list.style.display = "none";
			DundasListBox.currentList = null;
		}
    
    }


function DundasColorSelectObj(cntrName)
{
    
    this.cntrName = cntrName;
    this.selectedTr = null;
    this.currentColor = "";
    
    this.colorClick = function ( element)
    {
        var ex = element.getElementsByTagName("DIV");
        var color = "";
        var transp = "";
        if ( ex.length > 0 )
        {
            color = ex[0].style.backgroundColor;
            transp = ex[0].getAttribute("t");
        }  
        if ( this.selectedTr == null )
        {
            var trs = document.getElementById(this.cntrName + "_Table1").getElementsByTagName("TR");
            for( var idx = 0; idx < trs.length; idx++)
            {
                if ( trs[idx].className == "ds_clr_sel")
                {
                    trs[idx].className = "";
                    break;
                }
            }
        }
        else
        {
            this.selectedTr.className = "";
        }
        element.className = "ds_clr_sel";
        this.selectedTr = element;
        this.setColor( color, transp);
    }    
    
    this.getColorElement = function ( edName)
    {
        return document.getElementById(this.cntrName + "_TextBox" + edName + "Value");
    }
    
    this.parseColorEditor = function( edName, value)
    {
        var elm = this.getColorElement(edName);
        var v = parseInt(value);
        if ( !isNaN(v) )
        {
            elm.value = v.toString();
        }
        else
        {
            elm.value = "";
        }
        return elm
    };

	this.displayColor = function( displayElementName, color, transp )
	{
        var sample = document.getElementById(displayElementName);
        if( sample )
        {
            sample.style.backgroundColor = color;
            var trans = Math.round( 100 - ( parseInt(transp) / 2.55));
            //(int)(100 - (Color.A / 2.55));
            if ( !isNaN(trans))
            {
                if ( typeof(sample.filters) != "undefined" )
                {
                    sample.filters.item("alpha").opacity = (100-trans);
                    //sample.style.filter = "alpha(opacity = " +(100-trans)+");";
                }
                else if ( typeof(sample.style.opacity) != "undefined" )
                {
                    sample.style.opacity = (1 - trans/100);
                }
                else if ( typeof(sample.style.MozOpacity) != "undefined" )
                {
                    sample.style.MozOpacity = (1 - trans/100);
                }
            }
        }
	
	};
    
    this.setColor = function (color, transp)
    {
        this.currentColor = color;
        if ( color == "" )
        {
            this.parseColorEditor("R", "")
            this.parseColorEditor("G", "")
            this.parseColorEditor("B", "")
            this.parseColorEditor("T", "")
        }
        else if ( color.indexOf("rgb(") == 0)
        {
            var colors = color.replace("rgb(","").replace(")","").split(",");
            this.parseColorEditor("R", colors[0])
            this.parseColorEditor("G", colors[1])
            this.parseColorEditor("B", colors[2])
            this.parseColorEditor("T", transp)
        }
        else if ( color.indexOf("#") == 0)
        {
            this.parseColorEditor("R", "0x" + color.substring(1,3))
            this.parseColorEditor("G", "0x" + color.substring(3,5))
            this.parseColorEditor("B", "0x" + color.substring(5,7))
            this.parseColorEditor("T", transp)
        }
        this.displayColor(this.cntrName + "_Sample", color, transp);
    };
	
	this.getColorFromEditors = function ()
	{
		var v = this.getValues();
		if ( isNaN( v.r ) || isNaN( v.g ) || isNaN( v.b ) )
		{
			return "";
		}
		return "rgb(" + v.r +"," + v.g +"," + v.b +")"
	};

	this.getFullFromEditors = function ()
	{
		var v = this.getValues();
		if ( isNaN( v.r ) || isNaN( v.g ) || isNaN( v.b ) || isNaN( v.t ))
		{
			return "";
		}
		//var tt = Math.round((100 - parseInt(v.t)) * 2.55)
		var tt = parseInt(v.t);
		return tt +"," + v.r +"," + v.g +"," + v.b
	};

	this.getTranspFromEditors = function ()
	{
		var v = this.getValues();
		if ( isNaN( v.t ) )
		{
			return "";
		}
		return v.t
	};
	
	this.getValues = function ()
	{
		var result = new Object();	
        result.r = parseInt(this.getColorElement("R").value);
        result.g = parseInt(this.getColorElement("G").value);
        result.b = parseInt(this.getColorElement("B").value);
        result.t = parseInt(this.getColorElement("T").value);
        return result;
	};

    this.updateColorPanel = function()
    {
		var v = this.getValues();
        this.setColor( this.getColorFromEditors(), this.getTranspFromEditors());        
    }
    
    this.applyUpdate = function() { return true};    
    
    this.apply = function ( applyToName)
    {
		if (this.validateInput(this.getColorElement("R")) &&	
			this.validateInput(this.getColorElement("G")) &&	
			this.validateInput(this.getColorElement("B")) &&	
			this.validateInput(this.getColorElement("T")) )
		{
			var element = document.getElementById(applyToName);
			if ( element )
			{
				var imp = document.getElementById(applyToName + "Value");
				var result = this.getFullFromEditors();
				var shouldFire = imp.value != result;
				imp.value = this.getFullFromEditors();
				this.displayColor(applyToName + "Display", this.getColorFromEditors(), this.getTranspFromEditors());
				DundasChart.topDialog().close();
				if ( shouldFire && typeof(imp.onchange) == "function" )
				{
					imp.onchange();
				}
			}
		}
		return false;
    }
    
    this.close = function()
    {
		DundasChart.topDialog().close();
		return false;
    }
	
	this.validateInput = function (input)
	{
        //var topValue = input.id.substring(input.id.length-6) == "TValue" ? 100 : 255;
        var topValue = 255;
        if (input.value.length > 0 )
        {
			var res = parseInt(input.value);
			if ( isNaN( res ) || res < 0 || res > topValue)
			{
				alert( "Invalid value!(0-"+topValue+")");
				input.select();
				return false;
			}
        }
        return true;
	}
	    
    this.validate =  function( input)
    {
        if ( input.value.length == 0 )
        {
            this.updateColorPanel();
            return true;
        }
        if ( this.validateInput( input ))
        {
			this.updateColorPanel();        
			return true;
        }
        return false;
    }    
}

function DundasSelectFontObj(cntrName)
{
    if (cntrName.indexOf('Outer')>-1)
    {
        cntrName = cntrName.substr(0,cntrName.indexOf('Outer'));
    }
    
    this.cntrName = cntrName;
    this.close = function()
    {
		DundasChart.topDialog().close();
		return false;
    }

    this.getControlElement = function ( edName)
    {
        return document.getElementById(this.cntrName + edName);
    }

    this.apply = function ( applyToName)
    {
		var fontName = this.getControlElement("FontName").value;
		var fontSize = this.getControlElement("FontSize").value;
		var styles = new Array();
		var stylesNames = "Bold,Italic,Underline,Strikeout".split(",");
		
		for( var i = 0; i < stylesNames.length; i++)
		{
			if ( this.getControlElement(stylesNames[i]).checked )
			{
				styles.push(stylesNames[i]);
			}
		}
		
		var result = fontName + "," + fontSize + "pt";
		if ( styles.length > 0 )
		{
			result += ", style=" + styles.join(", "); 
		}

		var element = document.getElementById(applyToName);
		if ( element )
		{
			var imp = document.getElementById(applyToName + "Value");
			var shouldFire = imp.value != result;
			imp.value = result;
			document.getElementById(applyToName + "Display").innerHTML = result;
			if ( shouldFire && typeof(imp.onchange) == "function" )
			{
				imp.onchange();
			}
		}
		DundasChart.topDialog().close();		
		return false;
    }
}

if(typeof(Sys) != 'undefined')
{
    if(Sys.Application )
    {
        Sys.Application.notifyScriptLoaded();
    }
}
