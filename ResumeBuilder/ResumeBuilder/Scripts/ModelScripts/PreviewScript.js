$(document).ready(function () {
    $("body").on("click", "#preview", function () {
        //document.location = "/Resume/Preview";
        var flag = 0;
        $.ajax({
            type: "GET",
            url: "/Resume/Preview",
            success: function (result) {
                flag = 1;
                console.log(result);
                $("#mainDivModal").hide();
                $("#divPreview").html(result);
                $("#divPreview").show();
                
            }
        });

    });
});