function DropDownLists() {
    var selectableCategories = [];
    var _dropDownList;
    var _serviceURL;

    // initializes the drop down inteface item
    init = function (dropDownList, serviceURL, serviceMethod) {
        _dropDownList = $("." + dropDownList);
        _serviceURL = serviceURL;
        PopulateControls(serviceURL, serviceMethod);
    }

    // poopulates the inteface item with infromation from the database
    function PopulateControls(serviceUrl, serviceMethod) {
        var req = new PEService();
        req.ExecuteCall(serviceUrl, serviceMethod, JSON.stringify(), OnSuccess, OnError);
    }

    function OnSuccess(data) {
        var jsonostr = JSON.parse(data).d;
        var jsonobj = JSON.parse(jsonostr);
        for (i in jsonobj) {
            _dropDownList.append("<option value='" + jsonobj[i]["Key"] + "' >" + jsonobj[i]["Value"] + "</option>");
        }
    }

    function OnError(msg) { }

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