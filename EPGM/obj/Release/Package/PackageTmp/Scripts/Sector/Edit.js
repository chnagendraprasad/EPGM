/// <reference path="../../Common/_references.js" />
/// <reference path="../../Common/helper.js" />

jQuery(document).ready(function () {
    $("#Sector").validate({
        rules: {
            SectorName: {
                required: true,
            },
            SectorCode: {
                required: true,
            },
            ProjectCode: {
                required: true,
            },
            DistrictCode: {
                required: true,
            },
        },
        showErrors: validateForm,
        submitHandler: function (form) {
            ajaxPostForm("Admin/Sector/Edit", form, function (retval) {
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
