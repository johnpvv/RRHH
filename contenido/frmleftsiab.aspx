<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmleftsiab.aspx.cs" Inherits="contenido_frmleftsiab" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../css/css.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:TreeView ID="TreeView1" runat="server" Height="47px" ImageSet="Inbox" Style="z-index: 100; left: 0px; position: absolute; top: 0px"
                Width="200px">
                <ParentNodeStyle Font-Bold="True" />
                <HoverNodeStyle Font-Underline="True" Font-Bold="true" />
                <SelectedNodeStyle Font-Bold="true" VerticalPadding="0px" />
                <NodeStyle Font-Bold="false" />
                <Nodes>
                    <%--<asp:TreeNode Expanded="True" SelectAction="Expand" ImageUrl="~/imagenes/menu/home.png" Text="HCSBA" Value="0">
                        <asp:TreeNode Expanded="True" SelectAction="Expand" ImageUrl="~/imagenes/menu/compras.png" Text="Receta Electrónica" Value="Receta Electrónica" ToolTip="Receta Electrónica">
                            <asp:TreeNode NavigateUrl="~/contenido/Receta/IngresoRecetaTab.aspx?key=0" Target="main" ImageUrl="~/imagenes/menu/archivos.png" Text="Crear Receta" ToolTip="Crear Receta" Value="0"></asp:TreeNode>
                            <asp:TreeNode NavigateUrl="~/contenido/Receta/ListaReceta.aspx?id=1" Target="main" ImageUrl="~/imagenes/menu/consulta.png" Text="Gestión Receta <br /> Emitidas" ToolTip="Gestión Receta Emitidas" Value="0"></asp:TreeNode>
                        </asp:TreeNode>
                    </asp:TreeNode>--%>
                </Nodes>
                <NodeStyle Font-Names="Verdana" Font-Size="8pt" ForeColor="Black" HorizontalPadding="5px"
                    NodeSpacing="0px" VerticalPadding="0px" />
            </asp:TreeView>
            <asp:TreeView ID="TreeView3" runat="server" Height="47px" ImageSet="Inbox" Style="z-index: 100; left: 0px; position: absolute; top: 0px"
                Width="200px">
                <ParentNodeStyle Font-Bold="True" />
                <HoverNodeStyle Font-Underline="True" Font-Bold="true" />
                <SelectedNodeStyle Font-Bold="true" VerticalPadding="0px" />
                <NodeStyle Font-Bold="false" />
                <Nodes>
<%--                    <asp:TreeNode Expanded="True" SelectAction="Expand" ImageUrl="~/imagenes/menu/home.png" Text="HCSBA" Value="0">
                        <asp:TreeNode Expanded="True" SelectAction="Expand" ImageUrl="~/imagenes/menu/compras.png" Text="Receta Electrónica" Value="Pase Infectologia" ToolTip="Pase Infectologia">
                            <asp:TreeNode NavigateUrl="~/contenido/Receta/IngresoRecetaInfectologia.aspx?key=0" Target="main" ImageUrl="~/imagenes/menu/archivos.png" Text="Crear Pase  <br /> Infectologia" ToolTip="Crear Pase Infectologia" Value="0"></asp:TreeNode>
                            <asp:TreeNode NavigateUrl="~/contenido/Receta/ListaInfectologia.aspx?id=1" Target="main" ImageUrl="~/imagenes/menu/consulta.png" Text="Gestión Pase <br /> Emitidos" ToolTip="Gestión Pase Emitidos" Value="0"></asp:TreeNode>
                        </asp:TreeNode>
                    </asp:TreeNode>--%>
                </Nodes>
                <NodeStyle Font-Names="Verdana" Font-Size="8pt" ForeColor="Black" HorizontalPadding="5px"
                    NodeSpacing="0px" VerticalPadding="0px" />
            </asp:TreeView>
            <asp:TreeView ID="TreeView2" runat="server" Height="47px" ImageSet="Inbox" Style="z-index: 100; left: 0px; position: absolute; top: 0px"
                Width="200px">
                <ParentNodeStyle Font-Bold="True" />
                <HoverNodeStyle Font-Underline="True" Font-Bold="true" />
                <SelectedNodeStyle Font-Bold="true" VerticalPadding="0px" />
                <NodeStyle Font-Bold="false" />
                <Nodes>
                    <asp:TreeNode Expanded="True" SelectAction="Expand" ImageUrl="~/imagenes/menu/home.png" Text="RR.HH." Value="0">
                        <asp:TreeNode Expanded="True" SelectAction="Expand" ImageUrl="~/imagenes/menu/compras.png" Text="Personas" Value="Personas" ToolTip="Personas">
