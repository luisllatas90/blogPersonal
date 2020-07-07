
var ___array___ = new Array();

if ( typeof(___array___.push) == "undefined")
{
    Array.prototype.push = function (item) { this[this.length] = item };
    Array.prototype.pop  = function () { 
        if ( this.length > 0 )
        {
            var result = this[this.length-1]; 
            this.length --;
            return result;
        }
        return null;    
    };
}


var DundasChart = {

browserVersion : -1,
msieFlag: false,
geckoFlag: false,
operaFlag: false,
supportPng: true,
msieVer: 0,
ffVer: 0,

getInternetExplorerVersion: function()
// Returns the version of Internet Explorer or a -1
// (indicating the use of another browser).
{
  var rv = -1; // Return value assumes failure.
  if (navigator.appName == 'Microsoft Internet Explorer')
  {
    var ua = navigator.userAgent;
    var re  = new RegExp("MSIE ([0-9]{1,}[\.0-9]{0,})");
    if (re.exec(ua) != null)
      rv = parseFloat( RegExp.$1 );
  }
  return rv;
},


init: function(){
    DundasChart.browserVersion = parseInt(window.navigator.appVersion.charAt(0), 10);
	DundasChart.msieFlag  = window.navigator.userAgent.indexOf("MSIE") > -1;
	DundasChart.geckoFlag = window.navigator.userAgent.indexOf("Gecko") > -1;
	DundasChart.operaFlag = window.navigator.userAgent.toLowerCase().indexOf("opera") > -1;
	DundasChart.safariFlag = window.navigator.userAgent.toLowerCase().indexOf("safari") > -1;
	
    if (DundasChart.geckoFlag)
    {
        DundasChart.ffVer = parseInt(window.navigator.appVersion);
    }
	
	if ( DundasChart.operaFlag )
	{
		DundasChart.operaFlag = parseInt(window.navigator.appVersion) < 9;
		DundasChart.geckoFlag = true;
		DundasChart.ffVer = parseInt(window.navigator.appVersion);		
	}
	
	DundasChart.supportPng = DundasChart.geckoFlag || DundasChart.operaFlag;
	DundasChart.msieVer    =  0;
	if (DundasChart.msieFlag)
	{
	    var ind = navigator.userAgent.indexOf("MSIE ");
	    var sv = navigator.userAgent.substring(ind+5,ind+8);
	    DundasChart.msieVer = parseFloat(sv);
	    DundasChart.supportPng = DundasChart.msieVer >= 7.0;
	}
},


fixMouseEvent : function ( e )
{
   var docX, docY;
   if( e && !DundasChart.msieFlag)
   {
      if( typeof( e.pageX ) == 'number' )
      {
         docX = e.pageX;
         docY = e.pageY;
      }
      else
      {
         docX = e.clientX;
         docY = e.clientY;
      }
   }
   else
   {
      e = window.event;
      docX = e.clientX;
      docY = e.clientY;
      if( document.documentElement
        && ( document.documentElement.scrollTop
            || document.documentElement.scrollLeft ) )
      {
         docX += document.documentElement.scrollLeft;
         docY += document.documentElement.scrollTop;
      } 
      else if( document.body
         && ( document.body.scrollTop
             || document.body.scrollLeft ) )
      {
         docX += document.body.scrollLeft;
         docY += document.body.scrollTop;
      }
   }
   e.docX = docX;
   e.docY = docY;
   return e;
},

fixEvent : function( e )
{	
	if ( e && typeof(e.fixedEvent) == "boolean" && e.fixedEvent)
	{
		return e;
	}
	e = DundasChart.fixMouseEvent( e );		
	if (typeof(e.layerX) == 'undefined') e.layerX = e.offsetX;
	if (typeof(e.layerY) == 'undefined') e.layerY = e.offsetY;
	if (typeof(e.target) == 'undefined') e.target = e.srcElement;
	if (e.target.nodeType == 3) e.target = e.target.parentNode;
	e.fixedEvent = true;
	return e;
},
	
attachEvent : function(obj, evType, fn, useCapture){
	if (obj.addEventListener)
	{
		obj.addEventListener(evType, fn, useCapture == true);
		return true;
	}
	else if (obj.attachEvent)
	{
		var r = obj.attachEvent("on"+evType, fn);
		if ( useCapture && obj.setCapture)
		{
			obj.setCapture();
		}		
		return r;
	}
	else 
	{
		return false;
	}
},

cancelEvent : function (e)
{
    if (!e) var e = window.event
    if ( e )
    {
        e.cancelBubble = true;
        if (e.stopPropagation) e.stopPropagation();    
        e.returnValue = false;
        e.donotClick  = true;
        if ( e.cancelable && e.preventDefault)
        {
			e.preventDefault();
        }
    }
    return false;
},

getScrollPos : function ()
{
    var result = {top:0, left:0};
    result.top  = document.body.scrollTop;
    result.left = document.body.scrollLeft;
    if ( document.documentElement )
    {
        result.top  = Math.max(result.top,document.documentElement.scrollTop);
        result.left = Math.max(result.left,document.documentElement.scrollLeft);
    }
    return result;
},

getElementWidth : function (el)
{
    if ( el.style && el.style.width )
    {
        return parseInt(el.style.width);
    }
    return el.clientWidth;
},

getElementHeight : function (el)
{
    if ( el.style && el.style.height )
    {
        return parseInt(el.style.height);
    }
    return el.clientHeight;
},

detachEvent: function (obj, evType, fn, useCapture){
	if (obj.removeEventListener)
	{
		obj.removeEventListener(evType, fn, useCapture == true);
		return true;
	}
	else if (obj.detachEvent)
	{
		var r = obj.detachEvent("on"+evType, fn);
		if ( useCapture && obj.setCapture)
		{
			try
			{
				obj.setCapture(false);
			} catch (e) {}
		}		
		return r;
	}
	else
	{
		alert("Handler could not be removed");
	}
},

mouseSetCapture : function( element, moveFunc, upFunc )
{
	var connectionElement = element;
	DundasChart.attachEvent( connectionElement, "mousemove", moveFunc, true);
	DundasChart.attachEvent( connectionElement, "mouseup", upFunc, true);
	
},

mouseReleaseCapture : function( element, moveFunc, upFunc )
{
	var connectionElement = element;
	DundasChart.detachEvent( connectionElement, "mousemove", moveFunc, true);
	DundasChart.detachEvent( connectionElement, "mouseup", upFunc, true);
},

getScreenHeight: function () {
	if (window.innerHeight!=window.undefined) return window.innerHeight;
	if (document.compatMode=='CSS1Compat') return document.documentElement.clientHeight;
	if (document.body) return document.body.clientHeight; 
	return window.undefined; 
},
			
getScreenWidth : function () {
	if (window.innerWidth!=window.undefined) return window.innerWidth; 
	if (document.compatMode=='CSS1Compat') return document.documentElement.clientWidth; 
	if (document.body) return document.body.clientWidth; 
	return window.undefined; 
},

getFirstChild : function ( element)
{
    var result = element.firstChild;
    if (result && result.nodeType == 3)
    {
        return result.nextSibling;
    }
    return result;
},

tabIndexes   : new Array(),

disableTabs: function () {
	if (document.all) {
		var tabs = new Array("A","BUTTON","TEXTAREA","INPUT","IFRAME");
		storage = new Array();
		for (var i = 0; i < tabs.length; i++) {
			var tagElements = document.getElementsByTagName(tabs[i]);
			for (var j = 0 ; j < tagElements.length; j++) {
				var data = new Object();
				data.element  = tagElements[j];
				data.tabIndex = tagElements[j].tabIndex;
				storage[storage.length] = data;
				tagElements[j].tabIndex="-1";
			}
		}
		DundasChart.tabIndexes = storage;
	}
},


restoreTabs: function () {
	if (document.all) {
		for( var i = 0; i < DundasChart.tabIndexes.length; i++)
		{
			var elm = DundasChart.tabIndexes[i].element;
			elm.tabIndex = DundasChart.tabIndexes[i].tabIndex;
		}
	}
},

hidenSelects : null,

hideSelects : function () {
	if ( DundasChart.msieFlag && DundasChart.msieVer < 7)
	{
		var storage = new Array();
		for(var i = 0; i < document.forms.length; i++) {
			for(var e = 0; e < document.forms[i].length; e++){
				if(document.forms[i].elements[e].tagName == "SELECT") {
					var elm = document.forms[i].elements[e];
					if ( elm.style.visibility != "hidden" )
					{
						try
						{
						storage[storage.length] = new Array( elm, elm.style.visibility);
						elm.style.visibility = "hidden";
						} catch(exc) {;}
					}
				}
			}
		}
		DundasChart.hidenSelects = storage;
	}
},

restoreSelects: function () {
	if ( DundasChart.msieFlag && DundasChart.hidenSelects != null)
	{
		var storage = DundasChart.hidenSelects;
		for(var i = 0; i < storage.length; i++) {
			var data = storage[i]; 
			data[0].style.visibility=data[1];
		}
	}
},

dialogOrder : 19000,

nextOrder : function()
{
	return DundasChart.dialogOrder++;
},

releaseOrder : function()
{
	DundasChart.dialogOrder--;
},

dialogMasks   : new Array(),

createDlgMask : function()
{
	var mask = document.createElement("DIV");
	mask.className = "ds_pp_mask";
	mask.style.backgroundImage = "url(" + __DundasScrBrImgBag["empty"].src + ")";
	if ( DundasChart.popupDialogs.length > 0 )
	{
		var dlg = DundasChart.popupDialogs[DundasChart.popupDialogs.length-1];
		mask.style.height = dlg.container.offsetHeight + "px";
		mask.style.width = dlg.container.offsetWidth + "px";
		mask.style.top = "0px";
		mask.style.left = "0px";
		mask.style.display = "block";
		dlg.container.appendChild( mask);
	}
	else
	{
		var fullHeight = DundasChart.getScreenHeight();
		var fullWidth = DundasChart.getScreenWidth();
		var scroll = DundasChart.getScrollPos();
		mask.zIndex = DundasChart.nextOrder();
		mask.style.height = fullHeight + "px";
		mask.style.width = fullWidth + "px";
		mask.style.top = scroll.top + "px";
		mask.style.left = scroll.left + "px";
		mask.style.display = "block";
		document.body.appendChild( mask);
		
		DundasChart.attachEvent(window, "resize", DundasChart.centerDlgMask);
		DundasChart.attachEvent(window, "scroll", DundasChart.centerDlgMask);		
		
	}
	DundasChart.dialogMasks.push(mask);
	DundasChart.attachEvent(mask, "mousedown", DundasChart.blinkDialog);
	DundasChart.attachEvent(mask, "mouseup", DundasChart.blinkDialogA);	
	
},

blinkDialogA: function(color)
{
	var dialog = DundasChart.topDialog();
	if ( dialog && dialog.dialogTitle)
	{
		dialog.container.style.borderColor = "#a9a9a9";
	}
},

blinkDialog : function()
{
	var dialog = DundasChart.topDialog();
	if ( dialog && dialog.dialogTitle)
	{
		dialog.container.style.borderColor = "#ffffff";
	}
},

centerDlgMask : function ()
{
	if ( DundasChart.dialogMasks.length > 0 )
	{
		var mask = DundasChart.dialogMasks[DundasChart.dialogMasks.length - 1];
		var fullHeight = DundasChart.getScreenHeight();
		var fullWidth = DundasChart.getScreenWidth();
		var scroll = DundasChart.getScrollPos();
		mask.style.height = fullHeight + "px";
		mask.style.width = fullWidth + "px";
		mask.style.top = (scroll.top-1) + "px";
		mask.style.left = (scroll.left-1) + "px";
	}
},

releaseDlgMask : function()
{
	var mask = DundasChart.dialogMasks.pop();
	if ( mask && mask.parentNode)
	{
		mask.parentNode.removeChild(mask);
		DundasChart.releaseOrder();
	}
	if ( DundasChart.dialogMasks.length == 0 )
	{
		DundasChart.detachEvent(window, "resize", DundasChart.centerDlgMask);
		DundasChart.detachEvent(window, "scroll", DundasChart.centerDlgMask);
	}	
},

//*******************************************************

createElementID : function ( tag, id) {
	var result = document.createElement( tag);
	result.id = id;
	result.name = id;
	return result;
},

adjustPosition : function(e_x, e_y, element)
{
	var e_x = parseInt(e_x);
	var e_y = parseInt(e_y);
	var wnd_height=document.body.clientHeight;
	var wnd_width=document.body.clientWidth;
	if ( document.documentElement )
	{
	    wnd_height = Math.max(wnd_height,document.documentElement.clientHeight);
	    wnd_width  = Math.max(wnd_width,document.documentElement.clientWidth);
	}
	var tooltip_width =(element.style.pixelWidth) ? element.style.pixelWidth  : element.offsetWidth;
	var tooltip_height=(element.style.pixelHeight)? element.style.pixelHeight : element.offsetHeight;
	
    if ( (tooltip_width == 0 || tooltip_height == 0) )
	{
	    var elmnt = element.firstChild;
	    if ( elmnt )
	    {
	        tooltip_width =(elmnt.style.pixelWidth) ? elmnt.style.pixelWidth  : elmnt.offsetWidth;
	        tooltip_height=(elmnt.style.pixelHeight)? elmnt.style.pixelHeight : elmnt.offsetHeight;
	    }
	}
		
	var scroll = DundasChart.getScrollPos();
	
	var offset_y = (e_y + tooltip_height - scroll.top + 30 >= wnd_height) ? - tooltip_height: 0;
	var offset_x = (e_x + tooltip_width  - scroll.left + 30 >= wnd_width) ? - tooltip_width: 0;

	element.style.left = (e_x + offset_x) + "px";
	element.style.top = (e_y + offset_y) + "px";
	element.style.display = "block";
},


getEventXY : function (e)
{
    if (!e) var e = window.event
    if ( typeof e.offsetX == 'undefined')
    {
        var pos = DundasChart.getElmPosition( e.target);
        return {x:(e.pageX - pos.x) , y:(e.pageY - pos.y)};
    }
    return {x:e.offsetX , y:e.offsetY};
},

getElmPosition : function ( element, topElement, absolute ){
	var result = { x:0, y:0, w:0, h:0, lastParent: null};
	if (element == null) { return result; }

    if (element.offsetParent) {

        result.x = element.offsetLeft;
        result.y = element.offsetTop;
		var parent = element.offsetParent;
		result.lastParent = parent;
        if ( topElement && parent == topElement)
        {
			parent = null;
        }
        while (parent) {
            result.x += parent.offsetLeft;
            result.y += parent.offsetTop;
            var parentTagName = parent.tagName.toLowerCase();
            if (parentTagName != "table" &&
                parentTagName != "body" && 
                parentTagName != "html" && 
                parentTagName != "div" && 
                parent.clientTop && 
                parent.clientLeft) {
                
                result.x += parent.clientLeft;
                result.y += parent.clientTop;
            }
			if (absolute)
			{
				if (typeof(parent.scrollLeft) == "number")
				{
					result.x -= parent.scrollLeft;
				}
				if (typeof(parent.scrollTop) == "number")
				{
					result.y -= parent.scrollTop;
				}
			}
            parent = parent.offsetParent;
			result.lastParent = parent;
            if ( topElement && parent == topElement)
            {
				break;
            }
        }
    }
    else if (element.left && element.top) {
        result.x = element.left;
        result.y = element.top;
    }
    else {
        if (element.x) {
            result.x = element.x;
        }
        if (element.y) {
            result.y = element.y;
        }
    }
    if (element.offsetWidth && element.offsetHeight) {
        result.w = element.offsetWidth;
        result.h = element.offsetHeight;
    }
    else if (element.style && element.style.pixelWidth && element.style.pixelHeight) {
        result.w = element.style.pixelWidth;
        result.h = element.style.pixelHeight;
    }
    return result;
},

fixChartFrame: function( chartFrame)
{
	var el = document.getElementById(chartFrame);
	var w = el.clientWidth;
	var h = el.clientHeight;
	var src = el.src.substr(0, el.src.indexOf("&w")) + "&w=" + w + "&h=" + h;
	el.src = src;
	el.style.visibility = "visible";
},

getParentNodeByTag : function ( node, tagName )
{
	if ( node )
	{
		var parentNode = node.parentNode;		
		while( parentNode && parentNode.tagName.toUpperCase() != tagName )
		{
			parentNode = parentNode.parentNode;		
		} 
		return parentNode;
	}
	return null;
},


startFunctions : new Array(),

addLoadFunc : function ( func )
{
    DundasChart.startFunctions[DundasChart.startFunctions.length] = func;
},

startUpFlag : false,
startUp: function()
{
	if ( !DundasChart.startUpFlag )
	{
		
		var arr = DundasChart.startFunctions;
		for( var i = 0; i < arr.length; i++)
		{
			arr[i]();	
		}

		if ( typeof(_axd1a) != "undefined" )
		{
			for( var i = 0; i < _axd1a.length; i++)
			{
				_axd1a[i]();	
			}
		}
		
		DundasChart.startUpFlag = true;
	}
},

popupDialogs : new Array(),

topDialog : function()
{
	return DundasChart.popupDialogs[DundasChart.popupDialogs.length-1];
},

htmlDecode : function (s)
{
	var out = "";
	if (s==null) return;
	var l = s.length;
	for (var i=0; i<l; i++)
	{
		var ch = s.charAt(i);
		if (ch == '&') 
		{
			var semicolonIndex = s.indexOf(';', i+1);
            if (semicolonIndex > 0) 
            {
				var entity = s.substring(i + 1, semicolonIndex);
				if (entity.length > 1 && entity.charAt(0) == '#') 
				{
					if (entity.charAt(1) == 'x' || entity.charAt(1) == 'X')
						ch = String.fromCharCode(eval('0'+entity.substring(1)));
					else
						ch = String.fromCharCode(eval(entity.substring(1)));
				}
		        else 
			    {
					switch (entity)
					{
						case 'quot': ch = String.fromCharCode(0x0022); break;
						case 'amp': ch = String.fromCharCode(0x0026); break;
						case 'lt': ch = String.fromCharCode(0x003c); break;
						case 'gt': ch = String.fromCharCode(0x003e); break;
						case 'nbsp': ch = String.fromCharCode(0x00a0); break;
						case 'iexcl': ch = String.fromCharCode(0x00a1); break;
						case 'cent': ch = String.fromCharCode(0x00a2); break;
						case 'pound': ch = String.fromCharCode(0x00a3); break;
						case 'curren': ch = String.fromCharCode(0x00a4); break;
						case 'yen': ch = String.fromCharCode(0x00a5); break;
						case 'brvbar': ch = String.fromCharCode(0x00a6); break;
						case 'sect': ch = String.fromCharCode(0x00a7); break;
						case 'uml': ch = String.fromCharCode(0x00a8); break;
						case 'copy': ch = String.fromCharCode(0x00a9); break;
						case 'ordf': ch = String.fromCharCode(0x00aa); break;
						case 'laquo': ch = String.fromCharCode(0x00ab); break;
						case 'not': ch = String.fromCharCode(0x00ac); break;
						case 'shy': ch = String.fromCharCode(0x00ad); break;
						case 'reg': ch = String.fromCharCode(0x00ae); break;
						case 'macr': ch = String.fromCharCode(0x00af); break;
						case 'deg': ch = String.fromCharCode(0x00b0); break;
						case 'plusmn': ch = String.fromCharCode(0x00b1); break;
						case 'sup2': ch = String.fromCharCode(0x00b2); break;
						case 'sup3': ch = String.fromCharCode(0x00b3); break;
						case 'acute': ch = String.fromCharCode(0x00b4); break;
						case 'micro': ch = String.fromCharCode(0x00b5); break;
						case 'para': ch = String.fromCharCode(0x00b6); break;
						case 'middot': ch = String.fromCharCode(0x00b7); break;
						case 'cedil': ch = String.fromCharCode(0x00b8); break;
						case 'sup1': ch = String.fromCharCode(0x00b9); break;
						case 'ordm': ch = String.fromCharCode(0x00ba); break;
						case 'raquo': ch = String.fromCharCode(0x00bb); break;
						case 'frac14': ch = String.fromCharCode(0x00bc); break;
						case 'frac12': ch = String.fromCharCode(0x00bd); break;
						case 'frac34': ch = String.fromCharCode(0x00be); break;
						case 'iquest': ch = String.fromCharCode(0x00bf); break;
						case 'Agrave': ch = String.fromCharCode(0x00c0); break;
						case 'Aacute': ch = String.fromCharCode(0x00c1); break;
						case 'Acirc': ch = String.fromCharCode(0x00c2); break;
						case 'Atilde': ch = String.fromCharCode(0x00c3); break;
						case 'Auml': ch = String.fromCharCode(0x00c4); break;
						case 'Aring': ch = String.fromCharCode(0x00c5); break;
						case 'AElig': ch = String.fromCharCode(0x00c6); break;
						case 'Ccedil': ch = String.fromCharCode(0x00c7); break;
						case 'Egrave': ch = String.fromCharCode(0x00c8); break;
						case 'Eacute': ch = String.fromCharCode(0x00c9); break;
						case 'Ecirc': ch = String.fromCharCode(0x00ca); break;
						case 'Euml': ch = String.fromCharCode(0x00cb); break;
						case 'Igrave': ch = String.fromCharCode(0x00cc); break;
						case 'Iacute': ch = String.fromCharCode(0x00cd); break;
						case 'Icirc': ch = String.fromCharCode(0x00ce); break;
						case 'Iuml': ch = String.fromCharCode(0x00cf); break;
						case 'ETH': ch = String.fromCharCode(0x00d0); break;
						case 'Ntilde': ch = String.fromCharCode(0x00d1); break;
						case 'Ograve': ch = String.fromCharCode(0x00d2); break;
						case 'Oacute': ch = String.fromCharCode(0x00d3); break;
						case 'Ocirc': ch = String.fromCharCode(0x00d4); break;
						case 'Otilde': ch = String.fromCharCode(0x00d5); break;
						case 'Ouml': ch = String.fromCharCode(0x00d6); break;
						case 'times': ch = String.fromCharCode(0x00d7); break;
						case 'Oslash': ch = String.fromCharCode(0x00d8); break;
						case 'Ugrave': ch = String.fromCharCode(0x00d9); break;
						case 'Uacute': ch = String.fromCharCode(0x00da); break;
						case 'Ucirc': ch = String.fromCharCode(0x00db); break;
						case 'Uuml': ch = String.fromCharCode(0x00dc); break;
						case 'Yacute': ch = String.fromCharCode(0x00dd); break;
						case 'THORN': ch = String.fromCharCode(0x00de); break;
						case 'szlig': ch = String.fromCharCode(0x00df); break;
						case 'agrave': ch = String.fromCharCode(0x00e0); break;
						case 'aacute': ch = String.fromCharCode(0x00e1); break;
						case 'acirc': ch = String.fromCharCode(0x00e2); break;
						case 'atilde': ch = String.fromCharCode(0x00e3); break;
						case 'auml': ch = String.fromCharCode(0x00e4); break;
						case 'aring': ch = String.fromCharCode(0x00e5); break;
						case 'aelig': ch = String.fromCharCode(0x00e6); break;
						case 'ccedil': ch = String.fromCharCode(0x00e7); break;
						case 'egrave': ch = String.fromCharCode(0x00e8); break;
						case 'eacute': ch = String.fromCharCode(0x00e9); break;
						case 'ecirc': ch = String.fromCharCode(0x00ea); break;
						case 'euml': ch = String.fromCharCode(0x00eb); break;
						case 'igrave': ch = String.fromCharCode(0x00ec); break;
						case 'iacute': ch = String.fromCharCode(0x00ed); break;
						case 'icirc': ch = String.fromCharCode(0x00ee); break;
						case 'iuml': ch = String.fromCharCode(0x00ef); break;
						case 'eth': ch = String.fromCharCode(0x00f0); break;
						case 'ntilde': ch = String.fromCharCode(0x00f1); break;
						case 'ograve': ch = String.fromCharCode(0x00f2); break;
						case 'oacute': ch = String.fromCharCode(0x00f3); break;
						case 'ocirc': ch = String.fromCharCode(0x00f4); break;
						case 'otilde': ch = String.fromCharCode(0x00f5); break;
						case 'ouml': ch = String.fromCharCode(0x00f6); break;
						case 'divide': ch = String.fromCharCode(0x00f7); break;
						case 'oslash': ch = String.fromCharCode(0x00f8); break;
						case 'ugrave': ch = String.fromCharCode(0x00f9); break;
						case 'uacute': ch = String.fromCharCode(0x00fa); break;
						case 'ucirc': ch = String.fromCharCode(0x00fb); break;
						case 'uuml': ch = String.fromCharCode(0x00fc); break;
						case 'yacute': ch = String.fromCharCode(0x00fd); break;
						case 'thorn': ch = String.fromCharCode(0x00fe); break;
						case 'yuml': ch = String.fromCharCode(0x00ff); break;
						case 'OElig': ch = String.fromCharCode(0x0152); break;
						case 'oelig': ch = String.fromCharCode(0x0153); break;
						case 'Scaron': ch = String.fromCharCode(0x0160); break;
						case 'scaron': ch = String.fromCharCode(0x0161); break;
						case 'Yuml': ch = String.fromCharCode(0x0178); break;
						case 'fnof': ch = String.fromCharCode(0x0192); break;
						case 'circ': ch = String.fromCharCode(0x02c6); break;
						case 'tilde': ch = String.fromCharCode(0x02dc); break;
						case 'Alpha': ch = String.fromCharCode(0x0391); break;
						case 'Beta': ch = String.fromCharCode(0x0392); break;
						case 'Gamma': ch = String.fromCharCode(0x0393); break;
						case 'Delta': ch = String.fromCharCode(0x0394); break;
						case 'Epsilon': ch = String.fromCharCode(0x0395); break;
						case 'Zeta': ch = String.fromCharCode(0x0396); break;
						case 'Eta': ch = String.fromCharCode(0x0397); break;
						case 'Theta': ch = String.fromCharCode(0x0398); break;
						case 'Iota': ch = String.fromCharCode(0x0399); break;
						case 'Kappa': ch = String.fromCharCode(0x039a); break;
						case 'Lambda': ch = String.fromCharCode(0x039b); break;
						case 'Mu': ch = String.fromCharCode(0x039c); break;
						case 'Nu': ch = String.fromCharCode(0x039d); break;
						case 'Xi': ch = String.fromCharCode(0x039e); break;
						case 'Omicron': ch = String.fromCharCode(0x039f); break;
						case 'Pi': ch = String.fromCharCode(0x03a0); break;
						case 'Rho': ch = String.fromCharCode(0x03a1); break;
						case 'Sigma': ch = String.fromCharCode(0x03a3); break;
						case 'Tau': ch = String.fromCharCode(0x03a4); break;
						case 'Upsilon': ch = String.fromCharCode(0x03a5); break;
						case 'Phi': ch = String.fromCharCode(0x03a6); break;
						case 'Chi': ch = String.fromCharCode(0x03a7); break;
						case 'Psi': ch = String.fromCharCode(0x03a8); break;
						case 'Omega': ch = String.fromCharCode(0x03a9); break;
						case 'alpha': ch = String.fromCharCode(0x03b1); break;
						case 'beta': ch = String.fromCharCode(0x03b2); break;
						case 'gamma': ch = String.fromCharCode(0x03b3); break;
						case 'delta': ch = String.fromCharCode(0x03b4); break;
						case 'epsilon': ch = String.fromCharCode(0x03b5); break;
						case 'zeta': ch = String.fromCharCode(0x03b6); break;
						case 'eta': ch = String.fromCharCode(0x03b7); break;
						case 'theta': ch = String.fromCharCode(0x03b8); break;
						case 'iota': ch = String.fromCharCode(0x03b9); break;
						case 'kappa': ch = String.fromCharCode(0x03ba); break;
						case 'lambda': ch = String.fromCharCode(0x03bb); break;
						case 'mu': ch = String.fromCharCode(0x03bc); break;
						case 'nu': ch = String.fromCharCode(0x03bd); break;
						case 'xi': ch = String.fromCharCode(0x03be); break;
						case 'omicron': ch = String.fromCharCode(0x03bf); break;
						case 'pi': ch = String.fromCharCode(0x03c0); break;
						case 'rho': ch = String.fromCharCode(0x03c1); break;
						case 'sigmaf': ch = String.fromCharCode(0x03c2); break;
						case 'sigma': ch = String.fromCharCode(0x03c3); break;
						case 'tau': ch = String.fromCharCode(0x03c4); break;
						case 'upsilon': ch = String.fromCharCode(0x03c5); break;
						case 'phi': ch = String.fromCharCode(0x03c6); break;
						case 'chi': ch = String.fromCharCode(0x03c7); break;
						case 'psi': ch = String.fromCharCode(0x03c8); break;
						case 'omega': ch = String.fromCharCode(0x03c9); break;
						case 'thetasym': ch = String.fromCharCode(0x03d1); break;
						case 'upsih': ch = String.fromCharCode(0x03d2); break;
						case 'piv': ch = String.fromCharCode(0x03d6); break;
						case 'ensp': ch = String.fromCharCode(0x2002); break;
						case 'emsp': ch = String.fromCharCode(0x2003); break;
						case 'thinsp': ch = String.fromCharCode(0x2009); break;
						case 'zwnj': ch = String.fromCharCode(0x200c); break;
						case 'zwj': ch = String.fromCharCode(0x200d); break;
						case 'lrm': ch = String.fromCharCode(0x200e); break;
						case 'rlm': ch = String.fromCharCode(0x200f); break;
						case 'ndash': ch = String.fromCharCode(0x2013); break;
						case 'mdash': ch = String.fromCharCode(0x2014); break;
						case 'lsquo': ch = String.fromCharCode(0x2018); break;
						case 'rsquo': ch = String.fromCharCode(0x2019); break;
						case 'sbquo': ch = String.fromCharCode(0x201a); break;
						case 'ldquo': ch = String.fromCharCode(0x201c); break;
						case 'rdquo': ch = String.fromCharCode(0x201d); break;
						case 'bdquo': ch = String.fromCharCode(0x201e); break;
						case 'dagger': ch = String.fromCharCode(0x2020); break;
						case 'Dagger': ch = String.fromCharCode(0x2021); break;
						case 'bull': ch = String.fromCharCode(0x2022); break;
						case 'hellip': ch = String.fromCharCode(0x2026); break;
						case 'permil': ch = String.fromCharCode(0x2030); break;
						case 'prime': ch = String.fromCharCode(0x2032); break;
						case 'Prime': ch = String.fromCharCode(0x2033); break;
						case 'lsaquo': ch = String.fromCharCode(0x2039); break;
						case 'rsaquo': ch = String.fromCharCode(0x203a); break;
						case 'oline': ch = String.fromCharCode(0x203e); break;
						case 'frasl': ch = String.fromCharCode(0x2044); break;
						case 'euro': ch = String.fromCharCode(0x20ac); break;
						case 'image': ch = String.fromCharCode(0x2111); break;
						case 'weierp': ch = String.fromCharCode(0x2118); break;
						case 'real': ch = String.fromCharCode(0x211c); break;
						case 'trade': ch = String.fromCharCode(0x2122); break;
						case 'alefsym': ch = String.fromCharCode(0x2135); break;
						case 'larr': ch = String.fromCharCode(0x2190); break;
						case 'uarr': ch = String.fromCharCode(0x2191); break;
						case 'rarr': ch = String.fromCharCode(0x2192); break;
						case 'darr': ch = String.fromCharCode(0x2193); break;
						case 'harr': ch = String.fromCharCode(0x2194); break;
						case 'crarr': ch = String.fromCharCode(0x21b5); break;
						case 'lArr': ch = String.fromCharCode(0x21d0); break;
						case 'uArr': ch = String.fromCharCode(0x21d1); break;
						case 'rArr': ch = String.fromCharCode(0x21d2); break;
						case 'dArr': ch = String.fromCharCode(0x21d3); break;
						case 'hArr': ch = String.fromCharCode(0x21d4); break;
						case 'forall': ch = String.fromCharCode(0x2200); break;
						case 'part': ch = String.fromCharCode(0x2202); break;
						case 'exist': ch = String.fromCharCode(0x2203); break;
						case 'empty': ch = String.fromCharCode(0x2205); break;
						case 'nabla': ch = String.fromCharCode(0x2207); break;
						case 'isin': ch = String.fromCharCode(0x2208); break;
						case 'notin': ch = String.fromCharCode(0x2209); break;
						case 'ni': ch = String.fromCharCode(0x220b); break;
						case 'prod': ch = String.fromCharCode(0x220f); break;
						case 'sum': ch = String.fromCharCode(0x2211); break;
						case 'minus': ch = String.fromCharCode(0x2212); break;
						case 'lowast': ch = String.fromCharCode(0x2217); break;
						case 'radic': ch = String.fromCharCode(0x221a); break;
						case 'prop': ch = String.fromCharCode(0x221d); break;
						case 'infin': ch = String.fromCharCode(0x221e); break;
						case 'ang': ch = String.fromCharCode(0x2220); break;
						case 'and': ch = String.fromCharCode(0x2227); break;
						case 'or': ch = String.fromCharCode(0x2228); break;
						case 'cap': ch = String.fromCharCode(0x2229); break;
						case 'cup': ch = String.fromCharCode(0x222a); break;
						case 'int': ch = String.fromCharCode(0x222b); break;
						case 'there4': ch = String.fromCharCode(0x2234); break;
						case 'sim': ch = String.fromCharCode(0x223c); break;
						case 'cong': ch = String.fromCharCode(0x2245); break;
						case 'asymp': ch = String.fromCharCode(0x2248); break;
						case 'ne': ch = String.fromCharCode(0x2260); break;
						case 'equiv': ch = String.fromCharCode(0x2261); break;
						case 'le': ch = String.fromCharCode(0x2264); break;
						case 'ge': ch = String.fromCharCode(0x2265); break;
						case 'sub': ch = String.fromCharCode(0x2282); break;
						case 'sup': ch = String.fromCharCode(0x2283); break;
						case 'nsub': ch = String.fromCharCode(0x2284); break;
						case 'sube': ch = String.fromCharCode(0x2286); break;
						case 'supe': ch = String.fromCharCode(0x2287); break;
						case 'oplus': ch = String.fromCharCode(0x2295); break;
						case 'otimes': ch = String.fromCharCode(0x2297); break;
						case 'perp': ch = String.fromCharCode(0x22a5); break;
						case 'sdot': ch = String.fromCharCode(0x22c5); break;
						case 'lceil': ch = String.fromCharCode(0x2308); break;
						case 'rceil': ch = String.fromCharCode(0x2309); break;
						case 'lfloor': ch = String.fromCharCode(0x230a); break;
						case 'rfloor': ch = String.fromCharCode(0x230b); break;
						case 'lang': ch = String.fromCharCode(0x2329); break;
						case 'rang': ch = String.fromCharCode(0x232a); break;
						case 'loz': ch = String.fromCharCode(0x25ca); break;
						case 'spades': ch = String.fromCharCode(0x2660); break;
						case 'clubs': ch = String.fromCharCode(0x2663); break;
						case 'hearts': ch = String.fromCharCode(0x2665); break;
						case 'diams': ch = String.fromCharCode(0x2666); break;
						default: ch = ''; break;
					}
				}
				i = semicolonIndex; 
			}
		}
		out += ch;
	}
	return out;
}

}

