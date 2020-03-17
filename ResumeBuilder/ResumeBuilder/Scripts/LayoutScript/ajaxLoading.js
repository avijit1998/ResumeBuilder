$(document).ready(function () {

    var url = $("a.ajaxLink").eq(0).data('url');
    $.get(url, function(data) {
        $('#pageContent').html(data);
    });

    $("a.ajaxLink").click(function (e) {
        e.preventDefault();
        var url = $(this).data('url');

        $.get(url, function (data) {
            $('#pageContent').html(data);
        });
    });
});