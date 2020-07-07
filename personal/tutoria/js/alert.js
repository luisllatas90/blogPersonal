document.querySelector('.confirm-sweet-3').onclick = function(){
	swal({
		title: "¿Desea continuar?",
		text: "Los datos han sido modificados, si cierra esta ventana perderá los cambios.",
		type: "info",
		showCancelButton: true,
		confirmButtonClass: 'btn-info',
		confirmButtonText: 'Ok',
		closeOnConfirm: true,
          //closeOnCancel: false
      },
      function(){
      	swal("Good!", "Thanks for clicking!", "success");
      });
};