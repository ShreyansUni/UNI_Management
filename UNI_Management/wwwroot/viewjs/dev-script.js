$(document).ready(function () {
    // sweet alert error popup
    if (sweetAlertErrorPopup != undefined && sweetAlertErrorPopup != "") {
        fnSweetMessage('error', 'Error', sweetAlertErrorPopup);
    }

    // sweet alert success popup
    if (sweetAlertSuccessPopup != undefined && sweetAlertSuccessPopup != "") {
        fnSweetMessage('success', 'Success', sweetAlertSuccessPopup);
    }

    // sweet alert warning popup
    if (sweetAlertWarningPopup != undefined && sweetAlertWarningPopup != "") {
        fnSweetMessage('warning', 'Warning', sweetAlertWarningPopup);
    }
});
