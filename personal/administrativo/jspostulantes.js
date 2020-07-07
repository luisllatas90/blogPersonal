function checkuncheckall() {

    var chkBox = $("input[id$='ChkAll']");

    chkBox.click(function() {
        $("#grwListaPersonas INPUT[type='checkbox']")
	                        .attr('checked', chkBox
	                        .is(':checked'));
    });

    // To deselect CheckAll when a GridView CheckBox
    // is unchecked

    $("#grwListaPersonas INPUT[type='checkbox']").click(
	            function(e) {
	                if (!$(this)[0].checked) {
	                    chkBox.attr("checked", false);
	                }
	            });
}

function DescargarCartasCategorizacion() {
    var pagina = "doccartacategorizacion.aspx?test=" + $("#txtTest").val();
    var DataKeyName = "";
    var alumnosArray = new Array();
    var id;
    var i = 0;
    var j = 0;
    //var totalRows = $("#grwListaPersonas tr").length;                    

    if ($("#ddlAccion").val() == "I") {
        var gridView1Control = $("#grwListaPersonas");
        var DataKeyName = "";
        $('#grwListaPersonas tr:has(input:checked) input[type=hidden]').each(function(i, item) {
            DataKeyName = $(item).val();
            alumnosArray[i] = DataKeyName;
            i = i + 1;
            //alert(i);
            j = 1;
        });

        //alert(j);
        //Imprimir solo si han habido registros marcados
        if (j > 0) {

            window.open(pagina + "&alumnosArray=" + alumnosArray);

            $.ajax({
                type: "POST",
                url: "frmReportePostulantes.aspx/Imprimir",
                //data: "{'valor1':'" + num1 + "', 'valor2':'" + num2 + "'}",
                data: "{'alumnosArray':'" + alumnosArray + "'}",
                dataType: "json",
                contentType: "application/json; charset=utf-8",
                async: false,
                complete: function(objeto, exito) {
                    if (exito == "success") {
                        $("#btnBuscar").click();
                    }
                },
                success: resultado,
                error: errores
            });
        }

    }
    
}

//	        function DisplayIddleWarning() {
//	            $find('warningMPE').show();
//	        }

//	        function HideIddleWarning() {	            
//	            $find('warningMPE').hide();                                                                                                                                                                                                                                                                                                                                                                                                                                                                                 
//	        }

function PintarFilaElegida(obj) {
    if (obj.style.backgroundColor == "white") {
        obj.style.backgroundColor = "#E6E6FA"//#395ACC
    }
    else {
        obj.style.backgroundColor = "white"
    }
}

function MostrarOcultarCheck(str) {

    $("#ddlProceso option:selected").each(function() {
        vp = $(this).val();

        if (vp == "-1") {

            $("#grwListaPersonas tbody tr").each(function(index) {
                $(this).find(".chkSel input:checkbox").hide();
            });
        }
        else {

            $("#ddlAccion option:selected").each(function() {
                str = $(this).val();
            });

            //Mostrar boton para Descargar cuando se accion Impresion
            if (str == "I") {
                $("#cmdDescargar").show();
                $("#btnAccion").hide();
            }
            else {
                $("#cmdDescargar").hide();
                $("#btnAccion").show();
            }

            //Opción de impresión solo cuando tienen categorización y es ingresante, y aun no se imprime
            var estado, importecategorizacion, categorizacion, imprimio;
            $("#grwListaPersonas tbody tr").each(function(index) {
                $(this).children("td").each(function(index2) {
                    switch (index2) {
                        case 11:
                            estado = $(this).text();
                            break;
                        case 14:
                            importecategorizacion = $(this).find(".cat").val();
                            break;
                        case 15:
                            imprimio = $(this).text();
                            imprimio = imprimio.replace(/^\s*|\s*$/g, "");
                            break;
                        case 16:
                            categorizacion = $(this).text();
                            categorizacion = categorizacion.replace(/^\s*|\s*$/g, "");
                            break;
                        //case 12:            
                        //alert($(this).find(".chkSel input:checkbox").is(":visible"));                                                
                        //break;            
                    }
                })

                if (imprimio == 'Impresa') {
                    $(this).find(".chkSel input:checkbox").hide();
                    $(this).find(".cat").show();
                    $(this).find(".cat").attr('disabled', 'disabled');
                }
                else {
                    //========================================================
                    //Si el participante está retirado
                    //========================================================
                    if (estado == "Retirado") {
                        $(this).find(".chkSel input:checkbox").hide();
                        $(this).find(".cat").hide();
                    }

                    //========================================================
                    //Si el participante es Postulante
                    //========================================================

                    if (estado == "Postulante" || estado == "") {
                        if (str == "C" || str == "I") {
                            $(this).find(".chkSel input:checkbox").hide();
                            $(this).find(".cat").hide();
                        }
                        else {
                            $(this).find(".chkSel input:checkbox").show();
                        }
                    }

                    //========================================================
                    //Si el participante es Ingresante
                    //========================================================

                    if (estado == "Ingresante") {

                        $(this).find(".cat").show();

                        //if (parseFloat(categorizacion) > 0) {
                        if (categorizacion == 'Si') {
                            $(this).find(".cat").attr('disabled', 'disabled');

                            if (str == "I") {
                                $(this).find(".chkSel input:checkbox").show();
                            }
                            else {
                                $(this).find(".chkSel input:checkbox").hide();
                            }
                        }
                        else {
                            if (str == "I") {
                                if (parseFloat(importecategorizacion) > 0) {
                                    $(this).find(".chkSel input:checkbox").show();
                                    $(this).find(".cat").attr('disabled', 'disabled');
                                }
                                else {
                                    $(this).find(".chkSel input:checkbox").hide();
                                    $(this).find(".cat").attr('disabled', 'disabled');
                                }
                            }
                            else {
                                $(this).find(".chkSel input:checkbox").show();                                
                                $(this).find(".cat").attr('disabled', false);
                            }
                        }
                    }
                }
            })
        }

    });

}

