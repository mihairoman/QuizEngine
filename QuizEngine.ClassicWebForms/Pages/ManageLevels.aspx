<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Site.Master" AutoEventWireup="true" CodeBehind="ManageLevels.aspx.cs" Inherits="QuizEngine.ClassicWebForms.Pages.ManageLevels" Culture="auto" meta:resourcekey="Page" UICulture="auto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="server">
    <script type="text/javascript" src="../Scripts/CheckCookie.js"></script>

    <script type="text/javascript">
        checkPermission("ManageLevels");
    </script>

	<script type="text/javascript" src="../Scripts/ManageLevels.js"></script>

    <script type="text/javascript">
        $(document).ready(function () {
            $('.components').show();
            $('#Levels').text(LevelResources.Levels); 
            $('#LevelNameLabel').text(LevelResources.LevelName);
            $('#LevelDifficultyLabel').text(LevelResources.LevelDifficultyLabel);
            $('#Edit').text(LevelResources.Edit);
            $('#Delete').text(LevelResources.Delete);
        });
	</script>

    <label style="font-family: Arial; font-size: 24px;" id="Levels"></label>
            <hr/><br/>

    <div id="Div1" class="toperrorList">
            <ul id="manageerrorlist">
			</ul>
        </div>
    <input type="button" class="btnInsert" value="Insert" style="width:50px" />
    <table id="levelContainer" class="gridviewtable" style="width: 700px !important;">
		<tr>
			<th style="display: none;">Level ID</th>
            <th id="LevelNameLabel" style="width:40%; text-align:left;"></th>
            <th id="LevelDifficultyLabel" style="width:40%; text-align:left;"></th>
            <th id="Edit" style="width:10%; text-align:left;"></th>
            <th id="Delete" style="width:10%; text-align:left;"></th>
		</tr>
	</table>

	<script id="levelTemplate" type="text/x-jQuery-tmpl">
		<tr>
			<td style="display: none;">${LevelUID}</td>
			<td>${LevelName}</td>
			<td style="text-align:left;">${Difficulty}</td>
			<td style="text-align:left;">
                <div>
                    <a id="btnEdit" href="#">
                        <img src="../Images/edit.png" style="width: 30px; height: 30px;" />
                    </a>
                </div>
			</td>
			<td style="text-align:left;">
                <div>
                    <a id="btnDelete" href="#">
                        <img src="../Images/x.png" style="width: 18px; height: 18px;" />
                    </a>
                </div>
			</td>
		</tr>
	</script>
    <input type="button" class="btnInsert" value="Insert" style="width:50px" />

	<div id="LevelManagementPopup" style="display: none;">
		<div id="errors" class="toperrorList">
            <ul id="errorlist">
			</ul>
        </div>
		<div class="popupRow">
			<div class="otherlabelColumn">
				<label id="lblLevelName" for="LevelName" class="label">Level name</label>
			</div>
			<div class="othercontrolColumn">
				<input type="text" id="txtLevelName" class="leveltextbox" />
			</div>
		</div>
		<div class="popupRow">
			<div class="otherlabelColumn">
				<label id="lblDifficulty" for="Difficulty" class="label">Difficulty</label>
			</div>
			<div class="othercontrolColumn">
				<input type="number" id="numDifficulty" class="leveltextbox" />
			</div>
		</div>
	</div>
</asp:Content>
