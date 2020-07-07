﻿Imports System.IO
Imports System.Web.HttpRequest
Imports System.Collections.Generic
Imports System.Data

Partial Class operaciones
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim objDefensoria As New ClsDefensoria
        Dim Data As New Dictionary(Of String, Object)()
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim JSONresult As String = ""
        Try
            Dim k As String = Request("k")
            Dim f As String = Request("f")

            Select Case Request("action")
                Case "ValidaSession"
                    ValidaSession()
                Case "ope"
                    TiposOperacion()
            End Select

        Catch ex As Exception
            Data.Add("msje", ex.Message)
            JSONresult = serializer.Serialize(Data)
            Response.Write(JSONresult)
        End Try

    End Sub

    Private Sub TiposOperacion()
        Try
            Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
            Dim JSONresult As String = ""
            Dim obj As New ClsDefensoria
            Dim data As New Dictionary(Of String, Object)()

            data.Add("ValSes", obj.EncrytedString64("ValidaSession"))
            data.Add("lst", obj.EncrytedString64("Listar")) ' Listar
            data.Add("reg", obj.EncrytedString64("Registrar")) ' Registrar
            data.Add("edi", obj.EncrytedString64("Editar")) ' Modificar
            data.Add("mod", obj.EncrytedString64("Modificar")) ' Modificar
            data.Add("eli", obj.EncrytedString64("Eliminar")) ' Eliminar
            data.Add("btnd", obj.EncrytedString64("BuscaxTipoyNumDoc")) ' Busqueda por tipo y num de docuemnto interesado
            data.Add("bcon", obj.EncrytedString64("BuscaCoincidencia")) ' Busqueda por apellidos y nombres de interesado
            data.Add("scon", obj.EncrytedString64("SeleccionCoincidencia")) 'Seleccionar Coincidencia
            data.Add("bie", obj.EncrytedString64("BuscaInstitucionEducativa")) 'Buscar IE por Codigo departamento
            data.Add("vdup", obj.EncrytedString64("ValidaDuplicado")) 'Verifica que no este registrado por tipodoc y numdoc
            data.Add("ins", obj.EncrytedString64("InscribirInteresado")) ' Inscribi Interesado en Evento
            data.Add("pint", obj.EncrytedString64("PerfilInteresado"))
            data.Add("Idsint", obj.EncrytedString64("IdSessionInteresado"))
            data.Add("cfi", obj.EncrytedString64("CargaFiltros"))
            data.Add("cons", obj.EncrytedString64("Consultar")) ' Listar
            data.Add("resp", obj.EncrytedString64("RegistrarRespuesta")) ' Registrar Respuesta 24/01/2020
            data.Add("cres", obj.EncrytedString64("ConsultarRespuesta")) ' Ver Respuestas 28-02
            JSONresult = serializer.Serialize(data)
            Response.Write(JSONresult)

        Catch ex As Exception

        End Try
    End Sub

    Private Sub ValidaSession()
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim JSONresult As String = ""
        Dim list As New List(Of Dictionary(Of String, Object))()
        Dim data As New Dictionary(Of String, Object)()
        If Session("id_per") <> "" Then
            data.Add("msje", True)
            data.Add("link", "")
        Else
            data.Add("msje", False)
            data.Add("link", "../../../sinacceso.html")
        End If
        list.Add(data)
        JSONresult = serializer.Serialize(list)
        Response.Write(JSONresult)
    End Sub

End Class
