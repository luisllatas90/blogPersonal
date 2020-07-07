Imports Microsoft.VisualBasic

Public Class clsReportes

    Public strCadenaBDUSAT As String = ConfigurationManager.ConnectionStrings(1).ConnectionString

    Public Function ConsultaDatosDetalladoAlumnos(ByVal tipo As String, ByVal codigo_cac As String, ByVal cicloIng_Alu As String, ByVal sexo As String, _
    ByVal codigo_col As String, ByVal codigo_cpf As String, ByVal CONDICION As String, ByVal INGRESANTE As String, ByVal NOMBRE As String) As Data.DataTable

        Dim Obj As New ClsSqlServer(strCadenaBDUSAT)
        Return Obj.TraerDataTable("CON_DatosDeAlumnos", tipo, codigo_cac, cicloIng_Alu, sexo, codigo_col, codigo_cpf, CONDICION, NOMBRE)

    End Function

End Class
