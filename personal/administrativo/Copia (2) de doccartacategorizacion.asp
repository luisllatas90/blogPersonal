<%@language=vbscript%>
<!--#include file="fpdf.asp"-->    
<%             
        Set pdf=CreateJsObject("FPDF")
        pdf.CreatePDF()
        pdf.SetPath("fpdf/")
        pdf.SetFont "Arial","",12
        pdf.SetLeftMargin(20)
        pdf.SetRightMargin(20)
        pdf.SetTopMargin(20)
        pdf.Open()
               
        'Obtener datos de alumnos
        alumnosArray =  split(request.QueryString("alumnosArray"), ",")    
        fechaactual = FormatDateTime(Date(), 1)
        fechaactual = mid(fechaactual, instr(fechaactual, ",") + 2, len(fechaactual)-instr(fechaactual, ",") + 2)                
	    
	    For j = 0 to ubound(alumnosArray)
        
            pdf.AddPage()             
    
            codigo_alu =  mid(alumnosArray(j),3,Len(alumnosArray(j)-2))
                
            Set Obj=Server.CreateObject("PryUSAT.clsAccesoDatos")
	        Obj.AbrirConexion
		        Set rsHistorial=Obj.Consultar("EPRE_ListarPostulantes","FO","%",0,0,"%","%","%","%",1,codigo_alu)
	        Obj.CerrarConexion
	        Set Obj=nothing
	        	                	
	        If (rsHistorial.BOF and rsHistorial.EOF) then
        	    
	        else        		
                apellidos_alu = rsHistorial("apellidos")
	            nombres_alu= rsHistorial("nombres")
	            codigouniver_alu = rsHistorial("CodUniversitario")
	            nombre_cpf = rsHistorial("carrera")
	            credito_alu = replace(rsHistorial("Categorizacion"), ",", ".")	    
	            password_alu = rsHistorial("password_Alu")	            	            
                	            
	            pdf.SetFont "Arial","B",12	            
	            pdf.Cell 0,10,"Chiclayo, " & fechaactual, 0, 1, "R"
	            pdf.SetFont "Arial","",12
	            pdf.Cell 0,10,"Familia", 0, 1, "L"
	            pdf.SetFont "Arial","B",12	            
	            pdf.Cell 0,10,apellidos_alu, 0, 1
	            pdf.SetFont "Arial","",12
	            pdf.Cell 0,10,"Ciudad.-", 0, 1, "J"  
	            pdf.Ln()
	            'pdf.SetXY 15,80	            	            
	            'pdf.SetXY 10,85
	            pdf.MultiCell 0,4,"En nombre de la Universidad Católica Santo Toribio de Mogrovejo, les expreso mi cordial saludo y felicitación por el ingreso de su hijo (a) " & nombres_alu & " a la Escuela Profesional de " & nombre_cpf & ".",0,1, "J"
	            pdf.Ln()
	            pdf.MultiCell 0,4,"En la evaluación de su Expediente Socio-económico la Comisión de Pensiones ha determinado asignarle un costo de crédito por ciclo académico de S/. " & credito_alu & " Nuevos Soles. La pensión académica está en función del costo por crédito asignado y de la carga académica; la misma que podrá ser cancelada en 4 ó 5 cuotas los 30 de cada mes. Esta categorización será supervisada periódicamente y podrá suspenderse o extinguirse de conformidad con el ítem IV (j) del  Reglamento de  Pensiones 2012-I.",0,1, "J"
	            pdf.Ln()
	            pdf.MultiCell 0,4,"Su hijo (a) podrá realizar su matrícula a través de nuestra página web: www.usat.edu.pe/campusvirtual, ingresando su código universitario " & codigouniver_alu & ", cuya clave es " & password_alu & ". Asimismo, adjunto encontrará el Reglamento de  Pensiones 2012-I para su  atenta lectura. ",0,1, "J"
	            pdf.Ln()
	            pdf.MultiCell 0,4,"    Reciban nuestro agradecimiento por la confianza depositada en nuestra Universidad.",0,1	            
	            pdf.Ln()
	            pdf.Cell 0,10,"    Sin otro particular, quedo de ustedes.", 0, 1
	            'pdf.Ln()
	            pdf.Cell 0,10,"    Atentamente,", 0, 1
	            y = pdf.GetY()
	            pdf.Ln()
	            pdf.Ln()
	            pdf.Ln()
	            pdf.Image "firma.JPG",90,y+10,30	            
	            pdf.Cell 0,5,"Mgtr. Carlos Campana Marroquín", 0, 1, "C" 	            
	            pdf.Cell 0,5,"Administrador General", 0, 1, "C"  	            
	          end if        	       
            next
                
                                     
        pdf.Close()
        pdf.Output()              	   		
%>