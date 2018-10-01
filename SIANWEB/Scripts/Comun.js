
//tirulo de mensajes de alerta
var tituloMensajes = '';

//------------------------------------------------------------------------------------------------------------------------------
// Recortar los espacios del lado izquierdo de una cadena
function LTrim(str) {
    if (str == null) {
        return null;
    }
    for (var i = 0; str.charAt(i) == " "; i++);
    return str.substring(i, str.length);
}
//------------------------------------------------------------------------------------------------------------------------------
// Recortar los espacios del lado derecho de una cadena
function RTrim(str) {
    if (str == null) {
        return null;
    }
    for (var i = str.length - 1; str.charAt(i) == " "; i--);
    return str.substring(0, i + 1);
}
//------------------------------------------------------------------------------------------------------------------------------
// Recortar los espacios de los extremos de una cadena
function Trim(str) {
    return LTrim(RTrim(str));
}
//------------------------------------------------------------------------------------------------------------------------------


////the following code use radconfirm to mimic the blocking of the execution thread.
////The approach has the following limitations:
////1. It works inly for elements that have *click* method, e.g. links and buttons
////2. It cannot be used in if(!confirm) checks
//window.blockConfirm = function (text, mozEvent, oWidth, oHeight, callerObj, oTitle) {
//    var ev = mozEvent ? mozEvent : window.event; //Moz support requires passing the event argument manually
//    //Cancel the event
//    ev.cancelBubble = true;
//    ev.returnValue = false;
//    if (ev.stopPropagation) ev.stopPropagation();
//    if (ev.preventDefault) ev.preventDefault();

//    //Determine who is the caller
//    var callerObj = ev.srcElement ? ev.srcElement : ev.target;

//    //Call the original radconfirm and pass it all necessary parameters
//    if (callerObj) {
//        //Show the confirm, then when it is closing, if returned value was true, automatically call the caller's click method again.
//        var callBackFn = function (arg) {
//            if (arg) {
//                callerObj["onclick"] = "";
//                if (callerObj.click) callerObj.click(); //Works fine every time in IE, but does not work for links in Moz
//                else if (callerObj.tagName == "A") //We assume it is a link button!
//                {
//                    try {
//                        eval(callerObj.href)
//                    }
//                    catch (e) { }
//                }
//            }
//        }

//        radconfirm(text, callBackFn, oWidth, oHeight, callerObj, oTitle);
//    }
//    return false;
//} 