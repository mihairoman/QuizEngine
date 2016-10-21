
<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Site.Master" AutoEventWireup="true" CodeBehind="ManageCategories.aspx.cs" Inherits="QuizEngine.ClassicWebForms.Pages.ManageCategories" Culture="auto" meta:resourcekey="Page" UICulture="auto" %>

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
            $('#CategoryName').text(CategoryResources.CategoryName);
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
            <th id="CategoryName" style="text-decoration:none;text-align:left; width:40%;"></th>
            <th id="CorrespondingNumberOfQuestions" style="text-decoration:none;text-align:left; width:40%;" ></th>
            <th id="Edit" width="10%"  align="left" ></th>
            <th id="Delete" width="10%" align="left" ></th>
        </tr>
    </table>

    <script id="categoryTemplate" type="text/x-jQuery-tmpl">
        <tr>
            <td style="display: none;">${CategoryUID}</td>
            <td title="${RealCategoryName}" class="categorynametooltip">${CategoryName}</td>
            <td>${NumberOfUsingQuestions}</td>
            <td style="text-align: left;">
                <div>
                    <a id="btnEdit" href="#">
                        <img src="../Images/edit.png" style="width: 30px; height: 30px;" />
                    </a>
                </div>
            </td>
			<td style="text-align: left;">
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
