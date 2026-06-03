//Variables Globales.
var gsSepART = String.fromCharCode(127);
var gDvW = '_dvWait'
var gvhi = "<FONT class=cssFRES>";
var gSpl = '(*_DIV_*)';
var gsTtpx = "dhtmltooltip";
var gvhe = "</FONT>";
var gOvF = 'vdv01rl';
var gOvM = 'vdvovrl';
var gTop = 70;
var gTop1 = 61;
var vrwn = 'cssDgTrNormal';
var vrwh = 'cssDgTrHilite';
var vrws = 'cssDgTrSel';
var gsMdl = 'x_OVR';
var vTBN = 'xxPTB';
var xTPE = '';
var gsGrDef = 'xDG';
var gsvTip01 = '';
var gsMsg = '';
var gsRet = '';
//var ae_cb = null;
var mth = new Array(' ', 'january', 'february', 'march', 'april', 'may', 'june', 'july', 'august', 'september', 'october', 'november', 'december');
var day = new Array(31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31);
var numb = '0123456789';
var gFimg = "/sysobj/img/";
var lT = "";
var itm = 0;
var cn = "ctrl_";
var dobj = "";
//var nav4 = window.Event ? true : false;
var isIE = document.all;
var nn6 = document.getElementById && !document.all;
var isResize = false;
var isdrag = false;
var qx = 0;
var qy = 0;
var tx = 0;
var ty = 0;
var x = 0;
var y = 0;



    function KeyEnter(e) {
                if (e.keyCode == 13) {
        __doPostBack('KeyEnterPostBack', '')
        }

    }


function validateDecimal(valor) {
    if (!isNaN(valor.value)) {

        return true;
    } else {
        alert("El valor " + valor.value + " no es un numero ");
        return false;
    }
}

function fnUpper(valor) {
    valor.value = valor.value.toUpperCase();
}

function filterFloat(evt, input) {
    // Backspace = 8, Enter = 13, ‘0' = 48, ‘9' = 57, ‘.’ = 46, ‘-’ = 43
    var key = window.Event ? evt.which : evt.keyCode;
    var chark = String.fromCharCode(key);
    var tempValue = input.value + chark;
    
    if (key >= 48 && key <= 57) {
        if (filter(tempValue) === false) {
            return false;
        } else {
            return true;
        }
    } else {
        if (key == 8 || key == 13 || key == 0) {
            return true;
        } else if (key == 46) {
                return false;
        } else {
            return false;
        }
    }
}

function filterFloatPto(evt, input) {
    // Backspace = 8, Enter = 13, ‘0' = 48, ‘9' = 57, ‘.’ = 46, ‘-’ = 43
    var key = window.Event ? evt.which : evt.keyCode;
    var chark = String.fromCharCode(key);
    var tempValue = input.value + chark;

    if (key >= 48 && key <= 57) {
        if (filter(tempValue) === false) {
            return false;
        } else {
            return true;
        }
    } else {
        if (key == 8 || key == 13 || key == 0) {
            return true;
        } else if (key == 46) {
            if (filter(tempValue) === false) {
                return false;
            } else {
                return true;
            }
        } else {
            return false;
        }
    }
}

function filterEntero(evt, input) {
    // Backspace = 8, Enter = 13, ‘0' = 48, ‘9' = 57, ‘.’ = 46, ‘-’ = 43
    var key = window.Event ? evt.which : evt.keyCode;
    var chark = String.fromCharCode(key);
    var tempValue = input.value + chark;
    
    if (key >= 48 && key <= 57) {
        if (filter(tempValue) === false) {
            return false;
        }
        else {
            return true;
        }
    }
    else {
        return false;
    }

}

function filterDV(evt, input) {
    // Backspace = 8, Enter = 13, ‘0' = 48, ‘9' = 57, ‘.’ = 46, ‘-’ = 43
    var key = window.Event ? evt.which : evt.keyCode;
    var chark = String.fromCharCode(key);
    var tempValue = input.value + chark;
   
    if (key >= 48 && key <= 57)
    {
        if (filter(tempValue) === false)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
    else
    {
        if (key == 75 || key == 107)
        {
            
            return true;
        }
        else
        {
            return false;
        }
    }
}

function filter(__val__) {
    var preg = /^([0-9]+\.?[0-9]{0,4})$/;
    if (preg.test(__val__) === true) {
        return true;
    } else {
        return false;
    }

}


window.addEventListener("keypress", function (event) {
    if (event.keyCode == 13) {
        event.preventDefault();
    }
}, false);

function validarEmail(valor) {
    if (valor.value != '') {
        if (/^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3,4})+$/.test(valor)) {
            return true;
        } else {
            alert("La dirección de email es incorrecta.");
            valor.value = '';
        }
    }
}

function LimpiarTexto(obj) {
    document.getElementById(obj).value = "";
}

function IsInteger(y) {
    var valor = y.value;
    if (isNaN(valor)) {
        alert("Ups... " + valor + " no es un número.");
        y.value = '';
    } else {
        if (valor % 1 == 0) {
            return true;
        } else {
            alert("Es un numero decimal");
            y.value = '';
        }
    }
}

