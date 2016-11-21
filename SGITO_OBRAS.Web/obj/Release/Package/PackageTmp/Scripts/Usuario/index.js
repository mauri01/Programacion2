$(document).ready(function () {
    var urlDataTableLanguage = 'Scripts/DataTableSpanish.js';
    var table = $('#myDataTable').dataTable({
        "language": { "url": urlDataTableLanguage },
        "bLengthChange": false,
        "pageLength": 15,
        "scrollY": "250px",
        "scrollCollapse": true,
        "bServerSide": true,
        "sAjaxSource": "Usuario/AjaxHandler",
        "bProcessing": true,
        "order": [[1, "asc"]],
        "aoColumns": [{ "sName": "ID" }, { "sName": "Usuario Red" }, { "sName": "Dominio" }, { "sName": "Nombre y Apellido" }, { "sName": "E-Mail" },
                        {
                            mData: "Acciones",
                            bSortable: false,
                            sWidth: "10%",
                            sClass: "center",
                            "render": function (data, type, full) {
                                return '<span class="btn-actionTable glyphicon glyphicon-pencil blue button-grid" aria-hidden="true" onclick="EditRow(\'' + full[0] + '\');" title="Editar"></span> <span class="btn-actionTable glyphicon glyphicon-remove red button-grid" aria-hidden="true" onclick="DeleteRow(\'' + full[0] + '\',\'' + full[1] + '\',\'' + full[2] + '\');" title="Eliminar"></span>';
                            }
                        }
        ],
        "columnDefs": [
                        { "targets": [0], "visible": false },
                        { "targets": [1], "searchable": false, "orderable": true },
                        { "targets": [2], "searchable": false, "orderable": true },
                        { "targets": [3], "searchable": false, "orderable": true },
                        { "targets": [5], "searchable": false, "orderable": true }
        ]
    });

    

    $("#btn-eliminar").click(function () {
        var id = $("#idEliminar").val();
        window.location.href = '/Usuario/DeleteUsuario/'+ id;
    });


    $("#btn-nuevo").click(function () 
    {
        window.location.href = '/Usuario/Create/'           
    });


    $("#btn-volver").click(function () {
        $("#crearContenido, #btn-sgte").show();
        $("#perfilesContenido, #btn-crear, #btn-volver").hide();
    });
    $("#btn-sgte").click(function () {
        var id = 0;
        var ured = $("#ured").val();
        var dominio = $("#dominio").val();
        var nombre = $("#nombre").val();
        var email = $("#email").val();
        var tipoDoc = $("#tipoDoc").val();
        var doc = $("#doc").val();
        var accion = $("#accion").val();
        if (accion == "edit") {
            id = $("#idEditar").val();
        }
        // Expresion regular para validar el correo
        var regex = /[\w-\.]{2,}@([\w-]{2,}\.)*([\w-]{2,}\.)[\w-]{2,4}/;
        if (!ured) {
            msgWarning("El campo Usuario Red no puede estar vacío.");
        } else if (!dominio) {
            msgWarning("El campo Dominio no puede estar vacío.");
        } else if (!nombre) {
            msgWarning("El campo Nombre no puede estar vacío.");
        } 
        else if (email && !regex.test(email.trim())) {
            msgWarning("El E-Mail ingresado en inválido.");
        } else if (doc && !$.isNumeric(doc)) {
            msgWarning("El Documento ingresado en inválido.");
        } 
    });

    $("#btn-crear").click(function () {
        var id = 0;
        var ured = $("#ured").val();
        var dominio = $("#dominio").val();
        var nombre = $("#nombre").val();
        //var perfil = $("#perfil").val();
        var email = $("#email").val();
        var tipoDoc = $("#tipoDoc").val();
        var doc = $("#doc").val();
        var accion = $("#accion").val();
        if (accion == "edit") {
            id = $("#idEditar").val();
        }
        var arrayPerfil = [];
        window.location.href = '/Usuario/CreateUsuario/' + ured + dominio;
    }); 

});

function DeleteRow(id, ured, dominio) {
    $("#idEliminar").val(id);
    $("#descripcionEliminar").html(ured + "/" + dominio);
    $("#eliminarModal").modal({ backdrop: 'static' });
    //window.location.href = '/Usuario/NoAutorizado';
}

function EditRow(id) {
    window.location.href = '/Usuario/Edit/'+ id ;
}

