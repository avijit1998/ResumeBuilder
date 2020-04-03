$(document).ready(function () {

    // Basic Info Validation
    $("body").on('click', '.js-edit-user, .js-save-user', function () {
        event.preventDefault();

        $.validator.addMethod("regex", function (value, element, regexpr) {
            return this.optional(element) || !(regexpr.test(value));
        }, "Please enter valid data.");

        $.validator.addMethod("notStartWithNum", function (value, element, regexpr) {
            return this.optional(element) || !(regexpr.test(value[0]));
        }, "Shouldn't start with number.");

        $.validator.addMethod("exactlength", function (value, element, param) {
            return this.optional(element) || value.length == param;
        }, $.validator.format("Please enter {0} digits."));

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
                    exactlength: 10
                },
                Gender: {
                    required: true
                },
                DateOfBirth: {
                    required: true,
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
                    required: 'Please enter your date of birth.'
                },
                Summary: {
                    required: 'Please enter summary.'
                },
                chkLanguages: {
                    required: 'Please select at least one Language.'
                }
            },
            errorPlacement: function (error, element) {
                error.insertAfter(element.closest('.error-msg'));
            }
        });

        $("#basicInfoForm").removeAttr("novalidate");
    });

    // Education Details Validation
    $("body").on('click', '.js-add-education,.js-save-education, .js-edit-education', function () {
        $("#modalEducationDetails").on('shown.bs.modal', function () {

            $.validator.addMethod("regex", function (value, element, regexpr) {
                return this.optional(element) || regexpr.test(value);
            }, "Invalid input.");

            $('#educationDetailsForm').validate({
                rules: {
                    Stream: {
                        required: true,
                        regex: /^[A-Za-z \W]+$/
                    },
                    BoardOrUniversity: {
                        required: function () {
                            if ($("#boardType").val() === "")
                                return true;
                            return false;
                        }
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
                    Stream: {
                        required: "Please enter your Stream.",
                        regex: "Please enter valid Stream."
                    },
                    BoardOrUniversity: {
                        required: "Please select your Board/University."
                    },
                    PassingYear: {
                        required: "Please enter your passing year.",
                        regex: "Please enter valid year of passing."
                    },
                    TotalPercentageOrCGPAValue: {
                        required: function () {
                            var cgpa = $("#cgpa").prop('checked');

                            if (cgpa)
                                return "Please enter CGPA.";
                            return "Please enter Percentage.";
                        },
                        regex: "Please enter valid value."
                    }
                }
            });
            $("#educationDetailsForm").removeAttr("novalidate");
        });

    });

    // Project Details Validation
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

            $("#projectDetailsForm").removeAttr("novalidate");
        });
    });

    // Work Experience Validation
    $("body").on('click', '.js-add-workex, .js-edit-workex', function () {

        $("#modalWorkExperience").on('shown.bs.modal', function () {

            $.validator.addMethod("regex", function (value, element, regexpr) {
                return this.optional(element) || regexpr.test(value);
            }, "Invalid input.");

            jQuery.validator.addMethod('selectcheck', function (value) {
                return (value != '0');
            }, "This field is required.");

            $('#workExperienceForm').validate({
                rules: {
                    OrganizationName: {
                        required: true
                    },
                    Designation: {
                        required: true
                    },
                    StartMonth: {
                        selectcheck: true
                    },
                    StartYear: {
                        selectcheck: true
                    },
                    EndMonth: {
                        selectcheck: function() {
                            if($("#checkWorking").is(':checked')){
                                return true;
                            }
                            else {
                                return false;
                            }
                        }
                    },
                    EndYear: {
                        selectcheck: function () {
                            if ($("#checkWorking").is(':checked')) {
                                return true;
                            }
                            else {
                                return false;
                            }
                        }
                    }
                },
                messages: {
                    OrganizationName: {
                        required: "Please enter the organisation name."
                    },
                    Designation: {
                        required: "Please enter your designation."
                    },
                    StartMonth: {
                        selectcheck: "Please enter the starting month."
                    },
                    StartYear: {
                        selectcheck: "Please enter the starting year."
                    },
                    EndMonth: {
                        selectcheck: "Please enter the ending month."
                    },
                    EndYear: {
                        selectcheck: "Please enter the ending year."
                    }
                }
            });

            $("#workExperienceForm").removeAttr("novalidate");

        });
    });

});


