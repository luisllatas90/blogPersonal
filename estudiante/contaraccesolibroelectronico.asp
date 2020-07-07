<%
codigo_usu=session("codigo_alu")
tipo_usu="A"
if tipo_usu="" then tipo_usu="P"
session("tusu_biblioteca")= tipo_usu

if codigo_usu = "" then codigo_usu="0"
Set obj= Server.CreateObject("PryUSAT.clsAccesoDatos")
	obj.AbrirConexion			
	obj.Ejecutar "BIB_RegistrarVisita",false,tipo_usu,codigo_usu,43
	obj.CerrarConexion
Set obj=nothing

response.Redirect("URLreferida.html")
 %>