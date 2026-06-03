<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RepDespachoPacMicro.aspx.cs" Inherits="contenido_Reportes_RepDespachoPacMicro" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=11; IE=9; IE=8; IE=7; IE=EDGE" />
    <title>Despachos a Pacientes</title>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.5.1/css/all.min.css" />
    <link runat="server" href="~/css/Estilos1.css"      rel="stylesheet" type="text/css" id="Link1" />
    <link runat="server" href="~/css/css.css"            rel="stylesheet" type="text/css" id="Link2" />
    <link runat="server" href="~/css/layout.css?v=3"     rel="stylesheet" type="text/css" id="Link3" />
    <script type="text/javascript">
        function KeyEnter(e) { if (e.keyCode == 13) { __doPostBack('KeyEnterPostBack', ''); } }
    </script>
</head>
<body>
<form id="form1" runat="server">
<div class="report-container">

    <asp:ScriptManager ID="ToolkitScriptManager1" EnableScriptGlobalization="True"
        EnablePageMethods="True" runat="server" />

    <%-- Encabezado --%>
    <div class="report-header">
        <div class="report-header-title">
            <i class="fas fa-procedures"></i>
            <span>Reportes &rarr; Despachos a Pacientes</span>
        </div>
    </div>

    <%-- Panel de filtros --%>
    <div class="card-panel">
        <div class="filter-grid">

            <%-- Período --%>
            <div class="filter-section-title"><i class="fas fa-calendar-alt"></i> Período</div>
            <span class="form-label">Fecha Inicio:</span>
            <span>
                <asp:TextBox ID="em_f_desde" runat="server" CssClass="form-control" placeholder="dd/mm/aaaa"></asp:TextBox>
                <ajaxToolkit:CalendarExtender ID="CalendarExtender2" TargetControlID="em_f_desde" runat="server" />
                <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender5" TargetControlID="em_f_desde"
                    Mask="99/99/9999" runat="server" MessageValidatorTip="true"
                    OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="MaskedEditError" MaskType="Date" />
                <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator1" ControlToValidate="em_f_desde"
                    ControlExtender="MaskedEditExtender5" InvalidValueMessage="Fecha inválida"
                    runat="server" ErrorMessage="Fecha inicio inválida" />
            </span>
            <span class="form-label">Fecha Término:</span>
            <span>
                <asp:TextBox ID="em_f_hasta" runat="server" CssClass="form-control" placeholder="dd/mm/aaaa"></asp:TextBox>
                <ajaxToolkit:CalendarExtender ID="CalendarExtender1" TargetControlID="em_f_hasta" runat="server" />
                <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender1" TargetControlID="em_f_hasta"
                    Mask="99/99/9999" runat="server" MessageValidatorTip="true"
                    OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="MaskedEditError" MaskType="Date" />
                <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator2" ControlToValidate="em_f_hasta"
                    ControlExtender="MaskedEditExtender1" InvalidValueMessage="Fecha inválida"
                    runat="server" ErrorMessage="Fecha término inválida" />
            </span>

            <%-- Médico --%>
            <div class="filter-section-title"><i class="fas fa-user-md"></i> Médico</div>
            <span class="form-label">Rut:</span>
            <span>
                <asp:TextBox ID="TRutMed" runat="server" CssClass="form-control"
                    placeholder="Ej: 12345678" onkeypress="KeyEnter(event)" onblur="IsInteger(this);"></asp:TextBox>
            </span>
            <span class="form-label">Nombre:</span>
            <span class="search-wrap">
                <asp:TextBox ID="TNombMed" runat="server" CssClass="form-control"
                    placeholder="Nombre del médico" onkeypress="KeyEnter(event)"></asp:TextBox>
                <button type="button" class="search-btn" title="Buscar médico por nombre"
                        onclick="buscarMaestro('medicos','TNombMed','TRutMed','medico')">
                    <i class="fas fa-search"></i>
                </button>
                <div id="drop_TNombMed" class="search-dropdown"></div>
            </span>

            <%-- Paciente --%>
            <div class="filter-section-title"><i class="fas fa-procedures"></i> Paciente</div>
            <span class="form-label">Rut:</span>
            <span>
                <asp:TextBox ID="TRutPac" runat="server" CssClass="form-control"
                    placeholder="Ej: 12345678" onkeypress="KeyEnter(event)" onblur="IsInteger(this);"></asp:TextBox>
            </span>
            <span class="form-label">Nombre:</span>
            <span class="search-wrap">
                <asp:TextBox ID="TNombPac" runat="server" CssClass="form-control"
                    placeholder="Nombre del paciente" onkeypress="KeyEnter(event)"></asp:TextBox>
                <button type="button" class="search-btn" title="Buscar paciente por nombre"
                        onclick="buscarMaestro('pacientes','TNombPac','TRutPac','paciente')">
                    <i class="fas fa-search"></i>
                </button>
                <div id="drop_TNombPac" class="search-dropdown"></div>
            </span>

            <%-- Artículo --%>
            <div class="filter-section-title"><i class="fas fa-pills"></i> Artículo</div>
            <span class="form-label">Código:</span>
            <span>
                <asp:TextBox ID="TCodigo" runat="server" CssClass="form-control"
                    placeholder="Código artículo" onkeypress="KeyEnter(event)"></asp:TextBox>
            </span>
            <span class="form-label">Descripción:</span>
            <span class="search-wrap">
                <asp:TextBox ID="TDesc" runat="server" CssClass="form-control"
                    placeholder="Nombre del artículo" onkeypress="KeyEnter(event)"></asp:TextBox>
                <button type="button" class="search-btn" title="Buscar medicamento por descripción"
                        onclick="buscarMaestro('medicamentos','TDesc','TCodigo','medicamento')">
                    <i class="fas fa-search"></i>
                </button>
                <div id="drop_TDesc" class="search-dropdown"></div>
            </span>

            <%-- Clasificación --%>
            <div class="filter-section-title"><i class="fas fa-filter"></i> Clasificación</div>
            <span class="form-label">Servicio:</span>
            <span>
                <asp:DropDownList ID="ddlServicio" runat="server" CssClass="form-control"></asp:DropDownList>
            </span>
            <span class="form-label">Bodega:</span>
            <span>
                <asp:DropDownList ID="ddlBod" runat="server" CssClass="form-control"></asp:DropDownList>
            </span>

        </div><%-- /filter-grid --%>

        <%-- Acciones --%>
        <div class="filter-actions">
            <asp:Button ID="BtnDespachar0" runat="server" CssClass="btn-main"
                OnClick="btn_Buscar_Click" Text="🔍  Buscar" />
            <asp:LinkButton runat="server" ID="ImBtPrint0" CssClass="btn-excel"
                OnClick="btn_Excel_Click" OnClientClick="showLoaderExcel();" ToolTip="Exportar a Excel">
                <i class="fas fa-file-excel"></i><span>Excel</span>
            </asp:LinkButton>
            <asp:LinkButton runat="server" ID="pdf" CssClass="btn-pdf" Visible="false"
                OnClick="pdfBtn" ToolTip="Exportar a PDF">
                <i class="fas fa-file-pdf"></i><span>PDF</span>
            </asp:LinkButton>
        </div>

        <%-- Mensaje --%>
        <asp:UpdatePanel ID="UpdatePanel4" runat="server">
            <ContentTemplate>
                <asp:Label ID="lbmensaje" runat="server" Text="" CssClass="report-msg"></asp:Label>
            </ContentTemplate>
        </asp:UpdatePanel>

    </div><%-- /card-panel filtros --%>

    <%-- Resultados --%>
    <div class="card-panel">
        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate>
                <div class="grid-scroll-wrapper">
                <asp:GridView ID="dgData" runat="server" CssClass="grid-modern"
                    GridLines="None" Width="100%"
                    OnSelectedIndexChanged="dgData_SelectedIndexChanged"
                    OnRowDataBound="dgData_RowDataBound"
                    AllowPaging="True" onpageindexchanging="dgData_PageIndexChanging"
                    PageSize="30" AutoGenerateColumns="False"
                    AllowSorting="True" onsorting="dgData_Sorting">
                    <Columns>
                        <asp:BoundField DataField="UNIDAD"       HeaderText="Policlínico"      SortExpression="UNIDAD">      <ItemStyle HorizontalAlign="Center" /></asp:BoundField>
                        <asp:BoundField DataField="BODEGA"       HeaderText="Bodega"            SortExpression="BODEGA">      <ItemStyle HorizontalAlign="Center" /></asp:BoundField>
                        <asp:BoundField DataField="FOLIO"        HeaderText="N° Receta"         SortExpression="FOLIO">       <ItemStyle HorizontalAlign="Center" /></asp:BoundField>
                        <asp:BoundField DataField="NOMB_ART"     HeaderText="Medicamento"       SortExpression="NOMB_ART">    <ItemStyle HorizontalAlign="Center" /></asp:BoundField>
                        <asp:BoundField DataField="FRECUENCIA"   HeaderText="Frecuencia"        SortExpression="FRECUENCIA">  <ItemStyle HorizontalAlign="Center" /></asp:BoundField>
                        <asp:BoundField DataField="FDESPACHO"    HeaderText="Fecha Entrega"     SortExpression="FDESPACHO">   <ItemStyle HorizontalAlign="Center" /></asp:BoundField>
                        <asp:BoundField DataField="DIAS"         HeaderText="Días Tratamiento"  SortExpression="DIAS">        <ItemStyle HorizontalAlign="Center" /></asp:BoundField>
                        <asp:BoundField DataField="CANTIDAD"     HeaderText="Cant. Despachada"  SortExpression="CANTIDAD">    <ItemStyle HorizontalAlign="Center" /></asp:BoundField>
                        <asp:BoundField DataField="FNEXTDESPACHO" HeaderText="Fecha Próx. Desp." SortExpression="FNEXTDESPACHO"><ItemStyle HorizontalAlign="Center" /></asp:BoundField>
                        <asp:BoundField DataField="CANT_PEND"    HeaderText="Cant. Pendiente"   SortExpression="CANT_PEND">   <ItemStyle HorizontalAlign="Center" /></asp:BoundField>
                    </Columns>
                    <HeaderStyle Font-Bold="True" />
                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                    <PagerStyle CssClass="pager" />
                </asp:GridView>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>

