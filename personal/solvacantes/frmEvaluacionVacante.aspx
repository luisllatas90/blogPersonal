<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmEvaluacionVacante.aspx.vb" Inherits="solvacantes_frmEvaluacionVacante" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Evaluación Vacante</title>
        <link href="../../private/estilo.css?x=1212" rel="stylesheet" type="text/css" />
        <script type="text/javascript" language="JavaScript" src="../../private/funciones.js"></script>
        <script type="text/javascript" language="JavaScript" src="../../private/jq/jquery-1.4.2.min.js"></script>
        <script type="text/javascript" language="javascript" src="../../private/jq/jquery.mascara.js"></script> 
        <script type="text/javascript" language="JavaScript" src="../../private/PopCalendar.js"></script>
    
    <style type="text/css">
        /* Estilos para los label ::: dguevara 13.11.2013 */
        .etiquetas
         {
                width:380px;
                height:15px;
                border:2px solid #006699;
                padding:10px;
                font-size:10px;
                /*font-weight:bold;*/
                /*background:#FFFFA8;*/
                /*color:#ffffff;*/
                display:block;  
                margin-top:1px;
                letter-spacing:1px;
            }
         .titulos
         {
                width:380px;
                height:15px;
                border:2px solid #FFFFA8;
                padding:10px;
                font-size:10px;
                font-weight:bold;
                background:#006699;
                color:#ffffff;
                display:block;  
                margin-top:1px;
                letter-spacing:1px;
            }
            
    </style>
    
    <script type="text/javascript" language="javascript">

        function MarcarCursos(obj) {
            //asignar todos los controles en array
            var arrChk = document.getElementsByTagName('input');
            for (var i = 0; i < arrChk.length; i++) {
                var chk = arrChk[i];
                //verificar si es Check
                if (chk.type == "checkbox") {
                    //** dguevara: 08.11.2013
                    //Bloque: parar marcar con check, siempre y cuando no este desabilitado.
                    if (chk.disabled ==false) {
                        chk.checked = obj.checked;  //este es para marcar los checks
                        //Bloque: para pintar la fila con el check.
                        if (chk.id != obj.id) {
                            PintarFilaMarcada(chk.parentNode.parentNode, obj.checked)
                        }
                    }
               }
            }
        }

        function PintarFilaMarcada(obj, estado) {
            if (estado == true) {
                obj.style.backgroundColor = "#FFE7B3"
            } else {
                obj.style.backgroundColor = "white"
            }
        }

	   
    </script>
    
    
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table style="width: 100%" class="contornotabla">
            <tr>
                <td bgcolor="#D1DDEF" colspan="7" height="10px">
                    <b>
                    <asp:Label ID="Label11" runat="server" Text="Evaluación Vacante"></asp:Label></b>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:HiddenField ID="HiddenField" runat="server" Value="0" />
                </td>
            </tr>
            <tr>
                <td style="width:10%">
                    <asp:Label ID="Label1" runat="server" Text="Ciclo Académico"></asp:Label>
                </td>
                <td style="width:10%" align="left">
                    <asp:DropDownList 
                            ID="ddlCicloAcademico" 
                            SkinID="ComboObligatorio" 
                            Width="100px"
                            runat="server" AutoPostBack="True">
                    </asp:DropDownList>
                </td>
                <td align="right" style="width:5%">
                    <asp:Label ID="Label10" runat="server" Text="Estado"></asp:Label>
                </td>
                <td align="left" style="width:10%" >
                    <asp:DropDownList ID="ddlEstado" runat="server" Width="100px" AutoPostBack="True">
                        <asp:ListItem Value="T">--TODOS--</asp:ListItem>
                        <asp:ListItem Value="P">PENDIENTES</asp:ListItem>
                        <asp:ListItem Value="A">APROBADOS</asp:ListItem>
                        <asp:ListItem Value="R">RECHAZADOS</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td style="width:5%">
                    <asp:Label ID="Label2" runat="server" Text="Departamento"></asp:Label>
                </td>
                <td style="width:50%">
                     <asp:DropDownList 
                            ID="ddlDepartamento" 
                            SkinID="ComboObligatorio" 
                            Width="60%"
                            runat="server" AutoPostBack="True">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td colspan="7">
                    <table style="width: 100%" class="contornotabla">
                        <tr>
                            <td style="width:10%" >
                                <asp:Button 
                                ID="btnAprobar" 
                                CssClass="aprobarsolvac" ToolTip="Permite aprobar un determinada solictud vacante." 
                                runat="server" Height="25px"  Width="100px"  
                                Text=" Aprobar" />
                            </td>
                            <td valign="middle" style="width:10%">
                                <asp:Button 
                                ID="btnRechazar" 
                                CssClass="rechazarsolvac" ToolTip="Permite rechazar alguna solictud vacante." 
                                runat="server" Height="25px" Width="100px"    
                                Text="     Rechazar" />
                            </td style="width:10%">
                            <td valign="middle" style="width:10%">
                                  <asp:Button 
                                ID="btnLimpiar" 
                                CssClass="limpiarsolvac" 
                                ToolTip="Limpia toda seleccion o ingreso de datos en los controles." 
                                runat="server" Height="25px" Width="100px"  
                                Text="     Cancelar" />
                            </td>
                            <td valign="middle" style="width:10%">
                                <asp:Button 
                                    ID="btnExportarExcel" 
                                    ToolTip="Permite Exportar la Información Filtrada a un formáto Excel.xls" 
                                    CssClass="excelsolvac"  Height="25px" Width="100px"    
                                    runat="server" 
                                    Text="   Exportar" />
                            </td>
                            <td valign="middle" style="width:10%">
                            </td>
                            <td align="right" valign="middle" style="width:15%">
                               <asp:Image ID="imgAprobadas" ImageUrl="../../images/solicitudvacantes/solArpobado.png" runat="server" />          
                               <asp:Label ID="lblnumAprobados" runat="server" Text=""></asp:Label>
                            </td>
                            <td align="right" valign="middle" style="width:15%">
                                <asp:Image ID="Image3" ImageUrl="../../images/solicitudvacantes/solRechazado.png" runat="server" />         
                                <asp:Label ID="lblnumRechazados" runat="server" Text=""></asp:Label>
                            </td>
                           <td align="right" valign="middle" style="width:15%">
                                <asp:Image ID="Image4" ImageUrl="../../images/solicitudvacantes/solPendiente.png" runat="server" />         
                                <asp:Label ID="lblnumPendientes" runat="server" Text=""></asp:Label>
                            </td>
                         </tr>
                         <tr>
                            <td colspan="8" height="15px">
                                <asp:Label  
                                            ID="lblCalificar" 
                                            runat="server" 
                                            Text="<b>Instrucciones</b>: Para <font color='blue'><b>aprobar</b></font> o <font color='red'><b>rechazar</b></font> una o varias solicitudes debe marcar con check.">
                                </asp:Label>
                                <asp:Image ID="Image1" runat="server" ImageUrl="../../Images/solicitudvacantes/solcheck.png" />
                            </td>
                        </tr>
                     </table>  
                </td>
            </tr>
            <!--
            <tr>
                <td colspan="4" style="height:15px"></td>
            </tr> -->
            <tr>
                <td align="left" colspan="7" class="contornotabla" style="background-color:#006699";>
                    <table style="width: 100%">
                        <tr>
                            <td align"left">
                                 <asp:Label ID="Label16"  runat="server" Text="" 
                                    Font-Bold="True" ForeColor="White"></asp:Label>
                            </td>
                            <td align="right">
                                <asp:Label ID="lblnumeroregistros"  runat="server" ForeColor="White" ></asp:Label>
                            </td>
                        </tr>
                    </table>
                   
                </td>
            </tr>
           
            <tr>
                <td class="contornotabla" colspan="7" style="width:100%; background-color:#006699;">
                    <asp:GridView ID="gvLista" Width="100%" runat="server" BackColor="White" 
                        BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" 
                        AutoGenerateColumns="False" EmptyDataText="No se encontraron registros.." 
                        AllowPaging="True" PageSize="6">
                        <RowStyle ForeColor="#000066" />
                        <EmptyDataRowStyle BackColor="#FFFF99" ForeColor="#FF3300" />
                        <Columns>
                            <asp:BoundField HeaderText="#" >
                                <ItemStyle Width="15px" />
                            </asp:BoundField>
                            
                            <asp:BoundField DataField="Codigo" HeaderText="ID" />
                            <asp:BoundField DataField="Docente" HeaderText="Apellidos Nombres / Vacante" />
                            <asp:BoundField DataField="nombre_Dac" HeaderText="Departamento" />
                            <asp:BoundField DataField="descripcion_Cco" HeaderText="Centro Costo" />
                            <asp:BoundField DataField="EstadoRev_svac" HeaderText="Estado" />
                            
                            <asp:TemplateField>
                                    <HeaderTemplate>
                                        <asp:CheckBox ID="chkHeader" runat="server"  onclick="MarcarCursos(this)" />
                                    </HeaderTemplate>
                                    <ItemTemplate>                
                                        <asp:CheckBox ID="chkElegir" runat="server" />
                                    </ItemTemplate>
                                    <ItemStyle Width="5px" />
                            </asp:TemplateField>
                            <asp:CommandField SelectText="" ShowSelectButton="True" />
                        </Columns>
                        
                        <FooterStyle BackColor="White" ForeColor="#000066" />
                        <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                        <SelectedRowStyle BackColor="#FFFFB1" Font-Bold="True" ForeColor="Blue" />
                        <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                    </asp:GridView>   
                </td>
            </tr>
            
            <!--Tabs para el Menu -->
            <tr>
                <td colspan="7">
                        <asp:Menu
                                    ID="Menu1"
                                    runat="server"
                                    Orientation="Horizontal"
                                    StaticEnableDefaultPopOutImage="False"
                                    OnMenuItemClick="Menu1_MenuItemClick">
                                <Items>
                                    <asp:MenuItem ImageUrl="../../images/solicitudvacantes/TagButtons/btnDatos.png" Text=" " Value="0" ToolTip="Muestra los datos de la vacante seleccionada."></asp:MenuItem>
                                    <asp:MenuItem ImageUrl="../../images/solicitudvacantes/TagButtons/btnComentarios.png" Text=" " Value="1"></asp:MenuItem>
                                    <asp:MenuItem ImageUrl="../../images/solicitudvacantes/TagButtons/btnCargaAcademica.png" Text=" " Value="2"></asp:MenuItem>
                                </Items>
                        </asp:Menu>
                        <!--Panel para mostrar los tabs con contenido -->
                        <asp:Panel ID="pnlMenu" runat="server">
                            <asp:MultiView 
                            ID="MultiView1" 
                            ActiveViewIndex="0"
                            runat="server">
                            <!-- Tab para mostrar los datos de la vacante -->
                                <asp:View ID="Tab1" runat="server"  >
                                    <table style="
                                                    border: 2px solid #3366CC; 
                                                    width:100%; 
                                                    border-collapse: collapse;
                                                    font: 75%/1.5em arial, geneva, sans-serif;"
                                                    width="100%" height="100%" cellpadding=0 cellspacing=0>
                                                    
                                        
                                        <!-- ######################################################################################### -->
                                        <!--Este bloque solo se debe mostrar a las personas autorizadas de aprobar la solictud vacante -->
                                        <tr><td colspan="8" height="5px"></td></tr>
                                        <!--  -->
                                        <tr>
                                            <td colspan="8" height="5px">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width:10px"></td>
                                             <td>
                                                <asp:Label ID="Label3" CssClass="titulos" Width="110px" runat="server" Text="Nombre Vacante"></asp:Label>
                                            </td>
                                            <td style="width:1%"></td>
                                             <td>
                                                <asp:Label ID="lblnombrevacante"  Width="250px" CssClass="etiquetas" runat="server" Text=""></asp:Label>
                                            </td>
                                            <td style="width:100px">
                                                <asp:Label ID="Label4" runat="server" CssClass="titulos"  Text="Dedicación" Width="110px"></asp:Label>
                                            </td>
                                           <td style="width:1%">
                                            </td>   
                                            <td>
                                                <asp:Label ID="lblDedicacion" runat="server" CssClass="etiquetas" Text="" Width="250px"></asp:Label>
                                            </td>
                                            <td style="width:10px">
                                            </td> 
                                        </tr>
                                        <tr>
                                            <td style="width:10px"></td>
                                            <td>
                                                <asp:Label ID="Label5" CssClass="titulos"  Width="110px" runat="server" Text="Departamento Académico"></asp:Label>
                                            </td>
                                            <td style="width:1%"></td>
                                            <td>
                                                <asp:Label ID="lblDpto" CssClass="etiquetas" Width="250px" runat="server" Text=""></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label6"  CssClass="titulos"  Width="110px" runat="server" Text="Centro Costo  "></asp:Label>
                                            </td>
                                            <td style="width:1%"></td>
                                            <td>
                                                <asp:Label ID="lblCeco" CssClass="etiquetas" Width="250px" runat="server" Text=""></asp:Label>
                                            </td>
                                            <td style="width:10px"></td>
                                        </tr>
                                        <!-- Aqui vamos a poner un panel, para mostrar los txt de acuerdo al tipo de dedicación -->
                                            <asp:Panel ID="pnlMenor20" Visible="false" runat="server">
                                            <tr>
                                               <td style="width:10px"></td>
                                                <td >
                                                    <asp:Label ID="Label7" CssClass="titulos" Width="110px" runat="server" Text="Precio Hora Propuesto"></asp:Label>
                                                </td>
                                                <td style="width:1%"></td>
                                                <td>
                                                    <asp:Label ID="lblPrecioHora"  CssClass="etiquetas" Width="250px" runat="server" Text=""></asp:Label>
                                                </td>
                                                <td></td>
                                                <td style="width:1%"></td>
                                                <td></td>
                                                <td style="width:10px"></td>
                                            </tr>
                                        </asp:Panel>
                                        <!-- Horas y precio -->
                                        <asp:Panel ID="pnlMayor20" Visible="false" runat="server">
                                            <tr>
                                               <td style="width:10px"></td>
                                                <td>
                                                    <asp:Label ID="Label9" runat="server" Width="110px" CssClass="titulos"  Text="Remuneración Propuesta"></asp:Label>
                                                </td>
                                                <td style="width:1%"></td>
                                                <td>
                                                    <asp:Label ID="lblRemuneracion" CssClass="etiquetas"  Width="250px" runat="server" Text=""></asp:Label>
                                                </td>
                                            </tr>
                                        </asp:Panel>
                                        <!--Fin panel Dedicaciones  -->
                                        
                                        <tr>
                                            <td style="width:10px"></td>
                                            <td>
                                                <asp:Label ID="Label13" runat="server" CssClass="titulos"  Width="110px" Text="H.Semanales Propuestas"></asp:Label>
                                            </td>
                                            <td style="width:1%"></td>
                                            <td>
                                                <!-- <asp:Label ID="lblFechaInicio" runat="server" Width="250px" CssClass="etiquetas" Text=""></asp:Label> -->
                                                <asp:Label ID="lblHorasSemanales" CssClass="etiquetas" Width="250px" runat="server" Text=""></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label14" runat="server" CssClass="titulos"  Width="110px" Text="Fechas"></asp:Label>
                                            </td>
                                            <td style="width:1%"></td>
                                            <td>
                                                <asp:Label ID="lblFechaFin" CssClass="etiquetas"  Width="250px" runat="server" Text=""></asp:Label>    
                                            </td>
                                            <td style="width:10px"></td>
                                        </tr>
                                        <tr>
                                            <td></td>
                                            <td style="width:110px" valign="top">
                                                <asp:Label ID="Label15" Width="110px" CssClass="titulos" runat="server" Text="Justificación"></asp:Label>
                                            </td>
                                            <td></td>
                                            <td colspan="4" style="width:675px">
                                            <asp:Label ID="lblJustificacion"  CssClass="etiquetas" Width="780px" Height="35%"  runat="server" Text=""></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="8" height="15px">
                                                <b>
                                            </td>
                                        </tr>
                                    </table>
                                    
                                </asp:View>
                            <!-- Fin datos vacante -->
                            
                            <!--&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&& -->
                            <!--&&&&&&&&&&&&&&&&&&& COMENTARIOS &&&&&&&&&&&&&&&&&&&&& -->
                                <asp:View ID="Tab2" runat="server">
                                  <table style="
                                                    border: 2px solid #3366CC; 
                                                    width:100%; 
                                                    border-collapse: collapse;
                                                    font: 75%/1.5em arial, geneva, sans-serif;"
                                                    width="100%" height="100%" cellpadding=0 cellspacing=0>
                                        
                                        <tr>
                                            <td colspan="4" align="center">
                                                <asp:Label ID="lblmensajecomentarios" Visible="false" runat="server" Text="Label"></asp:Label>
                                            </td>
                                        </tr>
                                        <!-- Bloque del DataList -->
                                            <tr>
                                            <td height="20px"></td>
                                            <td colspan="2" style="width:100%">
                                                <asp:DataList 
                                                    ID="DataList1" 
                                                    runat="server" 
                                                    BackColor="White" 
                                                    BorderColor="#999999" 
                                                    BorderStyle="None" 
                                                    BorderWidth="1px" 
                                                    CellPadding="3" 
                                                    RepeatColumns="1" 
                                                    Width="100%"
                                                    RepeatDirection="Horizontal" >
                                                    <ItemTemplate>
                                                        <table style="width: 98%" cellpadding="3" cellspacing="0">
                                                            <tr>
                                                                <td style="font-weight: normal; font-size: 7pt; color: white; font-family: verdana; background-color: #006699">
                                                                    <asp:Image ID="Image1" runat="server" ImageUrl="../../images/solicitudvacantes/solchat.png"/>
                                                                    <b><font color="orange"> &nbsp &nbsp AUTOR:</font></b>
                                                                    <asp:Label ID="Autor" runat="server" Text='<%# Eval("Autor") %>'></asp:Label>
                                                                    <b><font color="orange"> &nbsp &nbsp FECHA:
                                                                        <asp:Label ID="Label17" runat="server" Text='<%# Eval("fechareg_csv", "{0:d}") %>' ForeColor="white"></asp:Label>
                                                                    <b><font color="orange"> &nbsp &nbsp HORA:
                                                                        <asp:Label ID="lblHora" runat="server" Text='<%# Eval("hora", "{0:d}") %>' ForeColor="white"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="font-size: 8pt; font-family: verdana; background-color: #ffffa8; font-weight: bold; text-align: justify;">
                                                                    <!--Mensaje:<br />-->
                                                                    <asp:Label ID="Mensaje_csv" runat="server" Text='<%# Eval("Mensaje_csv") %>' Font-Bold="False"></asp:Label>
                                                                        <br />
                                                                        <br />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                        <!--<hr style="border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid;border-bottom: black 1px solid; height: 1px" />-->
                                                    </ItemTemplate>
                                                </asp:DataList>
                                            </td>
                                            <td style="width:5%"></td>
                                            </tr>
                                       <!-- fin datalist -->
                                       
                                       <tr>
                                            <!--<td height="20px" style="width:0.5%"></td>
                                            <td style="width:0.5%"></td>-->
                                            <td colspan="4" style="width:60%; height:20px; background-color:#006699" 
                                                class="contornotabla">
                                                <asp:Label ID="lblcomentario" runat="server" 
                                                    Text="     Ingrese el comentario para la solicitud seleccionada" 
                                                    Font-Bold="True" ForeColor="White"></asp:Label>
                                            </td>
                                        </tr>
                                        <!--  -->
                                         <tr>
                                            <td height="20px" style="width:1%"></td>
                                            <td style="width:1%"></td>
                                            <td style="width:60%">
                                                <br />
                                                <asp:TextBox 
                                                        ID="txtComentario" 
                                                        MaxLength="750" 
                                                        TextMode="MultiLine" Height="70px" Width="97%" 
                                                        runat="server">
                                                </asp:TextBox>
                                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator1" 
                                                    runat="server" 
                                                    ErrorMessage="Favor ingrese un comentario para la solicitud vacante seleccionada." 
                                                    ControlToValidate="txtComentario" 
                                                    ValidationGroup="grComentario" 
                                                    SetFocusOnError="true"
                                                    EnableClientScript="true"  
                                                    Text="*" >
                                            </asp:RequiredFieldValidator>
                                            </td>
                                            <td style="width:5%"></td>
                                        </tr>
                                         
                                         <tr>
                                            <td height="60px"></td>
                                            <td></td>
                                            <td  style="width:100%">
                                                <asp:Button 
                                                    ID="btnEnviar" 
                                                    Height="38px" ToolTip="Permite registrar un comentario para la solictud seleccionada."  
                                                    Width="120px" 
                                                    CssClass="comentariosolvac" 
                                                    runat="server" Text="   Enviar" 
                                                    ValidationGroup="grComentario" />
                                            </td>
                                            <td style="width:5%"></td>
                                        </tr>
                                        
                                             
                                   </table>
                                </asp:View>
                            <!--&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&& -->    
                            
                            <!--:::::: CARGA ACEDEMICA :::::::::::::::::::::::-->
                            <asp:View ID="Tab3" runat="server">
                                <table  style="border: 2px solid #3366CC;  width:100%; border-collapse: collapse;font: 75%/1.5em arial, geneva, sans-serif;"width="100%" height="100%" cellpadding=0 cellspacing=0>
                                    <tr><td colspan="4" height="5px"></td></tr>
                                    <!--KI-->
                                    <tr>
                                        <td></td>
                                        <td style="width:1%">
                                            <asp:Label ID="Label8" CssClass="titulos" Width="110px" runat="server" Text="Carga del Ciclo:"></asp:Label>
                                        </td>
                                        <td align="left">
                                            <asp:DropDownList ID="ddlCicloAcademicoCarga" AutoPostBack="true" Width="250px" runat="server">
                                            </asp:DropDownList>
                                        </td>
                                        <td style="width:10px"></td>
                                    </tr>
                                    <tr>
                                        <td style="width:10px"></td>
                                        <td colspan="2">
                                            <table style="width:100%">
                                                <tr>
                                                    <td>
                                                        <asp:GridView 
                                                                    ID="grwCargaAcademica" 
                                                                    runat="server" 
                                                                    AutoGenerateColumns="False" 
                                                                    Width="100%" 
                                                                    GridLines="Vertical" 
                                                                    AllowSorting="True" 
                                                                    BorderColor="#999999">
                                                    <EmptyDataRowStyle Font-Bold="True" ForeColor="Red" BorderStyle="None" BorderWidth="0px" />
                                                    <Columns>
                                                        <asp:BoundField DataField="docente" HeaderText="Profesor">
                                                            <ItemStyle Font-Size="7pt" Width="17%" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="dptoprofesor" HeaderText="Departamento" >
                                                            <ItemStyle Width="10%" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="descripcion_fun" HeaderText="Función">
                                                            <ItemStyle Font-Size="7pt" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="totalHorasAula" HeaderText="Hrs. Clase">
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="totalHorasAsesoria" HeaderText="Hrs. Asesoría">
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </asp:BoundField>
                                                        <asp:BoundField HeaderText="Total Hrs." DataField="totalHoras_Car" >
                                                            <ItemStyle HorizontalAlign="Center" Width="5%" />
                                                        </asp:BoundField>
                                                        <asp:TemplateField HeaderText="Curso">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblCurso" runat="server" Font-Italic="True" Text='<%# Bind("nombre_cur") %>'></asp:Label>
                                                                <br />
                                                                <asp:Label ID="lblDpto" runat="server" Text='<%# eval("dptocurso") %>'></asp:Label>.
                                                                <asp:Label ID="lblEscuela" runat="server" Text='<%# eval("abreviatura_cpf") %>'></asp:Label>
                                                                (<asp:Label ID="lblPlan" runat="server" Text='<%# eval("abreviatura_pes") %>'></asp:Label>)
                                                            </ItemTemplate>
                                                            <ItemStyle Width="25%" />
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="grupohor_cup" HeaderText="Grupo">
                                                            <ItemStyle HorizontalAlign="Center" Width="5%" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="vacantes_cup" HeaderText="Vacantes">
                                                            <ItemStyle HorizontalAlign="Center" Width="5%" />
                                                        </asp:BoundField>
                                                        <asp:TemplateField HeaderText="Inscritos">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblinscritos" runat="server" 
                                                                    Text='<%# eval("nroMatriculados_Cup") + eval("nroPreMatriculados_Cup") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Registrado:">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblFecha" runat="server" 
                                                                    Text='<%# eval("fechareg_car","{0:g}") %>'></asp:Label>
                                                                <br />
                                                                <asp:Label ID="lblLogin" runat="server" Text='<%# eval("login_per") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </asp:TemplateField>
                                                   </Columns>
                                                    <EmptyDataTemplate>
                                                        &nbsp;&nbsp;&nbsp;&nbsp; No se encontrarios cursos programados según los criterios seleccionados
                                                    </EmptyDataTemplate>
                                                    <HeaderStyle BackColor="#006699" ForeColor="White" BorderColor="#99BAE2" BorderStyle="Solid" BorderWidth="1px" />
                                                </asp:GridView>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td style="width:10px"></td>
                                    </tr>
                                </table>    
                            </asp:View>
                                
                        </asp:MultiView>
                        </asp:Panel>
                        
                        <!-- Panel para mostrar cuando no se ha seleccionado ningun registro -->
                        <asp:Panel ID="pnlSeleccion" runat="Server">
                            <table style="border: 2px solid #3366CC; width:100%; border-collapse: collapse;" width="100%" height="100%" cellpadding=0 cellspacing=0>
                                <tr><td style="height:80px; background-color:#FFFFB1;"></td></tr>
                                    <tr>
                                        <td style=" background-color:#FFFFB1;" align="center">
                                            <asp:Image ID="Image2" runat="server" ImageUrl="../../Images/solicitudvacantes/solRegistros.png" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <!-- height:250px; -->
                                        <td style=" background-color:#FFFFB1;" align="center">
                                            <asp:Label 
                                                ID="lblMensajeEntrada" 
                                                runat="server" 
                                                Font-Bold="True" 
                                                ForeColor="Blue">
                                            </asp:Label>
                                        </td>
                                    </tr>
                                <tr><td style="height:80px; background-color:#FFFFB1;"></td></tr>
                            </table>
                        </asp:Panel>
                </td>
            </tr>
            <tr>
                <td colspan="5">
                    <asp:ValidationSummary 
                    ID="ValidationSummary1" 
                    ValidationGroup="grComentario" 
                    ShowMessageBox="true" 
                    ShowSummary="false"
                    runat="server" />   
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
