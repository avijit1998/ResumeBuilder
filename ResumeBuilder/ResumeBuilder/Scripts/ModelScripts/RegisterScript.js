$(document).ready(function () {
    var email = false;
    var password = false;
    var confirmPassword = false;

    if ($.trim($("#validationSummaryDiv").text()) === "User already exists.")
        bootbox.alert("<b style='color:black;'>This User already exists.</b>");

    function register() {
        if (email === false || password === false || confirmPassword === false) {
            $("#btnSubmit").prop('disabled', true);
        }
        else {
            $("#btnSubmit").prop('disabled', false);
        }
    }

    function validatePassword() {
        if ($.trim($("#Password").val()) === "") {
            $("#passwordErrorText").text("The Password field is required.");
            password = false;
        }
        else if ((/((?=.*[0-9])(?=.*[a-z])(?=.*[A-Z])(?=.*[@#$%!+-_=]).{8,})/).test($.trim($("#Password").val())) == false) {
            $("#passwordErrorText").text("The password must be of at least 8 characters including one upper and lower case letter,one " +
                "digit and one special character.");
            password = false;
        }
        else {
            $("#passwordErrorText").text("");
            password = true;
        }
    }

    function validateConfirmPassword() {
        if ($.trim($("#confirmPassword").val()) === "") {
            $("#confirmPasswordErrorText").text("Confirm your password!");
            confirmPassword = false;
        }
        if (($.trim($("#confirmPassword").val())) !== ($.trim($("#Password").val()))) {
            $("#confirmPasswordErrorText").text("The passwords doesn't match!");
            confirmPassword = false;
        }
        else {
            $("#confirmPasswordErrorText").text("");
            confirmPassword = true;
        }
    }

    $("#Password").on("blur", function () {
        validatePassword();
        register();
    });

    $("#confirmPassword").on("blur", function () {
        validateConfirmPassword();
        register();
    });

    $("#Username").on("blur", function () {
        if ($("#Username").val() === "") {
            $("#emailErrorText").text("Email field is required.");
            email = false;
        }
        else if ((/^([a-z A-Z 0-9\. -]+)@([a-z A-Z]+)\.([a-z]{2,10})$/).test($.trim($("#Username").val())) === false) {
            $("#emailErrorText").text("Enter valid Email.");
            email = false;
        }
        else {
            $("#emailErrorText").text("");
            email = true;
        }
        register();
    });

    $("#btnSubmit").on("click", function () {
        if (($.trim($("#validationSummaryDiv").text()) === "") && ($.trim($("#Username").text()) !== "") &&
            ($.trim($("#Password").text()) !== "") && ($.trim($("#ConfirmPassword").text()) !== "")) {
            bootbox.alert("<b class='alert-success' style='color:black;'>Registration Successful</b>");
        }
    });
});