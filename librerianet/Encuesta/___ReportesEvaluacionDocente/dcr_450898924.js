
var __nonMSDOMBrowser_ = (window.navigator.appName.toLowerCase().indexOf('explorer') == -1);

var __scrollingHandlerMode__ = false;

//*********** START ChartCBClass Object Definition *******************/

function ChartCBClass()
{
	this.cursorWait = true;
}

ChartCBClass.prototype.callAsync = function(e, cltrId, args )
{
	this.cursorWait = false;
	this.call(e, cltrId, args, null, null, null, true);
}

ChartCBClass.prototype.RemoveChartAreaContainer = function(areaContainerId)
{
    var areaContainer = document.getElementById(areaContainerId);
    if (areaContainer)
    {
        var parent = areaContainer.offsetParent;
        if (parent)
        {
            parent.removeChild(areaContainer);
        }
    }
}

ChartCBClass.prototype.call = function(e, cltrId, args, clientCallback, context, clientErrorCallback, asynk)
{

	if( typeof(asynk) != "boolean") {asynk = false;}    
    this.clearForm();
    this.ChartCallBack(cltrId, args, clientCallback, context, clientErrorCallback, asynk);
    this.cursorWait = true;
	if ( !asynk && typeof(ChartToolbar) != "undefined")
    {
        ChartToolbar.closeAllMenus();
    }    
    DundasChart.cancelEvent(e);    
    
    if (window.areaGridArray && !saveResources)
    {
        while (window.areaGridArray.length>0)
        {
            var areaGridObj = window.areaGridArray.pop();
            areaGridObj.cleanRes();
        }
        window.areaGridArray = null;
    }

}

ChartCBClass.prototype.ChartCallBack = function(controlName, args, clientCallback, context, clientErrorCallback, useAsync) {
	if( typeof(useAsync) != "boolean") {useAsync = false;}
	if( !clientCallback) {clientCallback = this.callBack;}
	if( !context) {context = this.context(controlName, useAsync);}
	if( !clientErrorCallback) {clientErrorCallback = this.error;}
	
    var ajaxZoomEnabled = true;
	
	if (document.getElementById(controlName+"AjaxEnabled") != null)
	{
	    ajaxZoomEnabled= document.getElementById(controlName+"AjaxEnabled").value == "1" ? true : false;
	} 

	
	if (args.indexOf('zoom') == -1 || ajaxZoomEnabled || false == this.postBackFlag)
	{
	    if (args.indexOf('zoom') != -1) 
	    {
	        this.postBackFlag = true;
	    }
	    document.postBackFlag = false;
	    
	    WebForm_DoCallback(controlName,args,clientCallback,context,clientErrorCallback, useAsync)
	}
	else
	{
	    document.postBackFlag = true;
	    this.postbackFlag = true;
	    __doPostBack(controlName,args);
        //WebForm_DoCallback(controlName,args,clientCallback,context,clientErrorCallback, useAsync)
	}
	return (false)
}

ChartCBClass.prototype.refreshPage = function()
{
	try
	{
	document.location.reload(true);
	}
	catch (exc) {};
}

ChartCBClass.prototype.click = function(e, cltrId, args, chartPanel)
{
	if ( this.cancelNextClick )
	{
		this.cancelNextClick = false;
		return;
	}
	e = DundasChart.fixEvent(e);
	if ( e && !(e.donotClick))
	{
		    if ( e.target )
		    {
			    if ( e.target.tagName == "AREA" )
			    {
				    if ( typeof(e.target.onclick) == "function" )
				    {
					    return;
				    }	
					if ( typeof(e.target.href) == "string" && e.target.href.length > 0)
					{
						return;
					}				    
				}
		    }
	}
	this.call( e, cltrId, args + this.getXY(e, chartPanel));
}
ChartCBClass.prototype.callBack  = function( result, context)
{
	if (context.asynchronous)
	{
		window.setTimeout( function () {context.cbObj.callBackInContext(result, context)}, 0);	
	}
	else
	{
		context.cbObj.callBackInContext(result, context);
	}
}

ChartCBClass.prototype.getInnerElementById = function (element, id)
{
    for(var i=0;i<element.childNodes.length;i++)
    {
        var el = element.childNodes[i];
        if (el.id == id)
        {
            return el;
        }       
        
        el = this.getInnerElementById(el,id);
        if (el)
        {
            return el;
        }
         
    }
    
    return null;
    
}
	
ChartCBClass.prototype.callBackInContext  = function( result, context)
{
    this.hideMaskDiv(context);
    
    // memleak - to avoid flickering GC should work here
    Garbage.emptyBin();
    // -memleak
    
    context.selects.enable();
    var s = result.substring( 1, result.length - 3)
    var items = s.split("!]].[");
//	ChartCB.debugOut("----------");
    for( var itemPos = 0; itemPos < items.length; itemPos++)
    {
		var index = items[itemPos].indexOf(":[!");
        if ( index == -1) continue;
        var data = new Array( items[itemPos].substring(0, index), items[itemPos].substring(index+3));        
        if ( data.length == 2 )
        {
			//ChartCB.debugOut("--" + data[0]);
			if ( data[0].indexOf("ds_popup_update") == 0 )
            {
				var popd =  data[0].split("@");
                var id = popd.length > 1 ? popd[1] : null;
				var title = popd.length > 2 ? popd[2] : "";
				var evalpos = popd.length > 3 ? popd[3] : null;
//				var rightLeft = popd.length > 4 ? popd[4] : null;
				if (id)
				{
				    var d = document.getElementById(id);
				    if (d)
				    {
				        var tmpDiv = document.createElement('DIV');
				        tmpDiv.innerHTML = data[1];
                        //tmpDiv.id="callBackInContextTmpDiv1";
                        var i; 
                                               
                        var coll = new Array();
                        
                        for (i = 0; i<d.childNodes.length;i++)
                        {
                            coll.push(d.childNodes[i]);
                        }                       
                        
				        for (i = 0; i<coll.length;i++)
				        {
				            var page = coll[i];
				            if (!page.id)
				            {
				                continue;
				            }
				            var display = page.style.display;
				            var newPage = this.getInnerElementById(tmpDiv,page.id);
				            if (newPage)
				            {
				                var parent = page.parentNode;
				                parent.removeChild(page);
				                parent.appendChild(newPage);
				                newPage.style.display = display;
				            }				       
				            // remove ML   
				            Garbage.discardElement(tmpDiv); // here
				            
				        }
				    }
				}
            }
            else if ( data[0].indexOf("ds_popup") == 0 )
            {
				var popd =  data[0].split("@");
				var title = popd.length > 1 ? popd[1] : "";
				var evalpos = popd.length > 2 ? popd[2] : null;
				var rightLeft = popd.length > 3 ? popd[3] : null;
				var popw = new Dundas_Dialog(title, data[1], true);
				if (rightLeft)
				{
				    popw.rightLeft = parseInt(rightLeft);
				}
				popw.open();
				if ( evalpos )
				{
					eval(evalpos);
				}
            }
            else if ( data[0] == "menu" && context.callback)
            {
                context.callback(context, data[1]);
            }
            else if ( data[0].indexOf("bold:") == 0 && typeof(DundasTree) == "object")
            {
                    var elementName = data[0].substring( 5);
                    var boldItems = data[1].split("||");
                    DundasTree.updateBoldItems( elementName, boldItems);
            }
            else if ( data[0] == "javascript")
            {
                try
                {
                    eval(data[1]);
                }
                catch(e){
                   alert( "ajax javascript error:" + e.message);
                }    
            }
            else 
            {
                var element = document.getElementById( data[0]);
                if ( element && data[1].length > 0 && element.tagName) 
                {
                    if (element.tagName == "TABLE" && data[1].indexOf("<!--partial-->") == 0)
					{
						this.processTable( element, data[1]);
					}                    
                    else if ( element.tagName == "IMG")
                    {
                        this.processChart( element, data[1]);
                    }
                    else if ( element.tagName == "STYLE")
                    {
                        this.processStyle( element, data[1]);
                    }
                    else if ( element.tagName == "SELECT" && data[1].toLowerCase().indexOf("<select") == -1)
                    {
                        this.processSelect( element, data[1]);
                    }
                    else if ( element.tagName == "INPUT" && data[1].toLowerCase().indexOf("<input") == -1)
                    {
                        element.value = data[1];
                    }
                    else {
						this.processUnknownElement(element, data[1]);						
                    }
                }
                else
                {
					this.pushElement( data[1], data[0]);
                }
            }
        }
        else
        {
            // debugOut("- data is corrupted: " + items[itemPos]);
        }
    }
    // mem leak
    context.cbObj = null;
    // -mem leak
}

ChartCBClass.prototype.processStyle = function ( element, chunk)
{
	if ( DundasChart.msieFlag)
	{
	    element.styleSheet.cssText=chunk;	
	}
	else
	{
		if (DundasChart.safariFlag)
		{
			element.innerText = chunk;
		}
		else
		{
	    	element.innerHTML = chunk;	
	 	}
	}
	
};

ChartCBClass.prototype.extractGridElement = function ( element)
{
    var codes = element.getElementsByTagName("CODE");
    if ( codes.length > 0 )
    {
        for( var i = 0; i < codes.length; i++)
        {
            if ( codes[i].className == "__ds_grid__")
            {
                return codes[i].previousSibling;
            }
        }
    }
    return null;
};


ChartCBClass.prototype.renameGridElement = function ( element, suffix)
{
    if ( element.id && element.id.length > 0 )
    {
        element.id = element.id + suffix;
    }
    for( var i = 0; i < element.childNodes.length; i++)
    {
        this.renameGridElement( element.childNodes[i]);
    }
};

ChartCBClass.prototype.executeStyles = function ( stylesContainer)
{
	var styleElements = stylesContainer.getElementsByTagName("STYLE");
	if ( styleElements.length > 0)
	{
		var heads = document.getElementsByTagName("HEAD");
	
		for( var i = 0; i < styleElements.length; i++)
		{
		    var style = styleElements[i];
		    
		    style.parentNode.removeChild(style);
            
		    var oldStyle = document.getElementById( style.id);
            if ( oldStyle )
            {
                this.processStyle( oldStyle, style.innerHTML );
            }
            else
            {
		        if ( heads.length > 0 )
		        {
			        heads[0].appendChild( style );
		        }
		        else
		        {
			        document.appendChild( style)
		        }
            }
		}
	}
};

ChartCBClass.prototype.canSwitchGrids = function ( newGridElement, oldGridElement)
{
    var canSwitchGrids = newGridElement != null && oldGridElement != null && newGridElement.id == oldGridElement.id;
    if ( canSwitchGrids )
    {
        if ( newGridElement.style.width != oldGridElement.style.width )
        {
            return false;
        }
        if ( newGridElement.style.height != oldGridElement.style.height )
        {
            return false;
        }
        if ( newGridElement.style.position != oldGridElement.style.position )
        {
            return false;
        }
        if ( newGridElement.style.top != oldGridElement.style.top )
        {
            return false;
        }
        if ( newGridElement.style.left != oldGridElement.style.left )
        {
            return false;
        }
    }
    return canSwitchGrids;
}



ChartCBClass.prototype.processUnknownElement = function ( element, chunk)
{
	var tmpDiv = document.createElement("DIV");
	tmpDiv.innerHTML = chunk;
//	tmpDiv.id="processUnknownElementTempDiv";
	var scriptElements = tmpDiv.getElementsByTagName("SCRIPT");
	var scripts = null;
	if ( scriptElements.length > 0)
	{
		scripts = new Array();
		for( var i = 0; i < scriptElements.length; i++)
		{
			scripts[scripts.length] = scriptElements[i].innerHTML;
		}
	}
	
	this.executeStyles(tmpDiv);
	
	var newElement = tmpDiv.firstChild;
	while( newElement && newElement.nodeType != 1 )
	{
		newElement = newElement.nextSibling;
	}
	if ( newElement )
	{
        var newGridElement = this.extractGridElement( tmpDiv);
        var oldGridElement = this.extractGridElement( element.parentNode);
        var canSwitchGrids = this.canSwitchGrids( newGridElement, oldGridElement);
        if (!canSwitchGrids)
        {
		    var par = element.parentNode;
		    var oldPos,oldTop;
		    if (!DundasChart.msieFlag)
		    {   
		        oldTop = newElement.style.top;
		        newElement.style.top="-10000px;";
		        oldPos = newElement.style.position;
		        newElement.style.position="absolute";

		    }	
            try
            {
                if (element.tagName != 'TABLE')
                {
                    element.innerHTML = "";
                }
            }
		    catch(err)
		    {
		    }
		    element.parentNode.replaceChild(newElement, element);
		    Garbage.discardElement(element); 
		}
		else
		{
		    this.renameGridElement (oldGridElement, "_o_");
		    var tables = newGridElement.getElementsByTagName("TABLE");
		    newGridElement.processEnabled = true;
		    newGridElement.oldGridElement = oldGridElement;
		    newGridElement.backupStyle = {position:newGridElement.style.position, top:newGridElement.style.top, left:newGridElement.style.left};
		    newGridElement.style.position = "absolute";
		    newGridElement.style.top = "-10000px";
		    newGridElement.style.left = "-10000px";
		    if ( element == oldGridElement )
		    {
		        element.parentNode.insertBefore(newGridElement, element);
		    }
		    else
		    {
		        newGridElement.parentNode.insertBefore( oldGridElement, newGridElement);
                element.innerHTML = "";		        
		        element.parentNode.replaceChild(newElement, element);
		    }
		}    
		if (!DundasChart.msieFlag)
		{
		    newElement.style.position=oldPos;
		    newElement.style.top=oldTop;
		}
	}
	Garbage.discardElement(tmpDiv); 
	if ( scripts != null )
	{
		try
		{
			for( var i = 0; i < scripts.length; i++)
			{
					eval(scripts[i]);
			}
		}
		catch(e){
		   alert( "ajax javascript error:" + e.message);
		}    
	}
}

