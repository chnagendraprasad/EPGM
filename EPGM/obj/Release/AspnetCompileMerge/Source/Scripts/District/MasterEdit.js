/// <reference path="../../Common/_references.js" />
/// <reference path="../../Common/helper.js" />

jQuery(document).ready(function () {
    $("#District").validate({
        rules: {
            DistrictName: {
                required: true,
            },
        },
        showErrors: validateForm,
        submitHandler: function (form) {
            ajaxPostForm("District/Edit", form, function (retval) {
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
