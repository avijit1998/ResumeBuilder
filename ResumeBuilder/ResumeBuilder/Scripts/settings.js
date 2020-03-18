$(document).ready(function () {

    var result = [-1, -1, -1, -1, -1];
     
    

    $(document).on("click", "#settingsBtn", function () {
        $.ajax({
            url: "/Resume/settingsValue",
            type: 'GET',
            success: function (ob) {
                debugger;
                if (ob.setWorkex === 0) {
                    $("#workex").prop('checked', true);
                }
                else {
                    $("#workex").prop('checked', false);
                }

                if (ob.setContact === 0) {
                    $("#contact").prop('checked', true);
                }
                else {
                    $("#contact").prop('checked', false);
                }
                if (ob.setSkills === 0) {
                    $("#skills").prop('checked', true);
                }
                else {
                    $("#skills").prop('checked', false);
                }
                if (ob.setProject === 0) {
                    $("#projects").prop('checked', true);
                }
                else {
                    $("#projects").prop('checked', false);
                }
                if (ob.setEducation === 0) {
                    $("#education").prop('checked', true);
                }
                else {
                    $("#education").prop('checked', false);
                }

                $("#myModal").modal("show");

            }

        });

    });



    $(document).on("click", "#btnSave", function () {
        result = [-1, -1, -1, -1, -1];
        if ($("#workex").prop("checked") === true)
            result[0] = 0;
        if ($("#projects").prop("checked") === true)
            result[1] = 0;
        if ($("#education").prop("checked") === true)
            result[2] = 0;
        if ($("#skills").prop("checked") === true)
            result[3] = 0;
        if ($("#contact").prop("checked") === true)
            result[4] = 0;
        $.ajax({
            url: "/Resume/DisplayDetails",
            type: 'post',
            data: {
               
                finalresult: result
            },
            success: function () {
                $("#myModal").modal("hide");
            }

        });
    });



});