ChartCBClass.prototype.processSelect = function ( element, chunk)
{
    if ( chunk.toLowerCase().indexOf("<select") != -1)
    {
		this.processUnknownElement(element, chunk)      
    }
    else 
    {
      element.length = 0;
      
      var ops = chunk.split("~~");
      var selectedIndex = -1;
      for( var opsi = 0; opsi < ops.length; opsi ++)
      {
          var xsrt = ops[opsi].split("//");
          if ( xsrt.length == 3)
          {
              var oOption = document.createElement("OPTION");
              oOption.value = xsrt[0];
              oOption.innerHTML  = xsrt[1];
              if ( xsrt[2] == "true" )
              {
                    oOption.selected  = true;
                    selectedIndex = opsi;
              }
              element.appendChild( oOption);
          }
      }
      element.selectedIndex = selectedIndex;
    }
}

ChartCBClass.prototype.processDiv = function ( element, chunk)
{
    var newDiv = document.createElement("div");
    newDiv.innerHTML = chunk;
//    newDiv.id = "processDiv1";
    if ( newDiv.firstChild )
    {
        var srcElement = newDiv.firstChild;
        element.innerHTML = srcElement.innerHTML;
        this.updatePosition(element, srcElement);
    }
    Garbage.discardElement(newDiv); 
}

ChartCBClass.prototype.pushElement =  function ( chunk, elementID)   
{	
    var newDiv = document.createElement("div");
    newDiv.innerHTML = chunk;
//    newDiv.id="pushElement";
    this.executeStyles( newDiv );
    if ( newDiv.childNodes.length > 0 )
    {
		var elementToPush = newDiv.firstChild;
		var parentElementName = elementToPush.getAttribute ? elementToPush.getAttribute("parent_Container") : null;
		if ( parentElementName)
		{
			var parentElement = document.getElementById(parentElementName);
			if ( parentElement )
			{
				parentElement.appendChild( elementToPush );
			}
		}
    }
	else
	{
		if ( chunk.indexOf("<style") == 0 )
		{
			var style = document.createElement("style");
			style.type = 'text/css';
			style.id = elementID;
			var styleBody = chunk.substring(chunk.indexOf(">")+1);
			styleBody = styleBody.substring(0,styleBody.indexOf("</"));
			if ( DundasChart.msieFlag)
			{
				style.styleSheet.cssText=styleBody;	
			}
			else
			{	
			    if (DundasChart.safariFlag)
			    {		    
			        style.innerText = styleBody;
			    }
			    else
			    {
			        style.innerHTML = styleBody;
			    }
			    
			}
			var heads = document.getElementsByTagName("HEAD");
			if ( heads.length > 0 )
			{
				heads[0].appendChild( style );
			}
			else
			{
				document.appendChild( style)
			}
		}
	}
	Garbage.discardElement(newDiv);
}

ChartCBClass.prototype.processChart =  function ( element, chunk)
{
    var newDiv = document.createElement("div");
//    newDiv.id="processChartId";
    newDiv.innerHTML = chunk;
    var img = newDiv.getElementsByTagName("IMG");
    if ( img.length > 0 )
    {
         img = img[0];
         element.parentNode.replaceChild(img, element);         
         element.src = __DundasScrBrImgBag["empty"].src;   // fix of bug #7432
         Garbage.discardElement(element);
         return;
    }
    var spans = newDiv.getElementsByTagName("SPAN");
    var imgVar = null;
    
    if ( spans.length > 0 )
    {
        var span = spans[0];
        if ( !DundasChart.msieFlag )
        {
            imgVar = element;
			element.src = span.getAttribute("src");
            this.fixChartTreeSize(element, span.style.width, span.style.height);
        }
        else
        {
			var newImageNode = element.cloneNode();
			imgVar = newImageNode;
			newImageNode.src = span.getAttribute("src");
			newImageNode.hopes = 1000;
			var thisObj = this;
			var timerFunc = function() {
				if ( newImageNode.readyState == "complete" || newImageNode.hopes < 1)
				{
					newImageNode.swapNode(element);
					element.src = __DundasScrBrImgBag["empty"].src;                    // fix of bug #7432
					Garbage.discardElement(element); 
                    thisObj.fixChartTreeSize(newImageNode, span.style.width, span.style.height);
                    thisObj = null; // -oan 
				}
				else
				{
					newImageNode.hopes --;
					window.setTimeout(timerFunc,30);
				};
			};		
			window.setTimeout( timerFunc, 30);
        }
    }
    
    var mapList = newDiv.getElementsByTagName("MAP");
    
    
    if ( mapList.length > 0 )
    {
        map = mapList[0];
        // implement the same approach as for img //??
        var newMapNode = element;
        newMapNode.hops = 1000;
        var mapElement = document.getElementById(map.id);

        if ( mapElement )
        {   
            // avoid ff issue
            if (DundasChart.msieFlag)
            {            
                var mapParent = mapElement.parentNode;
                mapParent.removeChild(mapElement);

                var parent = newMapNode.parentNode;
                if (null == parent)
                {
                    parent = mapParent;
                }           
                parent.appendChild( map);
                        
                Garbage.discardElement(mapElement);
            }
            else
            {
                if (DundasChart.safariFlag || DundasChart.geckoFlag && DundasChart.ffVer>=3)
                {
                    mapElement.innerHTML = map.innerHTML;
                }
                else
                {
                    // fix for FF 2.0.0.8 - avoid disabling the imagemap
                    if (imgVar)
                    {
                        imgVar.useMap = "";
                    }
                    var mapParent = mapElement.parentNode;
                    mapParent.appendChild(map);
                    mapParent.removeChild(mapElement);
                    Garbage.discardElement(mapElement);
                    if (imgVar)
                    {
                        imgVar.useMap = map.name;
                    }
                }
            }
        }
        else
        {    		
		    var timerFuncMap = function() 
		    {
		        if ( newMapNode.readyState == "complete" || newMapNode.hops < 1)
                {
                    if (newMapNode.parentNode)
                    {
                        newMapNode.parentNode.appendChild( map);
                    }                    
		        }
		        else
		        {
		            newMapNode.hops--;
			        window.setTimeout(timerFuncMap,30);
		        }
            };		
	        window.setTimeout( timerFuncMap, 30);
        }
    }
    
    Garbage.discardElement(newDiv); 
    
}

ChartCBClass.prototype.fixChartTreeSize = function ( chartImage,  width, height)
{
    var chartID = chartImage.id;
    var element = chartImage;
    while ( element && element.id.indexOf( chartID) == 0)
    {
        if ( !isNaN(parseInt(width)) )
        {
            element.style.width = width;
        }    
        if ( !isNaN(parseInt(height)) )
        {
            element.style.height = height;
        }
        element = element.parentNode;
    }
}


ChartCBClass.prototype.processTable = function ( element, chunk)
{
    var newDiv = document.createElement("div");
    newDiv.innerHTML = chunk;
//    newDiv.id="processTable1";
    var srcElement = null;
    var elArr = newDiv.getElementsByTagName("TABLE");
    for( var arrPos = 0; arrPos < elArr.length; arrPos++)
    {
        if ( elArr[arrPos].id && elArr[arrPos].id == element.id )
        {
            srcElement = elArr[arrPos];
        }
    }
    if ( srcElement )
    {
        var rowCount = element.rows.length;
        for( var rowIndex = 0; rowIndex < rowCount; rowIndex++)
        {
            try
            {
                element.deleteRow(0);
            }
            catch(e) {}
        }

        var tbody = element;
        var tbodies = element.getElementsByTagName("TBODY");
        
        if ( tbodies.length > 0 )
        {
            tbody = tbodies[0];
        }
        
        var tbodySrc = srcElement;
        var tbodiesSrc = srcElement.getElementsByTagName("TBODY");
        
        if ( tbodiesSrc.length > 0 )
        {
            tbodySrc = tbodiesSrc[0];
        }
        
		var rows = new Array(tbodySrc.childNodes.length);
        for( var i = 0; i < tbodySrc.childNodes.length; i++)
        {
			rows[i] = tbodySrc.childNodes[i];
        }
        for( var i = 0; i < rows.length; i++)
        {
        	tbody.appendChild( rows[i]);
		}
			        
        this.updatePosition(element, srcElement);
    }
}

ChartCBClass.prototype.updatePosition = function ( element, srcElement )
{
	if ( element.style.position == "absolute" || element.style.position == "relative" || srcElement.getAttribute("set_pos"))
	{
		if ( srcElement.style.left  ) element.style.left   = srcElement.style.left;
		if ( srcElement.style.top   ) element.style.top    = srcElement.style.top;
		if ( srcElement.style.width ) element.style.width  = srcElement.style.width;
		if ( srcElement.style.height) element.style.height = srcElement.style.height;
	} 
}

ChartCBClass.prototype.getChartPanel = function( context)
{
    var mask = document.getElementById(context.controlName);
    if ( mask && typeof(mask) != "undefined")
    {
        if ( mask.tagName && mask.tagName == "IMG")
        {
            return mask.parentNode;
        }
        return mask;
    }
    return null;
}

ChartCBClass.prototype.showMaskDiv = function ( o, suffix)
{
    
    if (!suffix) suffix = "";
    var mask = document.getElementById("dsCallbackMaskXX" + suffix);
    if (!mask )
    {
		var mask = document.createElement("DIV");
		mask.id = "dsCallbackMaskXX" + suffix;
		mask.style.display = "none";
		mask.style.position = "absolute";
		mask.style.backgroundColor = "white";
		mask.style.filter = "alpha(opacity=0)";
		mask.style.opacity = "0";
		mask.style.zIndex = 1000;
		document.body.appendChild( mask);
    }
    if ( mask )
    {
	    mask.style.cursor = "wait";
	    var fullHeight = this.getScreenHeight();
	    var fullWidth = this.getScreenWidth() - 17;
	    var scTop = parseInt(document.body.scrollTop,10);
	    var scLeft = parseInt(document.body.scrollLeft,10);
	    mask.style.height = fullHeight + "px";
	    mask.style.width = fullWidth + "px";
	    mask.style.top = scTop + "px";
	    mask.style.left = scLeft + "px";
        mask.style.display = "block";
    }
	return mask;
}

ChartCBClass.prototype.hideMaskDiv = function ( o, suffix)
{
    if (!suffix) suffix = "";
    var mask = document.getElementById("dsCallbackMaskXX"  + suffix);
    if ( mask )
    {
	    mask.style.cursor = "default";
        mask.style.display = "none";
	    mask.style.height = "0px";
	    mask.style.width = "0px";
	    mask.style.top = "0px";
	    mask.style.left = "0px";
	    //document.body.removeChild( mask);
    }
	return mask;
}
    
ChartCBClass.prototype._showMaskDiv = function ( context)
{
    var mask = this.getChartPanel(context);
    if ( mask )
    {
        context.saveCursorStyle = mask.style.cursor == "" ? "default" : mask.style.cursor;
        mask.style.cursor = "wait";
    }
	return mask;
}

ChartCBClass.prototype._hideMaskDiv = function (context)
{
    var mask = this.getChartPanel(context);
    if ( mask && context.saveCursorStyle)
    {
	        mask.style.cursor = context.saveCursorStyle;
    }
	return mask;
}
        
