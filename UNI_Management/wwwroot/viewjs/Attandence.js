$(document).ready(function () {
    var i, currentYear, startYear, endYear, newOption, dropdownYear;
    dropdownYear = document.getElementById("dropdownYear");
    currentYear = (new Date()).getFullYear();
    var currentMonth = (new Date()).getMonth();
    startYear = 2021;
    endYear = currentYear;
    // year dropdown
    for (i = startYear; i <= endYear; i++) {
        newOption = document.createElement("option");
        newOption.value = i;
        newOption.label = i;
        if (i == currentYear) {
            newOption.selected = true;
        }
        dropdownYear.appendChild(newOption);
    }
    // arguments month and year wise
    $('#month, #dropdownYear').change(function () {
        let year = $('#dropdownYear').val();
        let month = $('#month').val();
        generateCalendar(year, month);
    });

    $('#month').val(currentMonth);



    function generateCalendar(year, month) {

        let date = new Date(year, month);
        let dayone = new Date(year, month, 1).getDay();
        //  let lastdate = new Date(year, month, 0).getDate();
        //  let parsedMonth = parseInt(month, 10);
        let lastdate = new Date(year, parseInt(month) + 1, 0).getDate();
        let dayend = new Date(year, month, lastdate).getDay();
        let dayNames = ['S', 'M', 'T', 'W', 'T', 'F', 'S'];
        // print header Month- Year
        let monthNames = ['January', 'February', 'March', 'April', 'May', 'June', 'July', 'August', 'September', 'October', 'November', 'December'];
        $("#currentmonthyear").html(`<th colspan="7">${monthNames[month]}-${year}</th>`);
        let lit = "";
        // print date and day
        for (let i = 1; i <= lastdate; i++) {
            let dayIndex = (dayone + i - 1) % 7;
            lit += `<td class="border border-dark text-center w-10"><a value="${i}">${i}<br>${dayNames[dayIndex]}</a></td>`;
        }
        $("#rowofmonths").html(lit);


        $.ajax({
            url: '/Attandance/GetAttandaceForMonth',
            type: 'GET',
            data: {
                month: month,
                year: year,
            },
            success: function (response) {
                console.log(response);

                let lit2 = "";

                for (let i = 1; i <= lastdate; i++) {

                    let attendance = response.find(a => a.day === i)
                    console.log(attendance);

                    if (attendance) {
                        if (attendance.status === 1) {
                            statusText = 'P';
                        } else if (attendance.status === 2) {
                            statusText = 'A';
                        } else if (attendance.status === 3) {
                            statusText = 'HL';
                        }
                    } else {
                        statusText = ' '; // Optionally, handle days with no data
                    }

                    lit2 += `<td class="border border-dark text-center atte">${statusText}</td>`;
                }

                $("#additionalRow").html(lit2);
            },
            error: function (error) {
                console.error('Error:', error);
            }
        });




        //click on button print date
        $('#attdancetable').on('click', '.atte', function () {
            let index = $(this).index() + 1;
            console.log(' Date : ' + index + '/ ' + (parseInt(month) + 1) + '/ ' + year);
            showDropdown(this, index, parseInt(month) + 1, year);

        });

        $(document).click(function (event) {
            if (!$(event.target).closest('.atte').length) {
                $('.dropdowndata').remove();
            }
        });
    }




    generateCalendar(currentYear, new Date().getMonth());

    function showDropdown(cell, index, month, year, status) {

        // Remove any existing dropdowns
        $('.dropdowndata').remove();
        debugger
        // Create the dropdown menu
        let dropdown = `<div class="dropdown"> <div class="dropdowndata" style="display: none; position: absolute;">
            <a href="#" class="drp" data-value="1">Present</a>
            <a href="#" class="drp" data-value="2">Absent</a>
            <a href="#" class="drp" data-value="3">Half Leave</a>
          </div></div>`;

        // Append the dropdown to the cell
        $(cell).append(dropdown);

        // Show the dropdown
        $(cell).find('.dropdowndata').show();

        // Handle dropdown item click
        $('.drp').click(function (event) {
            event.preventDefault();
            let status = $(this).text();
            let value = $(this).data('value');
            console.log('Status:', status);
            $(this).closest('td').text(status); // Optionally, replace cell content with status
            $('.dropdowndata').remove(); // Remove the dropdown


            $.ajax({
                url: '/Attandance/SaveAttendance',
                type: 'GET',
                data: {
                    day: index,
                    month: month,
                    year: year,
                    status: value
                },
                success: function (response) {
                    console.log(data);

                    $('.dropdowndata').remove(); // Remove the dropdown
                },
                error: function (error) {
                    console.error('Error:', error);
                }
            });
        });
    }

});