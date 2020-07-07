<%@ Page Language="VB" AutoEventWireup="false" CodeFile="reglamentos.aspx.vb" Inherits="reglamentos" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .style1
        {
            font-family: Arial, Helvetica, sans-serif;
            font-weight: bold;
            font-size: large;
        }
        .style2
        {
            height: 134px;
        }
        .style3
        {
            text-align: center;
        }
        .style4
        {
            text-align: center;
            height: 204px;
        }
        .style5
        {
            text-align: center;
            height: 198px;
        }
        .style6
        {
            height: 198px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table style="width: 100%;">
            <tr>
                <td style="text-align: center">
                    &nbsp;
                </td>
                <td class="style1" style="text-align: center" colspan="2">
                    REGLAMENTOS USAT
                </td>
                <td class="style1" style="text-align: center">
                    &nbsp;
                </td>
                <td class="style1" style="text-align: center">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td style="text-align: center">
                    &nbsp;
                </td>
                <td style="text-align: center">
                    &nbsp;
                </td>
                <td style="text-align: center">
                    &nbsp;
                </td>
                <td style="text-align: center">
                    &nbsp;
                </td>
                <td style="text-align: center">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td>
                    <h4>
                        MODELOS</h4>
                </td>
            </tr>
            <tr>
                <td class="style3">
                    <img alt="" src="MOD_EDUCACION_VIRTUAL.png" style="width: 150px; height: 150px" />
                </td>
                <td class="style3"">
                    <img alt="" src="MOD_EDUCATIVO.png" style="width: 150px; height: 150px" />
                </td>
            </tr>
            <tr>
                <td class="style3">
                    <asp:Button ID="cmdReglamento3" runat="server" Style="text-align: center" Text="Descargar"
                        OnClientClick="target ='_blank';" />
                </td>
                <td class="style3">
                    <asp:Button ID="cmdReglamento7" runat="server" Style="text-align: center" Text="Descargar"
                        OnClientClick="target ='_blank';" />
                </td>
            </tr>
            <tr>
                <td>
                    <h4>
                        POLÍTICAS</h4>
                </td>
            </tr>
            <tr>
                <td class="style3">
                    <img alt="" src="POL_INVESTIGACION.png" style="height: 150px; width: 150px" />
                </td>
                <td class="style3">
                    <img alt="" src="POL_GEST_AMBIENTAL.PNG" style="width: 150px; height: 150px" />
                </td>
            </tr>
            <tr>
                <td class="style3">
                    <asp:Button ID="cmdReglamento11" runat="server" Style="text-align: center" Text="Descargar"
                        OnClientClick="target ='_blank';" />
                </td>
                <td class="style3">
                    <asp:Button ID="cmdReglamento34" runat="server" Style="text-align: center" Text="Descargar"
                        OnClientClick="target ='_blank';" />
                </td>
            </tr>
            <tr>
                <td>
                    <h4>
                        REGLAMENTOS</h4>
                </td>
            </tr>
            <tr>
                <td class="style3">
                    <img alt="" src="REGL_EST_PREGRADO.png" style="height: 150px; width: 150px" />
                </td>
                <td class="style3">
                    <img alt="" src="REGL_INT_TRABAJADOR.png" style="width: 150px; height: 150px" />
                </td>
                <td class="style3">
                    <img alt="" src="REGL_INT_SEGYSALUD_TRABAJO.png" style="height: 150px; width: 150px" />
                </td>
                <td class="style3">
                    <img alt="" src="REGL_AY_ECON_FORM_ACAD_TRABAJADOR.png" style="width: 150px; height: 150px" />
                </td>
                <td class="style3">
                    <img alt="" src="REGL_POS.png?x=1" style="width: 150px; height: 150px" />
                </td>
            </tr>
            <tr>
                <td class="style3">
                    <asp:Button ID="cmdReglamento2" runat="server" Style="text-align: center" Text="Descargar"
                        OnClientClick="target ='_blank';" />
                </td>
                <td class="style3">
                    <asp:Button ID="cmdReglamento1" runat="server" Style="text-align: center" Text="Descargar"
                        OnClientClick="target ='_blank';" />
                </td>
                <td class="style3">
                    <!-- <asp:Button ID="cmdReglamento15" runat="server" style="text-align: center" 
                        Text="Descargar" OnClientClick="target ='_blank';" /> -->
                    <asp:Button ID="cmdReglamento25" runat="server" Style="text-align: center" Text="Descargar"
                        OnClientClick="target ='_blank';" />
                </td>
                <td class="style3">
                    <asp:Button ID="cmdReglamento19" runat="server" Style="text-align: center" Text="Descargar"
                        OnClientClick="target ='_blank';" />
                </td>
                <td class="style3">
                    <asp:Button ID="Button9" runat="server" Style="text-align: center" Text="Descargar"
                        OnClientClick="target ='_blank';" />
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td class="style3">
                    <img alt="" src="REGL_EVAL_ACRED_UNIVER.png" style="width: 150px; height: 150px" />
                </td>
                <td class="style3">
                    <img alt="" src="REGL_GEST_CURRIC.png" style="width: 150px; height: 150px; margin-right: 8px;
                        margin-top: 0px;" />
                </td>
                <td class="style3">
                    <img alt="" src="REGL_ENTR_CARGO.png" style="height: 150x; width: 150px" />
                </td>
                <td class="style3">
                    <img alt="" src="REGL_EDUC_CONTINUA.png" style="width: 150px; height: 150px" />
                </td>
                <td class="style3">
                    <img alt="" src="REGL_AUSP_ACADEMICOS.png" style="width: 150px; height: 150px" />
                </td>
            </tr>
            <tr>
                <td class="style3">
                    <asp:Button ID="cmdReglamento14" runat="server" Style="text-align: center" Text="Descargar"
                        OnClientClick="target ='_blank';" />
                </td>
                <td class="style3">
                    <asp:Button ID="cmdReglamento9" runat="server" Style="text-align: center" Text="Descargar"
                        OnClientClick="target ='_blank';" />
                </td>
                <td class="style3">
                    <asp:Button ID="cmdReglamento12" runat="server" Style="text-align: center" Text="Descargar"
                        OnClientClick="target ='_blank';" />
                </td>
                <td class="style3">
                    <asp:Button ID="cmdReglamento6" runat="server" Style="text-align: center" Text="Descargar"
                        OnClientClick="target ='_blank';" />
                </td>
                <td class="style3">
                    <asp:Button ID="cmdReglamento21" runat="server" Style="text-align: center" Text="Descargar"
                        OnClientClick="target ='_blank';" />
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <!-- <td align="center">
                    <img alt="" src="directiva.academica.jpg" style="width: 177px; height: 154px" /></td> -->
                <td class="style3">
                    <img alt="" src="REGL_BIBLIOTECA.png" style="width: 150px; height: 150px" />
                </td>
                <td class="style3">
                    <img alt="" src="REGL_INVESTIGACION.png" style="width: 150px; height: 150px" />
                </td>
                <td class="style3">
                    <img alt="" src="REGL_GEN_ASOCIA_CIVIL.png" style="width: 150px; height: 150px" />
                </td>
                <td class="style3">
                    <img alt="" src="REGL_BECAS.png" style="width: 150px; height: 150px" />
                </td>
                <td class="style3">
                    <img alt="" src="REGL_PER_DOCENTE.png" style="width: 150px; height: 150px" />
                </td>
            </tr>
            <tr>
                <td class="style3">
                    <asp:Button ID="cmdReglamento4" runat="server" Style="text-align: center" Text="Descargar"
                        OnClientClick="target ='_blank';" />
                </td>
                <td class="style3">
                    <asp:Button ID="cmdReglamento10" runat="server" Style="text-align: center" Text="Descargar"
                        OnClientClick="target ='_blank';" />
                </td>
                <td class="style3">
                    <asp:Button ID="cmdReglamento23" runat="server" Style="text-align: center" Text="Descargar"
                        OnClientClick="target ='_blank';" />
                </td>
                <td class="style3">
                    <asp:Button ID="cmdReglamento16" runat="server" Style="text-align: center" Text="Descargar"
                        OnClientClick="target ='_blank';" />
                </td>
                <td class="style3">
                    <asp:Button ID="cmdReglamento28" runat="server" Style="text-align: center" Text="Descargar"
                        OnClientClick="target ='_blank';" />
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td class="style3">
                    <img alt="" src="REGL_BIEN_ESTUDIANTIL.png" style="width: 150px; height: 150px" />
                </td>
                <td class="style3">
                    <img alt="" src="REGL_ELA_TRJ_ACAD.png?x=3" style="width: 150px; height: 150px" />
                </td>
            </tr>
            <tr>
                <td class="style3">
                    <asp:Button ID="cmdReglamento30" runat="server" Style="text-align: center" Text="Descargar"
                        OnClientClick="target ='_blank';" />
                </td>
                <td class="style3">
                    <asp:Button ID="Button14" runat="server" Style="text-align: center" Text="Descargar"
                        OnClientClick="target ='_blank';" />
                </td>
            </tr>
            <tr>
                <td>
                    <h4>
                        DIRECTIVAS</h4>
                </td>
            </tr>
            <tr>
                <td class="style3">
                    <img alt="" src="DIR_APL_EXS_REC.png?x=1" style="width: 150px; height: 150px" />
                </td>
                <td class="style3">
                    <img alt="" src="DIR_BON.png?x=1" style="width: 150px; height: 150px" />
                </td>
                <td style="text-align: center" class="style2">
                    <img alt="" src="DIR_PROG_CURVERANO.png" style="height: 150px; width: 150px" />
                </td>
                <td class="style3">
                    <img alt="" src="DIR_CONT_DEUDA.png" style="width: 150px; height: 150px" />
                </td>
                <td class="style3">
                    <img alt="" src="DIR_PROG_DES_ACAD.png" style="width: 150px; height: 150px" />
                </td>
            </tr>
            <tr>
                <td class="style3">
                    <asp:Button ID="Button12" runat="server" Style="text-align: center" Text="Descargar"
                        OnClientClick="target ='_blank';" />
                </td>
                <td class="style3">
                    <asp:Button ID="Button10" runat="server" Style="text-align: center" Text="Descargar"
                        OnClientClick="target ='_blank';" />
                </td>
                <td class="style3">
                    <asp:Button ID="cmdReglamento33" runat="server" Style="text-align: center" Text="Descargar"
                        OnClientClick="target ='_blank';" />
                </td>
                <td class="style3">
                    <asp:Button ID="cmdReglamento24" runat="server" Style="text-align: center" Text="Descargar"
                        OnClientClick="target ='_blank';" />
                </td>
                <td class="style3">
                    <asp:Button ID="cmdReglamento22" runat="server" Style="text-align: center" Text="Descargar"
                        OnClientClick="target ='_blank';" Visible="True" />
                </td>
            </tr>
            <tr>
                <tr>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <td class="style3">
                    <img alt="" src="DIR_ENT_RENDIR.png" style="width: 150px; height: 150px" />
                </td>
                <td class="style3">
                    <img alt="" src="DIR_ADM_PROG_APO_DOC.png" style="width: 150px; height: 150px" />
                </td>
                <td class="style3">
                    <img alt="" src="DIR_ADM_INV_DOC.png" style="width: 150px; height: 150px" />
                </td>
                <!-- <td class="style3">
					<img alt="" src="DIR_ASIG_TAR_DOC.png?x=1" style="width: 150px; height: 150px" />
                </td>-->
                <td class="style3">
                    <img alt="" src="DIR_ASIG_CAR_LEC_DOC.png?x=1" style="width: 150px; height: 150px" />
                </td>
                <td class="style3">
                    <img alt="" src="REGL_ADSC_PROF.png?x=1" style="height: 150px; width: 150px" />
                </td>
            </tr>
            <tr>
                <td class="style3">
                    <asp:Button ID="cmdReglamento32" runat="server" Style="text-align: center" Text="Descargar"
                        OnClientClick="target ='_blank';" />
                </td>
                <td class="style3">
                    <asp:Button ID="Button1" runat="server" Style="text-align: center" Text="Descargar"
                        OnClientClick="target ='_blank';" />
                </td>
                <!-- 'cfarfan 29/10/2018 -->
                <td class="style3">
                    <asp:Button ID="Button3" runat="server" Style="text-align: center" Text="Descargar"
                        OnClientClick="target ='_blank';" />
                </td>
                <td class="style3">
                    <asp:Button ID="Button8" runat="server" Style="text-align: center" Text="Descargar"
                        OnClientClick="target ='_blank';" />
                </td>
                <td class="style3">
                    <asp:Button ID="cmdReglamento18" runat="server" Style="text-align: center" Text="Descargar"
                        OnClientClick="target ='_blank';" />
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td class="style3">
                    <img alt="" src="DIR_ADC_DOC_LEY.png?x=1" style="height: 150px; width: 150px" />
                </td>
                <td class="style3">
                    <img alt="" src="DIR_PLN_EST_OPE_PRE.png?x=1" style="height: 150px; width: 150px" />
                </td>
            </tr>
            <tr>
                <td class="style3">
                    <asp:Button ID="Button13" runat="server" Style="text-align: center" Text="Descargar"
                        OnClientClick="target ='_blank';" />
                </td>
                <td class="style3">
                    <asp:Button ID="btnDirPlanEstrategico" runat="server" Style="text-align: center"
                        Text="Descargar" OnClientClick="target ='_blank';" />
                </td>
            </tr>
            <tr>
                <td>
                    <h4>
                        LINEAMIENTOS</h4>
                </td>
            </tr>
            <tr>
                <td class="style3">
                    <img alt="" src="LIN_NORMA_FORM_DOC_OFI.png" style="width: 150px; height: 150px" />
                </td>
                <td class="style3">
                    <img alt="" src="LIN_DES_ASE_TESIS.png" style="width: 150px; height: 150px" />
                </td>
                <td class="style3">
                    <img alt="" src="LIN_ACT_SUS_TES.png" style="width: 150px; height: 150px" />
                </td>
            </tr>
            <tr>
                <td class="style3">
                    <asp:Button ID="cmdReglamento20" runat="server" Style="text-align: center" Text="Descargar"
                        OnClientClick="target ='_blank';" />
                </td>
                <td class="style3">
                    <asp:Button ID="cmdReglamento35" runat="server" Style="text-align: center" Text="Descargar"
                        OnClientClick="target ='_blank';" />
                </td>
                <td class="style3">
                    <asp:Button ID="cmdReglamento36" runat="server" Style="text-align: center" Text="Descargar"
                        OnClientClick="target ='_blank';" />
                </td>
            </tr>
            <tr>
                <td>
                    <h4>
                        PROCEDIMIENTOS</h4>
                </td>
            </tr>
            <tr>
                <td class="style3">
                    <img alt="" src="PROC_ING_ASIG_AF.png" style="width: 150px; height: 150px" />
                </td>
                <td class="style3">
                    <img alt="" src="PROC_CTRL_AF.png" style="width: 150px; height: 150px" />
                </td>
                <td class="style3">
                    <img alt="" src="PROC_TSL_AF.png" style="width: 150px; height: 150px" />
                </td>
                <td class="style3">
                    <img alt="" src="PROC_DIP_FIN_AF.png" style="width: 150px; height: 150px" />
                </td>
            </tr>
            <tr>
                <td class="style3">
                    <asp:Button ID="Button4" runat="server" Style="text-align: center" Text="Descargar"
                        OnClientClick="target ='_blank';" />
                </td>
                <td class="style3">
                    <asp:Button ID="Button5" runat="server" Style="text-align: center" Text="Descargar"
                        OnClientClick="target ='_blank';" />
                </td>
                <td class="style3">
                    <asp:Button ID="Button6" runat="server" Style="text-align: center" Text="Descargar"
                        OnClientClick="target ='_blank';" />
                </td>
                <td class="style3">
                    <asp:Button ID="Button7" runat="server" Style="text-align: center" Text="Descargar"
                        OnClientClick="target ='_blank';" />
                </td>
            </tr>
            <tr>
                <td>
                    <h4>
                        MANUALES</h4>
                </td>
            </tr>
            <tr>
                <td class="style3">
                    <img alt="" src="MAN_IDENTIDAD_USAT.png" style="height: 150px; width: 150px" />
                </td>
                <td class="style3">
                    <img alt="" src="MAN_LIC_PERMISOS.PNG" style="width: 150px; height: 150px" />
                </td>
            </tr>
            <tr>
                <td class="style3">
                    <asp:Button ID="cmdReglamento17" runat="server" Style="text-align: center" Text="Descargar"
                        OnClientClick="target ='_blank';" />
                </td>
                <td class="style3">
                    <asp:Button ID="Button2" runat="server" Style="text-align: center" Text="Descargar"
                        OnClientClick="target ='_blank';" />
                </td>
            </tr>
            <tr>
                <td>
                    <h4>
                        FOLLETOS</h4>
                </td>
            </tr>
            <tr>
                <td class="style3">
                    <img alt="" src="FOL_BIENESTAR.png" style="width: 150px; height: 150px" />
                </td>
            </tr>
            <tr>
                <td class="style3">
                    <asp:Button ID="cmdReglamento31" runat="server" Style="text-align: center" Text="Descargar"
                        OnClientClick="target ='_blank';" />
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
