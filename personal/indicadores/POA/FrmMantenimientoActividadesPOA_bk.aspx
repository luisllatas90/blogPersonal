<%@ Page Language="VB" AutoEventWireup="false" CodeFile="FrmMantenimientoActividadesPOA_bk.aspx.vb" Inherits="indicadores_POA_PROTOTIPOS_Registrar_POA" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script runat="server">

</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="css/estilo_poa.css" rel="stylesheet" type="text/css" media="screen" />
   
    <script src="../../../private/calendario.js" type="text/javascript"></script>
    
    <script type="text/javascript" language="javascript">
        function Valida_Presupuesto(sender, arguments) {
            egresos_act = document.getElementById("txtEgresos").value
            presupuesto_dis = document.getElementById("lbldisponible").innerHTML
            //Quito todas las Comas para Poder Comparar
            egresos_act = egresos_act.replace(/,/g, "")
            presupuesto_dis = presupuesto_dis.replace(/,/g, "")

            if (!/^[0-9]+([.][0-9]*)?$/.test(egresos_act)) {
                sender.errormessage = "Solo se admiten Números y el Punto Decimal en el Presupuesto."
                arguments.IsValid = false
            }
            else {
                /*
                if (parseFloat(egresos_act) > parseFloat(presupuesto_dis)) {
                sender.errormessage = "Presupuesto de Actividad : S/. " + document.getElementById("txtEgresos").value + " Debe Ser Menor a Presupuesto Disponible de POA : S/. " + document.getElementById("lbldisponible").innerHTML;
                arguments.IsValid = false;
                } else {*/
                arguments.IsValid = true;
                //}
            }

        }     
                    
//                    if (confirm("El Presupuesto de Programa/Proyecto : " + egresos_act + "\n es MAYOR que\n Presupuesto Disponible del POA : " + presupuesto_dis + "\n, Desea Continuar ?") == true) {

//                        arguments.IsValid = true;
//                    } else {
//                        arguments.IsValid = false;

                    //                    }

                    
                //}


       /* function ConfirmaExceso() {
            egresos_act = document.getElementById("txtEgresos").value
            presupuesto_dis = document.getElementById("lbldisponible").innerHTML
            alert(document.getElementById("ddlTipoActividad").value)
            alert(document.getElementById("ddlCeco").value)
            alert(document.getElementById("ddlProgPresupuestal").value)
            alert(document.getElementById("ddlResponsable").value)
            alert(document.getElementById("ddlApoyo").value)
            
            if (parseFloat(egresos_act) > parseFloat(presupuesto_dis)) {
                if (confirm("El Presupuesto de Programa/Proyecto : " + egresos_act + "\n es MAYOR que\n Presupuesto Disponible del POA : " + presupuesto_dis + "\n, Desea Continuar ?") == true)
                    return true;
                else
                    return false;
            } else { return true; }
        }*/




        function Valida_decimal_meta1(sender, arguments) {
            meta = document.getElementById("txt_meta1").value

            //Quito todas las Comas para Poder Comparar
            meta = meta.replace(/,/g, "")

            if (!/^([0-9]+(\.[0-9][0-9]?)?)*$/.test(meta)) {
                sender.errormessage = "Solo se admiten Números y el Punto Decimal en el Trimestre 1."
                arguments.IsValid = false
            }
         }

        function Valida_decimal_meta2(sender, arguments) {
            meta = document.getElementById("txt_meta2").value

            //Quito todas las Comas para Poder Comparar
            meta = meta.replace(/,/g, "")

            if (!/^([0-9]+(\.[0-9][0-9]?)?)*$/.test(meta)) {
                sender.errormessage = "Solo se admiten Números y el Punto Decimal en el Trimestre 2."
                arguments.IsValid = false
            }
         }
         function Valida_decimal_meta3(sender, arguments) {
             meta = document.getElementById("txt_meta3").value

             //Quito todas las Comas para Poder Comparar
             meta = meta.replace(/,/g, "")

             if (!/^([0-9]+(\.[0-9][0-9]?)?)*$/.test(meta)) {
                 sender.errormessage = "Solo se admiten Números y el Punto Decimal en el Trimestre 3."
                 arguments.IsValid = false
             }
         }

         function Valida_decimal_meta4(sender, arguments) {
             meta = document.getElementById("txt_meta4").value

             //Quito todas las Comas para Poder Comparar
             meta = meta.replace(/,/g, "")

             if (!/^([0-9]+(\.[0-9][0-9]?)?)*$/.test(meta)) {
                 sender.errormessage = "Solo se admiten Números y el Punto Decimal en el Trimestre 4."
                 arguments.IsValid = false
             }
         }


    </script>
   <style type="text/css" >
       .titulo_poa_w
        {
            font-family:'Helvetica Neue',Helvetica,Arial,sans-serif; 
            font-size:13px; 
            font-weight:bold;
            color:#337ab7; 
            padding-bottom:10px;
            padding-top:10px; 
            padding-left:5px;
            
        }
        
               .Caja_poa_Decimal
        {
            font-family:Verdana;
            font-size:8.5pt;
            text-align:right;
        }
        .contorno
        {
            border-width:1px;
            border-style:solid;
            border-color:#337ab7;
            border-top-width:0px; 

        }
        .nombre_pei
        {
            color:#d9534f;
            opacity:0.65;
            font-weight:bold;
            padding-top:3px;
            padding-bottom:3px;   
        }
        .nombre_poa
        {
            color:#468847;
            font-weight:bold;
            padding-top:3px;
            padding-bottom:3px;
        }
        .nombre_prog
        {
            color:#aa6708;
            font-weight:bold;
            padding-top:3px;
            padding-bottom:3px;
            text-decoration:underline;
           font-size:12px;
           font-style:italic;
           
        }
         .titulo_prog
        {
            color:#aa6708;
            font-weight:bold;
            padding-top:3px;
            padding-bottom:3px;
            text-decoration:underline;
            font-size:16px;
            font-weight:bold;
        }

        .AnchoImportes
        {
            width: 20px;
        }
        .AlineadoDerecha{
            text-align:right;
            font-family:Verdana;
            font-size:8.5pt;
            width: 100px;
        }     
       
         .Observacion
        {
            color:#aa6708;
            background-color:#f2dede;
            border: 1px solid #E9ABAB;
            padding:4px;
            height:auto;
            font-size:10px;

        }  
        .fecha
        {
            color:#088A85;
            opacity:0.65;
            font-weight:bold;
            padding-top:3px;
            padding-bottom:3px;   
        }
            
        .observado
        {
            color:#d9534f;
            opacity:0.65; 
            font-size:10px;
        }
        .resuelto
        {
            color:#468847;
            font-size:10.5px;
        }
        </style>
