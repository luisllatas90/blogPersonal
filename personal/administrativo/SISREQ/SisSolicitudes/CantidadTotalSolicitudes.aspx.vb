
Partial Class SisSolicitudes_CantidadTotalSolicitudes
    Inherits System.Web.UI.Page
    Private cant As Int16 = 0
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            
            Dim Obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
            ClsFunciones.LlenarListas(cboCicloAcad, Obj.TraerDataTable("ConsultarCicloAcademico", "TO", ""), "codigo_cac", "descripcion_cac", ">>Todos<<")
            'If Request.QueryString("mod") = -1 Then
            ClsFunciones.LlenarListas(cboEscuela, Obj.TraerDataTable("EVE_ConsultarCarreraProfesional", Request.QueryString("mod"), Request.QueryString("ctf"), Request.QueryString("id")), "codigo_cpf", "nombre_cpf", ">>Todos<<")
            'Else
            '    ClsFunciones.LlenarListas(cboEscuela, Obj.TraerDataTable("EVE_ConsultarCarreraProfesional", Request.QueryString("mod"), Request.QueryString("ctf"), Request.QueryString("id")), "codigo_cpf", "nombre_cpf", ">>Todos<<")
            'End If
            cboCicloAcad_SelectedIndexChanged(sender, e)
            Me.LblFecha.Text = Now.Date

        End If
    End Sub

    Private Sub Llenargrid(ByVal TablaDatos As Table)
        Dim Obj As New ClsConectarDatos
        Obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString

        Dim Solicitudes As New Data.DataTable
        '***Reporte indicando asunto***
        'Solicitudes = Obj.TraerDataTable("SOL_TotalDeSolicitudesRegistradas", 2, Me.HddAsunto.Value, Me.HddNroAsunto.Value)
        '***Reporte indicando motivos***
        Obj.AbrirConexion()
        If cboCicloAcad.SelectedValue > 0 Then
            'Solicitudes = Obj.TraerDataTable("SOL_TotalDeSolicitudesRegistradasXCicloAcademico", 3, Me.HddAsunto.Value, Me.HddNroAsunto.Value, cboCicloAcad.SelectedValue, Request.QueryString("mod"), cboEscuela.SelectedValue)
            Solicitudes = Obj.TraerDataTable("SOL_TotalDeSolicitudesRegistradasXCicloAcademico", 4, Me.HddAsunto.Value, Me.HddNroAsunto.Value, cboCicloAcad.SelectedValue, Request.QueryString("mod"), cboEscuela.SelectedValue)
        Else
            'Solicitudes = Obj.TraerDataTable("SOL_TotalDeSolicitudesRegistradas", 3, Me.HddAsunto.Value, Me.HddNroAsunto.Value, Request.QueryString("mod"), cboEscuela.SelectedValue)
            Solicitudes = Obj.TraerDataTable("SOL_TotalDeSolicitudesRegistradas", 4, Me.HddAsunto.Value, Me.HddNroAsunto.Value, Request.QueryString("mod"), cboEscuela.SelectedValue)
        End If
        Obj.CerrarConexion()
        Dim i As Int32 = 0
        Dim cont As Int32 = 0
        Dim strNum_Sol As String
        Dim Asunto As String = ""

        If Solicitudes.Rows.Count > 0 Then
            strNum_Sol = Solicitudes.Rows(0).Item("numero_sol").ToString
            Asunto = "<strong>»</strong> " + Solicitudes.Rows(0).Item("asunto").ToString

            ' DEFINICION DE LA CABECERA
            Dim FilaSol1 As New TableRow
            Dim ColumSol1 As New TableCell
            Dim ColumSol2 As New TableCell
            Dim ColumSol3 As New TableCell
            Dim ColumSol4 As New TableCell
            Dim ColumSol5 As New TableCell
            Dim ColumSol6 As New TableCell
            Dim ColumSol7 As New TableCell
            Dim ColumSol8 As New TableCell
            Dim ColumSol9 As New TableCell  'Sexo
            Dim ColumSol10 As New TableCell 'Correo electronico
            Dim ColumSol11 As New TableCell 'Telefono
            Dim ColumSol12 As New TableCell 'Direccion
            Dim ColumSol13 As New TableCell 'Institucion Tipo
            Dim ColumSol14 As New TableCell 'Institucion Nombre
            Dim ColumSol15 As New TableCell 'Cod Univ

            ColumSol1.Text = "N°"
            ColumSol2.Text = "Solicitud"
            ColumSol3.Text = "Fecha Reg"
            ColumSol4.Text = "evaluado por director"
            ColumSol5.Text = "Apellidos y Nombres"
            ColumSol6.Text = "Carrera Profesional"
            ColumSol7.Text = "Motivo"
            ColumSol8.Text = "Estado Estudiante"
            ColumSol9.Text = "Sexo"
            ColumSol10.Text = "Email"
            ColumSol11.Text = "Telefono"
            ColumSol12.Text = "Direccion"
            ColumSol13.Text = "Tipo Inst."
            ColumSol14.Text = "Nombre Inst."
            ColumSol15.Text = "Cod.Univ"

            ColumSol1.Width = 20
            ColumSol1.BorderColor = Drawing.Color.Black
            ColumSol1.BorderStyle = BorderStyle.Solid
            ColumSol1.BorderWidth = 1
            ColumSol1.HorizontalAlign = HorizontalAlign.Center
            ColumSol1.ForeColor = Drawing.Color.White
            ColumSol1.BackColor = Drawing.Color.FromName("#4182CD") '#CCCCCC")

            ColumSol2.Width = 70
            ColumSol2.BorderColor = Drawing.Color.Black
            ColumSol2.BorderStyle = BorderStyle.Solid
            ColumSol2.BorderWidth = 1
            ColumSol2.HorizontalAlign = HorizontalAlign.Center
            ColumSol2.ForeColor = Drawing.Color.White
            ColumSol2.BackColor = Drawing.Color.FromName("#4182CD")

            ColumSol3.Width = 80
            ColumSol3.BorderColor = Drawing.Color.Black
            ColumSol3.BorderStyle = BorderStyle.Solid
            ColumSol3.BorderWidth = 1
            ColumSol3.HorizontalAlign = HorizontalAlign.Center
            ColumSol3.ForeColor = Drawing.Color.White
            ColumSol3.BackColor = Drawing.Color.FromName("#4182CD")

            ColumSol4.Width = 80
            ColumSol4.BorderColor = Drawing.Color.Black
            ColumSol4.BorderStyle = BorderStyle.Solid
            ColumSol4.BorderWidth = 1
            ColumSol4.HorizontalAlign = HorizontalAlign.Center
            ColumSol4.ForeColor = Drawing.Color.White
            ColumSol4.BackColor = Drawing.Color.FromName("#4182CD")

            ColumSol5.Width = 250
            ColumSol5.BorderColor = Drawing.Color.Black
            ColumSol5.BorderStyle = BorderStyle.Solid
            ColumSol5.BorderWidth = 1
            ColumSol5.ForeColor = Drawing.Color.White
            ColumSol5.HorizontalAlign = HorizontalAlign.Center
            ColumSol5.BackColor = Drawing.Color.FromName("#4182CD")

            ColumSol6.HorizontalAlign = HorizontalAlign.Center
            ColumSol6.BorderColor = Drawing.Color.Black
            ColumSol6.BorderStyle = BorderStyle.Solid
            ColumSol6.BorderWidth = 1
            ColumSol6.ForeColor = Drawing.Color.White
            ColumSol6.BackColor = Drawing.Color.FromName("#4182CD")

            ColumSol7.Width = 100
            ColumSol7.HorizontalAlign = HorizontalAlign.Center
            ColumSol7.BorderColor = Drawing.Color.Black
            ColumSol7.BorderStyle = BorderStyle.Solid
            ColumSol7.BorderWidth = 1
            ColumSol7.ForeColor = Drawing.Color.White
            ColumSol7.BackColor = Drawing.Color.FromName("#4182CD")

            ColumSol8.HorizontalAlign = HorizontalAlign.Center
            ColumSol8.BorderColor = Drawing.Color.Black
            ColumSol8.BorderStyle = BorderStyle.Solid
            ColumSol8.BorderWidth = 1
            ColumSol8.ForeColor = Drawing.Color.White
            ColumSol8.BackColor = Drawing.Color.FromName("#4182CD")

            ColumSol9.HorizontalAlign = HorizontalAlign.Center
            ColumSol9.BorderColor = Drawing.Color.Black
            ColumSol9.BorderStyle = BorderStyle.Solid
            ColumSol9.BorderWidth = 1
            ColumSol9.ForeColor = Drawing.Color.White
            ColumSol9.BackColor = Drawing.Color.FromName("#4182CD")

            ColumSol10.HorizontalAlign = HorizontalAlign.Center
            ColumSol10.BorderColor = Drawing.Color.Black
            ColumSol10.BorderStyle = BorderStyle.Solid
            ColumSol10.BorderWidth = 1
            ColumSol10.ForeColor = Drawing.Color.White
            ColumSol10.BackColor = Drawing.Color.FromName("#4182CD")

            ColumSol11.HorizontalAlign = HorizontalAlign.Center
            ColumSol11.BorderColor = Drawing.Color.Black
            ColumSol11.BorderStyle = BorderStyle.Solid
            ColumSol11.BorderWidth = 1
            ColumSol11.ForeColor = Drawing.Color.White
            ColumSol11.BackColor = Drawing.Color.FromName("#4182CD")

            ColumSol12.HorizontalAlign = HorizontalAlign.Center
            ColumSol12.BorderColor = Drawing.Color.Black
            ColumSol12.BorderStyle = BorderStyle.Solid
            ColumSol12.BorderWidth = 1
            ColumSol12.ForeColor = Drawing.Color.White
            ColumSol12.BackColor = Drawing.Color.FromName("#4182CD")

            ColumSol13.HorizontalAlign = HorizontalAlign.Center
            ColumSol13.BorderColor = Drawing.Color.Black
            ColumSol13.BorderStyle = BorderStyle.Solid
            ColumSol13.BorderWidth = 1
            ColumSol13.ForeColor = Drawing.Color.White
            ColumSol13.BackColor = Drawing.Color.FromName("#4182CD")

            ColumSol14.HorizontalAlign = HorizontalAlign.Center
            ColumSol14.BorderColor = Drawing.Color.Black
            ColumSol14.BorderStyle = BorderStyle.Solid
            ColumSol14.BorderWidth = 1
            ColumSol14.ForeColor = Drawing.Color.White
            ColumSol14.BackColor = Drawing.Color.FromName("#4182CD")

            ColumSol15.Width = 50
            ColumSol15.BorderColor = Drawing.Color.Black
            ColumSol15.BorderStyle = BorderStyle.Solid
            ColumSol15.BorderWidth = 1
            ColumSol15.HorizontalAlign = HorizontalAlign.Center
            ColumSol15.ForeColor = Drawing.Color.White
            ColumSol15.BackColor = Drawing.Color.FromName("#4182CD")


            FilaSol1.Cells.Add(ColumSol1)
            FilaSol1.Cells.Add(ColumSol2)
            FilaSol1.Cells.Add(ColumSol3)
            FilaSol1.Cells.Add(ColumSol4)
            FilaSol1.Cells.Add(ColumSol15)
            FilaSol1.Cells.Add(ColumSol5)
            FilaSol1.Cells.Add(ColumSol6)
            FilaSol1.Cells.Add(ColumSol7)
            FilaSol1.Cells.Add(ColumSol8)
            FilaSol1.Cells.Add(ColumSol9)
            FilaSol1.Cells.Add(ColumSol10)
            FilaSol1.Cells.Add(ColumSol11)
            FilaSol1.Cells.Add(ColumSol12)
            FilaSol1.Cells.Add(ColumSol13)
            FilaSol1.Cells.Add(ColumSol14)


            FilaSol1.Height = 28
            FilaSol1.Font.Name = "Verdana"
            FilaSol1.Font.Size = 9
            FilaSol1.Font.Bold = True


            TablaDatos.Rows.Add(FilaSol1)

            For i = 0 To Solicitudes.Rows.Count - 2

                If Solicitudes.Rows(i + 1).Item("numero_sol").ToString <> strNum_Sol Then
                    ' Si es igual, --> Concatenar

                    cont += 1
                    'Asignar los nuevos parametros 
                    FilaSol1 = New TableRow
                    ColumSol1 = New TableCell
                    ColumSol2 = New TableCell
                    ColumSol3 = New TableCell
                    ColumSol4 = New TableCell
                    ColumSol5 = New TableCell
                    ColumSol6 = New TableCell
                    ColumSol7 = New TableCell
                    ColumSol8 = New TableCell
                    ColumSol9 = New TableCell
                    ColumSol10 = New TableCell
                    ColumSol11 = New TableCell
                    ColumSol12 = New TableCell
                    ColumSol13 = New TableCell
                    ColumSol14 = New TableCell
                    ColumSol15 = New TableCell


                    ColumSol1.Text = cont
                    ColumSol2.Text = Solicitudes.Rows(i).Item("numero_sol")
                    ColumSol3.Text = Solicitudes.Rows(i).Item("fecha_sol")
                    ColumSol4.Text = Solicitudes.Rows(i).Item("fecha_fin") 'tgd
                    ColumSol5.Text = Solicitudes.Rows(i).Item("alumno")
                    ColumSol6.Text = Solicitudes.Rows(i).Item("nombre_cpf")
                    ColumSol7.Text = Asunto.ToUpper
                    ColumSol8.Text = Solicitudes.Rows(i).Item("estado_Alu")
                    ColumSol9.Text = Solicitudes.Rows(i).Item("sexo_Alu")
                    ColumSol10.Text = Solicitudes.Rows(i).Item("eMail_Alu")
                    ColumSol11.Text = Solicitudes.Rows(i).Item("telefono_dal")
                    ColumSol12.Text = Solicitudes.Rows(i).Item("direccion_Dal")
                    ColumSol13.Text = Solicitudes.Rows(i).Item("Gestion_ied")
                    ColumSol14.Text = Solicitudes.Rows(i).Item("Nombre_ied")
                    ColumSol15.Text = Solicitudes.Rows(i).Item("codigoUniver_Alu")

                    ColumSol1.BorderColor = Drawing.Color.Black
                    ColumSol1.BorderStyle = BorderStyle.Solid
                    ColumSol1.BorderWidth = 1
                    ColumSol1.HorizontalAlign = HorizontalAlign.Center

                    ColumSol2.BorderColor = Drawing.Color.Black
                    ColumSol2.BorderStyle = BorderStyle.Solid
                    ColumSol2.BorderWidth = 1
                    ColumSol2.HorizontalAlign = HorizontalAlign.Center

                    ColumSol3.BorderColor = Drawing.Color.Black
                    ColumSol3.BorderStyle = BorderStyle.Solid
                    ColumSol3.BorderWidth = 1
                    ColumSol3.HorizontalAlign = HorizontalAlign.Center

                    ColumSol4.BorderColor = Drawing.Color.Black
                    ColumSol4.BorderStyle = BorderStyle.Solid
                    ColumSol4.BorderWidth = 1
                    ColumSol4.HorizontalAlign = HorizontalAlign.Center

                    ColumSol5.BorderColor = Drawing.Color.Black
                    ColumSol5.BorderStyle = BorderStyle.Solid
                    ColumSol5.BorderWidth = 1

                    'ColumSol6.Width = 250
                    ColumSol6.BorderColor = Drawing.Color.Black
                    ColumSol6.BorderStyle = BorderStyle.Solid
                    ColumSol6.BorderWidth = 1

                    ColumSol7.BorderColor = Drawing.Color.Black
                    ColumSol7.BorderStyle = BorderStyle.Solid
                    ColumSol7.BorderWidth = 1

                    ColumSol8.BorderColor = Drawing.Color.Black
                    ColumSol8.BorderStyle = BorderStyle.Solid
                    ColumSol8.BorderWidth = 1

                    ColumSol9.BorderColor = Drawing.Color.Black
                    ColumSol9.BorderStyle = BorderStyle.Solid
                    ColumSol9.BorderWidth = 1

                    ColumSol10.BorderColor = Drawing.Color.Black
                    ColumSol10.BorderStyle = BorderStyle.Solid
                    ColumSol10.BorderWidth = 1

                    ColumSol11.BorderColor = Drawing.Color.Black
                    ColumSol11.BorderStyle = BorderStyle.Solid
                    ColumSol11.BorderWidth = 1

                    ColumSol12.BorderColor = Drawing.Color.Black
                    ColumSol12.BorderStyle = BorderStyle.Solid
                    ColumSol12.BorderWidth = 1

                    ColumSol13.BorderColor = Drawing.Color.Black
                    ColumSol13.BorderStyle = BorderStyle.Solid
                    ColumSol13.BorderWidth = 1

                    ColumSol14.BorderColor = Drawing.Color.Black
                    ColumSol14.BorderStyle = BorderStyle.Solid
                    ColumSol14.BorderWidth = 1

                    ColumSol15.BorderColor = Drawing.Color.Black
                    ColumSol15.BorderStyle = BorderStyle.Solid
                    ColumSol15.BorderWidth = 1

                    FilaSol1.Cells.Add(ColumSol1)
                    FilaSol1.Cells.Add(ColumSol2)
                    FilaSol1.Cells.Add(ColumSol3)
                    FilaSol1.Cells.Add(ColumSol4)
                    FilaSol1.Cells.Add(ColumSol15)
                    FilaSol1.Cells.Add(ColumSol5)
                    FilaSol1.Cells.Add(ColumSol6)
                    FilaSol1.Cells.Add(ColumSol7)
                    FilaSol1.Cells.Add(ColumSol8)
                    FilaSol1.Cells.Add(ColumSol9)
                    FilaSol1.Cells.Add(ColumSol10)
                    FilaSol1.Cells.Add(ColumSol11)
                    FilaSol1.Cells.Add(ColumSol12)
                    FilaSol1.Cells.Add(ColumSol13)
                    FilaSol1.Cells.Add(ColumSol14)


                    FilaSol1.Height = 28
                    FilaSol1.Font.Name = "Verdana"
                    FilaSol1.Font.Size = 8

                    TablaDatos.CellPadding = 0
                    TablaDatos.CellSpacing = 0
                    TablaDatos.Rows.Add(FilaSol1)

                    ColumSol1.Dispose()
                    ColumSol2.Dispose()
                    ColumSol3.Dispose()
                    ColumSol4.Dispose()
                    ColumSol5.Dispose()
                    ColumSol6.Dispose()
                    ColumSol7.Dispose()
                    ColumSol8.Dispose()
                    ColumSol9.Dispose()
                    ColumSol10.Dispose()
                    ColumSol11.Dispose()
                    ColumSol12.Dispose()
                    ColumSol13.Dispose()
                    ColumSol14.Dispose()
                    ColumSol15.Dispose()

                    strNum_Sol = Solicitudes.Rows(i + 1).Item("numero_sol").ToString
                    Asunto = "<strong>» </strong> " + Solicitudes.Rows(i + 1).Item("asunto").ToString
                Else
                    Asunto = Asunto + " <br> <strong>» </strong>" + Solicitudes.Rows(i + 1).Item("asunto").ToString
                End If
            Next

            i = Solicitudes.Rows.Count - 1
            cont += 1
            'Asignar los nuevos parametros 
            FilaSol1 = New TableRow
            ColumSol1 = New TableCell
            ColumSol2 = New TableCell
            ColumSol3 = New TableCell
            ColumSol4 = New TableCell
            ColumSol5 = New TableCell
            ColumSol6 = New TableCell
            ColumSol7 = New TableCell
            ColumSol8 = New TableCell
            ColumSol9 = New TableCell
            ColumSol10 = New TableCell
            ColumSol11 = New TableCell
            ColumSol12 = New TableCell
            ColumSol13 = New TableCell
            ColumSol14 = New TableCell
            ColumSol15 = New TableCell

            'último registro
            ColumSol1.Text = cont
            ColumSol2.Text = Solicitudes.Rows(i).Item("numero_sol")
            ColumSol3.Text = Solicitudes.Rows(i).Item("fecha_sol")
            ColumSol4.Text = Solicitudes.Rows(i).Item("fecha_fin") 'TGD2
            ColumSol5.Text = Solicitudes.Rows(i).Item("alumno")
            ColumSol6.Text = Solicitudes.Rows(i).Item("nombre_cpf")
            ColumSol7.Text = Asunto
            ColumSol8.Text = Solicitudes.Rows(i).Item("estado_Alu")
            ColumSol9.Text = Solicitudes.Rows(i).Item("sexo_Alu")
            ColumSol10.Text = Solicitudes.Rows(i).Item("eMail_Alu")
            ColumSol11.Text = Solicitudes.Rows(i).Item("telefono_dal")
            ColumSol12.Text = Solicitudes.Rows(i).Item("direccion_Dal")
            ColumSol13.Text = Solicitudes.Rows(i).Item("Gestion_ied")
            ColumSol14.Text = Solicitudes.Rows(i).Item("Nombre_ied")
            ColumSol15.Text = Solicitudes.Rows(i).Item("codigoUniver_Alu")

            ColumSol1.BorderColor = Drawing.Color.Black
            ColumSol1.BorderStyle = BorderStyle.Solid
            ColumSol1.BorderWidth = 1
            ColumSol1.HorizontalAlign = HorizontalAlign.Center

            ColumSol2.BorderColor = Drawing.Color.Black
            ColumSol2.BorderStyle = BorderStyle.Solid
            ColumSol2.BorderWidth = 1
            ColumSol2.HorizontalAlign = HorizontalAlign.Center

            ColumSol3.BorderColor = Drawing.Color.Black
            ColumSol3.BorderStyle = BorderStyle.Solid
            ColumSol3.BorderWidth = 1
            ColumSol3.HorizontalAlign = HorizontalAlign.Center

            ColumSol4.BorderColor = Drawing.Color.Black
            ColumSol4.BorderStyle = BorderStyle.Solid
            ColumSol4.BorderWidth = 1
            ColumSol4.HorizontalAlign = HorizontalAlign.Center

            ColumSol5.BorderColor = Drawing.Color.Black
            ColumSol5.BorderStyle = BorderStyle.Solid
            ColumSol5.BorderWidth = 1

            'ColumSol6.Width = 250
            ColumSol6.BorderColor = Drawing.Color.Black
            ColumSol6.BorderStyle = BorderStyle.Solid
            ColumSol6.BorderWidth = 1

            ColumSol7.BorderColor = Drawing.Color.Black
            ColumSol7.BorderStyle = BorderStyle.Solid
            ColumSol7.BorderWidth = 1

            ColumSol8.BorderColor = Drawing.Color.Black
            ColumSol8.BorderStyle = BorderStyle.Solid
            ColumSol8.BorderWidth = 1

            ColumSol9.BorderColor = Drawing.Color.Black
            ColumSol9.BorderStyle = BorderStyle.Solid
            ColumSol9.BorderWidth = 1

            ColumSol10.BorderColor = Drawing.Color.Black
            ColumSol10.BorderStyle = BorderStyle.Solid
            ColumSol10.BorderWidth = 1

            ColumSol11.BorderColor = Drawing.Color.Black
            ColumSol11.BorderStyle = BorderStyle.Solid
            ColumSol11.BorderWidth = 1

            ColumSol12.BorderColor = Drawing.Color.Black
            ColumSol12.BorderStyle = BorderStyle.Solid
            ColumSol12.BorderWidth = 1

            ColumSol13.BorderColor = Drawing.Color.Black
            ColumSol13.BorderStyle = BorderStyle.Solid
            ColumSol13.BorderWidth = 1

            ColumSol14.BorderColor = Drawing.Color.Black
            ColumSol14.BorderStyle = BorderStyle.Solid
            ColumSol14.BorderWidth = 1

            ColumSol15.BorderColor = Drawing.Color.Black
            ColumSol15.BorderStyle = BorderStyle.Solid
            ColumSol15.BorderWidth = 1

            FilaSol1.Cells.Add(ColumSol1)
            FilaSol1.Cells.Add(ColumSol2)
            FilaSol1.Cells.Add(ColumSol3)
            FilaSol1.Cells.Add(ColumSol4)
            FilaSol1.Cells.Add(ColumSol15)
            FilaSol1.Cells.Add(ColumSol5)
            FilaSol1.Cells.Add(ColumSol6)
            FilaSol1.Cells.Add(ColumSol7)
            FilaSol1.Cells.Add(ColumSol8)
            FilaSol1.Cells.Add(ColumSol9)
            FilaSol1.Cells.Add(ColumSol10)
            FilaSol1.Cells.Add(ColumSol11)
            FilaSol1.Cells.Add(ColumSol12)
            FilaSol1.Cells.Add(ColumSol13)
            FilaSol1.Cells.Add(ColumSol14)


            FilaSol1.Height = 28
            FilaSol1.Font.Name = "Verdana"
            FilaSol1.Font.Size = 8

            TablaDatos.CellPadding = 0
            TablaDatos.CellSpacing = 0
            TablaDatos.Rows.Add(FilaSol1)

            ColumSol1.Dispose()
            ColumSol2.Dispose()
            ColumSol3.Dispose()
            ColumSol4.Dispose()
            ColumSol5.Dispose()
            ColumSol6.Dispose()
            ColumSol7.Dispose()
            ColumSol8.Dispose()
            ColumSol9.Dispose()
            ColumSol10.Dispose()
            ColumSol11.Dispose()
            ColumSol12.Dispose()
            ColumSol13.Dispose()
            ColumSol14.Dispose()
            ColumSol15.Dispose()
        Else
            Dim fila As New TableRow
            Dim colum As New TableCell
            colum.Text = "No se encontraron registros"
            colum.ForeColor = Drawing.Color.Red
            fila.Cells.Add(colum)
            TablaDatos.Rows.Add(fila)
            TablaDatos.BorderWidth = 0
        End If
    End Sub

    Protected Sub GvAsunto_DataBound(ByVal sender As Object, ByVal e As System.EventArgs) Handles GvAsunto.DataBound
        Me.LblTotal.Text = cant
    End Sub

    Protected Sub GvUnAsunto_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GvAsunto.RowDataBound
        If e.Row.RowType = DataControlRowType.Header Then
            e.Row.Cells(1).Text = "Tipo de Asunto"
            e.Row.Cells(2).Text = "Total"
            e.Row.Cells(3).Visible = False
            e.Row.Cells(4).Visible = False
        End If

        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim fila As Data.DataRowView
            fila = e.Row.DataItem
            e.Row.Cells(2).HorizontalAlign = HorizontalAlign.Right
            e.Row.Cells(3).Visible = False
            e.Row.Cells(4).Visible = False
            e.Row.Attributes.Add("OnMouseOver", "Resaltar(1,this,'S')")
            e.Row.Attributes.Add("OnMouseOut", "Resaltar(0,this,'S')")
            e.Row.Attributes.Add("OnClick", "javascript:__doPostBack('GvAsunto','Select$" & e.Row.RowIndex & "'); SeleccionarFila();")

            e.Row.Attributes.Add("Class", "Sel")
            e.Row.Attributes.Add("Typ", "Sel")

            e.Row.Style.Add("cursor", "hand")
            cant += fila.Row.Item("total")
        End If
    End Sub

    Protected Sub GvAsunto_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles GvAsunto.SelectedIndexChanged
        Me.HddAsunto.Value = Me.GvAsunto.DataKeys.Item(Me.GvAsunto.SelectedIndex).Values(0).ToString
        Me.HddNroAsunto.Value = Me.GvAsunto.DataKeys.Item(Me.GvAsunto.SelectedIndex).Values(1).ToString
        Llenargrid(Me.TblSolicitudes)
        LblTotal.Text = TblSolicitudes.Rows.Count - 1
    End Sub

    Protected Sub CmdExportar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmdExportar.Click
        Axls()
    End Sub

    Private Sub Axls()
        Response.Clear()
        Response.Buffer = True
        Response.ContentType = "application/vnd.ms-word"
        Response.AddHeader("Content-Disposition", "attachment; filename=ReporteSolicitudes_.doc")
        Response.Charset = "UTF-8"
        Response.ContentEncoding = System.Text.Encoding.Default
        Response.Write(HTML())
        Response.End()

    End Sub

    Private Function HTML() As String
        Dim Page1 As New Page()
        Dim Form2 As New HtmlForm()
        Dim grid As New Table
        Dim label As New Label

        Llenargrid(grid)

        grid.EnableViewState = False
        Page1.EnableViewState = False
        Page1.Controls.Add(Form2)
        Page1.EnableEventValidation = False

        label.Text = "NÚMERO DE SOLICITUDES REGISTRADAS DE <strong>" + Me.GvAsunto.SelectedRow.Cells(1).Text + ": " + Me.GvAsunto.SelectedRow.Cells(2).Text + "</strong> al " + Now.Date.ToShortDateString + "<br><br>"
        Form2.Controls.Add(label)
        Form2.Controls.Add(grid)

        Dim builder1 As New System.Text.StringBuilder()
        Dim writer1 As New System.IO.StringWriter(builder1)
        Dim writer2 As New HtmlTextWriter(writer1)

        Page1.DesignerInitialize()
        Page1.RenderControl(writer2)
        Page1.Dispose()
        Page1 = Nothing
        Return builder1.ToString()
    End Function

    Protected Sub cboCicloAcad_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboCicloAcad.SelectedIndexChanged
        ConsultarSolicitudes()
    End Sub


    Private Sub ConsultarSolicitudes()
        Dim Obj As New ClsConectarDatos
        Obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        Obj.AbrirConexion()
        If cboCicloAcad.SelectedValue > 0 Then
            Me.GvAsunto.DataSource = Obj.TraerDataTable("SOL_TotalDeSolicitudesRegistradasXCicloAcademico", 1, 0, 0, cboCicloAcad.SelectedValue, Request.QueryString("mod"), Me.cboEscuela.SelectedValue)
        Else
            Me.GvAsunto.DataSource = Obj.TraerDataTable("SOL_TotalDeSolicitudesRegistradas", 1, 0, 0, Request.QueryString("mod"), Me.cboEscuela.SelectedValue)
        End If
        Obj.CerrarConexion()
        Me.GvAsunto.DataBind()
    End Sub

    Protected Sub cboEscuela_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboEscuela.SelectedIndexChanged
        ConsultarSolicitudes()
    End Sub
End Class

