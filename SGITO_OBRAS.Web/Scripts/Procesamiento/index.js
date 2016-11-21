/// <reference path="../DataTableSpanish.js" />
$(document).ready(function () {
    var pageLength = 15;
    var urlDataTableLanguage = 'Scripts/DataTableSpanish.js';
    var table = $('#myDataTable').dataTable({
        "language": { "url": urlDataTableLanguage },
        "bLengthChange": false,
        "bFilter": false,
        "pageLength": pageLength,
        "scrollY": "350px",
        "scrollCollapse": true,
        "bServerSide": true,
        "sAjaxSource": "Procesamiento/AjaxHandler",
        "bProcessing": true,
        "order": [[0, "desc"]],
        "aoColumns": [{ "sName": "ID" }, { "sName": "Fuente" }, { "sName": "Carpeta" }, { "sName": "Fecha de Procesamiento" }, { "sName": "Resultado de ejecución" }               
        ],
        "columnDefs": [
                        { "targets": [0], "visible": false },
                        { "targets": [1], "searchable": false, "orderable": false },
                        { "targets": [2], "searchable": false, "orderable": false },
                        { "targets": [3], "searchable": false, "orderable": false },
                        { "targets": [4], "searchable": false, "orderable": false }
                        
        ]
    });

    $('#myDataTable').on('page.dt', function () {
        $('#myDataTable').on('draw.dt', function () {
            $("#pagina").val($('a.paginate_button.current').text());
        });
    });

    $('body').on('input', "input[type='search']", function (ev) {
        $("#search").val($("input[type='search']").val());
        $('#myDataTable').on('draw.dt', function () {
            $("#pagina").val($('a.paginate_button.current').text());
        });
    });



    $.fn.pageMe = function (opts) {
        var $this = this,
            defaults = {
                perPage: 7,
                showPrevNext: false,
                hidePageNumbers: false
            },
            settings = $.extend(defaults, opts);

        var listElement = $this;
        var perPage = settings.perPage;
        var children = listElement.children();
        var pager = $('.paginado');

        if (typeof settings.childSelector != "undefined") {
            children = listElement.find(settings.childSelector);
        }

        if (typeof settings.pagerSelector != "undefined") {
            pager = $(settings.pagerSelector);
        }

        var numItems = children.size();
        var numPages = Math.ceil(numItems / perPage);

        pager.data("curr", 0);

        if (settings.showPrevNext) {
            $('<li class="previous"><a href="#" class="prev_link">Anterior</a></li>').appendTo(pager);
        }

        var curr = 0;
        while (numPages > curr && (settings.hidePageNumbers == false)) {
            $('<li><a href="#" class="page_link">' + (curr + 1) + '</a></li>').appendTo(pager);
            curr++;
        }

        if (settings.showPrevNext) {
            $('<li class="next"><a href="#" class="next_link">Siguiente</a></li>').appendTo(pager);
        }

        pager.find('.page_link:first').addClass('active');
        pager.find('.prev_link').hide();
        if (numPages <= 1) {
            pager.find('.next_link').hide();
        }
        pager.children().eq(1).addClass("active");

        children.hide();
        children.slice(0, perPage).show();

        pager.find('li .page_link').click(function () {
            var clickedPage = $(this).html().valueOf() - 1;
            goTo(clickedPage, perPage);
            return false;
        });
        pager.find('li .prev_link').click(function () {
            previous();
            return false;
        });
        pager.find('li .next_link').click(function () {
            next();
            return false;
        });

        function previous() {
            var goToPage = parseInt(pager.data("curr")) - 1;
            goTo(goToPage);
        }

        function next() {
            goToPage = parseInt(pager.data("curr")) + 1;
            goTo(goToPage);
        }

        function goTo(page) {
            var startAt = page * perPage,
                endOn = startAt + perPage;

            children.css('display', 'none').slice(startAt, endOn).show();

            if (page >= 1) {
                pager.find('.prev_link').show();
            }
            else {
                pager.find('.prev_link').hide();
            }

            if (page < (numPages - 1)) {
                pager.find('.next_link').show();
            }
            else {
                pager.find('.next_link').hide();
            }

            pager.data("curr", page);
            pager.children().removeClass("active");
            pager.children().eq(page + 1).addClass("active");

        }
    };

});