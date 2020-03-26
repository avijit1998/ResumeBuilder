$(document).ready(function () {
    
    $("body").on("click", "#preview", function (e) {
        e.preventDefault();
        url = $(this).data('url');
        $("#mainDivModal").hide();
        $.get(url, function (data) {
            $('#previewDiv').replaceWith(data);
        });
        var id = $('#sessionIdFetch').data('sessionId');
    });

});