DundasChart.init();


DundasChart.init();

function Dundas_Dialog( title, content)
{
    this.container = null;
    this.title = title;
    this.content = content;
    this.opened = false;
    this.hiddenSelects = new Array();
    this.disabledTabs = new Array();
    this.mask = null;
}
   
Dundas_Dialog.prototype.close = function()
    {
        if ( this.opened && this.container)
        {
            this.restoreSelects();
			DundasChart.releaseDlgMask();            
			var parent = this.container.parentNode;			
            parent.removeChild( this.container);
            parent = null;
            this.opened = false;
            DundasChart.popupDialogs.pop();
        }
        return false;
    }

Dundas_Dialog.prototype.disableTabs = function () {
	if (document.all) {
		var tabs = new Array("A","BUTTON","TEXTAREA","INPUT","IFRAME");
		storage = new Array();
		for (var i = 0; i < tabs.length; i++) {
			var tagElements = document.getElementsByTagName(tabs[i]);
			for (var j = 0 ; j < tagElements.length; j++) {
				var data = new Object();
				data.element  = tagElements[j];
				data.tabIndex = tagElements[j].tabIndex;
				storage[storage.length] = data;
				tagElements[j].tabIndex="-1";
			}
		}
		this.disabledTabs = storage;
	}
}


Dundas_Dialog.prototype.restoreTabs = function () {
	if (document.all) {
		for( var i = 0; i < this.disabledTabs.length; i++)
		{
			var elm = this.disabledTabs[i].element;
			elm.tabIndex = this.disabledTabs[i].tabIndex;
		}
		this.disabledTabs.length = 0;
	}
}
	
