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

    $("body").on("click", "#btnCloseSkills", function () {
        $('#skillMenu').empty();
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
        $('#modalBasicInfo').find('.modal-title').html('UPDATE BASIC INFORMATION');
        $('#modalBasicInfo').modal('show');
    });


    $("body").on("click", ".js-save-user", function (e) {
        e.preventDefault();
        var flag = $("#basicInfoForm").valid();
        if (flag) {
            var userData = {
                "UserID": $.trim($("#userId").val()),
                "Name": $.trim($("#txtFullName").val()),
                "Gender": $.trim($('input[name="Gender"]:checked').val()),
                "DateOfBirth": $.trim($("#dateDOB").val()),
                "PhoneNumber": $.trim($("#txtPhoneNumber").val()),
                "LanguageIds": [],
                "Summary": $.trim($("#txtSummary").val())
            };

            $('input[type="checkbox"]:checked').each(function (e, el) {
                userData.LanguageIds.push($(el).val());
            });

            if (userData.LanguageIds[userData.LanguageIds.length - 1] == "on") {
                userData.LanguageIds.pop();
            }
            var params = $.extend({}, params);
            params['url'] = '/SaveDetails/SaveBasicInformation';
            params['data'] = userData;
            params['requestType'] = 'POST';
            params['successCallbackFunction'] = function () {
                $("#modalBasicInfo").modal("hide");
            };
            params['errorCallBackFunction'] = function () { }
            commonAjax(params);
        }
        else {
            bootbox.alert("<p style='color:red;'>Fill The Fields with * mark!</p>");
        }
        return false;
    });

    $('body').on('click', '.js-edit-workex', function (e) {
        e.preventDefault();
        var $button = $(this);
        var id = $button.data("workex-id");
        var organization = $button.data("organization");
        var role = $button.data("designation");
        var startMonth = $button.data("start-month");
        var startYear = $button.data("start-year");
        var endMonth = $button.data("end-month");
        var endYear = $button.data("end-year");
        var isWorking = $button.data("isworking");
       
        $('input[name="WorkExperienceID"]').val(id);
        $('input[name="OrganizationName"]').val(organization);
        $('input[name="Designation"]').val(role);
        $("#selectStartMonth").val(startMonth).change();
        $("#selectStartYear").val(startYear).change();
        $("#selectEndMonth").val(endMonth).change();
        $("#selectEndYear").val(endYear).change();

        if (isWorking == "True") {
            $('input[name="IsCurrentlyWorking"]').prop("checked", true);
            $(".hide-if-currently-working").hide();
        }
        else {
            $('input[name="IsCurrentlyWorking"]').prop("checked", false);
            $(".hide-if-currently-working").show();
        }

        $('#modalWorkExperience').find('.modal-title').html('UPDATE WORK EXPERIENCE DETAILS');
        $('#modalWorkExperience').modal('show');

    });

    $('body').on('click', '.js-save-workex', function (e) {
        e.preventDefault();

        var flag = $("#workExperienceForm").valid();

        if (flag) {
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
            params['url'] = '/SaveDetails/SaveWorkExperience';
            params['data'] = formData;
            params['requestType'] = 'POST';
            params['successCallbackFunction'] = function (result) {
                $("#modalWorkExperience").modal("hide");
            };
            params['errorCallBackFunction'] = function (result) {};
            commonAjax(params);
        }
        else {
            bootbox.alert("<p style='color:red;'>Fill the Fields with * Mark!</p>");
        }
        return false;
    });

    $('body').on('click', '.js-edit-project', function (e) {

        e.preventDefault();
        var $button = $(this);
        var projectId = $button.data("project-id");
        var title = $button.data("title");
        var role = $button.data("role");
        var duration = $button.data("duration");
        var description = $button.data("description");

        $('input[name="ProjectID"]').val(projectId);
        $('input[name="ProjectTitle"]').val(title);
        $('input[name="ProjectRole"]').val(role);
        $('input[name="DurationInMonth"]').val(duration);
        $('textarea[name="Description"]').val(description);

        $('#modalProject').find('.modal-title').html('UPDATE PROJECT DETAILS');
        $('#modalProject').modal('show');

    });

    $('body').on('click', '.js-save-project', function (e) {
        var flag = $("#projectDetailsForm").valid();
        if (flag) {
            var ProjectID = $('input[name="ProjectID"]').val();
            var formData = {
                "ProjectID": $('input[name="ProjectID"]').val(),
                "ProjectTitle": $('input[name="ProjectTitle"]').val(),
                "ProjectRole": $('input[name="ProjectRole"]').val(),
                "DurationInMonth": $('input[name="DurationInMonth"]').val(),
                "Description": $('textarea[name="Description"]').val()
            };
            var params = $.extend({}, params);
            params['url'] = '/SaveDetails/SaveProjectDetails';
            params['data'] = formData;
            params['requestType'] = 'POST';

            params['successCallbackFunction'] = function () {
                $("#modalProject").modal("hide");
            };
            params['errorCallBackFunction'] = function (result) { };
            commonAjax(params);
        }
        else {
            bootbox.alert("<p style='color:black;'>Error!</p>");
        }
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
        $('[name=BoardOrUniversity] option').filter(function () {
            return ($(this).text() == stream);
        }).prop('selected', true);

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
        var flag = $("#educationDetailsForm").valid();
        if (flag) {
            var id = $.trim($('input[name="EducationalDetailsID"]').val());
            var formData = {
                "EducationalDetailsID": id,
                "UserID": $.trim($("#userId").val()),
                "CourseID": $.trim($('input[name="courseOption"]:checked').val()),
                "Stream": $.trim($('input[name="Stream"]').val()),
                "PassingYear": $.trim($('input[name="PassingYear"]').val()),
                "TotalPercentageOrCGPAValue": $.trim($('input[name="TotalPercentageOrCGPAValue"]').val()),
                "CGPAOrPercentage": $.trim($('input[name="CGPAOrPercentage"]:checked').val()),
                "BoardOrUniversity": $.trim($('#boardType option:selected').text())
            };
            if (formData.CourseID == 1) {
                formData.Stream = 'N/A';
            }
            var params = $.extend({}, params);
            params['url'] = '/SaveDetails/SaveEducationalDetails';
            params['data'] = formData;
            params['requestType'] = 'POST';
            params['successCallbackFunction'] = function () {
                $("#modalEducationDetails").modal("hide");

            };
            params['errorCallBackFunction'] = function () { };
            commonAjax(params);
            //disable radio button for client-side

            if (formData.CourseID == 1 || formData.CourseID == 2) {
                $("input[type=radio][value=" + formData.CourseID + "]").prop("disabled", true);
                $("input[type=radio][value=" + formData.CourseID + "]").prop("checked", false);
                $(".all-other").hide();
            }
        }
        else {
            bootbox.alert("<p style='color:red;'>Fill the fields with * mark!</p>");
        }
        return false;
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


        if ($(".skillItem").text() !== "") {
            var params = $.extend({}, params);
            params['url'] = '/SaveDetails/SaveUserSkills';
            params['data'] = skillDetails;
            params['requestType'] = 'POST';
            params['successCallbackFunction'] = function () {
                $("#modalSkills").modal("hide");
            };
            params['errorCallBackFunction'] = function () { };
            commonAjax(params);
        }
        else {
            bootbox.alert("<p style='color:red;'>Fill the fields with * mark!</p>");
        }
        $('ul').empty();
        return false;
    });

    //delete operations
    $('body').on('click', '.js-delete-project', function (e) {
        e.preventDefault();
        var $button = $(this);
        var id = $button.data("project-id");

        bootbox.confirm("<p style='color:black;'>Are you sure to delete this Project Record?</p>", function (result) {
            if (result) {
                var params = $.extend({}, params);
                params['url'] = '/Delete/DeleteProject?id=' + id;
                params['requestType'] = 'DELETE';
                params['successCallbackFunction'] = function (resultfinal) { };
                params['errorCallBackFunction'] = function () { };
                commonAjax(params);
            }
            else {
                bootbox.hideAll();
            }
            return false;
        });
    });

    $('body').on('click', '.js-delete-workex', function (e) {
        e.preventDefault();
        var $button = $(this);

        var id = $button.data("workex-id");
        bootbox.confirm("<p style='color:black;'>Are you sure to delete this Work Experience Record?</p>", function (result) {
            if (result) {

                var params = $.extend({}, params);
                params['url'] = '/Delete/DeleteWorkExperience?id=' + id;
                params['requestType'] = 'DELETE';
                params['successCallbackFunction'] = function (resultfinal) { };
                params['errorCallBackFunction'] = function () {
                    bootbox.alert("<p style='color:black;'>Error!</p>");
                }
                commonAjax(params);
            }
            else {
                bootbox.hideAll();
            }
            return false;
        });
    });

    $('body').on('click', '.js-delete-skill', function (e) {
        e.preventDefault();
        var $button = $(this);
        var userID = $("#userId").val();
        var skillID = $button.data("skill-id");
        var formData = {
            "userID": $("#userId").val(),
            "skillID": skillID,
        };
        bootbox.confirm("<p style='color:black;'>Are you sure to delete this Skill?</p>", function (result) {
            if (result) {

                var params = $.extend({}, params);
                params['url'] = '/Delete/DeleteSkill';
                params['requestType'] = 'DELETE';
                params['data'] = formData;
                params['successCallbackFunction'] = function () { };
                params['errorCallBackFunction'] = function () {
                    bootbox.alert("<p style='color:black;'>Error!</p>");
                }
                commonAjax(params);
            }
            else {
                bootbox.hideAll();
            }
            return false;
        });
    })

    $('body').on('click', '.js-delete-education', function (e) {
        e.preventDefault();
        var $button = $(this);
        var educationId = $button.data("education-id");
        var courseId = $button.data("courseid");
        var formData = {
            "educationId": educationId,
        };
        bootbox.confirm("<p style='color:black;'>Are you sure to delete this Education Record?</p>", function (result) {
            if (result) {

                var params = $.extend({}, params);
                params['url'] = '/Delete/DeleteEducation';
                params['requestType'] = 'DELETE';
                params['data'] = formData;
                params['successCallbackFunction'] = function () { };            
                params['errorCallBackFunction'] = function () {
                    bootbox.alert("<p style='color:black;'>Error!</p>");
                }
                commonAjax(params);
            }
            else {
                bootbox.hideAll();
            }
            $("input[type=radio][value=" + courseId + "]").prop("disabled", false);
            return false;
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
                        response(data);
                    },
                    error: function (data) {

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
            bootbox.alert("<b style='color:black;'>" + item + " already added.</b>");
        }
    });
});

function removeBackdrop() {
    $('body').removeClass('modal-open');
    $('.modal-backdrop').remove();
}

function clearFields() {
    $(":input([type='text']):not([readonly]):not([type='submit']):not([type='date']):not([type='hidden'])").val('');
    $('select').val('');
    $('input[type="checkbox"]').prop('checked', false);
    $('input').removeClass("error valid");
    $('select').removeClass("error valid");
    $('textarea').removeClass("error valid");
    $("label").remove(".error,.valid");
}

function displaySearchButtonIfAdmin() {
    var checkadmin = $("#checkIsAdmin").data("checkadmin");
    if (checkadmin === "True") {
        $("#btnSearchNav").css("display", "block");
    }
}