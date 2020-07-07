/// Autor : jmanay
function resaltar  (x, op)
        {        
            if (op==1 /*&& x.seleccionado==0 */)
                 {
                    x.style.backgroundColor ="lemonchiffon";
                    x.style.BackColor="lemonchiffon";
                  }
             if (op!=1 /*&& x.seleccionado==0*/)
                {
                   x.style.backgroundColor ="white";
                   x.style.BackColor="white";                 
                 }
        }
        
function pintarfila(x, identificador)
    {
        
        var y ;
        y=document.getElementById(identificador);       
        
        /// desseleccionar las filas que estuviesen marcadas
        var ArrFilas = y.getElementsByTagName('tr');
        var i;
        var filaseleccionada;
        filaseleccionada='';
        for (i=0;i<y.rows.length;i++)
            {
                if (x.id!=ArrFilas[i].id && x.seleccionado==0)
                    {
                        ArrFilas[i].style.backgroundColor='white';
                        ArrFilas[i].seleccionado=0;
                    }             
                     
                
                if (ArrFilas[i].seleccionado==1)
                       {
                       filaseleccionada=ArrFilas[i].id;
                       }
                
            }      
              
           if (x.id==filaseleccionada && x.seleccionado==1)
            {
                return 0;
            }    
           
        
        
            if (x.seleccionado==0)
                {
                    x.style.backgroundColor='lemonchiffon';
                    x.seleccionado=1;
                }
            else
                {
                    if  (x.id!=filaseleccionada && x.seleccionado==1)
                    {
                        x.style.backgroundColor='white';
                        x.seleccionado=0;
                    }
                    else
                       {
                            x.seleccionado=0;
                       }
                }

    }