Dundas_Dialog.prototype.hideSelects = function ()
	{
		if ( DundasChart.msieFlag && DundasChart.msieVer < 7)
		{
			for(var i = 0; i < document.forms.length; i++) {
				for(var e = 0; e < document.forms[i].length; e++){
					if(document.forms[i].elements[e].tagName == "SELECT") {
						var elm = document.forms[i].elements[e];
						if ( elm.style.visibility != "hidden" )
						{
							try
							{
								this.hiddenSelects.push( {element: elm, state: elm.style.visibility } );
								elm.style.visibility = "hidden";
							} catch(exc) {;}
						}
					}
				}
			}
		}
		this.disableTabs();
	};	
	
Dundas_Dialog.prototype.restoreSelects = function()
	{
		while ( this.hiddenSelects.length > 0 )
		{
			var obj = this.hiddenSelects.pop();
			try
			{
				obj.element.style.visibility = obj.state;
			}
			catch(exc) {;}
		}
		this.restoreTabs();
	};
	
Dundas_Dialog.prototype.closeDoPost = function( componentID, command)
    {
        if ( this.container )
        {
            this.close();
            if ( componentID )
            {
		        if ( typeof(OlapCallBackQuirk) == "function" ){
		            OlapCallBackQuirk(componentID, command);
		        }
		        else if (typeof(__doPostBack) == 'function'){
			        __doPostBack("@","@")
		        }
            }
        }
    };
    
