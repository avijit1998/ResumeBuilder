$(document).ready(function () {
    if ($.trim($("#validationSummaryDiv").text()) === "User already exists.")
        bootbox.alert("<b style='color:black;'>This User already exists.</b>");

    function validatePassword() {
        if ($("#Password").val() === "") {
            $("#passwordErrorText").text("The Password field is required.");
        }
        else if ((/((?=.*[0-9])(?=.*[a-z])(?=.*[A-Z])(?=.*[@@#$%!+-_=]).{8,})/).test($.trim($("#Password").val())) == false) {
            $("#passwordErrorText").text("The password must be of at least 8 characters including one upper and lower case letter,one " +
                "digit and one special character.");
        }
        else {
            $("#passwordErrorText").text("");
        }
    }

    $("#Password").on("blur", function () {
        validatePassword();


    });
    $("#Username").on("blur", function () {
        if ((/^([a-z A-Z 0-9\. -]+)@@([a-z A-Z]+)\.([a-z]{2,10})$/).test($.trim($("#Username").val())) === false) {
            $("#emailErrorText").text("Enter valid Email.")
        }
    });

    $("#btnSubmit").on("click", function () {
        ConfirmPassword
        if (($.trim($("#validationSummaryDiv").text()) === "") && ($.trim($("#Username").text()) !== "") &&
            ($.trim($("#Password").text()) !== "") && ($.trim($("#ConfirmPassword").text()) !== "")) {
            bootbox.alert("<b class='alert-success' style='color:black;'>Registration Successful</b>");
        }

        validatePassword();
    })
})