
Partial Class personal_detallepersona
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If IsPostBack = False Then                
                Dim obj As New ClsConectarDatos
                Dim objPersonal As New clsPersonal

                Dim tbl As Data.DataTable
                Dim data As Data.DataTable
                obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
                obj.AbrirConexion()

                tbl = objPersonal.ConsultarDatosPersonales(Me.Request.QueryString("codigo_per"))
                data = obj.TraerDataTable("ConsultarPersonal", "CO", Me.Request.QueryString("codigo_per"))


                If (tbl.Rows.Count > 0) Then
                    If Not (IsDBNull(tbl.Rows(0).Item("paterno")) And IsDBNull(tbl.Rows(0).Item("materno")) And IsDBNull(tbl.Rows(0).Item("nombres"))) Then
                        Me.lblNombre.Text = tbl.Rows(0).Item("paterno") + " " + tbl.Rows(0).Item("materno") + " " + tbl.Rows(0).Item("nombres")
                    End If

                    If Not (IsDBNull(tbl.Rows(0).Item("civi"))) Then
                        Me.lblEstadoCivil.Text = tbl.Rows(0).Item("civi").ToString
                    End If

                    If Not (IsDBNull(tbl.Rows(0).Item("pais"))) Then
                        Me.lblNacionalidad.Text = tbl.Rows(0).Item("pais").ToString
                    End If


                    If Not (IsDBNull(tbl.Rows(0).Item("nace"))) Then
                        Me.lblFNacimiento.Text = CDate(tbl.Rows(0).Item("nace")).ToShortDateString
                    End If

                    If Not (IsDBNull(tbl.Rows(0).Item("sexo"))) Then
                        If (tbl.Rows(0).Item("sexo").ToString = "M") Then
                            Me.lblSexo.Text = "Masculino"
                        End If

                        If (tbl.Rows(0).Item("sexo").ToString = "F") Then
                            Me.lblSexo.Text = "Femenino"
                        End If
                    End If                   

                    If Not (IsDBNull(data.Rows(0).Item("nroDocIdentidad_Per"))) Then
                        Me.lblDNI.Text = data.Rows(0).Item("nroDocIdentidad_Per")
                    End If

                    If Not (IsDBNull(data.Rows(0).Item("descripcion_Cco"))) Then
                        Me.lblCco.Text = data.Rows(0).Item("descripcion_Cco").ToString
                    End If

                    If Not (IsDBNull(data.Rows(0).Item("descripcion_Tpe"))) Then
                        Me.lblTipo.Text = data.Rows(0).Item("descripcion_Tpe").ToString
                    End If

                    If Not (IsDBNull(data.Rows(0).Item("dedicacion_Per"))) Then
                        Me.lblDedicacion.Text = data.Rows(0).Item("dedicacion_Per").ToString
                    End If

                    If Not (IsDBNull(tbl.Rows(0).Item("horas"))) Then
                        Me.lblHorasSemanales.Text = tbl.Rows(0).Item("horas").ToString
                    End If

                    If Not (IsDBNull(data.Rows(0).Item("estado_per"))) Then
                        Me.lblEstado.Text = data.Rows(0).Item("estado_per").ToString
                    End If

                    If Not (IsDBNull(data.Rows(0).Item("email_Per"))) Then
                        Me.lblCorreo.Text = data.Rows(0).Item("email_Per").ToString
                    End If

                    If Not (IsDBNull(tbl.Rows(0).Item("foto"))) Then
                        If tbl.Rows(0).Item("foto") = "" Then
                            imgfoto.Visible = True
                            Me.imgfoto.ImageUrl = "../../personal/images/fotovacia.gif"
                        Else
                            Me.imgfoto.Visible = True
                            Me.imgfoto.ImageUrl = "../../personal/imgpersonal/" & tbl.Rows(0).Item("foto")
                        End If
                    Else
                        imgfoto.Visible = True
                        Me.imgfoto.ImageUrl = "../../personal/images/fotovacia.gif"
                    End If
                End If

                obj.CerrarConexion()
                obj = Nothing
            End If

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try

    End Sub
End Class
