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
   // $("body").on('click', '.js-save-workex', function () {

        //$.validator.addMethod("regex", function (value, element, regexpr) {
        //    return this.optional(element) || regexpr.test(value);
        //}, "Invalid input.");

    //    $('#modalWorkExperience').validate({
    //        rules: {
    //            OrganizationName: {
    //                required: true
    //            },
    //            Designation: {
    //                required: true
    //            },
    //            StartMonth: {
    //                required: true
    //            },
    //            StartYear: {
    //                required: true
    //            }

    //        },
    //        messages: {
    //        OrganizationName: {
    //                required: "Please enter the Organisation name"
    //        },
    //        Designation: {
    //            required: "Please enter the designation"
    //        },
    //        StartMonth: {
    //            required: "Please enter the starting month"
    //        },
    //        StartYear: {
    //            required: "Please enter the starting year"
    //        }

    //        }

    //    })
    //})

    //Anil
    $("body").on('click', '.js-edit-user, .js-save-user', function () {
        event.preventDefault();
        $.validator.addMethod("regex", function (value, element, regexpr) {
            return this.optional(element) || !(regexpr.test(value));
        }, "Please enter valid data.");

        $.validator.addMethod("notStartWithNum", function (value, element, regexpr) {
            return this.optional(element) || !(regexpr.test(value[0]));
        }, "Shouldn't start with number.");

        $.validator.addMethod("notFutureDate", function (value, element) {
            var now = new Date();
            var myDate = new Date(value);
            var past = new Date("1800-01-01");
            var year = value.split('-');
            return this.optional(element) || value.match(/^\d\d\d\d?\-\d\d?\-\d\d$/) && !(myDate > now || myDate < past);
        }, "Please enter a valid date.");

        $('#basicInfoForm').validate({
            rules: {
                Name: {
                    required: true,
                    regex: /[^a-zA-Z. 0-9]/,
                    notStartWithNum: /[\d/\.]/
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
                    notFutureDate: true
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
    $("body").on('click', '.js-add-education,.js-save-education, .js-edit-education', function () {
        $("#modalEducationDetails").on('shown.bs.modal', function () {

            $.validator.addMethod("regex", function (value, element, regexpr) {
                return this.optional(element) || regexpr.test(value);
            }, "Invalid input.");



            $('#educationDetailsForm').validate({
                rules: {
                    //courseOption: {
                    //    required: function () {
                    //        var course1 = $("#course1").prop('checked');
                    //        var course2 = $("#course2").prop('checked');
                    //        var course3 = $("#course3").prop('checked');
                    //        var course4 = $("#course4").prop('checked');
                    //        if (!(course1 || course2 || course3 || course4))
                    //            return true;
                    //        return false;
                    //    }
                    //},
                    Stream: {
                        required: true,
                        regex: /^[A-Za-z ]+$/
                    },
                    PassingYear: {
                        required: true,
                        regex: /^[12][0-9]{3}$/
                    },
                    TotalPercentageOrCGPAValue: {
                        required: true,
                        regex: /^(([0]|[0-9]\.(\d?\d?)|[10].[0])|(100$|^\d{0,2}(\.\d{1,2})? *%?))$/
                    }
                },
                messages: {
                    //courseOption: {
                    //    required: "Please check your Course first."
                    //},
                    Stream: {
                        required: "Enter your Stream.",
                        regex: "Enter valid Stream."
                    },
                    PassingYear: {
                        required: "Enter your passing year.",
                        regex: "Enter valid year of passing."
                    },
                    TotalPercentageOrCGPAValue: {
                        required: function () {
                            var cgpa = $("#cgpa").prop('checked');

                            if (cgpa)
                                return "Enter CGPA.";
                            return "Enter Percentage.";
                        },
                        regex: "Enter valid value."
                    }
                }
            })
        })

    });
});