<%--                            <asp:TreeNode NavigateUrl="~/contenido/Receta/ListaRecetaFarmacia.aspx?id=1" Target="main" ImageUrl="~/imagenes/menu/consulta.png" Text="Receta Ambulatoria" ToolTip="Receta Ambulatoria" Value="REC_AMBULA"></asp:TreeNode>
                            <asp:TreeNode NavigateUrl="~/contenido/Receta/ListaInfectologiaFarmacia.aspx?id=1" Target="main" ImageUrl="~/imagenes/menu/consulta.png" Text="Pase Infectologia" ToolTip="Pase Infectologia" Value="REC_INFECTO"></asp:TreeNode>--%>
                            <asp:TreeNode NavigateUrl="~/contenido/RRHH/ListaPersonas.aspx?id=1" Target="main" ImageUrl="~/imagenes/menu/consulta.png" Text="Listado Personas" ToolTip="Listado Personas" Value="MANT_USUARIO"></asp:TreeNode>
                        </asp:TreeNode>

                        <%--<asp:TreeNode Expanded="True" SelectAction="Expand" ImageUrl="~/imagenes/menu/compras.png" Text="Receta Electrónica" Value="Receta Electrónica" ToolTip="Receta Electrónica">
                            <asp:TreeNode NavigateUrl="~/contenido/Receta/ListaRecetaFarmacia.aspx?id=1" Target="main" ImageUrl="~/imagenes/menu/consulta.png" Text="Receta Ambulatoria" ToolTip="Receta Ambulatoria" Value="REC_AMBULA"></asp:TreeNode>
                            <asp:TreeNode NavigateUrl="~/contenido/Receta/ListaInfectologiaFarmacia.aspx?id=1" Target="main" ImageUrl="~/imagenes/menu/consulta.png" Text="Pase Infectologia" ToolTip="Pase Infectologia" Value="REC_INFECTO"></asp:TreeNode>
                            <asp:TreeNode NavigateUrl="~/contenido/Receta/ListaInfectologiaFarmacia.aspx?id=1" Target="main" ImageUrl="~/imagenes/menu/consulta.png" Text="Pase Infectologia" ToolTip="Pase Infectologia" Value="REC_INFECTO"></asp:TreeNode>
                        </asp:TreeNode>
                        <asp:TreeNode Expanded="True" SelectAction="Expand" ImageUrl="~/imagenes/menu/compras.png" Text="Dispensación" Value="Dispensación" ToolTip="Dispensación">
                            <asp:TreeNode NavigateUrl="~/contenido/Dispensacion/IngresoReceta.aspx?key=0" Target="main" ImageUrl="~/imagenes/menu/archivos.png" Text="Digitación <br> Receta Manual" ToolTip="Crear Receta" Value="REC_DIG_MANUAL"></asp:TreeNode>
                            <asp:TreeNode NavigateUrl="~/contenido/Dispensacion/ListaRecetaManual.aspx?id=1" Target="main" ImageUrl="~/imagenes/menu/consulta.png" Text="Gestion <br>Recetas Manuales" ToolTip="Recetas Manuales" Value="REC_GEST_MANUAL"></asp:TreeNode>
                            <asp:TreeNode NavigateUrl="~/contenido/Dispensacion/ListaConsultaReceta.aspx?id=1" Target="main" ImageUrl="~/imagenes/menu/consulta.png" Text="Consultar" ToolTip="Consultar" Value="REC_CONS_REC"></asp:TreeNode>
                            <asp:TreeNode NavigateUrl="~/contenido/Dispensacion/ListaRecetaValida.aspx?id=1" Target="main" ImageUrl="~/imagenes/menu/consulta.png" Text="Validar" ToolTip="Validar" Value="REC_VALIDAR"></asp:TreeNode>
                            <asp:TreeNode NavigateUrl="~/contenido/Dispensacion/ListaRecetaPrepara.aspx?id=1" Target="main" ImageUrl="~/imagenes/menu/consulta.png" Text="Preparar" ToolTip="Preparar" Value="REC_PREPARAR"></asp:TreeNode>
                            <asp:TreeNode NavigateUrl="~/contenido/Dispensacion/ListaRecetaDisp.aspx?id=1" Target="main" ImageUrl="~/imagenes/menu/consulta.png" Text="Dispensar" ToolTip="Dispensar" Value="REC_DISPENSAR"></asp:TreeNode>
                            <asp:TreeNode NavigateUrl="~/contenido/Dispensacion/ListaConsultaPendientes.aspx?id=1" Target="main" ImageUrl="~/imagenes/menu/consulta.png" Text="Pendientes" ToolTip="Pendientes" Value="REC_PENDIENTES"></asp:TreeNode>
                            <asp:TreeNode NavigateUrl="~/contenido/Despachos/ListaAnularDespachos.aspx?id=1" Target="main" ImageUrl="~/imagenes/menu/consulta.png" Text="Anular<br /> Dispensación" ToolTip="Anular Dispensación" Value="REC_ANUL_DISP"></asp:TreeNode>
                            <asp:TreeNode NavigateUrl="~/contenido/Dispensacion/NoDespPorSaldo.aspx?id=1" Target="main" ImageUrl="~/imagenes/menu/consulta.png" Text="No Dispensados" ToolTip="No Dispensados" Value="REC_REPORTES"></asp:TreeNode>
                        </asp:TreeNode>
                        <asp:TreeNode Expanded="False" SelectAction="Expand" ImageUrl="~/imagenes/menu/compras.png" Text="Reportes" Value="Reportes" ToolTip="Reportes">
                            <asp:TreeNode Expanded="False" SelectAction="Expand" ImageUrl="~/imagenes/menu/compras.png" Text="Agendados" Value="Agendados" ToolTip="Agendados">
                                <asp:TreeNode NavigateUrl="~/contenido/Reportes/RepRecAgendados.aspx" Target="main" ImageUrl="~/imagenes/menu/ajustesunidadoperativa.png" Text="Recetas <br>Agendados" ToolTip="Recetas Agendados" Value="RPT_REC_AGEN"></asp:TreeNode>
                                <asp:TreeNode NavigateUrl="~/contenido/Reportes/RepPacAgendados.aspx" Target="main" ImageUrl="~/imagenes/menu/ajustesunidadoperativa.png" Text="Pacientes <br>Agendados" ToolTip="Pacientes Agendados" Value="RPT_PAC_AGEN"></asp:TreeNode>
                                <asp:TreeNode NavigateUrl="~/contenido/Reportes/RepFarmPacAgendado.aspx" Target="main" ImageUrl="~/imagenes/menu/ajustesunidadoperativa.png" Text="Farmacos <br>Agendados" ToolTip="Farmacos Agendados" Value="RPT_FAR_AGEN"></asp:TreeNode>
                            </asp:TreeNode>

                            <asp:TreeNode Expanded="False" SelectAction="Expand" ImageUrl="~/imagenes/menu/compras.png" Text="Despachos" Value="Despachos" ToolTip="Despachos">
                                <asp:TreeNode NavigateUrl="~/contenido/Reportes/RepDespachos.aspx" Target="main" ImageUrl="~/imagenes/menu/ajustesunidadoperativa.png" Text="Despachos <br> General" ToolTip="Despachos General" Value="RPT_DES_GEN"></asp:TreeNode>
                                <asp:TreeNode NavigateUrl="~/contenido/Reportes/RepRecDispensadas.aspx" Target="main" ImageUrl="~/imagenes/menu/ajustesunidadoperativa.png" Text="Recetas <br> Prescritas" ToolTip="Recetas Prescritas" Value="RPT_REC_PRE"></asp:TreeNode>
                                <asp:TreeNode NavigateUrl="~/contenido/Reportes/RepDespachoPac.aspx" Target="main" ImageUrl="~/imagenes/menu/ajustesunidadoperativa.png" Text="Despachos <br> por Pacientes" ToolTip="Despachos por Pacientes" Value="RPT_DES_PAC"></asp:TreeNode>
                                <asp:TreeNode NavigateUrl="~/contenido/Reportes/rptDespPend.aspx" Target="main" ImageUrl="~/imagenes/menu/ajustesunidadoperativa.png" Text="Despachos <br>Pedientes" ToolTip="Pendientes" Value="RPT_DES_PEN"></asp:TreeNode>
                            </asp:TreeNode>

                            <asp:TreeNode Expanded="False" SelectAction="Expand" ImageUrl="~/imagenes/menu/compras.png" Text="Infectologia" Value="Infectologia" ToolTip="Infectologia">
                                <asp:TreeNode NavigateUrl="~/contenido/Reportes/RepDetInfect.aspx" Target="main" ImageUrl="~/imagenes/menu/ajustesunidadoperativa.png" Text="Detalle/ <br> Pase Infectologia" ToolTip="Detalle / Pase Infectologia" Value="RPT_INF_PAS"></asp:TreeNode>
                            </asp:TreeNode>
                            <asp:TreeNode Expanded="False" SelectAction="Expand" ImageUrl="~/imagenes/menu/compras.png" Text="Estadisticas" Value="Estadisticas" ToolTip="Estadisticas">
                                <asp:TreeNode NavigateUrl="~/contenido/Reportes/RepArsenal.aspx" Target="main" ImageUrl="~/imagenes/menu/ajustesunidadoperativa.png" Text="Arsenal Articulos <br> por Policlinico" ToolTip="Arsenal Articulos por Policlinico" Value="RPT_ARS_POL"></asp:TreeNode>
                                <asp:TreeNode NavigateUrl="~/contenido/Reportes/RepGastpPoli.aspx" Target="main" ImageUrl="~/imagenes/menu/ajustesunidadoperativa.png" Text="Gastos <br>Policlinicos" ToolTip="Gastos Policlinicos" Value="RPT_GAS_POL"></asp:TreeNode>
                                <asp:TreeNode NavigateUrl="~/contenido/Reportes/RepEstadisticaMesAnio.aspx" Target="main" ImageUrl="~/imagenes/menu/ajustesunidadoperativa.png" Text="Estadistica <br>Recetas Anual" ToolTip="Estadistica Recetas Anual" Value="RPT_EST_ANO"></asp:TreeNode>
                                <asp:TreeNode NavigateUrl="~/contenido/Reportes/RepEstadHora.aspx" Target="main" ImageUrl="~/imagenes/menu/ajustesunidadoperativa.png" Text="Estadistica RP / <br>Disp. por Hora" ToolTip="Estadistica Dispensación por Hora" Value="RPT_EST_HOR"></asp:TreeNode>
                                <asp:TreeNode NavigateUrl="~/contenido/Reportes/RepEstadoRecetaHora.aspx" Target="main" ImageUrl="~/imagenes/menu/ajustesunidadoperativa.png" Text="Estad. Rendimiento <br>Receta por Hora" ToolTip="Estad. Rendimiento Receta por Hora" Value="RPT_EST_RHOR"></asp:TreeNode>
                                <asp:TreeNode NavigateUrl="~/contenido/Reportes/RepEstRendimiento.aspx" Target="main" ImageUrl="~/imagenes/menu/ajustesunidadoperativa.png" Text="Estadistica <br>Rendimiento <br> Recetas Totales" ToolTip="Estadistica Rendimiento Recetas Totales" Value="RPT_EST_RTOT"></asp:TreeNode>
                            </asp:TreeNode>
                            <asp:TreeNode Expanded="False" SelectAction="Expand" ImageUrl="~/imagenes/menu/compras.png" Text="Controlados" Value="Controlados" ToolTip="Controlados">
                                <asp:TreeNode NavigateUrl="~/contenido/Reportes/rptControlados.aspx" Target="main" ImageUrl="~/imagenes/menu/ajustesunidadoperativa.png" Text="Controlados" ToolTip="Controlados" Value="RPT_CON_CON"></asp:TreeNode>
                                <asp:TreeNode NavigateUrl="~/contenido/Reportes/rptDespCheque.aspx" Target="main" ImageUrl="~/imagenes/menu/ajustesunidadoperativa.png" Text="Cheques" ToolTip="Cheques" Value="RPT_CON_CHQ"></asp:TreeNode>
                            </asp:TreeNode>
                            <asp:TreeNode Expanded="False" SelectAction="Expand" ImageUrl="~/imagenes/menu/compras.png" Text="Registro <br> Funcionarios" Value="Registro Funcionarios" ToolTip="Registro Funcionarios">
                                <asp:TreeNode NavigateUrl="~/contenido/Reportes/RepUsrFarm.aspx" Target="main" ImageUrl="~/imagenes/menu/ajustesunidadoperativa.png" Text="Registro Usuarios <br> Farmacia" ToolTip="Registro Usuarios Farmacia" Value="RPT_USR_FAR"></asp:TreeNode>
                                <asp:TreeNode NavigateUrl="~/contenido/Reportes/MedReceta.aspx" Target="main" ImageUrl="~/imagenes/menu/ajustesunidadoperativa.png" Text="Recetas Totales <br> por Medicos" ToolTip="Recetas Totales por Medicos" Value="RPT_TOT_MED"></asp:TreeNode>
                                <asp:TreeNode NavigateUrl="~/contenido/Reportes/RepRegMedicos.aspx" Target="main" ImageUrl="~/imagenes/menu/ajustesunidadoperativa.png" Text="Registro Medicos <br> por Policlinico" ToolTip="Registro Medicos por Policlinico" Value="RPT_MED_POL"></asp:TreeNode>
                            </asp:TreeNode>
                        </asp:TreeNode>
                        <asp:TreeNode Expanded="False" SelectAction="Expand" ImageUrl="~/imagenes/menu/ajustesb.png" Text="Administracion" Value="Administracion" ToolTip="Administracion">
                            <asp:TreeNode NavigateUrl="~/contenido/SysAdmin/ListaUsuarios.aspx?id=1" Target="main" ImageUrl="~/imagenes/menu/ajustesusuario.png" Text="Usuarios" ToolTip="Usuarios" Value="MANT_USUARIO"></asp:TreeNode>
                            <asp:TreeNode NavigateUrl="~/contenido/SysAdmin/ListaAccesos.aspx?id=1" Target="main" ImageUrl="~/imagenes/menu/ajustesaccesos.png" Text="Accesos" ToolTip="Accesos" Value="MANT_ACCESO"></asp:TreeNode>
                            <asp:TreeNode NavigateUrl="~/contenido/SysAdmin/ListaRoles.aspx?id=1" Target="main" ImageUrl="~/imagenes/menu/ajustesroles.png" Text="Roles" ToolTip="Roles" Value="MANT_ROLES"></asp:TreeNode>

                            <asp:TreeNode NavigateUrl="~/contenido/Administracion/ListaUniOperativa.aspx?id=1" Target="main" ImageUrl="~/imagenes/menu/ajustesunidadoperativa.png" Text="Lista Operativa" ToolTip="Unidad Operativa" Value="MANT_UNIDAD"></asp:TreeNode>
                            <asp:TreeNode NavigateUrl="~/contenido/Administracion/ListaArticulos.aspx?id=1" Target="main" ImageUrl="~/imagenes/menu/compras.png" Text="Articulos" ToolTip="Articulos" Value="MANT_ARTICULOS"></asp:TreeNode>
                            <asp:TreeNode NavigateUrl="~/contenido/SysAdmin/ListaPacientes.aspx?id=1" Target="main" ImageUrl="~/imagenes/menu/ajustesunidadoperativa.png" Text="Pacientes" ToolTip="Pacientes" Value="MANT_PACIENTE"></asp:TreeNode>
                            <asp:TreeNode NavigateUrl="~/contenido/SysAdmin/Constantes.aspx?id=1" Target="main" ImageUrl="~/imagenes/menu/firmas.png" Text="Const. Config" ToolTip="Const. de Configuracion" Value="MANT_CONST"></asp:TreeNode>
                            <asp:TreeNode NavigateUrl="~/contenido/SysAdmin/Feriados.aspx?id=1" Target="main" ImageUrl="~/imagenes/menu/firmas.png" Text="Feriados" ToolTip="Feriados" Value="MANT_CONST"></asp:TreeNode>
                            <asp:TreeNode NavigateUrl="~/contenido/Administracion/GestionEstadistica.aspx?id=1" Target="main" ImageUrl="~/imagenes/menu/compras.png" Text="Estadistica" ToolTip="Estadistica" Value="ADM_ESTAD"></asp:TreeNode>

                        </asp:TreeNode>
                    </asp:TreeNode>
                </Nodes>
                <NodeStyle Font-Names="Verdana" Font-Size="8pt" ForeColor="Black" HorizontalPadding="5px"
                    NodeSpacing="0px" VerticalPadding="0px" />
            </asp:TreeView>
            <asp:TreeView ID="TreeView4" runat="server" Height="47px" ImageSet="Inbox" Style="z-index: 100; left: 0px; position: absolute; top: 0px"
                Width="200px">
                <ParentNodeStyle Font-Bold="True" />
                <HoverNodeStyle Font-Underline="True" Font-Bold="true" />
                <SelectedNodeStyle Font-Bold="true" VerticalPadding="0px" />
                <NodeStyle Font-Bold="false" />
                <Nodes>
                    <asp:TreeNode Expanded="True" SelectAction="Expand" ImageUrl="~/imagenes/menu/home.png" Text="HCSBA" Value="0">
                        <asp:TreeNode Expanded="True" SelectAction="Expand" ImageUrl="~/imagenes/menu/compras.png" Text="Receta Electrónica" Value="Receta Electrónica" ToolTip="Receta Electrónica">
                            <asp:TreeNode NavigateUrl="~/contenido/Receta/ListaRecetaSalud.aspx?id=1" Target="main" ImageUrl="~/imagenes/menu/consulta.png" Text="Receta Ambulatoria" ToolTip="Receta Ambulatoria" Value="0"></asp:TreeNode>

                        </asp:TreeNode>
                    </asp:TreeNode>
                </Nodes>
                <NodeStyle Font-Names="Verdana" Font-Size="8pt" ForeColor="Black" HorizontalPadding="5px"
                    NodeSpacing="0px" VerticalPadding="0px" />
            </asp:TreeView>
            <asp:TreeView ID="TreeView5" runat="server" Height="47px" ImageSet="Inbox" Style="z-index: 100; left: 0px; position: absolute; top: 0px"
                Width="200px">
                <ParentNodeStyle Font-Bold="True" />
                <HoverNodeStyle Font-Underline="True" Font-Bold="true" />
                <SelectedNodeStyle Font-Bold="true" VerticalPadding="0px" />
                <NodeStyle Font-Bold="false" />
                <Nodes>
                    <asp:TreeNode Expanded="True" SelectAction="Expand" ImageUrl="~/imagenes/menu/home.png" Text="HCSBA2" Value="0">
                        <asp:TreeNode Expanded="True" SelectAction="Expand" ImageUrl="~/imagenes/menu/compras.png" Text="Receta Electrónica" Value="Receta Electrónica" ToolTip="Receta Electrónica">
                            <asp:TreeNode NavigateUrl="~/contenido/Receta/ListaRecetaFarmacia.aspx?id=1" Target="main" ImageUrl="~/imagenes/menu/consulta.png" Text="Receta Ambulatoria" ToolTip="Receta Ambulatoria" Value="REC_AMBULA"></asp:TreeNode>
                            <asp:TreeNode NavigateUrl="~/contenido/Receta/ListaInfectologiaFarmacia.aspx?id=1" Target="main" ImageUrl="~/imagenes/menu/consulta.png" Text="Pase Infectologia" ToolTip="Pase Infectologia" Value="REC_INFECTO"></asp:TreeNode>
                        </asp:TreeNode>
                        <asp:TreeNode Expanded="True" SelectAction="Expand" ImageUrl="~/imagenes/menu/compras.png" Text="Dispensación" Value="Dispensación" ToolTip="Dispensación">
                            <asp:TreeNode NavigateUrl="~/contenido/Dispensacion/IngresoReceta.aspx?key=0" Target="main" ImageUrl="~/imagenes/menu/archivos.png" Text="Digitación <br> Receta Manual" ToolTip="Crear Receta" Value="REC_DIG_MANUAL"></asp:TreeNode>
                            <asp:TreeNode NavigateUrl="~/contenido/Dispensacion/ListaRecetaManual.aspx?id=1" Target="main" ImageUrl="~/imagenes/menu/consulta.png" Text="Gestion <br>Recetas Manuales" ToolTip="Recetas Manuales" Value="REC_GEST_MANUAL"></asp:TreeNode>
                            <asp:TreeNode NavigateUrl="~/contenido/Dispensacion/ListaConsultaReceta.aspx?id=1" Target="main" ImageUrl="~/imagenes/menu/consulta.png" Text="Consultar" ToolTip="Consultar" Value="REC_CONS_REC"></asp:TreeNode>
                            <asp:TreeNode NavigateUrl="~/contenido/Dispensacion/ListaRecetaValida.aspx?id=1" Target="main" ImageUrl="~/imagenes/menu/consulta.png" Text="Validar" ToolTip="Validar" Value="REC_VALIDAR"></asp:TreeNode>
                            <asp:TreeNode NavigateUrl="~/contenido/Dispensacion/ListaRecetaPrepara.aspx?id=1" Target="main" ImageUrl="~/imagenes/menu/consulta.png" Text="Preparar" ToolTip="Preparar" Value="REC_PREPARAR"></asp:TreeNode>
                            <asp:TreeNode NavigateUrl="~/contenido/Dispensacion/ListaRecetaDisp.aspx?id=1" Target="main" ImageUrl="~/imagenes/menu/consulta.png" Text="Dispensar" ToolTip="Dispensar" Value="REC_DISPENSAR"></asp:TreeNode>
                            <asp:TreeNode NavigateUrl="~/contenido/Dispensacion/ListaConsultaPendientes.aspx?id=1" Target="main" ImageUrl="~/imagenes/menu/consulta.png" Text="Pendientes" ToolTip="Pendientes" Value="REC_PENDIENTES"></asp:TreeNode>
                            <asp:TreeNode NavigateUrl="~/contenido/Despachos/ListaAnularDespachos.aspx?id=1" Target="main" ImageUrl="~/imagenes/menu/consulta.png" Text="Anular<br /> Dispensación" ToolTip="Anular Dispensación" Value="REC_ANUL_DISP"></asp:TreeNode>
                            <asp:TreeNode NavigateUrl="~/contenido/Dispensacion/NoDespPorSaldo.aspx?id=1" Target="main" ImageUrl="~/imagenes/menu/consulta.png" Text="No Dispensados" ToolTip="No Dispensados" Value="REC_REPORTES"></asp:TreeNode>
                        </asp:TreeNode>
                        <asp:TreeNode Expanded="False" SelectAction="Expand" ImageUrl="~/imagenes/menu/compras.png" Text="Reportes" Value="Reportes" ToolTip="Reportes">
                            <asp:TreeNode Expanded="False" SelectAction="Expand" ImageUrl="~/imagenes/menu/compras.png" Text="Agendados" Value="Agendados" ToolTip="Agendados">
                                <asp:TreeNode NavigateUrl="~/contenido/Reportes/RepRecAgendados.aspx" Target="main" ImageUrl="~/imagenes/menu/ajustesunidadoperativa.png" Text="Recetas <br>Agendados" ToolTip="Recetas Agendados" Value="RPT_REC_AGEN"></asp:TreeNode>
                                <asp:TreeNode NavigateUrl="~/contenido/Reportes/RepPacAgendados.aspx" Target="main" ImageUrl="~/imagenes/menu/ajustesunidadoperativa.png" Text="Pacientes <br>Agendados" ToolTip="Pacientes Agendados" Value="RPT_PAC_AGEN"></asp:TreeNode>
                                <asp:TreeNode NavigateUrl="~/contenido/Reportes/RepFarmPacAgendado.aspx" Target="main" ImageUrl="~/imagenes/menu/ajustesunidadoperativa.png" Text="Farmacos <br>Agendados" ToolTip="Farmacos Agendados" Value="RPT_FAR_AGEN"></asp:TreeNode>
                            </asp:TreeNode>

                            <asp:TreeNode Expanded="False" SelectAction="Expand" ImageUrl="~/imagenes/menu/compras.png" Text="Despachos" Value="Despachos" ToolTip="Despachos">
                                <asp:TreeNode NavigateUrl="~/contenido/Reportes/RepDespachosMicro.aspx" Target="main" ImageUrl="~/imagenes/menu/ajustesunidadoperativa.png" Text="Despachos <br> General" ToolTip="Despachos General" Value="RPT_DES_GEN"></asp:TreeNode>
                                <asp:TreeNode NavigateUrl="~/contenido/Reportes/RepRecDispensadasMicro.aspx" Target="main" ImageUrl="~/imagenes/menu/ajustesunidadoperativa.png" Text="Recetas <br> Prescritas" ToolTip="Recetas Prescritas" Value="RPT_REC_PRE"></asp:TreeNode>
                                <asp:TreeNode NavigateUrl="~/contenido/Reportes/RepDespachoPacMicro.aspx" Target="main" ImageUrl="~/imagenes/menu/ajustesunidadoperativa.png" Text="Despachos <br> por Pacientes" ToolTip="Despachos por Pacientes" Value="RPT_DES_PAC"></asp:TreeNode>
                                <asp:TreeNode NavigateUrl="~/contenido/Reportes/rptDespPendMicro.aspx" Target="main" ImageUrl="~/imagenes/menu/ajustesunidadoperativa.png" Text="Despachos <br>Pedientes" ToolTip="Pendientes" Value="RPT_DES_PEN"></asp:TreeNode>
                            </asp:TreeNode>
                            <asp:TreeNode Expanded="False" SelectAction="Expand" ImageUrl="~/imagenes/menu/compras.png" Text="Infectologia" Value="Infectologia" ToolTip="Infectologia">
                                <asp:TreeNode NavigateUrl="~/contenido/Reportes/RepDetInfect.aspx" Target="main" ImageUrl="~/imagenes/menu/ajustesunidadoperativa.png" Text="Detalle/ <br> Pase Infectologia" ToolTip="Detalle / Pase Infectologia" Value="RPT_INF_PAS"></asp:TreeNode>
                            </asp:TreeNode>
                            <asp:TreeNode Expanded="False" SelectAction="Expand" ImageUrl="~/imagenes/menu/compras.png" Text="Estadisticas" Value="Estadisticas" ToolTip="Estadisticas">
                                <asp:TreeNode NavigateUrl="~/contenido/Reportes/RepArsenal.aspx" Target="main" ImageUrl="~/imagenes/menu/ajustesunidadoperativa.png" Text="Arsenal Articulos <br> por Policlinico" ToolTip="Arsenal Articulos por Policlinico" Value="RPT_ARS_POL"></asp:TreeNode>
                                <asp:TreeNode NavigateUrl="~/contenido/Reportes/RepGastpPoli.aspx" Target="main" ImageUrl="~/imagenes/menu/ajustesunidadoperativa.png" Text="Gastos <br>Policlinicos" ToolTip="Gastos Policlinicos" Value="RPT_GAS_POL"></asp:TreeNode>
                                <asp:TreeNode NavigateUrl="~/contenido/Reportes/RepEstadisticaMesAnio.aspx" Target="main" ImageUrl="~/imagenes/menu/ajustesunidadoperativa.png" Text="Estadistica <br>Recetas Anual" ToolTip="Estadistica Recetas Anual" Value="RPT_EST_ANO"></asp:TreeNode>
                                <asp:TreeNode NavigateUrl="~/contenido/Reportes/RepEstadHora.aspx" Target="main" ImageUrl="~/imagenes/menu/ajustesunidadoperativa.png" Text="Estadistica RP / <br>Disp. por Hora" ToolTip="Estadistica Dispensación por Hora" Value="RPT_EST_HOR"></asp:TreeNode>
                                <asp:TreeNode NavigateUrl="~/contenido/Reportes/RepEstadoRecetaHora.aspx" Target="main" ImageUrl="~/imagenes/menu/ajustesunidadoperativa.png" Text="Estad. Rendimiento <br>Receta por Hora" ToolTip="Estad. Rendimiento Receta por Hora" Value="RPT_EST_RHOR"></asp:TreeNode>
                                <asp:TreeNode NavigateUrl="~/contenido/Reportes/RepEstRendimiento.aspx" Target="main" ImageUrl="~/imagenes/menu/ajustesunidadoperativa.png" Text="Estadistica <br>Rendimiento <br> Recetas Totales" ToolTip="Estadistica Rendimiento Recetas Totales" Value="RPT_EST_RTOT"></asp:TreeNode>
                            </asp:TreeNode>
                            <asp:TreeNode Expanded="False" SelectAction="Expand" ImageUrl="~/imagenes/menu/compras.png" Text="Controlados" Value="Controlados" ToolTip="Controlados">
                                <asp:TreeNode NavigateUrl="~/contenido/Reportes/rptControlados.aspx" Target="main" ImageUrl="~/imagenes/menu/ajustesunidadoperativa.png" Text="Controlados" ToolTip="Controlados" Value="RPT_CON_CON"></asp:TreeNode>
                                <asp:TreeNode NavigateUrl="~/contenido/Reportes/rptDespCheque.aspx" Target="main" ImageUrl="~/imagenes/menu/ajustesunidadoperativa.png" Text="Cheques" ToolTip="Cheques" Value="RPT_CON_CHQ"></asp:TreeNode>
                            </asp:TreeNode>
                            <asp:TreeNode Expanded="False" SelectAction="Expand" ImageUrl="~/imagenes/menu/compras.png" Text="Registro <br> Funcionarios" Value="Registro Funcionarios" ToolTip="Registro Funcionarios">
                                <asp:TreeNode NavigateUrl="~/contenido/Reportes/RepUsrFarm.aspx" Target="main" ImageUrl="~/imagenes/menu/ajustesunidadoperativa.png" Text="Registro Usuarios <br> Farmacia" ToolTip="Registro Usuarios Farmacia" Value="RPT_USR_FAR"></asp:TreeNode>
                                <asp:TreeNode NavigateUrl="~/contenido/Reportes/MedReceta.aspx" Target="main" ImageUrl="~/imagenes/menu/ajustesunidadoperativa.png" Text="Recetas Totales <br> por Medicos" ToolTip="Recetas Totales por Medicos" Value="RPT_TOT_MED"></asp:TreeNode>
                                <asp:TreeNode NavigateUrl="~/contenido/Reportes/RepRegMedicos.aspx" Target="main" ImageUrl="~/imagenes/menu/ajustesunidadoperativa.png" Text="Registro Medicos <br> por Policlinico" ToolTip="Registro Medicos por Policlinico" Value="RPT_MED_POL"></asp:TreeNode>
                            </asp:TreeNode>
                        </asp:TreeNode>--%>
                        <asp:TreeNode Expanded="False" SelectAction="Expand" ImageUrl="~/imagenes/menu/ajustesb.png" Text="Administracion" Value="Administracion" ToolTip="Administracion">
                            <asp:TreeNode NavigateUrl="~/contenido/SysAdmin/ListaUsuarios.aspx?id=1" Target="main" ImageUrl="~/imagenes/menu/ajustesusuario.png" Text="Usuarios" ToolTip="Usuarios" Value="MANT_USUARIO"></asp:TreeNode>
                            <asp:TreeNode NavigateUrl="~/contenido/SysAdmin/ListaAccesos.aspx?id=1" Target="main" ImageUrl="~/imagenes/menu/ajustesaccesos.png" Text="Accesos" ToolTip="Accesos" Value="MANT_ACCESO"></asp:TreeNode>
                            <asp:TreeNode NavigateUrl="~/contenido/SysAdmin/ListaRoles.aspx?id=1" Target="main" ImageUrl="~/imagenes/menu/ajustesroles.png" Text="Roles" ToolTip="Roles" Value="MANT_ROLES"></asp:TreeNode>

                            <asp:TreeNode NavigateUrl="~/contenido/Administracion/ListaUniOperativa.aspx?id=1" Target="main" ImageUrl="~/imagenes/menu/ajustesunidadoperativa.png" Text="Lista Operativa" ToolTip="Unidad Operativa" Value="MANT_UNIDAD"></asp:TreeNode>
                            <asp:TreeNode NavigateUrl="~/contenido/Administracion/ListaArticulos.aspx?id=1" Target="main" ImageUrl="~/imagenes/menu/compras.png" Text="Articulos" ToolTip="Articulos" Value="MANT_ARTICULOS"></asp:TreeNode>
                            <asp:TreeNode NavigateUrl="~/contenido/SysAdmin/ListaPacientes.aspx?id=1" Target="main" ImageUrl="~/imagenes/menu/ajustesunidadoperativa.png" Text="Pacientes" ToolTip="Pacientes" Value="MANT_PACIENTE"></asp:TreeNode>
                            <asp:TreeNode NavigateUrl="~/contenido/SysAdmin/Constantes.aspx?id=1" Target="main" ImageUrl="~/imagenes/menu/firmas.png" Text="Const. Config" ToolTip="Const. de Configuracion" Value="MANT_CONST"></asp:TreeNode>
                            <asp:TreeNode NavigateUrl="~/contenido/SysAdmin/Feriados.aspx?id=1" Target="main" ImageUrl="~/imagenes/menu/firmas.png" Text="Feriados" ToolTip="Feriados" Value="MANT_CONST"></asp:TreeNode>
                            <asp:TreeNode NavigateUrl="~/contenido/Administracion/GestionEstadistica.aspx?id=1" Target="main" ImageUrl="~/imagenes/menu/compras.png" Text="Estadistica" ToolTip="Estadistica" Value="ADM_ESTAD"></asp:TreeNode>

                        </asp:TreeNode>
                    </asp:TreeNode>
                </Nodes>
                <NodeStyle Font-Names="Verdana" Font-Size="8pt" ForeColor="Black" HorizontalPadding="5px"
                    NodeSpacing="0px" VerticalPadding="0px" />
            </asp:TreeView>
        </div>
    </form>
</body>
</html>
