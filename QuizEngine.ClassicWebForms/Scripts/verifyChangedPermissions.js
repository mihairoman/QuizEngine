
var isAuthorizationDirty = false;
var isUserDirty = false;

$(document).ready(function () {
    $('.userpermissions').on('change', '[type=checkbox]', function (e) {
        isAuthorizationDirty = true;
    });

    $("#ddlUserType").on('change', function () {
        isUserDirty = true;
    });

    $("#txtUserName").on('change', function () {
        isUserDirty = true;
    });
});



function UserDataChanged() {

    var permissions_changed = document.createElement("INPUT");

    permissions_changed.type = "hidden";
    permissions_changed.name = "permissions_changed";

    if (isAuthorizationDirty === true) {
        permissions_changed.value = true;
    } else {
        permissions_changed.value = false;
    }

    document.forms[0].appendChild(permissions_changed);

    var user_changed = document.createElement("INPUT");

    user_changed.type = "hidden";
    user_changed.name = "user_changed";

    if (isUserDirty === true) {
        user_changed.value = true;
    } else {
        user_changed.value = false;
    }

    document.forms[0].appendChild(user_changed);
}

    


    



