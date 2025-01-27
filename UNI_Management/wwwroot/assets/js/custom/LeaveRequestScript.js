function calculateLeaveDuration() {
    debugger;
    const startDateInput = document.getElementById('startDate');
    const endDateInput = document.getElementById('endDate');
    const totalDaysOutput = document.getElementById('totalDays');
    const workingDaysOutput = document.getElementById('workingDays');

    const startDate = new Date(startDateInput.value);
    const endDate = new Date(endDateInput.value);

    if (isNaN(startDate) || isNaN(endDate) || startDate > endDate) {
        totalDaysOutput.textContent = 'Invalid date range';
        workingDaysOutput.textContent = 'Invalid date range';
        return;
    }

    // Calculate total days (inclusive)
    const totalDays = Math.floor((endDate - startDate) / (1000 * 60 * 60 * 24)) + 1;

    // Calculate working days (assuming Sunday is off)
    let workingDays = 0;
    let currentDate = new Date(startDate);

    while (currentDate <= endDate) {
        if (currentDate.getDay() !== 0) { // 0 is Sunday
            workingDays++;
        }
        currentDate.setDate(currentDate.getDate() + 1);
    }

    totalDaysOutput.textContent = totalDays;
    workingDaysOutput.textContent = workingDays;
}