ChartCBClass.prototype.getScreenHeight = function () {
    if (window.innerHeight!=window.undefined) return window.innerHeight;
    if (document.compatMode=='CSS1Compat') return document.documentElement.clientHeight;
    if (document.body) return document.body.clientHeight; 
    return window.undefined; 
}
			
ChartCBClass.prototype.getScreenWidth = function () {
    if (window.innerWidth!=window.undefined) return window.innerWidth; 
    if (document.compatMode=='CSS1Compat') return document.documentElement.clientWidth; 
    if (document.body) return document.body.clientWidth; 
    return window.undefined; 
}
        
ChartCBClass.prototype.context = function ( controlName, async)
{
	 var result = new Object();
	 result.controlName = controlName;
	 result.selects = new Dundas_DisableSelects();
	 result.cbObj = this;
	 result.asynchronous = async;
	 if ( this.cursorWait )
	 {
		 result.selects.disable();
		 this.showMaskDiv(result);
	 }
	 return result;
}

ChartCBClass.prototype.error = function( result, context)
{
    context.cbObj.hideMaskDiv(context);
    context.selects.enable();
    dundas_ShowErrorMessage( DundasChart.htmlDecode(result));
}


ChartCBClass.prototype.getXY = function (e, targetElement)
{
	e = DundasChart.fixEvent(e);
	var chartParent = e.target.parentNode;
	if (chartParent)
	{
		chartParent = chartParent.parentNode
	}
	if ( chartParent == targetElement || DundasChart.getFirstChild(chartParent) == targetElement)
	{
		return e.layerX +","+ e.layerY;
	}	
	else
	{
		var firstChild = DundasChart.getFirstChild(targetElement);
		if ( firstChild && firstChild.style.position == "relative" && firstChild.id && targetElement.id && firstChild.id == (targetElement.id + "Ex"))
		{
			targetElement = firstChild;
		}
		var offset = DundasChart.getElmPosition(e.target, targetElement, true, true);
		if ( DundasChart.msieFlag && e.target.tagName == "AREA")
		{
			return (e.layerX) + ","+ (e.layerY);
		}
		return (offset.x + e.layerX) + ","+ (offset.y + e.layerY);
	}
}

ChartCBClass.prototype.postCallBack = function( e, control, arg, target, docallback)
{
    if ( typeof(ChartToolbar) != "undefined" )
    {
        ChartToolbar.closeAllMenus();
    }
    DundasChart.cancelEvent(e);
            
    if ( theForm && WebForm_DoCallback)
    {
        var saveTarget = theForm.target;
		//theForm.target = "my_iframe"; 
        if ( target )
        {
			theForm.target = target;
        }
        
        var saveAction = theForm.action;
        var imp1 = null;
        var imp2 = null;
        
		if ( docallback )
		{        
			imp1 = document.createElement("INPUT");
			imp1.type = "hidden";        
			imp1.name = "__CALLBACKID";
			imp1.id = "__CALLBACKID";
			imp1.value = control;
			theForm.appendChild(imp1);

			imp2 = document.createElement("INPUT");
			imp2.type = "hidden";        
			imp2.name = "__CALLBACKPARAM";
			imp2.id = "__CALLBACKPARAM";
			imp2.value = arg;
			theForm.appendChild(imp2);
        }
        
        __doPostBack(control,arg);

        window.setTimeout( function() {
            theForm.target = saveTarget;
			theForm.__EVENTTARGET.value = "";
			theForm.__EVENTARGUMENT.value = "";
            if ( docallback ) 
            {
				theForm.removeChild(imp2);
				theForm.removeChild(imp1);
			}		
        }, 100);
    }
}    

ChartCBClass.prototype.clearForm = function ()
{
	__theFormPostData = "";
	__theFormPostCollection = new Array();
	
//	// clear grids
//	if (window.areaGridArray != null)
//	{
//	    while (window.areaGridArray.length > 0)
//	    {
//	        var areaGridObj = window.areaGridArray.pop();
//	        areaGridObj.theObj = null; // should release object (?)
//	    }
//	}
	
	if (typeof(WebForm_InitCallback) != 'undefined') WebForm_InitCallback();
}
ChartCBClass.prototype.debugOut = function ( s)
{
	var elm = document.getElementById("debug_out");
	if ( elm )
	{
		var line = document.createElement("DIV");
		line.innerHTML = s;
		elm.appendChild( line);
		elm.scrollTop = 10000;
	}
}    

ChartCBClass.prototype.cleanUp = function()
{
    
}



//*********** END ChartCBClass Object Definition *******************/
var ChartCB = new ChartCBClass();





function dundas_ShowErrorMessage( message)
{
    if ( message.indexOf( "session has expired") != -1) 
     {
        alert(message);
        document.location.replace( document.location.href);
     }
     else
     {
        alert(message);
     }        
}

//*********** START ZoomInfoObj Object Definition *******************/

function ZoomInfoObj( inputElementName )
{
	this.inputElement = document.getElementById(inputElementName);
	if ( this.inputElement )
	{
		var zoomInputStr = this.inputElement.value;
		var zoomInputStrArray = zoomInputStr.split("&");
		for( var i = 0; i < zoomInputStrArray.length; i++)
		{
			var entry = zoomInputStrArray[i].split("=");
			if ( entry.length == 2 )
			{
				this[entry[0]] = entry[1];
			}
		}
	}
}	

ZoomInfoObj.prototype.transformAxisName = function( axisName )
{
	if ( !axisName )
	{
		axisName = "";
	}
	else
	{
		axisName = "-" + axisName;
	}
	return axisName;
}
	
ZoomInfoObj.prototype.getAreaProp = function ( axisName, propName )
{
	return this[propName +  this.transformAxisName(axisName)];		
}
	
ZoomInfoObj.prototype.setAreaProp = function ( axisName, propName, value )
{
	this[propName +  this.transformAxisName(axisName)] = value;
	this.registerPostBackValue();
}
	
ZoomInfoObj.prototype.registerPostBackValue = function()
{
	var output = "";
	for( key in this)
	{
		if ("function,object".indexOf(typeof(this[key])) == -1)
		{
			output += key + "=" + this[key] + "&";
		}		
	}
	this.inputElement.value = output;
}

ZoomInfoObj.prototype.cleanUp = function()
{
    //Garbage.discardElement(this.zoomInfo);
}



//*********** END ZoomInfoObj Object Definition *******************/


//*********** START Dundas_DisableSelects Object Definition *******************/
function Dundas_DisableSelects()
{
    this.msieFlag = window.navigator.userAgent.indexOf("MSIE") > -1;
    this.selects = new Array();
}    

Dundas_DisableSelects.prototype.disable = function ()
{
    if ( this.msieFlag )
    {
		this.selects = new Array();
		var tabs = new Array("SELECT");
		for (var tabi = 0; tabi < tabs.length; tabi++)
		{
			var elements = document.body.getElementsByTagName(tabs[tabi]);
			for(var i = 0; i < elements.length; i++) {
				var elm = elements[i];
				if ( !elm.disabled )
				{
					try
					{
						this.selects[this.selects.length] = elm;
						if ( typeof(elm.blur) == "function" ) elm.blur();
						elm.disabled = true;
					} 
					catch(exc) {;}
				}
			}
	    }
    }
}
    
Dundas_DisableSelects.prototype.enable = function ()
{
    if ( this.msieFlag )
    {
	    for(var i = 0; i < this.selects.length; i++) {
		    try
		    {
		        this.selects[i].disabled=false;
		    }
		    catch(exc) {;};
	    }
    }
}
//*********** END Dundas_DisableSelects Object Definition *******************/

//*********** START DundasRect Object Definition *******************/
function DundasRect( x, y, width, height)
{
	this.X = x;
	this.Y = y;
	this.Width = width;
	this.Height = height;
}	

DundasRect.prototype.intersectsWith = function(rect)
{
	if (((rect.X < (this.X + this.Width)) && (this.X < (rect.X + rect.Width))) && (rect.Y < (this.Y + this.Height)))
	{
        return (this.Y < (rect.Y + rect.Height));
	}
	return false;
}

DundasRect.prototype.getIntersect = function( rect )
{
  var num1 = Math.max(this.X, rect.X);
  var num2 = Math.min((this.X + this.Width), (rect.X + rect.Width));
  var num3 = Math.max(this.Y, rect.Y);
  var num4 = Math.min((this.Y + this.Height), (rect.Y + rect.Height));
  if ((num2 >= num1) && (num4 >= num3))
  {
        return new DundasRect(num1, num3, num2 - num1, num4 - num3);
  }
  return new DundasRect(0,0,0,0);
};

DundasRect.prototype.offset = function( x, y )
{
	this.X += x;
	this.Y += y;
}

DundasRect.prototype.inflate = function( width, height )
{
	this.X -= width;
	this.Y -= height;
	this.Width += 2 * width;
	this.Height += 2 * height;
}

DundasRect.prototype.expand = function( times )
{
	this.inflate( this.Width * times, this.Height * times);
}

DundasRect.prototype.multiply = function( x, y )
{
	this.X = this.X * x;
	this.Y = this.Y * y;
	this.Width = this.Width * x;
	this.Height = this.Height * y;
}

DundasRect.prototype.round = function( )
{
	this.X = Math.round(this.X);
	this.Y = Math.round(this.Y);
	this.Width = Math.round(this.Width);
	this.Height = Math.round(this.Height);
}

DundasRect.prototype.contains = function( x, y )
{
	if (((this.X <= x) && (x < (this.X + this.Width))) && (this.Y <= y))
    {
        return (y < (this.Y + this.Height));
    }
    return false;
}

DundasRect.prototype.isEmpty = function()
{
	 if (((this.Height == 0) && (this.Width == 0)) && (this.X == 0))
     {
              return (this.Y == 0);
     }
     return false;
}

DundasRect.prototype.toString = function()
{
	return "X:" + this.X + ",Y:" + this.Y + ",Width:" + this.Width + ",Height:" + this.Height;
}
//*********** END DundasRect Object Definition *******************/

//*********** START DundasPoint Object Definition *******************/
function DundasPoint ( x, y )
{
	this.x = x;
	this.y = y;
}

DundasPoint.prototype.isValid = function ()
{
	return this.x >= 0 && this.y >= 0;
}
//*********** END DundasPoint Object Definition *******************/

var _gridMouseCaptureObj_ = null;

function DundasChartAreaGrid(	queryUrl, 
								chartName, 
								shortAreaName,			
								areaElementName, 
								loadImageUrl, 
								chunkRect, 
								rowCount, 
								colCount, 
								xReverse, 
								yReverse, 
								parentAreaName,
								selectObj,
								slaveAreaName,
								initPoint,
								innerAreaRect,
								availableRect,
								chartElementId,
								postBackFlag,
								
								// feature request #7591    VERSION_610
								chartScrollEvent,
								chartScrollTimeout,

                                // bug fix #7665								
								alignOrientation
								)
{

    //oan
    // create array to clear grids before callback
    
//    if (!window.areaGridArray)
//    {
//        window.areaGridArray = new Array();
//    }
//    else
//    {
        
//    }
    
    Garbage.cleanUpChartArea(areaElementName);
    
//    window.areaGridArray.push(this);

	this.queryUrl = queryUrl.replace(/&amp;/g,"&");
	this.chartName = chartName;
	this.chartElementId = chartElementId;
	this.areaName = areaElementName;
	this.chunkRect = chunkRect;
	this.chunkWidth = chunkRect.Width;
	this.chunkHeight = chunkRect.Height;
	this.innerAreaName = this.areaName + "T";
	this.area = document.getElementById(this.areaName);
	this.rowCount = rowCount;
	this.colCount = colCount; 	
	this.viewWidth = parseInt(this.area.style.width);
	this.viewHeight = parseInt(this.area.style.height);
	this.maxScrollHeight = this.chunkHeight * this.rowCount;
	this.maxScrollWidth  = this.chunkWidth * this.colCount;
	this.emptyImageUrl = loadImageUrl;
	this.xReverse = xReverse;
	this.yReverse = yReverse;
	this.innerArea = null;
	this.parentAreaName = parentAreaName;
	this.slaveAreaName  = slaveAreaName;
	this.timeoutID = -1;
	this.childAreas = null;
	this.selectObj = selectObj;
	this.selectionActive = false;
	this.startAbsPosition = {x:0, y:0};
	this.shortAreaName = shortAreaName;
	this.zoomInfo = new ZoomInfoObj( this.areaName + "Info");
	this.callbackScrollers  = {v: null, h: null};
	this.initPoint = initPoint;
	this.innerAreaRect = innerAreaRect;
	this.availableRect = availableRect;
	document.postBackFlag = postBackFlag;
	ChartCB.postbackFlag = postBackFlag;

    // feature request #7591    VERSION_610
	this.chartScrollEvent = chartScrollEvent; 	
    this.chartScrollTimeout = chartScrollTimeout;
	this.chartScrollTimeoutCall= null;
	//
	
	// bug fix #7665
    this.alignOrientation = alignOrientation;
    //

	this.initialize();
}	