Dundas_Dialog.prototype.getFirstChild = function ( element)
    {
        var result = element.firstChild;
        if (result && result.nodeType == 3)
        {
            return result.nextSibling;
        }
        return result;
    };
    
Dundas_Dialog.prototype.open = function( htmlContent)
    {
        
        if ( this.opened )
        {
            this.close();
        }
        this.hideSelects();
		DundasChart.createDlgMask();        
        var  pattern = document.getElementById("_ds_dlg_dlg_");
        this.container = pattern.cloneNode(true);
        this.container.id = this.container.id + Math.random().toString().replace(".","");
        this.container.className = "ds_dlg_dlg";
        this.container.style.width = "";
        this.container.style.height = "";
        this.container.proxy = this;
        if (typeof(theForm) != 'undefined')
        {
			theForm.appendChild( this.container);
        }
        else
        {
			document.body.appendChild( this.container);
        }
        var cnt = document.createElement("DIV");
        //cnt.style.textAlign = "center";
        cnt.className = "ds_dlg_dlg_cnt";
        cnt.innerHTML = htmlContent ? htmlContent : this.content;
        this.container.appendChild( cnt);
		this.setPosition(-1000, -1000);
        this.container.style.display = "block";
        this.dialogTitle = this.getFirstChild(this.container);
        this.dialogTitle.onmousedown = this.startDlgDrag;
        
        this.dialogTitle.getElementsByTagName("SPAN")[0].innerHTML = this.title;
        
        // rtl

        if (this.rightLeft)
        {
            var title = this.dialogTitle.getElementsByTagName("SPAN")[0];
            var img = this.dialogTitle.getElementsByTagName("IMG")[0];
            
            if (DundasChart.geckoFlag)
            {
                title.style.cssFloat = "right";
                img.style.cssFloat = "left";
            }
            else
            {
                title.style.cssText = "float:right;";
                img.style.cssText = "float:left;";
            }
        }
        
         // -rtl
        
        this.dialogTitle.dialog = this;
        
        var cntChild = this.getFirstChild(cnt);
        this.innerElement = cntChild;
        if ( cntChild)
        {
            cntChild.dialog = this;
            this.setSize(parseInt(cntChild.style.width), parseInt(cntChild.style.height));
        }
        this.opened = true;
        DundasChart.popupDialogs.push( this );
		this.center();
    };
    
