function fnEmployeeViewModal(encodeEmployeeId) {
    fnShowMainProgress();
    // Fetch data for modal content
    $.ajax({
        type: 'GET',
        url: getEmployeeViewModal,
        data: { encodeEmployeeId: encodeEmployeeId },
        success: function (data) {
            $("#employeeViewModelBody").empty();
            $("#employeeViewModelBody").html(data);
            $('#EmployeeViewModal').modal("show");
        },
        error: function (req, status, err) {
        },
        complete: function () {
            fnHideMainProgress(); // Hide the loader after the request completes (whether success or error)
        }
    });
}

function fnEmployeeDeleteModal(encodeEmployeeId) {
    // Define the URL for deleting the domain
    var deleteUrl = 'getAgencyDeleteModel'; // Replace with your actual delete URL

    // Show the confirmation popup
    confirmAndDeletePopup(function () {
        // If confirmed, proceed with the AJAX deletion
        $.ajax({
            type: 'POST',
            url: getEmployeeDeleteModel,
            data: { encodeEmployeeId: encodeEmployeeId },
            success: function (response) {
                if (response.success) {
                    Swal.fire('Deleted!', 'The Employee has been deleted.', 'success').then(function () {
                        location.reload(); // Reload the page on successful deletion
                    });
                } else {
                    Swal.fire('Error!', 'There was an error deleting the Employee.', 'error');
                }
            },
            error: function () {
                Swal.fire('Error!', 'There was an error processing your request.', 'error');
            }
        });
    });
}

//function fnEmployeeEdit(encodeEmployeeId) {
//    if (!encodeEmployeeId) {
//        encodeEmployeeId = 0; // Handle null or undefined employeeId
//    }
//    window.location.href = '/Employee/EmployeeForm/' + encodeEmployeeId;
//}
