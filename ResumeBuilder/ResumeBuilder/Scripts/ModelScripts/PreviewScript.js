//$(document).ready(function () {
//    $("body").on("click", "#preview", function () {
//        //document.location = "/Resume/Preview";
//        var flag = 0;
//        $.ajax({
//            type: "GET",
//            url: "/Resume/Preview",
//            success: function (result) {
//                flag = 1;
//                console.log(result);
//                //$("#mainDivModal").hide();
//                $("#divPreview").html(result);
//                $("#divPreview").show();
                
//            }
//        });

//    });
//});
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