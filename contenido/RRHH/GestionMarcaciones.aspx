<%@ Page Language="C#" AutoEventWireup="true" CodeFile="GestionMarcaciones.aspx.cs" Inherits="contenido_RRHH_GestionMarcaciones" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Marcaciones</title>
    <style type="text/css">
        .GridMarcaciones {
            border-collapse: collapse;
            width: 100%;
            font-family: Arial, Helvetica, sans-serif;
            font-size: 13px;
        }

        .textoNorm {
            border-collapse: collapse;
            width: 100%;
            font-family: Arial, Helvetica, sans-serif;
            font-size: 13px;
            text-align:right;
        }

        .GridMarcacionesHeader {
            background-color: #1F6FB2;
            color: White;
            font-weight: bold;
            text-align: center;
            height: 35px;
        }

        .GridMarcacionesRow {
            background-color: White;
            height: 32px;
        }

        .GridMarcacionesAltRow {
            background-color: #F5F8FC;
            height: 32px;
        }

        .GridMarcaciones tr:hover {
            background-color: #E7F1FF;
        }

        .BotonPortal {
            background-color: #1F6FB2;
            color: white;
            border: none;
            padding: 8px 15px;
            font-weight: bold;
            cursor: pointer;
            border-radius: 4px;
        }

            .BotonPortal:hover {
                background-color: #18578C;
            }

        .BotonPortalVerde {
            background-color: #28A745;
            color: white;
            border: none;
            padding: 8px 15px;
            font-weight: bold;
            cursor: pointer;
            border-radius: 4px;
        }

            .BotonPortalVerde:hover {
                background-color: #218838;
            }

        .BotonPortalGris {
            background-color: #6C757D;
            color: white;
            border: none;
            padding: 8px 15px;
            font-weight: bold;
            cursor: pointer;
            border-radius: 4px;
        }

            .BotonPortalGris:hover {
                background-color: #545B62;
            }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <table style="width: 100%; margin-bottom: 10px;">
            <tr>

                <td style="width: 70px;" class="textoNorm">Mes:
                </td>

                <td style="width: 180px;">
                    <asp:DropDownList ID="ddlMes"
                        runat="server"
                        Width="150px">
                    </asp:DropDownList>
                </td>

                <td style="width: 70px;" class="textoNorm">Año:
                </td>

                <td style="width: 120px;">
                    <asp:DropDownList ID="ddlAnio"
                        runat="server"
                        Width="100px">
                    </asp:DropDownList>
                </td>

                <td>
                    <asp:Button ID="btnBuscar"
                        runat="server"
                        Text="Buscar"
                        CssClass="BotonPortal"
                        OnClick="btnBuscar_Click" />
                </td>

                <td align="right">

                    <asp:Button ID="btnExportar"
                        runat="server"
                        Text="Exportar Excel"
                        CssClass="BotonPortalVerde"
                        OnClick="btnExportar_Click" />

                    &nbsp;

            <asp:Button ID="btnVolver"
                runat="server"
                Text="Volver"
                CssClass="BotonPortalGris"
                OnClick="btnVolver_Click" Height="38px" />

                </td>

            </tr>
        </table>
        <asp:GridView ID="dgData"
            runat="server"
            AutoGenerateColumns="False"
            Width="100%"
            CssClass="GridMarcaciones"
            GridLines="None"
            CellPadding="6"
            EmptyDataText="Sin Resultados">

            <Columns>

                <asp:BoundField DataField="F_H_MARCA"
                    HeaderText="Fecha"
                    DataFormatString="{0:dd/MM/yyyy}" />

                <asp:BoundField DataField="F_H_MARCA"
                    HeaderText="Hora"
                    DataFormatString="{0:HH:mm:ss}" />

                <asp:BoundField DataField="TIPO_MARCA"
                    HeaderText="Marcación" />

            </Columns>

            <HeaderStyle CssClass="GridMarcacionesHeader" />
            <RowStyle CssClass="GridMarcacionesRow" />
            <AlternatingRowStyle CssClass="GridMarcacionesAltRow" />

        </asp:GridView>
    </form>
</body>
</html>