DundasChartAreaGrid.prototype.updateZoom = function ( prop, value, axis )	
{
	if ( value > 0 && prop.indexOf("sel-") == 0)
	{
		value = this.getReverseCompensation(axis == "v", value, false);
	}
	if ( value > 0 && prop.indexOf("position-") == 0)
	{
		value = this.getReverseCompensation(axis == "v", value, true);
	}
	this.zoomInfo.setAreaProp( axis, prop, value );
}
	
DundasChartAreaGrid.prototype.getViewRect = function()	
{
    var result = new DundasRect( 0, 0, parseInt( this.area.style.width), parseInt( this.area.style.height));
    return result;
}
	
DundasChartAreaGrid.prototype.getChildAreaObjs = function()
{
	if ( !this.childAreas )
	{
		this.childAreas = new Array();
		for( var i = 0; i < 10; i++)
		{
			var element = document.getElementById(this.areaName + "Child" + i);
			if ( element && element.theObj)
			{
				this.childAreas.push( element.theObj );
			}
		}
	}
	return this.childAreas
}
	
	
DundasChartAreaGrid.prototype.invalidate = function( direct)
{
	if ( this.parentAreaName )
	{
		return;
	}  
	if ( direct )
	{
		this.ensureVisible();
	}
	else
	{
		if ( this.timeoutID != -1 )
		{
			window.clearTimeout(this.timeoutID)
		}
		var thisObject = this;
		this.timeoutID = window.setTimeout( function() { thisObject.ensureVisible(); thisObject = null; }, 50);
	}
},

DundasChartAreaGrid.prototype.getSlaveArea = function()
{
	var result = new Array();
	var slaveSreas = this.slaveAreaName.split("|~|");
	for( var ii = 0; ii < slaveSreas.length; ii++)
	{
		var el = document.getElementById(slaveSreas[ii]);
		if ( el && el.theObj )
		{
			result.push(el.theObj);
		}
	}
	return result;
};

DundasChartAreaGrid.prototype.getCompensation = function( vertical)
{
	if ( vertical && !this.yReverse)
	{
		return this.maxScrollHeight - this.availableRect.Height;
	}
	if ( !vertical && this.xReverse)
	{
		return this.maxScrollWidth - this.availableRect.Width;
	}
	return 0;
}

DundasChartAreaGrid.prototype.getReverseCompensation = function( vertical, value, setPos)
{
	var result = value;
	if ( vertical )
	{
		if ( !this.yReverse )
		{
			if ( setPos) 
			{
				value = this.maxScrollHeight - value - this.viewHeight;
			}
			else 
			{
				value = this.maxScrollHeight - value;
				value -= this.fScrollTop();
			}
		}
		else if (!setPos)
		{
			value += this.fScrollTop();
		}
	}
	else 
	{
		if (this.xReverse)
		{
			if ( setPos)
			{
				value = this.maxScrollWidth - value - this.viewWidth;
			}
			else
			{
				value = this.maxScrollWidth - value;
				value -= this.fScrollLeft();
			}
		}
		else if (!setPos)
		{
			value += this.fScrollLeft();
		}
	}
	return value;
}

DundasChartAreaGrid.prototype.fScrollLeft = function( posLeft )
{
	if ( typeof(posLeft) == "number" )
	{
		this.innerArea.style.left = -(posLeft) + "px";
		this.innerArea.calculatedLeft = posLeft;
	}
	return this.innerArea.calculatedLeft ? this.innerArea.calculatedLeft : 0;
}

DundasChartAreaGrid.prototype.fScrollTop = function( posTop )
{
	if ( typeof(posTop) == "number" )
	{
		this.innerArea.style.top = -(posTop) + "px";
		this.innerArea.calculatedTop = posTop;
	}
	return this.innerArea.calculatedTop ? this.innerArea.calculatedTop : 0;
}


DundasChartAreaGrid.prototype.setScrollLeft = function( positionLeft, scrollLength, direct, cback, srcArea )
{
	if ( this.colCount == 1 )
	{
		return;
	}
	var compensation = this.getCompensation();
	if ( this.fScrollLeft() != (compensation + positionLeft) || direct)
	{
		var maxScroll = this.maxScrollWidth - this.viewWidth; 
		this.fScrollLeft(Math.min(compensation + positionLeft, maxScroll));
		if ( !this.parentAreaName ) this.updateZoom("position-px", this.fScrollLeft(), "h");
		var childAreaList = this.getChildAreaObjs();
		for( var i = 0; i < childAreaList.length; i ++)
		{
			childAreaList[i].setScrollLeft( positionLeft, 0);
		}
		if ( cback )
		{
			if ( this.callbackScrollers.h)
			{
				this.callbackScrollers.h.setAbsPos(positionLeft, true);
			}
		}
		//else 
		if ( !this.parentAreaName )
		{
			var slaveAreas = this.getSlaveArea();
			if ( slaveAreas.length > 0 )
			{
				for(var ix = 0; ix < slaveAreas.length; ix++)
				{
					if ( srcArea != slaveAreas[ix])
					{
					    // bug fix #7665
						if (slaveAreas[ix].alignOrientation == 'vertical' || slaveAreas[ix].alignOrientation == 'all')
						{
    						slaveAreas[ix].setScrollLeft( positionLeft, scrollLength, direct, true, this);
						}
						else
						{
    					    if (slaveAreas[ix].alignOrientation == 'master' && (this.alignOrientation == 'vertical' || this.alignOrientation == 'all'))
	    				    {
           						slaveAreas[ix].setScrollLeft( positionLeft, scrollLength, direct, true, this);					    
		    			    }
						}
						//////////////
					}
				}
			}
		}

        // feature request #7591
		if (typeof(direct) != "undefined" && this.chartScrollEvent == true)
		{
            var obj = this;
            clearTimeout(obj.chartScrollTimeoutCall);
             if (obj.callbackScrollers.h)
            {
   			    obj.chartScrollTimeoutCall = window.setTimeout( function() { obj.callChartScrollEvent(obj.callbackScrollers.h.elementName); }, obj.chartScrollTimeout);
   			}
        }
        //
        
        this.invalidate( direct );
	}
};

DundasChartAreaGrid.prototype.setScrollTop = function( positionTop, scrollLength, direct, cback, srcArea )
{
	if ( this.rowCount == 1 )
	{
		return;
	}
	var compensation = 0; 
	if ( !this.yReverse)
	{
		compensation = this.getCompensation(true);
	}
	if ( this.fScrollTop() != (compensation + positionTop) || direct)
	{
		var maxScroll = this.maxScrollHeight - this.viewHeight;  
		this.fScrollTop(Math.min(compensation + positionTop, maxScroll));
		if ( !this.parentAreaName ) this.updateZoom("position-px", this.fScrollTop(), "v");
		var childAreaList = this.getChildAreaObjs();
		for( var i = 0; i < childAreaList.length; i ++)
		{
			childAreaList[i].setScrollTop( positionTop, scrollLength);
		}
		
		if ( cback )
		{
			if ( this.callbackScrollers.v)
			{
			    if (positionTop > this.callbackScrollers.v.initPosition)
			    {
				    this.callbackScrollers.v.setAbsPos(this.callbackScrollers.v.initPosition, true);
			    }
			    else
			    {
				    this.callbackScrollers.v.setAbsPos(positionTop, true);
				}
			}
		}

		if ( !this.parentAreaName ) 
		{
			var slaveAreas = this.getSlaveArea();
			if ( slaveAreas.length > 0 )
			{
				for(var ix = 0; ix < slaveAreas.length; ix++)
				{
					if ( srcArea != slaveAreas[ix])
					{
					    // bug fix #7665
						if (slaveAreas[ix].alignOrientation == 'horizontal' || slaveAreas[ix].alignOrientation == 'all')
						{
						    slaveAreas[ix].setScrollTop( positionTop, scrollLength, direct, true, this);
						}
						else
						{
    					    if (slaveAreas[ix].alignOrientation == 'master' && (this.alignOrientation == 'horizontal' || this.alignOrientation == 'all'))
	    				    {
						        slaveAreas[ix].setScrollTop( positionTop, scrollLength, direct, true, this);
		    			    }
						}
						//////////////
					}
				}
			}
		}
        
        // feature request #7591
		if (typeof(direct) != "undefined" && this.chartScrollEvent == true)
		{
            var obj = this;
            clearTimeout(obj.chartScrollTimeoutCall);
   			obj.chartScrollTimeoutCall = window.setTimeout( function() { obj.callChartScrollEvent(obj.callbackScrollers.v.elementName); }, obj.chartScrollTimeout);
        }
        //
        
	    this.invalidate( direct );
	}
};

DundasChartAreaGrid.prototype.scrollTo = function( x, y, compenX, compenY)
{
	var obj = this;
	window.setTimeout( function() { obj.setScrollLeft( x, compenX); }, 0);
	window.setTimeout( function() { obj.setScrollTop( y, compenY); }, 0);
};

// feature request #7591
DundasChartAreaGrid.prototype.callChartScrollEvent = function( scrollerElementName )
{
	var query = this.queryUrl.substring(this.queryUrl.indexOf("&a=") + 2);
	var index = query.indexOf("&");
	if ( index > -1)
	{
	    query = "&area" + query.substring(0, index);
	}
	var axisName = scrollerElementName.substring(scrollerElementName.length - 1);
	query = query + "&axis=" + axisName;
	ChartCB.call(null, this.chartName, "{dsi}-scrolling::" + query);
}
//

DundasChartAreaGrid.prototype.requestError = function (responseText, context)
{
	alert( responseText );
};

DundasChartAreaGrid.prototype.getCell = function( locationRow, locationCol )
{
	if ( this.rowCount == 1 )
	{
		locationRow = 0;
	} 
	if ( this.colCount == 1 )
	{
		locationCol = 0;
	} 
	if (this.innerArea.cellsArray )
	{
	    var result = this.innerArea.cellsArray[ locationRow + (locationCol * this.rowCount) ];
	    if ( !result )
	    {
	        result = this.getNewCell( locationRow, locationCol)
	    }
	    return result;
	}
	return this.innerArea.childNodes[ locationRow + (locationCol * this.rowCount) ];
}

DundasChartAreaGrid.prototype.getByTag = function (cell, tag) 
{
	for( var i = 0; i < cell.childNodes.length; i++)
	{
		var child =  cell.childNodes[i];
		if ( child.tagName && child.tagName.toUpperCase() == tag)
		{
			return child;
		}
	}
	return null;
}

DundasChartAreaGrid.prototype.getImage = function (cell) { return this.getByTag( cell, "IMG");	}

DundasChartAreaGrid.prototype.getMap = function (cell) 	{return this.getByTag( cell, "MAP");	};

DundasChartAreaGrid.prototype.updateImageAsync = function( locationRow, locationCol, imageUrl, imageMap)
{
	var obj = this;
	window.setTimeout( function() { obj.updateImage(locationRow, locationCol, imageUrl, imageMap)}, 10);
}

DundasChartAreaGrid.prototype.updateImage = function( locationRow, locationCol, imageUrl, imageMap)
{
	locationRow = this.yReverse ? locationRow : this.rowCount - locationRow - 1;
	locationCol = this.xReverse ? this.colCount - locationCol - 1 : locationCol;
	var cell = this.getCell( locationRow, locationCol);
	if ( ! cell )
	{
		return;
	}
	if ( cell.updated )
	{
		return;
	}
	var img = this.getImage( cell);
	if ( img )
	{	
		if ( img.parentNode && img.parentNode.tagName &&  img.parentNode.tagName.toUpperCase() == "DIV")
		{
			var divNode = img.parentNode;
			divNode.style.backgroundImage = "";
			if (DundasChart.msieFlag)
			{
					divNode.style.filter = "";
			}
		}
		if ( !DundasChart.supportPng)
		{
			img.style.filter = "progid:DXImageTransform.Microsoft.AlphaImageLoader(src='"+imageUrl+"')";
		}
		else
		{
			img.src = imageUrl;
		}
		if ( img.style.display == "none" )
		{
			img.style.display == "block";
		}
		if ( imageMap && imageMap != "")
		{
			var imgMap = this.getMap( cell);
			if (imgMap )
			{
				imgMap.innerHTML = imageMap;
			}
		}
	}
	cell.updated = true;
};

