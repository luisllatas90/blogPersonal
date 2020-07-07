// JavaScript Document
function validapersonal(source,arguments)
    {   
        if (form1.LstPersonal.length==0)
            arguments.IsValid = false;
        else
            {
                if (form1.LstPersonal.selectedIndex==-1)
                    arguments.IsValid = false;
                else
                    arguments.IsValid = true;
            }
    }