function WinPop(vUrl, vWn) {
    var vP = '';
    var vX = 100;
    var vScroll = '';
    vP += 'width=' + (parseInt(screen.width, 10) - vX) + 'px';
    vP += ', height=' + (parseInt(screen.height, 10) - vX) + 'px';
    vP += ', top=40, left=40';

    var myWindow = window.open(vUrl, vWn, vP);
    myWindow.focus();
    return false;
}

function Confirm(mensaje) {
    var confirm_value = document.createElement("INPUT");
    confirm_value.type = "hidden";
    confirm_value.name = "confirm_value";
    if (confirm(mensaje)) {
        confirm_value.value = "Yes";
    } else {
        confirm_value.value = "No";
    }
    document.forms[0].appendChild(confirm_value);
}

function getImgDirectory(source) {
    return source.substring(0, source.lastIndexOf('/') + 1);
}
function fnRnd() {
    var rand_no = Math.random();
    rand_no = rand_no * 10000;
    rand_no = Math.ceil(rand_no);
    return rand_no;
}
function ge$(obj) {
    return document.getElementById(obj);
}
function fnSelSH(sAccion, sExcept) {
    var vsel = document.getElementsByTagName('SELECT');
    for (var i = 0; i < vsel.length; i++) {
        if (vsel[i].className != 'nohide') {
            vsel[i].style.visibility = sAccion ? '' : 'hidden';
        }
    }
}
function fnMaxZ() {
    return 10000; //lZ;
}
// Trim leading and trailing blanks from an input field
function alltrim(obj) {
    var startpos = 0, endpos = obj.value.length - 1;
    // find start position of string 
    while (startpos <= obj.value.length && obj.value.substring(startpos, startpos + 1) == " ") {
        startpos++;
    }
    // null string 
    if (endpos == -1) {
        endpos = 0;
    }
    // find end position of string 
    while (endpos >= 0 && obj.value.substring(endpos, endpos + 1) == ' ') {
        endpos--;
    }
    // replace value with trimmed string
    obj.value = obj.value.substring(startpos, endpos + 1);
    return obj;
}
// nochars: check for the existence of a non int character within a string 
function nochars(strval) {
    var retval = true;
    for (var i = 0; i < strval.length; i++) {
        if (strval.substring(i, i + 1) < "0" || strval.substring(i, i + 1) > "9") {
            retval = false;
            break;
        }
    }
    return retval;
}
//Funciones Generales.
function stripBlanks(fld) {
    var result = "";
    for (var i = 0; i < fld.length; i++) {
        if (fld.charAt(i) != " " || c > 0) {
            result += fld.charAt(i);
            if (fld.charAt(i) != " ")
            { c = result.length; }
        }
    }
    return result.substr(0, c);
}
function isValid(parm, val) {
    if (parm === '') { return true; }
    for (var i = 0; i < parm.length; i++) {
        if (val.indexOf(parm.charAt(i), 0) == -1) { return false; }
    }
    return true;
}
function isNum(parm) {
    return isValid(parm, numb);
}
function validaFecha(fld, fmt, rng) {
    var dd, mm, yy;
    var today = new Date();
    var t = new Date();
    fld = stripBlanks(fld);
    if (fld === '') { return false; }
    fld = fld.replace(/-/gi, "\/");
    var d1 = fld.split('\/');
    if (d1.length != 3) { d1 = fld.split(' '); }
    if (d1.length != 3) { return false; }
    if (fmt == 'u' || fmt == 'U') {
        dd = d1[1]; mm = d1[0]; yy = d1[2];
    }
    else if (fmt == 'j' || fmt == 'J') {
        dd = d1[2]; mm = d1[1]; yy = d1[0];
    }
    else if (fmt == 'w' || fmt == 'W') {
        dd = d1[0]; mm = d1[1]; yy = d1[2];
    }
    else {
        return false;
    }
    var n = dd.lastIndexOf('st');
    if (n > -1) { dd = dd.substr(0, n); }
    n = dd.lastIndexOf('nd');
    if (n > -1) { dd = dd.substr(0, n); }
    n = dd.lastIndexOf('rd');
    if (n > -1) { dd = dd.substr(0, n); }
    n = dd.lastIndexOf('th');
    if (n > -1) { dd = dd.substr(0, n); }
    n = dd.lastIndexOf(',');
    if (n > -1) { dd = dd.substr(0, n); }
    n = mm.lastIndexOf(',');
    if (n > -1) { mm = mm.substr(0, n); }
    if (!isNum(dd)) { return false; }
    if (!isNum(yy)) { return false; }
    if (!isNum(mm)) {
        var nn = mm.toLowerCase();
        for (var i = 1; i < 13; i++) {
            if (nn == mth[i] || nn == mth[i].substr(0, 3)) {
                mm = i; i = 13;
            }
        }
    }
    if (!isNum(mm)) { return false; }
    dd = parseFloat(dd);
    mm = parseFloat(mm);
    yy = parseFloat(yy);
    if (yy < 100) { yy += 2000; }
    if (yy < 1582 || yy > 4881) { return false; }
    if (mm == 2 && (yy % 400 === 0 || (yy % 4 === 0 && yy % 100 !== 0))) { day[mm - 1]++; }
    if (mm < 1 || mm > 12) { return false; }
    if (dd < 1 || dd > day[mm - 1]) { return false; }
    t.setDate(dd); t.setMonth(mm - 1); t.setFullYear(yy);
    if (rng == 'p' || rng == 'P') {
        if (t > today) { return false; }
    }
    else if (rng == 'f' || rng == 'F') {
        if (t < today) { return false; }
    }
    else if (rng != 'a' && rng != 'A') { return false; }
    return true;
}
// istime - validate time format (hh:mm:ss)
// note: requires function alltrim() in file alltrim.cfm
// it also requires nochars() in file isdate.cfm
function isTime(obj, retval) {
    if (retval) {
        retval = false;
        var hours, minutes, seconds;
        obj = alltrim(obj);
        if (obj.value.length == 5) {
            // seconds are optional 
            obj.value += ":00";
        }
        if (obj.value !== '') {
            // check formatting of string 
            if (obj.value.length != 8 || obj.value.substring(2, 3) != ":" || obj.value.substring(5, 6) != ":") {
            }
            else {
                // Hours must be between 0 and 24 
                hours = obj.value.substring(0, 2);
                if (nochars(hours) && parseInt(hours, 10) >= 0 && parseInt(hours, 10) < 24) {
                    hours = parseInt(hours, 10);
                    minutes = obj.value.substring(3, 5);
                    if (nochars(minutes)) {
                        minutes = parseInt(minutes, 10);
                        if (minutes >= 0 && minutes <= 59) {
                            seconds = obj.value.substring(6, 8);
                            // seconds between 0 and 59 
                            if (nochars(seconds) && parseInt(seconds, 10) >= 0 && parseInt(seconds, 10) <= 59) {
                                retval = true;
                            }
                        }
                    }
                }
            }
            if (!retval) {
                //return alert("Hora inválida. Use formato hh:mm:ss");
            }
        }
    }
    return retval;
}
function fnGetDiv(vid, vcs, vlf, vtp, vwd, vhe, vzi, vopr, vost) {
    var ret = "" +
  "<div " +
    "id    ='" + vid + "'  " +
    "class ='" + vcs + "'  " +
    vopr +
    "style ='" +
    "left:   " + vlf + "px;" +
    "top:    " + vtp + "px;" +
    "width:  " + vwd + "px;" +
    vost +
    "height: " + vhe + "px;" +
    "z-index:" + vzi + ";" +
    "position:absolute;" + "'" +
  "></div>";
    return ret;
}
// retorna nombre de la máquina.
function fnMachine() {
    var nameEQ = "xIP";
    var theCookie = "" + document.cookie;
    var ind = theCookie.indexOf(nameEQ);
    if (ind == -1 || nameEQ === "") { return ""; }
    var ind1 = theCookie.indexOf(';', ind);
    if (ind1 == -1) { ind1 = theCookie.length; }
    return unescape(theCookie.substring(ind + nameEQ.length + 1, ind1));
}
function fnSoloNum(evt) {
    // NOTE: Backspace = 8, Enter = 13, '0' = 48, '9' = 57 , 44 = coma , 45 = guion
    var key = !isIE ? evt.which : evt.keyCode;
    var xkey = !isIE ? 1 : 2;
    return (key <= 13 || (key >= 48 && key <= 57) || key == 44 || key == 45);
}
function fnSoloFecha(evt) {
    // NOTE: Backspace = 8, Enter = 13, '0' = 48, '9' = 57 , 44 = coma , 45 = guion
    var key = !isIE ? evt.which : evt.keyCode;
    return (key <= 13 || (key >= 48 && key <= 57) || key == 45);
}
function fnSoloHora(evt) {
    // NOTE: Backspace = 8, Enter = 13, '0' = 48, '9' = 57 , 58 = dos puntos
    var key = !isIE ? evt.which : evt.keyCode;
    return (key <= 13 || (key >= 48 && key <= 57) || key == 58);
}
function fnCloseWM(sWin) {
    fnShowHide(sWin, true, true, false);
}
function fnShowHide(sD, sH, sC, sOvr) {
    var obj = ge$(sD);
    if (obj) {
        var sVis = '';
        var sDis = '';
        var sDO = sD + gsMdl;
        if (sH) {
            sVis = 'hidden';
            sDis = 'none';
            var obj1 = ge$(sDO);
            if (obj1) {
                document.body.removeChild(obj1);
                fnSelSH(true);
            }
        }
        obj.style.visibility = sVis;
        obj.style.display = sDis;
        if (sC && sVis == '') {
            var vw = parseInt(obj.style.width, 10);
            var vh = parseInt(obj.style.height, 10);
            var vsl = parseInt(document.documentElement.scrollLeft, 10);
            var vst = parseInt(document.documentElement.scrollTop, 10);
            var vl = parseInt((document.documentElement.clientWidth - vw) / 2, 10) + vsl;
            var vt = parseInt((document.documentElement.clientHeight - vh) / 2, 10) + vst;
            obj.style.position = 'absolute';
            obj.style.top = vt + 'px';
            obj.style.left = vl + 'px';
        }
        if (sOvr && sVis == '') {
            fnSelSH(false);
            var vZ = fnMaxZ();
            var xObN = sD + gsMdl;
            var w = "<div id='" + xObN + "'" + " class='cssOvrl'></div>";
            var xOb = document.createElement(w);
            document.body.appendChild(xOb);
            xOb.style.zIndex = vZ + 1;
            obj.style.zIndex = vZ + 2;
        }
    }
}
function trim(cadena) {
    for (var i = 0; i < cadena.length;) {
        if (cadena.charAt(i) == " ")
        { cadena = cadena.substring(i + 1, cadena.length); }
        else
        { break; }
    }
    for (i = cadena.length - 1; i >= 0; i = cadena.length - 1) {
        if (cadena.charAt(i) == " ")
        { cadena = cadena.substring(0, i); }
        else
        { break; }
    }
    return cadena;
}
/* 
Funciones NWA Certificadas 

Importante: No modifique estas funciones.


*/
function fnPop(vWn, vUrl, vSC, vTt) {
    var vP = '';
    var vX = 100;
    var vScroll = '';
    if (vSC) { vScroll = ',scrollbars=yes'; }
    vP += 'width=' + (parseInt(screen.width, 10) - vX) + 'px';
    vP += ', height=' + (parseInt(screen.height, 10) - vX) + 'px';
    vP += ', top=40, left=40';
    vP += vScroll;
    var vW = window.open(vUrl, vWn, vP);
    //if(vTt!=''){vW.document.title=vTt;}
    //if(window.focus){vW.focus()}
    vW.focus();
    return false;
}
function fnSwapTabButton(obj, obt) {
    var obn = '';
    var obx = '';
    obj = ge$(obj);
    if (!obj) { return; }
    var obtx = ge$(obt);
    if (obtx) { obtx.value = obj.id; }
    var objs = document.getElementsByTagName('input');
    if (objs) {
        for (var i = 0; i < objs.length; i++) {
            if (objs[i].className == 'cssBtnTbbOn') {
                objs[i].className = 'cssBtnTbbOff';
                obn = objs[i].id + 'D';
                obx = ge$(obn);
                if (obx) { obx.style.visibility = 'hidden'; obx.style.display = 'none'; }
            }
        }
        obj.className = 'cssBtnTbbOn';
        obn = obj.id + 'D';
        obx = ge$(obn);
        if (obx) {
            obx.style.visibility = 'visible';
            obx.style.display = '';
            obx.style.left = '0px';
            obx.style.top = '59px';
            obx.style.width = '100%';
            //obx.style.height='768px';	          
        }
    }
}
function doHighlight1(bodyText, searchTerm) {
    var vhi = gvhi;
    var vhe = gvhe;
    var newText = "";
    var i = -1;
    var lcSearchTerm = searchTerm.toLowerCase();
    var lcBodyText = bodyText.toLowerCase();
    while (bodyText.length > 0) {
        i = lcBodyText.indexOf(lcSearchTerm, i + 1);
        if (i < 0) {
            newText += bodyText;
            bodyText = "";
        } else {
            // skip anything inside an HTML tag
            if (bodyText.lastIndexOf(">", i) >= bodyText.lastIndexOf("<", i)) {
                // skip anything inside a <script> block
                if (lcBodyText.lastIndexOf("/script>", i) >= lcBodyText.lastIndexOf("<script", i)) {
                    newText += bodyText.substring(0, i) + vhi + bodyText.substr(i, searchTerm.length) + vhe;
                    bodyText = bodyText.substr(i + searchTerm.length);
                    lcBodyText = bodyText.toLowerCase();
                    i = -1;
                }
            }
        }
    }
    return newText;
}
function doHighlight(vText, searchTerm) {
    var newText = vText;
    var aText = searchTerm.split(" ");
    for (var vi1 = 0; vi1 < aText.length; vi1++) {
        var lcSearchTerm = aText[vi1];
        newText = doHighlight1(newText, lcSearchTerm)
    }
    return newText;
}
function fnDgEv(param, veodb, veoct, vocc, vipf, vslx, vkey, vdel) {
    var tab = ge$(param);
    if (!tab) { return; }
    var rows = tab.rows;
    var vIni = 1;
    if (vipf) { vIni = 0; }
    if (vdel) { vdel = ge$(vdel); }
    for (var i = vIni; i < rows.length; i++) {
        if (vslx && vkey) {
            var vTx1 = rows[i].cells[2].innerHTML;
            var vTx2 = doHighlight(vTx1, vkey);
            rows[i].cells[2].innerHTML = vTx2;
        }
        if (vdel) {
            rows[i].onkeydown = function (e) {
                var evento = window.event || e;
                if (evento.keyCode == 46) { vdel.click(); }
            };
        }

        rows[i].onmouseover = function () {
            if (this.isSel) { this.className = this.className.replace(vrwh, ''); return; }
            this.className = vrwh + ' ' + this.className;
        };
        rows[i].onmouseout = function () {
            this.className = this.className.replace(vrwh, '');
        };
        rows[i].ondblclick = function () {
            if (veodb === undefined || veodb === '') { return; }
            var oba = ge$(veodb);
            if (oba) {
                this.isSel = true;
                oba.click();
            }
        };
        rows[i].oncontextmenu = function () {
            if (veoct === undefined || veoct === '') { return; }
            if (this.isSel === undefined) { this.isSel = false; }
            if (!this.isSel) {
                this.isSel = true;
                this.className = this.className.replace(vrwh, '');
                this.className = vrws + ' ' + this.className;
            }
            eval(veoct);
            return false;
        };
        rows[i].onclick =
    function () {
        if (this.isSel === undefined) { this.isSel = false; }
        this.isSel = !this.isSel;
        if (this.isSel) {
            this.className = this.className.replace(vrwh, '');
            this.className = vrws + ' ' + this.className;
        }
        else {
            this.className = this.className.replace(vrws, '');
        }
    };
    }

    if (vocc) {
        var arr = new Array();
        for (var k = 0; k < rows[0].cells.length; k++) {
            //alert('Width[' + rows[0].cells[k].style.width + ']. Columna[' + k + ']');
            if (parseInt(rows[0].cells[k].style.width, 10) === 0) { arr.push(k); }
        }
        for (var ix = 0; ix < rows.length; ix++) {
            for (var j = 0; j < arr.length; j++) {
                rows[ix].cells[arr[j]].style.visibility = 'hidden';
                rows[ix].cells[arr[j]].style.display = 'none';
            }
        }
    }
}

