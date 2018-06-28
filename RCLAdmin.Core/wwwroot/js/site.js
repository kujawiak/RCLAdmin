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

$("#EventType").change(function (v) {
    var ev = v.target.value;
    var pafg = $("#pafg");
    console.log(v.target.value);
    if (ev === 100) {
        pafg.hide();
    } else {
        pafg.show();
    }
});