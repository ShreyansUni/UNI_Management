/*const { debug } = require("util");*/
function GridPagination(gridName, pageNo, urlCallBackMethodName, columnName) {
    $("#" + gridName + "PageNo").val(pageNo);
    var sortDirection = $("#" + gridName + "SortDirection").val();
    var previousSort = $("#" + gridName + "CurrentSort").val();
    if (!IsNullOrEmptyString(columnName)) {
        if (columnName == previousSort) {
            if (sortDirection == "asc") {
                $("#" + gridName + "SortDirection").val('desc');

            }
            else {
                $("#" + gridName + "SortDirection").val('asc');
            }
        }
        else {
            $("#" + gridName + "SortDirection").val('asc');
            sortDirection = 'asc';
        }
    } else {
        columnName = previousSort;
        sortDirection = 'desc';
    }
    var obj = $(".gridFilterElements");
    var result = [];
    console.log(obj);
    if (obj.length > 0) {
        for (var i = 0; i < obj.length; i++) {
            var objKeyValue = {};
            var ele = obj[i];
            objKeyValue.Text = ele.id;
            if (ele.value == 'on') {
                objKeyValue.Value = ele.checked ? "true" : "false";
            } else {
                objKeyValue.Value = ele.value;
            }
            result.push(objKeyValue);
        }
    }
    console.log(result);
    var pageIndex = IsValueUndefinedOrNull($("#" + gridName + "PageNo").val()) ? jsConfigDefaultPageNo : $("#" + gridName + "PageNo").val();
    var pageSize = IsValueUndefinedOrNull($("#" + gridName + "PageSize").val()) ? jsConfigDefaultPageSize : $("#" + gridName + "PageSize").val();
    fnShowMainProgress();
    $.ajax({
        type: 'GET',
        url: urlCallBackMethodName,
        data: { pageIndex: pageIndex, pageSize: pageSize, filterObj: JSON.stringify(result), columnName: columnName, sortDirection: sortDirection },
        async: true,
        contenttype: "application/json; charset=utf-8",
        datatype: 'json',
        success: function (data) {
            fnHideMainProgress();
            $("#" + gridName).html(data);
            $("#" + gridName + "PageSize").val(pageSize);
            $("#" + gridName + "SortDirection").val(sortDirection);
            $("#" + gridName + "CurrentSort").val(columnName);
            var ele = $('#' + columnName);
            if (ele != null) {
                var upperArrow = '<i id="up-caret" class="fa text-dark fa-caret-up top-up"></i>';
                var downArrow = '<i id="down-caret" class="fa text-dark fa-caret-down "></i>';
                if (sortDirection == "asc") {
                    console.log($('#' + gridName + ' #' + columnName + ' .fa-caret-up'));
                    $('#' + gridName + ' #' + columnName + ' .fa-caret-up').replaceWith(upperArrow);
                }
                else {
                    $('#' + gridName + ' #' + columnName + ' .fa-caret-down').replaceWith(downArrow);
                }
            }
        },
        error: function (req, status, error) {
            fnHideMainProgress();
            fnShowError(error);
        }
    });
}

function ToggleSortDirectionOfGridColumn(gridName) {
    if (!IsUndefinedOrNull($("#" + gridName + "SortDirection"))) {
        if ($("#" + gridName + "SortDirection").val().toLowerCase() == 'asc') {
            $("#" + gridName + "SortDirection").val('desc');
        }
        else {
            $("#" + gridName + "SortDirection").val('asc');
        }
    }
}
//function Clearfilter() {
//    debugger
//    var obj = $(".gridFilterElements");
//    if (obj.length > 0) {
//        for (var i = 0; i < obj.length; i++) {
//            var objKeyValue = {};
//            var ele = obj[i];
//            objKeyValue.Text = ele.id;
//            ele.value = '';
//        }
//    }
//}
function Clearfilter() {
    debugger
    var obj = $(".gridFilterElements");
    if (obj.length > 0) {
        for (var i = 0; i < obj.length; i++) {
            var ele = obj[i];

            if ($(ele).hasClass('filterstatusname')) {
                continue; // Skip this element
            }

            // Check if the element is a Select2 dropdown
            if ($(ele).hasClass("select2-example")) {
                $(ele).val(null).trigger('change'); // Clear Select2 dropdown
            } else {
                ele.value = ''; // Clear other input elements
            }
        }
    }
}

function updatePaginationControls(totalPages, currentPage) {
    var pagination = $('.pagination');
    pagination.empty();

    if (totalPages > 1) {
        var prevDisabled = currentPage <= 1 ? 'disabled' : '';
        var nextDisabled = currentPage >= totalPages ? 'disabled' : '';

        pagination.append(`<li class="page-item ${prevDisabled}"><a class="page-link" href="#" onclick="GridPagination('yourGridName', ${currentPage - 1}, 'urlCallBackMethodName')">Previous</a></li>`);

        for (var i = 1; i <= totalPages; i++) {
            pagination.append(`<li class="page-item ${i === currentPage ? 'active' : ''}"><a class="page-link" href="#" onclick="GridPagination('yourGridName', ${i}, 'urlCallBackMethodName')">${i}</a></li>`);
        }

        pagination.append(`<li class="page-item ${nextDisabled}"><a class="page-link" href="#" onclick="GridPagination('yourGridName', ${currentPage + 1}, 'urlCallBackMethodName')">Next</a></li>`);
    }
}