DundasChartAreaGrid.prototype.requestComplete = function( responseText, context )
{
	if ( responseText == "--reload--")
	{
		ChartCB.refreshPage();
		return;
	}
	responseText = responseText.substring( 1, responseText.length - 3)
	var items = responseText.split("!]].[");
	var childAreaList = context.grid.getChildAreaObjs();
	var cell = context.currentCell;
    for( var itemPos = 0; itemPos < items.length; itemPos++)
    {
        var data = items[itemPos].split(":[!");
        if ( data.length == 2 )
        {
			var elementName = data[0];
			var elementContent = data[1].split("##");
			if ( elementName == context.grid.areaName )
			{
				context.grid.updateImage( cell.cellRowIndex, cell.cellColIndex, elementContent[0], elementContent[1]);
			}
			else
			{
				for( var i = 0; i < childAreaList.length; i++)
				{
					if ( elementName == childAreaList[i].areaName )
					{
						childAreaList[i].updateImage( cell.cellRowIndex, cell.cellColIndex, elementContent[0], elementContent[1]);
					}
				}
			}
        }
    }    		
};

DundasChartAreaGrid.prototype.checkRequest = function( request )
{
	var respText = null;
	var msg = "";
	try {
		respText = request.GetLastResponseText();
	}
	catch (e) {
		msg = e.message;
	};
	
	if (respText && respText.length > 0 )
	{ 
		this.requestComplete( request.GetLastResponseText().substring(1), request.context)		
	}	
}
		
DundasChartAreaGrid.prototype.setupCell = function ( cell, r, c )
{
	cell.cellRowIndex = this.yReverse ? r : this.rowCount - r - 1;
	cell.cellColIndex = this.xReverse ? this.colCount - c - 1 : c;
	cell.locationRow = r;
	cell.locationCol = c;
	cell.isLoaded     = false;
};

DundasChartAreaGrid.prototype.isUpdated = function( parentCell)
{
	var cell = this.getCell(parentCell.locationRow, parentCell.locationCol);
	return cell.updated;		
}

DundasChartAreaGrid.prototype.isChildsNeedsUpdate = function( cell)
{
	var force = false;
	var childAreaList = this.getChildAreaObjs();
	for( var i = 0; i < childAreaList.length; i++)
	{
		if (!childAreaList[i].isUpdated(cell))
		{
			return true;
		}
	}				
	return false;		
}
	
DundasChartAreaGrid.prototype.ensureVisible = function()
{
	if ( (this.rowCount * this.colCount) <= 1 )
	{
		return;
	}
	this.timeoutID = -1;
	var table = this.innerArea;
	var viewRect = new DundasRect(this.fScrollLeft(), this.fScrollTop(), this.viewWidth, this.viewHeight);
	viewRect.expand(1);
	var prepCells = new Array();
	for( var r = 0; r < this.rowCount; r++)
	{
		for( var c = 0; c < this.colCount; c ++)
		{
			var cellRect = new DundasRect( c * this.chunkRect.Width, r * this.chunkRect.Height , this.chunkRect.Width, this.chunkRect.Height );
			if ( cellRect.intersectsWith(viewRect) )
			{
			    var cell = this.getCell( r, c );
			    
			    if (null == cell)
			    {
			        continue;
			    }
			    
			    if (cell.cellRowIndex == window.undefined)
			    {
				    this.setupCell(cell, r, c);
			    }
			    if ( !cell.isLoaded && !cell.updated)
			    {
				    if ( cell.cellRect == window.undefined)
				    {
					    cell.cellRect = new DundasRect( cell.offsetLeft, cell.offsetTop, cell.offsetWidth, cell.offsetHeight )
				    }
				    if ( cell.cellRect.intersectsWith(viewRect))
				    {
						    if ( __scrollingHandlerMode__ )
						    {
							    var query = this.queryUrl + "&x=" + cell.cellColIndex + "&y=" + cell.cellRowIndex + "&force=" + this.isChildsNeedsUpdate(cell) + "&_tmstm_=" + (new Date()).toString();
							    var context = { grid: this, currentCell: cell, queryUrl : query};
							    DundasAjaxCallback.callPost( query, "", context, this.requestComplete, this.requestError);
							    cell.isLoaded = true;
						    }
						    else
						    {
							    prepCells.push( cell );
						    }
				    }
			    }
			}
		}
	}
	if ( prepCells.length > 0 )
	{
		var query = "{dsi}-zoomimg::" + this.queryUrl.substring(this.queryUrl.indexOf("&a=") +1) + "&count=" + prepCells.length;
		for( var i = 0; i < prepCells.length; i++)
		{
			var cell = prepCells[i];
			query += "&x"+i+"=" + cell.cellColIndex + "&y"+i+"=" + cell.cellRowIndex + "&force"+i+"=" + this.isChildsNeedsUpdate(cell);
		}
		query += "&_tmstm_=" + (new Date()).toString();
		ChartCB.cursorWait = false;
		ChartCB.postBackFlag = false;
		ChartCB.call(null, this.chartName, query, null, null, null, true,true);
		for( var i = 0; i < prepCells.length; i++)
		{
			prepCells[i].isLoaded = true;
		}
	}
	
	while (prepCells.length>0)
	{
	    prepCells.pop();
	}
	
	prepCells = null;
	table = null;
	
};


DundasChartAreaGrid.prototype.getRelMousePosition = function(e)
{
    var targetObj = e.target;
    if (targetObj.tagName && targetObj.tagName == "AREA"
		&& (targetObj.parentNode == this.chartImageMap || targetObj.parentNode == this.innerImageMap))
    {
        var im = this.chartImageMap;
        var dp = new DundasPoint(
				e.layerX + im.offsetLeft - parseInt(this.area.style.left),
				e.layerY + im.offsetTop - parseInt(this.area.style.top)
			);
        return dp;
    }

    if (targetObj)
    {
        var parent = targetObj;
        var found = true;
        if (parent != this.area)
        {
            found = false;
            while (parent)
            {
                parent = parent.parentNode;
                if (parent == this.area)
                {
                    found = true;
                    break;
                }
            }
        }
        if (!found)
        {
            return new DundasPoint(-1, -1);
        }
    }
    var offset = new DundasPoint(0, 0);
    if (targetObj.tagName && targetObj.tagName == "AREA")
    {
        offset = DundasChart.getElmPosition(targetObj, targetObj.parentNode);
        targetObj = targetObj.parentNode.parentNode;
    }
    var elementPos = DundasChart.getElmPosition(targetObj, this.area);

    // ie8 fix
    if (DundasChart.msieFlag && DundasChart.msieVer >= 8)
    {
        elementPos.x = e.x - this.area.offsetLeft;
        elementPos.y = e.y - this.area.offsetTop;
    }
    else
    {
        // 
        elementPos.x += e.layerX;
        elementPos.y += e.layerY;
    }
    var dp = new DundasPoint(elementPos.x, elementPos.y)
    return dp;
};

DundasChartAreaGrid.prototype.restrictPoint = function ( relPos )
{
	 relPos.x = Math.min( Math.max( relPos.x, 0), this.availableRect.Width);
	 relPos.y = Math.min( Math.max( relPos.y, 0), this.availableRect.Height);
	 return relPos;
}

DundasChartAreaGrid.prototype.mouseDownCustom = function ( e )
{
	var obj = this.theObj;
	if ( obj.selectObj )
	{
		 var relPos = obj.getRelMousePosition( e );
		 if ( !relPos.isValid())
		 {
			return;
		 }
		 relPos = obj.restrictPoint(relPos);
		 obj.selectObj.selectStart( relPos);
	}
	_gridMouseCaptureObj_ = obj;
	DundasChart.attachEvent(document, "mouseup", obj.mouseUp);
	DundasChart.attachEvent(document, "mousemove", obj.mouseMove);
	DundasChart.attachEvent(document, "keypress", obj.keyPress);
	DundasChart.cancelEvent(e);
}

DundasChartAreaGrid.prototype.mouseDown = function ( e )
{
	
	var e = DundasChart.fixEvent(e);
	if ( e && (e.button > 1 || e.ctrlKey ) )
	{
		return;
	}	
	
	if ( e.target.tagName && e.target.tagName == "AREA" )
    {
	    if ( typeof(e.target.onclick) == "function" )
	    {
		    return;
	    }	
	    if ( typeof(e.target.href) == "string" && e.target.href.length > 0)
	    {
		    return;
	    }	
	}
	
	var obj = this.theObj;
	{
		obj.theObj = obj;
		obj.mouseDownCustom(e);			    	
	}
};

DundasChartAreaGrid.prototype.keyPress = function ( e )
{
	if (!_gridMouseCaptureObj_) return;
	e = DundasChart.fixEvent(e);
	if ( e && e.keyCode == 27 )
	{
		var obj = _gridMouseCaptureObj_;
		obj.cancelSelection();	
		DundasChart.detachEvent(document, "mouseup", obj.mouseUp);
		DundasChart.detachEvent(document, "mousemove", obj.mouseMove);
		DundasChart.detachEvent(document, "keypress", obj.keyPress);
		_gridMouseCaptureObj_ = null;
        obj.theObj = null;		
	}
}

DundasChartAreaGrid.prototype.mouseUp = function ( e )
{
	if (!_gridMouseCaptureObj_) return;
	var e = DundasChart.fixEvent(e);
	
	var obj = _gridMouseCaptureObj_;
	if ( obj.selectObj )
	{
		 var relPos = obj.getRelMousePosition( e );
		 if ( relPos.isValid())
		 {
			relPos = obj.restrictPoint(relPos);
			obj.selectObj.selectEnd(relPos);
		 }
	}
	DundasChart.detachEvent(document, "mouseup", obj.mouseUp);
	DundasChart.detachEvent(document, "mousemove", obj.mouseMove);
	DundasChart.detachEvent(document, "keypress", obj.keyPress);
	_gridMouseCaptureObj_ = null;
	
	obj.theObj = null;

    // fix for 8040 - 'if (obj.selectObj)' added
	if (obj.selectObj)
	{	
	    if ( obj.selectObj.isValidSelection() )
	    {
		    ChartCB.call(e, obj.chartName, "{dsi}-zoom::" + obj.shortAreaName);
            if ( !DundasChart.geckoFlag)
		    {
		        ChartCB.cancelNextClick = true;
		    }
	    }
	    else 
	    {
		    obj.selectObj.cancelSelection();
	    }
	} 
};

DundasChartAreaGrid.prototype.mouseMove = function ( e )
{
	if (!_gridMouseCaptureObj_) return;
	var e = DundasChart.fixEvent(e);
	var obj = _gridMouseCaptureObj_;
	if ( obj.selectObj )
	{
		 var relPos = obj.getRelMousePosition( e );
		 if ( relPos.isValid())
		 {
			relPos = obj.restrictPoint(relPos);
			obj.selectObj.selectProcess(relPos);
			// oan
			obj.theObj = null;
		 }
	}
	DundasChart.cancelEvent(e);
};

DundasChartAreaGrid.prototype.isValidSelection = function()
{
	var rect = this.selectObj.getSelectedAreaRect();
};
	
DundasChartAreaGrid.prototype.cancelSelection = function()
{
	if ( this.selectObj )
	{
		this.selectObj.cancelSelection();
	}
}

DundasChartAreaGrid.prototype.getNewCell = function( r, c)
{
    if ( !this.cellPrototype )
    {
        var arr = new Array();
        this.pushCell( arr, 0, 0 )
        var div = document.createElement("DIV");
//        div.id="getNewCell";
        div.innerHTML = arr.join(""); 
        this.cellPrototype = div.firstChild;
        Garbage.discardElement(div);
    }    
    var result = this.cellPrototype.clone(true);
	var posx = c * this.chunkWidth;
	var posy = r * this.chunkHeight;
	var itemName = this.innerAreaName + '_' + r + '_' + c;
    result.id = itemName;
    result.style.top  = posy + "px";
    result.style.left = posx + "px";
    result.childNodes[0].useMap =  "#" + itemName;   
    result.childNodes[1].id =  itemName;    
    return result;
}

