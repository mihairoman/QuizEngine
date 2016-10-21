<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Site.Master" AutoEventWireup="true" Inherits="QuizEngine.App.Pages.ManageUsers" Culture="auto" meta:resourcekey="PageResource2" UICulture="auto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="server">
    <script type="text/javascript" src="../Scripts/CheckCookie.js"></script>

    <script type="text/javascript">
        checkPermission("ManageUsers");
    </script>
    <script type="text/javascript" src="../Scripts/PagerControl.js"></script>
    <script type="text/javascript" src="../Scripts/ValidateEditUser.js"></script>
    <script type="text/javascript" src="../Scripts/ManageUsers.js"></script>
    
    <script type="text/javascript">
        $(document).ready(function () {
            $('.components').show();
            $("#lblUsername").text(UserResources.UsernameLabel);
            $("#lblUsernameRestrictionMessage").text(UserResources.UsernameRestrictionMessage);
            $("#lblIsDomain").text(UserResources.IsDomainNameLabel);
            $("#lblUsernameCheckboxMessage").text(UserResources.UsernameCheckboxMessage);
            $("#lblUserType").text(UserResources.UserTypeLabel);
            $("#lblIsActive").text(UserResources.IsActiveLabel);
            $("#lblPermissions").text(UserResources.PermissionsLabel);
            $("#UsersTitle").text(UserResources.UsersTitle);
            $("#UsersOnPage").text(UserResources.UsersOnPage);
            $(".UsernameSort").text(UserResources.TableUsername);
            $(".UserTypeSort").text(UserResources.TableUserType);
            $(".IsActiveSort").text(UserResources.TableIsActive);
            $(".DeactivateUser").text(UserResources.TableDeactivate);
            $(".EditUser").text(UserResources.TableEdit);
            $(".insertUser").text(UserResources.InsertButton);
        });
    </script>

    <div>
		<label id="Users" style="font-family: Arial; font-size: 24px;"></label>
            <hr/><br/>
        <div style="display: inline-block; width: 265px;">
            <label id="UsersOnPage" style="font-size: 12px; font-family: Arial; font-weight: bold; color: #000000;"></label>
            <select class="usersonpagedropdown" style="width: 100px; float: right;">
                <option value="five">5</option>
                <option value="ten">10</option>
                <option value="twenty">20</option>
                <option value="fifty">50</option>
            </select>
        </div>
        <br/>
        <br/>
        <button type="button" class="insertUser" style="width: 50px; float: left;"></button>
        <table id="userContainer" class="gridviewtable">
            <tr>
                <td style="width: 35%; text-align: left;" class="UsernameSort"></td>
                <td class="UserTypeSort" style="text-align: left;"></td>
                <td class="IsActiveSort" style="text-align: left;"></td>
                <td style="width: 5%;" class="DeactivateUser"></td>
                <td style="width: 5%;" class="EditUser"></td>
            </tr>
        </table>

        <script id="userTemplate" type="text/x-jQuery-tmpl">
            <tr>
                <td title="${RealUserName}" class="usernametooltip">${UserName}</td>
                <td>${UserType}</td>
                <td>${IsActive}</td>
                <td class="activecolumn"></td>
                <td>
                    <a class="userediticon" href="#">
                        <img src="../Images/edit.png" style="width: 30px; height: 30px" />
                    </a>
                </td>
            </tr>
        </script>


        <div style="display: inline">
            <div>
                <button type="button" class="insertUser" style="width: 50px; float: left;"></button>
            </div>
            <div class="userpager" style="text-align: right;"></div>
        <br/>
    </div>

		

		
        </div>
    </div>

	<div id="UserManagementPopup" style="display: none;">
        <div id="errors" class="toperrorList">
            <ul id="errorlist">
            </ul>
        </div>
        <div class="popupRow">
            <div class="labelColumn">
                <label id="lblUsername" for="txtUsername" class="label"></label>
            </div>
            <div class="controlColumn">
                <input type="text" id="txtUsername" class="textbox" /><br />
                <label id="lblUsernameRestrictionMessage" class="label"></label>
            </div>
        </div>
        <div class="popupRow">
            <div class="labelColumn">
                <label id="lblIsDomain" class="label"></label>
            </div>
            <div class="controlColumn">
                <input id="chkLoginMode" type="checkbox" class="radiobutton" /><br />
                <label id="lblUsernameCheckboxMessage" class="label"></label>
            </div>
        </div>
        <div class="popupRow">
            <div class="labelColumn">
                <label id="lblUserType" class="label"></label>
            </div>
            <div class="controlColumn">
                <select id="ddlUserType" class="dropdown" name="D1"></select>
            </div>
        </div>
        <div id="IsActiveRow" class="popupRow">
            <div class="labelColumn">
                <label id="lblIsActive" class="label"></label>
            </div>
            <div class="controlColumn">
                <input id="chkIsActive" type="checkbox" class="radiobutton" />
            </div>
        </div>
        <div class="popupRow">
            <div class="labelColumn">
                <label id="lblPermissions" class="label"></label>
            </div>
            <div class="controlColumn">
                <div class="permissions">
                </div>
            </div>
        </div>
    </div>
</asp:Content>
