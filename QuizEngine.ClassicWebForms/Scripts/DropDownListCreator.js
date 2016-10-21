function DropDownLists() {
    var selectableCategories = [];
    var _dropDownList;
    var _serviceURL;

    // initializes the drop down inteface item
    init = function (dropDownList, serviceURL) {
        _dropDownList = $("." + dropDownList);
        _serviceURL = serviceURL;
        PopulateControls(serviceURL);
    }

    // poopulates the inteface item with infromation from the database
    function PopulateControls(serviceUrl) {
        $.ajax({
            type: "POST",
            url: serviceUrl,
            data: '{}',
            contentType: "application/json; charset=utf-8",
            dataType: 'json',
            success: function (data) {
                var jsonostr = data.d;
                var jsonobj = eval('(' + jsonostr + ')');
                for (i in jsonobj) {
                    _dropDownList.append("<option value='" + jsonobj[i]["Key"] + "' >" + jsonobj[i]["Value"] + "</option>");
                }
            }
        });
    }
    values = function () {
        return $(_dropDownList).find($("option:selected")).val();
    }

    texts = function () {
        return $(_dropDownList).find($("option:selected")).text();
    }

    return {
        Init: init,
        Values: values,
        Texts: texts
    }
}