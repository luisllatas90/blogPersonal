function WebForm_CallbackComplete() {
    for (var i = 0; i < __pendingCallbacks.length; i++) {
        var callbackObject = __pendingCallbacks[i];
        if (callbackObject && callbackObject.xmlRequest && (callbackObject.xmlRequest.readyState == 4) && (!callbackObject.executing)) {
            callbackObject.executing = true;
            if ( WebForm_InspectStatus(callbackObject ))
            {
                WebForm_ExecuteCallback(callbackObject);
            }
            if (!callbackObject.async) {
                __synchronousCallBackIndex = -1;
            }
            __pendingCallbacks[i] = null;
            callbackObject.executing = false;
            var callbackFrameID = "__CALLBACKFRAME" + i;
            var xmlRequestFrame = document.getElementById(callbackFrameID);
            if (xmlRequestFrame) {
                xmlRequestFrame.parentNode.removeChild(xmlRequestFrame);
            }
        }
    }
}

function WebForm_InspectStatus(callbackObject )
{
    if ( callbackObject.xmlRequest.status )
    {
        var responseStatus = callbackObject.xmlRequest.status;
        if ( responseStatus == 500 )
        {
            var response = callbackObject.xmlRequest.responseText;
            var responseL = response.toLowerCase();
            var indexOf1 = responseL.indexOf("<title>");
            var indexOf2 = responseL.indexOf("</title>");
            var errText = "An unhandled exception occurred during the execution of the current web request.\n\n      ";
            if ( indexOf2 != -1 && indexOf1 != -1)
            {
                errText = errText + response.substring(indexOf1+7,indexOf2);
            }
            else
            {
                errText = errText + "Unknown error.";
            }
            if ((typeof(callbackObject.errorCallback) != "undefined") && (callbackObject.errorCallback != null)) {
                callbackObject.errorCallback(errText, callbackObject.context);
            }        
            return false;
        }
        else if ( responseStatus >= 300 )
        {
            var errText = "Unsupported server response code [" +responseStatus+"].\n\n  " + callbackObject.xmlRequest.statusText;
            if ((typeof(callbackObject.errorCallback) != "undefined") && (callbackObject.errorCallback != null)) {
                callbackObject.errorCallback(errText, callbackObject.context);
            }        
            return false;
        }
    }
    return true;
}

if(typeof(Sys) != 'undefined')
{
    if(Sys.Application )
    {
        Sys.Application.notifyScriptLoaded();
    }
}
