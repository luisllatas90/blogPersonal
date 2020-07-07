/*
jQWidgets v4.5.4 (2017-June)
Copyright (c) 2011-2017 jQWidgets.
License: http://jqwidgets.com/license/
*/
!function(a){a.jqx.jqxWidget("jqxCheckBox","",{}),a.extend(a.jqx._jqxCheckBox.prototype,{defineInstance:function(){var b={animationShowDelay:300,animationHideDelay:300,width:null,height:null,boxSize:"13px",checked:!1,hasThreeStates:!1,disabled:!1,enableContainerClick:!0,locked:!1,groupName:"",keyboardCheck:!0,enableHover:!0,hasInput:!0,rtl:!1,updated:null,disabledContainer:!1,changeType:null,_canFocus:!0,aria:{"aria-checked":{name:"checked",type:"boolean"},"aria-disabled":{name:"disabled",type:"boolean"}},events:["checked","unchecked","indeterminate","change"]};return this===a.jqx._jqxCheckBox.prototype?b:(a.extend(!0,this,b),b)},createInstance:function(a){var b=this;b._createFromInput("CheckBox"),b.render()},_createFromInput:function(b){var c=this;if("input"==c.element.nodeName.toLowerCase()){c.field=c.element,c.field.className&&(c._className=c.field.className);var d={title:c.field.title};c.field.value&&(d.value=c.field.value),c.field.checked&&(d.checked=!0),c.field.id.length?d.id=c.field.id.replace(/[^\w]/g,"_")+"_"+b:d.id=a.jqx.utilities.createId()+"_"+b;var e=c.element.nextSibling,f=!1;!e||"#text"!=e.nodeName&&"span"!=e.nodeName||(f=!0);var g=0,h=a("<div></div>",d);if(f){h.append(e);var i=a("<span>"+a(e).text()+"</span>");i.appendTo(a(document.body)),g+=i.width(),i.remove()}h[0].style.cssText=c.field.style.cssText,c.width||(c.width=a(c.field).width()+g+10),c.height||(c.height=a(c.field).outerHeight()+10),a(c.field).hide().after(h);var j=c.host.data();if(c.host=h,c.host.data(j),c.element=h[0],c.element.id=c.field.id,c.field.id=d.id,c._className&&(c.host.addClass(c._className),a(c.field).removeClass(c._className)),c.field.tabIndex){var k=c.field.tabIndex;c.field.tabIndex=-1,c.element.tabIndex=k}}},_addInput:function(){if(this.hasInput){this.input&&this.input.remove();var b=this.host.attr("name");this.input=a("<input type='hidden'/>"),this.host.append(this.input),b&&this.input.attr("name",b),this.input.val(this.checked),this.host.attr("role","checkbox"),a.jqx.aria(this)}},render:function(){this.init=!0;var b=this;this.setSize(),this.propertyChangeMap.width=function(a,c,d,e){b.setSize()},this.propertyChangeMap.height=function(a,c,d,e){b.setSize()},this._removeHandlers(),this.width||this.host.css("overflow-x","visible"),this.height||this.host.css("overflow-y","visible"),this.checkbox&&(this.checkbox.remove(),this.checkbox=null),this.checkMark&&(this.checkMark.remove(),this.checkMark=null),this.box&&(this.box.remove(),this.box=null),this.clear&&(this.clear.remove(),this.clear=null),null==this.boxSize&&(this.boxSize=13);var c=parseInt(this.boxSize)+"px",d=Math.floor((parseInt(this.boxSize)-13)/2),e=d;d+="px",e+="px","13px"!=this.boxSize?this.checkbox=a('<div><div style="width: '+c+"; height: "+c+';"><span style="position: relative; left: '+d+"; top: "+e+'; width: 13px; height: 13px;"></span></div></div>'):this.checkbox=a('<div><div style="width: '+c+"; height: "+c+';"><span style="width: '+c+"; height: "+c+';"></span></div></div>'),this.host.prepend(this.checkbox),this.disabledContainer||(this.host.attr("tabIndex")||this.host.attr("tabIndex",0),this.clear=a('<div style="clear: both;"></div>'),this.host.append(this.clear)),this.checkMark=a(this.checkbox[0].firstChild.firstChild),this.box=this.checkbox,this.box.addClass(this.toThemeProperty("jqx-checkbox-default")+" "+this.toThemeProperty("jqx-fill-state-normal")+" "+this.toThemeProperty("jqx-rc-all")),this.disabled&&this.disable(),this.disabledContainer||(this.host.addClass(this.toThemeProperty("jqx-widget")),this.host.addClass(this.toThemeProperty("jqx-checkbox"))),this.locked&&!this.disabledContainer&&this.host.css("cursor","auto");var f=this.element.getAttribute("checked");"checked"!=f&&"true"!=f&&1!=f||(this.checked=!0),this._addInput(),this._render(),this._addHandlers(),this.init=!1,this._centerBox()},_centerBox:function(){if(this.height&&-1==this.height.toString().indexOf("%")&&this.box){var a=parseInt(this.height);this.host.css("line-height",a+"px");var b=a-parseInt(this.boxSize)-1;b/=2,this.box.css("margin-top",parseInt(b))}},refresh:function(a){a||(this.setSize(),this._render())},resize:function(a,b){this.width=a,this.height=b,this.refresh()},setSize:function(){null!=this.width&&-1!=this.width.toString().indexOf("px")?this.host.width(this.width):void 0==this.width||isNaN(this.width)?null!=this.width&&-1!=this.width.toString().indexOf("%")&&(this.element.style.width=this.width):this.host.width(this.width),null!=this.height&&-1!=this.height.toString().indexOf("px")?this.host.height(this.height):void 0==this.height||isNaN(this.height)?null!=this.height&&-1!=this.height.toString().indexOf("%")&&(this.element.style.height=this.height):this.host.height(this.height),this._centerBox()},_addHandlers:function(){var b=this,c=a.jqx.mobile.isTouchDevice(),d="mousedown";c&&(d=a.jqx.mobile.getTouchEventName("touchend")),this.addHandler(this.box,d,function(a){if(!b.disabled&&!b.enableContainerClick&&!b.locked)return b.changeType="mouse",b.toggle(),b.updated&&(a.owner=b,b.updated(a,b.checked,b.oldChecked)),a.preventDefault&&a.preventDefault(),!1}),this.disabledContainer||(this.addHandler(this.host,"keydown",function(a){if(!b.disabled&&!b.locked&&b.keyboardCheck&&32==a.keyCode)return!b._canFocus||(b.changeType="keyboard",b.toggle(),b.updated&&(a.owner=b,b.updated(a,b.checked,b.oldChecked)),a.preventDefault&&a.preventDefault(),!1)}),this.addHandler(this.host,d,function(a){if(!b.disabled&&b.enableContainerClick&&!b.locked)return b.changeType="mouse",b.toggle(),a.preventDefault&&a.preventDefault(),b._canFocus&&b.focus(),!1}),this.addHandler(this.host,"selectstart",function(a){if(!b.disabled&&b.enableContainerClick)return a.preventDefault&&a.preventDefault(),!1}),this.addHandler(this.host,"mouseup",function(a){!b.disabled&&b.enableContainerClick&&a.preventDefault&&a.preventDefault()}),this.addHandler(this.host,"focus",function(a){if(!b.disabled&&!b.locked)return!b._canFocus||(b.enableHover&&b.box.addClass(b.toThemeProperty("jqx-checkbox-hover")),b.box.addClass(b.toThemeProperty("jqx-fill-state-focus")),a.preventDefault&&a.preventDefault(),b.hovered=!0,!1)}),this.addHandler(this.host,"blur",function(a){if(!b.disabled&&!b.locked)return!b._canFocus||(b.enableHover&&b.box.removeClass(b.toThemeProperty("jqx-checkbox-hover")),b.box.removeClass(b.toThemeProperty("jqx-fill-state-focus")),a.preventDefault&&a.preventDefault(),b.hovered=!1,!1)}),this.addHandler(this.host,"mouseenter",function(a){if(b.locked&&b.host.css("cursor","arrow"),b.enableHover&&!b.disabled&&b.enableContainerClick&&!b.locked)return b.box.addClass(b.toThemeProperty("jqx-checkbox-hover")),b.box.addClass(b.toThemeProperty("jqx-fill-state-hover")),a.preventDefault&&a.preventDefault(),b.hovered=!0,!1}),this.addHandler(this.host,"mouseleave",function(a){if(b.enableHover&&!b.disabled&&b.enableContainerClick&&!b.locked)return b.box.removeClass(b.toThemeProperty("jqx-checkbox-hover")),b.box.removeClass(b.toThemeProperty("jqx-fill-state-hover")),a.preventDefault&&a.preventDefault(),b.hovered=!1,!1}),this.addHandler(this.box,"mouseenter",function(){b.locked||b.disabled||b.enableContainerClick||(b.box.addClass(b.toThemeProperty("jqx-checkbox-hover")),b.box.addClass(b.toThemeProperty("jqx-fill-state-hover")))}),this.addHandler(this.box,"mouseleave",function(){b.disabled||b.enableContainerClick||(b.box.removeClass(b.toThemeProperty("jqx-checkbox-hover")),b.box.removeClass(b.toThemeProperty("jqx-fill-state-hover")))}))},focus:function(){try{this.host.focus()}catch(a){}},_removeHandlers:function(){var b=a.jqx.mobile.isTouchDevice(),c="mousedown";b&&(c="touchend"),this.box&&(this.removeHandler(this.box,c),this.removeHandler(this.box,"mouseenter"),this.removeHandler(this.box,"mouseleave")),this.removeHandler(this.host,c),this.removeHandler(this.host,"mouseup"),this.removeHandler(this.host,"selectstart"),this.removeHandler(this.host,"mouseenter"),this.removeHandler(this.host,"mouseleave"),this.removeHandler(this.host,"keydown"),this.removeHandler(this.host,"blur"),this.removeHandler(this.host,"focus")},_render:function(){this.disabled?this.disable():this.enableContainerClick?this.host.css("cursor","pointer"):this.init||this.host.css("cursor","auto"),this.rtl&&(this.box.addClass(this.toThemeProperty("jqx-checkbox-rtl")),this.host.addClass(this.toThemeProperty("jqx-rtl"))),this.updateStates()},_setState:function(a,b){this.checked!=a&&(this.checked=a,this.checked?this.checkMark[0].className=this.toThemeProperty("jqx-checkbox-check-checked"):null==this.checked?this.checkMark[0].className=this.toThemeProperty("jqx-checkbox-check-indeterminate"):this.checkMark[0].className=""),!1!==b&&!0!==b||(this.locked=b)},val:function(a){return 0==arguments.length||null!=a&&"object"==typeof a?this.checked:("string"==typeof a?("true"==a&&this.check(),"false"==a&&this.uncheck(),""==a&&this.indeterminate()):(1==a&&this.check(),0==a&&this.uncheck(),null==a&&this.indeterminate()),this.checked)},check:function(){this.checked=!0;var b=this;if(this.checkMark.removeClass(),a.jqx.browser.msie||0==this.animationShowDelay?this.checkMark.addClass(this.toThemeProperty("jqx-checkbox-check-checked")):(this.checkMark.addClass(this.toThemeProperty("jqx-checkbox-check-checked")),this.checkMark.css("opacity",0),this.checkMark.stop().animate({opacity:1},this.animationShowDelay,function(){})),null!=this.groupName&&this.groupName.length>0){var c=a.find(this.toThemeProperty(".jqx-checkbox",!0));a.each(c,function(){a(this).jqxCheckBox("groupName")==b.groupName&&this!=b.element&&a(this).jqxCheckBox("uncheck")})}this._raiseEvent("0",!0),this._raiseEvent("3",{checked:!0}),void 0!=this.input&&(this.input.val(this.checked),a.jqx.aria(this,"aria-checked",this.checked))},uncheck:function(){this.checked=!1;var b=this;a.jqx.browser.msie||0==this.animationHideDelay?""!=b.checkMark[0].className&&(b.checkMark[0].className=""):(this.checkMark.css("opacity",1),this.checkMark.stop().animate({opacity:0},this.animationHideDelay,function(){""!=b.checkMark[0].className&&(b.checkMark[0].className="")})),this._raiseEvent("1"),this._raiseEvent("3",{checked:!1}),void 0!=this.input&&(this.input.val(this.checked),a.jqx.aria(this,"aria-checked",this.checked))},indeterminate:function(){this.checked=null,this.checkMark.removeClass(),a.jqx.browser.msie||0==this.animationShowDelay?this.checkMark.addClass(this.toThemeProperty("jqx-checkbox-check-indeterminate")):(this.checkMark.addClass(this.toThemeProperty("jqx-checkbox-check-indeterminate")),this.checkMark.css("opacity",0),this.checkMark.stop().animate({opacity:1},this.animationShowDelay,function(){})),this._raiseEvent("2"),this._raiseEvent("3",{checked:null}),void 0!=this.input&&(this.input.val(this.checked),a.jqx.aria(this,"aria-checked","undefined"))},toggle:function(){if(!this.disabled&&!this.locked){if(null!=this.groupName&&this.groupName.length>0)return void(1!=this.checked&&(this.checked=!0,this.updateStates()));this.oldChecked=this.checked,1==this.checked?this.checked=!!this.hasThreeStates&&null:this.checked=null!=this.checked,this.updateStates(),void 0!=this.input&&this.input.val(this.checked)}},updateStates:function(){this.checked?this.check():0==this.checked?this.uncheck():null==this.checked&&this.indeterminate()},disable:function(){this.disabled=!0,1==this.checked?this.checkMark.addClass(this.toThemeProperty("jqx-checkbox-check-disabled")):null==this.checked&&this.checkMark.addClass(this.toThemeProperty("jqx-checkbox-check-indeterminate-disabled")),this.box.addClass(this.toThemeProperty("jqx-checkbox-disabled-box")),this.host.addClass(this.toThemeProperty("jqx-checkbox-disabled")),this.host.addClass(this.toThemeProperty("jqx-fill-state-disabled")),this.box.addClass(this.toThemeProperty("jqx-checkbox-disabled")),a.jqx.aria(this,"aria-disabled",this.disabled)},enable:function(){1==this.checked?this.checkMark.removeClass(this.toThemeProperty("jqx-checkbox-check-disabled")):null==this.checked&&this.checkMark.removeClass(this.toThemeProperty("jqx-checkbox-check-indeterminate-disabled")),this.box.removeClass(this.toThemeProperty("jqx-checkbox-disabled-box")),this.host.removeClass(this.toThemeProperty("jqx-checkbox-disabled")),this.host.removeClass(this.toThemeProperty("jqx-fill-state-disabled")),this.box.removeClass(this.toThemeProperty("jqx-checkbox-disabled")),this.disabled=!1,a.jqx.aria(this,"aria-disabled",this.disabled)},destroy:function(){this.host.remove()},_raiseEvent:function(b,c){if(!this.init){var d=this.events[b],e=new a.Event(d);e.owner=this,c||(c={}),c.type=this.changeType,this.changeType=null,e.args=c;try{var f=this.host.trigger(e)}catch(a){}return f}},propertiesChangedHandler:function(a,b,c){c.width&&c.height&&2==Object.keys(c).length&&a.setSize()},propertyChangedHandler:function(b,c,d,e){if(void 0!=this.isInitialized&&0!=this.isInitialized&&!(b.batchUpdate&&b.batchUpdate.width&&b.batchUpdate.height&&2==Object.keys(b.batchUpdate).length)){if("enableContainerClick"!=c||b.disabled||b.locked||(e?b.host.css("cursor","pointer"):b.host.css("cursor","auto")),"rtl"==c&&(e?(b.box.addClass(b.toThemeProperty("jqx-checkbox-rtl")),b.host.addClass(b.toThemeProperty("jqx-rtl"))):(b.box.removeClass(b.toThemeProperty("jqx-checkbox-rtl")),b.host.removeClass(b.toThemeProperty("jqx-rtl")))),"boxSize"==c&&b.render(),"theme"==c&&a.jqx.utilities.setTheme(d,e,b.host),"checked"==c&&e!=d)switch(e){case!0:b.check();break;case!1:b.uncheck();break;case null:b.indeterminate()}"disabled"==c&&e!=d&&(e?b.disable():b.enable())}}})}(jqxBaseFramework);

