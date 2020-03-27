$("body").on('change', 'input[type=radio][name=CGPAOrPercentage]', function () {
    if ($(this).val() == 'CGPA') {
        $('.marks').attr('placeholder', 'Enter CGPA');
        $('.marks').val('');
    } else {
        $('.marks').attr('placeholder', 'Enter percentage ');
        $('.marks').val('');
    }
});

$("body").on('click', '#checkWorking',function(){
    if ($(this).is(':checked')) {
        $(".hide-if-currently-working").hide();
    } else {
        $(".hide-if-currently-working").show();
    }
});

$("body").on("click", ".js-edit-user", function (e) {
    e.preventDefault();
    var $button = $(this);

    var userId = $button.data("user-id");
    $.ajax({
        url: "GetCurrentUser/" + userId,
        method: "GET",
        success: function (result) {
            $('#userId').val(result.UserID);
            $('#txtFullName').val(result.Name);
            $('#txtEmail').val(result.Username);
            $('#txtPhoneNumber').val(result.PhoneNumber); 
            $('#txtSummary').val(result.Summary);
            
            $('input[name="Gender"]').each(function (e, el) {
                if ($(el).val() == result.Gender) {
                    $(el).prop('checked', true);
                }
            })
            for (var i = 0; i < result.LanguageIds.length; i++) {
                $('input[type="checkbox"]').each(function (e, el) {
                    if ($(el).val() == result.LanguageIds[i]) {
                        $(el).prop('checked', true);
                    }
                })
            }
            $('#modalBasicInfo').modal('show');
        },
        error: function (error) {
            botbox.alert("<p style='color:black;'>Sorry ! Unable to edit user</p>");
        }
    });
});
 
$("body").on("click", ".js-save-user", function () {
    var user = {
        "UserID":$("#userId").val(),
        "Name": $("#txtFullName").val(),
        "Username": $("#txtEmail").val(),
        "Gender": $('input[name="Gender"]:checked').val(),
        "DateOfBirth": $("#dateDOB").val(),
        "PhoneNumber": $("#txtPhoneNumber").val(),
        "LanguageIds": [],
        "Summary": $("#txtSummary").val()
    };

    $('input[type="checkbox"]:checked').each(function (e, el) {
        user.LanguageIds.push($(el).val());
    });

    if (user.LanguageIds[user.LanguageIds.length - 1] == "on") {
        debugger;
        user.LanguageIds.pop();
    }

    $.ajax({
        type: "POST",
        url: 'UpdateUser',
        data: user,
        success: function () {           
            bootbox.alert("<p style='color:black;'>Basic information successfully saved.</p>");
            $("#modalBasicInfo").modal("hide");
            removeBackdrop();
            var url = $("#ajaxEditForm").data('url');
            $.get(url, function (data) {
                $('#pageContent').html(data);
            });
        },
        error: function () {
            bootbox.alert("<p style='color:black;'>Error!</p>");
        }
    });
    return false;
});

$('body').on('click', '.js-edit-project', function (e) {
    debugger;
    e.preventDefault();
    var $button = $(this);
    var projectId = $button.data("project-id");
    $.ajax({
        url: "GetProjectById/" + projectId,
        method: "GET",
        success: function (result) {
            $('input[name="ProjectId"]').val(result.ProjectId);
            $('input[name="Title"]').val(result.Title);
            $('input[name="ProjectRole"]').val(result.ProjectRole);
            $('input[name="Duration"]').val(result.Duration);
            $('textarea[name="Description"]').val(result.Description);
            $('#modalProject').find('.modal-title').html('UPDATE PROJECT DETAILS');
            $('#modalProject').modal('show');
        },
        error: function (error) {           
            bootbox.alert("<p style='color:black;'>Sorry ! Unable to edit project</p>");
        }
    });
});

