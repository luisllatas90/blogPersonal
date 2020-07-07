Imports System.Data
Partial Class rss_docentes
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Request.QueryString("d") IsNot Nothing Then
            Response.ContentType = "text/xml"
            Response.Charset = "utf-8"
            Response.Write("<?xml version=""1.0"" encoding=""UTF-8""?>")
            Response.ContentType = "text/xml"
            Response.Charset = "utf-8"
            Response.Write(GetXML(CInt(Request.QueryString("d").ToString), iif(Request.QueryString("n") Is Nothing, "", Request.QueryString("n")), iif(Request.QueryString("n") Is Nothing, "", Request.QueryString("a"))))
        End If
    End Sub


    Function GetXML(ByVal d As Integer, ByVal nombres As String, ByVal apellidos As String) As String
        Dim Obj As New ClsConectarDatos
        Dim datos As DataTable
        Obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        Obj.AbrirConexion()
        datos = Obj.TraerDataTable("Inv_XmlConsultarDocentes", d, nombres, apellidos)
        Obj.CerrarConexion()
        Obj = Nothing
        GetXML = "<docentesusat>"
        If datos.Rows.Count Then
            For i As Integer = 0 To datos.Rows.Count - 1
                GetXML &= "<docente id=""d" & i + 1.ToString & """>"
                GetXML &= getXML_datospersonales(datos.Rows(i))
                If Request.QueryString("n") IsNot Nothing And Request.QueryString("a") IsNot Nothing Then
                    GetXML &= getXML_titulosgradosacademicos(datos.Rows(i).Item("codigo_Per").ToString, i)
                    GetXML &= getXML_experiencia(datos.Rows(i).Item("codigo_Per").ToString, i)
                    GetXML &= getXML_distincionesypublicaciones(datos.Rows(i).Item("codigo_Per").ToString, i)
                    GetXML &= getXML_investigaciones(datos.Rows(i).Item("codigo_Per").ToString, i)
                End If
                GetXML &= "</docente>"
            Next
        End If
        GetXML &= "</docentesusat>"
    End Function

    Function getXML_datospersonales(ByVal datos As DataRow) As String
        Dim xml_datospersonales As String = ""
        getXML_datospersonales &= "<datospersonales>"
        getXML_datospersonales &= "<nombres>" & datos.Item("nombres").ToString & "</nombres>"
        getXML_datospersonales &= "<apellidos>" & datos.Item("apellidos").ToString & "</apellidos>"
        getXML_datospersonales &= "<sexo>" & datos.Item("sexo").ToString & "</sexo>"
        getXML_datospersonales &= "<nacionalidad>" & datos.Item("nacionalidad").ToString & "</nacionalidad>"
        getXML_datospersonales &= "<ruc>" & datos.Item("ruc").ToString & "</ruc>"
        getXML_datospersonales &= "<niveleducativo>" & datos.Item("niveleducativo").ToString & "</niveleducativo>"
        getXML_datospersonales &= "<perfilprofesional>" & datos.Item("perfilprofesional").ToString & "</perfilprofesional>"
        getXML_datospersonales &= "</datospersonales>"


    End Function

    Function getXML_titulosgradosacademicos(ByVal codigo_per As Integer, ByVal xI As Integer) As String
        Dim Obj As New ClsConectarDatos
        Dim xml As String = ""
        Dim datos As DataTable
        Obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString

        xml = "<titulosgradosacademicos>"
        'Titulos
        Obj.AbrirConexion()
        datos = Obj.TraerDataTable("Inv_XmlConsultarTitulo", codigo_per)
        Obj.CerrarConexion()

        If datos.Rows.Count Then
            xml &= "<titulos>"
            For i As Integer = 0 To datos.Rows.Count - 1
                xml &= "<titulo id=""t" & xI.ToString & i + 1.ToString & """>"
                xml &= "<nombretitulo>" & datos.Rows(i).Item("nombretitulo").ToString & "</nombretitulo>"
                xml &= "<institucion>" & datos.Rows(i).Item("institucion").ToString & "</institucion>"
                xml &= "<situacion>" & datos.Rows(i).Item("situacion").ToString & "</situacion>"
                xml &= "<ingreso>" & datos.Rows(i).Item("ingreso").ToString & "</ingreso>"
                xml &= "<egreso>" & datos.Rows(i).Item("egreso").ToString & "</egreso>"
                xml &= "<titulacion>" & datos.Rows(i).Item("titulacion").ToString & "</titulacion>"
                xml &= "</titulo>"
            Next
            xml &= "</titulos>"
        End If

        'grados
        Obj.AbrirConexion()
        datos = Obj.TraerDataTable("Inv_XmlConsultarGrados", codigo_per)
        Obj.CerrarConexion()
        If datos.Rows.Count Then
            xml &= "<grados>"
            For i As Integer = 0 To datos.Rows.Count - 1
                xml &= "<grado id=""g" & xI.ToString & i + 1.ToString & """>"
                xml &= "<nombregrado>" & datos.Rows(i).Item("nombregrado").ToString.Trim & "</nombregrado>"
                xml &= "<instituciongrado>" & datos.Rows(i).Item("institucion").ToString.Trim & "</instituciongrado>"
                xml &= "<situaciongrado>" & datos.Rows(i).Item("situacion").ToString.Trim & "</situaciongrado>"
                xml &= "<ingresogrado>" & datos.Rows(i).Item("ingreso").ToString.Trim & "</ingresogrado>"
                xml &= "<egresogrado>" & datos.Rows(i).Item("egreso").ToString.Trim & "</egresogrado>"
                xml &= "<titulaciongrado>" & datos.Rows(i).Item("titulacion").ToString.Trim & "</titulaciongrado>"
                xml &= "</grado>"
            Next
            xml &= "</grados>"
        End If

        'idiomas
        Obj.AbrirConexion()
        datos = Obj.TraerDataTable("Xml_ConsultarIdiomas", codigo_per)
        Obj.CerrarConexion()
        If datos.Rows.Count Then
            xml &= "<idiomas>"
            For i As Integer = 0 To datos.Rows.Count - 1
                xml &= "<idioma id=""i" & xI.ToString & i + 1.ToString & """>"
                xml &= "<nombreidioma>" & datos.Rows(i).Item("nombreidioma").ToString & "</nombreidioma>"
                xml &= "<centroestudios>" & datos.Rows(i).Item("centroestudios").ToString & "</centroestudios>"
                xml &= "<graduacion>" & datos.Rows(i).Item("graduacion").ToString & "</graduacion>"
                xml &= "<nivellectura>" & datos.Rows(i).Item("nivellectura").ToString & "</nivellectura>"
                xml &= "<nivelescritura>" & datos.Rows(i).Item("nivelescritura").ToString & "</nivelescritura>"
                xml &= "<nivelhabla>" & datos.Rows(i).Item("nivelhabla").ToString & "</nivelhabla>"
                xml &= "</idioma>"
            Next
            xml &= "</idiomas>"
        End If

        'otros
        Obj.AbrirConexion()
        datos = Obj.TraerDataTable("Xml_ConsultarOtrosEstudios", codigo_per)
        Obj.CerrarConexion()
        Obj = Nothing

        If datos.Rows.Count Then
            xml &= "<otrosestudios>"
            For i As Integer = 0 To datos.Rows.Count - 1
                xml &= "<otroestudio id=""o" & xI.ToString & i + 1.ToString & """>"
                xml &= "<area>" & datos.Rows(i).Item("area").ToString.Trim & "</area>"
                xml &= "<estudio>" & datos.Rows(i).Item("estudio").ToString.Trim & "</estudio>"
                xml &= "<institucionotro>" & datos.Rows(i).Item("institucionotro").ToString.Trim & "</institucionotro>"
                xml &= "<inicio>" & datos.Rows(i).Item("inicio").ToString.Trim & "</inicio>"
                xml &= "<fin>" & datos.Rows(i).Item("fin").ToString.Trim & "</fin>"
                xml &= "<modalidad>" & datos.Rows(i).Item("modalidad").ToString.Trim & "</modalidad>"
                xml &= "</otroestudio>"
            Next
            xml &= "</otrosestudios>"
        End If
        xml &= "</titulosgradosacademicos>"
        getXML_titulosgradosacademicos = xml
    End Function

    Function getXML_experiencia(ByVal codigo_per As Integer, ByVal xI As Integer) As String
        Dim Obj As New ClsConectarDatos
        Dim xml As String = ""
        Dim datos As DataTable
        Obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString

        xml = "<experiencialaboralprofesional>"

        'ExperienciaUniversitaria
        Obj.AbrirConexion()
        datos = Obj.TraerDataTable("XmlConsultarExpUni", codigo_per)
        Obj.CerrarConexion()

        If datos.Rows.Count Then
            xml &= "<experienciauniversitaria>"
            For i As Integer = 0 To datos.Rows.Count - 1
                xml &= "<universidad id=""u" & xI.ToString & i + 1.ToString & """>"
                xml &= "<nombreuniversidad>" & datos.Rows(i).Item("universidaduni").ToString & "</nombreuniversidad>"
                xml &= "<ciudaduni>" & datos.Rows(i).Item("ciudaduni").ToString & "</ciudaduni>"
                xml &= "<cargouni>" & datos.Rows(i).Item("cargouni").ToString & "</cargouni>"
                xml &= "<funcionuni>" & datos.Rows(i).Item("funcionuni").ToString & "</funcionuni>"
                xml &= "<fechasuni>" & datos.Rows(i).Item("fechasuni").ToString & "</fechasuni>"
                xml &= "<brevedescripcionuni>" & datos.Rows(i).Item("brevedescripcionuni").ToString & "</brevedescripcionuni>"
                xml &= "</universidad>"
            Next
            xml &= "</experienciauniversitaria>"
        End If

        'experiencia laboral
        Obj.AbrirConexion()
        datos = Obj.TraerDataTable("XmlConsultarExpLab", codigo_per)
        Obj.CerrarConexion()
        If datos.Rows.Count Then
            xml &= "<experiencialaboral>"
            For i As Integer = 0 To datos.Rows.Count - 1
                xml &= "<laboral id=""l" & xI.ToString & i + 1.ToString & """>"
                xml &= "<institucionlab>" & datos.Rows(i).Item("institucionlab").ToString & "</institucionlab>"
                xml &= "<ciudadlab>" & datos.Rows(i).Item("ciudadlab").ToString & "</ciudadlab>"
                xml &= "<cargolab>" & datos.Rows(i).Item("cargolab").ToString & "</cargolab>"
                xml &= "<funcionlab>" & datos.Rows(i).Item("funcionlab").ToString & "</funcionlab>"
                xml &= "<fechaslab>" & datos.Rows(i).Item("fechaslab").ToString & "</fechaslab>"
                xml &= "<brevedescripcionlab>" & datos.Rows(i).Item("brevedescripcionlab").ToString & "</brevedescripcionlab>"
                xml &= "</laboral>"
            Next
            xml &= "</experiencialaboral>"
        End If

        'Eventos
        Obj.AbrirConexion()
        datos = Obj.TraerDataTable("XmlConsultarParEve", codigo_per)
        Obj.CerrarConexion()
        If datos.Rows.Count Then
            xml &= "<participacioneventos>"
            For i As Integer = 0 To datos.Rows.Count - 1
                xml &= "<evento id=""e" & xI.ToString & i + 1.ToString & """>"
                xml &= "<tipoevento>" & datos.Rows(i).Item("tipoevento").ToString & "</tipoevento>"
                xml &= "<procedencia>" & datos.Rows(i).Item("procedencia").ToString & "</procedencia>"
                xml &= "<nombreevento>" & datos.Rows(i).Item("nombreevento").ToString & "</nombreevento>"
                xml &= "<tipoparticipacion>" & datos.Rows(i).Item("tipoparticipacion").ToString & "</tipoparticipacion>"
                xml &= "</evento>"
            Next
            xml &= "</participacioneventos>"
        End If
        xml &= "</experiencialaboralprofesional>"
        getXML_Experiencia = xml

    End Function

    Function getXML_distincionesypublicaciones(ByVal codigo_per As Integer, ByVal xI As Integer) As String
        Dim Obj As New ClsConectarDatos
        Dim xml As String = ""
        Dim datos As DataTable
        Obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString

        xml = "<distincionesypublicaciones>"

        'Distinciones
        Obj.AbrirConexion()
        datos = Obj.TraerDataTable("XmlConsultarDistinciones", codigo_per)
        Obj.CerrarConexion()

        If datos.Rows.Count Then
            xml &= "<distinciones>"
            For i As Integer = 0 To datos.Rows.Count - 1
                xml &= "<distincion id=""d" & xI.ToString & i + 1.ToString & """>"
                xml &= "<nombredistincion>" & datos.Rows(i).Item("nombredistincion").ToString.Trim & "</nombredistincion>"
                xml &= "<otorgadopor>" & datos.Rows(i).Item("otorgadopor").ToString.Trim & "</otorgadopor>"
                xml &= "<tipodistincion>" & datos.Rows(i).Item("tipodistincion").ToString.Trim & "</tipodistincion>"
                xml &= "<fechadistincion>" & datos.Rows(i).Item("fechadistincion").ToString.Trim & "</fechadistincion>"
                xml &= "<motivo>" & datos.Rows(i).Item("motivo_dis").ToString.Trim & "</motivo>"
                xml &= "</distincion>"
            Next
            xml &= "</distinciones>"
        End If

        'Publicaciones
        Obj.AbrirConexion()
        datos = Obj.TraerDataTable("HvPersonalPublicacion_Listar", codigo_per)
        Obj.CerrarConexion()
        If datos.Rows.Count Then
            xml &= "<publicaciones>"
            For i As Integer = 0 To datos.Rows.Count - 1
                xml &= "<publicacion id=""p" & xI.ToString & i + 1.ToString & """>"
                xml &= "<nombrepublicacion>" & datos.Rows(i).Item("nombre").ToString & "</nombrepublicacion>"
                xml &= "<editorial>" & datos.Rows(i).Item("editorial").ToString & "</editorial>"
                xml &= "<procedenciapub>" & datos.Rows(i).Item("procedencia").ToString & "</procedenciapub>"
                xml &= "<autoria>" & datos.Rows(i).Item("autoria").ToString & "</autoria>"
                xml &= "<tipopublicacion>" & datos.Rows(i).Item("tipo").ToString & "</tipopublicacion>"
                xml &= "</publicacion>"
            Next
            xml &= "</publicaciones>"
        End If     
        xml &= "</distincionesypublicaciones>"
        getXML_distincionesypublicaciones = xml
    End Function
    Function getXML_investigaciones(ByVal codigo_per As Integer, ByVal xI As Integer) As String
        Dim Obj As New ClsConectarDatos
        Dim xml As String = ""
        Dim datos As DataTable
        Obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString

        xml = "<investigaciones>"

        Obj.AbrirConexion()
        datos = Obj.TraerDataTable("XmlConsultarInvestigaciones", codigo_per)
        Obj.CerrarConexion()

        If datos.Rows.Count Then
            For i As Integer = 0 To datos.Rows.Count - 1
                xml &= "<investigacion id=""n" & xI.ToString & i + 1.ToString & """>"
                xml &= "<areainv>" & datos.Rows(i).Item("area").ToString & "</areainv>"
                xml &= "<linea>" & datos.Rows(i).Item("linea").ToString & "</linea>"
                xml &= "<titulo>" & datos.Rows(i).Item("titulo").ToString & "</titulo>"
                xml &= "<fechainicio>" & datos.Rows(i).Item("fechainicio").ToString & "</fechainicio>"
                xml &= "<fechafin>" & datos.Rows(i).Item("fechafin").ToString & "</fechafin>"
                xml &= "</investigacion>"
            Next
        End If
        xml &= "</investigaciones>"
        getXML_investigaciones = xml
    End Function
End Class
