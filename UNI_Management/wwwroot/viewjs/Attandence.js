﻿//$(document).ready(function () {
//    var i, currentYear, startYear, endYear, newOption, dropdownYear;
//    dropdownYear = document.getElementById("dropdownYear");
//    currentYear = (new Date()).getFullYear();
//    var currentMonth = (new Date()).getMonth();
//    startYear = 2021;
//    endYear = currentYear;

//    // Year dropdown
//    for (i = startYear; i <= endYear; i++) {
//        newOption = document.createElement("option");
//        newOption.value = i;
//        newOption.label = i;
//        if (i == currentYear) {
//            newOption.selected = true;
//        }
//        dropdownYear.appendChild(newOption);
//    }

//    // Change month or year
//    $('#month, #dropdownYear').change(function () {
//        let year = $('#dropdownYear').val();
//        let month = $('#month').val();
//        generateCalendar(year, month);
//    });

//    $('#month').val(currentMonth);

//    // Generate Calendar
//    function generateCalendar(year, month) {
//        let date = new Date(year, month);
//        let dayone = new Date(year, month, 1).getDay();
//        let lastdate = new Date(year, parseInt(month) + 1, 0).getDate();
//        let dayNames = ['S', 'M', 'T', 'W', 'T', 'F', 'S'];

//        // Print header Month-Year
//        let monthNames = ['January', 'February', 'March', 'April', 'May', 'June', 'July', 'August', 'September', 'October', 'November', 'December'];
//        $("#currentmonthyear").html(`<th colspan="7">${monthNames[month]}-${year}</th>`);

//        let lit = "";
//        // Print date and day
//        for (let i = 1; i <= lastdate; i++) {
//            let dayIndex = (dayone + i - 1) % 7;
//            let cellClass = "border border-dark text-center w-10";

//            // Get current date and compare
//            let currentDate = new Date();
//            let targetDate = new Date(year, month, i);

//            // If the target date is in the past, disable interaction
//            if (targetDate < currentDate) {
//                cellClass += " disabled"; // Add a "disabled" class to style or disable
//            }

//            lit += `<td class="${cellClass}" data-day="${i}"><a value="${i}">${i}<br>${dayNames[dayIndex]}</a></td>`;
//        }
//        $("#rowofmonths").html(lit);

//        // Get attendance data via AJAX
//        $.ajax({
//            url: '/Attandance/GetAttandaceForMonth',
//            type: 'GET',
//            data: {
//                month: month,
//                year: year,
//            },
//            success: function (response) {
//                let lit2 = "";

//                for (let i = 1; i <= lastdate; i++) {
//                    let attendance = response.find(a => a.day === i);
//                    let statusText = ' ';

//                    if (attendance) {
//                        if (attendance.status === 1) {
//                            statusText = 'P';
//                        } else if (attendance.status === 2) {
//                            statusText = 'A';
//                        } else if (attendance.status === 3) {
//                            statusText = 'HL';
//                        }
//                    }

//                    // If attendance is already filled, mark it in data attribute
//                    let dataFilled = attendance ? 'data-attendance-filled="true"' : '';

//                    lit2 += `<td class="border border-dark text-center atte" ${dataFilled}>${statusText}</td>`;
//                }
//                $("#additionalRow").html(lit2);
//            },
//            error: function (error) {
//                console.error('Error:', error);
//            }
//        });

//        // Click on button to print date
//        $('#attdancetable').on('click', '.atte', function () {
//            if ($(this).attr('data-attendance-filled')) {
//                return; // Do not open dropdown if attendance is already filled
//            }

//            let index = $(this).index() + 1;
//            let selectedDate = new Date(year, month, index);
//            let currentDate = new Date();
//            let selectedDateString = selectedDate.toDateString(); // This will give you the date part in a human-readable format
//            let currentDateString = currentDate.toDateString();

//            if (selectedDateString >= currentDateString) {  // Allow editing of the current and future dates
//                showDropdown(this, index, month, year);
//            }
//        });
//    }

//    generateCalendar(currentYear, new Date().getMonth());

//    // Show dropdown with dynamic options based on date
//    function showDropdown(cell, index, month, year) {
//        // Remove any existing dropdowns
//        $('.dropdowndata').remove();

//        if ($(cell).attr('data-attendance-filled')) {
//            return; // Do not open dropdown if attendance is already filled
//        }

//        // Get the current date and compare it with the selected date
//        let currentDate = new Date();
//        let targetDate = new Date(year, month, index);

//        // Determine dropdown options based on the date (past, present, or future)
//        let dropdownOptions = '';

//        // Check if the selected date is today
//        if (targetDate.toDateString() === currentDate.toDateString()) {
//            // Today's date: Show all options (Present, Absent, Half Leave)
//            dropdownOptions = `
//                <a href="#" class="drp" data-value="1">Present</a>
//                <a href="#" class="drp" data-value="2">Absent</a>
//                <a href="#" class="drp" data-value="3">Half Leave</a>
//            `;
//        } else {
//            // Past dates: Do not show dropdown (disabled)
//            return; // Simply return to prevent showing the dropdown for past dates
//        }

//        // Create the dropdown menu
//        let dropdown = `
//            <div class="dropdown">
//                <div class="dropdowndata" style="display: none; position: absolute;">
//                    ${dropdownOptions}
//                </div>
//            </div>
//        `;

//        // Append the dropdown to the cell
//        $(cell).append(dropdown);

//        // Show the dropdown
//        $(cell).find('.dropdowndata').show();