$('body').on('click', '.js-save-project', function (e) {
    e.preventDefault();
    var Id = $('input[name="ProjectId"]').val();
    var formData = {
        "ProjectId": $('input[name="ProjectId"]').val(),
        "Title": $('input[name="Title"]').val(),
        "ProjectRole": $('input[name="ProjectRole"]').val(),
        "Duration": $('input[name="Duration"]').val(),
        "Description": $('textarea[name="Description"]').val()
    };
    $.ajax({
        url: "UpdateProject",
        method: "POST",
        data: formData,
        success: function (result) {
            $('#modalProject').modal('hide');
            removeBackdrop();
            bootbox.alert("<p style='color:black;'>Project Details updated sucessfully</p>");
            var url = $("#ajaxEditForm").data('url');
            $.get(url, function (data) {
                $('#pageContent').html(data);
            });           
        },
        error: function (error) {
            bootbox.alert("<p style='color:black;'>Sorry ! Unable to update project</p>");
        }
    });

});

$('body').on('click', '.js-delete-project', function (e) {
    debugger;
    e.preventDefault();
    var $button = $(this);
    var projectID = $button.data("project-id");
    bootbox.confirm("<p style='color:black;'>Are you sure to delete this Project Record?</p>", function (result) {
        if (result) {
            $.ajax({
                url: "DeleteProject",
                method: "POST",
                data: { projectID: projectID },
                success: function (resultfinal) {
                    
                    var url = $("#ajaxEditForm").data('url');
                    $.get(url, function (data) {
                        $('#pageContent').html(data);
                    });                    
                },
                error: function (error) {
                    bootbox.alert("<p style='color:black;'>Error!</p>");
                }
            })
        }
    });
});

$('body').on('click', '.js-edit-workex', function (e) {
    e.preventDefault();
    var $button = $(this);
    var id = $button.data("workex-id");
    $.ajax({
        url: "GetWorkExperienceById/" + id,
        method: "GET",
        success: function (result) {
            $('input[name="WorkExperienceid"]').val(result.WorkExperienceid);
            $('input[name="OrganizationName"]').val(result.OrganizationName);
            $('input[name="Role"]').val(result.Role);
            $("#selectStartMonth").val(result.StartMonth).change();
            $("#selectStartYear").val(result.StartYear).change();
            $("#selectEndMonth").val(result.EndMonth).change();
            $("#selectEndYear").val(result.EndYear).change();
            
            if (result.CurrentlyWorking)
            {
                $('input[name="CurrentlyWorking"]').prop("checked", true);
                $(".hide-if-currently-working").hide();
            }
            else {
                $('input[name="CurrentlyWorking"]').prop("checked", false);
                $(".hide-if-currently-working").show();
            }
            $('#modalWorkExperience').find('.modal-title').html('UPDATE WORK EXPERIENCE DETAILS');
            $('#modalWorkExperience').modal('show');
        },
        error: function (error) {
            bootbox.alert("<p style='color:black;'>Sorry ! Unable to edit WorkEx</p>");
        }
    });
});

$('body').on('click', '.js-save-workex', function (e) {
    debugger;
    e.preventDefault();
    var formData = {
        "WorkExperienceid": $('input[name="WorkExperienceid"]').val(),
        "OrganizationName": $('input[name="OrganizationName"]').val(),
        "Role": $('input[name="Role"]').val(),
        "StartMonth": $("#selectStartMonth").val(),
        "StartYear":  $("#selectStartYear").val(),
        "EndMonth":  $("#selectEndMonth").val(),
        "EndYear": $("#selectEndYear").val(),
        "CurrentlyWorking": $('#checkWorking').is(":checked")
    };
    $.ajax({
        url: "UpdateWorkExperience",
        method: "POST",
        data: formData,
        success: function (result) {
            $('#modalWorkExperience').modal('hide');
            removeBackdrop();
            bootbox.alert("<p style='color:black;'>WorkEx Updated Successfully</p>");
            var url = $("#ajaxEditForm").data('url');
            $.get(url, function (data) {
                $('#pageContent').html(data);
            });
           

        },
        error: function (error) {
            bootbox.alert("<p style='color:black;'>Sorry ! Unable to update details</p>");
        }
    });

});

$('body').on('click', '.js-delete-workex', function (e) {
    debugger;
    e.preventDefault();
    var $button = $(this);

    var workExId = $button.data("workex-id");
    bootbox.confirm("<p style='color:black;'>Are you sure to delete this Work Experience Record?</p>", function (result) {
        if (result) {
            $.ajax({
                url: "DeleteWorkExperience",
                method: "POST",
                data: { workExId: workExId },
                success: function (result) {
                  
                    var url = $("#ajaxEditForm").data('url');
                    $.get(url, function (data) {
                        $('#pageContent').html(data);
                    });                   
                },
                error: function (error) {
                    bootbox.alert("<p style='color:black;'>Error!</p>");
                }
            })
        }
    });
});

