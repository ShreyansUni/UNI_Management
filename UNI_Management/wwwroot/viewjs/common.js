

//success popup
function
    fnSweetMessage(msgType, title, message, confirmButtonText = "Ok", showCancelButton = false, confirmButtonColor = '#5149e5') {
    return swal({
        title: title,
        text: message,
        icon: msgType,
        buttons: {
            cancel: {
                text: "Cancel",
                visible: showCancelButton,
                className: 'btn btn-secondary',
            },
            confirm: {
                text: confirmButtonText,
                className: 'btn btn-primary',
                closeModal: true
            }
        },
        dangerMode: false,
        closeOnClickOutside: false, // Disable clicking outside the popup
    });
}

//error popup
function fnShowError(error) {
    Swal.fire({
        title: 'Oops!',
        text: 'Your error message',
        icon: 'error'
    });
}

function IsNullOrEmptyString(str) {
    if (str == undefined || str == "" || str == null || str.trim() == "")
        return true;

    return false;
}

function IsUndefinedOrNull(obj) {
    if (obj == undefined || obj == null)
        return true;

    return false;
}

function IsValueUndefinedOrNull(val) {
    if (val == undefined || val == null)
        return true;

    return false;
}

function fnAddValidationErrorClass(element) {
    var id = element.attr('id');
    var errorSpanId = "#error_" + id;
    $(errorSpanId).show();
    element.addClass('border border-2 border-danger');
    element.focus();
}

function fnRemoveValidationErrorClass(element) {
    element.removeClass('border border-2 border-danger');
}

//Remove error message from span tag
function fnRemoveValidationMessage(event) {
    var id = event.id;
    $(event).removeClass("border border-danger");
    $('#error_' + id).text('');
}

function fnToggleSidebar() {
    //$('#sidebar-main').toggleClass('d-none');
    $('.layout-wrapper').toggleClass('slider-custom');
    //$('#sidebar-main').toggleClass('slidebar-left');
    $('#sidebar-main').toggleClass('d-none');
}


//Notification Modal
function fnOpenNotificationModal() {
    var myOffcanvas = document.getElementById('notificationOffCanvas');
    var bsOffcanvas = new bootstrap.Offcanvas(myOffcanvas);
    bsOffcanvas.show();
}


//loader
function fnShowMainProgress() {
    $(".main-loader").removeClass("loader-hide");
    $(".main-loader").addClass("loader-show");
}

function fnHideMainProgress() {
    $(".main-loader").removeClass("loader-show");
    $(".main-loader").addClass("loader-hide");
}

function submitApprovalForm() {
    fnShowMainProgress(); // Show the loader

    // Submit the form
    $('form.floating-wrapper').submit(); // Ensure this selector targets your form correctly
}



function getNiceScrollResize() {
    setTimeout(function () {
        $(".table-responsive").getNiceScroll().resize();
    }, 400)
}

function deleteAllCookies() {
    const cookies = document.cookie.split(";");

    for (let i = 0; i < cookies.length; i++) {
        const cookie = cookies[i];
        const eqPos = cookie.indexOf("=");
        const name = eqPos > -1 ? cookie.substr(0, eqPos) : cookie;
        document.cookie = name + "=;expires=Thu, 01 Jan 1970 00:00:00 GMT";
    }
}

function fnCallPostMethod(route, menuId) {
    $.ajax({
        type: 'GET',
        url: route,
        data: { menuId: menuId },
        contenttype: "application/json; charset=utf-8",
        datatype: 'json',
        success: function (data) {

        },
        error: function (req, status, err) {

        }
    });
}

//Email Validation
function CheckEmailValidation(fieldObj) {
    let pattern = /^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,10})+$/
    if (pattern.test(fieldObj)) {
        return true;
    }
    else {
        return false;
    }
}
function fnFilterDropDown() {
    var filterCollapse = document.getElementById('filterModel')
    new bootstrap.Collapse(filterCollapse, {
        toggle: true
    });

    getNiceScrollResize();
}

/**
 *  Function to set the value of an input element or default to "N/A"
 */
function setValueOrDefault(element, value) {
    // Check if the provided value is null or an empty string
    if (value === null || value === '') {
        // Set the element's value to "N/A" if the value is null or empty
        $(element).val("N/A");
    } else {
        // Otherwise, set the element's value to the provided value
        $(element).val(value);
    }
}

/**
 * Function to set the text of an element or default to "N/A"
 */
function setTextOrDefault(element, value) {
    // Check if the provided value is null or an empty string
    if (value === null || value === '') {
        // Set the element's text to "N/A" if the value is null or empty
        $(element).text("N/A");
    } else {
        // Otherwise, set the element's text to the provided value
        $(element).text(value);
    }
}

// common.js

