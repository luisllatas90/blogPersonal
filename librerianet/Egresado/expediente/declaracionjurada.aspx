<%@ Page Language="VB" AutoEventWireup="false" CodeFile="declaracionjurada.aspx.vb" Inherits="Egresado_expediente_declaracionjurada" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
 
    <style>
    .content
       {
                padding:2px;
                border:1px solid #e33439;
                background-color:#FEFFE1;
                font-family:Verdana;
                font-size:10px;        
            }
        .head_titulo
        { 
            font-weight:bold; background-color:#e33439; color:White;
            padding:4px;        
        }
        .btn
        {
        padding:4px;
        font-weight:bold;
        font-size :12px;   
        text-decoration:none;
        border:1px solid #C2C2C2;
        color:Black;}
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div style="">
    <div id="divdj" runat="server">
        <div class="content">       
           <br />
           <span class="head_titulo">DECLARACIÓN JURADA</span>
            <br />
           <br />
           <span class="">YO <b><asp:Label ID="lblnombre" runat="server" Text="Label"></asp:Label></b>&nbsp;declaro que:</span>
            <br />
           <br />
           <span class="">
           <b>1.-</b> Los datos que se consignan a continuación tienen  carácter de <b>DECLARACION JURADA</b>.<br /> 
           Solo el egresado es personalmente responsable de la veracidad de los mismos.      
           <br />
           <br />
           <b>2.-</b> La USAT facilitará el envío de la información consignada en el CV a las empresas que soliciten egresados USAT 
            <br />
           <br />
           <b>3.-</b>La información consignada en el CV deberá ser actualizada por el egresado, cada vez que ingrese al módulo <b>Bolsa de trabajo - alumniUSAT</b>.
              <br />
           <br />              
               <asp:Button ID="btnAcepta" runat="server"  CssClass="btn" Text="Acepto" />
               &nbsp;
               <asp:Button ID="btnNoAcepta" runat="server"  CssClass="btn" Text="No Acepto" />
           </span>         
        
        </div>
    </div>
</div>               
    </form>
</body>
</html>