$('body').on('click', '.js-delete-skill', function (e) {
    e.preventDefault();
    var $button = $(this);
    var skillId = $button.data("skill-id");
    bootbox.confirm("<p style='color:black;'>Are you sure to delete this Skill?</p>", function (result) {
        if (result) {
            $.ajax({
                url: "DeleteSkill",
                method: "POST",
                data: { skillId: skillId },
                cache:false,
                success: function (result) {
                    
                    var url = $("#ajaxEditForm").data('url');
                    $.get(url, function (data) {
                        $('#pageContent').html(data);
                    });
                },
                error: function (error) {
                    bootbox.alert("<p style='color:black;'>Error!</p>");
                }
            })
        }
    });
})

$('body').on('click', '.js-edit-education', function (e) {
    debugger;
    e.preventDefault();
    var $button = $(this);
    var id = $button.data("education-id");
    $.ajax({
        url: "GetEducationById/" + id,
        method: "GET",
        success: function (result) {
            $('input[name="EducationalDetailID"]').val(result.EducationalDetailID);
            $('input[name="CourseId"]').val(result.CourseId);
            $('input[name="Stream"]').val(result.Stream);
            $('input[name="PassingYear"]').val(result.PassingYear);
            $('input[name="TotalPercentorCGPAValue"]').val(result.TotalPercentorCGPAValue);
            $('input[name="CGPAOrPercentage"]').each(function (e, el) {
                if ($(el).val() == result.CGPAOrPercentage) {
                    $(el).prop('checked', true);
                }
            })

            if (result.CourseId == "1")
            {
                $(".all-other").show();
                $(".stream").hide();
            }
            else {
                $(".stream").show();
                $(".all-other").show();
            }

            $('#modalEducationDetails').find('.modal-title').html('UPDATE EDUCATION DETAILS');
            $('#modalEducationDetails').modal('show');
        },
        error: function (error) {
            bootbox.alert("<p style='color:black;'>Sorry ! Unable to edit education</p>");
        }
    });
});

$('body').on('click', '.js-save-education', function (e) {
    debugger;
    e.preventDefault();
    var id = $('input[name="EducationalDetailID"]').val();
    var formData = {
        "EducationalDetailID": $('input[name="EducationalDetailID"]').val(),
        "CourseID": $('input[name="CourseId"]').val(),
        "Stream": $('input[name="Stream"]').val(),
        "PassingYear": $('input[name="PassingYear"]').val(),
        "TotalPercentorCGPAValue": $('input[name="TotalPercentorCGPAValue"]').val(),
        "CGPAOrPercentage": $('input[name="CGPAOrPercentage"]:checked').val(),
        "Board": $('#boardType option:selected').text()
    };
    $.ajax({
        url: "UpdateEducation",
        method: "POST",
        data: formData,
        success: function (result) {
            $('#modalEducationDetails').modal('hide');
            removeBackdrop();
            bootbox.alert("<p style='color:black;'>Education Details updated sucessfully</p>");
            var url = $("#ajaxEditForm").data('url');
            $.get(url, function (data) {
                $('#pageContent').html(data);
                $('#pageContent').show();
            });
        },
        error: function (error) {
            bootbox.alert("<p style='color:black;'>Sorry ! Unable to edit education</p>");
        }
    });

});

$('body').on('click', '.js-delete-education', function (e) {
    e.preventDefault();
    var $button = $(this);
    var educationId = $button.data("education-id");
    bootbox.confirm("<p style='color:black;'>Are you sure to delete this Project Record?</p>", function (result) {
        if (result) {
            $.ajax({
                url: "DeleteEducation",
                method: "POST",
                data: { educationId: educationId },
                success: function (result) {
                   
                    var url = $("#ajaxEditForm").data('url');
                    $.get(url, function (data) {
                        $('#pageContent').html(data);
                    });
                },
                error: function (error) {
                    bootbox.alert("<p style='color:black;'>Error!</p>");
                }
            })
        }
    });
});

function removeBackdrop()
{
    $('body').removeClass('modal-open');
    $('.modal-backdrop').remove();
}