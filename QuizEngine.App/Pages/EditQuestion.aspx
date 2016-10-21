<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Pages/Site.Master"  Inherits="QuizEngine.WebForms.EditQuestion" Culture="auto" meta:resourcekey="EditQuestion" UICulture="auto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="server">
    <div id="editQuestionControl">
        <link rel="stylesheet" href="../Content/EditQuestionStyle.css"/>
        <script src="../Scripts/EditQuestion.js"></script>
        <script src="../Scripts/TagsControl.js"></script>
        <script src="../Scripts/ValidateEditQuestionControl.js"></script>
        <script type="text/javascript" src="../Scripts/ChoiceTemplates.js"> </script>
        <script type="text/javascript" src="../Scripts/ChoiceControl.js"> </script>
        <div id="errors">
            <ul id="errorList">
            </ul>
        </div>
        <label for="QuestionTextBox"><%= GetLocalResourceObject("lblQuestionText.Text") %></label>
        <textarea id="QuestionTextBox"  rows="2" cols="60"> </textarea><br />
        <label for="questionLevelDropDownList"><%= GetLocalResourceObject("lblQuestionLevel.Text") %></label>
        <select id="questionLevelDropDownList" name="D1"></select>
        <label for="questionCategoryDropDownList"><%= GetLocalResourceObject("lblQuestionCategory.Text") %></label>
        <select id="questionCategoryDropDownList" name="D2"></select>
        <div id="tags" class="tagsCollection" style="width:160px;">
            <label for="tagsInput"><%= GetLocalResourceObject("lblQuestionTags.Text") %></label>
            <input id="tagsInput" name="tagsInput" /><br />
        </div>
        <div class="choicesection"></div>
        <input id="submitButton" type="submit" value="<%= GetLocalResourceObject("lblQuestionSubmit.Value") %>" />

        <script type="text/javascript">
            $(document).ready(function () {
                var tagsControl = new TagsControl('tags');
                var editQuestion = new EditQuestion();
                var pageMode = GetQueryStringParam("PageMode");
                var questionUid = GetQueryStringParam("QuestionID");
                var questionType = GetQueryStringParam("QuestionType");
                var choiceControlObject = new ChoiceControl();
                choiceControlObject.Init(questionUid, questionType, pageMode == PageMode.Update, "../WebServices/QuestionService.asmx/GetChoiceByQuestionID");
                tagsControl.Init(questionUid, pageMode);
                editQuestion.Init(pageMode, questionUid);
                $('#submitButton').on('click', function (e) {
                    e.preventDefault();
                    e.stopPropagation();
                    var question = editQuestion.Question();
                    question.QuestionType = choiceControlObject.QuestionType();
                    var _allChoices = choiceControlObject.SelectedChoices();
                    var _allSelectedTags = tagsControl.SelectedTags();
                    var valid = new ValidateQuestion(_allSelectedTags, _allChoices, question);
                    if (valid.isValid) {
                        var prepareForSent = {
                            question: JSON.stringify(question),
                            tags: JSON.stringify(_allSelectedTags),
                            choices: JSON.stringify(_allChoices)

                        }
                        if (pageMode == "1") {
                            $.ajax({
                                type: "POST",
                                url: "../WebServices/QuestionService.asmx/AddQuestion",
                                data: JSON.stringify(prepareForSent),
                                contentType: "application/json; charset=utf-8",
                                dataType: "json",
                                success: function () { window.location.href="ManageQuestions.aspx"; },
                                failure: function (response) {
                                    alert(response.d);
                                }
                            });
                        } else if (pageMode == "2") {
                            $.ajax({
                                type: "POST",
                                url: "../WebServices/QuestionService.asmx/UpdateQuestion",
                                data: JSON.stringify(prepareForSent),
                                contentType: "application/json; charset=utf-8",
                                dataType: "json",
                                success: function () { window.location.href ="ManageQuestions.aspx"; },
                                failure: function (response) {
                                    alert(response.d);
                                }
                            });
                        }
                   
                    } else {
                        $(".errorListItem").each(function () { $(this).remove();});
                        $("#errorControl").tmpl(valid.Errors).appendTo(".errorListItem");
                    }
                    return false;
                });
            });
        </script>
        <script id="errorControl" type="text/x-jQuery-tmpl">
            <li class="errorListItem">${error}</li>        
        </script>
    </div>
</asp:Content>
