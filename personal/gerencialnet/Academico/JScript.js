// JScript File

function tabControlOver(element)
{
    element.className += " ppbtn_over";
}

function tabControlOut(element)
{
    element.className = element.className.replace(/ ppbtn_over/gi,"");
}