DundasChartAreaGrid.prototype.pushCell = function ( sArray, r, c )
{
		var posx = c * this.chunkWidth;
		var posy = r * this.chunkHeight;
		var itemName = this.innerAreaName + '_' + r + '_' + c;
		var backGround = "";
		if ( (this.colCount > 1 || this.rowCount > 1) && !this.parentAreaName)
		{
			if ((this.colCount + this.rowCount ) > 2 )
			{
				var imageSrc = this.colCount > 1 || this.rowCount > 1 ?  __DundasScrBrImgBag['not_rendered'].src : __DundasScrBrImgBag['empty'].src;
			    backGround = 'background-image:url('+imageSrc+'); background-position: center center; background-repeat:no-repeat;';
			}
		}
		sArray.push('<div style="width:'+this.chunkWidth+'px; height:'+this.chunkHeight+'px; position:absolute; top:'+posy+'px; left:'+posx+'px;' + backGround + '" >');
		sArray.push('<img src="' + this.emptyImageUrl + '" alt="" width="' + this.chunkWidth + 'px" height="' + this.chunkHeight + 'px" usemap="#' + itemName +'Map" border="0"/>');
		sArray.push('<map id="' + itemName  + 'Map" name="' + itemName  + 'Map" ></map>');
		sArray.push('</div>');
};

DundasChartAreaGrid.prototype.getTableSource = function()
{
	var sArray = new Array();
	//sArray.push( '<div id="'+ this.innerAreaName +'" style="background:transparent; margin:0px; padding: 0px; position: absolute; top:0px; left:0px; width:'+this.maxScrollWidth+'px; height:'+this.maxScrollHeight+'px" >');
	sArray.push( '<div id="'+ this.innerAreaName +'" style="margin:0px; padding: 0px; position: absolute; top:0px; left:0px; width:'+this.maxScrollWidth+'px; height:'+this.maxScrollHeight+'px" >');
	for( var c = 0; c < this.colCount; c ++)
	{
		for( var r = 0; r < this.rowCount; r++)
		{
			this.pushCell( sArray, r, c );
		}
	}
	sArray.push( '</div>');
	return sArray.join("");
};

DundasChartAreaGrid.prototype.getNewCell = function( r, c)
{
    if ( !this.cellPrototype )
    {
        var arr = new Array();
        this.pushCell( arr, 0, 0 )
        var div = document.createElement("DIV");
//        div.id="getNewCell1";
        div.innerHTML = arr.join(""); 
        this.cellPrototype = div.firstChild;        
//        ChartCB.debugOut("create cell prototype");
    }    
    var result = this.cellPrototype.cloneNode(true);
	var posx = c * this.chunkWidth;
	var posy = r * this.chunkHeight;
	var itemName = this.innerAreaName + '_' + r + '_' + c;
    result.id = itemName;
    result.style.top  = posy + "px";
    result.style.left = posx + "px";
    if (result.childNodes.length > 1)
    {
        result.childNodes[0].useMap =  "#" + itemName;   
        result.childNodes[1].id =  itemName;    
        result.childNodes[1].name =  itemName;    
    }
    this.innerArea.appendChild( result );
    this.innerArea.cellsArray[ r + (c * this.rowCount) ] = result;
    return result;
}

DundasChartAreaGrid.prototype.getTableSourceStr = function( imgSrc)
{
	var sArray = new Array();
	sArray.push( '<div id="'+ this.innerAreaName +'" style="margin:0px; padding: 0px; position: absolute; top:0px; left:0px; width:'+this.maxScrollWidth+'px; height:'+this.maxScrollHeight+'px" >');
	if ( imgSrc )sArray.push( imgSrc);
	sArray.push( '</div>');
	return sArray.join("");
};

DundasChartAreaGrid.prototype.populateArea = function()
{
    this.area.innerHTML = this.getTableSourceStr();
    this.innerArea = document.getElementById(this.innerAreaName);
    this.innerArea.cellsArray = new Array((this.colCount + 1) * (this.rowCount+1));
};

DundasChartAreaGrid.prototype.cleanRes = function()
{   
    if (this.selectObj)
    {
        if (this.selectObj.hCursor)
        {   
            this.selectObj.hCursor.element = null; 
            this.selectObj.hCursor = null;            
        }
        
        if (this.selectObj.vCursor)
        {   
            this.selectObj.vCursor.element = null;             
            this.selectObj.vCursor = null;
        }
        
        this.selectObj.element = null;
    }        
    
    if (this.callbackScrollers)
    {   
        if (this.callbackScrollers.v)
        {
            this.callbackScrollers.v.cleanUp();
        }
        
        if (this.callbackScrollers.h)
        {
            this.callbackScrollers.h.cleanUp();
        }    
    }
    
    this.element = null;

    
    if (this.chartImageMap)
    {
        Garbage.discardElement(this.chartImageMap);
    }   
    
    this.chartImageMap = null;
    
    this.innerImageMap = null;
    
    if (this.innerArea)
    {    
        if (this.innerArea.cellsArray)
        {
            while (this.innerArea.cellsArray.length>0)
            {
                var img = this.innerArea.cellsArray.pop();
                img = null;
            }
        }
    
        DundasChart.detachEvent(this.innerArea,"mousedown",this.mouseDown);
        
        this.innerArea.theObj = null;
    }
    
    if (this.zoomInfo)
    {    
        this.zoomInfo.cleanUp();
    }

    this.selectObj = null;

    if (this.area)
    {
        this.area.theObj = null;
    }
    
    var childAreasList = this.getChildAreaObjs();
    
    if (childAreasList)
    {
        while(this.childAreas.length > 0)
        {
            var childArea = this.childAreas.pop();
            childArea.cleanRes();
        }
    }
    this.childAreas = null;
    
	if ( !this.parentAreaName )
	{
	    /*
		var slaveAreas = this.getSlaveArea();
		
		while (slaveAreas.length>0)
		{
		    var a = slaveAreas.pop();
		    a.cleanRes();
		}
		
		slaveaAreas = null;
		*/
	}
};

DundasChartAreaGrid.prototype.initialize = function()
{
	if ( (this.colCount * this.rowCount) > 1 )
	{
		if ((this.colCount * this.rowCount) < 1000 )
		{
			this.area.innerHTML = this.getTableSource();
			this.innerArea = document.getElementById(this.innerAreaName);
		}
		else
		{
			this.populateArea();
		}
	}    
	else
	{
		var chartElementMap = document.getElementById(this.chartElementId + "ImageMap");
		if (chartElementMap)
		{
			var rect = new DundasRect( parseInt(this.area.style.left), parseInt(this.area.style.top), 0, 0);
			var chartImg = document.getElementById(this.chartElementId);
			rect.Width = chartImg.width == 0 ? parseInt(chartImg.style.width) : chartImg.width;
			rect.Height = chartImg.height == 0 ? parseInt(chartImg.style.height) : chartImg.height;
			var newMap = chartElementMap.cloneNode(false);
			newMap.id = this.innerAreaName + "InnMap";
			
			newMap.name = newMap.id;
			var imgSrc = '<img id="'+newMap.id +'Img" usemap="#'+newMap.id +'" src="'+__DundasScrBrImgBag["empty"].src+'" width="'+rect.Width + 'px" height="'+rect.Height+'px" style="position:absolute;top:-'+rect.Y+'px;left:-'+rect.X+'px;" />';
			this.area.innerHTML = this.getTableSourceStr(imgSrc);

			this.innerArea = document.getElementById(this.innerAreaName);
            this.innerArea.appendChild( newMap);			
			newMap.innerHTML = chartElementMap.innerHTML;
			
			this.innerImageMap = newMap;
			this.chartImageMap = chartElementMap;
			Garbage.discardElement(chartElementMap);
		}
		else
		{
			this.area.innerHTML = this.getTableSource();
		}
		this.innerArea = document.getElementById(this.innerAreaName);
	}
	this.innerArea.theObj = this;
	this.area.theObj = this;

    //DundasChart.attachEvent( this, "unload", Garbage.cleanUpChartArea(this.areaName));
	
	if ( this.selectObj )
	{
		this.selectObj.populate( this );
		this.innerArea.onmousedown = this.mouseDown;
	}
	
	if ( this.availableRect )
	{
		var innerWidth  = parseInt(this.innerArea.style.width);
		var innerHeight = parseInt(this.innerArea.style.height);
		
		if ( this.availableRect.Width == 0 )
		{
			this.availableRect.Width = this.maxScrollWidth;
		}
		if ( this.availableRect.Height == 0 )
		{
			this.availableRect.Height = this.maxScrollHeight;
		}
		if ( this.xReverse )
		{
			var shift = this.maxScrollWidth - this.availableRect.Width;
			this.availableRect.X =  shift;
		}
		if ( !this.yReverse )
		{
			var shift = this.maxScrollHeight - this.availableRect.Height;
			this.availableRect.Y =  shift;
		}
	}
	else
	{
		this.availableRect = new DundasRect( 0, 0, this.maxScrollWidth, this.maxScrollHeight);
	}
}

//*********** START DundasAreaSelect Object Definition *******************/

function DundasAreaSelect( color, hCursor, vCursor )
{
	this.hCursor = hCursor;
	this.color   = typeof(color) == "string" ? color : "#000000";
	this.vCursor = vCursor;
	this.areaObj = null;
	this.element = null;
	this.startPoint = new DundasPoint( -1, -1);
	this.endPoint   = new DundasPoint( -1, -1);
}	

DundasAreaSelect.prototype.isValidSelection = function()
{
	var rect = this.getSelectedAreaRect();
	var valid = false;
	if ( this.vCursor )
	{
		valid = rect.Width > 10;
	}
	if ( this.hCursor )
	{
		valid = valid || rect.Height > 10;
	}
	return valid;
};
	
DundasAreaSelect.prototype.initialize = function()
{
	
};	
	
DundasAreaSelect.prototype.selectEnd = function( pos )
{
	this.endPoint = pos;
	this.invalidate();
}

DundasAreaSelect.prototype.selectStart = function( pos )
{
	this.startPoint = pos;
	this.endPoint = pos;
	this.element.style.display = "none";
	if ( this.vCursor )
	{
		this.vCursor.selectStart(pos.x);
	}
	if ( this.hCursor )
	{
		this.hCursor.selectStart(pos.y);
	}
}

DundasAreaSelect.prototype.selectProcess = function( pos )
{
	this.endPoint = pos;
	if ( this.vCursor )
	{
		this.vCursor.selectProcess(pos.x);
	}
	if ( this.hCursor )
	{
		this.hCursor.selectProcess(pos.y);
	}
	this.invalidate();
}
	
DundasAreaSelect.prototype.getChunkRect = function()
{
	var rect = this.areaObj.chunkRect;
	if ( this.areaObj.innerAreaRect )
	{
		var newRect = this.areaObj.innerAreaRect.getIntersect(rect);
		if (!newRect.isEmpty())
		{
			rect = newRect;
		}
	}
	return rect;
};
	
DundasAreaSelect.prototype.getSelectedAreaRect = function()
{
	var rectArea = this.getChunkRect();
	var p1 = this.startPoint;
	var p2 = this.endPoint;

	p2.x = p1.x < p2.x ?  p2.x + 3 :p2.x - 3;
	p2.y = p1.y < p2.y ?  p2.y + 3 :p2.y - 3;
	
	var rect = new DundasRect(
				p1.x < p2.x ? p1.x : p2.x,
				p1.y < p2.y ? p1.y : p2.y,
				p1.x < p2.x ? p2.x - p1.x : p1.x - p2.x,
				p1.y < p2.y ? p2.y - p1.y : p1.y - p2.y
	);
	if ( !this.hCursor && this.vCursor )
	{
		if ( this.vCursor.enabled )
		{
		    rect.Y = rectArea.Y;
		    rect.Height = this.vCursor.cursorLength;
		}
		else
		{
		    rect.Width = 0;
		}
	}
	if ( !this.vCursor && this.hCursor )
	{
		if ( this.hCursor.enabled )
		{
		    rect.X = rectArea.X;
		    rect.Width = this.hCursor.cursorLength;
		}
		else
		{
		    rect.Height = 0;
		}
	}
	return rect;
}
	
DundasAreaSelect.prototype.cancelSelection = function()
{
	this.selectStart(new DundasPoint( -1, -1));		
	this.selectEnd(new DundasPoint( -1, -1));
	this.element.style.display == "none";
};
	
