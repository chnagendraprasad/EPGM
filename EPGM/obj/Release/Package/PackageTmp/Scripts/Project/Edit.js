/// <reference path="../../Common/_references.js" />
/// <reference path="../../Common/helper.js" />

jQuery(document).ready(function () {
    $("#Project").validate({
        rules: {
            DistrictCode: {
                required: true,
            },
            ProjectName: {
                required: true,
            },
            ProjectCode: {
                required: true,
            },
        },
        showErrors: validateForm,
        submitHandler: function (form) {
            ajaxPostForm("Admin/Project/Edit", form, function (retval) {
                if (retval == "True") {
                    closePopup();
                }
                else {
                    hAlert(retval);
                }
            });
        }
    });
});
