$(document).ready(function () {
    var urlDataTableLanguage = 'Scripts/DataTableSpanish.js';
    var table = $('#myDataTable').dataTable({
        "language": { "url": urlDataTableLanguage },
        "bLengthChange": false,
        "pageLength": 15,
        "scrollY": "250px",
        "scrollCollapse": true,
        "bServerSide": true,
        "sAjaxSource": "Perfil/AjaxHandler",
        "bProcessing": true,
        "order": [[0, "desc"]],
        "aoColumns": [{ "sName": "ID" }, { "sName": "Perfil" },
                        {
                            mData: "Acciones",
                            bSortable: false,
                            sWidth: "15%",
                            sClass: "center",
                            "render": function (data, type, full) {
                                return '<span class="btn-actionTable glyphicon glyphicon-pencil blue button-grid" aria-hidden="true" onclick="EditRow(\'' + full[0] + '\');" title="Editar"></span> <span class="btn-actionTable glyphicon glyphicon-remove red button-grid" aria-hidden="true" onclick="DeleteRow(\'' + full[0] + '\', \'' + full[1] + '\');" title="Eliminar"></span>';
                            }
                        }
        ],
        "columnDefs": [
                        { "targets": [0], "visible": false },
                        { "targets": [1], "searchable": false, "orderable": true },
                        { "targets": [2], "searchable": false, "orderable": true }
        ]
    });

    $("#btn-nuevo").click(function () {
        window.location.href = '/Perfil/Create/'
    });

});

function EditRow(id) {
    window.location.href = '/Perfil/Edit/' + id;
}


function DeleteRow(id, descripcion) {
    $("#idEliminar").val(id);
    $("#descripcionEliminar").html("");
    $("#descripcionEliminar").append(descripcion);
    $("#eliminarModal").modal({ backdrop: 'static' });
}