DundasAreaSelect.prototype.invalidate = function()
{
	var rect = this.getSelectedAreaRect();
	this.element.style.left = rect.X + "px";			
	this.element.style.top  = rect.Y + "px";			
	this.element.style.width   = rect.Width + "px";			
	this.element.style.height  = rect.Height + "px";
	if ( this.element.style.display == "none" )
	{
		this.element.style.display = "block";
	}
	
    /*	
	//  old program code before fixing bug #7476
	this.areaObj.updateZoom("sel-start-px", -1, "v");
	this.areaObj.updateZoom("sel-end-px", -1, "v");
	this.areaObj.updateZoom("sel-start-px", -1, "h");
	this.areaObj.updateZoom("sel-end-px", -1, "h");
	if (this.vCursor)
	{		
		this.areaObj.updateZoom("sel-start-px", rect.X, "h");
		this.areaObj.updateZoom("sel-end-px", rect.X + rect.Width, "h");
	}
	if ( this.hCursor )
	{
		this.areaObj.updateZoom("sel-start-px", rect.Y, "v");
		this.areaObj.updateZoom("sel-end-px", rect.Y + rect.Height, "v");
	}
	*/
	
    // fix of bug #7476
	if (this.vCursor)
	{		
	    if (rect.X > 0)
	    {
		    this.areaObj.updateZoom("sel-start-px", rect.X, "h");
		    this.areaObj.updateZoom("sel-end-px", rect.X + rect.Width, "h");
		}    
	}
	
	if ( this.hCursor )
	{
	    if (rect.Y > 0)
	    {
		    this.areaObj.updateZoom("sel-start-px", rect.Y, "v");
		    this.areaObj.updateZoom("sel-end-px", rect.Y + rect.Height, "v");
		}
	}
    // end of fix of bug #7476
}
	
DundasAreaSelect.prototype.getIEDivFix = function ()
{
	return '<img src="' + this.areaObj.emptyImageUrl + '" width="1px" height="1px" style="visibility:hidden" />';
}
				
DundasAreaSelect.prototype.populate = function( areaObj)
{
    
	this.areaObj = areaObj;
	if ( this.hCursor )
	{
		this.hCursor.populate(this)
	}
	if ( this.vCursor )
	{
		this.vCursor.populate(this)
	}

	divElementOuter = document.createElement("DIV"); 
//	divElementOuter.id = "populate1";
	var opacity = "-moz-opacity:0.5;opacity:0.5;";
	var innerData = "";
	var border = "";
	
	if ( DundasChart.msieFlag )
	{
		opacity = "filter:alpha(opacity=50);";
		innerData = this.getIEDivFix();
	}
	if ( DundasChart.operaFlag )
	{
		opacity = "";
		innerData = this.getIEDivFix();
		border = "; border: 1px solid " + this.color + ";";
		this.color = "";
	}
    
	var rect = this.getChunkRect();
	divElementOuter.innerHTML = '<div style="position: absolute; left:'+rect.X+'px; top:'+rect.Y+'px; width:'+rect.Width+'px; height:'+rect.Height+'px; background-color:'+this.color+'; display: none;'+opacity + border +'">'+innerData+'</div>';
	this.element = divElementOuter.childNodes[0];
	


	this.areaObj.area.appendChild( this.element );
	Garbage.discardElement(divElementOuter);
}	

//*********** END DundasAreaSelect Object Definition *******************/

//*********** START DundasAreaCursor Object Definition *******************/

function DundasAreaCursor( vertical, visible, enabled, color, width, selectionEnd, selectionStart  )
{
	this.vertical = typeof(vertical) == "boolean" ? vertical : true;
	this.visible = typeof(visible) == "boolean" ? visible : false;
	this.enabled = typeof(enabled) == "boolean" ? enabled : false;
	this.color   = typeof(color) == "string" ? color : "red";
	this.width   = typeof(width) == "number" ? width : 1;
	this.selectonEnd     = typeof(selectonEnd)   == "number" ? selectonEnd : -1;
	this.selectonStart   = typeof(selectonStart) == "number" ? selectonStart : -1;
	this.areaObj = null;
	this.selectObj = null;
	this.element = null;
	this.height = 0;
	this.offset = new DundasPoint( 0, 0);
} 					  
	
DundasAreaCursor.prototype.updateBackData = function ()
{
	
};
	
DundasAreaCursor.prototype.selectEnd = function( pos )
{
	
}
	
DundasAreaCursor.prototype.selectStart = function( posX )
{
	this.setPosition( posX );
}

DundasAreaCursor.prototype.selectProcess = function( posX )
{
	this.setPosition( posX );
}
	
DundasAreaCursor.prototype.setPosition  = function ( position )
{
	if ( this.element.style.display == "none" )
	{
		this.element.style.display = "block"; 
	}
	var viewRect = this.areaObj.getViewRect();
	if ( this.vertical )
	{
		this.element.style.left = Math.round(position - this.width/2)+ "px";
	}
	else
	{
		this.element.style.top  = Math.round(position - this.width/2)  + "px";	
	}
	if ( position < 0 )
	{
		this.element.style.display = "none"; 
	}
}

DundasAreaCursor.prototype.hide = function()
{
	if ( this.element != null )
	{
		this.areaObj.area.removeChild( this.element );
		this.element = null;
	}
};
	
DundasAreaCursor.prototype.populate = function( selectObj)
{
	this.areaObj = selectObj.areaObj;
	this.selectObj = selectObj;
	var selectRect =  this.selectObj.getChunkRect();
	var rect = new DundasRect(0,0,0,0);
	var divElementOuter = document.createElement("DIV");
//	divElementOuter.id="populate2";
	if ( this.vertical)
	{
		rect.Width  = this.width;
		rect.Height = selectRect.Height; 
		rect.Y	= selectRect.Y;
	}
	else
	{
		rect.Height  = this.width;
		rect.Width   = selectRect.Width; 
		rect.X	= selectRect.X;
	}
	innerData = "";
	if ( DundasChart.msieFlag )
	{
		innerData = this.selectObj.getIEDivFix();
	}
	this.offset = new DundasPoint( rect.X, rect.Y );
	divElementOuter.innerHTML = '<div style="position: absolute; left:'+rect.X+'px; top:'+rect.Y+'px; width:'+rect.Width+'px; height:'+rect.Height+'px; display: none; background-color: '+ this.color+'; ">'+innerData+'</div>';				
	this.element = divElementOuter.childNodes[0];
	this.areaObj.area.appendChild(this.element);

	if ( !this.vertical) this.element.style.height = rect.Height + "px";
	this.height = rect.Height;
	this.cursorLength = !this.vertical ? rect.Width : rect.Height;
	Garbage.discardElement(divElementOuter);
	
}
//*********** End DundasAreaCursor Object Definition *******************/

var __ds_scrollbar_pressed_image = null;

//*********** START DundasScrollbar *******************/
function DundasScrollbar( elementName, chartID, scrollLength, scrollAreaName, resetCommand, initPosition)
{
    this.elementName = elementName;
    this.scrollLength =  scrollLength;
    this.areaElementName = scrollAreaName;
    this.element = document.getElementById(elementName);
    this.element.theObj = this;
    this.isVertical = parseInt( this.element.style.height) > parseInt( this.element.style.width);
    this.tumbElement = document.getElementById(elementName + "Tumb");
    this.tumbElementContainer = document.getElementById(elementName + "TumbCont");
    this.images     = this.element.getElementsByTagName("IMG");    
    this.startPoint = {x:0 , y:0};
    this.maxPixelLength = null;
	this.initPosition = initPosition;
	this.chartID = chartID;
	this.resetCommand = resetCommand;
	this.initialize();    
}
	 				
DundasScrollbar.prototype.getArea = function()
{
	return document.getElementById( this.areaElementName );		
}
        
DundasScrollbar.prototype.scrollPixelLength = function()
{
    if ( !this.maxPixelLength )
    {
        if ( this.isVertical )
        {
            this.maxPixelLength = parseInt( this.tumbElementContainer.style.height ) - parseInt( this.tumbElement.style.height );
        }
        else
        {
            this.maxPixelLength = parseInt( this.tumbElementContainer.style.width ) - parseInt( this.tumbElement.style.width );
        }
    }
    return this.maxPixelLength;
}

DundasScrollbar.prototype.scrollElementLength = function()
{
    if ( !this.maxElementLength )
    {
        if ( this.isVertical )
        {
            this.maxElementLength = parseInt( this.element.style.height );
        }
        else
        {
            this.maxElementLength = parseInt( this.element.style.width );
        }
    }
    return this.maxElementLength;
}
        
DundasScrollbar.prototype.getMultiplier = function()
{
    return (this.scrollLength-this.scrollElementLength()) / this.scrollPixelLength();
}
    
    
DundasScrollbar.prototype.captureBtnImages = function()
{
    for( var i = 0; i < this.images.length; i++)
    {
        var imageId = this.images[i].id;
        this.images[i].theObj = this;
        this.images[i].masterElement  = this.element;
        this.images[i].pressMethod = this.pressImage;
                    
        if ( imageId.lastIndexOf("btn_", imageId.length - 7) != -1 )
        {
            this.images[i].suffix = "btn";
            this.images[i].onmousedown = this.elementDown;
            this.images[i].onclick       = this.elementClick;
            var imageName = this.getImageName(this.images[i], false);
            if ( imageName.indexOf( "ner") != -1 || imageName.indexOf( "far") != -1 )
            {
                this.images[i].scrollOffset = imageName.indexOf( "ner") != -1 ? -4 : +4;
            }
        }
        else
        {
            this.images[i].suffix = "tmb";
        }
    }
}
    
    
DundasScrollbar.prototype.pressImage = function( pressed)
{
    var imageObj = this;
    var imageUrl = __DundasScrBrImgBag[ imageObj.theObj.getImageName(imageObj, pressed) ].src;
    
    // bug #7369
    if (typeof(imageObj.filters) == "unknown" && imageObj.style.filter)
    {
        imageObj.style.filter = "progid:DXImageTransform.Microsoft.AlphaImageLoader(src='"+imageUrl+"', sizingMethod='scale')";
    }    
    ////////////
    else if (imageObj.filters && imageObj.style.filter)
    {
        imageObj.style.filter = "progid:DXImageTransform.Microsoft.AlphaImageLoader(src='"+imageUrl+"', sizingMethod='scale')";
    }
    else
    {
        imageObj.src = imageUrl;
    }
}

DundasScrollbar.prototype.pressTumb = function( pressed)
{
    var images = this.getElementsByTagName("IMG");
    for( var i =0; i < images.length; i++ )
    {
        images[i].pressMethod(pressed);
    }
}

DundasScrollbar.prototype.pressTumbContainer = function( pressed, e)
{
	var tumpContainer = this;
	if ( e && e.target == tumpContainer )
	{
		var mousePos = this.theObj.isVertical ? e.layerY : e.layerX;
		var currentPos  = this.theObj.currentPos(); 
		var sign = mousePos < currentPos ? -1 : 1;
		var scrollOffset = sign * tumpContainer.theObj.scrollElementLength();
		this.theObj.scrollByAbs(scrollOffset);
	}
}
       
DundasScrollbar.prototype.getImageName = function( imgObj, pressed)
{
    var imageId = imgObj.id;
    var imageName = imageId.substring( imageId.lastIndexOf(imgObj.suffix));
    if ( imageName.indexOf("rst") == -1 ) 
    {
        imageName += this.isVertical ? "_v" : "_h";
    }
    return imageName + (pressed ? "_dn" : "");
}
        
DundasScrollbar.prototype.elementDown = function(e)
{
    if ( this.theObj)
    {
        e = DundasChart.fixEvent(e);
        this.theObj.startPoint = {x: e.docX, y: e.docY};
        this.pressMethod( true, e);
        __ds_scrollbar_pressed_image = this;
        DundasChart.attachEvent( this, "mouseout", this.theObj.elementOut);
        DundasChart.attachEvent( this, "mouseover", this.theObj.elementOver);
        DundasChart.attachEvent( document, "mouseup", this.theObj.elementUp);
        DundasChart.attachEvent( document, "mousemove", this.theObj.elementMove);
        if ( this.scrollOffset )
        {
            var scrollOffset = this.scrollOffset;
            var theObj = this.theObj;
            theObj.scrollBy(scrollOffset); 
            theObj.winTimerOutID = window.setTimeout( function() { theObj.repeatScroll(scrollOffset) }, 500 );
        }
    }
    return DundasChart.cancelEvent(e);;
}

DundasScrollbar.prototype.repeatScroll = function( scrollOffset )
{
    var theObj = this;
	this.winTimerID = window.setInterval( function() { theObj.scrollBy(scrollOffset) }, 20 );
}

    
DundasScrollbar.prototype.elementOut = function( e )
{
    e = DundasChart.fixEvent(e);
    var img = __ds_scrollbar_pressed_image;
    if ( e.target == img)
    {
        img.pressMethod( false);
    }    
}

DundasScrollbar.prototype.elementOver = function( e )
{
    e = DundasChart.fixEvent(e);
    var img = __ds_scrollbar_pressed_image;
    if ( img && e.target == img)
    {
        img.pressMethod( true);
    }    
}

