// Write your JavaScript code.
function fPL() {
    var filter = $('#printersFilter')[0].value.toUpperCase();
    $('#printersTable tr').each(function (i, v) {
        var me = $(v);
        var td = me.find('td');
        if (td.length > 0) {
            if (td.html().toUpperCase().indexOf(filter) > -1)
                { me.show(); }
            else
                { me.hide(); }
        }
    });
}