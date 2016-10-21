function SwapperControl() {

    var _leftListBox;
    var _sortLeftListBox;
    var _rightListBox;
    var _sortRightListBox;
    var _addButton;
    var _removeButton;
    var _addAllButton;
    var _removeAllButton;
    var _selectedItems = [];
    var _unselectedItems = []

    // initializes the interface items
    init = function (swapperContainer, serviceUrl, serviceMethod, width) {

        if (width == undefined || width == null) {
            // appends the design, default width should be 120
            $("." + swapperContainer).append('<select class="unselectable" multiple="multiple" style="width: 120px; height: 120px;"> </select> <div style="display: inline-block;">     <input type="button" class="add" style="width: 50px;" value=">" />    <br />    <input type="button" class="remove" style="width: 50px;" value="<" /><br />    <input type="button" class="addall" style="width: 50px;" value=">>" /><br />    <input type="button" class="removeall" style="width: 50px;" value="<<" /><br /> </div> <select class="selectable" multiple="multiple" style="width: 120px; height: 120px;">');
        } else {
            $("." + swapperContainer).append('<select class="unselectable" multiple="multiple" style="width: ' + width + 'px; height: 120px;"> </select> <div style="display: inline-block;">     <input type="button" class="add" style="width: 50px;" value=">" />    <br />    <input type="button" class="remove" style="width: 50px;" value="<" /><br />    <input type="button" class="addall" style="width: 50px;" value=">>" /><br />    <input type="button" class="removeall" style="width: 50px;" value="<<" /><br /> </div> <select class="selectable" multiple="multiple" style="width: ' + width + 'px; height: 120px;">');
        }

        _leftListBox = $("." + swapperContainer + " .unselectable");
        _sortLeftListBox = "." + swapperContainer + " .unselectable";

        _rightListBox = $("." + swapperContainer + " .selectable");
        _sortRightListBox = "." + swapperContainer + " .selectable"

        _addButton = $("." + swapperContainer + " .add");
        _removeButton = $("." + swapperContainer + " .remove");
        _addAllButton = $("." + swapperContainer + " .addall");
        _removeAllButton = $("." + swapperContainer + " .removeall");
        BindEvents();
        PopulateControls(serviceUrl, serviceMethod);
    }

    // populates the itnerface items with info5rmation from the database
    function PopulateControls(serviceUrl, serviceMethod) {
        var req = new PEService();
        req.ExecuteCall(serviceUrl, serviceMethod, JSON.stringify(), OnSuccess, OnError);
    }
    function OnSuccess(data) {
        _leftListBox.innerHTML = "";
        var jsonostr = JSON.parse(data).d;
        var jsonobj = JSON.parse(jsonostr);

        for (i in jsonobj) {
            _leftListBox.append("<option class='ui-widget-content' value='" + jsonobj[i]["Key"] + "'>" + jsonobj[i]["Value"] + "</option>");
        }
        SortLeftDropdownList();
    }

    function OnError(msg) { }

    function SortLeftDropdownList() {

        $(_sortLeftListBox).html($(_sortLeftListBox + ' option').sort(function (x, y) {
            return $(x).text() < $(y).text() ? -1 : 1;
        }))
        $(_sortLeftListBox).get(0).selectedIndex = 0;
    }

    function SortRighttDropdownList() {

        $(_sortRightListBox).html($(_sortRightListBox + ' option').sort(function (x, y) {
            return $(x).text() < $(y).text() ? -1 : 1;
        }))
        $(_sortRightListBox).get(0).selectedIndex = 0;
    }



    // defines all the events on the swapper itnerface item
    function BindEvents() {
        var unselectedItem;
        var selectedItem;
        _leftListBox.on("dblclick", "option", function () {
            _rightListBox.append("<option class='ui-widget-content' value='" + $(this).val() + "'>" + $(this).text() + "</option>");
            $(this).remove();
            SortRighttDropdownList();
        });

        _rightListBox.on("dblclick", "option", function () {
            _leftListBox.append("<option class='ui-widget-content' value='" + $(this).val() + "'>" + $(this).text() + "</option>");
            $(this).remove();
            SortLeftDropdownList();
        });

        _addButton.on("click", function () {
            var leftListBoxArr = _leftListBox.find("option:selected");
            for (var i = 0; i < leftListBoxArr.length ; i++) {
                _rightListBox.append("<option class='ui-widget-content' value='" + leftListBoxArr[i].value + "'>" + leftListBoxArr[i].text + "</option>");
                leftListBoxArr[i].remove();
            }
            SortRighttDropdownList();
        });

        _removeButton.on("click", function () {
            var rightListBoxArr = _rightListBox.find("option:selected");

            for (var i = 0; i < rightListBoxArr.length ; i++) {
                _leftListBox.append("<option class='ui-widget-content' value='" + rightListBoxArr[i].value + "'>" + rightListBoxArr[i].text + "</option>");
                rightListBoxArr[i].remove();
            }
            SortLeftDropdownList();
        });

        _addAllButton.on("click", function () {
            _leftListBox.find("option").each(function () {
                _rightListBox.append("<option class='ui-widget-content' value='" + $(this).val() + "'>" + $(this).text() + "</option>");
                $(this).remove();
            });
            SortRighttDropdownList();
        });
        _removeAllButton.on("click", function () {
            _rightListBox.find("option").each(function () {
                _leftListBox.append("<option class='ui-widget-content' value='" + $(this).val() + "'>" + $(this).text() + "</option>");
                $(this).remove();
            });
            SortLeftDropdownList();
        });

    }


    values = function () {
        var selectedOptions = [];
        if (_rightListBox != undefined) {
            _rightListBox.find("option").each(function () {
                var option = { OptionGUID: $(this).val() };
                selectedOptions.push(option);
            });
        }
        return selectedOptions;
    }

    texts = function () {
        var selectedOptions = [];
        if (_rightListBox != undefined) {
            _rightListBox.find("option").each(function () {
                var option = { OptionName: $(this).text() };
                selectedOptions.push(option);
            });
        }
        return selectedOptions;
    }

    reset = function () {
        _rightListBox.find("option").each(function () {
            _leftListBox.append("<option class='ui-widget-content' value='" + $(this).val() + "'>" + $(this).text() + "</option>");
            $(this).remove();
        });
    }

    return {
        Init: init,
        Values: values,
        Texts: texts,
        Reset: reset
    }

}



