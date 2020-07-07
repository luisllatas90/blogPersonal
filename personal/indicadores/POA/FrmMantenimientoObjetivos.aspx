<%@ Page Language="VB" AutoEventWireup="false" CodeFile="FrmMantenimientoObjetivos.aspx.vb" Inherits="indicadores_POA_PROTOTIPOS_FrmMantenimientoObjetivos" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
        <link href="../../css/estilo.css" rel="stylesheet" type="text/css" media="screen" />

    <style type="text/css">
       .lblbusqueda
       {
       padding-top:100px;
       }
       .boton1
       {
           font-size:11px;
           color:#333300;
           border: 1px solid #666666;
           background: #FEFFE1 url('../../images/previo.gif') no-repeat 0% 80%;
           width: 80;
           font-family: Tahoma;
           font-size: 8pt;
           cursor: hand;
           }
       .boton2
       {
           font-size:11px;
           color:#333300;
           border: 1px solid #666666;
           background: #FEFFE1 url('../../images/nuevo.gif') no-repeat 0% center;
           width: 80;
           font-family: Tahoma;
           font-size: 8pt;
           cursor: hand;
           }
           
           
        .menuporelegir
        {
            border: 1px solid #808080;
            background-color: #FFCC66;
        }    
        .AlineadoDerecha{
            text-align:right;
            width: 100px
        }  
        .style
        {
            height: 22px;
            width: 80px
        }
        
        .titulo_poa 
        {
            position:absolute;
            top:15px;
            left:15px;
            font-size:14px;
            font-weight:bold;
            font-family:"Helvetica Neue",Helvetica,Arial,sans-serif;
            color:#337ab7;
            background-color:White;
            padding-bottom:10px;
            padding-left:5px;
            padding-right:5px;    
            z-index:1;    
        }    
        .contorno_poa
        {
            position:relative;
            top:10px;
            border:1px solid #C0C0C0;
            padding-left:4px;
            padding-top:20px;
            padding-right:4px;
        }
                    
    </style>
</head>
<body>
    <form id="form1" runat="server">

    <div class="titulo_poa">
        <asp:Label ID="Label2" runat="server" Text="Mantenimiento de Objetivos"></asp:Label>
    </div>
    
    
    <div class="contorno_poa">
        <table width="100%">
        <tr>
        <td class="style">Actividad</td>
            <td colspan="5" class="style">
                <asp:DropDownList ID="ddl_Actividad" runat="server" Width="650px" ></asp:DropDownList>
                <br />
                <asp:TextBox ID="txt_actividad" runat="server" Width="650px" Visible="False"></asp:TextBox>
            </td>
        </tr>   
        
        <tr>
        <td class="style">Descripción</td>
            <td colspan="5" class="style"><asp:TextBox ID="txt_descripcion" runat="server" Width="650px"></asp:TextBox></td>
           
        </tr>   
        
       
        <tr>
            <td colspan="6"></td>
        </tr>
        
        <tr>
            <td class="style">
                <asp:Label ID="lbl_trimestre1" runat="server" Text="Trimestre 1"></asp:Label>
            </td>
            <td class="style">
                <asp:TextBox ID="txt_meta1" runat="server" CssClass="AlineadoDerecha"></asp:TextBox>
            </td>
            <td class="style">
                <asp:Label ID="lbl_trimestre2" runat="server" Text="Trimestre 2"></asp:Label>
            </td>
            <td class="style">
                <asp:TextBox ID="txt_meta2" runat="server" CssClass="AlineadoDerecha"></asp:TextBox>
            </td>
            <td class="style"></td>
            <td class="style"></td>
        </tr>
        
        <tr>
            <td class="style">
                <asp:Label ID="lbl_trimestre3" runat="server" Text="Trimestre 3" ></asp:Label>
            </td>
            <td class="style">
                <asp:TextBox ID="txt_meta3" runat="server" CssClass="AlineadoDerecha"></asp:TextBox>
            </td>
            <td class="style">
                <asp:Label ID="lbl_trimestre4" runat="server" Text="Trimestre 4" ></asp:Label>
            </td>
            <td class="style">
                <asp:TextBox ID="txt_meta4" runat="server" CssClass="AlineadoDerecha"></asp:TextBox>
            </td>
            <td class="style"></td>
            <td class="style"></td>
        </tr>
        
                
        <tr>
            <td colspan="6"></td>        
        </tr>
        
        
        <tr>
            <td colspan="6" align="right"><br />
                <asp:Button ID="cmdGuardar" runat="server" CssClass="guardar2" Text="   Guardar"/>
                <asp:Button ID="cmdCancelar" runat="server" CssClass="regresar2" Text="  Cancelar"/>
            </td>
            
         
            
                    
        </tr>
    </table>
    </div>
    </form>
</body>
</html>

