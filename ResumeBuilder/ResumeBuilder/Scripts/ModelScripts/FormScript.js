var radioValue = $('input[name="courseOption"]:checked').val();
if (radioValue == "10th") {
    $(".tenth-form").show();
    $(".all-other").hide();

}
else {
    $(".tenth-form").hide();
    $(".all-other").show();
}

if ($('#workingCheck').is(":checked"))
{
    $(".end-duration").hide();
}