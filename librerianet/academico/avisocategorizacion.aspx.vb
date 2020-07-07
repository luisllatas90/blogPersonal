﻿
Partial Class librerianet_academico_avisocategorizacion
    Inherits System.Web.UI.Page
    Public pagina As String = "../../estudiante/abriraplicacion.asp?codigo_tfu=3&codigo_apl=8&descripcion_apl=Campus Virtual USAT&op=1"


    Protected Sub btnAceptar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAceptar.Click
        Try
            Dim obj As New ClsConectarDatos
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            obj.TraerDataTable("[AlumnoCategorizacionAceptada_Registrar]", Session("codigo_alu"), Request.ServerVariables("HTTP_HOST"))
            obj.CerrarConexion()
            obj = Nothing

        Catch ex As Exception

        End Try
        Response.Redirect(pagina)
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Page.IsPostBack = False Then
            Dim obj As New ClsConectarDatos
            Dim tb As New Data.DataTable
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            tb = obj.TraerDataTable("[AlumnoCategorizacionAceptada_Consultar]", Session("codigo_alu"))
            obj.CerrarConexion()
            obj = Nothing
            If tb.Rows(0).Item("ip_usu").ToString.Length > 0 Then
                Response.Redirect(pagina)
            Else
                Me.lblFamilia.Text = tb.Rows(0).Item("Familia").ToString
                Me.txtCosto.Text = tb.Rows(0).Item("Costo").ToString
                Me.txtCostoEnfermeria.Text = tb.Rows(0).Item("Costo").ToString
                Me.txtCostoMedicinaOdontologia.Text = tb.Rows(0).Item("Costo").ToString

                Parrafo1EnfermeriaInternadoMedicinaOdontologia.Visible = False
                Parrafo2Enfermeria.Visible = False
                Parrafo1General.Visible = False
                Parrafo2General.Visible = False
                Parrafo2MedicinaOdontologia.Visible = False
                Parrafo2Internado.Visible = False
                ParrafoInfo.Visible = False

                If tb.Rows(0).Item("cpf") = 11 Then
                    'Enfermeria
                    Parrafo1EnfermeriaInternadoMedicinaOdontologia.Visible = True
                    Parrafo2Enfermeria.Visible = True
                    
                ElseIf tb.Rows(0).Item("Internado") > 0 Then
                    'Internado Med.Odon
                    Parrafo1EnfermeriaInternadoMedicinaOdontologia.Visible = True
                    Parrafo2Internado.Visible = True
                  
                ElseIf tb.Rows(0).Item("cpf") = 24 Or tb.Rows(0).Item("cpf") = 31 Then
                    'Medicina o Odontología
                    Parrafo1EnfermeriaInternadoMedicinaOdontologia.Visible = True
                    Parrafo2MedicinaOdontologia.Visible = True
                    ParrafoInfo.Visible = True
                    
                Else 'Simple Mortales
                    Parrafo1General.Visible = True
                    Parrafo2General.Visible = True
                    ParrafoInfo.Visible = True
                    
                End If

            End If
        End If
      
    End Sub

    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        Try
            Response.Redirect(pagina)
        Catch ex As Exception
            Response.Redirect(pagina)
        End Try

    End Sub
End Class
