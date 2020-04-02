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
    $("body").on('click', '.js-save-project', function () {
        $('#modalProject').validate({
            rules: {

            }
        })
    })

    //avijeet
    $("body").on('click', '.js-save-skill', function () {
        $('#modalSkills').validate({
            rules: {

            }
        })
    })

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