/**
 * Shows a confirmation popup and performs an action if confirmed.
 * @param {string} url - The URL for the delete action.
 * @param {number} encodeDomainId - The ID of the domain to delete.
 * @param {function} onSuccess - The callback function to execute on successful delete.
 */
//function confirmAndDelete(url, encodeDomainId, onSuccess) {
//    Swal.fire({
//        title: 'Are you sure?',
//        text: "You won't be able to revert this!",
//        icon: 'warning',
//        showCancelButton: true,
//        confirmButtonColor: '#3085d6',
//        cancelButtonColor: '#d33',
//        confirmButtonText: 'Yes, delete it!'
//    }).then((result) => {
//        if (result.isConfirmed) {
//            $.ajax({
//                type: 'POST',
//                url: url,
//                data: { encodeDomainId: encodeDomainId },
//                success: function (response) {
//                    if (response.success) {
//                        Swal.fire('Deleted!', 'The domain has been deleted.', 'success').then(onSuccess);
//                    } else {
//                        Swal.fire('Error!', 'There was an error deleting the domain.', 'error');
//                    }
//                },
//                error: function () {
//                    Swal.fire('Error!', 'There was an error processing your request.', 'error');
//                }
//            });
//        }
//    });
//}

function confirmAndDeletePopup(onConfirm) {
    Swal.fire({
        title: 'Are you sure?',
        text: "This action will permanently delete the data!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#d33',
        cancelButtonColor: '#e44141',
        confirmButtonText: 'Yes, delete it !'
    }).then((result) => {
        if (result.isConfirmed) {
            onConfirm(); // Call the provided callback if user confirms
        }
    });
}

function confirmAndRejectPopup(onConfirm) {
    Swal.fire({
        title: 'Are you sure?',
        text: "Are you sure you want to Reject",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, Reject it!'
    }).then((result) => {
        if (result.isConfirmed) {
            onConfirm(); // Call the provided callback if user confirms
        }
    });
}

function confirmAndTicketCompletePopup(onConfirm) {
    Swal.fire({
        title: 'Are you sure?',
        text: "Are you sure you want to Complete",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Done'
    }).then((result) => {
        if (result.isConfirmed) {
            onConfirm(); // Call the provided callback if user confirms
        }
    });
}


// Confirm Popup for Revert
function confirmAndRevertPopup(onConfirm) {
    Swal.fire({
        title: 'Are you sure?',
        text: "You won't be able to revert this!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#62c370',
        cancelButtonColor: '#e44141',
        confirmButtonText: 'Yes, revert it!'
    }).then((result) => {
        if (result.isConfirmed) {
            onConfirm(); // Call the provided callback if user confirms
        }
    });
}


$(document).ready(function () {
    // Function to update display counts from hidden fields
    function updateDisplayCounts() {
        var pendingCount = $('#pendingCountHidden').val();
        var workingCount = $('#workingCountHidden').val();
        var completedCount = $('#completedCountHidden').val();

        $('#pendingCount h2').text(pendingCount);
        $('#workingCount h2').text(workingCount);
        $('#completedCount h2').text(completedCount);
    }

    // Initial load
    updateDisplayCounts();
});



$(document).ready(function () {
    // Initialize Select2
    $('.select2-example').select2({
        width: '100%' // Adjust width as necessary
    });
});

document.addEventListener('DOMContentLoaded', function () {
    // Function to focus the input field
    function focusInputField() {
        // Your logic to focus the input field
        console.log('Input field should be focused!');
        // For example, you might want to select the first input inside the Select2 container:
        let inputField = document.querySelector('.select2-container--open .select2-search__field');
        if (inputField) {
            inputField.focus();
        }
    }

    // Add event listener to elements with specific class names
    document.addEventListener('click', function (event) {
        // Check if the clicked element has the required classes
        if (event.target.closest('.select2.select2-container.select2-container--default.select2-container--below.select2-container--open.select2-container--focus')) {
            focusInputField();
        }
    });
});


//function Clearfilter() {
//    // Clear Select2 dropdown
//    $('#domainDropdown').val(null).trigger('change'); // Clear the selection and update Select2

//    // Reset other filters if necessary
//    const textInput = document.getElementById('filterTextInput');
//    if (textInput) {
//        textInput.value = '';
//    }

//    // Add more filters to reset as needed
//}
function showLogoutConfirmation() {
    // Display SweetAlert confirmation
    Swal.fire({
        title: 'Are you sure?',
        text: "Do you really want to log out?",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Log Out',
        cancelButtonText: 'Cancel'
    }).then((result) => {
        if (result.isConfirmed) {
            // If confirmed, proceed with the log out (you can redirect to log out action)
            Swal.fire(
                'Logged Out!',
                'You have been logged out successfully.',
                'success'
            ).then(function () {
                window.location.href = "Account/Index"; // Redirect to log out action in your Account controller
            });
        }
    });
}

