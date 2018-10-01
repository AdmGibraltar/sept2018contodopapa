<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage02.Master" AutoEventWireup="true" 
CodeBehind="VentanaProtocolos.aspx.cs" Inherits="SIANWEB.VentanaProtocolos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPH" runat="server">

<div style="width:1020px;height:574px; line-height:3em;overflow : auto;  padding:5px;" >
<Table>
<tr>
<td>
    Guía de referencia | INCUMPLIMENTOS CAPTACION VENTA INSTALADA
</td>
</table>
<table width="1000px" cellspacing="0" 
        style="height: 700px; font-size: small;" border="1">
<body>
<tbody>
<tr>
<td width="450px"  colspan ="3">
</td>

<td colspan="3" style="background-color:#808080; font-weight: bold;" 
        align ="Center" width="450px">
Acciones
</td>
</tr>
<tr style="background-color:#808080; font-weight: bold;" align="center">
<%--<td width="150px">
Acción
</td>--%>
<td width="150px">
Causa
</td>
<td width="150px">
Detalle
</td>
<td width="150px">
RSC
</td>
<td width="150px">
Asesor
</td>
<td width="200px">
RIK
</td>

</tr>
<tr>
<%--<td rowspan="10" style="background-color:Yellow" align="Center" vertical-align="Middle">
Incumplimiento de captación de venta instalada
</td>--%>
<td>
Tiene stock de producto
</td>
<td>
Empleando incorrectamente el producto
</td>
<td>
Notificar al RIK
</td>
<td>
Recomendar capacitación 
</td>
<td>
Revisión de notas de manera semana a mensual del reporte de VI
</td>

</tr>
<tr bgcolor="#CCCCCC">
<td>
Tiene stock de producto
</td>
<td>
Tienen producto de la competencia
</td>
<td>
Indagar competidor y notificar al RIK
</td>
<td>
Indagar competidor y notificar al RIK
</td>
<td>
Revisión de notas de forma semana a mensual del reporte de VI
</td>

</tr>
<tr>
<td>
Baja en el consumo
</td>
<td>
Baja permanente
</td>
<td>
Avisar al RIK para modificar ACyS
</td>
<td>
Avisar al RIK para modificar ACyS
</td>
<td>
Revisión de notas de forma semana a mensual del reporte de VI
</td>

</tr>
<tr bgcolor="#CCCCCC">
<td>
Baja en el consumo
</td>
<td>
Baja puntual (temporal)
</td>
<td>
Avisar al RIK para monitoreo del consumo del cliente
</td>
<td>
Avisar al RIK para monitoreo del consumo del cliente
</td>
<td>
Revisión de notas de forma semana a mensual del reporte de VI
</td>

</tr>
<tr>
<td>
Falta de presupuesto
</td>
<td>
El cliente no tiene presupuesto para resurtir pedido
</td>
<td>
Avisar al RIK para que contacte al cliente y revisen el detalle del presupuesto
</td>
<td>
Avisar al RIK para que contacte al cliente y revisen el detalle del presupuesto
</td>
<td>
Revisión de notas de forma semana a mensual del reporte de VI
</td>
</tr>
<tr bgcolor="#CCCCCC">
<td>
Desinstalación de VI
</td>
<td>
El cliente ya no requiere nuestro producto / servicio
</td>
<td>
Notificar al RIK 
</td>
<td>
Notificar al RIK 
</td>
<td>
Visitar al cliente para renegociar, si se retiene al cliente validar si se modifica ACyS y en caso contrario capturar en sistema el motivo de la baja
</td>

</tr>
<tr>
<td>
Crédito Suspendido
</td>
<td>
El cliente tiene el crédito suspendido
</td>
<td>
Notificar al RIK 
</td>
<td>
Notificar al RIK 
</td>
<td>
Visitar al cliente para negociar fecha de pago
</td>

</tr>
<tr bgcolor="#CCCCCC">
<td>
Dosificadores en mal estado
</td>
<td>
Los dosificadores instalados con el cliente se encuentran en mal estado 
</td>
<td>
Notificar a Servicio Técnico
</td>
<td>
Notificar a Servicio Técnico
</td>
<td>
Revisión de notas de forma semana a mensual del reporte de VI
</td>
</tr>
<tr>
<td>
Alta en el consumo
</td>
<td>
Alta permanente
</td>
<td>
Avisar al RIK para modificar ACyS
</td>
<td>
Avisar al RIK para modificar ACyS
</td>
<td>
Revisión de notas de forma semana a mensual del reporte de VI
</td>
</tr>
<tr bgcolor="#CCCCCC">
<td>
Alta en el consumo
</td>
<td>
Alta puntual (temporal)
</td>
<td>
Avisar al RIK para monitoreo del consumo del cliente
</td>
<td>
Avisar al RIK para monitoreo del consumo del cliente
</td>
<td>
Revisión de notas de forma semana a mensual del reporte de VI
</td>
</tr>

<tbody>
</body>
</table>
</div>
</asp:Content>
