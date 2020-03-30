$(document).ready(function () {

    var result = [-1, -1, -1, -1, -1];
     
    

    $(document).on("click", "#settingsBtn", function () {
        $.ajax({
            url: "/Resume/settingsValue",
            type: 'GET',
            success: function (ob) {

                if (ob.setWorkex == 0) {
                    $("#cbWorkex").prop('checked', true);
                }
                else {
                    $("#cbWorkex").prop('checked', false);
                }

                if (ob.setContact == 0) {
                    $("#cbLanguage").prop('checked', true);
                }
                else {
                    $("#cbLanguage").prop('checked', false);
                }
                if (ob.setSkills == 0) {
                    $("#cbSkills").prop('checked', true);
                }
                else {
                    $("#cbSkills").prop('checked', false);
                }
                if (ob.setProject == 0) {
                    $("#cbProjects").prop('checked', true);
                }
                else {
                    $("#cbProjects").prop('checked', false);
                }
                if (ob.setEducation == 0) {
                    $("#cbEducation").prop('checked', true);
                }
                else {
                    $("#cbEducation").prop('checked', false);
                }

                $("#settingsModal").modal("show");

            }

        });

    });



    $(document).on("click", "#btnSave", function () {
        result = [-1, -1, -1, -1, -1];
        if ($("#cbWorkex").prop("checked") == true)
            result[0] = 0;
        if ($("#cbProjects").prop("checked") == true)
            result[1] = 0;
        if ($("#cbEducation").prop("checked") == true)
            result[2] = 0;
        if ($("#cbSkills").prop("checked") == true)
            result[3] = 0;
        if ($("#cbLanguage").prop("checked") == true)
            result[4] = 0;
        $.ajax({
            url: "/Resume/DisplayDetails",
            type: 'post',
            data: {
               
                finalresult: result
            },
            success: function () {
                $("#settingsModal").modal("hide");
            }

        });
    });



});