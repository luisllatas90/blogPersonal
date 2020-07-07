<%@ Page Language="VB" AutoEventWireup="false" CodeFile="AsignarEvaluador.aspx.vb" Inherits="_AsignarEvaluador" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript" language="JavaScript" src="../../private/jq/jquery-1.4.2.min.js"></script>
    <script type="text/javascript" language="javascript" src="../../private/jq/jquery.mascara.js"></script> 
    <script type="text/javascript" language="javascript">
        $(document).ready(function() {
	        jQuery(function($) {
	            $("#TxtFechaNac").mask("99/99/9999"); //.mask("(999)-999999");
	            //   $("#txttelefono").mask("(999)-9999999");
	            //   $("#txtcelular").mask("(999)-9999999");  
	        });

	    })
        function MarcarEvaluadores(obj)
        {
           //asignar todos los controles en array
            var arrChk = document.getElementsByTagName('input');
            for (var i = 0 ; i < arrChk.length ; i++){
                var chk = arrChk[i];
                //verificar si es Check
                if (chk.type == "checkbox"){
                    chk.checked = obj.checked;
                    if (chk.id!=obj.id){
                        PintarFilaMarcada(chk.parentNode.parentNode,obj.checked)
                    }
                }
            }
        }
      
         
        function PintarFilaMarcada(obj,estado)
        {
            if (estado==true){
                obj.style.backgroundColor="#FFE7B3"
            }
            else{
                obj.style.backgroundColor="white"
            }
        }
        
         if(top.location==self.location)
            //location.href='../../tiempofinalizado.asp'} //El ../ depende de la ruta de la página   
    </script>  
    <style type="text/css">
        body
        { font-family:Trebuchet MS;
          font-size:11px;
          cursor:hand;
         /* background-color:#F0F0F0;	*/
          padding:25px;
        }
        
        .celda1
        {
            width: 20%;
            background:white;
            padding:5px;
            border:1px solid #808080;
            border-right:0px;
            color:#2F4F4F;
            font-weight:bold;
            
        }
        .celda2
        {
            width: 80%;
            background:white;
            padding:5px;
            border:1px solid #808080;
            border-left:0px;
            color:#2F4F4F;
            font-weight:bold;
        }
       .celda3
       {    width: 80%;
            background:white;
            padding:5px;
            border:1px solid #808080;                  
            color:#2F4F4F;
            font-weight:bold;
       }
       
       #celdaGrid
       {
          color:#5D7B9D;   padding:5px;font-weight:bold; font-style:italic;
       }
      .celdaGrid
       {
          color:#5D7B9D;   padding:5px;font-weight:bold; font-style:italic;
       }
       .titulo
       { 
           font-weight:bold; font-size: 10px; 
       }
       .btn
       {
            border:1px solid #5D7B9D; 
            background:#F7F6F3; 
            font-family:Tahoma; 
            font-size:8pt; 
            font-weight:bold;  padding:3px;
            text-align: center;
        }
                
        .sinborderTop
        { border-top:0px;
            
            }
            
                .sinborderBottom
        { border-bottom:0px;
            
            }
    </style>
        
</head>
<body>

<form runat="server" name="frm">
<div>
    <h3>ASIGNACIÓN DE EVALUADORES</h3>
   <table cellpadding="0" cellspacing="0" >   
    <tr>      
        <td><b>Departamento Académico a Asignar :&nbsp; </b></td>
        <td>
        <asp:DropDownList ID="ddlDepartamentoAcademico" runat="server" AutoPostBack="True" 
                style="height: 22px"></asp:DropDownList>
        </td>
    </tr>
    <tr><td></td></tr>
    <tr>     
        <td><b>Departamento Académico del Evaluador:&nbsp; </b></td>
        <td>
        <asp:DropDownList ID="ddlDepartamentoAcadem" runat="server" AutoPostBack="True" 
                style="height: 22px"></asp:DropDownList>
        </td>
    </tr>
    <tr>      
        <td><b>Seleccionar Docente Evaluador :&nbsp; </b></td>
        <td>
        <asp:DropDownList ID="ddlDocente" runat="server" AutoPostBack="True"></asp:DropDownList>
            <br />
        </td>            
    </tr>
    <tr><td></td></tr>  
    <tr>     
        <td>
        <asp:Button ID="btnAsignarEvaluador" runat="server" 
                        Text="Asignar Evaluador" CssClass="btn"
                ToolTip="* Asigna un Evaluador a la lista.." Width="137px"  />
                </td>
        <td>
                <asp:Label ID="lblMensaje0" runat="server" ForeColor="Maroon" style="color: #CC0000"></asp:Label>
        </td>
     
    </tr>
    
    <tr><td><br /></td></tr>
    <tr>
    <td colspan="2" class="" >
    <asp:GridView ID="gvCarga" runat="server" AutoGenerateColumns="False" 
            DataKeyNames="codigo_evl" Width="100%" CellPadding="4" ForeColor="#333333">
            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
            <Columns>
                <asp:TemplateField>
                    <HeaderTemplate>
                               <asp:CheckBox ID="chkHeader" runat="server" onclick="MarcarEvaluadores(this)" />
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:CheckBox ID="CheckBox1" runat="server" ItemStyle-HorizontalAlign="left" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField HeaderText="#">
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </asp:BoundField>
                <asp:BoundField DataField="nombre_dac" HeaderText="Departamento Académico Asignado" />
                <asp:BoundField DataField="Personal" HeaderText="Docente Evaluador" />
                <asp:BoundField DataField="descripcion_Cac" HeaderText="Ciclo" 
                    ItemStyle-HorizontalAlign="Center" >
<ItemStyle HorizontalAlign="Center"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField DataField="fechaRegistro" HeaderText="Fecha Registro" />
                <asp:BoundField DataField="estado_evl" HeaderText="Estado" 
                    ItemStyle-HorizontalAlign="Center" >
<ItemStyle HorizontalAlign="Center"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField DataField="codigo_evl" HeaderText="codigo_evl" Visible="false"
                    ItemStyle-HorizontalAlign="Center" >
<ItemStyle HorizontalAlign="Center"></ItemStyle>
                </asp:BoundField>
            </Columns>
            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
            <EditRowStyle BackColor="#999999" />
            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
            
        </asp:GridView>
        <p runat="server" id ="celdaGrid" visible="false">
            <asp:Label ID="lblMensaje" runat="server" Text="Label"></asp:Label>
        </p>
    </td>
    <td class="" >
        &nbsp;</td>
    <td class="" >
        &nbsp;</td>
    </tr>
    <tr><td>
        <asp:Button ID="btnCambiarEstado" runat="server" Text="Cambiar Estado" 
            CssClass="btn" Width="118px"/>
        <br /></td></tr>
   </table>
    </div>
</form>
</body>
</html>
