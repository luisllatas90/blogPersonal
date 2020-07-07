$(document).ready(function() {
   // $('#btnListar').click(function() {
      //      fnExportarCVS();
      //  var data = $('#txt').val();
      //  if (data == '')
      //      return;

        ///JSONToCSVConvertor(data, "Vehicle Report", true);
    //});
});

function eliminarPorName(jsonVar, name) {
    jsonVar.forEach(function(currentValue, index, arr) {
 //   console.log(jsonVar[index].2010-I);
    //console.log(jsonVar[index]);
    //console.log(jsonVar[index]['DISCONTINUO']);
    if (jsonVar[index] == name) {
        console.log(name);
            jsonVar.splice(index, index);
        }
    })
    for (var key in jsonVar) {
       // console.log(key);
       // console.log(jsonVar[key]);
        for (var key2 in jsonVar[key]) {
            if (key2 == name) {
               // jsonVar.splice(key, key2);
                delete jsonVar[key][key2];
            }
            //console.log(key2);
            //console.log(jsonVar[key][key2]);
        } 
    }
    return jsonVar;   //console.log(jsonVar);
}

function JSONToCSVConvertor(JSONData, ReportTitle, ShowLabel, HideColumns) {
//    var key;
//    var obj = {};
//    var i = 0;
//    for (var idx in JSONData) {
//        key = JSONData[idx];
//        delete key+'.2017-I';
//        obj[i] = key;

//        i++;
//    }
//    console.log(obj);
   // delete JSONData.M2010_I;
    //If JSONData is not an object then JSON.parse will parse the JSON string in an Object
    var arrData = typeof JSONData != 'object' ? JSON.parse(JSONData) : JSONData;
   //delete arrData.M2010_I;
  // arrData= eliminarPorName(arrData, 'M2010_I');
//  
  var keyCol;
            var obj = {};
            var i = 0;
            for (var idx in HideColumns) {
                keyCol = HideColumns[idx];
                console.log(keyCol);

                arrData = eliminarPorName(arrData, keyCol);
            }
               
  
  
  
   // console.log(arrData);
    var CSV = '';
    //Set Report title in first row or line

  //  CSV += ReportTitle + '\r\n\n';

    //This condition will generate the Label/Header
    if (ShowLabel) {
        var row = "";

        //This loop will extract the label from 1st index of on array
        for (var index in arrData[0]) {

            //Now convert each value to string and comma-seprated
            row += index + ',';
        }

        row = row.slice(0, -1);

        //append Label row with line break
        CSV += row + '\r\n';
    }

    //1st loop is to extract each row
    for (var i = 0; i < arrData.length; i++) {
        var row = "";

        //2nd loop will extract each column and convert it in string comma-seprated
        for (var index in arrData[i]) {
            row += '"' + arrData[i][index] + '",';
        }

        row.slice(0, row.length - 1);

        //add a line break after each row
        CSV += row + '\r\n';
    }

    if (CSV == '') {
        alert("Invalid data");
        return;
    }

    //Generate a file name
    var fileName = "Alumnos_";
    //this will remove the blank-spaces from the title and replace it with an underscore
    fileName += ReportTitle.replace(/ /g, "_");

    //Initialize file format you want csv or xls
    var uri = 'data:text/csv;charset=utf-8,' + escape(CSV);
    
    var dat = setTimeout(function() {
    downloadWithName(uri, "Data.csv");
        
    }, 4000);
    // Now the little tricky part.
    // you can use either>> window.open(uri);
    // but this will not work in some browsers
    // or you will not get the correct file extension    

    //this trick will generate a temp <a /> tag
//    var link = document.createElement("a");
//    link.href = uri;

//    //set the visibility hidden so it will not effect on your web-layout
//    link.style = "visibility:hidden";
//    link.download = fileName + ".csv";

//    //this part will append the anchor tag and remove it after automatic click
//    document.body.appendChild(link);
//    link.click();
//    document.body.removeChild(link);
}

function downloadWithName(uri, name) {
    var link = document.createElement("a");
    link.download = name;
    link.href = uri;
    link.click();
    // alert(link);
}
function fnExportarCVS() {
    var str = '';
    $.ajax({
        type: "POST",
        url: "../../DataJson/PredictorDesercion/GenerarMatrizCSV.aspx",
        data: { "Funcion": "CVS" },
        dataType: "json",
        cache: false,
        success: function(data) {
            console.log(data);
            JSONToCSVConvertor(data, "Matriz de Conocimiento Alumnos", true);

        },
        error: function(result) {

            console.log(result);
            // oTable.fnOpen(nTr, sOut, 'details');
            // location.reload();
        }
    });
}
