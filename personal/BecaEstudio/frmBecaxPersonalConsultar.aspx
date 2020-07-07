<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmBecaxPersonalConsultar.aspx.vb" Inherits="BecaEstudio_frmBecaxConvenio" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
        <link href="../../private/estilo.css?x=x" rel="stylesheet" type="text/css" />
        <!-- <link href="css/estilos.css" rel="stylesheet" type="text/css" /> -->
        
        <script type="text/javascript" language="JavaScript" src="../../private/funciones.js"></script>
        <script type="text/javascript" language="JavaScript" src="../../private/jq/jquery-1.4.2.min.js"></script>
        <script type="text/javascript" language="javascript" src="../../private/jq/jquery.mascara.js"></script> 
        <script type="text/javascript" language="JavaScript" src="../../private/PopCalendar.js"></script>
        <script type="text/javascript" language="javascript">

            function MarcarCursos(obj) {
                //asignar todos los controles en array
                var arrChk = document.getElementsByTagName('input');
                for (var i = 0; i < arrChk.length; i++) {
                    var chk = arrChk[i];
                    //verificar si es Check
                    if (chk.type == "checkbox") {
                        chk.checked = obj.checked;
                        if (chk.id != obj.id) {
                            PintarFilaMarcada(chk.parentNode.parentNode, obj.checked)
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
    <div id="">
        <asp:Panel ID="pnlPrincipal" runat="server" Visible="true">
            <table style="width: 100%; class="contornotabla">
            <tr>
                <td bgcolor="#EFF3FB" height="35px">
                    <b>
                    <asp:Label ID="Label11" runat="server" 
                        Text="Solicitudes de Becas por Personal USAT"></asp:Label></b>
                </td>
            </tr>
            <tr>
                <td align="left" height="10px" class="contornotabla" ;>
                    
                </td>
            </tr>
            <tr>
                <td>
                    <table style="width: 100%" class="">
                        <tr>
                            <td style="width:160px">
                                <asp:Label ID="Label1" runat="server" Text="Semestre Académico"></asp:Label>
                            </td>                
                            <td style="width:160px">
                                <asp:DropDownList ID="ddlCiclo" Width="100%" runat="server" AutoPostBack="True"></asp:DropDownList>
                            </td> 
                             <td align="right">
                                 &nbsp;</td>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr>
                             <td>
                            </td>
                            <td>
                                &nbsp;</td>
                             <td style="width:160px" align="right">
                                 &nbsp;</td>
                            <td>
                                &nbsp;</td>
                        </tr>
                    </table>
                </td>
            
                
                
               
            </tr>
            <tr>
                <td bgcolor="#EFF3FB">
                    &nbsp;</td>
            </tr>
            <tr>
                 <asp:Panel ID="pnlDatosAlumno" Visible="false" runat="server">
                     <!-- Datos del Alumno -->
                            <td valign="top">
                    <table style="width: 100%" class="contornotabla">
                        <tr>
                            <td style="width:100px">
                                <asp:Label ID="Label10" runat="server" Text="Alumno"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblAlumno" runat="server" Font-Bold="False" ForeColor="Blue"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label6" runat="server" Text="Código Universitario"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblCodigoUniv" Font-Bold="False" ForeColor="Blue" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label7" runat="server" Text="Ciclo Ingreso"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblcicloingreso" Font-Bold="False" ForeColor="Blue" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label8" runat="server" Text="Escuela"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblEscuelaprofesional" Font-Bold="False" ForeColor="Blue" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label9" runat="server" Text="Plan Estudios"></asp:Label></td>
                            <td>
                                <asp:Label ID="lblPlanEstudios" Font-Bold="False" ForeColor="Blue" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
                <!--Requisitos -->
                            <td valign="top" style="width:50%; height="100%">
                                <table style="width: 100%; height:100%" class="contornotabla">
                                    <tr class="row-title">                  
                                        <td class="cell cell-3">Requisito</td>
                                        <td class="cell cell-3">Requerido</td>
                                        <td class="cell cell-4">Cumplimiento</td>
                                        <td class="cell cell-5"></td>                    
                                    </tr>                
                                    <div id="tb" runat="server"></div>
                                    <tr>
                                        <td><br />
                                            <div id="btn" runat="server"></div>
                                        </td>
                                    </tr>                
                               </table>   
                </td>
                 </asp:Panel> 
                <!-- :::::::::::::::::::::::::::::::::::::::::::::::: -->
                
                
                
            </tr>
            <tr>
                <td align="left">
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                     <asp:GridView ID="gvListaBecas" 
                         Width="100%" runat="server" BackColor="White" 
                         BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" 
                         AutoGenerateColumns="False" 
                         DataKeyNames="codigo_bso" 
                         EmptyDataText="No se encontraron registros...">
                         <RowStyle ForeColor="#000066" />
                         <EmptyDataRowStyle BackColor="#FFFFCC" BorderColor="#3399FF" Font-Bold="True" 
                             ForeColor="#3333CC" />
                         <Columns>
                             <asp:BoundField DataField="codigo_bso" HeaderText="ID" />
                             <asp:BoundField DataField="descripcion_bec" HeaderText="Tipo Beca" />
                             <asp:BoundField DataField="alumno" HeaderText="Estudiante USAT" />
                             <asp:BoundField DataField="porcentaje_bco" HeaderText="Beneficio" >
                                 <ItemStyle HorizontalAlign="Center" />
                             </asp:BoundField>
                             <asp:BoundField DataField="estado_bso" HeaderText="Estado" >                             
                                 <ItemStyle HorizontalAlign="Center" />
                             </asp:BoundField>
                             <asp:BoundField DataField="PersonalUSAT" HeaderText="PersonalUSAT" />
                         </Columns>
                         <FooterStyle BackColor="White" ForeColor="#000066" />
                         <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                         <SelectedRowStyle BackColor="#FFFFB1" Font-Bold="True" ForeColor="Blue" />
                         <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                    </asp:GridView>
                </td>
            </tr>
        </table>
        </asp:Panel>   
            
        <!--:::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::: -->
            
        
        
    </div>
    </form>
</body>
</html>
