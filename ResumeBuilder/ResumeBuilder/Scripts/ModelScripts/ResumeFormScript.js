
$(document).ready(function () {
    $("body").on('click', '#educationDetails', function () {
        
        if ($('#spanId1').data('value') == 1) {
            var disableValue = $('#spanId1').data('value');
            $("input[type=radio][value=" + disableValue + "]").prop("disabled", true);
        }

        if ($('#spanId2').data('value') == 2) {
            var disableValue = $('#spanId2').data('value');
            $("input[type=radio][value=" + disableValue + "]").prop("disabled", true);
        }
    });
    
   

    //empty auto fill data
    $("body").on('click', 'hidden.bs.modal', function () {
       //
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
            debugger;
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

    //save summary info of user
    $("body").on("click", "#btnSaveSummary", function () {
        debugger;
        var user = {};
        user.Summary = $("#txtSummary").val();
        debugger;
        user.UserID = $("#userId").val();
        $.ajax({
            type: "POST",
            url: '/Resume/SaveSummary',
            data: '{user: ' + JSON.stringify(user) + '}',
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            success: function () {
                $("#modalSummary").modal("hide");
                bootbox.alert("<b style='color:black;'>Summary details successfully saved.</b>");
            },
            error: function () {
                bootbox.alert("Error!");

            }
        });
        return false;
    });


    //save basic info of user
    $("body").on("click", "#btnSaveBasicInfo", function () {
        var user = {
            "UserID": $("#userId").val(),
            "Name": $("#txtFullName").val(),
            "Gender": $('input[name="Gender"]:checked').val(),
            "DateOfBirth": $("#dateDOB").val(),
            "PhoneNumber": $("#txtPhoneNumber").val(),
            "LanguageIds": []
        };
        debugger;

        $('input[type="checkbox"]:checked').each(function (e, el) {
            user.LanguageIds.push($(el).val());
        })
        $.ajax({
            type: "POST",
            url: '/Resume/SaveBasicInformation',
            data: '{addUserViewModel: ' + JSON.stringify(user) + '}',
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            success: function () {
                $("#modalBasicInfo").modal("hide");
                bootbox.alert("<b style='color:black;'>Basic information successfully saved.</b>");
            },
            error: function () {
                bootbox.alert("Error!");

            }
        });
        return false;
    });

    //save projects info of user
    $("body").on("click", "#btnSaveProjectDetials", function () {
        var project = {};
        project.UserID = $("#userId").val();
        project.Title = $("#txtTitle").val();
        project.ProjectRole = $('#txtProjectRole').val();
        project.Duration = $("#txtDuration").val();
        project.Description = $("#txtDescription").val();
        debugger;
        $.ajax({
            type: "POST",
            url: '/Resume/SaveProjectDetails',
            data: '{project: ' + JSON.stringify(project) + '}',
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            success: function () {
                $("#modalProject").modal("hide");
                bootbox.alert("<b style='color:black;'>Project details successfully saved.</b>");
            },
            error: function () {
                bootbox.alert("Error!");

            }
        });
        return false;
    });

    //save work experience info of user
    $("body").on("click", "#btnSaveWorkExperience", function () {
        var project = {};
        project.UserID = $("#userId").val();
        project.StartMonth = $("#selectStartMonth").val();
        project.EndMonth = $('#selectEndMonth').val();
        project.StartYear = $("#selectStartYear").val();
        project.EndYear = $("#selectEndYear").val();
        project.OrganizationName = $("#txtCompanyName").val();
        project.Role = $('#txtRole').val();
        if ($("#checkWorking").prop('checked') == true)
            project.CurrentlyWorking = true;
        else
            project.CurrentlyWorking = false;

        $.ajax({
            type: "POST",
            url: '/Resume/SaveWorkExperience',
            data: '{workExperience: ' + JSON.stringify(project) + '}',
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            success: function () {
                $("#modalWorkExperience").modal("hide");
                bootbox.alert("<b style='color:black;'>Work experience successfully saved.</b>");

            },
            error: function () {
                bootbox.alert("Error!");

            }
        });
        return false;
    });


    //save Educational details of user
    $("body").on("click", "#btnSaveEducationalDetails", function () {
        var educationalDetails = {};

        educationalDetails.UserID = $("#userId").val();
        educationalDetails.CourseId = $('input[name="courseOption"]:checked').val();
        educationalDetails.Board = $("#boardType").val();
        educationalDetails.PassingYear = $("#yearOfPassing").val();

        if ($('input[name="courseOption"]:checked').val() != '1') {
            educationalDetails.Stream = $("#stream").val();
        }

        educationalDetails.CGPAOrPercentage = $('input[name="marksOption"]:checked').val();
        educationalDetails.TotalPercentorCGPAValue = $("#txtMarks").val();

        //disable radio button for client-side
        if (educationalDetails.CourseId == 1 || educationalDetails.CourseId == 2) {
            $("input[type=radio][value=" + educationalDetails.CourseId + "]").prop("disabled", true);
            $("input[type=radio][value=" + educationalDetails.CourseId + "]").prop("checked", false);
            $(".all-other").hide();
        }

        $.ajax({
            type: "POST",
            url: '/Resume/SaveEducationalDetails',
            data: '{educationalDetails: ' + JSON.stringify(educationalDetails) + '}',
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            success: function () {
                $("#modalEducationDetails").modal("hide");
                bootbox.alert("<b style='color:black;'>Educational details successfully saved.</b>");
            },
            error: function () {
                bootbox.alert("Error!");
            }
        });
        return false;
    });

    $("body").on("click", "#btnSaveSkills", function () {
        var skillDetails = {
            "UserID": $("#userId").val(),
            "SkillNames": []
        };

        $(".skillItem").each(function (index) {
            skillDetails.SkillNames.push($(this).text());
        })

        $.ajax({
            type: "POST",
            url: '/Resume/SaveUserSkills',
            data: '{addUserSkillsViewModel: ' + JSON.stringify(skillDetails) + '}',
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            success: function () {
                $("#modalSkills").modal("hide");
                bootbox.alert("<b style='color:black;'>Skills successfully saved.</b>");
            },
            error: function () {
                bootbox.alert("<b style='color:black;'>Some error occurred.</b>");
            }
        });
        return false;
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
        debugger;
        $(".skillItem").each(function (index) {
            var skillValue = $(this).text();
            if (skillValue == item) {
                isSkillFound = 1;
                return false;
            }
        });

        if (isSkillFound == 0) {
            $("#skillMenu").append('<li class="skillItem">' + item + '</li>');
        } else {
            alert(item + " already added.");
        }
    });


    $("body").on("click", "#preview", function (e) {
        var ids = $('#sessionIdFetch').data('sessionid');
        $.ajax({
            url: "/Resume/PreviewUser/" + ids,
            method: "GET",
            success: function (result) {
                console.log(result);
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
                console.log("error!");
            }
        });
    });

  

    
});