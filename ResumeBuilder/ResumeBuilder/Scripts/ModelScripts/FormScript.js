$(document).ready(function () {
    $("body").on("click", "#educationDetails", function () {
        var radioValue = $('input[name="courseOption"]:checked').val();
        
        if (radioValue === "10th") {
            $(".tenth-form").show();
            $(".all-other").hide();

        }
        else {
            $(".tenth-form").hide();
            $(".all-other").show();
        }
    });


    $('body').on("click", "#workingCheck", function () {
        if ($('#workingCheck').is(":checked")) {
            $(".end-duration").hide();
        }
    });
});