function fnEnable(vobx, venab) {
    var ob1 = ge$(vobx);
    if (!ob1) {
        return;
    }
    ob1.disabled = !venab;
}
function fnSyncTxt(vPg, vIf, vDv, vTx, vPr, vW, vH, vT) {
    if (!vPg || !vIf || !vDv || !vTx || !vPr) { return; }
    var vIfr = ge$(vIf); if (!vIfr) { return; }
    var vDiv = ge$(vDv); if (!vDiv) { return; }
    vDiv.style.visibility = '';
    vDiv.style.display = '';
    vDiv.style.zIndex = '99999';
    vDiv.style.position = 'absolute';
    //alert('vW: ' + vW + '\nvH: ' + vH);
    vDiv.style.width = vW + 'px';
    vDiv.style.height = vH + 'px';
    vDiv.style.left = vTx.style.left;
    vDiv.style.top = gTop + parseInt(vTx.style.top, 10) + parseInt(vTx.style.height, 10) + 'px';
    var vTxt = vTx.value;
    if (vT) { vTxt = ''; }
    var vLnk = vPg + vPr + vTxt;

    vIfr.style.width = vDiv.style.width;
    vIfr.style.height = vDiv.style.height;

    vIfr.src = '';
    vIfr.src = vLnk;
}
function fnGetItemDG(obDG, vIdx, vun) {
    if (!vIdx) { vIdx = 0; }
    var aux = '';
    var tb = ge$(obDG);
    if (!tb) { return ''; }
    for (var i = 0; i < tb.rows.length; i++) {
        if (tb.rows[i].isSel) {
            if (vun) {
                return tb.rows[i].cells[vIdx].innerHTML;
            }
            aux += tb.rows[i].cells[vIdx].innerHTML + ',';
        }
    }
    if (!vun && aux && aux.charAt(aux.length - 1) == ',') { aux = aux.substr(0, aux.length - 1); }
    //if(!vun&&aux){var vnx=aux.lastIndexOf(',');if(vnx==aux.length - 1){aux=aux.subst(0,aux.length - 1}}
    //alert("Indice: " + vnx + '\n. Largo: ' + aux.length)
    return aux;
}
function fnHideOvr() {
    var vF = ge$(gOvF); if (vF) { document.body.removeChild(vF); }
    var vM = ge$(gOvM); if (vM) { document.body.removeChild(vM); }
    fnSelSH(true);
}
function fnDgDelItm(obDG, obTxt, vErr, vMsg, vQ) {
    if (obDG != '') {
        var vRet = fnGetItemDG(obDG, 0);
        if (!vErr) { vErr = 'Debe seleccionar item de la lista.'; }
        if (vRet === '') { alert(vErr); return false; }
    }
    var vV = true;
    if (vQ) {
        if (!vMsg) { vMsg = '¿Seguro de Eliminar?'; }
        vV = confirm(vMsg);
    }
    if (vV) { var v001 = 'Espere...'; if (vMsg != '') { v001 = vMsg }; fnMsgWait(v001); if (obTxt != '') { ge$(obTxt).value = vRet; } }
    return vV;
}

