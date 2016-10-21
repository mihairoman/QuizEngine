$(document).ready(function () {
    var _id;
    var _mode;
    var _u;

    _allUsers = [];
    var _filterInfo = [];
    var _sortExpression = 'u.UserName asc';

    // the sort direction specifies if it is ascending or descending
    var _sortDir = 0;
    // old sort text helps when desiring to sort another  column in order to aleays start from ascending order
    var _oldSortText;
    // the actual number of the page
    var _pageNumber = 1;
    // specifies how many information should be on the page, it has a default value 5
    var _rowsPerPage = 5;
    // the old page Number is used in order to rpevent displaying the same content on the page serveral times
    var _oldPageNr = 1;
    // used to return the pager object
    var _pagerObject = new PagerControl();
    // this one is used to get the count of the fitlered users
    var _usersCount;

    // the form of the pager
    var _pagerForm = 'userpager';

    //create the table that contains all the existing permissions
    var tableUserPermissions = document.getElementById("UserPermissions");
    var row;
    var cellName;
    var cellValue;

    //verify if at least one permission has been changed
    var _isAuthorizationDirty = false;

    //save the list of permissions for a specific user
    var _allCheckedPermissions = "";

    PopulateTable();
    PopulateDropdown();
    //BindEvents();

    dialog = $("#UserManagementPopup").dialog({
        width: 650,
        minHeight: 693,
        maxHeight: 693,
        modal: true,
        closeOnEscape: true,
        resizable: false,
        autoOpen: false,
        buttons: [
			{
			    text: UserResources.UserPopupSaveButton,
			    click: function () {
			        var ok = true;
			        $("#errorlist").empty();
			        $("#errorlist").slideUp();
			        $("#txtUsername").removeClass("incorrectfield");
			        $("#ddlUserType").removeClass("incorrectfield");
			        //if at least one permission has been changed, get all the checked permissions
			        if (_isAuthorizationDirty === true) {
			            _allCheckedPermissions = $('input[type=checkbox]:checked').map(function () {
			                if ((this.id != "chkIsActive") && (this.id != "chkLoginMode")) {
			                    return this.id;
			                }
			            }).get().join(',');
			        }

			        //get the new data from the rest of the fields
			        var _selectedUserType = document.getElementById("ddlUserType").value;
			        var _filledUsername = document.getElementById("txtUsername").value;

			        //var valid = new ValidateUser(_selectedUserType, _filledUsername);

			        var valid = true;
			        var errString = [];

			        if (_filledUsername.length < 1) {
			            valid = false;
			            $("#errorlist").append("<li>" + UserResources.UsernameEmptyError + "</li>");
			            $("#txtUsername").addClass("incorrectfield");
			        }

			        if ((document.getElementById("chkLoginMode").checked == true) && (valid == true)) {
			            if (_filledUsername.replace(/[^\\]/g, "").length != 1) {
			                valid = false;
			                $("#errorlist").append("<li>" + UserResources.UsernameNotDomainError + "</li>");
			                $("#txtUsername").addClass("incorrectfield");
			            }
			        }
			        if ((document.getElementById("chkLoginMode").checked == false) && (valid == true)) {
			            var atpos = _filledUsername.indexOf("@");
			            var dotpos = _filledUsername.lastIndexOf(".");
			            if (atpos < 1 || dotpos < atpos + 2 || dotpos + 2 >= _filledUsername.length) {
			                valid = false;
			                $("#errorlist").append("<li>" + UserResources.UsernameNotEmailError + "</li>");
			                $("#txtUsername").addClass("incorrectfield");
			            }
			        }

			        if ($("#ddlUserType option:selected").text() === "None") {
			            valid = false;
			            $("#errorlist").append("<li>" + UserResources.UserTypeNotSelectedError + "</li>");
			            $("#ddlUserType").addClass("incorrectfield");
			        }

			        if (valid) {
			            var selectedUser = {
			                UserGUID: _id,
			                UserName: _filledUsername,
			                UserType: _selectedUserType,
			            }

			            var selectedUserForEdit = {
			                UserGUID: _id,
			                UserName: _filledUsername,
			                UserType: _selectedUserType,
			                IsActive: document.getElementById("chkIsActive").checked
			            }

			            var prepareForSent = {
			                allCheckedPermissions: JSON.stringify(_allCheckedPermissions),
			                user: JSON.stringify(selectedUser)
			            }

			            var prepareForUpdate = {
			                allCheckedPermissions: JSON.stringify(_allCheckedPermissions),
			                user: JSON.stringify(selectedUserForEdit)
			            }

			            if (_mode == "1") {
			                $.ajax({
			                    type: "POST",
			                    url: "../Webservices/UserService.asmx/AddUser",
			                    data: JSON.stringify(prepareForSent),
			                    contentType: "application/json; charset=utf-8",
			                    dataType: "json",
			                    success: function (response) {
			                        if (response.d === true) {
			                            var validUserExists = VerifyUserExists();
			                            if (validUserExists.Errors.length > 0) {
			                                ok = false;
			                                $("#txtUsername").addClass("incorrectfield");
			                                for (var i = 0; i < validUserExists.Errors.length; i++) {
			                                    $("#errorlist").append("<li>" + validUserExists.Errors[i].error + "</li>");
			                                }
			                            }
			                            else {
			                                $(".ui-dialog-content").dialog().dialog("close");
			                                PopulateTable();
			                            }
			                        }
			                    },
			                    failure: function (response) {
			                        ok = false;
			                        $("#errorlist").append("<li>" + UserResources.UserInsertError + "</li>");
			                    }
			                }).done(function () {
			                    if (!ok)
			                        $("#errorlist").slideDown();
			                });
			            }

			            else if (_mode == "2") {
			                $.ajax({
			                    type: "POST",
			                    url: "../Webservices/UserService.asmx/EditUser",
			                    data: JSON.stringify(prepareForUpdate),
			                    contentType: "application/json; charset=utf-8",
			                    dataType: "json",
			                    success: function () {
			                        $(".ui-dialog-content").dialog().dialog("close");
			                        PopulateTable();
			                    },
			                    failure: function (response) {
			                        ok = false;
			                        $("#errorlist").append("<li>" + UserResources.UserEditError + "</li>");
			                    }
			                }).done(function () {
			                    if (!ok)
			                        $("#errorlist").slideDown();
			                });
			            }
			        }
			        if (!valid)
			            $("#errorlist").slideDown();
			    }
			},
			{
			    text: UserResources.UserPopupCancelButton,
			    click: function () {
			        dialog.dialog("close");
			    }
			}
        ]
    });

    //verify if at least one permission has been changed
    $('.permissions').on('change', '[type=checkbox]', function (e) {
        _isAuthorizationDirty = true;
    });

    // this event is for the dropdown lsit that decides how many items should be on the page
    $('.usersonpagedropdown').on('change', function () {
        _oldPageNr = 1;
        _pageNumber = 1;
        _pagerObject.ResetLink();
        PopulateTable();
    });

    function PopulateDropdown() {
        $("#ddlUserType").append($("<option  guid-value=" + emptyGUID + ">" + "None" + "</option>"));
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
                    $("#ddlUserType").append(el);
                }
            }
        });
    }

    function PopulatePermissions() {
        var deff = new $.Deferred();
        var newID = {
            userID: JSON.stringify(_id)
        }
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
                    data: JSON.stringify(newID),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (responsePermissions) {
                        var jsonostrPerm = responsePermissions.d;
                        var jsonobjPerm = eval('(' + jsonostrPerm + ')');

                        for (i in jsonobj) {
                            var realObj = eval(jsonobj[i]);

                            $(".permissions").append('	<div class="popupRow">\
																	<div class="permissioncontrol">\
																		<input type="checkbox" class="radiobutton" id="' + realObj.PermissionValue + '" />' +
															'</div>' +
															'<div class="permissionlabel">\
																		<label class="label">' + UserResources[realObj.PermissionName] + '</label>\
																	</div>\
																</div>');
                            var chkbox = $("#" + realObj.PermissionValue);
                            for (j in jsonobjPerm) {
                                if (realObj.PermissionValue == jsonobjPerm[j]["PermissionGuid"]) {
                                    chkbox.prop('checked', true);
                                }

                            }
                        }
                        deff.resolve();
                    },
                    failure: function () {
                        deff.reject();
                    }
                });
            }
        });
        return deff.promise();
    }

    //Populate table
    function PopulateTable() {

        for (var i = document.getElementById("userContainer").rows.length; i > 1; i--) {
            document.getElementById("userContainer").deleteRow(i - 1);
        }

        _rowsPerPage = $('.usersonpagedropdown').find($('option:selected')).text();
        _filterInfo = [];
        _filterInfo = {
            sortExpression: JSON.stringify(_sortExpression),
            rowsPerPage: JSON.stringify(_rowsPerPage),
            pageNumber: JSON.stringify(_pageNumber)
        };

        CountUsersWithAjax();
        GetUsersWithAjax();
    }

    function CountUsersWithAjax() {
        $.ajax({
            type: "POST",
            url: "../Webservices/UserService.asmx/CountAllUsers",
            data: "{ }",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                _usersCount = response.d;
                _pagerObject.Reset();
                _pagerObject.Init(_usersCount, _rowsPerPage, _pagerForm);
            }
        });
    }

    // The Ajax call used to get the users
    function GetUsersWithAjax() {

        _allUsers = [];

        $.ajax({
            type: "POST",
            url: "../Webservices/UserService.asmx/GetAllUsers",
            data: JSON.stringify(_filterInfo),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                var jsonostr = response.d;
                var jsonobj = eval('(' + jsonostr + ')');

                $.ajax({
                    type: "POST",
                    url: "../Webservices/UserService.asmx/GetAllUserTypes",
                    data: JSON.stringify(),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (responseType) {

                        var jsonostrType = responseType.d;
                        var jsonobjType = eval(jsonostrType);

                        for (i in jsonobj) {

                            for (j in jsonobjType) {
                                var realObjType = eval(jsonobjType[j]);
                                var _userType;

                                if (jsonobj[i]["UserType"] == realObjType.UserValue) {
                                    _userType = realObjType.UserType;
                                }
                            }

                            fuser = {
                                UserGuid: jsonobj[i]["UserGuid"],
                                UserName: TrimUserName(jsonobj[i]["UserName"]),
                                RealUserName: jsonobj[i]["UserName"],
                                UserType: _userType,
                                IsActive: jsonobj[i]["IsActive"],
                                btnIsActive: jsonobj[i]["IsActive"] == true ? "Deactivate" : "Activate",
                            }

                            _allUsers.push(fuser);
                        }
                        $("#userTemplate").tmpl(_allUsers).appendTo("#userContainer");

                        $('tr').each(function () {
                            var index = $(this).index() - 1;

                            if (index >= 0) {
                                if (_allUsers[index].btnIsActive == "Deactivate") {
                                    $(this).find('.activecolumn').find('.useractivateicon').remove();
                                    $(this).find('.activecolumn').append('<a href="#"><img src="../Images/deactivateuser.png" class="useractivateicon" style="width: 25px; height:25px;"></img></a>');
                                }
                                else {
                                    $(this).find('.activecolumn').find('.useractivateicon').remove();
                                    $(this).find('.activecolumn').append('<a href="#"><img src="../Images/activateuser.png" class="useractivateicon" style="width: 25px; height:25px;"></img></a>');
                                }
                            }
                        });

                        BindEvents();
                    }
                });
            }
        });//end ajax
    }

    function TrimUserName(text) {
        if (text.length >= 30) {
            return text.substring(0, 29) + " ...";
        }
        return text;
    }

    $(".insertUser").on('click', function () {
        $("#txtUsername").removeClass("incorrectfield");
        $("#ddlUserType").removeClass("incorrectfield");
        $("#chkIsActive").prop("checked", false);
        $("#chkLoginMode").prop("checked", false);
        $("#txtUsername").val("");
        $("#ddlUserType").find("option").each(function () {
            if ($(this).text() === "None")
                $(this).prop("selected", true);
        });
        $(".permissions").empty();
        _id = GenerateGuid();
        _mode = 1;
        $("#IsActiveRow").hide();
        dialog.dialog({ title: UserResources.UserInsert });
        $("#errorlist").empty();
        $("#errorlist").slideUp();
        $.when(PopulatePermissions())
			.then(function () {
			    dialog.dialog("open");
			});
    });

    // Binds the events for the links on the edit and deactivate columns
    function BindEvents() {
        $('#userContainer').off('click', '.userediticon').on('click', '.userediticon', function (event) {
            event.preventDefault();
            event.stopPropagation();

            $("#txtUsername").removeClass("incorrectfield");
            $("#ddlUserType").removeClass("incorrectfield");

            var index = $(this).closest('tr').index();
            index = index - 1;
            //window.location.href = "EditUser.aspx?PageMode=" + PageMode.Update + "&userID=" + _allUsers[index].UserGuid;
            $("#txtUsername").val(_allUsers[index].UserName);
            //$("#ddlUserType").selectedIndex = 1;

            $("#ddlUserType").find("option").each(function () {
                if ($(this).text() === _allUsers[index].UserType)
                    $(this).prop("selected", true);
            });

            if (!_allUsers[index].IsActive)
                $("#chkIsActive").prop('checked', false);
            else
                $("#chkIsActive").prop('checked', true);

            $(".permissions").empty();

            _id = _allUsers[index].UserGuid;

            _mode = 2;

            $("#IsActiveRow").show();

            dialog.dialog({ title: UserResources.UserEdit });

            $("#errorlist").empty();
            $("#errorlist").slideUp();

            if ((_allUsers[index].UserName).indexOf('@') != -1) {
                document.getElementById("chkLoginMode").checked = false;
            }
            else if ((_allUsers[index].UserName).indexOf('\\') != -1) {
                document.getElementById("chkLoginMode").checked = true;
            }

            $.when(PopulatePermissions())
				.then(function () {
				    dialog.dialog("open");
				});
        });

        $('#userContainer').off('click', '.useractivateicon').on('click', '.useractivateicon', function (event) {
            event.preventDefault();
            event.stopPropagation();

            var index = $(this).closest('tr').index();
            index = index - 1;
            var btnVal = $(this).val();

            if (btnVal == "Deactivate") { $(this).css("background-color", "green"); }
            else {
                $(this).css("background-color", "red");
            }

            var setUserStatus = {
                UserGuid: JSON.stringify(_allUsers[index].UserGuid),
                setStatus: JSON.stringify(_allUsers[index].IsActive)
            }

            $.ajax({
                type: "POST",
                url: "../Webservices/UserService.asmx/SetUserStatus",
                data: JSON.stringify(setUserStatus),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function () {
                    // location.reload();
                }
            });//end ajax

            PopulateTable();


        });


        // pager event
        $(document).on('pagerIndexChanged', function (event) {
            event.preventDefault();
            event.stopPropagation();

            _pageNumber = parseInt(event.onPage);
            if (_oldPageNr != _pageNumber) {
                PopulateTable();
                _oldPageNr = _pageNumber;

            }
        });

    }

    // events for the sort header links
    $('.UsernameSort').on('click', function (event) {

        SortTable('u.UserName', $(this), event);
    })
    $('.UserTypeSort').on('click', function (event) {

        SortTable('u.UserType', $(this), event);
    })
    $('.IsActiveSort').on('click', function (event) {

        SortTable('u.IsActive', $(this), event);
    })


    // This funciton is used to srt the table based on the clicked header
    function SortTable(sortText, form, event) {
        event.preventDefault();
        event.stopPropagation();


        _oldPageNr = 1;
        _pageNumber = 1;

        if (_oldSortText != sortText) { _sortDir = 0; }

        if (_sortDir == 0) {
            $('.arrowimg').remove();
            form.append('<img src="../Images/up_arrow.png"  class="arrowimg" style="float:right">');
            _sortExpression = sortText + ' desc';
            _sortDir = 1;
        } else if (_sortDir == 1) {
            $('.arrowimg').remove();
            form.append('<img src="../Images/down_arrow.png" class="arrowimg" style="float:right">');
            _sortExpression = sortText + ' asc';
            _sortDir = 0;
        }
        _oldSortText = sortText;

        _pagerObject.ResetLink();

        PopulateTable();
    }
});