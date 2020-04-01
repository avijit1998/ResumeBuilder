// avijit
// first check whether modal/div exists and for that modal/div choose a "selector" and provide 
// an event and only when that event is triggered 
// the validate function should be called
//$(document/body).on("event","selector",function())
//$('#projectFormId').validate({
//    rules: {

//    }
//})



$(document).ready(function () {

    //avijeet
    $("body").on('click', '.js-add-project, .js-edit-project', function () {

        $("#modalProject").on('shown.bs.modal', function () {
            
            $.validator.addMethod("regex", function (value, element, regexpr) {
                return this.optional(element) || regexpr.test(value);
            }, "Invalid input.");

            $('#projectDetailsForm').validate({
                rules: {
                    ProjectTitle: {
                        required: true,
                        regex: /^[ A-Za-z0-9_@./#&+-]*$/
                    },
                    ProjectRole: {
                        required: true,
                        regex: /^[ A-Za-z0-9_@./#&+-]*$/
                    },
                    DurationInMonth: {
                        required: true,
                        regex: /^\d+$/
                    },
                    Description: {
                        required: true
                    }
                },
                messages: {
                    ProjectTitle: {
                        required: "Please enter the project title.",
                        regex: "Please enter a valid project title."
                    },
                    ProjectRole: {
                        required: "Please enter your role in the project.",
                        regex: "Please enter a valid role"
                    },
                    DurationInMonth: {
                        required: "Please enter the project duration.",
                        regex: "Please enter a numeric data."
                    },
                    Description: {
                        required: "Please enter the project description."
                    }
                }
            });
        });
    });

    //avijeet
    //$("body").on('click', '.js-add-skill', function () {

    //    $("#modalSkills").on('shown.bs.modal', function () {

            
    //    });
    //});

    //Abhishek
    $("body").on('click', '.js-save-workex', function () {
        $('#modalWorkExperience').validate({
            rules: {

            }
        })
    })

    //Anil
    $("body").on('click', '.js-save-user', function () {
        console.log('hi u r in validation');
        debugger;
        $('#modalBasicInfo').validate({
            rules: {

            }
        })
    })

    //Rahul
    $("body").on('click', '.js-save-education', function () {
        $('#modalEducationDetails').validate({
            rules: {

            }
        })
    })
});


