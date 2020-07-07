﻿'----------------------------------------------------------------------
'Fecha: 30.10.2012
'Usuario: gcastillo
'Motivo: Cambio de URL del servidor de la WebUSAT
'----------------------------------------------------------------------
Partial Class academico_JustificacionDni
    Inherits System.Web.UI.Page

    Protected Sub cmdGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdGuardar.Click
        Dim ObjCnx As New ClsConectarDatos
        ObjCnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        ObjCnx.AbrirConexion()
        ObjCnx.Ejecutar("PERSON_DesbloquearDni", Me.TxtFechaH.Text, hddCodigo_alu.Value, txtObservacion.Text)
        ObjCnx.CerrarConexion()
        Page.RegisterStartupScript("ok", "<script>alert('Se grabaron los datos correctamente');window.parent.location.reload();self.parent.tb_remove();</script>")
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack = False Then

            Dim obj As New ClsConectarDatos
            Dim datos, DatDni As New Data.DataTable
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            Dim objEnc As New EncriptaCodigos.clsEncripta
            hddCodigo_alu.Value = CInt(Mid(objEnc.Decodifica(Request.QueryString("c")), 4))
            'Cargar datos del alumno
            datos = obj.TraerDataTable("ConsultarAlumno", "DI", hddCodigo_alu.Value) '"081IA10264"
            DatDni = obj.TraerDataTable("PERSON_ConsultarDesbloqueoDni", hddCodigo_alu.Value)
            obj.CerrarConexion()
            If datos.Rows.Count > 0 Then
                Me.dlEstudiante.DataSource = datos
                Me.dlEstudiante.DataBind()
                Me.TxtFechaH.Text = Now.ToShortDateString.ToString
                If Request.QueryString("accion") = "M" Then
                    Me.cmdCancelar.UseSubmitBehavior = False
                    Me.cmdCancelar.Attributes.Add("onclick", "self.parent.tb_remove();")
                Else
                    Me.cmdCancelar.UseSubmitBehavior = True
                End If
                If DatDni.Rows.Count > 0 Then
                    Me.TxtFechaH.Text = DatDni.Rows(0).Item("fechaFin_adni")
                    Me.txtObservacion.Text = DatDni.Rows(0).Item("justificacion_adni")
                    Me.lblTitulo.Text = "DATOS VIGENTES DE DESBLOQUEO POR DNI"
                Else
                    Me.lblTitulo.Text = "NUEVO DESBLOQUEO POR DNI"
                End If
            End If
            obj = Nothing
        End If
    End Sub

    Protected Sub dlEstudiante_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataListItemEventArgs) Handles dlEstudiante.ItemDataBound
        Dim ruta As String
        Dim img As Image
        Dim obEnc As Object
        obEnc = Server.CreateObject("EncriptaCodigos.clsEncripta")

        ruta = obEnc.CodificaWeb("069" & CType(e.Item.FindControl("lblcodigo"), Label).Text)
        'ruta = "http://www.usat.edu.pe/imgestudiantes/" & ruta
        ruta = "//intranet.usat.edu.pe/imgestudiantes/" & ruta
        img = e.Item.FindControl("FotoAlumno")
        img.ImageUrl = ruta
        obEnc = Nothing
    End Sub

End Class
