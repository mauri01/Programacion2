function msgSuccess(msg){
    BootstrapDialog.alert({
        title: 'EXCELENTE!',
        message: msg,
        type: BootstrapDialog.TYPE_SUCCESS,
        size: BootstrapDialog.SIZE_WIDE,
        closable: true,
        draggable: true,
        buttonLabel: 'Aceptar',
        callback: function (result) {
            location.reload();
        }
    });
}
function msgSuccessNorefresh(msg) {
    BootstrapDialog.alert({
        title: 'EXCELENTE!',
        message: msg,
        type: BootstrapDialog.TYPE_SUCCESS,
        size: BootstrapDialog.SIZE_WIDE,
        closable: true,
        draggable: true,
        buttonLabel: 'Aceptar',
    });
}
function msgWarning(msg) {
    BootstrapDialog.alert({
        title: 'CUIDADO!',
        message: msg,
        type: BootstrapDialog.TYPE_WARNING,
        size: BootstrapDialog.SIZE_WIDE,
        closable: true,
        draggable: true,
        buttonLabel: 'Aceptar'
    });
}
function msgError(msg) {
    BootstrapDialog.alert({
        title: 'ERROR!',
        message: msg,
        type: BootstrapDialog.TYPE_DANGER,
        size: BootstrapDialog.SIZE_WIDE,
        closable: true,
        draggable: true,
        buttonLabel: 'Aceptar'
    });
}