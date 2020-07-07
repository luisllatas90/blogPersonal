<%@ Page Language="VB" AutoEventWireup="false" CodeFile="AsignarObservador.aspx.vb" Inherits="_AsignarObservador" %>

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
        
        .texto    { font-family:Trebuchet MS;
          color:Black; 
          font-size:11px;
          cursor:hand;
         /* background-color:#F0F0F0;	*/
          padding:15px;
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
    <h3>ASIGNACIÓN DE OBSERVADORES</h3>
   <table cellpadding="0" cellspacing="0" >   
    <tr>      
        <td><b>Departamento Académico:&nbsp; </b></td>
        <td>
        <asp:DropDownList ID="ddlDepartamentoAcademico" runat="server" AutoPostBack="True" 
                style="height: 22px"></asp:DropDownList>
        <asp:DropDownList ID="ddlDepAcad" runat="server" AutoPostBack="True" 
                style="height: 22px" Width="52px" Visible="false"></asp:DropDownList>
        </td>
    </tr>
    <tr><td><b>Docente Evaluador:</b></td>
    <td> <asp:DropDownList ID="ddlEvaluador" runat="server" AutoPostBack="True" 
                style="height: 22px"></asp:DropDownList>
                <asp:Button ID="btnAsignar" runat="server" 
                        Text="Asignar" CssClass="btn"
                ToolTip="* Asigna un Evaluador a la lista.." Width="70px"  />
    </td>
    </tr>
    </table></div>
    
    <div><table cellpadding="1" cellspacing="1">
    <tr><td></td><td></td></tr>
    <tr><td><h4>Filtrar por:</h4></td><td></td></tr>
    <tr><td></td><td></td></tr>
    <tr>     
    <td align="left"><asp:TextBox ID="TextBox1" CssClass="texto" runat="server" 
            BorderWidth="0" Height="21px" Width="104px">Estado:
            </asp:TextBox><asp:DropDownList ID="ddlEstado" runat="server" AutoPostBack="True" 
                style="height: 22px">
                <asp:ListItem Value="T">TODOS</asp:ListItem>
                <asp:ListItem Value="P" Selected="True">Por Asignar</asp:ListItem>
                <asp:ListItem Value="A">Asignados</asp:ListItem>
                </asp:DropDownList></td>
    <td align="right">
        <asp:TextBox ID="txtEvaluador" CssClass="texto" runat="server" 
            BorderWidth="0" Height="21px" Width="104px">Evaluador: </asp:TextBox>
      <asp:DropDownList ID="ddlObservador" runat="server" AutoPostBack="True" 
                style="height: 22px"></asp:DropDownList></td>             
    </tr>
    <tr>     
        <td></td>
        <td><asp:Label ID="lblMensaje0" runat="server" ForeColor="Maroon" style="color: #CC0000"></asp:Label></td>    
    </tr>
    <tr><td><br /></td></tr>
    <tr>
    <td colspan="2" class="" >
    <asp:GridView ID="gvCarga" runat="server" AutoGenerateColumns="False" 
            DataKeyNames="codigo_Per" Width="100%" CellPadding="4" ForeColor="#333333">
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
                <asp:BoundField DataField="nombre_Dac" HeaderText="Departamento Académico" />
                <asp:BoundField DataField="Docente" HeaderText="Docente" />
                <asp:BoundField DataField="descripcion_Cac" HeaderText="Ciclo" 
                    ItemStyle-HorizontalAlign="Center" >
<ItemStyle HorizontalAlign="Center"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField DataField="Evaluador" HeaderText="Docente Observador" />
                <asp:BoundField DataField="estado_evaluado" HeaderText="Evaluado" ItemStyle-HorizontalAlign="center"/>
                <asp:BoundField DataField="codigo_obs" HeaderText="codigo_obs" Visible="false"
                    ItemStyle-HorizontalAlign="Center" >
<ItemStyle HorizontalAlign="Center"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField DataField="codigo_Per" HeaderText="codigo_Per" Visible="false"
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

    </tr>
    <tr><td>
        <br /></td><td>
        <br /></td></tr>
   </table>
    </div>
</form>
</body>
</html>
