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
    $("body").on('click', '.js-save-education', function () {
        $('#modalEducationDetails').validate({
            rules: {

            }
        })
    })
});


