<%@ Page Language="VB" AutoEventWireup="false" CodeFile="FrmArbol.aspx.vb" Inherits="administrativo_pec_crm_FrmListaInteresados" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta http-equiv='X-UA-Compatible' content='IE=8' />
    <meta http-equiv='X-UA-Compatible' content='IE=10' />
    <meta http-equiv='X-UA-Compatible' content='IE=11' />
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=0" />
    <title>Arbol jQuery, Ajax y Bootstrap </title>
    <!-- Latest compiled and minified CSS -->
    <%--<link href="bootstrap-3.3.7-dist/css/bootstrap.min.css" rel="stylesheet" type="text/css" />--%>
    <link href="../../../assets/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <%--<link href='../../../assets/css/material.css' rel='stylesheet' />--%>
    <link href="../../../assets/css/style.css?x=1" rel="stylesheet" type="text/css" />
    <link href="../../../assets/css/jtree.css" rel="stylesheet" type="text/css" />
    <%--    <link href="../../../assets/css/animated-masonry-gallery.css" rel="stylesheet" type="text/css" />
    <link href="../../../assets/css/rotated-gallery.css" rel="stylesheet" type="text/css" />--%>

    <script src="../../../assets/js/jquery.js" type="text/javascript"></script>

    <script src="../../../assets/js/bootstrap.min.js" type="text/javascript"></script>

    <script src="../../../assets/js/tree-view/tree.js" type="text/javascript"></script>

    <%--<script src="js/Arbol.js?x=222" type="text/javascript"></script>--%>
    <%--<script src="../../../assets/js/app.js" type="text/javascript"></script>--%>

<%--    <script type="text/javascript">
        $(document).ready(function() {
            Arbol();
        })
        function Arbol() {
            var rpta = "";
//            rpta += '<ul>'
//            rpta += '<li class="parent_li"><span title="Expand this branch" class="parent"><i class="fa fa-folder-down fa-chevron-circle-down"></i>111111111111</span><a href=""></a>'
//            rpta += '<ul>'
//            rpta += '<li style="display: none;"><span title="Expand this branch" class="child"><i class="fa fa-file"></i>2222222</span> <a href=""></a></li>'
//            rpta += '</ul>'
//            rpta += '</li>'
//            rpta += '</ul>'
//            rpta += '<ul>'
//            rpta += '<li class="parent_li"><span title="Expand this branch" class="parent"><i class="fa fa-folder-down fa-chevron-circle-down"></i>111111111111</span><a href=""></a>'
//            rpta += '<ul>'
//            rpta += '<li style="display: none;"><span title="Expand this branch" class="child"><i class="fa fa-file"></i>2222222</span> <a href=""></a></li>'
//            rpta += '</ul>'
//            rpta += '</li>'
//            rpta += '</ul>'


            rpta += '<ul>'
            rpta += '<li ><span class="parent"><i class="fa fa-folder-down fa-chevron-circle-down"></i> 111111111111</span><a href=""></a>'
            rpta += '<ul>'
            rpta += '<li style="display: none;" ><span class="child"><i class="fa fa-file"></i> 2222222</span><a href=""></a></li>'
            rpta += '</ul>'
            rpta += '</li>'
            rpta += '</ul>'
            rpta += '<ul>'
            rpta += '<li ><span class="parent"><i class="fa fa-folder-down fa-chevron-circle-down"></i> 111111111111</span><a href=""></a>'
            rpta += '<ul>'
            rpta += '<li style="display: none;" ><span class="child"><i class="fa fa-file"></i> 2222222</span><a href=""></a></li>'
            rpta += '</ul>'
            rpta += '</li>'
            rpta += '</ul>'
            
            //            $('#CuerpoArbol').empty();

            $("#CuerpoArbol").append(rpta);
            
            $('.tree li:has(ul)').addClass('parent_li').find(' > span').attr('title', 'Collapse this branch');
            $('.tree li.parent_li > span').on('click', function(e) {
                var children = $(this).parent('li.parent_li').find(' > ul > li');
                if (children.is(":visible")) {
                    children.hide('fast');
                    $(this).attr('title', 'Expand this branch').find(' > i').addClass('fa-chevron-circle-down').removeClass('fa-chevron-circle-up');
                } else {
                    children.show('fast');
                    $(this).attr('title', 'Collapse this branch').find(' > i').addClass('fa-chevron-circle-up').removeClass('fa-chevron-circle-down');
                }
                e.stopPropagation();
            });
        }

    </script>--%>

   <%-- <style type="text/css">
        body
        {
            font-size: 10px;
        }
        .form-control
        {
            color: gray;
        }
        .form-group
        {
            margin: 6px;
        }
    </style>--%>