</div><%-- /report-container --%>

<%-- GridView oculto para Excel --%>
<asp:UpdatePanel ID="UpdatePanel3" runat="server">
    <ContentTemplate>
        <asp:GridView ID="dgData0" runat="server" CssClass="grid-modern"
            GridLines="None" Width="100%"
            OnSelectedIndexChanged="dgData_SelectedIndexChanged"
            OnRowDataBound="dgData_RowDataBound"
            onpageindexchanging="dgData_PageIndexChanging"
            PageSize="30" AutoGenerateColumns="False" Visible="False">
            <Columns>
                <asp:BoundField DataField="UNIDAD"       HeaderText="Policlínico"      SortExpression="UNIDAD" />
                <asp:BoundField DataField="BODEGA"       HeaderText="Bodega"            SortExpression="BODEGA" />
                <asp:BoundField DataField="FOLIO"        HeaderText="N° Receta"         SortExpression="FOLIO" />
                <asp:BoundField DataField="NOMB_ART"     HeaderText="Medicamento"       SortExpression="NOMB_ART" />
                <asp:BoundField DataField="FRECUENCIA"   HeaderText="Frecuencia"        SortExpression="FRECUENCIA" />
                <asp:BoundField DataField="FDESPACHO"    HeaderText="Fecha Entrega"     SortExpression="FDESPACHO" />
                <asp:BoundField DataField="DIAS"         HeaderText="Días Tratamiento"  SortExpression="DIAS" />
                <asp:BoundField DataField="CANTIDAD"     HeaderText="Cant. Despachada"  SortExpression="CANTIDAD" />
                <asp:BoundField DataField="FNEXTDESPACHO" HeaderText="Fecha Próx. Desp." SortExpression="FNEXTDESPACHO" />
                <asp:BoundField DataField="CANT_PEND"    HeaderText="Cant. Pendiente"   SortExpression="CANT_PEND" />
            </Columns>
            <HeaderStyle Font-Bold="True" />
            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
        </asp:GridView>
    </ContentTemplate>
