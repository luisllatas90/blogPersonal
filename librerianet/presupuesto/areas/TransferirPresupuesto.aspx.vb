﻿
Partial Class presupuesto_areas_TransferirPresupuesto
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If IsPostBack = False Then
                Dim obj As New ClsPresupuesto
                Dim dt As New Data.DataTable
                Dim codigo_dpr As Integer = Request.QueryString("id")

                dt = obj.ConsultarConceptoTransferir()

                If dt.Rows.Count > 0 Then
                    For i As Integer = 0 To dt.Rows.Count - 1
                        Dim Lista As New ListItem(dt.Rows(i).Item("concepto").ToString, dt.Rows(i).Item("CodigoCon").ToString)
                        Me.ddlItem.Items.Add(Lista)
                    Next
                End If

                dt = obj.ObtenerDatosTransferirPto(codigo_dpr)

                Me.hdcodDpr.Value = Request.QueryString("id")
                Me.lblPoa.Text = dt.Rows(0).Item("nombre_poa").ToString
                Me.lblPrograma.Text = dt.Rows(0).Item("resumen_acp").ToString
                Me.lblpresupuesto.Text = "S/ " + Math.Round(dt.Rows(0).Item("subtotal"), 2).ToString
                Dim codigo_poa As Integer = dt.Rows(0).Item("codigo_poa")
                Dim codigo_acp As Integer = dt.Rows(0).Item("codigo_acp")

                Me.ddlItem.SelectedValue = dt.Rows(0).Item("idArt")

                dt = obj.ListarProgProyTransferirPto(codigo_poa, codigo_acp)

                If dt.Rows.Count > 0 Then
                    For i As Integer = 0 To dt.Rows.Count - 1
                        Dim Lista As New ListItem(dt.Rows(i).Item("resumen_acp").ToString, dt.Rows(i).Item("codigo_acp").ToString)
                        Me.ddlPrograma.Items.Add(Lista)
                    Next
                End If


            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try

    End Sub

    Protected Sub ddlPrograma_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlPrograma.SelectedIndexChanged
        Me.ddlActividad.Items.Clear()
        Me.ddlActividad.Items.Add(New ListItem("--Seleccione--", ""))

        If Me.ddlPrograma.SelectedValue <> "" Then
            Dim obj As New ClsPresupuesto
            Dim dt As New Data.DataTable

            dt = obj.ListarActividadesProgProy(Me.ddlPrograma.SelectedValue)

            If dt.Rows.Count > 0 Then
                For i As Integer = 0 To dt.Rows.Count - 1
                    Dim Lista As New ListItem(dt.Rows(i).Item("descripcion_dap").ToString, dt.Rows(i).Item("codigo_dap").ToString)
                    Me.ddlActividad.Items.Add(Lista)
                Next
            End If
        End If
    End Sub

    Private Sub FnMensaje(ByVal mensaje As String, ByVal tipo As String)
        Me.Mensaje.InnerText = mensaje
        Me.Mensaje.Attributes.Add("class", "alert alert-" + tipo + "")
    End Sub

    Private Function Validar() As Boolean
        Me.Mensaje.InnerText = ""
        Me.Mensaje.Attributes.Remove("class")
        If Me.ddlPrograma.SelectedValue = "" Then
            FnMensaje("Seleccione un Programa/Proyecto", "danger")
            Return False
        End If

        If Me.ddlActividad.SelectedValue = "" Then
            FnMensaje("Seleccione una Actividad", "danger")
            Return False
        End If

        If Me.ddlItem.SelectedValue = "" Then
            FnMensaje("Seleccione un item", "danger")
            Return False
        End If

        If Me.txtmonto.Text = "" Then
            FnMensaje("Ingrese Monto a Transferir", "danger")
            Return False
        End If

        Dim monto As String = Me.txtmonto.Text
        Dim arreglo() As String = monto.Split(".")

        If arreglo.Length > 2 Then
            FnMensaje("Ingrese Correctamente Monto a Transferir con el formato número o con dos decimales maximo.", "danger")
            Return False
        End If

        If arreglo.Length = 2 Then 'si es numero decimal de dos partes ####.##
            If arreglo(1).Length <= 2 Then ' que la segunda parte solo tenga dos decimales
                For i As Integer = 0 To arreglo.Length - 1
                    If IsNumeric(arreglo(i)) = True Then
                    Else
                        FnMensaje("Ingrese Correctamente Monto a Transferir con el formato número o con dos decimales maximo.", "danger")
                        Return False
                    End If
                Next
            Else
                FnMensaje("Ingrese Correctamente Monto a Transferir solo debe contar con dos decimales maximo.", "danger")
                Return False
            End If
        Else ' numeros enteros
            For i As Integer = 0 To arreglo.Length - 1
                If IsNumeric(arreglo(i)) = True Then
                Else
                    FnMensaje("Ingrese Correctamente Monto a Transferir con el formato número o con dos decimales maximo.", "danger")
                    Return False
                End If
            Next
        End If

           
        If Me.txtmonto.Text <= 0 Then
            FnMensaje("Ingrese Correctamente Monto a Transferir", "danger")
            Return False
        End If


        If CDec(Me.txtmonto.Text) > CDec(Replace(Me.lblpresupuesto.Text, "S/", "")) Then
            FnMensaje("El Monto : S/ " + Me.txtmonto.Text + " Excede a el monto Disponible : " + Me.lblpresupuesto.Text, "danger")
            Return False
        End If

        If Me.ddlMes.SelectedValue = "" Then
            FnMensaje("Seleccione un Mes", "danger")
            Return False
        End If

        Return True
    End Function

    'Function valida_num2decimales(ByVal numero As String)
    '    Try
    '        Dim validacion As New Regex("!/^[0-9]+([.][0-9]*)?$/")
    '        Dim test As Match
    '        test = validacion.Match(Me.txtmonto.Text.ToString)
    '    Catch ex As Exception
    '        Return False
    '    End Try

    '    Return False
    'End Function

    Protected Sub btnGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGuardar.Click
        If Session("id_per") = "" Or Request.QueryString("id") = "" Then
            Response.Redirect("../../../sinacceso.html")
        End If
        Try
            If Validar() = True Then
                Dim obj As New ClsPresupuesto
                Dim dt As New Data.DataTable
                dt = obj.TransferirMontoPresupuesto(Me.hdcodDpr.Value, Me.ddlPrograma.SelectedValue, Me.ddlActividad.SelectedValue, Me.ddlItem.SelectedValue, Me.txtmonto.Text, Me.ddlMes.SelectedValue, Session("id_per"))

                'Mensaje de Confirmación o error
                If dt.Rows(0).Item("Respuesta").ToString = "1" Then
                    FnMensaje(dt.Rows(0).Item("Mensaje").ToString, "success")
                    ClientScript.RegisterStartupScript(Me.GetType, "Mostrar", "CerrarVentana('" + dt.Rows(0).Item("Mensaje").ToString + "');", True)
                Else
                    FnMensaje(dt.Rows(0).Item("Mensaje").ToString, "danger") ' Serverdev
                    'FnMensaje("No se Puedo Importar Archivo.", "danger") ' Producción
                End If
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub btnCancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancelar.Click
        ClientScript.RegisterStartupScript(Me.GetType, "Mostrar", "Cancelar();", True)
    End Sub
End Class
