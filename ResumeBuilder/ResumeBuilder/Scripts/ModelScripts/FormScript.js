$(document).ready(function () {
    $("body").on("click", "#educationDetails", function () {
        var radioValue = $('input[name="courseOption"]:checked').val();
        debugger;
        if (radioValue == "10th") {
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


﻿//$(document).ready(function () {
//    $(body).on("click", "#educationDetails", function () {
//        var radioValue = $('input[name="courseOption"]:checked').val();
//        if (radioValue == "10th") {
//            $(".tenth-form").show();
//            $(".all-other").hide();

//        }
//        else {
//            $(".tenth-form").hide();
//            $(".all-other").show();
//        }
//    });


//    $(body).on("click", "#workingCheck", function () {
//        if ($('#workingCheck').is(":checked")) {
//            $(".end-duration").hide();
//        }
//    });
//});

//$('input[type=radio][name=courseOption]').change(function() {
//    if($(this).val()=='10th'){
//        //alert("tenth details is there!");
//        $(".tenth-form").show();
//        $(".all-other").hide();
//    }
//    else{
//        // alert("everythin else is there!!");
//        $(".all-other").show();
//        $(".tenth-form").hide();
//    }









