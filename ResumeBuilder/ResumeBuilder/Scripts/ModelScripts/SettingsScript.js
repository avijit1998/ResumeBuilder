$(document).ready(function () {

    $(document).on("click", "#settingsBtn", function () {

         $.ajax({
            url: "/Settings/SetUserSettingStatus",
            type: 'GET',
            success: function (userSettingsStatus) {

                if (userSettingsStatus.WorkExperienceStatus == true) {
                    $("#cbWorkex").prop('checked', true);
                }
                else {
                    $("#cbWorkex").prop('checked', false);
                }

                if (userSettingsStatus.LanguagesStatus == true) {
                    $("#cbLanguage").prop('checked', true);
                }
                else {
                    $("#cbLanguage").prop('checked', false);
                }
                if (userSettingsStatus.SkillsDetailsStatus == true) {
                    $("#cbSkills").prop('checked', true);
                }
                else {
                    $("#cbSkills").prop('checked', false);
                }
                if (userSettingsStatus.ProjectDetailsStatus == true) {
                    $("#cbProjects").prop('checked', true);
                }
                else {
                    $("#cbProjects").prop('checked', false);
                }
                if (userSettingsStatus.EducationalDetailsStatus == true) {
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
        var settingStatus = {
            "WorkExperienceStatus": $("#cbWorkex").prop("checked"),
            "EducationalDetailsStatus": $("#cbEducation").prop("checked"),
            "ProjectDetailsStatus": $("#cbProjects").prop("checked"),
            "SkillsDetailsStatus": $("#cbSkills").prop("checked"),
            "LanguagesStatus": $("#cbLanguage").prop("checked")
        };
        $.ajax({
            url: "/Settings/SaveSettingStatus",
            type: 'post',
            data: settingStatus,
            success: function () {
                $("#settingsModal").modal("hide");
            }
        });
    });

});