<style type="text/css">
<!--
a {
	font-family: Verdana, Arial, Helvetica, sans-serif;
	font-size: 9px;
}
body,td,th {
	font-family: Verdana, Arial, Helvetica, sans-serif;
	font-size: 10px;
}
.Estilo1 {
	color: #FF0000;
	font-weight: bold;
}
.Estilo2 {color: #FF0000}
-->
</style>
<script>
function Validar(){

 var marcas=0
 for(i=0;i<form1.elements.length;i++){
  var obj=form1.elements[i]
  
  if(obj.type=="radio")
 	{
//		for(x=0;x<5;x++){
			if (obj.checked==true){
			  marcas=eval(marcas)+1
//			} 
		}

	}
  }
  if ((marcas<28) || (document.all.cboCiclo.value=="Seleccione") || (document.all.cboEdad.value=="Seleccione") || (document.all.cboSexo.value=="Seleccione")){
	alert("Debe Responder a todas las preguntas")
  }
  else{
    form1.action="RegistraEncuesta.asp"
	form1.submit()
  }
  
}
</script>
<form  name="form1" method="post" action="RegistraEncuesta.asp">

<table width="100%" border="0">
  <tr>
    <td bgcolor="#990000">&nbsp;</td>
  </tr>
  <tr>
    <td>&nbsp;</td>
  </tr>
  <tr>
    <td> <strong>Estimado:&nbsp;&nbsp;<font color="#003399">  <% Response.Write(session("Nombre_Usu")) %></font> </strong>  </td>
  </tr>
  <tr>
    <td><table width="100%" border="0">
      <tr>
        <td><p align="justify"><strong><span lang="es" xml:lang="es">AL CULMINAR   ESTE PRIMER SEMESTRE DEL A&Ntilde;O ACAD&Eacute;MICO EN NUESTRA UNIVERSIDAD QUEREMOS CONOCER   TUS IMPRESIONES, PERCEPCIONES Y OPINIONES SOBRE EL DESARROLLO DEL MISMO Y SOBRE   LA USAT. </span></strong><strong><span lang="es" xml:lang="es">TUS   RESPUESTAS Y OPINIONES SON DE SUMA IMPORTANCIA Y NOS PERMITIR&Aacute;N MEJORAR D&Iacute;A A   D&Iacute;A.</span></strong><br />
              <strong><span lang="es" xml:lang="es">AGRADECEREMOS   RESPONDAS ESTA BREVE ENCUESTA. </span></strong></p>
          <p align="left"><strong><span lang="es" xml:lang="es">MUCHAS   GRACIAS POR TU ATENCI&Oacute;N.</span></strong></p>


          </td>
        <td><img src="../images/LogoOficial.jpg" width="305" height="138" /></td>
      </tr>
    </table>    </td>
  </tr>
  <tr>
    <td><hr size="1" noshade="noshade" /></td>
  </tr>
  <tr>
    <td><table width="100%" border="0">
      <tr>
        <td width="8%" align="left"><strong><span lang="es" xml:lang="es">ESCUELA</span></strong></td>
        <td width="92%"><% Response.Write(session("nombre_cpf"))%></td>
      </tr>
    </table></td>
  </tr>
  <tr>
    <td><table width="100%" border="0">
      <tr>
        <td width="8%"><strong>CICLO</strong></td>
        <td width="25%"><label>
          <select name="cboCiclo" id="cboCiclo">
            <option value="Seleccione" selected="selected">Seleccione</option>
            <option value="1">I</option>
            <option value="2">II</option>
            <option value="3">III</option>
            <option value="4">IV</option>
            <option value="5">V</option>
            <option value="6">VI</option>
            <option value="7">VII</option>
            <option value="8">VIII</option>
            <option value="9">IX</option>
            <option value="10">X</option>
            <option value="11">XI</option>
            <option value="12">XII</option>
          </select>
        </label></td>
        <td width="8%"><strong>EDAD</strong></td>
        <td width="26%"><select name="cboEdad" id="cboEdad">
          <option value="Seleccione">Seleccione</option>
          <option value="16">16</option>
          <option value="17">17</option>
          <option value="18">18</option>
          <option value="19">19</option>
          <option value="20">20</option>
          <option value="21">21</option>
          <option value="22">22</option>
          <option value="23">23</option>
          <option value="24">24</option>
          <option value="25">25</option>
          <option value="26">26</option>
          <option value="27">27</option>
          <option value="28">28</option>
          <option value="29">29</option>
          <option value="30">30</option>
          <option value="31">31</option>
          <option value="32">32</option>
          <option value="33">33</option>
          <option value="34">34</option>
          <option value="35">35</option>
          <option value="36">36</option>
          <option value="37">37</option>
          <option value="38">38</option>
          <option value="39">39</option>
          <option value="40">40</option>
          <option value="41">41</option>
          <option value="42">42</option>
          <option value="43">43</option>
          <option value="44">44</option>
          <option value="45">45</option>
          <option value="46">46</option>
          <option value="47">47</option>
          <option value="48">48</option>
          <option value="49">49</option>
          <option value="50">50</option>
          <option value="51">51</option>
          <option value="52">52</option>
          <option value="53">53</option>
          <option value="54">54</option>
          <option value="55">55</option>
          <option value="56">56</option>
          <option value="57">57</option>
          <option value="58">58</option>
          <option value="59">59</option>
          <option value="60">60</option>
          <option value="61">61</option>
          <option value="62">62</option>
          <option value="63">63</option>
          <option value="64">64</option>
          <option value="65">65</option>
        </select>        </td>
        <td width="8%"><strong>SEXO</strong></td>
        <td width="25%"><select name="cboSexo" id="cboSexo">
          <option value="Seleccione">Seleccione</option>
          <option value="Masculino">Masculino</option>
          <option value="Femenino">Femenino</option>
                </select></td>
      </tr>
    </table></td>
  </tr>
  <tr>
    <td><hr size="1" noshade="noshade" /></td>
  </tr>
  <tr>
    <td><ol type="1">
      <li><strong><span lang="es" xml:lang="es">LA </span></strong><span class="Estilo1">EXIGENCIA   ACAD&Eacute;MICA</span><strong><span lang="es" xml:lang="es"> DE LA USAT   EN:</span></strong></li>
    </ol></td>
  </tr>
  <tr>
    <td><table width="95%" border="0" align="right">
      <tr>
        <td width="30%">&nbsp;</td>
        <td width="70%"><table width="100%" border="0">
          <tr align="center" valign="middle">
            <td width="20%">Malo</td>
            <td width="20%">Regular</td>
            <td width="20%">Bueno</td>
            <td width="20%">Muy Bueno </td>
            <td width="20%">Excelente</td>
          </tr>
        </table></td>
      </tr>
      <tr>
        <td><strong>TRABAJOS DE   INVESTIGACI&Oacute;N</strong></td>
        <td><table width="100%" border="0">
          <tr align="center" valign="middle">
            <td width="20%"><input name="optTrabajos" type="radio" value="malo" /></td>
            <td width="20%"><input name="optTrabajos" type="radio" value="regular" /></td>
            <td width="20%"><input name="optTrabajos" type="radio" value="bueno" /></td>
            <td width="20%"><input name="optTrabajos" type="radio" value="muybueno" /></td>
            <td width="20%"><input name="optTrabajos" type="radio" value="excelente" /></td>
          </tr>
        </table></td>
      </tr>
      <tr>
        <td><strong>EVALUACIONES</strong></td>
        <td><table width="100%" border="0">
          <tr align="center" valign="middle">
            <td width="20%"><input name="optEvaluaciones" type="radio" value="malo" /></td>
            <td width="20%"><input name="optEvaluaciones" type="radio" value="regular" /></td>
            <td width="20%"><input name="optEvaluaciones" type="radio" value="bueno" /></td>
            <td width="20%"><input name="optEvaluaciones" type="radio" value="muybueno" /></td>
            <td width="20%"><input name="optEvaluaciones" type="radio" value="excelente" /></td>
          </tr>
        </table></td>
      </tr>
      <tr>
        <td><strong>TALLERES</strong></td>
        <td><table width="100%" border="0">
          <tr align="center" valign="middle">
            <td width="20%"><input name="optTalleres" type="radio" value="malo" /></td>
            <td width="20%"><input name="optTalleres" type="radio" value="regular" /></td>
            <td width="20%"><input name="optTalleres" type="radio" value="bueno" /></td>
            <td width="20%"><input name="optTalleres" type="radio" value="muybueno" /></td>
            <td width="20%"><input name="optTalleres" type="radio" value="excelente" /></td>
          </tr>
        </table></td>
      </tr>
      <tr>
        <td><strong>ASESOR&Iacute;AS</strong></td>
        <td><table width="100%" border="0">
          <tr align="center" valign="middle">
            <td width="20%"><input name="optAsesorias" type="radio" value="malo" /></td>
            <td width="20%"><input name="optAsesorias" type="radio" value="regular" /></td>
            <td width="20%"><input name="optAsesorias" type="radio" value="bueno" /></td>
            <td width="20%"><input name="optAsesorias" type="radio" value="muybueno" /></td>
            <td width="20%"><input name="optAsesorias" type="radio" value="excelente" /></td>
          </tr>
        </table></td>
      </tr>
    </table></td>
  </tr>
  <tr>
    <td>&nbsp;</td>
  </tr>
  <tr>
    <td><ol type="1" start="2">
      <li><strong><span lang="es" xml:lang="es">LA </span></strong><span class="Estilo1">CAPACIDAD   DE LOS PROFESORES</span><strong><span lang="es" xml:lang="es"> PARA LA   ENSE&Ntilde;ANZA ES:</span></strong></li>
    </ol></td>
  </tr>
  <tr>
    <td><table width="95%" border="0" align="right">
      <tr>
        <td width="30%">&nbsp;</td>
        <td width="70%"><table width="100%" border="0">
            <tr align="center" valign="middle">
              <td width="20%">Malo</td>
              <td width="20%">Regular</td>
              <td width="20%">Bueno</td>
              <td width="20%">Muy Bueno </td>
              <td width="20%">Excelente</td>
            </tr>
        </table></td>
      </tr>
      <tr>
        <td><strong>SOBRE DOMINIO DE   LA ASIGNATURA</strong></td>
        <td><table width="100%" border="0">
            <tr align="center" valign="middle">
              <td width="20%"><input name="optDominio" type="radio" value="malo" /></td>
              <td width="20%"><input name="optDominio" type="radio" value="regular" /></td>
              <td width="20%"><input name="optDominio" type="radio" value="bueno" /></td>
              <td width="20%"><input name="optDominio" type="radio" value="muybueno" /></td>
              <td width="20%"><input name="optDominio" type="radio" value="excelente" /></td>
            </tr>
        </table></td>
      </tr>
      <tr>
        <td><strong>SOBRE TRATO AL   ESTUDIANTE</strong></td>
        <td><table width="100%" border="0">
            <tr align="center" valign="middle">
              <td width="20%"><input name="optTrato" type="radio" value="malo" /></td>
              <td width="20%"><input name="optTrato" type="radio" value="regular" /></td>
              <td width="20%"><input name="optTrato" type="radio" value="bueno" /></td>
              <td width="20%"><input name="optTrato" type="radio" value="muybueno" /></td>
              <td width="20%"><input name="optTrato" type="radio" value="excelente" /></td>
            </tr>
        </table></td>
      </tr>
      <tr>
        <td><strong>SOBRE LLEGADA AL   ESTUDIANTE</strong></td>
        <td><table width="100%" border="0">
            <tr align="center" valign="middle">
              <td width="20%"><input name="optLlegada" type="radio" value="malo" /></td>
              <td width="20%"><input name="optLlegada" type="radio" value="regular" /></td>
              <td width="20%"><input name="optLlegada" type="radio" value="bueno" /></td>
              <td width="20%"><input name="optLlegada" type="radio" value="muybueno" /></td>
              <td width="20%"><input name="optLlegada" type="radio" value="excelente" /></td>
            </tr>
        </table></td>
      </tr>
      <tr>
        <td><strong>SOBRE EL TIEMPO Y   DISPONIBILIDAD QUE BRINDA EL PROFESOR AL ESTUDIANTE</strong></td>
        <td><table width="100%" border="0">
            <tr align="center" valign="middle">
              <td width="20%"><input name="optDisponibilidad" type="radio" value="malo" /></td>
              <td width="20%"><input name="optDisponibilidad" type="radio" value="regular" /></td>
              <td width="20%"><input name="optDisponibilidad" type="radio" value="bueno" /></td>
              <td width="20%"><input name="optDisponibilidad" type="radio" value="muybueno" /></td>
              <td width="20%"><input name="optDisponibilidad" type="radio" value="excelente" /></td>
            </tr>
        </table></td>
      </tr>
    </table></td>
  </tr>
  <tr>
    <td>&nbsp;</td>
  </tr>
  <tr>
    <td><ol type="1" start="3">
      <li><strong><span lang="es" xml:lang="es">EL </span></strong><span class="Estilo1">CONTACTO   DE LAS AUTORIDADES</span><strong><span lang="es" xml:lang="es"> HACIA LOS   ESTUDIANTES ES:</span></strong></li>
    </ol></td>
  </tr>
  <tr>
    <td><table width="95%" border="0" align="right">
      <tr>
        <td width="30%">&nbsp;</td>
        <td width="70%"><table width="100%" border="0">
            <tr align="center" valign="middle">
              <td width="17%">Malo</td>
              <td width="16%">Regular</td>
              <td width="17%">Bueno</td>
              <td width="17%">Muy Bueno </td>
              <td width="16%">Excelente</td>
              <td width="17%">No lo conozco </td>
            </tr>
        </table></td>
      </tr>
      <tr>
        <td><strong>DEL DECANO DE MI   FACULTAD</strong></td>
        <td><table width="100%" border="0">
          <tr align="center" valign="middle">
            <td width="17%"><input name="optDecano" type="radio" value="malo" /></td>
            <td width="16%"><input name="optDecano" type="radio" value="regular" /></td>
            <td width="17%"><input name="optDecano" type="radio" value="bueno" /></td>
            <td width="17%"><input name="optDecano" type="radio" value="muybueno" /></td>
            <td width="16%"><input name="optDecano" type="radio" value="excelente" /></td>
            <td width="17%"><input name="optDecano" type="radio" value="no lo conozco" /></td>
          </tr>
        </table></td>
      </tr>
      <tr>
        <td><strong>DEL DIRECTOR DE   MI ESCUELA</strong></td>
        <td><table width="100%" border="0">
          <tr align="center" valign="middle">
            <td width="17%"><input name="optDirEscuela" type="radio" value="malo" /></td>
            <td width="16%"><input name="optDirEscuela" type="radio" value="regular" /></td>
            <td width="17%"><input name="optDirEscuela" type="radio" value="bueno" /></td>
            <td width="17%"><input name="optDirEscuela" type="radio" value="muybueno" /></td>
            <td width="16%"><input name="optDirEscuela" type="radio" /></td>
            <td width="17%"><input name="optDirEscuela" type="radio" value="no lo conozco" /></td>
          </tr>
        </table>
          </td>
      </tr>
      <tr>
        <td><strong>DE LOS   PROFESORES</strong></td>
        <td><table width="100%" border="0">
          <tr align="center" valign="middle">
            <td width="17%"><input name="optProfesores" type="radio" value="malo" /></td>
            <td width="16%"><input name="optProfesores" type="radio" value="regular" /></td>
            <td width="17%"><input name="optProfesores" type="radio" value="bueno" /></td>
            <td width="17%"><input name="optProfesores" type="radio" value="muybueno" /></td>
            <td width="16%"><input name="optProfesores" type="radio" value="excelente" /></td>
            <td width="17%"><input name="optProfesores" type="radio" value="no lo conozco" /></td>
          </tr>
        </table></td>
      </tr>
      <tr>
        <td><strong>DE OTRAS   AUTORIDADES</strong></td>
        <td><table width="100%" border="0">
          <tr align="center" valign="middle">
            <td width="17%"><input name="optAutoridades" type="radio" value="malo" /></td>
            <td width="16%"><input name="optAutoridades" type="radio" value="regular" /></td>
            <td width="17%"><input name="optAutoridades" type="radio" value="bueno" /></td>
            <td width="17%"><input name="optAutoridades" type="radio" value="muybueno" /></td>
            <td width="16%"><input name="optAutoridades" type="radio" value="excelente" /></td>
            <td width="17%"><input name="optAutoridades" type="radio" value="no lo conozco" /></td>
          </tr>
        </table></td>
      </tr>
    </table></td>
  </tr>
  <tr>
    <td>&nbsp;</td>
  </tr>
  <tr>
    <td><ol type="1" start="4">
      <li><strong><span lang="es" xml:lang="es">LA </span></strong><span class="Estilo2"><strong>INFRAESTRUCTURA</strong> <strong>E   IMPLEMENTACI&Oacute;N</strong></span><strong><span lang="es" xml:lang="es"> DE LA   UNIVERSIDAD ES:</span></strong></li>
    </ol></td>
  </tr>
  <tr>
    <td><table width="95%" border="0" align="right">
      <tr>
        <td width="30%">&nbsp;</td>
        <td width="70%"><table width="100%" border="0">
            <tr align="center" valign="middle">
              <td width="20%">Malo</td>
              <td width="20%">Regular</td>
              <td width="20%">Bueno</td>
              <td width="20%">Muy Bueno </td>
              <td width="20%">Excelente</td>
            </tr>
        </table></td>
      </tr>
      <tr>
        <td><strong>SALONES</strong></td>
        <td><table width="100%" border="0">
            <tr align="center" valign="middle">
              <td width="20%"><input name="optSalones" type="radio" value="malo" /></td>
              <td width="20%"><input name="optSalones" type="radio" value="regular" /></td>
              <td width="20%"><input name="optSalones" type="radio" value="bueno" /></td>
              <td width="20%"><input name="optSalones" type="radio" value="muybueno" /></td>
              <td width="20%"><input name="optSalones" type="radio" value="excelente" /></td>
            </tr>
        </table></td>
      </tr>
      <tr>
        <td><strong>LABORATORIO</strong></td>
        <td><table width="100%" border="0">
            <tr align="center" valign="middle">
              <td width="20%"><input name="optLaboratorio" type="radio" value="malo" /></td>
              <td width="20%"><input name="optLaboratorio" type="radio" value="regular" /></td>
              <td width="20%"><input name="optLaboratorio" type="radio" value="bueno" /></td>
              <td width="20%"><input name="optLaboratorio" type="radio" value="muybueno" /></td>
              <td width="20%"><input name="optLaboratorio" type="radio" value="excelente" /></td>
            </tr>
        </table></td>
      </tr>
      <tr>
        <td><strong>BIBLIOTECA</strong></td>
        <td><table width="100%" border="0">
            <tr align="center" valign="middle">
              <td width="20%"><input name="optBiblioteca" type="radio" value="malo" /></td>
              <td width="20%"><input name="optBiblioteca" type="radio" value="regular" /></td>
              <td width="20%"><input name="optBiblioteca" type="radio" value="bueno" /></td>
              <td width="20%"><input name="optBiblioteca" type="radio" value="muybueno" /></td>
              <td width="20%"><input name="optBiblioteca" type="radio" value="excelente" /></td>
            </tr>
        </table></td>
      </tr>
      <tr>
        <td><strong>CAFETERIA</strong></td>
        <td><table width="100%" border="0">
            <tr align="center" valign="middle">
              <td width="20%"><input name="optCafeteria" type="radio" value="malo" /></td>
              <td width="20%"><input name="optCafeteria" type="radio" value="regular" /></td>
              <td width="20%"><input name="optCafeteria" type="radio" value="bueno" /></td>
              <td width="20%"><input name="optCafeteria" type="radio" value="muybueno" /></td>
              <td width="20%"><input name="optCafeteria" type="radio" value="excelente" /></td>
            </tr>
        </table></td>
      </tr>
      <tr>
        <td><strong>BA&Ntilde;OS</strong></td>
        <td><table width="100%" border="0">
          <tr align="center" valign="middle">
            <td width="20%"><input name="optBanios" type="radio" value="malo" /></td>
            <td width="20%"><input name="optBanios" type="radio" value="regular" /></td>
            <td width="20%"><input name="optBanios" type="radio" value="bueno" /></td>
            <td width="20%"><input name="optBanios" type="radio" value="muybueno" /></td>
            <td width="20%"><input name="optBanios" type="radio" value="excelente" /></td>
          </tr>
        </table></td>
      </tr>
    </table></td>
  </tr>
  <tr>
    <td>&nbsp;</td>
  </tr>
  <tr>
    <td><ol type="1" start="5">
      <li><strong><span lang="es" xml:lang="es">EL </span></strong><span class="Estilo2"><strong>TRABAJO   DE LA UNIVERSIDAD</strong></span><strong><span lang="es" xml:lang="es"> EN CUANTO   A:</span></strong></li>
    </ol></td>
  </tr>
  <tr>
    <td><table width="95%" border="0" align="right">
      <tr>
        <td width="30%">&nbsp;</td>
        <td width="70%"><table width="100%" border="0">
            <tr align="center" valign="middle">
              <td width="20%">Malo</td>
              <td width="20%">Regular</td>
              <td width="20%">Bueno</td>
              <td width="20%">Muy Bueno </td>
              <td width="20%">Excelente</td>
            </tr>
        </table></td>
      </tr>
      <tr>
        <td><span lang="es" xml:lang="es"> </span><strong><span lang="es" xml:lang="es">ORIENTACI&Oacute;N   ESPIRITUAL</span></strong></td>
        <td><table width="100%" border="0">
            <tr align="center" valign="middle">
              <td width="20%"><input name="optEspiritual" type="radio" value="malo" /></td>
              <td width="20%"><input name="optEspiritual" type="radio" value="regular" /></td>
              <td width="20%"><input name="optEspiritual" type="radio" value="bueno" /></td>
              <td width="20%"><input name="optEspiritual" type="radio" value="muybueno" /></td>
              <td width="20%"><input name="optEspiritual" type="radio" value="excelente" /></td>
            </tr>
        </table></td>
      </tr>
      <tr>
        <td><strong>COMUNICACI&Oacute;N   INTERNA</strong></td>
        <td><table width="100%" border="0">
            <tr align="center" valign="middle">
              <td width="20%"><input name="optComInterna" type="radio" value="malo" /></td>
              <td width="20%"><input name="optComInterna" type="radio" value="regular" /></td>
              <td width="20%"><input name="optComInterna" type="radio" value="bueno" /></td>
              <td width="20%"><input name="optComInterna" type="radio" value="muybueno" /></td>
              <td width="20%"><input name="optComInterna" type="radio" value="excelente" /></td>
            </tr>
        </table></td>
      </tr>
      <tr>
        <td><strong>BOLSA   DE TRABAJO (INSERCI&Oacute;N LABORAL)</strong></td>
        <td><table width="100%" border="0">
            <tr align="center" valign="middle">
              <td width="20%"><input name="optBolsaTrabajo" type="radio" value="malo" /></td>
              <td width="20%"><input name="optBolsaTrabajo" type="radio" value="regular" /></td>
              <td width="20%"><input name="optBolsaTrabajo" type="radio" value="bueno" /></td>
              <td width="20%"><input name="optBolsaTrabajo" type="radio" value="muybueno" /></td>
              <td width="20%"><input name="optBolsaTrabajo" type="radio" value="excelente" /></td>
            </tr>
        </table></td>
      </tr>
      <tr>
        <td><strong>POSIBILIDADES   DE INTERCAMBIOS, PR&Aacute;CTICAS, PASANT&Iacute;AS A NIVEL INTERNACIONAL</strong></td>
        <td><table width="100%" border="0">
            <tr align="center" valign="middle">
              <td width="20%"><input name="optIntercambio" type="radio" value="malo" /></td>
              <td width="20%"><input name="optIntercambio" type="radio" value="regular" /></td>
              <td width="20%"><input name="optIntercambio" type="radio" value="bueno" /></td>
              <td width="20%"><input name="optIntercambio" type="radio" value="muybueno" /></td>
              <td width="20%"><input name="optIntercambio" type="radio" value="excelente" /></td>
            </tr>
        </table></td>
      </tr>

    </table></td>
  </tr>
  <tr>
    <td>&nbsp;</td>
  </tr>
  <tr>
    <td><ol type="1" start="6">
      <li><span class="Estilo2"><strong>IDENTIFICACI&Oacute;N   CON LA UNIVERSIDAD</strong></span><strong><span lang="es" xml:lang="es"> Y PARTICIPACI&Oacute;N   EN ACTIVIDADES INTERNAS:</span></strong></li>
    </ol></td>
  </tr>
  <tr>
    <td><table width="95%" border="0" align="right">
      <tr>
        <td width="30%">&nbsp;</td>
        <td width="70%"><table width="100%" border="0">
            <tr align="center" valign="middle">
              <td width="20%">Nunca</td>
              <td width="20%">A veces </td>
              <td width="20%">Casi siempre </td>
              <td width="20%">Siempre</td>
              </tr>
        </table></td>
      </tr>
      <tr>
        <td><span lang="es" xml:lang="es"> </span><strong>LAS OPINIONES Y   APORTES VERTIDOS POR LOS ESTUDIANTES SON CONSIDERADOS</strong></td>
        <td><table width="100%" border="0">
            <tr align="center" valign="middle">
              <td width="20%"><input name="optOpiniones" type="radio" value="Nunca" /></td>
              <td width="20%"><input name="optOpiniones" type="radio" value="A veces" /></td>
              <td width="20%"><input name="optOpiniones" type="radio" value="Casi siempre" /></td>
              <td width="20%"><input name="optOpiniones" type="radio" value="Siempre" /></td>
              </tr>
        </table></td>
      </tr>
      <tr>
        <td><strong>EXPRESO   LIBREMENTE MI PUNTO DE VISTA A LAS AUTORIDADES Y PROFESORES</strong></td>
        <td><table width="100%" border="0">
          <tr align="center" valign="middle">
            <td width="20%"><input name="optPtoVista" type="radio" value="Nunca" /></td>
            <td width="20%"><input name="optPtoVista" type="radio" value="A veces" /></td>
            <td width="20%"><input name="optPtoVista" type="radio" value="Casi siempre" /></td>
            <td width="20%"><input name="optPtoVista" type="radio" value="Siempre" /></td>
          </tr>
        </table></td>
      </tr>
      <tr>
        <td><strong>EXISTEN   ACTIVIDADES DE INTEGRACI&Oacute;N ENTRE ESTUDIANTES, PROFESORES Y AUTORIDADES</strong></td>
        <td><table width="100%" border="0">
          <tr align="center" valign="middle">
            <td width="20%"><input name="optIntegracion" type="radio" value="Nunca" /></td>
            <td width="20%"><input name="optIntegracion" type="radio" value="A veces" /></td>
            <td width="20%"><input name="optIntegracion" type="radio" value="Casi siempre" /></td>
            <td width="20%"><input name="optIntegracion" type="radio" value="Siempre" /></td>
          </tr>
        </table></td>
      </tr>
      <tr>
        <td><strong>&ldquo;TENGO PUESTA LA   CAMISETA DE LA USAT&rdquo;</strong></td>
        <td><table width="100%" border="0">
          <tr align="center" valign="middle">
            <td width="20%"><input name="optCamiseta" type="radio" value="Nunca" /></td>
            <td width="20%"><input name="optCamiseta" type="radio" value="A veces" /></td>
            <td width="20%"><input name="optCamiseta" type="radio" value="Casi siempre" /></td>
            <td width="20%"><input name="optCamiseta" type="radio" value="Siempre" /></td>
          </tr>
        </table></td>
      </tr>

    </table></td>
  </tr>
  <tr>
    <td>&nbsp;</td>
  </tr>
  <tr>
    <td><ol type="1" start="7">
      <li><strong><span lang="es" xml:lang="es">CU&Aacute;L CREES QUE ES   LA </span></strong><span class="Estilo2"><strong>PERCEPCI&Oacute;N   DE LA IMAGEN DE LA UNIVERSIDAD</strong></span><strong><span lang="es" xml:lang="es"> EN LA   COLECTIVIDAD:</span></strong></li>
      </ol></td>
  </tr>
  <tr>
    <td>&nbsp;</td>
  </tr>
  <tr>
    <td><table width="95%" border="0" align="right">
      <tr>
        <td width="30%">&nbsp;</td>
        <td width="70%"><table width="100%" border="0">
            <tr align="center" valign="middle">
              <td width="20%">Malo</td>
              <td width="20%">Regular</td>
              <td width="20%">Bueno</td>
              <td width="20%">Muy Bueno </td>
              <td width="20%">Excelente</td>
            </tr>
        </table></td>
      </tr>
      <tr>
        <td><span lang="es" xml:lang="es"> </span><strong><span lang="es" xml:lang="es">FAMILIA</span></strong></td>
        <td><table width="100%" border="0">
            <tr align="center" valign="middle">
              <td width="20%"><input name="optFamilia" type="radio" value="malo" /></td>
              <td width="20%"><input name="optFamilia" type="radio" value="regular" /></td>
              <td width="20%"><input name="optFamilia" type="radio" value="bueno" /></td>
              <td width="20%"><input name="optFamilia" type="radio" value="muybueno" /></td>
              <td width="20%"><input name="optFamilia" type="radio" value="excelente" /></td>
            </tr>
        </table></td>
      </tr>
      <tr>
        <td><strong>AMIGOS</strong></td>
        <td><table width="100%" border="0">
            <tr align="center" valign="middle">
              <td width="20%"><input name="optAmigos" type="radio" value="malo" /></td>
              <td width="20%"><input name="optAmigos" type="radio" value="regular" /></td>
              <td width="20%"><input name="optAmigos" type="radio" value="bueno" /></td>
              <td width="20%"><input name="optAmigos" type="radio" value="muybueno" /></td>
              <td width="20%"><input name="optAmigos" type="radio" value="excelente" /></td>
            </tr>
        </table></td>
      </tr>
      <tr>
        <td><strong>MEDIOS DE   COMUNICACI&Oacute;N</strong></td>
        <td><table width="100%" border="0">
            <tr align="center" valign="middle">
              <td width="20%"><input name="optMedios" type="radio" value="malo" /></td>
              <td width="20%"><input name="optMedios" type="radio" value="regular" /></td>
              <td width="20%"><input name="optMedios" type="radio" value="bueno" /></td>
              <td width="20%"><input name="optMedios" type="radio" value="muybueno" /></td>
              <td width="20%"><input name="optMedios" type="radio" value="excelente" /></td>
            </tr>
        </table></td>
      </tr>

    </table></td>
  </tr>
  <tr>
    <td>&nbsp;</td>
  </tr>
  <tr>
    <td><ol type="1" start="8">
        <li><strong><span lang="es" xml:lang="es">EN ESTE ESPACIO   PODR&Aacute;S HACERNOS LLEGAR TUS APORTES, SUGERENCIAS U OPINIONES SOBRE LA USAT</span></strong><strong>.</strong></li>
    </ol></td>
  </tr>
  <tr>
    <td align="right"><label>
    <input name="txtComentario" type="text" id="txtComentario" maxlength="120"  style="width:95%"/>
    </label></td>
  </tr>
  <tr>
    <td>&nbsp;</td>
  </tr>
  <tr>
    <td align="center"><input onclick="Validar()" type="button" name="Submit2" value="Enviar" />&nbsp;&nbsp;&nbsp;&nbsp;
    <input type="reset" name="Submit" value="Restablecer" /></td>
  </tr>
  <tr>
    <td>&nbsp;</td>
  </tr>
  <tr>
    <td align="right"><strong>&iexcl;&iexcl;&iexcl;MUCHAS   GRACIAS POR TU TIEMPO!!!</strong></td>
  </tr>
  <tr>
    <td bgcolor="#990000">&nbsp;</td>
  </tr>
</table>

</form>