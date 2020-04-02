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
    $("body").on('click', '.js-edit-user, .js-save-user', function () {
        event.preventDefault();
        $.validator.addMethod("regex", function (value, element, regexpr) {
            return this.optional(element) || !(regexpr.test(value));
        }, "Please enter valid data.");

        $.validator.addMethod("notStartWithNum", function (value, element, regexpr) {
            return this.optional(element) || !(regexpr.test(value[0]));
        }, "Shouldn't start with number.");

        $.validator.addMethod("birth", function (value, element) {
            var year = value.split('-');
            return value.match(/^\d\d\d\d?\-\d\d?\-\d\d$/) && parseInt(year[0]) <= 2002;
        }, "Minimum age is 18 Years.");

        $.validator.addMethod("notFutureDate", function (value, element) {
            var now = new Date();
            var myDate = new Date(value);
            var past = new Date("1800-01-01")
            var x = !(myDate > now || myDate < past);
            return x;
        }, "Please enter a valid date.");

        $('#formBasicInfo').validate({
            rules: {
                Name: {
                    required: true,
                    regex: /[^a-zA-Z 0-9]/,
                    notStartWithNum: /[\d]/
                },
                PhoneNumber: {
                    required: true,
                    regex: /[^\d]/,
                    minlength: 10,
                    maxlength: 13
                },
                Gender: {
                    required: true
                },
                DateOfBirth: {
                    required : true,
                    notFutureDate: true,
                    birth: true
                },
                Summary: {
                    required: true
                },
                chkLanguages: {
                    required: true
                }
            },
            messages: {
                Name: {
                    required: 'Please enter your name',
                    regex: 'Please enter a valid name'
                },
                PhoneNumber: {
                    required: 'Please enter your phone number.',
                    regex: 'Please enter a valid phone number.'
                },
                Gender: {
                    required: 'Please select your gender.'
                },
                DateOfBirth: {
                    required : 'Please enter your date of birth.'
                },
                Summary: {
                    required: 'Please enter summaary.'
                },
                chkLanguages: {
                    required: 'One language should be selected.'
                }
            },
            errorPlacement: function(error, element) {
                error.insertAfter(element.closest('.error-msg'));
            }
        });
    });

    //Rahul
    $("body").on('click', '.js-save-education', function () {
        $('#modalEducationDetails').validate({
            rules: {

            }
        })
    })
});