</asp:UpdatePanel>

</form>

<%-- Overlay de carga (FUERA del form) --%>
<div id="loadingOverlay" class="loading-overlay" style="display:none;">
    <div class="loading-box">
        <i class="fas fa-circle-notch loading-icon"></i>
        <span>Buscando...</span>
    </div>
</div>

<script type="text/javascript">
    function showLoader(texto) {
        var el = document.getElementById('loadingOverlay');
        if (!el) return;
        var msg = el.querySelector('span');
        if (msg) msg.textContent = texto || 'Buscando...';
        el.style.display = 'flex';
    }
    function hideLoader() {
        var el = document.getElementById('loadingOverlay');
        if (el) el.style.display = 'none';
    }
    hideLoader();

    var frm = document.getElementById('form1');
    if (frm) { frm.addEventListener('submit', function () { showLoader('Buscando...'); }); }

    function showLoaderExcel() {
        document.cookie = 'xlsxReady=; expires=Thu, 01 Jan 1970 00:00:00 UTC; path=/;';
        showLoader('Generando Excel...');
        var timer = setInterval(function () {
            if (document.cookie.indexOf('xlsxReady=1') !== -1) {
                clearInterval(timer);
                document.cookie = 'xlsxReady=; expires=Thu, 01 Jan 1970 00:00:00 UTC; path=/;';
                hideLoader();
            }
        }, 400);
        setTimeout(function () { clearInterval(timer); hideLoader(); }, 90000);
    }

    window.addEventListener('load',     hideLoader);
    window.addEventListener('pageshow', hideLoader);

    if (typeof Sys !== 'undefined' && !window._prmHandlersAdded) {
        window._prmHandlersAdded = true;
        Sys.Application.add_load(function () {
            if (!window._prmBound) {
                window._prmBound = true;
                var prm = Sys.WebForms.PageRequestManager.getInstance();
                prm.add_beginRequest(function () { showLoader('Buscando...'); });
                prm.add_endRequest(function ()   { hideLoader(); });
            }
        });
    }

    function buscarMaestro(endpoint, inputBusqId, inputTargetId, tipo) {
        var inputEl = document.getElementById(inputBusqId);
        var termino = inputEl ? inputEl.value.trim() : '';
        var drop    = document.getElementById('drop_' + inputBusqId);
        if (!drop) return;
        if (termino.length < 3) {
            drop.innerHTML = '<div class="search-msg-info"><i class="fas fa-info-circle"></i> Ingrese al menos 3 caracteres</div>';
            drop.style.display = 'block'; return;
        }
        var btn = inputEl.parentNode ? inputEl.parentNode.querySelector('.search-btn') : null;
        if (btn) btn.disabled = true;
        drop.innerHTML = '<div class="search-msg-info"><i class="fas fa-spinner fa-spin"></i> Buscando...</div>';
        drop.style.display = 'block';
        PageMethods.BuscarMaestro(endpoint, termino,
            function (result) {
                if (btn) btn.disabled = false;
                try {
                    var data = JSON.parse(result);
                    if (data && data.__error) {
                        drop.innerHTML = '<div class="search-msg-error"><i class="fas fa-exclamation-circle"></i> Error del servidor (' + data.status + ')</div>';
                    } else { renderDropdown(data, inputBusqId, inputTargetId, tipo); }
                } catch (e) { drop.innerHTML = '<div class="search-msg-error"><i class="fas fa-exclamation-circle"></i> Error en la respuesta</div>'; }
            },
            function () { if (btn) btn.disabled = false; drop.innerHTML = '<div class="search-msg-error"><i class="fas fa-exclamation-circle"></i> Error al conectar</div>'; }
        );
    }
    function renderDropdown(data, inputBusqId, targetId, tipo) {
        var drop = document.getElementById('drop_' + inputBusqId);
        if (!data || data.length === 0) { drop.innerHTML = '<div class="search-msg-empty"><i class="fas fa-search"></i> Sin resultados</div>'; return; }
        var html = '';
        for (var i = 0; i < data.length; i++) {
            var item = data[i], lbl = '', val = '';
            if (tipo === 'medico') {
                lbl = '<strong>' + escH(item.Nombre) + '</strong><span class="search-item-sub">RUT ' + item.Rut + '-' + item.Dv + (item.Especialidad ? ' &mdash; ' + escH(item.Especialidad) : '') + '</span>';
                val = String(item.Rut);
            } else if (tipo === 'paciente') {
                var nc = ((item.Nombre||'')+' '+(item.ApPaterno||'')+' '+(item.ApMaterno||'')).trim();
                lbl = '<strong>' + escH(nc) + '</strong><span class="search-item-sub">RUT ' + item.Rut + '-' + item.Dv + '</span>';
                val = String(item.Rut);
            } else {
                lbl = '<span class="search-item-cod">' + escH(item.CodArticulo) + '</span><span class="search-item-desc">' + escH(item.DescripcionLarga) + '</span>';
                val = item.CodArticulo;
            }
            html += '<div class="search-item" data-val="' + escA(val) + '" data-tgt="' + targetId + '" data-src="' + inputBusqId + '">' + lbl + '</div>';
        }
        drop.innerHTML = html;
        var items = drop.querySelectorAll('.search-item');
        for (var j = 0; j < items.length; j++) {
            items[j].addEventListener('click', function () {
                var tgt = document.getElementById(this.getAttribute('data-tgt'));
                if (tgt) tgt.value = this.getAttribute('data-val');
                document.getElementById('drop_' + this.getAttribute('data-src')).style.display = 'none';
            });
        }
    }
    function escH(s) { return String(s||'').replace(/&/g,'&amp;').replace(/</g,'&lt;').replace(/>/g,'&gt;'); }
    function escA(s) { return String(s||'').replace(/"/g,'&quot;'); }
    document.addEventListener('click', function (e) {
        var node = e.target;
        while (node) {
            if (node.className && typeof node.className === 'string' && node.className.indexOf('search-wrap') !== -1) return;
            node = node.parentElement;
        }
        var drops = document.querySelectorAll('.search-dropdown');
        for (var k = 0; k < drops.length; k++) drops[k].style.display = 'none';
    });
</script>

</body>
</html>

