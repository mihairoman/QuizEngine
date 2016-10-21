<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Pages/Site.Master"  Inherits="QuizEngine.App.Pages.EditUser" Culture="auto" meta:resourcekey="Page" UICulture="auto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="server">
    <div>
        <script src="../Scripts/ValidateEditUser.js"></script>

        <div id="errors">
            <ul id="errorList">
            </ul>
        </div>

        <div style="padding-left: 90px; padding-top: 20px; font-size: 20px;">User Details</div>
        <br /> <br />

        * Please make the button as checked if you want to insert a Domain name for your username. Otherwise let the button as unchecked.
        <input type="checkbox" id="LoginMode" />
        <br /> <br />

        Username: 
        <input type="text" id="UserName" rows="2" cols="60" />
        <br />
        User Type:
        <select id="UserType" name="D1">
        </select><br />
        <label id="lblIsActive">Is Active:</label>
        <input type="checkbox" id="IsActive" />

        <br />
        <br />

        <div style="padding-left: 90px; padding-top: 20px; font-size: 20px;">Manage Permissions</div>

        <br />
        <br />

        <table id="UserPermissions"></table>


        <br />

        <input id="insertUser" type="submit" />



        <script type="text/javascript">
            $(document).ready(function () {

                var PageMode = '<%=PageMode%>';
                var userGuid = '<%=UserId%>';

                //create the table that contains all the existing permissions
                var tableUserPermissions = document.getElementById("UserPermissions");
                var row;
                var cellName;
                var cellValue;

                //verify if at least one permission has been changed
                var _isAuthorizationDirty = false;

                //save the list of permissions for a specific user
                var _allCheckedPermissions = "";

                //populate the fileds with old data in case that PageMode = Update
                var prepareForPopulate = {
                    userID: JSON.stringify(userGuid),
                }

                //if PageMode = Insert, change the button value and hide IsActive lnl and btn
                if (PageMode == '1') {
                    document.getElementById("lblIsActive").style.visibility = 'hidden';
                    document.getElementById("IsActive").style.visibility = 'hidden';
                    document.getElementById("insertUser").value = "Insert";
                }

                //if PageMode = Update, change the button value and populate the UserName, UserType and IsActive with old data
                if (PageMode == '2') {

                    document.getElementById("insertUser").value = "Update";

                    $.ajax({
                        type: "POST",
                        url: "../Webservices/UserService.asmx/GetUserByID",
                        data: JSON.stringify(prepareForPopulate),
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (response) {
                            var jsonostr = response.d;
                            var jsonobj = eval('(' + jsonostr + ')');

                            document.getElementById("UserName").value = jsonobj.UserName
                            document.getElementById("UserType").selectedIndex = jsonobj.UserType
                            document.getElementById("IsActive").checked = jsonobj.IsActive;

                            if ((jsonobj.UserName).indexOf('@') != -1) {
                                document.getElementById("LoginMode").checked = false;
                            }
                            else if ((jsonobj.UserName).indexOf('\\') != -1) {
                                document.getElementById("LoginMode").checked = true;
                            }

                        }
                    });
                }

                //populate the dropdown list with all the possible types of user
                $.ajax({
                    type: "POST",
                    url: "../Webservices/UserService.asmx/GetAllUserTypes",
                    data: JSON.stringify(),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (response) {
                        var jsonostr = response.d;
                        var jsonobj = eval(jsonostr);

                        for (i in jsonobj) {
                            var realObj = eval(jsonobj[i]);

                            var el = document.createElement("option");
                            el.textContent = realObj.UserType;
                            el.value = realObj.UserValue;
                            document.getElementById('UserType').appendChild(el);
                        }
                    }
                });

                //generate dynamically the permissions checkbox 
                //and activate those that appear in permissions list of the current user
                $.ajax({
                    type: "POST",
                    url: "../Webservices/UserService.asmx/GetPermissionList",
                    data: JSON.stringify(),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (response) {
                        var jsonostr = response.d;
                        var jsonobj = eval(jsonostr);

                        $.ajax({
                            type: "POST",
                            url: "../Webservices/UserService.asmx/GetUserPermissions",
                            data: JSON.stringify(prepareForPopulate),
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            success: function (responsePermissions) {
                                var jsonostrPerm = responsePermissions.d;
                                var jsonobjPerm = eval('(' + jsonostrPerm + ')');

                                for (i in jsonobj) {
                                    var realObj = eval(jsonobj[i]);

                                    row = tableUserPermissions.insertRow(i);

                                    cellName = row.insertCell(0);
                                    cellName.innerHTML = realObj.PermissionName;

                                    var chkbox = document.createElement('input');
                                    chkbox.type = "checkbox";
                                    chkbox.id = realObj.PermissionValue;
                                    chkbox.name = realObj.PermissionName;

                                    for (j in jsonobjPerm) {
                                        if (realObj.PermissionValue == jsonobjPerm[j]["PermissionGuid"]) {
                                            chkbox.checked = true;
                                        }

                                    }

                                    cellValue = row.insertCell(1);
                                    cellValue.appendChild(chkbox);
                                }
                            }

                        });
                    }
                });


                //verify if at least one permission has been changed
                $('#UserPermissions').on('change', '[type=checkbox]', function (e) {
                    _isAuthorizationDirty = true;
                });


                //if the Insert/Update button has been pressed insert or update a user
                $('#insertUser').on('click', function (e) {
                    e.preventDefault();
                    e.stopPropagation();

                    //if at least one permission has been changed, get all the checked permissions
                    if (_isAuthorizationDirty === true) {
                        _allCheckedPermissions = $('input[type=checkbox]:checked').map(function () {
                            if ((this.id != "IsActive") && (this.id != "LoginMode")) {
                                return this.id;
                            }
                        }).get().join(',');
                    }

                    //get the new data from the rest of the fields
                    var _selectedUserType = document.getElementById("UserType").value;
                    var _filledUsername = document.getElementById("UserName").value;

                    var valid = new ValidateUser(_selectedUserType, _filledUsername);

                    var selectedUser = {
                        UserGUID: userGuid,
                        UserName: _filledUsername,
                        UserType: _selectedUserType,
                    }

                    var selectedUserForEdit = {
                        UserGUID: userGuid,
                        UserName: _filledUsername,
                        UserType: _selectedUserType,
                        IsActive: document.getElementById("IsActive").checked
                    }

                    if (valid.isValid) {
                        var prepareForSent = {
                            allCheckedPermissions: JSON.stringify(_allCheckedPermissions),
                            user: JSON.stringify(selectedUser)
                        }

                        var prepareForUpdate = {
                            allCheckedPermissions: JSON.stringify(_allCheckedPermissions),
                            user: JSON.stringify(selectedUserForEdit)
                        }


                        //PageMode = Insert
                        if (PageMode == "1") {
                            $.ajax({
                                type: "POST",
                                url: "../Webservices/UserService.asmx/AddUser",
                                data: JSON.stringify(prepareForSent),
                                contentType: "application/json; charset=utf-8",
                                dataType: "json",
                                //  success: function () { window.location.href = "ManageUsers.aspx"; },
                                success: function (response) {

                                    if (response.d === true) {
                                        var validUserExists = VerifyUserExists();
                                        $(".errorListItem").each(function () { $(this).remove(); });
                                        $("#errorControl").tmpl(validUserExists.Errors).appendTo("#errorList");
                                    }
                                    else {
                                        window.location.href = "ManageUsers.aspx";
                                    }
                                },

                                failure: function (response) {
                                    alert(response.d);
                                }
                            });
                        }

                        //PageMode = Update
                        else if (PageMode == "2") {
                            $.ajax({
                                type: "POST",
                                url: "../Webservices/UserService.asmx/EditUser",
                                data: JSON.stringify(prepareForUpdate),
                                contentType: "application/json; charset=utf-8",
                                dataType: "json",
                                success: function () { window.location.href = "ManageUsers.aspx"; },
                                failure: function (response) {
                                    alert(response.d);
                                }
                            });

                        }
                    }

                    // if some fields are not valid, a specific error message will appear
                    else {
                        $(".errorListItem").each(function () { $(this).remove(); });
                        $("#errorControl").tmpl(valid.Errors).appendTo("#errorList");
                    }
                });
            });
        </script>

        <script id="errorControl" type="text/x-jQuery-tmpl">
            <li class="errorListItem">${error}</li>
        </script>

    </div>
</asp:Content>