</head>
<body>
    <div class="content">
        <div class="row grid">
            <div class="col-md-12">
                <!-- panel -->
                <div class="panel panel-piluku">
                    <div class="panel-heading">
                        <h3 class="panel-title">
                            Tree View <span class="panel-options">
                                <%-- <a class="panel-refresh" href="#"><i class="icon ti-reload"></i></a>
               <a class="panel-minimize" href="#"><i class="icon ti-angle-up"></i></a>
                <a class="panel-close" href="#"><i class="icon ti-close"></i></a>--%>
                            </span>
                        </h3>
                    </div>
                </div>
            </div>
        </div>
        <!-- /panel -->
        <div class="main-content">
            <div class="row grid">
                <div class="col-md-6">
                    <!-- panel -->
                    <div class="panel panel-piluku">
                        <div class="panel-body">
                            <div class="row">
                                <div class="form-group">
                                    <label class="control-label col-sm-4">
                                        Ejercicio Presupuestal</label>
                                    <div class="col-sm-8">
                                        <select class="form-control input-sm">
                                            <option value="0">--SELECCIONE--</option>
                                        </select>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="form-group">
                                    <label class="control-label col-sm-4">
                                        Plan Estrátegico</label>
                                    <div class="col-sm-8">
                                        <select class="form-control input-sm">
                                            <option value="0">--SELECCIONE--</option>
                                        </select>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="form-group">
                                    <label for="apepat" class="control-label col-sm-4">
                                        Centro de Costos - Área:</label>
                                    <div class="col-sm-8">
                                        <input type="text" class="form-control input-sm " id="apepat" name="apepat" runat="server" />
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="form-group">
                                    <label for="apepat" class="control-label col-sm-4">
                                        Nombre de Plan (Poa):</label>
                                    <div class="col-sm-8">
                                        <input type="text" class="form-control input-sm " id="Text1" name="apepat" runat="server" />
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="form-group">
                                    <label class="control-label col-sm-4">
                                        Responsable de Plan</label>
                                    <div class="col-sm-8">
                                        <select class="form-control input-sm">
                                            <option value="0">--SELECCIONE--</option>
                                        </select>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="form-group">
                                    <label class="control-label col-sm-4">
                                        Vigencia</label>
                                    <div class="col-sm-8">
                                        <input type="checkbox" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-md-6">
                    <!-- panel -->
                    <div class="panel panel-piluku">
                        <%--<div class="panel-body">
                            <%--<div class="tree well" id="CuerpoArbol">
                                <%--           <div runat="server">
                                </div>--%>
                                <%-- <ul>
                        <li class="parent_li"><span title="Collapse this branch" class="parent"><i class="fa fa-folder-open fa-chevron-circle-up">
                        </i>Parent</span> <a href=""></a>
                            <ul>
                                <li class="parent_li" style="display: list-item;"><span title="Collapse this branch"
                                    class="child"><i class="fa fa-chevron-circle-up"></i>Child</span> <a href=""></a>
                                    <ul>
                                        <li style="display: list-item;"><span class="grandchild"><i class="fa fa-file"></i>Grand
                                            Child</span> <a href=""></a></li>
                                    </ul>
                                </li>
                                <li class="parent_li" style="display: list-item;"><span title="Expand this branch"
                                    class="child"><i class="fa fa-chevron-circle-down"></i>Child</span> <a href=""></a>
                                    <ul>
                                        <li style="display: none;"><span class="grandchild"><i class="fa fa-file"></i>Grand
                                            Child</span> <a href=""></a></li>
                                        <li class="parent_li" style="display: none;"><span title="Collapse this branch" class="grandchild">
                                            <i class="fa fa-chevron-circle-up"></i>Grand Child</span> <a href=""></a>
                                            <ul>
                                                <li class="parent_li"><span title="Collapse this branch" class="great-grandchild"><i
                                                    class="fa fa-chevron-circle-up"></i>Great Grand Child</span> <a href=""></a>
                                                    <ul>
                                                        <li><span class="greatgrand-grandchild"><i class="fa fa-file"></i>Great great Grand
                                                            Child</span> <a href=""></a></li>
                                                        <li><span class="greatgrand-grandchild"><i class="fa fa-file"></i>Great great Grand
                                                            Child</span> <a href=""></a></li>
                                                    </ul>
                                                </li>
                                                <li><span class="great-grandchild"><i class="fa fa-file"></i>Great Grand Child</span>
                                                    <a href=""></a></li>
                                                <li><span class="great-grandchild"><i class="fa fa-file"></i>Great Grand Child</span>
                                                    <a href=""></a></li>
                                            </ul>
                                        </li>
                                        <li style="display: none;"><span class="grandchild"><i class="fa fa-file"></i>Grand
                                            Child</span> <a href=""></a></li>
                                    </ul>
                                </li>
                            </ul>
                        </li>
                        <li class="parent_li"><span title="Collapse this branch" class="parent"><i class="fa fa-folder-open">
                        </i>Parent</span> <a href=""></a>
                            <ul>
                                <li><span class="child"><i class="fa fa-file"></i>Child</span> <a href=""></a></li>
                            </ul>
                        </li>
                    </ul>--%>
                            <%--</div>--%>
                            <div class="tree well">
					<ul>
						<li class="parent_li">
							<span class="parent" title="Expand this branch"><i class="fa fa-folder-open fa-chevron-circle-down"></i> Parent</span> <a href=""></a>
							<ul>
								<li class="parent_li" style="display: none;">
									<span class="child" title="Collapse this branch"><i class="fa fa-chevron-circle-up"></i> Child</span> <a href=""></a>
									<ul>
										<li>
											<span class="grandchild"><i class="fa fa-file"></i> Grand Child</span> <a href=""></a>
										</li>
									</ul>
								</li>
								<li class="parent_li" style="display: none;">
									<span class="child" title="Collapse this branch"><i class="fa fa-chevron-circle-up"></i> Child</span> <a href=""></a>
									<ul>
										<li>
											<span class="grandchild"><i class="fa fa-file"></i> Grand Child</span> <a href=""></a>
										</li>
										<li class="parent_li">
											<span class="grandchild" title="Collapse this branch"><i class="fa fa-chevron-circle-up"></i> Grand Child</span> <a href=""></a>
											<ul>
												<li class="parent_li">
													<span class="great-grandchild" title="Collapse this branch"><i class="fa fa-chevron-circle-up"></i> Great Grand Child</span> <a href=""></a>
													<ul>
														<li>
															<span class="greatgrand-grandchild"><i class="fa fa-file"></i> Great great Grand Child</span> <a href=""></a>
														</li>
														<li>
															<span class="greatgrand-grandchild"><i class="fa fa-file"></i> Great great Grand Child</span> <a href=""></a>
														</li>
													</ul>
												</li>
												<li>
													<span class="great-grandchild"><i class="fa fa-file"></i> Great Grand Child</span> <a href=""></a>
												</li>
												<li>
													<span class="great-grandchild"><i class="fa fa-file"></i> Great Grand Child</span> <a href=""></a>
												</li>
											</ul>
										</li>
										<li>
											<span class="grandchild"><i class="fa fa-file"></i> Grand Child</span> <a href=""></a>
										</li>
									</ul>
								</li>
							</ul>
						</li>
						<li class="parent_li">
							<span class="parent" title="Expand this branch"><i class="fa fa-folder-open fa-chevron-circle-down"></i> Parent</span> <a href=""></a>
							<ul>
								<li style="display: none;">
									<span class="child"><i class="fa fa-file"></i> Child</span> <a href=""></a>
								</li>
							</ul>
						</li>
					</ul>
				</div>
                            
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</body>
</html>
