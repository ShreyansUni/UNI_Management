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

//function setStatusAndFilter(status) {
//    document.getElementById("filterIsActive").value = status; // Set the value of filterIsActive
//    GridPagination('employeeList_Grid', 1, '@gridUrl', 'ClientUserID'); // Refresh the grid
//}

//function fnEmployeeEdit(encodeEmployeeId) {
//    if (!encodeEmployeeId) {
//        encodeEmployeeId = 0; // Handle null or undefined employeeId
//    }
//    window.location.href = '/Employee/EmployeeForm/' + encodeEmployeeId;
//}

//filter button
document.getElementById('filterButton').addEventListener('click', function () {
    var filterCard = document.getElementById('filterCard');
    if (filterCard.style.display === 'none' || filterCard.style.display === '') {
        filterCard.style.display = 'block';
    } else {
        filterCard.style.display = 'none';
    }
});
//filterEmployeeList();
////IsActive filtering
//function setStatusAndFilter(status) {
//    document.getElementById('filterIsActive').value = status;
//    filterEmployeeList();
//}
////filtered data ajax
//function filterEmployeeList() {
//    $('#loader-list').show();
//    $.ajax({
//        type: 'GET',
//        url: '/Employee/GetEmployeeList',
//        data: $("#employeeformfilter").serializeArray(),
//        success: function (response) {
//            $('#loader-list').hide();
//            $('#EmployeeListPartialView').html(response);
//        },
//        error: function (req, status, err) {
//            // Handle error during AJAX call

//        }
//    });
//}

////filter clear button js
//function clearFilters() {
//    $('#employeeformfilter')[0].reset();
//    // document.getElementById("clientformfilter").reset();
//    $.ajax({
//        type: 'GET',
//        url: '@Url.Action("GetEmployeeList", "Employee")',
//        success: function (data) {
//            // Update the table body with the returned partial view
//            $('#EmployeeListPartialView').html(data);
//        },
//        error: function (req, status, err) {
//            console.error("Error: ", err);
//            alert("An error occurred while fetching the employee list.");
//        }
//    });
//}