function fnMsgWait(asMsg, aObHide) {
    var vw = 240;
    var vh = 120;
    var vsl = parseInt(document.documentElement.scrollLeft, 10);
    var vst = parseInt(document.documentElement.scrollTop, 10);
    vl = parseInt((document.documentElement.clientWidth - vw) / 2, 10) + vsl;
    vt = parseInt((document.documentElement.clientHeight - vh) / 2, 10) + vst;
    var lZ = fnMaxZ();
    var lZ1 = lZ - 1;
    fnSelSH(false);
    if (aObHide) { aObHide.style.visibility = 'hidden'; aObHide.style.display = 'none'; }
    var xObN = gOvF;
    w = "<div id='" + xObN + "'" + " style='z-index:" + lZ1 + ";' " + " class='cssOvrl'></div>";
    var xOb = document.createElement(w);
    xOb.style.display = "";
    document.body.appendChild(xOb);
    // Crea DIV PRINCIPAL.  
    w = fnGetDiv(gOvM, "xWin_F", vl, vt, vw, vh, lZ, "", "");
    d1 = document.createElement(w);
    d1.style.background = 'white';
    var sWt = "<img src='" + gFimg + "/wait.gif'>";
    var sMs = '<p class="xWinSpan">' + asMsg + '</p><p class="xWinSpan">' + sWt + '</p>';
    d1.innerHTML = sMs;
    if (asMsg.substring(0, gSpl.length) == gSpl) {
        var vx01 = asMsg.substring(gSpl.length, 100);
        var xOb001 = ge$(vx01);
        if (xOb001) {
            //xOb001.style.visibility='';
            //xOb001.style.display='';
            //d1.appendChild(xOb001);
            d1.innerHTML = xOb001.innerHTML;
            d1.style.width = xOb001.style.width;
            d1.style.height = xOb001.style.height;
            fnCenter(d1.id);
        }
    }
    document.body.appendChild(d1);
    fnCenter(d1.id);
}
function fnRollDv(vD, vT, vG) {
    var vD = ge$(vD);
    if (!vD) { return; }
    var vV = vD.style.visibility;
    var vH = (vV != 'hidden');
    var vT = ge$(vT);
    if (vT) { vT.value = vH; }
    fnShowHide(vD.id, vH);
    fnStack(vD.id, vG, vT.id);
}
function fnStack(vD, vG, vT) {
    vD = ge$(vD);
    vG = ge$(vG);
    vT = ge$(vT);
    if (vD && vG && vT) {
        var vt = parseInt(vD.style.top) + 10;
        if (vT.value == 'false') { vt += parseInt(vD.style.height) }
        vG.style.top = vt + 'px';
    }
}
function fnSelItm(param, sel) {
    var tb = ge$(param);
    if (!tb) { return; }
    var vCss1 = vrws;
    for (var i = 1; i < tb.rows.length; i++) {
        tb.rows[i].isSel = sel;
        tb.rows[i].className = tb.rows[i].className.replace(vCss1, '');
        if (sel) { tb.rows[i].className = vCss1 + ' ' + tb.rows[i].className; }
    }
}
function fnClickSel(vObj, vIfr) {
    var obi = ge$(vIfr);
    if (obi) { var ff = window.frames[vIfr]; if (ff) { ff.fnSlx(); } }
}
function fnSetTxtLbl(vT, vL) {
    var vT = ge$(vT);
    if (vT) { vT.value = vL; }
}
function fnWHlp(vV) {
    var vA = fnSetFlv();
    var vPly = "/sysobj/hlp/flowplayer-3.1.3.swf"
    var vW = 670; var vH = 510; var vT = 0; var vL = 0; var vX = 25;
    var vY = 0; var vH1 = vH - vX + 4; var vW1 = vW - vY;
    var vId1 = 'vxHLP01'; var vId2 = vId1 + '_01'; //'vxHLP02';
    var vO = ge$(vId1);
    if (vO) { document.body.removeChild(vO); }
    var w = "<div id='" + vId1 + "' class='cssWinHlp' style='z-index:10000;height:" + vH + "px;width:" + vW + "px;'></div>";
    //position:absolute;left:" + vT + "px;top:" + vT + "px;
    var xOb = document.createElement(w);
    var wT = "<input type='button' value='Cerrar' class='bCl' onclick='fnShowHide(this.parentNode.id,true);'>";
    var xT = document.createElement(wT);
    xOb.appendChild(xT);
    var w1 = "<div id='" + vId2 + "' style='height:" + vH1 + "px;position:absolute;top:" + vX + "px;width:" + vW1 + "px'></div>";
    var xOb1 = document.createElement(w1);
    xOb.appendChild(xOb1);
    document.body.appendChild(xOb);
    if (xOb) {
        $f(xOb1.id, vPly, vV);
    }
    fnShowHide(vId1, false, true, true);
}
function fnSetFlv() {
    var vI = '___fpl';
    var vF = ge$(vI);
    if (vF) { return 1; }
    var vH = document.getElementsByTagName('head')[0];
    if (!vH) { return 2; }
    var vS = document.createElement('script');
    vS.id = vI;
    if (!vS) { return 3; }
    vS.type = 'text/javascript';
    var vP = '/hddiab/dhtml/flowplayer-3.1.4.min.js';
    vS.src = vP;
    vH.appendChild(vS);
    return 0;
}
function fnPgn(vS, vT, vB) {
    var vS = ge$(vS); if (!vS) { return; }
    var vT = ge$(vT); if (!vT) { return; }
    var vB = ge$(vB); if (!vB) { return; }
    vT.value = vS.options[vS.selectedIndex].value;
    vB.click();
}
function fnPgnx(vV) {
    var vT = ge$('txtDhtmlPgn'); if (!vT) { return; }
    var vB = ge$('btnBuscar1'); if (!vB) { return; }
    vT.value = vV;
    vB.click();
}
function fnShowMenuB(oDv1, oDv2) {
    var vT = 0;
    var vL = 0;
    var ob1 = ge$(oDv1);
    var ob2 = ge$(oDv2);
    if (ob1) {
        vT = parseInt(ob1.style.top, 10) + parseInt(ob1.style.height, 10) + gTop1 - 3;
        vL = parseInt(ob1.style.left, 10);
    }
    else {
        vT = event.offsetY;
        vL = event.offsetX;
        vT = event.clientY + document.body.scrollTop;
        vL = event.clientX + document.body.scrollLeft;
    }
    if (ob2) {
        ob2.style.left = vL;
        ob2.style.top = vT;
        ob2.style.zIndex = fnMaxZ() + 1;
        ob2.style.visibility = 'visible';
        ob2.style.display = '';
        ob2.onmouseover = function () { this.style.visibility = 'visible'; this.style.display = ''; };
        ob2.onmouseout = function () { this.style.visibility = 'hidden'; this.style.display = 'none'; };
    }
}
function fnShowMenu(oDv1, oDv2) {
    var vT = 0;
    var vL = 0;
    var ob1 = ge$(oDv1);
    var ob2 = ge$(oDv2);
    if (ob1) {
        vT = parseInt(ob1.style.top, 10) + parseInt(ob1.style.height, 10) - 3;
        vL = parseInt(ob1.style.left, 10);
    }
    else {
        vT = event.offsetY;
        vL = event.offsetX;
        vT = event.clientY + document.body.scrollTop;
        vL = event.clientX + document.body.scrollLeft;
    }
    if (ob2) {
        ob2.style.left = vL;
        ob2.style.top = vT;
        ob2.style.zIndex = fnMaxZ() + 1;
        ob2.style.visibility = 'visible';
        ob2.style.display = '';
        ob2.onmouseover = function () { this.style.visibility = 'visible'; this.style.display = ''; };
        ob2.onmouseout = function () { this.style.visibility = 'hidden'; this.style.display = 'none'; };
    }
}
function fnIfRdr(vI, vS) {
    if (!vI || !vS) { return; }
    var vIf = ge$(vI); if (!vIf) { return; }
    //vIfr.style.width  = vDiv.style.width;
    //vIfr.style.height = vDiv.style.height;
    vIf.src = '';
    vIf.src = vS;
}