Dundas_Dialog.prototype.setSize = function( width, height )
    {
        if ( width ) {
             if ( DundasChart.msieFlag )
             {
                 this.container.style.width   = (width+4) + "px";
                 this.dialogTitle.style.width = (width-4) + "px";
                 if ( parseInt(window.navigator.appVersion) < 5 )
                 {
					this.dialogTitle.style.width = (width) + "px";
                 }
             }
             else
             {
                 this.container.style.width   = (width) + "px";
                 this.dialogTitle.style.width = (width-4) + "px";
             }
        }
        if ( height )
        {
            //this.container.style.height = (height + 23) + "px";
        }
    };
    
Dundas_Dialog.prototype.setPosition = function( top, left)
    {
        if ( this.container )
        {
            this.container.style.top  = top + "px";
            this.container.style.left = left + "px";
        }
    };
    
Dundas_Dialog.prototype.setTitle = function ( titleStr )
    {
        if ( this.dialogTitle )
        {
			this.dialogTitle.getElementsByTagName("SPAN")[0].innerHTML = titleStr;
        }
    };
        
Dundas_Dialog.prototype.center = function( width, height )
    {
	    if (this.container)
	    {
		    if (width == null || isNaN(width)) {
			    width = this.container.offsetWidth;
		    }
		    if (height == null) {
			    height = this.container.offsetHeight;
		    }
			var titleBarHeight = 20;
    		var fullHeight = 0;
    		var fullWidth = 0;
    		var scrollData = null;
    		if ( DundasChart.popupDialogs.length <= 1 )
    		{
				fullHeight = DundasChart.getScreenHeight();
				fullWidth = DundasChart.getScreenWidth();
				scrollData = DundasChart.getScrollPos();
		    }
		    else
		    {
				var topDialog = DundasChart.popupDialogs[DundasChart.popupDialogs.length - 2];
				fullHeight = topDialog.container.offsetHeight;
				fullWidth = topDialog.container.offsetWidth;
				scrollData = {top : parseInt(topDialog.container.style.top), left: parseInt(topDialog.container.style.left)};
		    }
			var top  = scrollData.top + ((fullHeight - (height+titleBarHeight)) / 2);
			var left = scrollData.left + ((fullWidth - width) / 2);
		    this.setPosition( top, left );
        }    
    };
    