//        // Handle dropdown item click
//        $('.drp').click(function (event) {
//            event.preventDefault();
//            let status = $(this).text();
//            let value = $(this).data('value');
//            console.log('Status:', status);
//            $(cell).text(status); // Replace cell content with status
//            $(cell).attr('data-attendance-filled', true); // Mark cell as filled
//            $('.dropdowndata').remove(); // Remove the dropdown

//            // Save attendance status via AJAX
//            $.ajax({
//                url: '/Attandance/SaveAttendance',
//                type: 'GET',
//                data: {
//                    day: index,
//                    month: month,
//                    year: year,
//                    status: value
//                },
//                success: function (response) {
//                    console.log(response);
//                    $(cell).attr('data-attendance-filled', true); // Ensure it is marked filled
//                    $('.dropdowndata').remove(); // Remove the dropdown
//                },
//                error: function (error) {
//                    console.error('Error:', error);
//                }
//            });
//        });
//    }
//});
$(document).ready(function () {
    var currentYear = new Date().getFullYear();
    var currentMonth = new Date().getMonth();
    var currentDay = new Date().getDate();
    var selectedEmployeeId = $("#employeeDropdown").val() || "default";

    populateYearDropdown();
    $("#month").val(currentMonth);

    // Event listeners for dropdown changes
    $('#month, #dropdownYear, #employeeDropdown').change(function () {
        generateCalendar($('#dropdownYear').val(), $('#month').val(), $('#employeeDropdown').val() || "default");
    });

    function populateYearDropdown() {
        var startYear = 2021;
        var dropdownYear = $("#dropdownYear");
        dropdownYear.empty();

        for (var i = startYear; i <= currentYear; i++) {
            dropdownYear.append(new Option(i, i, i === currentYear, i === currentYear));
        }
    }

    function generateCalendar(year, month, employeeId) {
        let lastDate = new Date(year, parseInt(month) + 1, 0).getDate();
        let firstDay = new Date(year, month, 1).getDay();
        let monthNames = ['January', 'February', 'March', 'April', 'May', 'June', 'July', 'August', 'September', 'October', 'November', 'December'];
        let dayNames = ['S', 'M', 'T', 'W', 'T', 'F', 'S'];

        $("#currentmonthyear").html(`<th colspan="7">${monthNames[month]}-${year}</th>`);

        let rowContent = "";
        for (let i = 1; i <= lastDate; i++) {
            let dayIndex = (firstDay + i - 1) % 7;
            let cellClass = "border border-dark text-center w-10";
            let targetDate = new Date(year, month, i);
            let today = new Date();
            today.setHours(0, 0, 0, 0); // Reset time for accurate comparison
            targetDate.setHours(0, 0, 0, 0);

            if (targetDate < today) cellClass += " disabled"; // Disable past dates

            rowContent += `<td class="${cellClass}" data-day="${i}"><a>${i}<br>${dayNames[dayIndex]}</a></td>`;
        }
        $("#rowofmonths").html(rowContent);

        $.ajax({
            url: '/Attandance/GetAttandaceForMonth',
            type: 'GET',
            data: { month, year, employeeId },
            success: function (response) {
                let attendanceRow = "";
                for (let i = 1; i <= lastDate; i++) {
                    let attendance = response.find(a => a.day === i);
                    let statusText = attendance ? getStatusText(attendance.status) : " ";
                    let dataFilled = attendance ? 'data-attendance-filled="true"' : '';
                    attendanceRow += `<td class="border border-dark text-center atte" data-day="${i}" ${dataFilled}>${statusText}</td>`;
                }
                $("#additionalRow").html(attendanceRow);
            },
            error: function (error) {
                console.error('Error:', error);
            }
        });
    }

    function getStatusText(status) {
        return status === 1 ? 'P' : status === 2 ? 'A' : status === 3 ? 'HL' : ' ';
    }

    $('#attdancetable').on('click', '.atte', function () {
        if ($(this).attr('data-attendance-filled')) return; // Prevent editing once filled

        let index = $(this).data('day');
        let year = $('#dropdownYear').val();
        let month = $('#month').val();
        let employeeId = $('#employeeDropdown').val() || "default";
        let targetDate = new Date(year, month, index);
        let today = new Date();
        today.setHours(0, 0, 0, 0);
        targetDate.setHours(0, 0, 0, 0);

        if (targetDate.getTime() === today.getTime()) {
            showDropdown(this, index, month, year, employeeId);
        }
    });

    function showDropdown(cell, index, month, year, employeeId) {
        $('.dropdowndata').remove();
        let dropdown = `
            <div class="dropdown">
                <div class="dropdowndata" style="position: absolute;">
                    <a href="#" class="drp" data-value="1">Present</a>
                    <a href="#" class="drp" data-value="2">Absent</a>
                    <a href="#" class="drp" data-value="3">Half Leave</a>
                </div>
            </div>`;
        $(cell).append(dropdown);
        $(cell).find('.dropdowndata').show();

        $('.drp').click(function (event) {
            event.preventDefault();
            let value = $(this).data('value');
            let status = getStatusText(value);
            $(cell).text(status).attr('data-attendance-filled', true);
            $('.dropdowndata').remove();

            $.ajax({
                url: '/Attandance/SaveAttendance',
                type: 'GET',
                data: { day: index, month, year, status: value, employeeId },
                success: function () {
                    console.log('Attendance saved');
                },
                error: function (error) {
                    console.error('Error:', error);
                }
            });
        });
    }

    generateCalendar(currentYear, currentMonth, selectedEmployeeId);
});



