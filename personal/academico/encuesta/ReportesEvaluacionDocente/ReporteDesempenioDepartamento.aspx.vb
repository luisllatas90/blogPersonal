
Partial Class Encuesta_ReportesEvaluacionDocente_ReporteDesempenioDepartamento
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Dim Objcnx As New ClsConectarDatos
            Dim ObjFun As New ClsFunciones
            Objcnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            Objcnx.AbrirConexion()

            ObjFun.CargarListas(cboDepartamento, Objcnx.TraerDataTable("ConsultarDepartamentoAcademico", "TO", ""), "codigo_Dac", "nombre_Dac")
            ObjFun.CargarListas(cboEscuela, Objcnx.TraerDataTable("ConsultarCarreraProfesional", "TO", ""), "codigo_Cpf", "nombre_cpf")
            ObjFun.CargarListas(cboCicloAcad, Objcnx.TraerDataTable("ConsultarCicloAcademico", "TO", ""), "codigo_cac", "descripcion_cac")
            cboCicloAcad_SelectedIndexChanged(sender, e)
            'ObjFun.CargarListas(cboNroEvaluacion, Objcnx.TraerDataTable("EAD_ConsultarCronogramaEvaluacionDocente", "DD", cboCicloAcad.SelectedValue), "codigo_cev", "descripcion_cev", "<<No definido>>")
            Objcnx.CerrarConexion()
        End If

    End Sub

    Private Sub Llenargrid(ByVal TablaDatos As Table)
        Dim Obj As New ClsConectarDatos
        Obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        Dim DatosEncuesta As New Data.DataTable

        '***Reporte x Departameno x Escuela***
        If cboNroEvaluacion.SelectedValue > -1 Then
            Obj.AbrirConexion()
            DatosEncuesta = Obj.TraerDataTable("EAD_ConsultarEncuestaXDepartamentoYEscuela", cboDepartamento.SelectedValue, cboEscuela.SelectedValue, cboCicloAcad.SelectedValue, cboNroEvaluacion.SelectedValue)
            Obj.CerrarConexion()
        Else
            DatosEncuesta.Dispose()
        End If
        Dim i As Int32 = 0
        Dim cont As Int32 = 0
        Dim IntNroEncuesta As Int32
        Dim Respuesta(39) As Int16


        If DatosEncuesta.Rows.Count > 0 Then
            IntNroEncuesta = CInt(DatosEncuesta.Rows(0).Item("codigo_eed"))
            Respuesta(0) = CInt(DatosEncuesta.Rows(0).Item("respuesta_edd"))

            ' DEFINICION DE LA CABECERA
            Dim Fila1 As New TableRow
            Dim Colum1 As New TableCell
            Dim Colum2 As New TableCell
            Dim Colum3 As New TableCell
            Dim Colum4 As New TableCell
            Dim Colum5 As New TableCell
            Dim Colum6 As New TableCell
            ' Columnas para preguntas (39)
            Dim ColumP1 As New TableCell
            Dim ColumP2 As New TableCell
            Dim ColumP3 As New TableCell
            Dim ColumP4 As New TableCell
            Dim ColumP5 As New TableCell
            Dim ColumP6 As New TableCell
            Dim ColumP7 As New TableCell
            Dim ColumP8 As New TableCell
            Dim ColumP9 As New TableCell
            Dim ColumP10 As New TableCell
            Dim ColumP11 As New TableCell
            Dim ColumP12 As New TableCell
            Dim ColumP13 As New TableCell
            Dim ColumP14 As New TableCell
            Dim ColumP15 As New TableCell
            Dim ColumP16 As New TableCell
            Dim ColumP17 As New TableCell
            Dim ColumP18 As New TableCell
            Dim ColumP19 As New TableCell
            Dim ColumP20 As New TableCell
            Dim ColumP21 As New TableCell
            Dim ColumP22 As New TableCell
            Dim ColumP23 As New TableCell
            Dim ColumP24 As New TableCell
            Dim ColumP25 As New TableCell
            Dim ColumP26 As New TableCell
            Dim ColumP27 As New TableCell
            Dim ColumP28 As New TableCell
            Dim ColumP29 As New TableCell
            Dim ColumP30 As New TableCell
            Dim ColumP31 As New TableCell
            Dim ColumP32 As New TableCell
            Dim ColumP33 As New TableCell
            Dim ColumP34 As New TableCell
            Dim ColumP35 As New TableCell
            Dim ColumP36 As New TableCell
            Dim ColumP37 As New TableCell
            Dim ColumP38 As New TableCell
            Dim ColumP39 As New TableCell

            Colum1.Text = "N°"
            Colum2.Text = "Departamento"
            Colum3.Text = "Escuela"
            Colum4.Text = "Curso"
            Colum5.Text = "Grupo Horario"
            Colum6.Text = "Profesor"


            Colum1.BackColor = Drawing.Color.FromName("#99CCFF") '#CCCCCC")
            Colum2.BackColor = Drawing.Color.FromName("#99CCFF")
            Colum3.BackColor = Drawing.Color.FromName("#99CCFF")
            Colum4.BackColor = Drawing.Color.FromName("#99CCFF")
            Colum5.BackColor = Drawing.Color.FromName("#99CCFF")
            Colum6.BackColor = Drawing.Color.FromName("#99CCFF")
            ColumP1.BackColor = Drawing.Color.FromName("#99CCFF")
            ColumP2.BackColor = Drawing.Color.FromName("#99CCFF")
            ColumP3.BackColor = Drawing.Color.FromName("#99CCFF")
            ColumP4.BackColor = Drawing.Color.FromName("#99CCFF")
            ColumP5.BackColor = Drawing.Color.FromName("#99CCFF")
            ColumP6.BackColor = Drawing.Color.FromName("#99CCFF")
            ColumP7.BackColor = Drawing.Color.FromName("#99CCFF")
            ColumP8.BackColor = Drawing.Color.FromName("#99CCFF")
            ColumP9.BackColor = Drawing.Color.FromName("#99CCFF")
            ColumP10.BackColor = Drawing.Color.FromName("#99CCFF")
            ColumP11.BackColor = Drawing.Color.FromName("#99CCFF")
            ColumP12.BackColor = Drawing.Color.FromName("#99CCFF")
            ColumP13.BackColor = Drawing.Color.FromName("#99CCFF")
            ColumP14.BackColor = Drawing.Color.FromName("#99CCFF")
            ColumP15.BackColor = Drawing.Color.FromName("#99CCFF")
            ColumP16.BackColor = Drawing.Color.FromName("#99CCFF")
            ColumP17.BackColor = Drawing.Color.FromName("#99CCFF")
            ColumP18.BackColor = Drawing.Color.FromName("#99CCFF")
            ColumP19.BackColor = Drawing.Color.FromName("#99CCFF")
            ColumP20.BackColor = Drawing.Color.FromName("#99CCFF")
            ColumP21.BackColor = Drawing.Color.FromName("#99CCFF")
            ColumP22.BackColor = Drawing.Color.FromName("#99CCFF")
            ColumP23.BackColor = Drawing.Color.FromName("#99CCFF")
            ColumP24.BackColor = Drawing.Color.FromName("#99CCFF")
            ColumP25.BackColor = Drawing.Color.FromName("#99CCFF")
            ColumP26.BackColor = Drawing.Color.FromName("#99CCFF")
            ColumP27.BackColor = Drawing.Color.FromName("#99CCFF")
            ColumP28.BackColor = Drawing.Color.FromName("#99CCFF")
            ColumP29.BackColor = Drawing.Color.FromName("#99CCFF")
            ColumP30.BackColor = Drawing.Color.FromName("#99CCFF")
            ColumP31.BackColor = Drawing.Color.FromName("#99CCFF")
            ColumP32.BackColor = Drawing.Color.FromName("#99CCFF")
            ColumP33.BackColor = Drawing.Color.FromName("#99CCFF")
            ColumP34.BackColor = Drawing.Color.FromName("#99CCFF")
            ColumP35.BackColor = Drawing.Color.FromName("#99CCFF")
            ColumP36.BackColor = Drawing.Color.FromName("#99CCFF")
            ColumP37.BackColor = Drawing.Color.FromName("#99CCFF")
            ColumP38.BackColor = Drawing.Color.FromName("#99CCFF")
            ColumP39.BackColor = Drawing.Color.FromName("#99CCFF")

            ColumP1.Width = 25
            ColumP2.Width = 25
            ColumP3.Width = 25
            ColumP4.Width = 25
            ColumP5.Width = 25
            ColumP6.Width = 25
            ColumP7.Width = 25
            ColumP8.Width = 25
            ColumP9.Width = 25
            ColumP10.Width = 25
            ColumP11.Width = 25
            ColumP12.Width = 25
            ColumP13.Width = 25
            ColumP14.Width = 25
            ColumP15.Width = 25
            ColumP16.Width = 25
            ColumP17.Width = 25
            ColumP18.Width = 25
            ColumP19.Width = 25
            ColumP20.Width = 25
            ColumP21.Width = 25
            ColumP22.Width = 25
            ColumP23.Width = 25
            ColumP24.Width = 25
            ColumP25.Width = 25
            ColumP26.Width = 25
            ColumP27.Width = 25
            ColumP28.Width = 25
            ColumP29.Width = 25
            ColumP30.Width = 25
            ColumP31.Width = 25
            ColumP32.Width = 25
            ColumP33.Width = 25
            ColumP34.Width = 25
            ColumP35.Width = 25
            ColumP36.Width = 25
            ColumP37.Width = 25
            ColumP38.Width = 25
            ColumP39.Width = 25

            ColumP1.Text = "P1"
            ColumP2.Text = "P2"
            ColumP3.Text = "P3"
            ColumP4.Text = "P4"
            ColumP5.Text = "P5"
            ColumP6.Text = "P6"
            ColumP7.Text = "P7"
            ColumP8.Text = "P8"
            ColumP9.Text = "P9"
            ColumP10.Text = "P10"
            ColumP11.Text = "P11"
            ColumP12.Text = "P12"
            ColumP13.Text = "P13"
            ColumP14.Text = "P14"
            ColumP15.Text = "P15"
            ColumP16.Text = "P16"
            ColumP17.Text = "P17"
            ColumP18.Text = "P18"
            ColumP19.Text = "P19"
            ColumP20.Text = "P20"
            ColumP21.Text = "P21"
            ColumP22.Text = "P22"
            ColumP23.Text = "P23"
            ColumP24.Text = "P24"
            ColumP25.Text = "P25"
            ColumP26.Text = "P26"
            ColumP27.Text = "P27"
            ColumP28.Text = "P28"
            ColumP29.Text = "P29"
            ColumP30.Text = "P30"
            ColumP31.Text = "P31"
            ColumP32.Text = "P32"
            ColumP33.Text = "P33"
            ColumP34.Text = "P34"
            ColumP35.Text = "P35"
            ColumP36.Text = "P36"
            ColumP37.Text = "P37"
            ColumP38.Text = "P38"
            ColumP39.Text = "P39"

            '### Dar formato a las celdas a agregar ###
            DarFormatoACelda(Colum1)
            DarFormatoACelda(Colum2)
            DarFormatoACelda(Colum3)
            DarFormatoACelda(Colum4)
            DarFormatoACelda(Colum5)
            DarFormatoACelda(Colum6)
            DarFormatoACelda(ColumP1)
            DarFormatoACelda(ColumP2)
            DarFormatoACelda(ColumP3)
            DarFormatoACelda(ColumP4)
            DarFormatoACelda(ColumP5)
            DarFormatoACelda(ColumP6)
            DarFormatoACelda(ColumP7)
            DarFormatoACelda(ColumP8)
            DarFormatoACelda(ColumP9)
            DarFormatoACelda(ColumP10)
            DarFormatoACelda(ColumP11)
            DarFormatoACelda(ColumP12)
            DarFormatoACelda(ColumP13)
            DarFormatoACelda(ColumP14)
            DarFormatoACelda(ColumP15)
            DarFormatoACelda(ColumP16)
            DarFormatoACelda(ColumP17)
            DarFormatoACelda(ColumP18)
            DarFormatoACelda(ColumP19)
            DarFormatoACelda(ColumP20)
            DarFormatoACelda(ColumP21)
            DarFormatoACelda(ColumP22)
            DarFormatoACelda(ColumP23)
            DarFormatoACelda(ColumP24)
            DarFormatoACelda(ColumP25)
            DarFormatoACelda(ColumP26)
            DarFormatoACelda(ColumP27)
            DarFormatoACelda(ColumP28)
            DarFormatoACelda(ColumP29)
            DarFormatoACelda(ColumP30)
            DarFormatoACelda(ColumP31)
            DarFormatoACelda(ColumP32)
            DarFormatoACelda(ColumP33)
            DarFormatoACelda(ColumP34)
            DarFormatoACelda(ColumP35)
            DarFormatoACelda(ColumP36)
            DarFormatoACelda(ColumP37)
            DarFormatoACelda(ColumP38)
            DarFormatoACelda(ColumP39)


            Fila1.Cells.Add(Colum1)
            Fila1.Cells.Add(Colum2)
            Fila1.Cells.Add(Colum3)
            Fila1.Cells.Add(Colum4)
            Fila1.Cells.Add(Colum5)
            Fila1.Cells.Add(Colum6)
            Fila1.Cells.Add(ColumP1)
            Fila1.Cells.Add(ColumP2)
            Fila1.Cells.Add(ColumP3)
            Fila1.Cells.Add(ColumP4)
            Fila1.Cells.Add(ColumP5)
            Fila1.Cells.Add(ColumP6)
            Fila1.Cells.Add(ColumP7)
            Fila1.Cells.Add(ColumP8)
            Fila1.Cells.Add(ColumP9)
            Fila1.Cells.Add(ColumP10)
            Fila1.Cells.Add(ColumP11)
            Fila1.Cells.Add(ColumP12)
            Fila1.Cells.Add(ColumP13)
            Fila1.Cells.Add(ColumP14)
            Fila1.Cells.Add(ColumP15)
            Fila1.Cells.Add(ColumP16)
            Fila1.Cells.Add(ColumP17)
            Fila1.Cells.Add(ColumP18)
            Fila1.Cells.Add(ColumP19)
            Fila1.Cells.Add(ColumP20)
            Fila1.Cells.Add(ColumP21)
            Fila1.Cells.Add(ColumP22)
            Fila1.Cells.Add(ColumP23)
            Fila1.Cells.Add(ColumP24)
            Fila1.Cells.Add(ColumP25)
            Fila1.Cells.Add(ColumP26)
            Fila1.Cells.Add(ColumP27)
            Fila1.Cells.Add(ColumP28)
            Fila1.Cells.Add(ColumP29)
            Fila1.Cells.Add(ColumP30)
            Fila1.Cells.Add(ColumP31)
            Fila1.Cells.Add(ColumP32)
            Fila1.Cells.Add(ColumP33)
            Fila1.Cells.Add(ColumP34)
            Fila1.Cells.Add(ColumP35)
            Fila1.Cells.Add(ColumP36)
            Fila1.Cells.Add(ColumP37)
            Fila1.Cells.Add(ColumP38)
            Fila1.Cells.Add(ColumP39)

            Fila1.Font.Bold = True

            TablaDatos.Rows.Add(Fila1)

            For i = 0 To DatosEncuesta.Rows.Count - 2

                If CInt(DatosEncuesta.Rows(i + 1).Item("codigo_eed")) <> IntNroEncuesta Then
                    ' Si es igual, --> Concatenar

                    cont += 1
                    'Asignar los nuevos parametros 
                    Fila1 = New TableRow
                    Colum1 = New TableCell
                    Colum2 = New TableCell
                    Colum3 = New TableCell
                    Colum4 = New TableCell
                    Colum5 = New TableCell
                    Colum6 = New TableCell
                    ' Columnas para preguntas (39)
                    ColumP1 = New TableCell
                    ColumP2 = New TableCell
                    ColumP3 = New TableCell
                    ColumP4 = New TableCell
                    ColumP5 = New TableCell
                    ColumP6 = New TableCell
                    ColumP7 = New TableCell
                    ColumP8 = New TableCell
                    ColumP9 = New TableCell
                    ColumP10 = New TableCell
                    ColumP11 = New TableCell
                    ColumP12 = New TableCell
                    ColumP13 = New TableCell
                    ColumP14 = New TableCell
                    ColumP15 = New TableCell
                    ColumP16 = New TableCell
                    ColumP17 = New TableCell
                    ColumP18 = New TableCell
                    ColumP19 = New TableCell
                    ColumP20 = New TableCell
                    ColumP21 = New TableCell
                    ColumP22 = New TableCell
                    ColumP23 = New TableCell
                    ColumP24 = New TableCell
                    ColumP25 = New TableCell
                    ColumP26 = New TableCell
                    ColumP27 = New TableCell
                    ColumP28 = New TableCell
                    ColumP29 = New TableCell
                    ColumP30 = New TableCell
                    ColumP31 = New TableCell
                    ColumP32 = New TableCell
                    ColumP33 = New TableCell
                    ColumP34 = New TableCell
                    ColumP35 = New TableCell
                    ColumP36 = New TableCell
                    ColumP37 = New TableCell
                    ColumP38 = New TableCell
                    ColumP39 = New TableCell

                    Colum1.Text = cont
                    Colum2.Text = DatosEncuesta.Rows(i).Item("nombre_Dac")
                    Colum3.Text = DatosEncuesta.Rows(i).Item("nombre_Cpf")
                    Colum4.Text = DatosEncuesta.Rows(i).Item("nombre_Cur")
                    Colum5.Text = DatosEncuesta.Rows(i).Item("grupoHor_Cup")
                    Colum6.Text = DatosEncuesta.Rows(i).Item("Personal")

                    ColumP1.Text = Respuesta(0)
                    ColumP2.Text = Respuesta(1)
                    ColumP3.Text = Respuesta(2)
                    ColumP4.Text = Respuesta(3)
                    ColumP5.Text = Respuesta(4)
                    ColumP6.Text = Respuesta(5)
                    ColumP7.Text = Respuesta(6)
                    ColumP8.Text = Respuesta(7)
                    ColumP9.Text = Respuesta(8)
                    ColumP10.Text = Respuesta(9)
                    ColumP11.Text = Respuesta(10)
                    ColumP12.Text = Respuesta(11)
                    ColumP13.Text = Respuesta(12)
                    ColumP14.Text = Respuesta(13)
                    ColumP15.Text = Respuesta(14)
                    ColumP16.Text = Respuesta(15)
                    ColumP17.Text = Respuesta(16)
                    ColumP18.Text = Respuesta(17)
                    ColumP19.Text = Respuesta(18)
                    ColumP20.Text = Respuesta(19)
                    ColumP21.Text = Respuesta(20)
                    ColumP22.Text = Respuesta(21)
                    ColumP23.Text = Respuesta(22)
                    ColumP24.Text = Respuesta(23)
                    ColumP25.Text = Respuesta(24)
                    ColumP26.Text = Respuesta(25)
                    ColumP27.Text = Respuesta(26)
                    ColumP28.Text = Respuesta(27)
                    ColumP29.Text = Respuesta(28)
                    ColumP30.Text = Respuesta(29)
                    ColumP31.Text = Respuesta(30)
                    ColumP32.Text = Respuesta(31)
                    ColumP33.Text = Respuesta(32)
                    ColumP34.Text = Respuesta(33)
                    ColumP35.Text = Respuesta(34)
                    ColumP36.Text = Respuesta(35)
                    ColumP37.Text = Respuesta(36)
                    ColumP38.Text = Respuesta(37)
                    ColumP39.Text = Respuesta(38)

                    '### Dar formato a las celdas a agregar ###
                    DarFormatoACelda(Colum1)
                    DarFormatoACelda(Colum2)
                    DarFormatoACelda(Colum3)
                    DarFormatoACelda(Colum4)
                    DarFormatoACelda(Colum5)
                    DarFormatoACelda(Colum6)
                    DarFormatoACelda(ColumP1)
                    DarFormatoACelda(ColumP2)
                    DarFormatoACelda(ColumP3)
                    DarFormatoACelda(ColumP4)
                    DarFormatoACelda(ColumP5)
                    DarFormatoACelda(ColumP6)
                    DarFormatoACelda(ColumP7)
                    DarFormatoACelda(ColumP8)
                    DarFormatoACelda(ColumP9)
                    DarFormatoACelda(ColumP10)
                    DarFormatoACelda(ColumP11)
                    DarFormatoACelda(ColumP12)
                    DarFormatoACelda(ColumP13)
                    DarFormatoACelda(ColumP14)
                    DarFormatoACelda(ColumP15)
                    DarFormatoACelda(ColumP16)
                    DarFormatoACelda(ColumP17)
                    DarFormatoACelda(ColumP18)
                    DarFormatoACelda(ColumP19)
                    DarFormatoACelda(ColumP20)
                    DarFormatoACelda(ColumP21)
                    DarFormatoACelda(ColumP22)
                    DarFormatoACelda(ColumP23)
                    DarFormatoACelda(ColumP24)
                    DarFormatoACelda(ColumP25)
                    DarFormatoACelda(ColumP26)
                    DarFormatoACelda(ColumP27)
                    DarFormatoACelda(ColumP28)
                    DarFormatoACelda(ColumP29)
                    DarFormatoACelda(ColumP30)
                    DarFormatoACelda(ColumP31)
                    DarFormatoACelda(ColumP32)
                    DarFormatoACelda(ColumP33)
                    DarFormatoACelda(ColumP34)
                    DarFormatoACelda(ColumP35)
                    DarFormatoACelda(ColumP36)
                    DarFormatoACelda(ColumP37)
                    DarFormatoACelda(ColumP38)
                    DarFormatoACelda(ColumP39)


                    Fila1.Cells.Add(Colum1)
                    Fila1.Cells.Add(Colum2)
                    Fila1.Cells.Add(Colum3)
                    Fila1.Cells.Add(Colum4)
                    Fila1.Cells.Add(Colum5)
                    Fila1.Cells.Add(Colum6)
                    Fila1.Cells.Add(ColumP1)
                    Fila1.Cells.Add(ColumP2)
                    Fila1.Cells.Add(ColumP3)
                    Fila1.Cells.Add(ColumP4)
                    Fila1.Cells.Add(ColumP5)
                    Fila1.Cells.Add(ColumP6)
                    Fila1.Cells.Add(ColumP7)
                    Fila1.Cells.Add(ColumP8)
                    Fila1.Cells.Add(ColumP9)
                    Fila1.Cells.Add(ColumP10)
                    Fila1.Cells.Add(ColumP11)
                    Fila1.Cells.Add(ColumP12)
                    Fila1.Cells.Add(ColumP13)
                    Fila1.Cells.Add(ColumP14)
                    Fila1.Cells.Add(ColumP15)
                    Fila1.Cells.Add(ColumP16)
                    Fila1.Cells.Add(ColumP17)
                    Fila1.Cells.Add(ColumP18)
                    Fila1.Cells.Add(ColumP19)
                    Fila1.Cells.Add(ColumP20)
                    Fila1.Cells.Add(ColumP21)
                    Fila1.Cells.Add(ColumP22)
                    Fila1.Cells.Add(ColumP23)
                    Fila1.Cells.Add(ColumP24)
                    Fila1.Cells.Add(ColumP25)
                    Fila1.Cells.Add(ColumP26)
                    Fila1.Cells.Add(ColumP27)
                    Fila1.Cells.Add(ColumP28)
                    Fila1.Cells.Add(ColumP29)
                    Fila1.Cells.Add(ColumP30)
                    Fila1.Cells.Add(ColumP31)
                    Fila1.Cells.Add(ColumP32)
                    Fila1.Cells.Add(ColumP33)
                    Fila1.Cells.Add(ColumP34)
                    Fila1.Cells.Add(ColumP35)
                    Fila1.Cells.Add(ColumP36)
                    Fila1.Cells.Add(ColumP37)
                    Fila1.Cells.Add(ColumP38)
                    Fila1.Cells.Add(ColumP39)

                    TablaDatos.CellPadding = 0
                    TablaDatos.CellSpacing = 0
                    TablaDatos.Rows.Add(Fila1)

                    Colum1.Dispose()
                    Colum2.Dispose()
                    Colum3.Dispose()
                    Colum4.Dispose()
                    Colum5.Dispose()
                    Colum6.Dispose()
                    ColumP1.Dispose()
                    ColumP2.Dispose()
                    ColumP3.Dispose()
                    ColumP4.Dispose()
                    ColumP5.Dispose()
                    ColumP6.Dispose()
                    ColumP7.Dispose()
                    ColumP8.Dispose()
                    ColumP9.Dispose()
                    ColumP10.Dispose()
                    ColumP11.Dispose()
                    ColumP12.Dispose()
                    ColumP13.Dispose()
                    ColumP14.Dispose()
                    ColumP15.Dispose()
                    ColumP16.Dispose()
                    ColumP17.Dispose()
                    ColumP18.Dispose()
                    ColumP19.Dispose()
                    ColumP20.Dispose()
                    ColumP21.Dispose()
                    ColumP22.Dispose()
                    ColumP23.Dispose()
                    ColumP24.Dispose()
                    ColumP25.Dispose()
                    ColumP26.Dispose()
                    ColumP27.Dispose()
                    ColumP28.Dispose()
                    ColumP29.Dispose()
                    ColumP30.Dispose()
                    ColumP31.Dispose()
                    ColumP32.Dispose()
                    ColumP33.Dispose()
                    ColumP34.Dispose()
                    ColumP35.Dispose()
                    ColumP36.Dispose()
                    ColumP37.Dispose()
                    ColumP38.Dispose()
                    ColumP39.Dispose()

                    IntNroEncuesta = DatosEncuesta.Rows(i + 1).Item("codigo_eed").ToString
                    Respuesta(0) = DatosEncuesta.Rows(i + 1).Item("respuesta_edd")
                Else
                    Select Case DatosEncuesta.Rows(i + 1).Item("numero_eva").ToString.Trim
                        'Case "1." : Respuesta(0) = DatosEncuesta.Rows(i + 1).Item("respuesta_edd")
                        Case "2." : Respuesta(1) = DatosEncuesta.Rows(i + 1).Item("respuesta_edd")
                        Case "3." : Respuesta(2) = DatosEncuesta.Rows(i + 1).Item("respuesta_edd")
                        Case "4." : Respuesta(3) = DatosEncuesta.Rows(i + 1).Item("respuesta_edd")
                        Case "5." : Respuesta(4) = DatosEncuesta.Rows(i + 1).Item("respuesta_edd")
                        Case "6." : Respuesta(5) = DatosEncuesta.Rows(i + 1).Item("respuesta_edd")
                        Case "7." : Respuesta(6) = DatosEncuesta.Rows(i + 1).Item("respuesta_edd")
                        Case "8." : Respuesta(7) = DatosEncuesta.Rows(i + 1).Item("respuesta_edd")
                        Case "9." : Respuesta(8) = DatosEncuesta.Rows(i + 1).Item("respuesta_edd")
                        Case "10." : Respuesta(9) = DatosEncuesta.Rows(i + 1).Item("respuesta_edd")
                        Case "11." : Respuesta(10) = DatosEncuesta.Rows(i + 1).Item("respuesta_edd")
                        Case "12." : Respuesta(11) = DatosEncuesta.Rows(i + 1).Item("respuesta_edd")
                        Case "13." : Respuesta(12) = DatosEncuesta.Rows(i + 1).Item("respuesta_edd")
                        Case "14." : Respuesta(13) = DatosEncuesta.Rows(i + 1).Item("respuesta_edd")
                        Case "15." : Respuesta(14) = DatosEncuesta.Rows(i + 1).Item("respuesta_edd")
                        Case "16." : Respuesta(15) = DatosEncuesta.Rows(i + 1).Item("respuesta_edd")
                        Case "17." : Respuesta(16) = DatosEncuesta.Rows(i + 1).Item("respuesta_edd")
                        Case "18." : Respuesta(17) = DatosEncuesta.Rows(i + 1).Item("respuesta_edd")
                        Case "19." : Respuesta(18) = DatosEncuesta.Rows(i + 1).Item("respuesta_edd")
                        Case "20." : Respuesta(19) = DatosEncuesta.Rows(i + 1).Item("respuesta_edd")
                        Case "21." : Respuesta(20) = DatosEncuesta.Rows(i + 1).Item("respuesta_edd")
                        Case "22." : Respuesta(21) = DatosEncuesta.Rows(i + 1).Item("respuesta_edd")
                        Case "23." : Respuesta(22) = DatosEncuesta.Rows(i + 1).Item("respuesta_edd")
                        Case "24." : Respuesta(23) = DatosEncuesta.Rows(i + 1).Item("respuesta_edd")
                        Case "25." : Respuesta(24) = DatosEncuesta.Rows(i + 1).Item("respuesta_edd")
                        Case "26." : Respuesta(25) = DatosEncuesta.Rows(i + 1).Item("respuesta_edd")
                        Case "27." : Respuesta(26) = DatosEncuesta.Rows(i + 1).Item("respuesta_edd")
                        Case "28." : Respuesta(27) = DatosEncuesta.Rows(i + 1).Item("respuesta_edd")
                        Case "29." : Respuesta(28) = DatosEncuesta.Rows(i + 1).Item("respuesta_edd")
                        Case "30." : Respuesta(29) = DatosEncuesta.Rows(i + 1).Item("respuesta_edd")
                        Case "31." : Respuesta(30) = DatosEncuesta.Rows(i + 1).Item("respuesta_edd")
                        Case "32." : Respuesta(31) = DatosEncuesta.Rows(i + 1).Item("respuesta_edd")
                        Case "33." : Respuesta(32) = DatosEncuesta.Rows(i + 1).Item("respuesta_edd")
                        Case "34." : Respuesta(33) = DatosEncuesta.Rows(i + 1).Item("respuesta_edd")
                        Case "35." : Respuesta(34) = DatosEncuesta.Rows(i + 1).Item("respuesta_edd")
                        Case "36." : Respuesta(35) = DatosEncuesta.Rows(i + 1).Item("respuesta_edd")
                        Case "37." : Respuesta(36) = DatosEncuesta.Rows(i + 1).Item("respuesta_edd")
                        Case "38." : Respuesta(37) = DatosEncuesta.Rows(i + 1).Item("respuesta_edd")
                        Case "39." : Respuesta(38) = DatosEncuesta.Rows(i + 1).Item("respuesta_edd")
                    End Select
                End If
            Next

            i = DatosEncuesta.Rows.Count - 1
            cont += 1

            '### Asignar los nuevos parametros ###
            Fila1 = New TableRow
            Colum1 = New TableCell
            Colum2 = New TableCell
            Colum3 = New TableCell
            Colum4 = New TableCell
            Colum5 = New TableCell
            Colum6 = New TableCell
            ' Columnas para preguntas (39)
            ColumP1 = New TableCell
            ColumP2 = New TableCell
            ColumP3 = New TableCell
            ColumP4 = New TableCell
            ColumP5 = New TableCell
            ColumP6 = New TableCell
            ColumP7 = New TableCell
            ColumP8 = New TableCell
            ColumP9 = New TableCell
            ColumP10 = New TableCell
            ColumP11 = New TableCell
            ColumP12 = New TableCell
            ColumP13 = New TableCell
            ColumP14 = New TableCell
            ColumP15 = New TableCell
            ColumP16 = New TableCell
            ColumP17 = New TableCell
            ColumP18 = New TableCell
            ColumP19 = New TableCell
            ColumP20 = New TableCell
            ColumP21 = New TableCell
            ColumP22 = New TableCell
            ColumP23 = New TableCell
            ColumP24 = New TableCell
            ColumP25 = New TableCell
            ColumP26 = New TableCell
            ColumP27 = New TableCell
            ColumP28 = New TableCell
            ColumP29 = New TableCell
            ColumP30 = New TableCell
            ColumP31 = New TableCell
            ColumP32 = New TableCell
            ColumP33 = New TableCell
            ColumP34 = New TableCell
            ColumP35 = New TableCell
            ColumP36 = New TableCell
            ColumP37 = New TableCell
            ColumP38 = New TableCell
            ColumP39 = New TableCell

            Colum1.Text = cont
            Colum2.Text = DatosEncuesta.Rows(i).Item("nombre_Dac")
            Colum3.Text = DatosEncuesta.Rows(i).Item("nombre_Cpf")
            Colum4.Text = DatosEncuesta.Rows(i).Item("nombre_Cur")
            Colum5.Text = DatosEncuesta.Rows(i).Item("grupoHor_Cup")
            Colum6.Text = DatosEncuesta.Rows(i).Item("Personal")
            ColumP1.Text = Respuesta(0)
            ColumP2.Text = Respuesta(1)
            ColumP3.Text = Respuesta(2)
            ColumP4.Text = Respuesta(3)
            ColumP5.Text = Respuesta(4)
            ColumP6.Text = Respuesta(5)
            ColumP7.Text = Respuesta(6)
            ColumP8.Text = Respuesta(7)
            ColumP9.Text = Respuesta(8)
            ColumP10.Text = Respuesta(9)
            ColumP11.Text = Respuesta(10)
            ColumP12.Text = Respuesta(11)
            ColumP13.Text = Respuesta(12)
            ColumP14.Text = Respuesta(13)
            ColumP15.Text = Respuesta(14)
            ColumP16.Text = Respuesta(15)
            ColumP17.Text = Respuesta(16)
            ColumP18.Text = Respuesta(17)
            ColumP19.Text = Respuesta(18)
            ColumP20.Text = Respuesta(19)
            ColumP21.Text = Respuesta(20)
            ColumP22.Text = Respuesta(21)
            ColumP23.Text = Respuesta(22)
            ColumP24.Text = Respuesta(23)
            ColumP25.Text = Respuesta(24)
            ColumP26.Text = Respuesta(25)
            ColumP27.Text = Respuesta(26)
            ColumP28.Text = Respuesta(27)
            ColumP29.Text = Respuesta(28)
            ColumP30.Text = Respuesta(29)
            ColumP31.Text = Respuesta(30)
            ColumP32.Text = Respuesta(31)
            ColumP33.Text = Respuesta(32)
            ColumP34.Text = Respuesta(33)
            ColumP35.Text = Respuesta(34)
            ColumP36.Text = Respuesta(35)
            ColumP37.Text = Respuesta(36)
            ColumP38.Text = Respuesta(37)
            ColumP39.Text = DatosEncuesta.Rows(i).Item("respuesta_edd")

            '### Dar formato a las celdas a agregar ###
            DarFormatoACelda(Colum1)
            DarFormatoACelda(Colum2)
            DarFormatoACelda(Colum3)
            DarFormatoACelda(Colum4)
            DarFormatoACelda(Colum5)
            DarFormatoACelda(Colum6)
            DarFormatoACelda(ColumP1)
            DarFormatoACelda(ColumP2)
            DarFormatoACelda(ColumP3)
            DarFormatoACelda(ColumP4)
            DarFormatoACelda(ColumP5)
            DarFormatoACelda(ColumP6)
            DarFormatoACelda(ColumP7)
            DarFormatoACelda(ColumP8)
            DarFormatoACelda(ColumP9)
            DarFormatoACelda(ColumP10)
            DarFormatoACelda(ColumP11)
            DarFormatoACelda(ColumP12)
            DarFormatoACelda(ColumP13)
            DarFormatoACelda(ColumP14)
            DarFormatoACelda(ColumP15)
            DarFormatoACelda(ColumP16)
            DarFormatoACelda(ColumP17)
            DarFormatoACelda(ColumP18)
            DarFormatoACelda(ColumP19)
            DarFormatoACelda(ColumP20)
            DarFormatoACelda(ColumP21)
            DarFormatoACelda(ColumP22)
            DarFormatoACelda(ColumP23)
            DarFormatoACelda(ColumP24)
            DarFormatoACelda(ColumP25)
            DarFormatoACelda(ColumP26)
            DarFormatoACelda(ColumP27)
            DarFormatoACelda(ColumP28)
            DarFormatoACelda(ColumP29)
            DarFormatoACelda(ColumP30)
            DarFormatoACelda(ColumP31)
            DarFormatoACelda(ColumP32)
            DarFormatoACelda(ColumP33)
            DarFormatoACelda(ColumP34)
            DarFormatoACelda(ColumP35)
            DarFormatoACelda(ColumP36)
            DarFormatoACelda(ColumP37)
            DarFormatoACelda(ColumP38)
            DarFormatoACelda(ColumP39)


            Fila1.Cells.Add(Colum1)
            Fila1.Cells.Add(Colum2)
            Fila1.Cells.Add(Colum3)
            Fila1.Cells.Add(Colum4)
            Fila1.Cells.Add(Colum5)
            Fila1.Cells.Add(Colum6)
            Fila1.Cells.Add(ColumP1)
            Fila1.Cells.Add(ColumP2)
            Fila1.Cells.Add(ColumP3)
            Fila1.Cells.Add(ColumP4)
            Fila1.Cells.Add(ColumP5)
            Fila1.Cells.Add(ColumP6)
            Fila1.Cells.Add(ColumP7)
            Fila1.Cells.Add(ColumP8)
            Fila1.Cells.Add(ColumP9)
            Fila1.Cells.Add(ColumP10)
            Fila1.Cells.Add(ColumP11)
            Fila1.Cells.Add(ColumP12)
            Fila1.Cells.Add(ColumP13)
            Fila1.Cells.Add(ColumP14)
            Fila1.Cells.Add(ColumP15)
            Fila1.Cells.Add(ColumP16)
            Fila1.Cells.Add(ColumP17)
            Fila1.Cells.Add(ColumP18)
            Fila1.Cells.Add(ColumP19)
            Fila1.Cells.Add(ColumP20)
            Fila1.Cells.Add(ColumP21)
            Fila1.Cells.Add(ColumP22)
            Fila1.Cells.Add(ColumP23)
            Fila1.Cells.Add(ColumP24)
            Fila1.Cells.Add(ColumP25)
            Fila1.Cells.Add(ColumP26)
            Fila1.Cells.Add(ColumP27)
            Fila1.Cells.Add(ColumP28)
            Fila1.Cells.Add(ColumP29)
            Fila1.Cells.Add(ColumP30)
            Fila1.Cells.Add(ColumP31)
            Fila1.Cells.Add(ColumP32)
            Fila1.Cells.Add(ColumP33)
            Fila1.Cells.Add(ColumP34)
            Fila1.Cells.Add(ColumP35)
            Fila1.Cells.Add(ColumP36)
            Fila1.Cells.Add(ColumP37)
            Fila1.Cells.Add(ColumP38)
            Fila1.Cells.Add(ColumP39)


            TablaDatos.CellPadding = 0
            TablaDatos.CellSpacing = 0
            TablaDatos.Rows.Add(Fila1)

            Colum1.Dispose()
            Colum2.Dispose()
            Colum3.Dispose()
            Colum4.Dispose()
            Colum5.Dispose()
            Colum6.Dispose()
            ColumP1.Dispose()
            ColumP2.Dispose()
            ColumP3.Dispose()
            ColumP4.Dispose()
            ColumP5.Dispose()
            ColumP6.Dispose()
            ColumP7.Dispose()
            ColumP8.Dispose()
            ColumP9.Dispose()
            ColumP10.Dispose()
            ColumP11.Dispose()
            ColumP12.Dispose()
            ColumP13.Dispose()
            ColumP14.Dispose()
            ColumP15.Dispose()
            ColumP16.Dispose()
            ColumP17.Dispose()
            ColumP18.Dispose()
            ColumP19.Dispose()
            ColumP20.Dispose()
            ColumP21.Dispose()
            ColumP22.Dispose()
            ColumP23.Dispose()
            ColumP24.Dispose()
            ColumP25.Dispose()
            ColumP26.Dispose()
            ColumP27.Dispose()
            ColumP28.Dispose()
            ColumP29.Dispose()
            ColumP30.Dispose()
            ColumP31.Dispose()
            ColumP32.Dispose()
            ColumP33.Dispose()
            ColumP34.Dispose()
            ColumP35.Dispose()
            ColumP36.Dispose()
            ColumP37.Dispose()
            ColumP38.Dispose()
            ColumP39.Dispose()
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

    Private Sub DarFormatoACelda(ByVal colum As TableCell)
        colum.BorderColor = Drawing.Color.Black
        colum.BorderStyle = BorderStyle.Solid
        colum.BorderWidth = 1
        colum.HorizontalAlign = HorizontalAlign.Center
    End Sub

    Protected Sub CmdExportar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmdExportar.Click
        Axls()
    End Sub

    Private Sub Axls()
        Response.Clear()
        Response.Buffer = True
        Response.ContentType = "application/vnd.ms-xls"
        Response.AddHeader("Content-Disposition", "attachment; filename=ReporteEncuestas_.xls")
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

        label.Text = "REPORTE DETALLADO DE EVALUACIÓN DOCENTE"
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

    Protected Sub cmdBuscar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdBuscar.Click
        Llenargrid(Me.TblEncuestas)
    End Sub

    Protected Sub cboCicloAcad_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboCicloAcad.SelectedIndexChanged
        Dim objCnx As New ClsConectarDatos
        Dim objFun As New ClsFunciones
        Dim datosEvaluacion As New Data.DataTable
        objCnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        objCnx.AbrirConexion()
        datosEvaluacion = objCnx.TraerDataTable("EAD_ConsultarCronogramaEvaluacionDocente", "DD", cboCicloAcad.SelectedValue)
        objCnx.CerrarConexion()
        If datosEvaluacion.Rows.Count > 0 Then
            objFun.CargarListas(cboNroEvaluacion, datosEvaluacion, "codigo_cev", "descripcion_cev")
        Else
            cboNroEvaluacion.Items.Clear()
            cboNroEvaluacion.Items.Add(">>No definido<<")
            cboNroEvaluacion.Items(0).Value = -1
        End If
        objCnx = Nothing
        objFun = Nothing
    End Sub
End Class
