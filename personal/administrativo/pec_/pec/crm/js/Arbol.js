
$(document).ready(function() {
    Arbol()
});




function Arbol() {
    var rpta = "";
    rpta += '<li class="parent_li"><span title="Collapse this branch" class="parent"><i class="fa fa-folder-open fa-chevron-circle-up"></i>Parent</span> <a href=""></a>'
    rpta += '<ul>'
    rpta += '<li class="parent_li" style="display: list-item;"><span title="Collapse this branch" class="child"><i class="fa fa-chevron-circle-up"></i>Child</span> <a href=""></a>'
    rpta += '<ul>'
    rpta += '<li style="display: list-item;"><span class="grandchild"><i class="fa fa-file"></i>Grand Child</span> <a href=""></a></li>'
    rpta += '</ul>'
    rpta += '</li>'
    rpta += '</ul>'
    rpta += '</li>'
    $("#arbol").append(rpta);
}


                           