function fnCenter(vO) {
    var obj = ge$(vO);
    if (obj) {
        var vw = parseInt(obj.style.width, 10);
        var vh = parseInt(obj.style.height, 10);
        var vsl = parseInt(document.documentElement.scrollLeft, 10);
        var vst = parseInt(document.documentElement.scrollTop, 10);
        var vl = parseInt((document.documentElement.clientWidth - vw) / 2, 10) + vsl;
        var vt = parseInt((document.documentElement.clientHeight - vh) / 2, 10) + vst;
        obj.style.position = 'absolute';
        obj.style.top = vt + 'px';
        obj.style.left = vl + 'px';
    }
}
function setDvx() {
    var w1 = "<div id='" + gsTtpx + "' style='left:0px;position:absolute;top:0px;z-index:1000000;'></div>";
    var xOb = document.createElement(w1);
    document.body.appendChild(xOb);
    getDvx();
}
function fnCheckAreaLength(textBox, e, length) {
    var mLen = textBox["MaxLength"];
    if (null == mLen) mLen = length;
    var maxLength = parseInt(mLen);
    if (!checkSpecialKeys(e)) {
        if (textBox.value.length > maxLength - 1) {
            if (window.event)//IE
                e.returnValue = false;
            else//Firefox
                e.preventDefault();
        }
    }
}
function checkSpecialKeys(e) {
    if (e.keyCode != 8 && e.keyCode != 46 && e.keyCode != 37 && e.keyCode != 38 && e.keyCode != 39 && e.keyCode != 40)
        return false;
    else
        return true;
}
// inicio: callback
function putCallbackResult(vArg) {
    if (!(vArg == "")) {
        var vDiv = '';
        var vHtm = '';
        var vJS = '';
        var vP = vArg.split(gsSepART); //'|');
        var vLen = vP.length;
        if (vLen >= 1) { vDiv = vP[0]; }
        if (vLen >= 2) { vHtm = vP[1]; }
        if (vLen >= 3) { vJS = vP[2]; }
        var vDV = document.getElementById(vDiv);
        if (vDV && vHtm) { vDV.innerHTML = vHtm; }
        //if(vJS){alert(vDiv);var tmpF = new Function(vJS);tmpF();}
        if (vJS) { fnExec(vJS); }
    }
}

