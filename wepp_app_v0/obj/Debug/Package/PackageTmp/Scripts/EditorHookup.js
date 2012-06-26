$(document).ready(function () {
    function getDateYymmdd(value) {
        if (value == null)
            return null;
        return $.datepicker.parseDate("yy/mm/dd", value);
    }
    $('.date').each(function () {
        $(this).datepicker({
            dateFormat: "dd/mm/yy",  // hard-coding uk date format, but could embed this as an attribute server-side (based on the current culture)
        });
    });
});