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

    $("body").on('change', 'input[type=radio][name=CGPAOrPercentage]', function () {
        if ($(this).val() == 'CGPA') {
            $('.marks').attr('placeholder', 'Enter CGPA');
            $('.marks').val('');
        } else {
            $('.marks').attr('placeholder', 'Enter percentage ');
            $('.marks').val('');
        }
    });

    $("body").on('click', '#checkWorking', function () {
        if ($(this).is(':checked')) {
            $(".hide-if-currently-working").hide();
        } else {
            $(".hide-if-currently-working").show();
        }
    });

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

    //add and edit operations
    $("body").on("click", ".js-edit-user", function (e) {
        e.preventDefault();
        var $button = $(this);

        var userId = $button.data("user-id");
        var name = $button.data("name");
        var emailId = $button.data("emailid");
        var phoneNumber = $button.data("phone");
        var summary = $button.data("summary");
        var gender = $button.data("gender");
        var languages = $button.data("languages");

        $('#userId').val(userId);
        $('#txtFullName').val(name);
        $('#txtPhoneNumber').val(phoneNumber);
        $('#txtSummary').val(summary);

        $('input[name="Gender"]').each(function (e, el) {
            if ($(el).val() == gender) {
                $(el).prop('checked', true);
            }
        })
        for (var i = 0; i < languages.length; i++) {
            $('input[type="checkbox"]').each(function (e, el) {
                if ($(el).val() == languages[i]) {
                    $(el).prop('checked', true);
                }
            })
        }
        $('#modalBasicInfo').find('.modal-title').html('UPDATE BASIC INFORMATION DETAILS');
        $('#modalBasicInfo').modal('show');
    });

    $("body").on("click", ".js-save-user", function () {
        var userData = {
            "UserID": $("#userId").val(),
            "Name": $("#txtFullName").val(),
            "Gender": $('input[name="Gender"]:checked').val(),
            "DateOfBirth": $("#dateDOB").val(),
            "PhoneNumber": $("#txtPhoneNumber").val(),
            "LanguageIds": [],
            "Summary": $("#txtSummary").val()
        };

        $('input[type="checkbox"]:checked').each(function (e, el) {
            userData.LanguageIds.push($(el).val());
        });

        if (userData.LanguageIds[userData.LanguageIds.length - 1] == "on") {
            userData.LanguageIds.pop();
        }
        var params = $.extend({}, params);
        params['url'] = '/Resume/SaveBasicInformation';
        params['data'] = userData;
        params['requestType'] = 'POST';
        params['successCallbackFunction'] = function () {
            bootbox.alert("<p style='color:black;'>Basic information successfully saved.</p>");
            $("#modalBasicInfo").modal("hide");
            
        };
        params['errorCallBackFunction'] = function () {
            bootbox.alert("<p style='color:black;'>Error!</p>");
        }
        commonAjax(params);

        return false;
    });

    $('body').on('click', '.js-edit-workex', function (e) {
        e.preventDefault();
        var $button = $(this);
        var id = $button.data("workex-id");
        var organization = $button.data("organization");
        var role = $button.data("designation");
        var startMonth = $button.data("start-month");
        var startYear = $button.data("end-month");
        var endMonth = $button.data("start-year");
        var endYear = $button.data("end-year");
        var isWorking = $button.data("isworking");

        $('input[name="WorkExperienceid"]').val(id);
        $('input[name="OrganizationName"]').val(organization);
        $('input[name="Role"]').val(role);
        $("#selectStartMonth").val(startMonth).change();
        $("#selectStartYear").val(startYear).change();
        $("#selectEndMonth").val(endMonth).change();
        $("#selectEndYear").val(endYear).change();

        if (isWorking) {
            $('input[name="CurrentlyWorking"]').prop("checked", true);
            $(".hide-if-currently-working").hide();
        }
        else {
            $('input[name="CurrentlyWorking"]').prop("checked", false);
            $(".hide-if-currently-working").show();
        }

        $('#modalWorkExperience').find('.modal-title').html('UPDATE WORK EXPERIENCE DETAILS');
        $('#modalWorkExperience').modal('show');

    });

    $('body').on('click', '.js-save-workex', function (e) {

        e.preventDefault();
        var formData = {
            "WorkExperienceID": $('input[name="WorkExperienceID"]').val(),
            "OrganizationName": $('input[name="OrganizationName"]').val(),
            "Designation": $('input[name="Designation"]').val(),
            "StartMonth": $("#selectStartMonth").val(),
            "StartYear": $("#selectStartYear").val(),
            "EndMonth": $("#selectEndMonth").val(),
            "EndYear": $("#selectEndYear").val(),
            "IsCurrentlyWorking": $('#checkWorking').is(":checked")
        };

        var params = $.extend({}, params);
        params['url'] = '/Resume/SaveWorkExperience';
        params['data'] = formData;
        params['requestType'] = 'POST';
        params['successCallbackFunction'] = function () {
            bootbox.alert("<p style='color:black;'>Work Experience Successfully saved.</p>");
            $("#modalWorkExperience").modal("hide");
            
        };
        params['errorCallBackFunction'] = function () {
            bootbox.alert("<p style='color:black;'>Error!</p>");
        }
        commonAjax(params);

        
    });

    $('body').on('click', '.js-edit-project', function (e) {
        e.preventDefault();
        var $button = $(this);
        var projectId = $button.data("project-id");
        var title = $button.data("title");
        var role = $button.data("role");
        var duration = $button.data("duration");
        var description = $button.data("description");

        $('input[name="ProjectId"]').val(projectId);
        $('input[name="Title"]').val(result.Title);
        $('input[name="ProjectRole"]').val(role);
        $('input[name="Duration"]').val(duration);
        $('textarea[name="Description"]').val(description);

        $('#modalProject').find('.modal-title').html('UPDATE PROJECT DETAILS');
        $('#modalProject').modal('show');

    });

    $('body').on('click', '.js-save-project', function (e) {
        e.preventDefault();
        var ProjectID = $('input[name="ProjectID"]').val();
        var formData = {
            "ProjectID": $('input[name="ProjectID"]').val(),
            "ProjectTitle": $('input[name="ProjectTitle"]').val(),
            "ProjectRole": $('input[name="ProjectRole"]').val(),
            "DurationInMonth": $('input[name="DurationInMonth"]').val(),
            "Description": $('textarea[name="Description"]').val()
        };

        var params = $.extend({}, params);
        params['url'] = '/Resume/SaveProjectDetails';
        params['data'] = formData;
        params['requestType'] = 'POST';
        params['successCallbackFunction'] = function () {
            bootbox.alert("<p style='color:black;'>Project Details updated sucessfully</p>");
            $("#modalBasicInfo").modal("hide");
            
        };
        params['errorCallBackFunction'] = function () {
            bootbox.alert("<p style='color:black;'>Error!</p>");
        }
        commonAjax(params);

        return false;

        
    });

    $('body').on('click', '.js-edit-education', function (e) {
        e.preventDefault();
        var $button = $(this);
        var id = $button.data("education-id");
        var courseid = $button.data("courseid");
        var passingyear = $button.data("passingyear");
        var stream = $button.data("stream");
        var CGPAOrPercentage = $button.data("cgpaorpercentage");
        var marks = $button.data("marks");
        var board = $button.data("board");

        $('input[name="EducationalDetailsID"]').val(id);
        $('input[name="CourseId"]').val(courseid);
        $('input[name="Stream"]').val(stream);
        $('input[name="PassingYear"]').val(passingyear);
        $('#boardType').val(board).change();
        $('input[name="TotalPercentageOrCGPAValue"]').val(marks);
        $('input[name="CGPAOrPercentage"]').each(function (e, el) {
            if ($(el).val() == CGPAOrPercentage) {
                $(el).prop('checked', true);
            }
        })

        if (courseid == "1") {
            $(".all-other").show();
            $(".stream").hide();
        }
        else {
            $(".stream").show();
            $(".all-other").show();
        }

        $('input[name="courseOption"]').each(function (e, el) {
            if ($(el).val() == courseid) {
                $(el).prop('checked', true);
                $(el).prop('disabled', false);
            }
            else {
                $(el).prop('checked', false);
                $(el).prop('disabled', true);
            }
        })

        $('#modalEducationDetails').find('.modal-title').html('UPDATE EDUCATION DETAILS');
        $('#modalEducationDetails').modal('show');
    });

    $('body').on('click', '.js-save-education', function (e) {
        
        e.preventDefault();
        var id = $('input[name="EducationalDetailsID"]').val();
        var formData = {
            "EducationalDetailsID": id,
            "UserID": $("#userId").val(),
            "CourseID": $('input[name="courseOption"]:checked').val(),
            "Stream": $('input[name="Stream"]').val(),
            "PassingYear": $('input[name="PassingYear"]').val(),
            "TotalPercentageOrCGPAValue": $('input[name="TotalPercentageOrCGPAValue"]').val(),
            "CGPAOrPercentage": $('input[name="CGPAOrPercentage"]:checked').val(),
            "BoardOrUniversity": $('#boardType option:selected').text()
        };

        if (formData.CourseID == 1) {
            formData.Stream = 'N/A';
        }
        var params = $.extend({}, params);
        params['url'] = '/Resume/SaveEducationalDetails';
        params['data'] = formData;
        params['requestType'] = 'POST';
        params['successCallbackFunction'] = function () {
            $("#modalEducationDetails").modal("hide");
            bootbox.alert("<p style='color:black;'>Education Details updated sucessfully</p>");
        };
        params['errorCallBackFunction'] = function () {
            bootbox.alert("<p style='color:black;'>Error!</p>");
        }
        commonAjax(params);

        //disable radio button for client-side

        if (formData.CourseID == 1 || formData.CourseID == 2) {
            $("input[type=radio][value=" + formData.CourseID + "]").prop("disabled", true);
            $("input[type=radio][value=" + formData.CourseID + "]").prop("checked", false);
            $(".all-other").hide();
        }

    });

    $("body").on("click", "#btnSaveSkills", function (e) {
        e.preventDefault();
        var skillDetails = {
            "UserID": $("#userId").val(),
            "SkillNames": []
        };

        $(".skillItem").each(function (index) {
            skillDetails.SkillNames.push($(this).text());
        })

        var params = $.extend({}, params);
        params['url'] = '/Resume/SaveUserSkills';
        params['data'] = skillDetails;
        params['requestType'] = 'POST';
        params['successCallbackFunction'] = function () {
            $("#modalSkills").modal("hide");
            bootbox.alert("<p style='color:black;'>Skills successfully saved.</p>");
            
        };
        params['errorCallBackFunction'] = function () {
            bootbox.alert("<p style='color:black;'>Error!</p>");
        }
        commonAjax(params);

        return false;
    });

    //delete operations
    $('body').on('click', '.js-delete-project', function (e) {
        e.preventDefault();
        var $button = $(this);
        var projectID = $button.data("project-id");
        bootbox.confirm("<p style='color:black;'>Are you sure to delete this Project Record?</p>", function (result) {
            if (result) {

                var params = $.extend({}, params);
                params['url'] = '/Resume/DeleteProject' + projectID;
                params['requestType'] = 'POST';
                params['successCallbackFunction'] = function () {

                };
                params['errorCallBackFunction'] = function () {
                    bootbox.alert("<p style='color:black;'>Error!</p>");
                }
                commonAjax(params);
                return false;
             }
        });
    });

    $('body').on('click', '.js-delete-workex', function (e) {
        e.preventDefault();
        var $button = $(this);

        var workExId = $button.data("workex-id");
        bootbox.confirm("<p style='color:black;'>Are you sure to delete this Work Experience Record?</p>", function (result) {
            if (result) {

                var params = $.extend({}, params);
                params['url'] = '/Resume/DeleteWorkExperience' + workExId;
                params['requestType'] = 'POST';
                params['successCallbackFunction'] = function () {

                };
                params['errorCallBackFunction'] = function () {
                    bootbox.alert("<p style='color:black;'>Error!</p>");
                }
                commonAjax(params);

            }
        });
    });

    $('body').on('click', '.js-delete-skill', function (e) {
        e.preventDefault();
        var $button = $(this);
        var userID = $("#userId").val();
        var skillID = $button.data("skill-id");
        var formData = {
            "userID":$("#userId").val(),
            "skillID": skillID,
        };
        bootbox.confirm("<p style='color:black;'>Are you sure to delete this Skill?</p>", function (result) {
            if (result) {

                var params = $.extend({}, params);
                params['url'] = '/Resume/DeleteSkill';
                params['requestType'] = 'POST';
                params['data'] = formData;
                params['successCallbackFunction'] = function () {

                };
                params['errorCallBackFunction'] = function () {
                    bootbox.alert("<p style='color:black;'>Error!</p>");
                }
                commonAjax(params);
            }
        });
    })

    $('body').on('click', '.js-delete-education', function (e) {
        e.preventDefault();
        var $button = $(this);
        var educationId = $button.data("education-id");
        var formData = {
            "educationId": educationId,
        };
        bootbox.confirm("<p style='color:black;'>Are you sure to delete this Education Record?</p>", function (result) {
            if (result) {

                var params = $.extend({}, params);
                params['url'] = '/Resume/DeleteEducation';
                params['requestType'] = 'POST';
                params['data'] = formData;
                params['successCallbackFunction'] = function () {
                    bootbox.alert("<p style='color:black;'>Skill succesfully deleted.</p>");
                };
                params['errorCallBackFunction'] = function () {
                    bootbox.alert("<p style='color:black;'>Error!</p>");
                }
                commonAjax(params);
            }
        });
    });

});

function removeBackdrop()
{
    $('body').removeClass('modal-open');
    $('.modal-backdrop').remove();
}

function clearFields() {
    $('input[type="text"]').val('');
    $('select').val('');
    $('input[type="checkbox"]').prop('checked', false);
}