function fnExec(vJS) {
    var tmpF = new Function(vJS); tmpF();
}

function clientErrorCallback(error, context)
{ alert('Error en llamada de componente Ajax: ' + error); }

// fin: callback

function fnWaitDv(vH, vM) {
    var vD = ge$(gDvW);
    if (!vD) { return; }
    vD.className = 'cssWMSG';
    var vVs = 'visible';
    var vDs = '';
    if (vH) {
        vVs = 'hidden';
        vDs = 'none';
    }
    vD.style.visibility = vVs;
    vD.style.display = vDs;
    if (!vH) { vD.innerHTML = vM; }
}

function fnInfo(vM, vE, vA) {
    if (vA) { fnWaitDv(true, ''); alert(vM); return; }
    var vD = ge$(gDvW);
    if (!vD) { return; }
    var vCl = 'cssWINF';
    var vMl = 4000;
    if (vE) { vCl = 'cssWERR'; vMl = 30000; }
    vD.className = vCl;
    var vVs = 'visible';
    var vDs = '';
    vD.style.visibility = vVs;
    vD.style.display = vDs;
    vD.innerHTML = vM;
    setTimeout("fnWaitDv(true,'');", vMl)
}

function fnAddEvent(obj, type, fn) {
    var obN = obj;
    obj = ge$(obj);
    if (!(obj)) { return; }
    var fnx = new Function(fn);
    if (obj.attachEvent) {
        //obj['e'+type+fn] = fn;
        //obj[type+fn] = function(){obj['e'+type+fn]( window.event );}
        //obj.attachEvent( 'on'+type, obj[type+fn] );
        //var fny = new Function(obj[type+fn]);  
        obj.attachEvent('on' + type, fnx);
    } else
        obj.addEventListener(type, fnx, false);
}

