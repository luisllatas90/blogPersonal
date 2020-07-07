<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Tree.aspx.vb" Inherits="administrativo_pec_crm_FrmListaInteresados" %>

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

    <script src="js/Arbol.js?x=222" type="text/javascript"></script>

    <%--<script src="../../../assets/js/app.js" type="text/javascript"></script>--%>

    <script type="text/javascript">

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

    </script>

</head>
<body>
<div class="row grid">
			<div class="col-md-4">
				<!-- panel -->
				<div class="panel panel-piluku">
					<div class="panel-heading">
						<h3 class="panel-title">
							Basic Alert
							<span class="panel-options">
								<a class="panel-refresh" href="#">
									<i class="icon ti-reload"></i> 
								</a>
								<a class="panel-minimize" href="#">
									<i class="icon ti-angle-up"></i> 
								</a>
								<a class="panel-close" href="#">
									<i class="icon ti-close"></i> 
								</a>
							</span>
						</h3>
					</div>
					<div class="panel-body">
						<button class="btn btn-warning btn-lg basic-sweet-1" onclick="_gaq.push(['_trackEvent', 'example', 'try', 'basic-sweet-1']);">Warning</button>

						<button class="btn btn-success btn-lg basic-sweet-2" onclick="_gaq.push(['_trackEvent', 'example', 'try', 'basic-sweet-2']);">Success</button>

						<button class="btn btn-info btn-lg basic-sweet-3" onclick="_gaq.push(['_trackEvent', 'example', 'try', 'basic-sweet-3']);">Info</button>

						<button class="btn btn-danger btn-lg basic-sweet-4" onclick="_gaq.push(['_trackEvent', 'example', 'try', 'basic-sweet-4']);">Danger</button>
					</div>
				</div>
				<!-- /panel -->
			</div>
			<!-- /col-md-6 -->

			<div class="col-md-8">
				<!-- panel -->
				<div class="panel panel-piluku">
					<div class="panel-heading">
						<h3 class="panel-title">
							Basic Alert with tagline
							<span class="panel-options">
								<a class="panel-refresh" href="#">
									<i class="icon ti-reload"></i> 
								</a>
								<a class="panel-minimize" href="#">
									<i class="icon ti-angle-up"></i> 
								</a>
								<a class="panel-close" href="#">
									<i class="icon ti-close"></i> 
								</a>
							</span>
						</h3>
					</div>
					<div class="panel-body">
						<button class="btn btn-warning btn-lg tagline-sweet-1" onclick="_gaq.push(['_trackEvent', 'example', 'try', 'tagline-sweet-1']);">Warning</button>

						<button class="btn btn-success btn-lg tagline-sweet-2" onclick="_gaq.push(['_trackEvent', 'example', 'try', 'tagline-sweet-2']);">Success</button>

						<button class="btn btn-info btn-lg tagline-sweet-3" onclick="_gaq.push(['_trackEvent', 'example', 'try', 'tagline-sweet-3']);">Info</button>

						<button class="btn btn-danger btn-lg tagline-sweet-4" onclick="_gaq.push(['_trackEvent', 'example', 'try', 'tagline-sweet-4']);">Danger</button>
					</div>
				</div>
				<!-- /panel -->
			</div>
			<!-- /col-md-6 -->
		</div>

    <!-- panel -->
    <div class="panel panel-piluku">
        <div class="panel-heading">
            <h3 class="panel-title">
                Tree View <span class="panel-options"><a class="panel-refresh" href="#"><i class="icon ti-reload">
                </i></a><a class="panel-minimize" href="#"><i class="icon ti-angle-up"></i></a><a
                    class="panel-close" href="#"><i class="icon ti-close"></i></a></span>
            </h3>
        </div>
        <div class="panel-body">
            <div class="tree well">
                <ul id="arbol">
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
                </ul>
            </div>
        </div>
    </div>
    <!-- /panel -->
</body>
</html>