Dundas_Dialog.prototype.startDlgDrag = function ( event)
    {
        var dialog = this.dialog;
        if ( dialog != DundasChart.topDialog())
        {
			return;
        }
	    var e = DundasChart.fixEvent(event);
	    if ( e.target && e.target.tagName && e.target.tagName == "IMG")
	    {
	        if (typeof(dialog.canClose) == "function")
	        {
				if (!dialog.canClose())
				{
					return;
				}
	        }
//	        dialog.close();
	        return;
	    }
	    dialog.dialogDragData = {x:e.screenX, y:e.screenY};
	    document.current_ds_dialog = dialog;
	    DundasChart.attachEvent(document,"mousemove", dialog.moveDlgDrag);
	    DundasChart.attachEvent(document,"mouseup", dialog.stopDlgDrag);
	    return false;
    };
    
Dundas_Dialog.prototype.canClose = function () { return true; }
    
Dundas_Dialog.prototype.stopDlgDrag = function ( )
    {
	    var dialog = document.current_ds_dialog;
	    if ( dialog )
	    {
	        dialog.dialogDragData = null;
	        dialog.dialogTitle.style.cursor = "default";
	        DundasChart.detachEvent(document,"mousemove", dialog.moveDlgDrag);
	        DundasChart.detachEvent(document,"mouseup", dialog.stopDlgDrag);
	        document.current_ds_dialog = null;
	    }
	    return false;
    };

Dundas_Dialog.prototype.moveDlgDrag = function ( event)
    {
		var dialog = document.current_ds_dialog;
	    if ( dialog )
	    { 
		    var e = DundasChart.fixEvent(event);
		    var oldData = dialog.dialogDragData;
		    var newData = {x:e.screenX, y:e.screenY};
		    var top  = parseInt(dialog.container.style.top);
		    var left = parseInt(dialog.container.style.left);
		    dialog.setPosition( (top  + (newData.y - oldData.y)), (left + (newData.x - oldData.x)));
		    dialog.dialogDragData = newData;
	    }
	    return false;
    };
      

if(typeof(Sys) != 'undefined') 
{
    if(Sys.Application )
    {
        Sys.Application.notifyScriptLoaded();
    }
}