function fnMsgbox(asMsg) {
    var vw = 240;
    var vh = 120;
    var vsl = parseInt(document.documentElement.scrollLeft, 10);
    var vst = parseInt(document.documentElement.scrollTop, 10);
    vl = parseInt((document.documentElement.clientWidth - vw) / 2, 10) + vsl;
    vt = parseInt((document.documentElement.clientHeight - vh) / 2, 10) + vst;
    var lZ = fnMaxZ();
    var lZ1 = lZ - 1;
    fnSelSH(false);
    var xObN = gOvF;
    w = "<div id='" + xObN + "'" + " style='z-index:" + lZ1 + ";' " + " class='cssOvrl'></div>";
    var xOb = document.createElement(w);
    xOb.style.display = "";
    document.body.appendChild(xOb);
    // Crea DIV PRINCIPAL.  
    w = fnGetDiv(gOvM, "xWin_F", vl, vt, vw, vh, lZ, "", "");
    d1 = document.createElement(w);
    d1.style.background = 'white';
    var sWt = "<img src='" + gFimg + "/wait.gif'>";
    var sMs = '<p class="xWinSpan">' + asMsg + '</p><p class="xWinSpan">' + sWt + '</p>';
    d1.innerHTML = sMs;
    if (asMsg.substring(0, gSpl.length) == gSpl) {
        var vx01 = asMsg.substring(gSpl.length, 100);
        var xOb001 = ge$(vx01);
        if (xOb001) {
            d1.innerHTML = xOb001.innerHTML;
            d1.style.width = xOb001.style.width;
            d1.style.height = xOb001.style.height;
            fnCenter(d1.id);
        }
    }
    document.body.appendChild(d1);
    fnCenter(d1.id);
}

function fnAddObj(obj) {
    obj = document.createElement(obj);
    if (obj) { document.body.appendChild(obj); }
}

function fnPagAjax(vT, vB, vP) {
    var vTx = ge$(vT);
    if (vTx) {
        var vBx = ge$(vB);
        vTx.value = vP;
        if (vBx) {
            //setTimeout('vTx.value.value=vP;',1);
            //setTimeout('',2);
            //setTimeout('',100);
            //& Trim(CStr(ob)) & "\').click()',10);"
            // vTx.value.value=vP;vBx.click();vTx.value.value=1;
            vBx.click();
            vTx.value = 1;
        }
    }
}

function fnStack1(vO1, vO2) {
    vO1 = ge$(vO1);
    vO2 = ge$(vO2);
    if (vO1 && vO2) {
        var vt = parseInt(vO1.style.top) + 10;
        vt += parseInt(vO1.style.height);
        vO2.style.top = vt + 'px';
    }
}

function fnGetTxtID(vX) {
    var vRet = '';
    var vX = ge$(vX);
    if (vX) {
        vRet = vX.value;
        var vN = parseInt(vX.xIDX, 10);
        if (vN > 0) { vRet = vN; }
    }
    return vRet;
}
