<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmGenBannerSes.aspx.vb" Inherits="Alumni_frmGenBannerSes" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../assets/bootstrap-4.1/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.11.2/jquery.min.js"></script>
    <script src="https://files.codepedia.info/files/uploads/iScripts/html2canvas.js"></script>

    
     <style type="text/css" >

        table2{
            padding: 10px;
            background:#8ba987 url('img/insLab600x600_5T.jpg') no-repeat center center;
            height:600px;
            width:600px;
        }

        table{
            background: #8ba987 url('img/FondoOferta.jpg');
            background-size:100% 100%;
            color:white;
            border: 1px solid Gray;
        }
    </style>
</head>

<body>
    <form id="form1" runat="server">
    <div class="container align-items-center">
        <div class="card align-items-center">
            <div class="card-body">
                <asp:TextBox ID="txtCodigo_ofe" runat="server" Visible="False"></asp:TextBox>
                <div id="html-content-holder" style="width: 700px; height: 700px;">
                    <table style = "width: 700px; height: 700px;  border-color: Gray;">
                        <tbody>
                            <tr style="height: 180px;">
								<td style="width: 190px; height: 74px;">&nbsp;</td>
								<td style="width: 443px; height: 74px;">&nbsp;</td>
							</tr>                        
							<tr style="height: 120px; vertical-align:middle;">
								<td style="width: 300px;">
                                    <asp:TextBox ID="txt_codigo_emp" runat="server" Visible = "false"></asp:TextBox>
								</td>
								<td style="width: 400px;">
								    <p><strong><asp:Label ID="lblTituloOfe" runat="server" Text="Label" Font-Size="28px" Font-Names="Calibri"></asp:Label></strong></p>
								</td>
							</tr>
							<tr style="height: 400px; vertical-align:top;">
							    <td style="width: 300px; height: 64px;" align="center">
							        <asp:Image ID="imgLogo" runat="server" ImageUrl="" Width="167px" Height="99px"/>
							    </td>
							    <td style="width: 400px; height: 64px;">
							        <ul>
							           <asp:Label ID="lblRequisitos" runat="server" Text="Label" Font-Size="22px" Font-Names="Calibri"></asp:Label> 
							        </ul>
							    </td>
							</tr>
                        </tbody>
                <%--    <tr style="height:35.66px;">
                        <td>                       
                        </td>
                        <td colspan="4" rowspan="2" style=" width:38.66px">
                            
                        </td>
                        <td style="width:3.66px"></td>
                        <td style="width:38.66px"></td>
                        <td style="width:38.66px"></td>
                        <td style="width:38.66px"></td>
                        <td style="width:38.66px"></td>
                        
                        <td style="width:38.66px"></td>                    
                    </tr>
                    <tr style="height:35.66px;">
                        <td style="height:38.66px"></td>
                        <td style="height:38.66px"></td>
                        <td style="height:38.66px"></td>
                        <td style="height:38.66px"></td>
                        <td style="height:38.66px"></td>
                        <td style="height:38.66px"></td>
                        <td style="height:38.66px"></td>
                    </tr>                
                    <tr style="height:35.66px;">
                        <td></td>
                        <td></td>
                        <td></td>
                        <td colspan="11" rowspan="2">--%>
                        <%-- 
                            <img src="img/groupWhiteCD.png" alt="grupo" />
                        --%>
                <%--        <br /><br /><br /><br /><br />
                        </td>                    
                        <td></td>
                    </tr>
                    <tr style="height:35.66px;">
                        <td></td>
                        <td></td>
                        <td></td>
                        <td colspan="10"></td>
                    </tr>
                    <tr style="height:37px;">
                        <td style="width:38.66px"></td>
                        <td style="width:38.66px; border-style:none;"> <img src="img/BuscaBaner.png" alt="grupo" /></td>
                        <td colspan="12" style="font-family:Arial Black; font-size:20px; background-color:White; text-align:center; border-style:none; color:Black;">
                            <asp:Label ID="lblTituloOfe" runat="server" Text="Label"></asp:Label>
                        </td>                    
                        <td></td>
                    </tr>
                    <tr style="height:5.33px; padding:0px;">
                        <td style="width:38.66px;"></td>
                        <td style="width:38.66px;"></td>
                        <td style="width:38.66px;"></td>
                        <td style="width:38.66px;"></td>
                        <td style="width:38.66px;"></td>
                        <td style="width:38.66px;"></td>
                        <td style="width:38.66px;"></td>
                        <td style="width:38.66px;"></td>
                        <td style="width:38.66px;"></td>
                        <td style="width:38.66px;"></td>
                        <td style="width:38.66px;"></td>
                        <td style="width:38.66px;"></td>
                        <td style="width:38.66px;"></td>
                        <td style="width:38.66px;"></td>
                        <td style="width:38.66px;"></td>
                    </tr>
                    <tr style="height:38.66px;">--%>
                    <%-- 
                        <td style="height:38.66px"></td>
                    --%>                    
                     <%--   <td style="height:38.66px"></td>                
                        <td style="font-family:Calibri;font-weight:bold; color:Yellow; font-size: 22px; text-align:left;" colspan=3>
                            <asp:Label ID="Label1" runat="server" Text="Label">EMPRESA:</asp:Label>
                        </td>                    
                        <td style="height:38.66px ;font-family:Calibri; font-weight:bold; color:white; font-size:25px;" colspan=9>
                            <asp:Label ID="lblEmpresa" runat="server" Text="Label"></asp:Label>
                        </td>
                        <td style="height:38.66px"></td>
                    </tr>--%>
                    <%--<tr style="height:38.66px;">
                        <td style="width:38.66px;"></td>
                        <td colspan="3" style="font-family:Calibri; font-weight:bold; color:Yellow; font-size: 18px; text-align:left;">
                            <asp:Label ID="Label2" runat="server" Text="Label">REQUIERE:</asp:Label>
                        </td>
                        <td colspan="10" style="height:38.66px ;font-family:Arial Narrow; font-weight:bold; color:White; font-size:18px;">
                            <asp:Label ID="lblRequiere" runat="server" Text="Label"></asp:Label>
                        </td>                   
                        <td style="width:38.66px;"></td>
                    </tr>                
                    <tr style="height:38.66px;">
                        <td style="width:38.66px;"></td>
                        <td colspan="3" style="font-family:Calibri; font-weight:bold; color:Yellow; text-align:left;" >
                            <asp:Label ID="Label3" runat="server" Text="Label">ESPECIFICACIONES:</asp:Label> 
                        </td>                    
                        <td colspan="12" style="font-family:Calibri; font-weight:bold; color:White">
                            <asp:Label ID="lblRequisitos" runat="server" Text="Label"></asp:Label>
                        </td>
                    </tr>               
                    <tr style="height:19.33px;">
                        <td style="width:38.66px;"></td>    
                        <td style="width:38.66px;"></td>                 
                        <td style="font-family:Calibri; font-size:14px; font-weight:bold; font-style:italic; color:Yellow;" colspan="6">
                            <asp:Label ID="lblFc" runat="server" Text="Encuentra mas ofertas laborales en :">                        
                            </asp:Label>                         
                        </td>                   
                       <td style="width:38.66px;">
                           <img src="img/fcbW.png" / alt="fcb" >
                       </td>
                       <td colspan="3" style="font-family:Calibri; font-size:14px; font-weight:bold; font-style:italic; color:Yellow ">
                            <asp:Label ID="Label4" runat="server" Text="AlumniUSAT">                     
                            </asp:Label>
                       </td>
                       <td style="width:38.66px;"></td>
                       <td style="width:38.66px;"></td> 
                       <td style="width:38.66px;"></td>            
                    </tr>--%>
                </table>
                </div>
            </div>
            <div class="card-footer align-items-center">
                <a id="btn-Convert-Html2Image" href="#">DESCARGAR IMAGEN</a>
                <br />
                <div id="previewImage" style="display: none;">
                </div>
            </div>
        </div>        
    </div>
    <script type="text/javascript">
        $(document).ready(function() {
            var element = $("#html-content-holder"); // global variable
            var getCanvas; // global variable

            html2canvas(element, {
                onrendered: function(canvas) {
                    $("#previewImage").append(canvas);
                    getCanvas = canvas;
                }
            });
            $("#btn-Convert-Html2Image").on('click', function() {
                var imgageData = getCanvas.toDataURL("image/png");
                // Now browser starts downloading it instead of just showing it
                var newData = imgageData.replace(/^data:image\/png/, "data:application/octet-stream");
                $("#btn-Convert-Html2Image").attr("download", "your_pic_name.png").attr("href", newData);
            });
            
        });
    </script>
    </form>
</body>
</html>
