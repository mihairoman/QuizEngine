<%@ Page Language="C#" MasterPageFile="~/Pages/Site.Master" AutoEventWireup="true" CodeBehind="QuizGenerate.aspx.cs" Inherits="QuizEngine.ClassicWebForms.Pages.QuizGenerate" Culture="auto" meta:resourcekey="Page" UICulture="auto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="server">
	<script type="text/javascript" src="../Scripts/CheckCookie.js"></script>

	<script type="text/javascript">
		checkPermission("GenerateQuizzes");
	</script>

	<script type="text/javascript">
		$(document).ready(function () {
			$('#generatequiz').text(QuizGenerateResources.GenerateQuiz);
			$('.categorieslabel').text(QuizGenerateResources.SelectedCategoryLabel);
			$('.levelslabel').text(QuizGenerateResources.SelectedLevelLabel);
			$('.taglabel').text(QuizGenerateResources.SelectedTagsLabel);
			$('.questionlabel').text(QuizGenerateResources.SelectedQuestionTypesLabel);
			$('#lblAddLevel').text(QuizGenerateResources.SelectLevelLabel);
			$('#lblAddCategory').text(QuizGenerateResources.SelectCategoryLabel);
			$('#lblAddTag').text(QuizGenerateResources.SelectTagsLabel);
			$('#lblAddType').text(QuizGenerateResources.SelectQuestionTypesLabel);
			$('#lblNumberOfQuestions').text(QuizGenerateResources.NumberOfQuestions);
			$('#templatetext').text(QuizGenerateResources.TemplateText);
			$('.save').attr('value', QuizGenerateResources.Save);
			$('.cancel').attr('value', QuizGenerateResources.Cancel);
			$('.sendquizlinkmail').attr('value', QuizGenerateResources.SendMail);
			$('.cancelsend').attr('value', QuizGenerateResources.Cancel);
			$('#usermail').text(QuizGenerateResources.UserMail);
			$('#selectedfeatures').text(QuizGenerateResources.SelectedFeatures);
			$('#preview').text(QuizGenerateResources.Preview);
		});
	</script>

	<script type="text/javascript" src="../Scripts/SwapperCreator.js"></script>
	<script src="../Scripts/DropDownListCreator.js"></script>
	<script type="text/javascript" src="../Scripts/QuizGenerate.js"></script>
	<script type="text/javascript">
		$(document).ready(function () {
			$(function () {
				$('.texttooltip').tooltip({
					track: true
				});
			});

			$(function () {
				$('.tagtooltip').tooltip({
					track: true
				});
			});
		});
	</script>
	<div style="font-family: Arial; font-size: small;">
		<div id="errorList" style="color: red;">
			<ul id="errorContainer">
			</ul>
		</div>
		<br />
		<label style="font-family: Arial; font-size: 24px" id="generatequiz"></label>
		<div class="separator" style="margin-bottom: 3%;"></div>
		<br />
		<div style="display: inline-block; width: 260px;">
			<label id="lblAddLevel" style="color: black; float: left;"></label>
			<select class="leveldropdown" style="width: 100px; float: right; height: 20px;">
				<option selected="selected" value="None">None
				</option>
			</select>

		</div>
		<br />
		<br />
		<div style="display: inline-block; width: 260px;">
			<label id="lblAddCategory" style="color: black; float: left;"></label>
			<select class="categorydropdown" style="width: 100px; float: right; height: 20px;">
				<option selected="selected" value="None">None
				</option>
			</select>
		</div>
		<br />
		<br />
		<br />
		<div style="width: 460px; display: inline-block;">
			<label id="lblAddTag" style="color: black; float: left"></label>
			<div class="tagswapper" style="float: right;">
			</div>
		</div>
		<br />
		<br />
		<div style="width: 460px; display: inline-block;">
			<label id="lblAddType" style="color: black; float: left;"></label>
			<div class="typeswapper" style="float: right;">
			</div>
		</div>
		<br />
		<br />
		<br />
		<div style="width: 345px; display: inline-block;">
			<label id="lblNumberOfQuestions" style="color: black; float: left;"></label>
			<input class="numberofquestions" id="txtNumberOfQuestions" type="number" style="width: 185px; float: right;" />
		</div>
		<br />
		<br />
		<br />
		<div style="display: inline-block; width: 200px;">
			<input type="button" value="Generate" class="generatequizbutton" style="float: left;" />
		</div>
		<br />
	</div>



	<div id="questionPopup" style="display: none;">
		<div id="errors" class="toperrorList">
			<ul id="errorlist">
			</ul>
		</div>
		<div id="approvals" class="topapprovalList">
			<ul id="approvallist">
			</ul>
		</div>

		<div class="popupRow">
			<div class="labelColumn">
				<label id="selectedfeatures"></label>
			</div>
			<div class="controlColumn">
				<div class="popupRow" style="margin-top: 0;">
					<div class="labelColumn" style="margin-top: 0;">
						<label class="categorieslabel label"></label>
					</div>
					<div class="controlColumn" style="margin-top: 0;">
						<label id="lblCategory" class="label"></label>
					</div>
				</div>
				<div class="popupRow">
					<div class="labelColumn">
						<label class="levelslabel label"></label>
					</div>
					<div class="controlColumn">
						<label id="lblLevel" class="label"></label>
					</div>
				</div>
				<div class="popupRow">
					<div class="labelColumn">
						<label class="taglabel label"></label>
					</div>
					<div class="controlColumn">
						<label id="lblTags" class="label"></label>
					</div>
				</div>
				<div class="popupRow" style="margin-bottom: 0;">
					<div class="labelColumn" style="margin-bottom: 0;">
						<label class="questionlabel label"></label>
					</div>
					<div class="controlColumn" style="margin-bottom: 0;">
						<label id="lblType" class="label"></label>
					</div>
				</div>
			</div>
		</div>

		<div id="grdQuestionPreview"></div>

		<div id="linkArea" class="popupRow">
			<div class="labelColumn">
				<label id="usermail" class="label"></label>
			</div>
			<div class="controlColumn">
				<div class="swpUserList"></div>
				<div class="sendmailsection popupRow">
					<input type="button" class="sendquizlinkmail ui-button ui-widget ui-state-default ui-corner-all ui-button-text-only popupbutton" name="btnSendMail" />
					<input type="button" class="cancelsend ui-button ui-widget ui-state-default ui-corner-all ui-button-text-only popupbutton" name="btnCancelSend" />
					<img class="loadimg" src="../Images/loadingcircles.gif" />
				</div>
			</div>
		</div>

		<div id="templateArea" class="popupRow">
			<div class="labelColumn">
				<label id="templatetext" class="label"></label>
			</div>
			<div class="controlColumn">
				<input type="text" id="savetemplate" class="textbox" name="txtsavetemplate" />
				<div class="popupRow">
					<input type="button" class="save ui-button ui-widget ui-state-default ui-corner-all ui-button-text-only popupbutton" name="btnsave" />
					<input type="button" class="cancel ui-button ui-widget ui-state-default ui-corner-all ui-button-text-only popupbutton" name="btncancel" />
				</div>
			</div>
		</div>
	</div>

	<script id="questionTemplate" type="text/x-jQuery-tmpl">
		<tr>
			<td style="display: none;">${QuestionGUID}</td>
			<td title="${RealText}" class="texttooltip">${QuestionText}</td>
			<td>${QuestionType}</td>
			<td title="${RealTag}" class="tagtooltip">${TagName}</td>
			<td style="text-align: center;">
				<a class="questionreplace" href="#">
					<img src="../Images/delete_and_replace.png" style="width: 30px; height: 30px;" />
				</a>
			</td>
		</tr>
	</script>

</asp:Content>

