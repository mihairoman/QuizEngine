﻿
<%@ Page MasterPageFile="../MasterPages/Site.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="server">
    <script type="text/javascript" src="../Scripts/CheckCookie.js"></script>

    <script type="text/javascript">
        checkPermission("ManageCategories");
    </script>

	<script type="text/javascript" src="../Scripts/ManageCategories.js"></script>

    <script type="text/javascript">
        $(document).ready(function () {
            $('.components').show();
            $('#Categories').text(CategoryResources.Categories);
            $('#lblCategoryName').text(CategoryResources.CategoryName);
            $('#CorrespondingNumberOfQuestions').text(CategoryResources.CorrespondingNumberOfQuestions);
            $('#Edit').text(CategoryResources.Edit);
            $('#Delete').text(CategoryResources.Delete);
        });
	</script>

    <label style="font-family: Arial; font-size: 24px;" id="Categories"></label>
    <hr />
    <br />

    <div id="Div1" class="toperrorList">
        <ul id="manageerrorlist">
        </ul>
    </div>
    <input type="button" class="btnInsert" value="Insert" style="width: 50px" />
    <table id="categoryContainer" class="gridviewtable" style="width: 700px !important">
        <tr>
            <th style="display: none;">Category ID</th>
            <th id="CategoryName"></th>
            <th id="CorrespondingNumberOfQuestions" ></th>
            <th id="Edit" ></th>
            <th id="Delete" ></th>
        </tr>
    </table>

    <script id="categoryTemplate" type="text/x-jQuery-tmpl">
        <tr>
            <td style="display: none;">${CategoryUID}</td>
            <td title="${RealCategoryName}" class="categorynametooltip">${CategoryName}</td>
            <td style="text-align: center;" >${NumberOfUsingQuestions}</td>
            <td style="text-align: center;">
                <div>
                    <a id="btnEdit" href="#">
                        <img src="../Images/edit.png" style="width: 30px; height: 30px;" />
                    </a>
                </div>
            </td>
			<td style="text-align: center;">
                <div>
                    <a id="btnDelete" href="#">
                        <img src="../Images/x.png" style="width: 18px; height: 18px;" />
                    </a>
                </div>
            </td>
        </tr>
    </script>
    <input type="button" class="btnInsert" value="Insert" style="width: 50px" />

    <div id="CategoryManagementPopup" style="display: none;">
        <div id="errors" class="toperrorList">
            <ul id="errorlist">
            </ul>
        </div>
        <div class="popupRow">
            <div class="otherlabelColumn">
				<label id="lblCategoryName" for="txtCategoryName" class="label"></label>
            </div>
            <div class="othercontrolColumn">
                <input type="text" id="txtCategoryName" class="categorytextbox" />
            </div>
        </div>
    </div>
</asp:Content>
