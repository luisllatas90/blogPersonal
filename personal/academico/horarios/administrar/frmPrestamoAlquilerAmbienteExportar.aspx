<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmPrestamoAlquilerAmbienteExportar.aspx.vb" Inherits="academico_horarios_frmPrestamoAlquilerAmbiente" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style>
     body
        { font-family:Trebuchet MS;
          font-size:11.5px;
          cursor:hand;
          background-color:white;	 
        }
     .btn
       {
            border:1px solid #5D7B9D; 
            background:#F7F6F3 ; 
            font-family:Tahoma; 
            font-size:8pt; 
            font-weight:bold;  padding:3px; 
       }
    TBODY {
	display: table-row-group;
}
	
        #txtDesde, #txtHasta
        {
            background-color: #C9DDF5;
        }
          </style>
  
  <link rel="stylesheet" href="//code.jquery.com/ui/1.11.2/themes/smoothness/jquery-ui.css">   
  <script src="jquery/jquery-1.10.2.js" type="text/javascript"></script>   
  <script src="jquery/jquery-ui.js" type="text/javascript"></script>    
  <script src="jquery/jquery.ui.datepicker-es.js" type="text/javascript"></script>
  
    <script type="text/javascript">
        $(function() {
            $.datepicker.setDefaults($.datepicker.regional["es"]);
            $("#txtDesde").datepicker({
                firstDay: 0
            });
            $("#txtHasta").datepicker({
                firstDay: 0
            });
        });

        function confirmardia() {
           // var txt;
            //var r = confirm("Press a button!");
            //if (r == true) {
              //  return true
            //} else {
              //  return false
            //}
            alert("a");
        }
       
       
    </script>    
</head>
<body>
    
    <form id="form1" runat="server">
      
    
         <asp:GridView ID="gridHorario" runat="server" AutoGenerateColumns="False"
                                                CaptionAlign="Top" DataKeyNames="codigo_lho"
                                                    BorderStyle="None" CellPadding="4" 
             AlternatingRowStyle-BackColor="#F7F6F4" >
             <RowStyle BorderColor="#C2CFF1" />
             <Columns>
                 <asp:BoundField DataField="dia_Lho" HeaderText="Día" >
                 <ItemStyle Font-Underline="false" ForeColor="black" />
                 </asp:BoundField>
                 <asp:BoundField DataField="fechaIni_lho" HeaderText="Fecha" >
                 <ItemStyle Font-Underline="false" />
                 </asp:BoundField>
                  <asp:BoundField DataField="descripcion_lho" HeaderText="Descripción" />
                 <asp:BoundField DataField="nombre_hor" HeaderText="Inicio" />
                 <asp:BoundField DataField="horaFin_Lho" HeaderText="Fin" >
                 <ItemStyle Font-Underline="false" />
                 </asp:BoundField>
                 <asp:BoundField DataField="Ambiente" HeaderText="Ambiente" 
                     ItemStyle-HorizontalAlign="left">
                  <ItemStyle Font-Underline="false" />
                   </asp:BoundField>
                 
                 <asp:BoundField DataField="estadoHorario_lho" HeaderText="Estado" >
                   <ItemStyle Font-Underline="false" />
                  </asp:BoundField>
                
             </Columns>
             <EmptyDataTemplate>
                 <div style="color:#3266DB; background-color:#E8EEF7; padding:5px; font-style:italic;">
                     No se ha registrado horarios.
                 </div>
             </EmptyDataTemplate>
             <HeaderStyle BackColor="#e8eef7" BorderColor="#99BAE2" BorderStyle="Solid" 
                                                        BorderWidth="1px" 
                 ForeColor="#3366CC" />
             <AlternatingRowStyle BackColor="#F7F6F4" />
         </asp:GridView>

 

</form> 
</body>
</html>