</head>
<body>
    <form id="form1" runat="server" >
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <table style="width:100%;" cellpadding="0px" cellspacing="0px">
    <tr style="height:35px;">
    <td runat="server" align="center" width="20%" ><asp:Button runat="server" ID="tab1" Text="PASO N° 01 : REGISTRO" Width="100%" Height="35px" CssClass="tab_activo" /></td>
    <td runat="server" align="center" width="20%" ><asp:Button runat="server" ID="tab2" Text="PASO N° 02 : ALINEAMIENTO" Width="100%" Height="35px" CssClass="tab_inactivo" /></td>
    <td runat="server" align="center" width="20%" ><asp:Button runat="server" ID="tab3" Text="PASO N° 03 : OBJETIVOS" Width="100%" Height="35px" CssClass="tab_inactivo" /></td>   
    <td runat="server" align="center" width="20%" ><asp:Button runat="server" ID="tab4" Text="PASO N° 04 : ACTIVIDADES" Width="100%" Height="35px" CssClass="tab_inactivo" /></td>
    <td runat="server" class="tab_opciones" align="center" id="tab5" style="border-top-width:0px; border-right-width:0px; background-color:#FFF">
        <span id="observar" runat="server" visible="false">
        <asp:ImageButton ID="btnObservar" runat="server"  src='../../Images/menus/noconforme_small.gif' height='15'/>
        <asp:LinkButton  ID="linkObservar" runat="server" Text="Observar" title='Observar' />
        </span>
        &nbsp;
        <span id="aprobar" runat="server" visible="false">
        <asp:ImageButton runat="server" ID="btnAprobar" ImageUrl='../../Images/Okey.gif' height='15' OnClientClick="return confirm('Esta Seguro que Desea Aprobar el Programa/Proyecto?.')" />
        <asp:LinkButton ID="linkAprobar" runat="server" Text="Aprobar" title='Aprobar' OnClientClick="return confirm('Esta Seguro que Desea Aprobar el Programa/Proyecto?.')" />
        </span>
    </td>
    </tr>
    <tr>
    <td colspan="5" class="contorno" >
    <%--Observaciones--%>
    <asp:Panel ID="PanelObservaciones" runat="server">
         <div runat="server" id="aviso_observaciones" style="padding:4px;">
             <div runat="server" id="lblobservaciones"></div>
         </div>
    </asp:Panel>
    <%--Observaciones--%>
    <asp:panel id="paso1" runat="server">
    <div class="titulo_poa_w">
        <asp:Label ID="Label1" runat="server"  Text="Registro de Programas y Proyectos : "></asp:Label>
        <asp:Label ID="lblNombreActividad" runat="server" Text="" class="titulo_prog"></asp:Label>
    </div>
    <div style="padding-left:4px">
    <table style="width:100%;">
            <tr>
                <td colspan="3" align="right">
                    <asp:Button ID="btnregresarp1_top" runat="server" CssClass="btnRegresar" 
                        Text="  Regresar" />
                    &nbsp;
                    <asp:Button ID="btnguardarp1_top" runat="server" CssClass="btnGuardar" 
                         Text="   Guardar y Continuar" Width="155px" ValidationGroup="Panel1" />
                </td>
            </tr>
            <tr>
                <td style="width:20%">
                <asp:HiddenField ID="hdcodigoPei" runat="server" Value="0" />
                <asp:HiddenField ID="hdcodigo_ejp" runat="server" Value="0" />
                <asp:HiddenField ID="hddescripcion_ejp" runat="server" Value="0" />
                <asp:HiddenField ID="hdcodigo_cco" runat="server" Value="0" />
                    Plan Estratégico</td>
                <td width="550px">
                    <div class="nombre_pei">
                    <asp:Label ID="lblPeiPaso1" runat="server"> </asp:Label>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                <asp:HiddenField ID="hdcodigoacp" runat="server" Value="0" />
                    Plan Operativo Anual</td>
                <td width="500px">
                    <div class="nombre_poa">
                    <asp:Label ID="lblPoaPaso1" runat="server"> </asp:Label>
                    </div>
                </td>
                <td></td>
            </tr>
            <div id="tabla_load" runat="server">
            <tr>
                <td>
                    Tipo de Actividad</td>
                <td>
                    <asp:DropDownList ID="ddlTipoActividad" runat="server" AutoPostBack="True" 
                        Width="150px">
                    </asp:DropDownList>
                    <asp:CompareValidator ID="CompareValidator2" runat="server" Operator="NotEqual"
                    ControlToValidate="ddlTipoActividad" ValueToCompare="0" ValidationGroup="Panel1"
                    ErrorMessage="Seleccionar Tipo de Actividad.">&nbsp;</asp:CompareValidator>
                    <asp:CompareValidator ID="CompareValidator5" runat="server" Operator="NotEqual"
                    ControlToValidate="ddlTipoActividad" ValueToCompare="0" ValidationGroup="Panel1_cco"
                    ErrorMessage="Seleccionar Tipo de Actividad.">&nbsp;</asp:CompareValidator>                    
                </td>
                <td></td>
            </tr>
            <tr>
                <td >
                    Centro de Costos</td>
                <td >
                    <asp:UpdatePanel runat="server">
                    <ContentTemplate>
                    <asp:DropDownList ID="ddlCeco" runat="server" AutoPostBack="true" Width="500px">
                    </asp:DropDownList>
                    <asp:CompareValidator ID="CompareValidator1" runat="server" Operator="NotEqual"
                    ControlToValidate="ddlceco" ValueToCompare="0" ValidationGroup="Panel1"
                    ErrorMessage="Seleccionar Centro De Costos.">&nbsp;</asp:CompareValidator>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="ddlTipoActividad" EventName="SelectedIndexChanged" />
                    </Triggers>
                    </asp:UpdatePanel>

                </td>
                <td>
                    <asp:Button ID="btnAdicionarCco" runat="server" CssClass="btnNuevo" Text="   Nuevo" />
                </td>
            </tr>
            <tr>
                <td >
                    Programa Presupuestal</td>
                <td>
                  <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                    <asp:DropDownList ID="ddlProgPresupuestal" runat="server"
                        Width="500px" Enabled="false" >
                    </asp:DropDownList>
                    <asp:CompareValidator ID="CompareValidator4" runat="server" Operator="NotEqual"
                    ControlToValidate="ddlProgPresupuestal" ValueToCompare="0" ValidationGroup="Panel1"
                    ErrorMessage="Seleccionar Programa Presupuestal.">&nbsp;</asp:CompareValidator>
                    </ContentTemplate>
                    <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="ddlCeco" EventName="SelectedIndexChanged" />
                    </Triggers>
                    </asp:UpdatePanel>
                </td>
                <td></td>
            </tr>
            <tr>
                <td>
                    Responsable</td>
                <td>
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                    <asp:DropDownList ID="ddlResponsable" runat="server" Width="500px">
                    </asp:DropDownList>
                    <asp:CompareValidator ID="CompareValidator3" runat="server" 
                        ControlToValidate="ddlResponsable" 
                        ErrorMessage="Seleccione Responsable de Actividad." Operator="NotEqual" 
                        ValidationGroup="Panel1" ValueToCompare="0">&nbsp;</asp:CompareValidator>
                </ContentTemplate>
                <Triggers>
                <asp:AsyncPostBackTrigger ControlID="ddlCeco" EventName="SelectedIndexChanged" />
                </Triggers>
                </asp:UpdatePanel>
                </td>
                <td></td>
            </tr>
            </div>
            <tr>
            <td colspan="3">
              <table ID="tabla_Cco" runat="server" style="border: 1px solid #FF0000;" visible="false" width="65%">
                <tr>
                    <td width="30%" style="font-weight:bold">
                        Tipo de Actividad</td>
                    <td><asp:Label runat="server"><b>PROYECTO</b></asp:Label></td>
                </tr>
                  <tr>
                      <td style="font-weight:bold" width="30%" class="style1">
                          Centro de Costos</td>
                      <td class="style1">
                          <asp:TextBox ID="txtdescripcion_cco" runat="server" CssClass="caja_poa" 
                              Width="500px" MaxLength="500"></asp:TextBox>
                          <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
                              ControlToValidate="txtdescripcion_cco" 
                              ErrorMessage="Ingrese Descripcion de Centro de Costo." 
                              ValidationGroup="Panel1_cco">&nbsp;</asp:RequiredFieldValidator>
                      </td>
                  </tr>
                <tr>
                    <td style="font-weight:bold">
                        Programa Presupuestal</td>
                    <td>
                        <asp:DropDownList ID="ddlprogpresupuestal_cco" runat="server" 
                            AutoPostBack="true" Width="500px" Enabled="false" >
                        </asp:DropDownList>
                        <asp:CompareValidator ID="CompareValidator6" runat="server" 
                            ControlToValidate="ddlprogpresupuestal_cco" 
                            ErrorMessage="Seleccionar Programa Presupuestal." Operator="NotEqual" 
                            ValidationGroup="Panel1_cco" ValueToCompare="0">&nbsp;</asp:CompareValidator>
                    </td>
                </tr>
                <tr>
                    <td style="font-weight:bold">
                        Responsable</td>
                    <td>
                        <asp:DropDownList ID="ddlresponsable_cco" runat="server" Width="500px">
                        </asp:DropDownList>
                        
                        <asp:CompareValidator ID="CompareValidator7" runat="server" 
                            ControlToValidate="ddlresponsable_cco" 
                            ErrorMessage="Seleccione Responsable de Actividad." Operator="NotEqual" 
                            ValidationGroup="Panel1_cco" ValueToCompare="0">&nbsp;</asp:CompareValidator>    
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td>
                        <div style="padding-left:310px">
                            <asp:Button ID="btnguardar_cco" runat="server" CssClass="btnGuardarCheck" 
                                Text="   Guardar" ValidationGroup="Panel1_cco"/>
                            <asp:Button ID="btncancelar_cco" runat="server" CssClass="btnCancelar" 
                                Text="  Cancelar" />
                        </div>
                        <asp:ValidationSummary ID="ValidationSummary2" runat="server" 
                            HeaderText="Errores" ShowMessageBox="False" ShowSummary="True" 
                            ValidationGroup="Panel1_cco" />
                    </td>
            </tr>
            </table>
            </tr>
            <tr>
                <td>
                    Personal de Apoyo (Presupuesto)</td>
                <td>
                    <asp:DropDownList ID="ddlApoyo" runat="server" Width="500px">
                    </asp:DropDownList>
                    <asp:CompareValidator ID="CompareValidator10" runat="server" 
                        ControlToValidate="ddlApoyo" 
                        ErrorMessage="Seleccione Personal de Apoyo de Actividad." Operator="NotEqual" 
                        ValidationGroup="Panel1" ValueToCompare="0">&nbsp;</asp:CompareValidator>                        
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                    Abreviatura</td>
                <td>
                <asp:UpdatePanel runat="server">
                    <ContentTemplate>
                    <asp:TextBox ID="txtabreviatura" runat="server" CssClass="caja_poa" 
                        MaxLength="10" Width="100px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                        ControlToValidate="txtabreviatura" ErrorMessage="Ingresar Abreviatura." 
                        ValidationGroup="Panel1">&nbsp;</asp:RequiredFieldValidator>
                    </ContentTemplate>
                    <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="ddlTipoActividad" EventName="SelectedIndexChanged" />
                    </Triggers>
                </asp:UpdatePanel>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td>
                <div class="nombre_prog">
                <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                <ContentTemplate>
                    <asp:Label ID="lbltipoactividadPaso1" runat="server" Text="Actividad"></asp:Label>
                </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="ddlTipoActividad" EventName="SelectedIndexChanged" />
                    </Triggers>
                </asp:UpdatePanel>
                
                </div>
                </td>
                <td>
                  <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                    <ContentTemplate>
                    <asp:TextBox ID="txtdescripcion" runat="server" CssClass="caja_poa" Width="500px" MaxLength="200"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                        ControlToValidate="txtdescripcion" 
                        ErrorMessage="Ingrese Descripcion de Actividad." ValidationGroup="Panel1">&nbsp;</asp:RequiredFieldValidator>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="ddlCeco" EventName="SelectedIndexChanged" />
                    </Triggers>
                  </asp:UpdatePanel>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td>
                    Inicio (Mes)&nbsp;</td>
                <td>
                <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                    <ContentTemplate>
                    <asp:DropDownList ID="ddlMesInicio" runat="server" AutoPostBack="true" Width="100px">
                        <asp:ListItem value="0">--Seleccione--</asp:ListItem>
                        <asp:ListItem value="1">Enero</asp:ListItem>
                        <asp:ListItem value="2">Febrero</asp:ListItem>
                        <asp:ListItem value="3">Marzo</asp:ListItem>
                        <asp:ListItem value="4">Abril</asp:ListItem>
                        <asp:ListItem value="5">Mayo</asp:ListItem>
                        <asp:ListItem value="6">Junio</asp:ListItem>
                        <asp:ListItem value="7">Julio</asp:ListItem>
                        <asp:ListItem value="8">Agosto</asp:ListItem>
                        <asp:ListItem value="9">Septiembre</asp:ListItem>
                        <asp:ListItem value="10">Octubre</asp:ListItem>
                        <asp:ListItem value="11">Noviembre</asp:ListItem>
                        <asp:ListItem value="12">Diciembre</asp:ListItem>
                    </asp:DropDownList>
                    <asp:CompareValidator ID="CompareValidator8" runat="server" Operator="NotEqual"
                    ControlToValidate="ddlMesInicio" ValueToCompare="0" ValidationGroup="Panel1"
                    ErrorMessage="Seleccionar Mes de Inicio.">&nbsp;</asp:CompareValidator>
                    </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="ddlTipoActividad" EventName="SelectedIndexChanged" />
                </Triggers>
                </asp:UpdatePanel>
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                    Fin (Mes)</td>
                <td>
                    <asp:UpdatePanel runat="server">
                    <ContentTemplate>
                        <asp:DropDownList ID="ddlMesFin" runat="server" Width="100px">
                            <asp:ListItem value="0">--Seleccione--</asp:ListItem>
                        </asp:DropDownList>
                        <asp:CompareValidator ID="CompareValidator9" runat="server" Operator="NotEqual"
                        ControlToValidate="ddlMesFin" ValueToCompare="0" ValidationGroup="Panel1"
                        ErrorMessage="Seleccionar Mes de Finalización.">&nbsp;</asp:CompareValidator>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="ddlMesInicio" EventName="SelectedIndexChanged" />
                        <asp:AsyncPostBackTrigger ControlID="ddlTipoActividad" EventName="SelectedIndexChanged" />
                    </Triggers>
                    </asp:UpdatePanel>

                </td>
                <td>&nbsp;</td>
            </tr>
            <tr style="height:20px;">
                <td>
                    Ingresos - POA</td>
                <td>
                    <asp:Label ID="Label7" runat="server" Font-Bold="True" ForeColor="#3871b0" 
                        Text="S/."></asp:Label>
                    <asp:Label ID="lblingresospoa" runat="server" CssClass="Caja_poa_Decimal" 
                        Font-Bold="True" ForeColor="#3871b0" Text="0.00" Width="100"></asp:Label>
                   &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    ACUMULADO &nbsp;&nbsp;&nbsp;
                    <asp:Label ID="Label2" runat="server" Font-Bold="True" ForeColor="#003366" 
                        Text="S/."></asp:Label>
                    <asp:Label ID="lblacumulado" runat="server" Font-Bold="True" 
                        ForeColor="#003366" Text="0.00" Width="100" CssClass="Caja_poa_Decimal"></asp:Label>                   
                </td>
                <td>    
                </td>
            </tr>
            <tr style="height:20px;">
                <td>
                    Egresos - POA
                </td>
                <td>
                    <asp:Label ID="Label5" runat="server" Font-Bold="True" ForeColor="#FF5050" 
                        Text="S/."></asp:Label>
                    <asp:Label ID="lblpresupuestopoa" runat="server" CssClass="Caja_poa_Decimal" 
                        Font-Bold="True" ForeColor="#FF5050" Text="0.00" Width="100"></asp:Label>
                   &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    DISPONIBLE &nbsp;&nbsp;&nbsp;
                    <asp:Label ID="Label6" runat="server" Font-Bold="True" ForeColor="#339966" 
                        Text="S/."></asp:Label>
                    <asp:Label ID="lbldisponible" runat="server" Font-Bold="True" 
                        ForeColor="#339966" Text="0.00" Width="100" CssClass="Caja_poa_Decimal"></asp:Label>
                    <asp:Label ID="lblexcedido" runat="server" Visible="false" ForeColor="Red" 
                     Font-Bold="true" Text=" (EXCEDIDO)"></asp:Label>
                </td>
                <td>
                </td>
            </tr>
            <tr style="height:20px;">
                <td style="color:#aa6708;font-weight:bold;">
                    Ingresos de Actividad</td>
                <td>
                <asp:updatepanel ID="Updatepanel5" runat="server">
                <ContentTemplate>
                    <asp:TextBox ID="txtIngresos" runat="server" AutoPostBack="true" 
                        CssClass="Caja_poa_Decimal" Text="0.00" ValidationGroup="Panel1" Width="120px"></asp:TextBox>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="txtEgresos" EventName="TextChanged" />
                </Triggers>
                </asp:updatepanel>
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td style="color:#aa6708;font-weight:bold;">
                    Egresos de Actividad</td>
                <td>
                <asp:updatepanel ID="Updatepanel4" runat="server">
                <ContentTemplate>
                    <asp:TextBox ID="txtEgresos" runat="server" CssClass="Caja_poa_Decimal" Text="0.00" 
                        ValidationGroup="Panel1" Width="120px" AutoPostBack="true"></asp:TextBox>
