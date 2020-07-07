
Partial Class PlanProyecto_frmCalendarioAnual
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If (IsPostBack = False) Then                
                Session.Clear()
                If (Request.QueryString("ctf") = 1) Then
                    Session("id_per") = Request.QueryString("id")                    
                    Me.btnRefrescar.Attributes.Add("onclick", "location.reload();")
                    Me.btnListar.Attributes.Add("onclick", "window.open('frmListaGrupo.aspx', '', 'toolbar=no, location=no, directories=no, status=no, menubar=no, resizable=no, copyhistory=no, width=700, height=500, top=70');")
                    CargaProyectos(0)

                    If (Session("cod_pro") IsNot Nothing) Then
                        Me.dpProyecto.SelectedValue = Session("cod_pro")
                    Else
                        If Me.dpProyecto.SelectedValue.ToString <> "0" Then
                            If (Right(Me.dpProyecto.SelectedValue.ToString, 1) = "T") Then
                                Session("cod_pro") = Me.dpProyecto.SelectedValue.ToString.Substring(0, Me.dpProyecto.SelectedValue.ToString.Length - 1)
                            Else
                                Session("cod_pro") = Me.dpProyecto.SelectedValue
                            End If
                        Else
                            Session("cod_pro") = 0
                        End If
                    End If
                Else
                    Response.Redirect("frmCalendarioUser.aspx?id=" & Request.QueryString("id") & "&ctf=" & Request.QueryString("ctf"))
                End If
            End If

            If (Right(Me.dpProyecto.SelectedValue.ToString, 1) = "T") Then
                OcultaControles(False)
            Else
                OcultaControles(True)
            End If

            If (Session("cod_pro") IsNot Nothing) Then
                Me.fradetalle.Attributes("src") = "lstCalendario.aspx?pro=" & Me.dpProyecto.SelectedValue & "&titPro=" & Me.dpProyecto.SelectedItem.Text
                CargaCalendarioAnual()
            End If
        Catch ex As Exception
            Response.Write("Error al Generar el calendario: " & ex.Message)
        End Try
    End Sub

    Private Sub CargaCalendarioAnual()
        Dim strTabla As String = ""
        Dim strCabecera As String = ""
        Dim Tipo_pro As String = "T"    'T: Tipo    P: Proyecto
        Dim codigo_pro As Integer = 0
        If (Right(Me.dpProyecto.SelectedValue.ToString, 1) = "T") Then
            codigo_pro = Me.dpProyecto.SelectedValue.ToString.Substring(0, Me.dpProyecto.SelectedValue.ToString.Length - 1)
            Tipo_pro = "T"
        Else
            codigo_pro = Me.dpProyecto.SelectedValue
            Tipo_pro = "P"
        End If

        Dim obj As New ClsConectarDatos
        Dim dtAnio As New Data.DataTable
        Dim anio As Integer = Date.Today.Year
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()        
        dtAnio = obj.TraerDataTable("PLAN_RetornaAnioSeleccion", Tipo_pro, codigo_pro)
        obj.CerrarConexion()
        obj = Nothing

        If (dtAnio.Rows.Count > 0) Then
            anio = dtAnio.Rows(0).Item(0)
            hfAnio.Value = anio
            'Response.Write(codigo_pro)
        End If
        
        'Muestra el link para mostrar leyenda
        'strTabla = strTabla & "<a href='lstLeyendaCalendario.aspx?pro=" & Me.dpProyecto.SelectedValue & "&titPro=" & Me.dpProyecto.SelectedItem.Text & "' class='popup' style='color:Blue'><img border='0' src='../../images/librohoja.gif' />  <u>Mostrar Leyenda</u></a>"

        strTabla = strTabla & "<table style='font-family: Arial; font-size:x-small' cellpadding='3' cellspacing='0' width='1280px'>"
        strTabla = strTabla & "<tr><td colspan='60' align='center' style='font-size:medium;'><b>" & Me.dpProyecto.SelectedItem.Text & "</b></td></tr>"
        strTabla = strTabla & fn_cabecera(hfAnio.Value)
        strTabla = strTabla & "</table><br/><br/>"        
        Cal1.InnerHtml = strTabla
        If (Right(Me.dpProyecto.SelectedValue.ToString, 1) = "T") Then
            leyenda.InnerHtml = ListaLeyendaProyecto(Me.dpProyecto.SelectedValue.ToString.Substring(0, Me.dpProyecto.SelectedValue.ToString.Length - 1))
        Else
            leyenda.InnerHtml = ListaLeyendaProyecto(Me.dpProyecto.SelectedValue)
        End If
    End Sub

    Private Function ListaLeyendaProyecto(ByVal cod_pro As Integer) As String
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        Try
            Dim dtLeyenda As New Data.DataTable
            Dim strLeyenda As String = ""
            obj.AbrirConexion()

            If (Me.dpProyecto.SelectedItem.Text.ToString.StartsWith("[Calendario]") = True) Then
                dtLeyenda = obj.TraerDataTable("PLAN_BuscaGrupoProyectoActividad", 0, cod_pro, "")
            Else
                dtLeyenda = obj.TraerDataTable("PLAN_BuscaGrupoProyectoActividad", cod_pro, 0, "")
            End If

            obj.CerrarConexion()

            If (dtLeyenda.Rows.Count > 0) Then
                strLeyenda = "<table style='border:1px; border-style:solid; border-color:Black'>"
                For i As Integer = 0 To dtLeyenda.Rows.Count - 1 Step 3
                    If (dtLeyenda.Rows(i).Item("nombre_gpr").ToString <> "NINGUNO") Then
                        strLeyenda = strLeyenda & "<tr>"
                        strLeyenda = strLeyenda & "<td style='width:24%'>" & dtLeyenda.Rows(i).Item("nombre_gpr").ToString & "</td>"
                        strLeyenda = strLeyenda & "<td style='width:10%'><label style='border-style: solid; border-color:black; border-width:1px; background:" & dtLeyenda.Rows(i).Item("color_gpr").ToString & "'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</label></td>"
                        If i + 1 < dtLeyenda.Rows.Count Then
                            strLeyenda = strLeyenda & "<td style='width:23%'>" & dtLeyenda.Rows(i + 1).Item("nombre_gpr").ToString & "</td>"
                            strLeyenda = strLeyenda & "<td style='width:10%'><label style='border-style: solid; border-color:black; border-width:1px; background:" & dtLeyenda.Rows(i + 1).Item("color_gpr").ToString & "'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</label></td>"
                        End If
                        If i + 2 < dtLeyenda.Rows.Count Then
                            strLeyenda = strLeyenda & "<td style='width:23%'>" & dtLeyenda.Rows(i + 2).Item("nombre_gpr").ToString & "</td>"
                            strLeyenda = strLeyenda & "<td style='width:10%'><label style='border-style: solid; border-color:black; border-width:1px; background:" & dtLeyenda.Rows(i + 2).Item("color_gpr").ToString & "'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</label></td>"
                        End If
                        strLeyenda = strLeyenda & "</tr>"
                    Else
                        strLeyenda = strLeyenda & "<tr>"
                        If i + 1 < dtLeyenda.Rows.Count Then
                            strLeyenda = strLeyenda & "<td style='width:23%'>" & dtLeyenda.Rows(i + 1).Item("nombre_gpr").ToString & "</td>"
                            strLeyenda = strLeyenda & "<td style='width:10%'><label style='border-style: solid; border-color:black; border-width:1px; background:" & dtLeyenda.Rows(i + 1).Item("color_gpr").ToString & "'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</label></td>"
                        End If
                        If i + 2 < dtLeyenda.Rows.Count Then
                            strLeyenda = strLeyenda & "<td style='width:23%'>" & dtLeyenda.Rows(i + 2).Item("nombre_gpr").ToString & "</td>"
                            strLeyenda = strLeyenda & "<td style='width:10%'><label style='border-style: solid; border-color:black; border-width:1px; background:" & dtLeyenda.Rows(i + 2).Item("color_gpr").ToString & "'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</label></td>"
                        End If
                        strLeyenda = strLeyenda & "<td <td style='width:24%'></td><td style='width:10%'></td>"
                        strLeyenda = strLeyenda & "</tr>"
                    End If
                Next
                strLeyenda = strLeyenda & "</table>"
            End If
            obj = Nothing

            Return strLeyenda
        Catch ex As Exception
            Return "Error al generar Leyenda"
        End Try
    End Function

    Private Sub OcultaControles(ByVal sw As Boolean)
        Me.btnListar.Visible = False
        Me.btnAgregar.Enabled = sw
        Me.btnDuplicar.Enabled = sw
        Me.btnFinalizar.Enabled = Not sw
    End Sub

    Private Sub CargaProyectos(ByVal codigo_pro As Integer)
        Try
            Dim obj As New ClsConectarDatos
            Dim dtPlan As New Data.DataTable
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            dtPlan = obj.TraerDataTable("PLAN_BuscaProyectoxTipo", 0, codigo_pro)
            obj.CerrarConexion()

            Dim strTipo As String = ""
            For i As Integer = 0 To dtPlan.Rows.Count - 1
                If (dtPlan.Rows(i).Item("Tipo").ToString() = "[Proyecto]") Then
                    Dim itemProyecto As New ListItem
                    itemProyecto.Text = "[Calendario] " & dtPlan.Rows(i).Item("titulo_pro").ToString()
                    itemProyecto.Value = dtPlan.Rows(i).Item("codigo_pro")
                    Me.dpProyecto.Items.Add(itemProyecto)
                Else
                    Dim item As New ListItem
                    If (strTipo <> dtPlan.Rows(i).Item("Tipo").ToString()) Then
                        Dim itemTipo As New ListItem
                        strTipo = dtPlan.Rows(i).Item("Tipo").ToString()

                        itemTipo.Text = dtPlan.Rows(i).Item("Tipo").ToString()
                        itemTipo.Value = dtPlan.Rows(i).Item("CodTipo").ToString & "T"
                        Me.dpProyecto.Items.Add(itemTipo)
                    End If
                    item.Text = "[Calendario] " & dtPlan.Rows(i).Item("titulo_pro").ToString()
                    item.Value = dtPlan.Rows(i).Item("codigo_pro")
                    Me.dpProyecto.Items.Add(item)
                End If
            Next

            dtPlan.Dispose()
            obj = Nothing
        Catch ex As Exception
            Response.Write("Error al cargar proyectos")
        End Try
    End Sub

    Private Function fn_cabecera(ByVal Anio As Integer) As String
        Dim strCab As String = ""
        strCab = "<tr>"
        strCab = strCab & "<td rowspan='3' align='center' style='width:150px; background: #CEE3F6; font-weight:bold; border-bottom-width:3px; border-bottom-color:#0B0B61; border-bottom-style:solid'>ACTIVIDAD ACADÉMICA - ADMINISTRATIVA</td>"
        strCab = strCab & "<td rowspan='2' align='center' style='width:75px; background: #CEE3F6; font-weight:bold; font-size:10px' valign='bottom'>MES</td>"
        strCab = strCab & "<td colspan='9' align='center' style='width:225px; border-left-style:solid; border-left-color:#FFFFFF; border-left-width:1px; border-right-style:solid; border-right-color:#FFFFFF; border-right-width:1px; background: #CEE3F6; font-weight:bold; font-size:11px; border-bottom-width:3px; border-bottom-color:#FFFFFF; border-bottom-style:solid'>CICLO <br/>" & Anio & "-0</td>"
        strCab = strCab & "<td colspan='19' align='center' style='width:475px; border-right-style:solid; border-right-color:#FFFFFF; border-right-width:1px; background: #CEE3F6; font-weight:bold; font-size:12px; border-bottom-width:3px; border-bottom-color:#FFFFFF; border-bottom-style:solid'>CICLO " & Anio & "-I</td>"
        strCab = strCab & "<td colspan='4' align='center' style='width:100px; border-right-style:solid; border-right-color:#FFFFFF; border-right-width:1px; background: #BDBDBD; font-weight:bold; font-size:12px; border-bottom-width:3px; border-bottom-color:#FFFFFF; border-bottom-style:solid'>PERIODO VACACIONAL</td>"
        strCab = strCab & "<td colspan='17' align='center' style='width:425px; border-right-style:solid; border-right-color:#FFFFFF; border-right-width:1px; background: #CEE3F6; font-weight:bold; font-size:12px; border-bottom-width:3px; border-bottom-color:#FFFFFF; border-bottom-style:solid'>CICLO " & Anio & "-II</td>"
        strCab = strCab & "<td colspan='3' align='center' style='width:75px; border-right-style:solid; border-right-color:#FFFFFF; border-right-width:1px; background: #BDBDBD; font-weight:bold; font-size:12px; border-bottom-width:3px; border-bottom-color:#FFFFFF; border-bottom-style:solid'>P. FIN DE AÑO</td>"
        strCab = strCab & "<td colspan='4' align='center' style='width:100px; font-weight:bold; font-size:12px;'></td>"
        strCab = strCab & "</tr>"
        strCab = strCab & "<tr>"
        strCab = strCab & "<td colspan='4' align='center' style='width:100px; border-left-style:solid; border-left-color:#FFFFFF; border-left-width:1px; border-right-style:solid; border-right-color:#FFFFFF; border-right-width:1px; background: #CEE3F6; font-weight:bold; font-size:11px'>ENERO</td>"  '
        strCab = strCab & "<td colspan='4' align='center' style='width:100px; border-right-style:solid; border-right-color:#FFFFFF; border-right-width:1px; background: #CEE3F6; font-weight:bold; font-size:11px'>FEBRERO</td>"
        strCab = strCab & "<td colspan='4' align='center' style='width:100px; border-right-style:solid; border-right-color:#FFFFFF; border-right-width:1px; background: #CEE3F6; font-weight:bold; font-size:11px'>MARZO</td>"
        strCab = strCab & "<td colspan='5' align='center' style='width:125px; border-right-style:solid; border-right-color:#FFFFFF; border-right-width:1px; background: #CEE3F6; font-weight:bold; font-size:11px'>ABRIL</td>"
        strCab = strCab & "<td colspan='4' align='center' style='width:100px; border-right-style:solid; border-right-color:#FFFFFF; border-right-width:1px; background: #CEE3F6; font-weight:bold; font-size:11px'>MAYO</td>"
        strCab = strCab & "<td colspan='5' align='center' style='width:125px; border-right-style:solid; border-right-color:#FFFFFF; border-right-width:1px; background: #CEE3F6; font-weight:bold; font-size:11px'>JUNIO</td>"
        strCab = strCab & "<td colspan='4' align='center' style='width:100px; border-right-style:solid; border-right-color:#FFFFFF; border-right-width:1px; background: #CEE3F6; font-weight:bold; font-size:11px'>JULIO</td>"
        strCab = strCab & "<td colspan='4' align='center' style='width:100px; border-right-style:solid; border-right-color:#FFFFFF; border-right-width:1px; background: #CEE3F6; font-weight:bold; font-size:11px'>AGOSTO</td>"
        strCab = strCab & "<td colspan='4' align='center' style='width:125px; border-right-style:solid; border-right-color:#FFFFFF; border-right-width:1px; background: #CEE3F6; font-weight:bold; font-size:11px'>SETIEMBRE</td>"
        strCab = strCab & "<td colspan='5' align='center' style='width:125px; border-right-style:solid; border-right-color:#FFFFFF; border-right-width:1px; background: #CEE3F6; font-weight:bold; font-size:11px'>OCTUBRE</td>"
        strCab = strCab & "<td colspan='4' align='center' style='width:100px; border-right-style:solid; border-right-color:#FFFFFF; border-right-width:1px; background: #CEE3F6; font-weight:bold; font-size:11px'>NOVIEMBRE</td>"
        strCab = strCab & "<td colspan='5' align='center' style='width:125px; border-right-style:solid; border-right-color:#FFFFFF; border-right-width:1px; background: #CEE3F6; font-weight:bold; font-size:11px'>DICIEMBRE</td>"
        strCab = strCab & "<td colspan='4' align='center' style='width:100px; font-weight:bold; font-size:11px'></td>"
        strCab = strCab & "</tr>"
        strCab = strCab & "<tr>"
        strCab = strCab & "<td align='center' style='width:75px; background: #CEE3F6; font-weight:bold; font-size:10px; border-bottom-width:3px; border-bottom-color:#0B0B61; border-bottom-style:solid'>SEMANA</td>"
        For i As Integer = 1 To 56
            If (i > 52) Then
                strCab = strCab & "<td align='center' style='width:25px; color: #0B0B61; font-weight:bold; font-size:9px;'>&nbsp;&nbsp;&nbsp;</td>"
            Else
                If (i < 10) Then
                    strCab = strCab & "<td align='center' style='border-style:solid; border-width:1.5px; width:25px; color: #0B0B61; font-weight:bold; font-size:9px; border-bottom-width:3px; border-bottom-color:#0B0B61; border-bottom-style:solid'>&nbsp;" & i & "&nbsp;</td>"
                Else
                    strCab = strCab & "<td align='center' style='border-style:solid; border-width:1.5px; width:25px; color: #0B0B61; font-weight:bold; font-size:9px; border-bottom-width:3px; border-bottom-color:#0B0B61; border-bottom-style:solid'>" & i & "</td>"
                End If

            End If
        Next

        strCab = strCab & "<tr>"
        Return strCab
    End Function

    Protected Sub btnAgregar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAgregar.Click
        If (Me.dpProyecto.SelectedItem.Text.StartsWith("[Calendario]") = True) Then
            If (Me.dpProyecto.SelectedItem.Text = "TODOS") Then
                Session("cod_pro") = 0
                'OcultaControles(False)
            Else
                Session("cod_pro") = Me.dpProyecto.SelectedValue
                'OcultaControles(True)
            End If
            Response.Write("<script>window.open('frmRegistraActividad.aspx', '', 'toolbar=no, location=no, directories=no, status=no, menubar=no, resizable=no, copyhistory=no, width=700, height=500, top=70');</script>")
        Else
            Response.Write("<script>alert('Ha Seleccionado un Tipo proyecto')</script>")
        End If
    End Sub

    Protected Sub dpProyecto_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dpProyecto.SelectedIndexChanged        
        Try
            Session("cod_pro") = Me.dpProyecto.SelectedValue            
            If (Me.dpProyecto.SelectedItem.Text.StartsWith("[Calendario]") = True) Then                
                'OcultaControles(True)
            Else
                'OcultaControles(False)
            End If
            Me.fradetalle.Attributes("src") = "lstCalendario.aspx?pro=" & Me.dpProyecto.SelectedValue & "&titPro=" & Me.dpProyecto.SelectedItem.Text

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub btnDuplicar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDuplicar.Click
        Try
            If (Right(Me.dpProyecto.SelectedValue.ToString, 1) = "T") Then
                Response.Write("<script>alert('Debe seleccionar el proyecto, no el grupo.')</script>")
            Else
                Dim obj As New ClsConectarDatos
                obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
                obj.AbrirConexion()
                obj.TraerDataTable("PLAN_DuplicaCalendario", Me.dpProyecto.SelectedValue)
                obj.CerrarConexion()
                obj = Nothing
                Response.Write("<script>alert('Duplicado del año " & Me.hfAnio.Value & " finalizado.')</script>")
            End If            
        Catch ex As Exception
            Response.Write("Error al generar duplicado: " & ex.Message)
        End Try
    End Sub

    Protected Sub btnFinalizar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnFinalizar.Click
        Dim dtPlan As New Data.DataTable
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        Try
            Dim codigo_pro As Integer = 0
            Dim codigo_tpr As Integer = 0
            If (Right(Me.dpProyecto.SelectedValue.ToString, 1) = "T") Then            
                codigo_tpr = Me.dpProyecto.SelectedValue.ToString.Substring(0, Me.dpProyecto.SelectedValue.ToString.Length - 1)
                codigo_pro = Me.dpProyecto.SelectedValue.ToString.Substring(0, Me.dpProyecto.SelectedValue.ToString.Length - 1)
            Else
                codigo_tpr = 0
                codigo_pro = Me.dpProyecto.SelectedValue
            End If

            If (VerificaExiste(codigo_tpr) = False) Then
                obj.AbrirConexion()
                obj.TraerDataTable("PLAN_FinalizaTipoProyecto", codigo_pro)

                'Capturamos la lista de Proyectos
                Dim strProyectos As String = ""
                Dim dtProy As New Data.DataTable
                dtProy = obj.TraerDataTable("PLAN_RetornaProyectosxTipo", codigo_pro)
                If (dtProy.Rows.Count > 0) Then
                    For j As Integer = 0 To dtProy.Rows.Count - 2
                        strProyectos = strProyectos & ", " & dtProy.Rows(j).Item("codigo_pro").ToString
                    Next
                    strProyectos = strProyectos & ", " & dtProy.Rows(dtProy.Rows.Count - 1).Item("codigo_pro").ToString
                End If

                obj.Ejecutar("CAL_FinalizaCalendarioAnual", strProyectos)
                obj.CerrarConexion()
                obj = Nothing
                Response.Write("<script>alert('Calendario del año " & Me.hfAnio.Value & " finalizado.')</script>")
            Else
                Response.Write("<script>alert('Calendario ya ha sido finalizado anteriormente.')</script>")
            End If
        Catch ex As Exception
            Response.Write("Error al finalizar proyecto: " & ex.Message)
        End Try
    End Sub

    Private Function VerificaExiste(ByVal codigo_tpr As Integer) As Boolean
        Try
            Dim obj As New ClsConectarDatos
            Dim dt As New Data.DataTable
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            dt = obj.TraerDataTable("PLAN_VerificaCalFinalizado", codigo_tpr, 0)
            If (dt.Rows.Count = 0) Then
                obj.CerrarConexion()
                obj = Nothing
                Return False
            End If

            obj.CerrarConexion()
            obj = Nothing
            Return True
        Catch ex As Exception
            Return True
            Response.Write("Error al verificar calendario finalizado: " & ex.Message)
        End Try

    End Function

    Protected Sub btnExportar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExportar.Click
        Response.Write("<script>window.open('frmExportarCalendario.aspx?pro=" & Me.dpProyecto.SelectedValue & "&titPro=" & Me.dpProyecto.SelectedItem.Text & "', '', 'toolbar=no, location=no, directories=no, status=no, menubar=no, resizable=yes, copyhistory=no, width=800, height=600, top=50')</script>")
    End Sub
End Class
