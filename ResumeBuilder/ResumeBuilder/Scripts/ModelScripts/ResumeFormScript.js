
$(document).ready(function () {
    $("body").on("click", ".js-add-education", function () {
        $('#modalEducationDetails').modal('show');
    });

    $("body").on("click", ".js-add-project", function () {
        $('#modalProject').modal('show');
    });

    $("body").on("click", ".js-add-workex", function () {
        $('#modalWorkExperience').modal('show');
    });

    $("body").on("click", ".js-add-skill", function () {
        $('#modalSkills').modal('show');
    });

    $("body").on('click', '#educationDetails', function () {
        var addedTenDetails = $('#addedTenthDetails').data('value')
        var addedTwelfthDetails = $('#addedTwelfthDetails').data('value')

        if (addedTenDetails == 1) {
            $("input[type=radio][value=" + addedTenDetails + "]").prop("disabled", true);
        }

        if (addedTwelfthDetails == 2) {
            $("input[type=radio][value=" + addedTwelfthDetails + "]").prop("disabled", true);
        }
    });
    
    //empty auto fill data

    $("body").on('click', '[data-dismiss=modal]', function () {
        $('#modalWorkExperience').on('hidden.bs.modal', function () {
            clearFields();      
        });
        $('#modalProject').on('hidden.bs.modal', function () {
            clearFields();
        });
        $('#modalSkills').on('hidden.bs.modal', function () {
            clearFields();
        });
        $('#modalEducationDetails').on('hidden.bs.modal', function () {
            clearFields();
        });   
    });

    $("body").on('change', 'input[type=radio][name=marksOption]', function () {
        if ($(this).val() == 'CGPA') {
            $('.marks').attr('placeholder', 'Enter CGPA');
        } else {
            $('.marks').attr('placeholder', 'Enter percentage ');
        }
    });

    $('#checkWorking').click(function () {
        if ($(this).is(':checked')) {
            $(".hide-if-currently-working").hide();
        } else {
            $(".hide-if-currently-working").show();
        }
    });

    $("body").on("click", "#educationDetails", function () {
        $('input[type=radio][name=courseOption]').change(function () {
          
            if ($(this).val() == '1') {
                $(".all-other").show();
                $(".stream").hide();
            }
            else {
                $(".stream").show();
                $(".all-other").show();
            }
        });
});

    
    var selector = 'input#txtSearch';
    $(document).on('keydown.autocomplete', selector, function () {
        $(this).autocomplete({
            source: function (request, response) {
                $.ajax({
                    url: "GetSkill",
                    method: "GET",
                    dataType: "json",
                    data: {
                        term: request.term
                    },
                    success: function (data) {
                        console.log("data");
                        response(data);
                    },
                    error: function (data) {
                        console.log("error");
                    }
                });
            },
            appendTo: $('#autoComplete')
        });
    });


    $("body").on('click', '#addSkill', function () {
        var item = $("#txtSearch").val();
        var isSkillFound = 0;
        $(".skillItem").each(function (index) {
            var skillValue = $(this).text();
            if (skillValue == item) {
                isSkillFound = 1;
                return false;
            }
        });

        if (isSkillFound == 0) {
            $("#skillMenu").append('<li class="skillItem">' + item + '</li>');
            clearFields();
        } else {
            bootbox.alert("<b style='color:black;'>"+item + " already added.</b>");
        }
    });


    $("body").on("click", "#preview", function (e) {
        var ids = $('#sessionIdFetch').data('sessionid');
        $.ajax({
            url: "/Resume/PreviewUser/" + ids,
            method: "GET",
            success: function (result) {
  
                $("#modalUserName").html(result.Name);
                $("#modalUserPhone").html(result.PhoneNumber);
                $("#modalUserEmail").html(result.Email);
                $("#modalSkills").html(result.SkillList);
                var data = '';
                for (var i = 0; i < result.SkillList.Count() ; i++) {
                    data.append(result.SkillList[i]);
                }
            },
            error: function () {
                bootbox.alert("<b style='color:black;'>Error!</b>")
   
            }
        });
    });
});

function clearFields()
{
    $('input[type="text"]').val('');
    $('select').val('');
    $('input[type="checkbox"]').prop('checked', false);
}