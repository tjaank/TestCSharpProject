function regexReplace(str, reg) {
    var re = new RegExp(reg);
    var m = re.exec(str);
    if (m == null) {
        return '';
    } else {
        var s = '';
        for (i = 0; i < m.length; i++) {
            s = s + m[i];
        }
        return s;
    }
}
function rpItemsDataBind(rpSelector, url) {
    var rp = $(rpSelector).html();
    $(rpSelector).html('<img src="progress-indicator.gif" style="margin-bottom: -3px" /> Talha');
    $.ajax({
        type: "POST",
        url: url,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            $(rpSelector).html('');
            var c = eval(msg.d);
            for (var i in c) {
                var item = rp;
                while (true) {
                    var r = regexReplace(item, '{D:\\w+}')
                    if (r == '') {
                        break;
                    }
                    var ri = r.replace('{D:', '');
                    ri = ri.replace('}', '');
                    item = item.replace(r, c[i][ri]);
                }
                $(rpSelector).append(item);
            }
            $(rpSelector).replaceWith($(rpSelector).html());
        },
        error: function () {
            $(rpSelector).html('Could not load data');
        }
    });
}