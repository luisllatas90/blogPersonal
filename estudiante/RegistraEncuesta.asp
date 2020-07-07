<%
		Set obj=Server.CreateObject("PryUSAT.clsAccesoDatos")
			Obj.AbrirConexion
				mensaje=obj.Ejecutar("ENC_RegistrarEncuestaPromocion",false,session("nombre_cpf"),Request.Form("cboCiclo"), Request.Form("cboEdad"),Request.Form("cboSexo"),Request.form("optTrabajos"),Request.form("optEvaluaciones"),Request.form("optTalleres"),Request.form("optAsesorias"),Request.form("optDominio"),Request.form("optTrato"),Request.form("optLlegada"),Request.form("optDisponibilidad"),Request.form("optDecano"),Request.form("optDirEscuela"),Request.form("optProfesores"),Request.form("optAutoridades"),Request.form("optSalones"),Request.form("optLaboratorio"),Request.form("optBiblioteca"),Request.form("optCafeteria"),Request.form("optBanios"),Request.form("optEspiritual"),Request.form("optComInterna"),Request.form("optBolsaTrabajo"),Request.form("optIntercambio"),Request.form("optOpiniones"),Request.form("optPtoVista"),Request.form("optIntegracion"),Request.form("optCamiseta"),Request.form("optFamilia"),Request.form("optAmigos"),Request.form("optMedios"),session("codigo_alu"), Request.form("txtcomentario"))
			Obj.CerrarConexion
		Set obj=nothing
		Response.Write("Se ha registrado la encuesta satisfactoriamente")
		response.Redirect("principal.asp")

%>