function resultado(msg) {
	            //msg.d tiene el resultado devuelto por el método
	            //$('#num3').val(msg.d);
    //alert(msg.d);
    //$('#form1').submit();
	        }

function errores(msg) {
	            //msg.responseText tiene el mensaje de error enviado por el servidor
	            alert('Error: ' + msg.responseText);
	        }

function exportar() {

    if ($("#ddlProceso").val() == "-1")
        var proceso = "%";
    else    
        var proceso = $("#ddlProceso").val();

    if ($("#ddlCeco").val() == "-1")
    var ceco = 0;
    else    
    var ceco = $("#ddlCeco").val();
        
    if ($("#ddlModalidad").val() == "-1")
    var modalidad = 0;
    else
        var modalidad = $("#ddlModalidad").val();
    
    if ($("#txtDNI").val() == undefined)
        var dni = "%"            
    else        
        var dni = $("#txtDNI").val();
    
    if ($("#txtCodigoUni").val() == undefined)
        var coduni = "%"           
    else        
        var coduni = $("#txtCodigoUni").val();

    if ($("#txtNombres").val() == undefined)
        var nombres = "%"        
    else        
        var nombres = $("#txtNombres").val();

    var cpf = $("#ddlEscuela").val();
    //alert("frmExportaPostulantes.aspx?pro=" + proceso + "&ceco=" + ceco + "&min=" + modalidad + "&dni=" + dni + "&coduni=" + coduni + "&nombres=" + nombres + "&estpos=" + $("#ddlEstPostulacion").val() + "&mod=" + getParameter("mod") + "&alu=0&categor=%&impre=%");
    window.open("frmExportaPostulantes.aspx?pro=" + proceso + "&ceco=" + ceco + "&min=" + modalidad + "&dni=" + dni + "&coduni=" + coduni + "&nombres=" + nombres + "&estpos=" + $("#ddlEstPostulacion").val() + "&mod=" + getParameter("mod") + "&cpf=" + cpf + "&alu=0&categor=%&impre=%", "", "toolbar=no, location=no, directories=no, status=no, menubar=no, resizable=yes, width=800, height=600, top=50");
}

function getParameter(parameter){
// Obtiene la cadena completa de URL
var url = location.href;
/* Obtiene la posicion donde se encuentra el signo ?,
ahi es donde empiezan los parametros */
var index = url.indexOf("?");
/* Obtiene la posicion donde termina el nombre del parametro
e inicia el signo = */
index = url.indexOf(parameter,index) + parameter.length;
/* Verifica que efectivamente el valor en la posicion actual
es el signo = */
if (url.charAt(index) == "="){
// Obtiene el valor del parametro
var result = url.indexOf("&",index);
if (result == -1){result=url.length;};
// Despliega el valor del parametro
//alert(url.substring(index + 1,result));
 return url.substring(index + 1, result);
}
} 