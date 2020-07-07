<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmActualizarObservacionCargo.aspx.vb" Inherits="administrativo_pec_frmActualizarObservacionCargo" Theme="Acero" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Actualizar Observación</title>
    <link href="../../../private/estilo.css" rel="stylesheet" type="text/css" />
    <script src="../js/jquery-1.4.4.min.js" type="text/javascript"></script>
    
    <style type="text/css"">
        /***** Para avisos **************/
        .mensajeError
        {
        	border: 1px solid #e99491;
        	background-color: #fed8d5;        	
        	padding-top:2px;        	
        	-moz-border-radius: 15px;
        	padding-left:25px;  	
        }
        
         .mensajeExito
        {
        	border: 1px solid #488e00;
        	background-color:#c5f4b5;        	
        	padding-top:2px;        	
        	-moz-border-radius: 15px;
        	padding-left:25px;        	
        }
        
        /********************************/   
    
    </style>  
    
    <script type="text/javascript">

        $(document).ready(function() {
            $("#txtObservacion").focus();
        });
        
        function SubmitToParent() {
            opener.RefrescarGridDeudas();
            window.close();
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div>            
            <!-- Para avisos -->            
            <div id="mensajes" runat="server" style="height:25px; padding-top:2px; width: 92%;">
                <asp:Image ID="Image1" runat="server" ImageUrl="../../Images/beforelastchild.GIF"/>            
                <asp:Label ID="lblMensaje" runat="server" Visible="true"></asp:Label>            
            </div>            
            <!-------------------------------------------->    
            <div style="height:10px"></div>            
            <div style="float:left; width:70px">
                <asp:Label ID="Label1" runat="server" Text="Observación"></asp:Label> 
            </div>
            <div>
                <asp:TextBox ID="txtObservacion" runat="server" TextMode="MultiLine" Height="80px" Width="300px"></asp:TextBox>                                
            </div>
        </div>        
        <div style="height:20px"></div>
        <div>
            <div style="float:left; width:70px">                
            </div>
            <div>
                <asp:Button ID="btnGuardar" runat="server" Text="Guardar" SkinID="BotonAceptar" OnClientClick="SubmitToParent()"/>                
                <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" 
                SkinID="BotonCancelar" OnClientClick="window.close()"/>
            </div>
        </div>
        
        <asp:GridView ID="grw" runat="server" AutoGenerateColumns="False">
            <Columns>
                <asp:BoundField DataField="codigo_Alu" HeaderText="codigo_Alu" />
                <asp:BoundField DataField="codigo_Deu" HeaderText="codigo_Deu" />
                <asp:BoundField DataField="observacion_Deu" HeaderText="observacion_Deu" />
                <asp:BoundField DataField="ipObs_Deu" HeaderText="ipObs_Deu" />
                <asp:BoundField DataField="usuObs_Deu" HeaderText="usuObs_Deu" />
            </Columns>
        </asp:GridView>
    
    </div>
    </form>
</body>
</html>