DundasScrollbar.prototype.elementUp = function(e)
{
    var img = __ds_scrollbar_pressed_image;
    
    ///// fix of bugs #7994, #8025
    if (!img)
    {
        __ds_scrollbar_pressed_image = null;
        return false;
    }

    if (!img.theObj)
    {
        __ds_scrollbar_pressed_image = null;
        return false;
    }
    ///////////////
    
    if ( img )
    {
        if ( img.theObj.winTimerOutID )
        {
            window.clearTimeout(img.theObj.winTimerOutID);
            img.theObj.winTimerOutID = null;
        }
        if ( img.theObj.winTimerID )
        {
            window.clearInterval(img.theObj.winTimerID);
            img.theObj.winTimerID = null;
        }
        img.pressMethod( false);
        DundasChart.detachEvent( img, "mouseout", img.theObj.elementOut);
        DundasChart.detachEvent( img, "mouseover", img.theObj.elementOut);
        DundasChart.detachEvent( document, "mouseup", img.theObj.elementUp);
        DundasChart.detachEvent( document, "mousemove", img.theObj.elementMove);
        __ds_scrollbar_pressed_image = null;
    }
    return DundasChart.cancelEvent(e);        
}

DundasScrollbar.prototype.elementMove = function(e)
{
    var tump = __ds_scrollbar_pressed_image;

    ///// fix of bugs #7994, #8025
    if (!tump)
    {
        __ds_scrollbar_pressed_image = null;
        return false;
    }
    
    if (!tump.theObj)
    {
        __ds_scrollbar_pressed_image = null;
        return false;
    }
    ///////////////
    
    if ( tump && tump.theObj && tump == tump.theObj.tumbElement)
    {
        var theObj = tump.theObj;
        e = DundasChart.fixEvent(e);
        var pos = {x: e.docX, y: e.docY};
        theObj.scrollBy( theObj.isVertical ? (pos.y - theObj.startPoint.y) : (pos.x - theObj.startPoint.x) ); 
        theObj.startPoint = pos;
    };
    return DundasChart.cancelEvent(e);        
}

DundasScrollbar.prototype.currentPos = function( newPos )
{
    var tumb = this.tumbElement;
    var result = this.isVertical ? parseInt(tumb.style.top) : parseInt(tumb.style.left);
    if ( typeof(newPos) == "number" )
    {
        if ( this.isVertical ) { tumb.style.top = newPos + "px" } else { tumb.style.left = newPos + "px";}            
    }
    return result;
}

DundasScrollbar.prototype.scrollBy = function ( offset )
{
        var maxLength = this.scrollPixelLength();
        var currentPos = this.currentPos();
        if ( currentPos  > 0 || currentPos < maxLength )
        {
            var newPos = Math.max(0, Math.min(currentPos + offset, maxLength));
            this.currentPos(newPos);
            this.elementScroll( newPos);
        }
}

DundasScrollbar.prototype.scrollByAbs = function ( offset )
{
    this.scrollBy(offset / this.getMultiplier())
}

DundasScrollbar.prototype.elementScroll = function ( newPos )
{
    var scrollPos = Math.round(newPos * this.getMultiplier());
    this.setParentPos(scrollPos);
}

DundasScrollbar.prototype.setAbsPos = function( absPos, donotCallback )
{
	this.currentPos( absPos / this.getMultiplier());
	if (!( donotCallback ))
	{
		this.setParentPos( absPos );
	}		
}

DundasScrollbar.prototype.setParentPos = function ( absPos )
{
	var scrollObj = this.getArea().theObj;
	if ( !this.isVertical )
	{
		scrollObj.setScrollLeft( absPos, this.scrollLength, false );
	}
	else
	{
		scrollObj.setScrollTop( absPos, this.scrollLength, false);
	}
} 

DundasScrollbar.prototype.elementClick = function(e)
{
    if ( this.theObj )
    {
        var imageName = this.theObj.getImageName(this, false);
        if (  imageName.indexOf("rst")  != -1 )
        {
            ChartCB.call( e, this.theObj.chartID, this.theObj.resetCommand);            
        }
    }
    return DundasChart.cancelEvent(e);                    
}

DundasScrollbar.prototype.initialize = function()
{
	this.captureBtnImages();
	this.tumbElement.theObj = this;
	this.tumbElement.masterElement  = this.element;
	this.tumbElement.pressMethod = this.pressTumb;
	this.tumbElement.onmousedown = this.elementDown;
	this.tumbElementContainer.theObj = this;
	this.tumbElementContainer.masterElement  = this.element;
	this.tumbElementContainer.pressMethod = this.pressTumbContainer;
	this.tumbElementContainer.onmousedown = this.elementDown;
	this.element.onclick = this.elementClick;
	this.setAbsPos( this.initPosition, true );
	var areaObj = this.getArea().theObj;
	if (null != areaObj)
	{
	    if ( this.isVertical )
	    {
		    areaObj.callbackScrollers.v = this;
	    }
	    else
	    {
		    areaObj.callbackScrollers.h = this;
	    }
	}
}

DundasScrollbar.prototype.cleanUp = function()
{
    this.element.theObj = null;
    this.tumbElement.theObj = null;
    this.tumbElement.masterElement  = null;
    this.tumbElementContainer.theObj = null;
    
    if (this.images)
    {
        try
        {
            for (var i=0; i<this.images.length;i++)
            {
                var image = this.images[i];
                image.theObj = null;
                image.masterElement  = null
                image.pressMethod = null;
                image.onclick = null;
                //Garbage.discardElement(image);
            }
        }
        catch(e)
        {
        }
        finally
        {        
            this.images = null;
        }
    }
    
    DundasChart.detachEvent(this.tumbElement,"mousedown",this.elementDown);
    DundasChart.detachEvent(this.tumbElementContainer,"mousedown",this.elementDown);
    DundasChart.detachEvent(this.element,"click",this.elementClick);
}

//*********** END DundasScrollbar *******************/

// *********** START DundasAjaxCallback Object Variable   *******************/


var DundasAjaxCallback = {
		
	slots : new Array(),

	getSlotsForCheck : function ()
	{
		var arrayCheck = new Array();
		for (var index = 0; index < DundasAjaxCallback.slots.length; index++) {
			var requestObj = DundasAjaxCallback.slots[index];
			if ( requestObj )
			{
				requestObj.index = index;
				arrayCheck[arrayCheck.length] = requestObj;
			}			
		}
		return arrayCheck;
	},
		
	readyStateChange : function ()
	{
		var arrayCheck = DundasAjaxCallback.getSlotsForCheck();
		for (var index = 0; index < arrayCheck.length; index++) {
			var requestObj = arrayCheck[index];
			if ( requestObj )
			{
				var responseStatus = requestObj.getResponseStatus();
				if ( responseStatus == 1 )
				{
					DundasAjaxCallback.slots[requestObj.index] = null;
					var responseText = requestObj.getResponseText();
					if ( responseText.indexOf("e") == 0 )
					{
						if ( requestObj.error)
						    requestObj.error( responseText.substring(1), requestObj.context);
					}
					else if ( responseText.indexOf("s") == 0 )
					{
						if ( requestObj.callback)
						    requestObj.callback( responseText.substring(1), requestObj.context);
					}
					else
					{
						if ( requestObj.callback)
						    requestObj.callback( responseText, requestObj.context);
					}
					requestObj.uninitialize();
				}
				else if ( responseStatus == 2 )
				{
					DundasAjaxCallback.slots[requestObj.index];
					if ( requestObj.error )
						requestObj.error( "Invalid request.", requestObj.context);
					requestObj.uninitialize();
				}
			}
		}
	},
	
	callGet : function ( url, context, callbackFunc, errorFunc, async)
	{
		var request = new DundasAjaxRequest( context, callbackFunc, errorFunc, async);
		DundasAjaxCallback.register( request);
		request.requestGet( url );
	},

	callPost : function ( url, postData, context, callbackFunc, errorFunc, async)
	{
		var request = new DundasAjaxRequest( context, callbackFunc, errorFunc, async);
		DundasAjaxCallback.register( request);
		request.requestPost( url, postData );
	},
	
	register : function ( requestObj )
	{
		var index;
		for (index = 0; index < DundasAjaxCallback.slots.length; index++) {
			if (!DundasAjaxCallback.slots[index]) break;
		}
		DundasAjaxCallback.slots[index] = requestObj;
		requestObj.index = index;
		return index;
	},
	
	encode : function( parameter)
	{
		if (encodeURIComponent) 
		{
			return encodeURIComponent(parameter);
		}
		else 
		{
			return escape(parameter);
		} 
	}
				
}
// *********** END DundasAjaxCallback Object Variable   *******************/	

// *********** START DundasAjaxRequest Object Definition   *******************/	

function DundasAjaxRequest( context, callbackFunc, errorFunc, async)
{
	this.requestObject = this.BuildRequestObject();
	this.responseWaiting = false;
	this.callback = typeof(callbackFunc) != "function" ? null : callbackFunc;
	this.error = typeof(errorFunc) != "function" ? null : errorFunc;
	this.context = context == window.undefined ? null : context;
	this.async = async == window.undefined ? true : async;
}

DundasAjaxRequest.prototype.BuildRequestObject = function()
{
	if(window.XMLHttpRequest) 
	{ // Mozilla, Safari, ...
		return new XMLHttpRequest()
	} 
	else if(window.ActiveXObject) 
	{ // IE
		try
		{
			return new ActiveXObject("Microsoft.XMLHTTP");
		}
		catch(e)
		{
			// do nothing
		}
	}
	return null;
}

DundasAjaxRequest.prototype.requestGet = function(
	serverURL)
{
	this.responseWaiting = false;
	this.requestObject.onreadystatechange = DundasAjaxCallback.readyStateChange;
	this.requestObject.open("GET", serverURL, true);
	this.requestObject.send(null);
}

DundasAjaxRequest.prototype.requestPost = function( 
	serverURL, postData)
{
	this.responseWaiting = false;
	this.requestObject.onreadystatechange = DundasAjaxCallback.readyStateChange;		
	this.requestObject.open("POST", serverURL, true);
    this.requestObject.setRequestHeader("Content-Type", "application/x-www-form-urlencoded");
    this.requestObject.send(postData);
 }


DundasAjaxRequest.prototype.getResponseStatus = function()
{
	if(this.requestObject.readyState == 4)
	{
		if(this.requestObject.status == 200) 
		{
			this.responseWaiting = true;
			return 1; // ready
		} 
		else
		{
			this.requestBusy	= false;
			return 2; // error
		}
	}
	
	return 0; // waiting
}

DundasAjaxRequest.prototype.getResponseText = function()
{
	if(this.responseWaiting == false)
		return "";
	return this.requestObject.responseText;
}

DundasAjaxRequest.prototype.getResponseXml = function()
{
	if(this.responseWaiting == false)
		return "";
	return this.requestObject.responseXML;
}

DundasAjaxRequest.prototype.uninitialize = function()
{

}

// *********** END DundasAjaxRequest Object Definition   *******************/	



var Garbage = 
{
    garbageColl :  new Array(),

    addBin: function ()
    {
        this.garbageColl = new Array();        
    },

    emptyBin : function ()
    {
        if (this.garbageColl)
        {
            if (!window.garbageBin)
            {
                window.garbageBin = document.createElement('DIV');
                window.garbageBin.id = 'IELeakGarbageBin';
                window.garbageBin.style.display = 'none';
                DundasChart.attachEvent(window, "load",Garbage.addBin);
	            DundasChart.attachEvent(window, "unload",Garbage.deleteBin);
	            document.body.appendChild(window.garbageBin);
            }
            
            while (this.garbageColl.length>0)
            {
                var item = this.garbageColl.pop();
                window.garbageBin.appendChild(item);
                //delete item;
            }
            window.garbageBin.innerHTML = "";                        
        }
    },

    deleteBin : function ()
    {
        Garbage.emptyBin();
        DundasChart.detachEvent(window, "onload",Garbage.addBin);
	    DundasChart.detachEvent(window, "onunload",Garbage.deleteBin);
    },


    discardElement : function (element) 
    {    
        if (element == null || !DundasChart.msieFlag)
        {
            return;
        }    
        
        this.garbageColl.push(element);                
        element = null;
    },
    
    cleanUpChartArea : function(charAreaName)
    {
        eval('if (window.'+charAreaName+'Obj) { window.'+charAreaName+'Obj.cleanRes(); }');         
    }
    
    
}

if (typeof(Sys) != 'undefined')
{
    if(Sys.Application )
    {
       Sys.Application.notifyScriptLoaded();
    }
}