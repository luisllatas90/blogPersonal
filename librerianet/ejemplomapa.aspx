<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ejemplomapa.aspx.vb" Inherits="ejemplomapa" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Página sin título</title>
       <style>
       /* Set the size of the div element that contains the map */
      #map {
        height: 500px;  /* The height is 400 pixels */
        width: 500px;  /* The width is the width of the web page */
       }
      #coords{width: 500px;}
     iframe{width:600px; height:500px}
    </style>
     <script async defer     src="https://maps.googleapis.com/maps/api/js?key=AIzaSyD7mxYUjIBQdOKZgzEYHnUiHTRmYXBakGw&callback=initMap">     </script>
    <script>
    function initMap() {

 var map = new google.maps.Map(document.getElementById('map'), {
    zoom: 18,
    center: {lat: -6.760248, lng: -79.859883}
  });

  marker = new google.maps.Marker({
    map: map,
    draggable: true,
    animation: google.maps.Animation.DROP,
    position: {lat: -6.760248, lng: -79.859883}
  });
  marker.addListener('click', toggleBounce);
  marker.addListener( 'dragend', function (event)
      {
        //escribimos las coordenadas de la posicion actual del marcador dentro del input #coords
        document.getElementById("coords").value = this.getPosition().lat()+","+ this.getPosition().lng();
      });
}

function toggleBounce() {
  if (marker.getAnimation() !== null) {
    marker.setAnimation(null);
  } else {
    marker.setAnimation(google.maps.Animation.BOUNCE);
  }
}
    </script>
</head>
<body>
     <h3>ESTUDIANTE por favor: copia y pega las coordenadas de tu domicilio actual</h3>
	<div>
     	 <input style=" font-size:24px;" type="text" id="coords" />
	</div>
    	<div style="float:left; width=400px;" id="map"></div>

    <form id="form1" runat="server">
    
    
    </form>
</body>
</html>