<%--                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                        ControlToValidate="txtEgresos" 
                        ErrorMessage="Ingrese Egresos de Actividad." ValidationGroup="Panel1">&nbsp;</asp:RequiredFieldValidator>--%>
                    <asp:CustomValidator ID="CustomValidator1" runat="server" 
                        ClientValidationFunction="Valida_Presupuesto" 
                        ControlToValidate="txtEgresos" display="Dynamic" ErrorMessage="" 
                        ValidationGroup="Panel1">&nbsp;</asp:CustomValidator>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="txtEgresos" EventName="TextChanged" />
                </Triggers>
                </asp:updatepanel>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td>
                <asp:updatepanel ID="Updatepanel7" runat="server">
                <ContentTemplate>
                <asp:Label runat="server" ID="lblutilidadp1" Text=""></asp:Label>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="ddltipoactividad" EventName="SelectedIndexChanged" />
                </Triggers>
                </asp:updatepanel>
                
                </td>
                <td>
                <asp:updatepanel runat="server">
                <ContentTemplate>
                 <asp:TextBox ID="txtUtilidad" runat="server" CssClass="Caja_poa_Decimal" 
                        Text="0.00" ValidationGroup="Panel1" Width="120px" ReadOnly="true" Visible="false" ></asp:TextBox>
                 &nbsp;&nbsp; <asp:Label ID="lblutilidad_porcentaje" runat="server" Text="0"></asp:Label>
                 <asp:Label ID="Label4" runat="server" Text="%"></asp:Label>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="txtIngresos" EventName="TextChanged" />
                    <asp:AsyncPostBackTrigger ControlID="txtEgresos" EventName="TextChanged" />
                </Triggers>
                </asp:updatepanel>
                   
                </td>
                <td>
                    &nbsp;</td>
            </tr>
             <tr>
            <td colspan="3">
            <div runat="server" id="aviso">
            <asp:Label ID="lblmensajeDuplicidad" runat="server" Font-Bold="true"></asp:Label>
            </div>
                <asp:updatepanel ID="Updatepanel9" runat="server">
                <ContentTemplate>
                <div runat="server" id="aviso_cco_per">
                <asp:Label ID="lblmensaje_cco" runat="server" Font-Bold="true" Font-Size="11px"></asp:Label>    
                </div>
                </ContentTemplate>
                <Triggers>
                <asp:AsyncPostBackTrigger ControlID="txtIngresos" EventName="TextChanged" />
                <asp:AsyncPostBackTrigger ControlID="txtEgresos" EventName="TextChanged" />
                </Triggers>
                </asp:updatepanel>
            </td>
            </tr>
            <tr>
                <td colspan="3" align="right">
                    <asp:Button ID="cmdCancelarPaso1" runat="server" CssClass="btnRegresar" 
                        Text="  Regresar" />
                    &nbsp;
                    <asp:Button ID="cmdGuardarPaso1" runat="server" CssClass="btnGuardar" 
                        Text="   Guardar y Continuar" Width="155px"  ValidationGroup="Panel1" />
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" 
                        HeaderText="Errores" ShowMessageBox="False" ShowSummary="True" 
                        ValidationGroup="Panel1" />
                </td>
            </tr>
            </table>
            </div>
    </asp:panel>
    <asp:panel id="paso2" runat="server">
        <div class="titulo_poa_w">
            <asp:Label ID="lbltituloPaso2" runat="server" Text="Alineamiento de Actividad"></asp:Label>
        </div> 
        <div style="padding-left:4px">
        <table style="width:100%">
            <tr>
                <td colspan="2" align="right">
                    <asp:Button ID="btnregresarp2_top" runat="server" CssClass="btnRegresar" 
                        Text="  Regresar" />
                    &nbsp;
                    <asp:Button ID="btnguardarp2_top" runat="server" CssClass="btnGuardar" 
                         Text="   Guardar y Continuar" Width="155px" ValidationGroup="Panel1" />
                </td>
            </tr>
            <tr>
                <td>Plan Estratégico</td>
                <td width="500px">
                    <div class="nombre_pei">
                    <asp:Label ID="lblPeiPaso2" runat="server"> </asp:Label>
                    </div>
                </td>
            </tr>
            <tr>
                <td style="width:20%">
                    Plan Operativo Anual</td>
                <td width="500px">
                    <div class="nombre_poa">
                        <asp:Label ID="lblPoaPaso2" runat="server"> </asp:Label>
                    </div>
                </td>
            </tr>
            <tr>
                <td style="width:20%">
                <div class="nombre_prog">
                    <asp:Label ID="lbltipoactividadPaso2" runat="server" Text="Actividad"></asp:Label>
                </div>
                </td>
                <td width="600px">
                    <div class="nombre_prog">
                        <asp:Label ID="lblactividadPaso2" runat="server"> </asp:Label>
                    </div>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    Objetivos Estrategicos :</td>
            </tr>
            <tr>
                <td colspan="2" valign="top">
                    <div>
                        <asp:TreeView ID="ArbolPaso2" runat="server" ExpandDepth="0" 
                            Font-Size="XX-Small" MaxDataBindDepth="4" Width="300px">
                            <SelectedNodeStyle />
                            <Nodes>
                            <asp:TreeNode Text="nodo 1">
                                <asp:TreeNode ShowCheckBox="true" Checked="true" Text="sub nodo11"></asp:TreeNode>
                                <asp:TreeNode ShowCheckBox="true" Text="sub nodo12">
                                <asp:TreeNode ShowCheckBox="true"  Text="sub nodo121"></asp:TreeNode>
                                <asp:TreeNode ShowCheckBox="true" Checked="true" Text="sub nodo122"></asp:TreeNode>
                                </asp:TreeNode>
                                <asp:TreeNode ShowCheckBox="true" Text="sub nodo13"></asp:TreeNode>
                            </asp:TreeNode>
                            </Nodes>
                            <HoverNodeStyle CssClass="menuporelegir" />
                            <%--<SelectedNodeStyle CssClass="menuporelegir"  />--%>
                        </asp:TreeView>
                        <asp:ListBox ID="ListBox1" runat="server" Visible="false" ></asp:ListBox>
                    </div>
                </td>
            </tr>
            <tr>
                <td align="right" colspan="2">
                    <asp:Button ID="cmdCancelarPaso2" runat="server" CssClass="btnRegresar" 
                        Text="  Regresar" />
                    &nbsp;
                    <asp:Button ID="cmdGuardarPaso2" runat="server" CssClass="btnGuardar" 
                        Text="   Guardar y Continuar" Width="155px" ValidationGroup="Panel1" />
                </td>
            </tr>    
            </table> 
            </div>
            </asp:panel>
    <asp:panel id="paso3" runat="server">
            <div class="titulo_poa_w">
                <asp:Label ID="lbltituloPaso3" runat="server" Text="Objetivos e Indicadores de Actividad"></asp:Label>
            </div> 
            <div style="padding-left:4px">
            <table style="width:100%">
            <tr>
                <td align="right" colspan="3">
                <asp:Button ID="btnregresarp3_top" runat="server" CssClass="btnRegresar" 
                Text="  Regresar" />
                &nbsp;
                <asp:Button ID="btnguradarp3_top" runat="server" CssClass="btnGuardar" 
                Text="   Guardar y Continuar" Width="155px" ValidationGroup="Panel1" />
                </td>
            </tr>
             <tr>
                <td>Plan Estratégico</td>
                <td width="500px">
                    <div class="nombre_pei">
                    <asp:Label ID="lblPeiObjetivosPaso3" runat="server"> </asp:Label>
                    </div>
                </td>
                <td></td>
            </tr>
            <tr>
                <td style="width:20%">
                    Plan operativo Anual</td>
                <td width="500px">
                    <div class="nombre_poa">
                        <asp:Label ID="lblpoaPaso3" runat="server"> </asp:Label>
                    </div>
                </td>
                <td></td>
            </tr>
            <tr>
                <td style="width:20%">
                <div class="nombre_prog">
                    <asp:Label ID="lbltipoactividadPaso3" runat="server" Text="Actividad"></asp:Label>
                </div>
                </td>
                <td width="600px">
                    <div class="nombre_prog">
                        <asp:Label ID="lblactividadPaso3" runat="server"> </asp:Label>
                    </div>
                </td>
                <td></td>
            </tr>
            <tr><td colspan="2">
                <table ID="tabla_obj" runat="server" style="border: 1px solid #FF0000;" visible="false" width="900px">
                    <tr>
                        <td width="190px">
                            <asp:Label ID="lbl_objetivo" runat="server" Text="Objetivo" Visible="False"></asp:Label>
                        </td>
                        <td colspan="4">
                            <asp:TextBox ID="txt_p3_Objetivo" runat="server" CssClass="caja_poa" 
                                Visible="False" Width="600px" MaxLength="500"></asp:TextBox>
                            &nbsp;</td>
                       
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lbl_indicador" runat="server" Text="Indicador" Visible="False"></asp:Label>
                        </td>
                        <td colspan="4">
                            <asp:TextBox ID="txt_p3_Indicador" runat="server" CssClass="caja_poa" 
                                Visible="False" Width="600px" MaxLength="500"></asp:TextBox>
                            &nbsp;</td>
                                   
                    </tr>
                    <tr>
                        <td>
                            <asp:TextBox ID="txt_p3_codigo_pobj" runat="server" CssClass="caja_poa" 
                                Enabled="False" Width="16px" Visible="False"></asp:TextBox>
                        </td>
                        <td>
                            <asp:Label ID="lbl_trimestre1" runat="server" Text="Trimestre 1  ( % )" 
                                Visible="False"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txt_meta1" runat="server" CssClass="AlineadoDerecha" 
                                Visible="False" ></asp:TextBox>
                                
                                <asp:CustomValidator ID="CustomValidator2" runat="server" 
                        ClientValidationFunction="Valida_decimal_meta1" 
                        ControlToValidate="txt_meta1" display="Dynamic" ErrorMessage="" 
                        ValidationGroup="PanelObj">&nbsp;</asp:CustomValidator>
                        </td>
                        
                        <td>
                            <asp:Label ID="lbl_trimestre2" runat="server" Text="Trimestre 2  ( % )" 
                                Visible="False"></asp:Label>

                        </td>
                        <td>
                            <asp:TextBox ID="txt_meta2" runat="server" CssClass="AlineadoDerecha" 
                                Visible="False" Enabled="False"></asp:TextBox>
                        <asp:CustomValidator ID="CustomValidator3" runat="server" 
                        ClientValidationFunction="Valida_decimal_meta2" 
                        ControlToValidate="txt_meta2" display="Dynamic" ErrorMessage="" 
                        ValidationGroup="PanelObj">&nbsp;</asp:CustomValidator>                                
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td>
                            <asp:Label ID="lbl_trimestre3" runat="server" Text="Trimestre 3  ( % )" 
                                Visible="False"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txt_meta3" runat="server" CssClass="AlineadoDerecha"></asp:TextBox>
                                
                                <asp:CustomValidator ID="CustomValidator4" runat="server" 
                        ClientValidationFunction="Valida_decimal_meta3" 
                        ControlToValidate="txt_meta1" display="Dynamic" ErrorMessage="" 
                        ValidationGroup="PanelObj">&nbsp;</asp:CustomValidator>
                        </td>
                        
                        <td>
                            <asp:Label ID="lbl_trimestre4" runat="server" Text="Trimestre 4  ( % )" 
                                Visible="False"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txt_meta4" runat="server" CssClass="AlineadoDerecha" 
                                Enabled="False"></asp:TextBox>
                                <asp:CustomValidator ID="CustomValidator5" runat="server" 
                        ClientValidationFunction="Valida_decimal_meta4" 
                        ControlToValidate="txt_meta1" display="Dynamic" ErrorMessage="" 
                        ValidationGroup="PanelObj">&nbsp;</asp:CustomValidator>
                        </td>                        
                    </tr>
                    
                    <tr>
                        <td colspan="5" style="text-align: right">
                             <asp:ValidationSummary ID="ValidationSummary4" runat="server" 
                            HeaderText="Errores" ShowMessageBox="False" ShowSummary="True" 
                            ValidationGroup="PanelObj" />
                        </td>
                    </tr>
                    
                    <tr>
                        <td colspan="5" style="text-align: right">
                            &nbsp;<asp:Button ID="btn_p3_AgregarIndicador" runat="server" CssClass="btnGuardarCheck" 
                                Text="   Agregar Indicador" Visible="False" ValidationGroup="PanelObj" />
                            <asp:Button ID="btn_p3_CancelarIndicador" runat="server" CssClass="btnRegresar" 
                                Text="  Cancelar" Visible="False" />
                            &nbsp;
                            <asp:Button ID="btn_p3_AgregarObjetivo" runat="server" CssClass="btnGuardarCheck" Width="200px"
                                Text="   Agregar Objetivo/Indicador" Visible="False" ValidationGroup="PanelObj" />
                            <asp:Button ID="btn_p3_CancelarObjetivo" runat="server" CssClass="btnCancelar" 
                                Text="  Cancelar" Visible="False" />
                        </td>
                    </tr>
                </table>
                </td></tr>
                <tr>
                    <td colspan="3">
                        <asp:Button ID="btnNuevo" runat="server" CssClass="btnNuevo"
                            Text="   Nuevo Objetivo"  />
                        &nbsp;&nbsp;
                        <asp:Button ID="btnNuevoIndicador" runat="server" CssClass="btnNuevo" 
                           Text="   Nuevo Indicador" Visible="False" />
                        
                    </td>
                    
                </tr>
                <tr>
                    <td colspan="3">
                        <div id="p3_aviso" runat="server" ><asp:Label ID="lblmensaje_p3" runat="server" Text="" Font-Bold="True"></asp:Label>
                        </div>
                         
                    </td>
                </tr>
                <tr>
                    <td colspan="3">
                        <asp:GridView ID="dgw_p3_Objetivos" runat="server" AutoGenerateColumns="False" 
                            DataKeyNames="codigo_pobj,codigo_acp,codigo_pind,codmeta1,codmeta2,codmeta3,codmeta4" 
                            Width="100%">
                            <Columns>
                                <asp:BoundField DataField="objetivo" HeaderText="OBJETIVO" HtmlEncode="false" >
                                    <HeaderStyle Width="150px" />
                                    <ItemStyle BorderColor="DarkGray" BorderStyle="Solid" Width="200px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="indicador" HeaderText="INDICADORES" HtmlEncode="false">
                                    <HeaderStyle Width="150px" />
                                    <ItemStyle Width="200px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="meta1" HeaderText="META 1 (%)">
                                    <HeaderStyle HorizontalAlign="Right" Width="80px" />
                                    <ItemStyle HorizontalAlign="Right" Width="80px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="meta2" HeaderText="META 2 (%)">
                                    <HeaderStyle HorizontalAlign="Right" Width="80px" />
                                    <ItemStyle HorizontalAlign="Right" Width="80px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="meta3" HeaderText="META 3 (%)">
                                    <HeaderStyle HorizontalAlign="Right" Width="80px" />
                                    <ItemStyle HorizontalAlign="Right" Width="80px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="meta4" HeaderText="META 4 (%)">
                                    <HeaderStyle HorizontalAlign="Right" Width="80px" />
                                    <ItemStyle HorizontalAlign="Right" Width="80px" />
                                </asp:BoundField>
                                <asp:CommandField ButtonType="Image" HeaderText="EDITAR" 
                                    SelectImageUrl="../../../images/editar_poa.png"  ShowSelectButton="True">
                                    <HeaderStyle HorizontalAlign="Center" Width="60px" />
                                    <ItemStyle HorizontalAlign="Center" Width="60px" />
                                </asp:CommandField>
                                <asp:TemplateField HeaderText="ELIMINAR INDICADOR" ShowHeader="False">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="EliminarIndicador1" runat="server" 
                                            CausesValidation="False" CommandName="Delete" 
                                            ImageUrl="../../images/eliminar.gif" OnClick="ibtnEliminaIndicador_Click" 
                                            OnClientClick="return confirm('¿Desea Eliminar el Indicador?.')" 
                                            Text="Eliminar" />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="80px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="ELIMINAR OBJETIVO" ShowHeader="False">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="EliminarObjetivo1" runat="server" CausesValidation="False" 
                                            CommandName="Delete" ImageUrl="../../Images/menus/noconforme_small.gif" 
                                            OnClick="ibtnEliminaObjetivo_Click" 
                                            OnClientClick="return confirm('¿Desea Eliminar el Objetivo?.')" 
                                            Text="Eliminar" />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="60px" />
                                </asp:TemplateField>
                                <asp:BoundField DataField="codigo_pobj" HeaderText="codigo_pobj" 
                                    Visible="False" />
                                <asp:BoundField DataField="codigo_acp" HeaderText="codigo_acp" 
                                    Visible="False" />
                                <asp:BoundField DataField="codigo_pind" HeaderText="codigo_pind" 
                                    Visible="False" />
                                <asp:BoundField DataField="codmeta1" HeaderText="codmeta1" Visible="False">
                                    <ItemStyle Width="60px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="codmeta2" HeaderText="codmeta2" Visible="False">
                                    <ItemStyle Width="60px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="codmeta3" HeaderText="codmeta3" Visible="False">
                                    <ItemStyle Width="60px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="codmeta4" HeaderText="codmeta4" Visible="False">
                                    <ItemStyle Width="60px" />
                                </asp:BoundField>
                            </Columns>
                            <HeaderStyle BackColor="#3871b0" ForeColor="White" Height="25px" />
                            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                        </asp:GridView>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:HiddenField ID="hd_codigo_pind" runat="server" Value="0" />
                        <asp:HiddenField ID="hd_codigo_pobj" runat="server" Value="0" />
                        <asp:HiddenField ID="hdcodigo_meta1" runat="server" Value="0" />
                        <asp:HiddenField ID="hdcodigo_meta2" runat="server" Value="0" />
                        <asp:HiddenField ID="hdcodigo_meta3" runat="server" Value="0" />
                        <asp:HiddenField ID="hdcodigo_meta4" runat="server" Value="0" />
                        <asp:HiddenField ID="hdAccion" runat="server" Value="0" />
                        <br />
                    </td>
                        <td align="right" colspan="2">
                            <asp:Button ID="cmdCancelarPaso3" runat="server" CssClass="btnRegresar" 
                                Text="  Regresar" />
                            &nbsp;
                            <asp:Button ID="cmdGuardarPaso3" runat="server" CssClass="btnGuardar" 
                                Text="   Guardar y Continuar" Width="155px" ValidationGroup="Panel1" />
                      </td>
                </tr>
            </table>
        </div>
  
  
    </asp:panel>
    <asp:panel id="paso4" runat="server">
     <div class="titulo_poa_w">
          <asp:Label ID="lbltituloPaso4" runat="server" Text="Actividades de Programa o Proyecto"></asp:Label>
      </div> 
      <div style="padding-left:4px">
        <table style="width:100%">
          <tr>
            <td colspan="3" align="right" >
                &nbsp;
                
                <asp:Button ID="btnregresarp4_top" runat="server" CssClass="btnRegresar" 
                    Text="  Regresar" />
                &nbsp;
                <asp:Button ID="btnguardarp4_top" runat="server" CssClass="btnGuardar" 
                    Text="   Guardar y Finalizar" Width="150px" ValidationGroup="Panel1" />
            </td>
          </tr>

            <tr>
                <td class="style3">Plan Estratégico</td>
                <td><div class="nombre_pei"><asp:Label ID="lblPeiPaso4" runat="server"> </asp:Label></div></td>
                <td></td>
            </tr>
            
            <tr>
                <td class="style3">Plan Operativo Anual</td>
                <td><div class="nombre_poa"><asp:Label ID="lblPoaPaso4" runat="server"> </asp:Label></div></td>
                <td></td>
            </tr>
            
            <tr>
                <td class="style3" >
                    <div class="nombre_prog">
                    <asp:Label ID="lblTipoactividadPaso4" runat="server" Text="Actividad"></asp:Label></div>
                </td>
                <td><div class="nombre_prog"><asp:Label ID="lblactividadPaso4" runat="server"> </asp:Label></div></td>
                <td ></td>
            </tr>

            <tr>
                <td class="style3">Fecha de Inicio</td>
                <td><div class="fecha"><asp:Label ID="lblfecini" runat="server" Text="dd/mm/yyyy"></asp:Label></div></td>
                <td></td>
            </tr>
            
            <tr>
                <td class="style3">Fecha Fin</td>
                <td><div class="fecha"><asp:Label ID="lblfecfin" runat="server" Text="dd/mm/yyyy"></asp:Label></div></td>
                <td></td>
            </tr>            
                   
            <tr>
                <td colspan="3">
                <table ID="tabla_Act" runat="server" style="border: 1px solid #FF0000; margin-bottom: 0px;" visible="false" width="900px">
                    <tr>
                        <td>Descripción</td>
                        <td><asp:TextBox ID="txt_p4_descripcion" runat="server" CssClass="caja_poa" Width="725px" MaxLength="500"></asp:TextBox>
                            <asp:TextBox ID="txt_p4_meta" runat="server" CssClass="AlineadoDerecha" 
                                Visible="False" Width="21px">0.00</asp:TextBox>
                            <asp:TextBox ID="txt_p4_avance" runat="server" CssClass="AlineadoDerecha" 
                                Enabled="False" Visible="False" Width="16px">0.00</asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>Fecha Inicio</td>
                        <td>
                            <asp:TextBox ID="txt_p4_fecini" runat="server" Width="100px"></asp:TextBox>
                            <asp:Button ID="btnCalendario_ini" runat="server" Text="..." Width="20px" />
                        </td>
                    </tr>
                    
                    <tr>
                        <td></td>
                        <td>
                            <div id="divCalendario_ini" runat="server">
                                <asp:Calendar ID="Calendario_ini" runat="server" EnableTheming="True" >
                                <SelectedDayStyle BackColor="#FF6666" />
                                <TitleStyle Font-Bold="True" Font-Names="Arial Narrow" 
                                     />
                                </asp:Calendar>
                            </div>
                        </td>
                    </tr>                    
                    
                    
                    <tr>
                        <td>Fecha Fin</td>
                        <td>
                            <asp:TextBox ID="txt_p4_fecfin" runat="server" Width="100px"></asp:TextBox>
                            <asp:Button ID="btnCalendario_fin" runat="server" Text="..." Width="20px" />
                        </td>
                    </tr>
    
                    <tr>
                        <td></td>
                        <td>
                            <div id="divCalendario_fin" runat="server">
                                <asp:Calendar ID="Calendario_fin" runat="server" EnableTheming="True" >
                                <SelectedDayStyle BackColor="#FF6666" />
                                <TitleStyle Font-Bold="True" Font-Names="Arial Narrow" 
                                     />
                                </asp:Calendar>
                            </div>
                        </td>
                    </tr>   
                        
                    <tr>
                        <td>Responsable</td>
                        <td>
                            <asp:DropDownList ID="ddl_p4_responsable" runat="server" Width="353px">
                            </asp:DropDownList>
                        </td>
                    </tr>

                    <tr>
                        <td>Presupuesto</td>
                        <td>
                            <asp:CheckBox ID="chk_requiere_pto" runat="server" 
                                Text="Requiere de Presupuesto" />
                        </td>
                    </tr>
                                        
                    <tr>
                        <td colspan="2"></td>
                    </tr>
                    
                    <tr>
                        <td colspan="2" style="text-align: right">
                            &nbsp;<asp:Button ID="btn_p4_agregarActividad" runat="server" CssClass="btnGuardarCheck"
                                Text="   Agregar Actividad" />
                            &nbsp;&nbsp;<asp:Button ID="btn_p4_cancelar" runat="server" CssClass="btnCancelar" 
                                Text="  Cancelar" />
                        </td>
                    </tr>
                </table>
                </td>

            </tr>
            
            <tr>
                <td colspan="3">
                    <asp:Button ID="btn_p4_NuevaActividad" runat="server" Text="   Nueva Actividad" 
                        CssClass="btnNuevo"  />
                </td>
            </tr>
            <tr>
                <td colspan="3">
                     <div id="p4_aviso" runat="server" >
                     <asp:Label ID="lblmensaje" runat="server" Font-Bold="True"></asp:Label>
                     </div>
                </td>
            </tr>
            
            <tr>
                <td colspan="3">
                    <asp:GridView ID="dgv_p4_detalle" runat="server" AutoGenerateColumns="False" 
                        DataKeyNames="codigo_dap,codigo_acp,responsable_dap" ShowFooter="True" 
                        Width="100%">
                        <Columns>
                            <asp:BoundField DataField="descripcion_dap" HeaderText="DESCRIPCION" 
                                HtmlEncode="false">
                                <ItemStyle Width="300px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="meta_dap" DataFormatString="{0:N}" HeaderText="META" 
                                Visible="False">
                                <ItemStyle HorizontalAlign="Right" Width="80px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="avance_dap" DataFormatString="{0:N}" 
                                HeaderText="AVANCE" Visible="False">
                                <ItemStyle HorizontalAlign="Right" Width="10px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="fecini_dap" HeaderText="F. INICIO">
                                <ItemStyle HorizontalAlign="Center" Width="80px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="fecfin_dap" HeaderText="F. FIN">
                                <ItemStyle HorizontalAlign="Center" Width="80px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="nombreresponsable_dap" HeaderText="RESPONSABLE">
                                <ItemStyle Width="200px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="requiere_pto" HeaderText="PRESUPUESTO REQUERIDO">
                                <ItemStyle HorizontalAlign="Center" Width="40px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="ingresos" DataFormatString="{0:C2}" 
                                HeaderText="INGRESOS">
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="egresos" DataFormatString="{0:C2}" 
                                HeaderText="EGRESOS">
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:CommandField ButtonType="Image" EditImageUrl="../../images/agregar1.gif" 
                                HeaderText="REG. PTO" ShowEditButton="true" Visible="False">
                                <HeaderStyle Width="6.6%" />
                                <ItemStyle HorizontalAlign="Center" Width="60px" />
                            </asp:CommandField>
                            <asp:CommandField ButtonType="Image" HeaderText="EDITAR" 
                                SelectImageUrl="../../../images/editar_poa.png" ShowSelectButton="True">
                                <HeaderStyle Width="6.6%" />
                                <ItemStyle HorizontalAlign="Center" Width="60px" />
                            </asp:CommandField>
                            <asp:TemplateField HeaderText="ELIMINAR" ShowHeader="False">
                                <ItemTemplate>
                                    <asp:ImageButton ID="ImageButton1" runat="server" CausesValidation="False" 
                                        CommandName="Delete" ImageUrl="../../Images/menus/noconforme_small.gif" 
                                        OnClientClick="return confirm('¿Desea Eliminar Registro?.')" Text="Eliminar" />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Width="60px" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="codigo_dap" HeaderText="codigo_dap" Visible="False">
                                <ItemStyle Width="80px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="codigo_acp" HeaderText="codigo_acp" Visible="False">
                                <ItemStyle Width="80px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="responsable_dap" HeaderText="responsable_dap" 
                                Visible="False" />
                            <asp:BoundField HeaderText="Mover A" Visible="false">
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                                
                        </Columns>
                        <HeaderStyle BackColor="#3871b0" ForeColor="White" Height="25px" />
                        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                    </asp:GridView>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:HiddenField ID="hdcodigo_dap" runat="server" Value="0" />
                    <asp:HiddenField ID="hd_indexGrid" runat="server" Value="0" />
                    <asp:HiddenField ID="hd_accion" runat="server" Value="0" />
                </td>
                <td></td>
                <td></td>
              </tr>
                <tr>
                    <td align="right" colspan="3">
                        &nbsp;
                        
                        <asp:Button ID="cmdCancelarPaso4" runat="server" CssClass="btnRegresar" 
                            Text="  Regresar" />
                        &nbsp;
                        <asp:Button ID="cmdGuardarPaso4" runat="server" CssClass="btnGuardar" 
                            Text="   Guardar y Finalizar" Width="150px" ValidationGroup="Panel1" />
                    </td>
                </tr>
            </tr>
            </table>
            </div>
            </asp:panel>
            <asp:Panel ID="PasoObservacion" runat="server" Visible="false">
            <div class="titulo_poa_w">
            <asp:Label ID="Label3" runat="server"  Text="Observación de Programa/Proyecto."></asp:Label>
            </div>
            <div style="padding-left:4px">
                <table width="100%">
                <tr>
                <td style="width:30%" align="right">Observación</td>
                <td style="width:60%">
                <asp:TextBox ID="txtobservacion" runat="server" TextMode="MultiLine" Columns="40" Rows="5" ValidationGroup="PanelObs" ></asp:TextBox>
                         <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" 
                        ControlToValidate="txtobservacion" 
                        ErrorMessage="Ingrese Descripcion de Observación." ValidationGroup="PanelObs">&nbsp;</asp:RequiredFieldValidator>
                </td>
                </tr>
                <tr>
                <td colspan="2">
                    <asp:ValidationSummary ID="ValidationSummary3" runat="server" 
                        HeaderText="Errores" ShowMessageBox="False" ShowSummary="True" 
                        ValidationGroup="PanelObs" />
                </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <div runat="server" id="aviso_obs">
                        <asp:Label ID="lblrpta_obs" runat="server" Font-Bold="true"></asp:Label>
                        </div>
                    </td> 
                </tr>
                <tr><td colspan="2" align="center">
                    <asp:Button ID="btnCancelarObs" runat="server" CssClass="btnCancelar" Text="  Cancelar" />
                        &nbsp;
                    <asp:Button ID="btnGuardarObs" runat="server" CssClass="btnGuardar" Text="   Guardar" ValidationGroup="PanelObs" />
                </td></tr>

                </table>
                </div>
            </asp:Panel>
      </td>
      </tr>
      </table>  
   </form>
</body>
</html>
