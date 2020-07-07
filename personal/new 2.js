 $('#btnPostular').click(function() {
                    var sw = 0;
                    var arrayvalida = new Array();
                    document.getElementById('error[0]').style.visibility = 'hidden';
                    if ($("#txtSMS").val() == "") {
                        document.getElementById('error[0]').style.visibility = 'visible';
                        return false;
                    } else {
                    $('.piluku-preloader').removeClass('hidden');
                        //$("input#param0").val("gEnviarCorreoPostularOferta");
                        var form = $('#frmOfertasLaborales').serialize();
                        var boton = document.getElementById("btnPostular");
                        boton.innerHTML = "Procesando"
                        boton.disabled = true;
                        var botonC = document.getElementById("btnCancelar");
                        botonC.disabled = true;

                        $.ajax({
                            type: "GET",
                            contentType: "application/json; charset=utf-8",
                            url: "processegresado.aspx",
                            data: form,
                            dataType: "json",
                            success: function(data) {
                                fnMensaje(data[0].alert, data[0].msje);
                                f_Menu('ofertaslaborales.aspx');
                            },
                            error: function(result) {
                                //console.log(result)
                                f_Menu('ofertaslaborales.aspx');
                            }
                        });
                        $('.piluku-preloader').addClass('hidden');
                        $('#param1').val("");
                        // ini olluen 20191016
                        fnListar();
                        // fin olluen 20191016




                    }
                });

            });
