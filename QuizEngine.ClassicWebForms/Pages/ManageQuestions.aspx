<%@ Page Language="C#" UICulture="auto" Culture="auto" AutoEventWireup="true" MasterPageFile="~/Pages/Site.Master" CodeBehind="ManageQuestions.aspx.cs" Inherits="QuizEngine.ClassicWebForms.ManageQuestions" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="server">
    <script type="text/javascript" src="../Scripts/CheckCookie.js"></script>

    <script type="text/javascript">
        checkPermission("ManageQuestions");
    </script>
    <script type="text/javascript" src="../Scripts/SwapperCreator.js"></script>
    <script type="text/javascript" src="../Scripts/PagerControl.js"></script>
    <script src="../Scripts/EditQuestion.js"></script>
    <script src="../Scripts/TagsControl.js"></script>
    <script src="../Scripts/ValidateEditQuestionControl.js"></script>
    <script type="text/javascript" src="../Scripts/ChoiceTemplates.js"> </script>
    <script type="text/javascript" src="../Scripts/ChoiceControl.js"> </script>
    <script type="text/javascript" src="../Scripts/QuestionPopUp.js"> </script>

    <script type="text/javascript">
        $(document).ready(function () {
            $('#QuestionsTitle').text(QuestionResources.QuestionsTitle);
            $('#QuestionsOnPage').text(QuestionResources.QuestionsOnPage);
            $('.textsort').text(QuestionResources.TableTextSort);
            $('.categorysort').text(QuestionResources.TableCategorySort);
            $('.levelsort').text(QuestionResources.TableLevelSort);
            $('.tagsort').text(QuestionResources.TableTagSort);
            $('.questiontypesort').text(QuestionResources.TableTypeSort);
            $('.editquestion').text(QuestionResources.TableEdit);
            $('.deletequestion').text(QuestionResources.TableDelete);
            $('.filterexpand').text(QuestionResources.BtnFilters);
            $('.filtersSelectLevel').text(QuestionResources.FiltersSelectLevel);
            $('.filtersSelectCategory').text(QuestionResources.FiltersSelectCategory);
            $('.filtersSelectTags').text(QuestionResources.FiltersSelectTags);
            $('.filtersSelectTypes').text(QuestionResources.FiltersSelectTypes);
            $('.insertquestion').text(QuestionResources.BtnInsertQuestion);
            $('.filterall').text(QuestionResources.BtnApplyFilter);
            $('.resetallfilters').text(QuestionResources.BtnResetFilter);
            $('.label.questiontextLabel').text(QuestionResources.QuestionTextLabel);
            $('.label.levelLabel').text(QuestionResources.LevelLabel);
            $('.label.categorylabel').text(QuestionResources.CategoryLabel);
            $('.label.tagsInputLabel').text(QuestionResources.TagsLabel);
            $('.label.choiceLabel').text(QuestionResources.QuestionTypeLabel);
            $('.label').text(QuestionResources.FreeText);
            $('.label').text(QuestionResources.MultiChoice);
            $('.label').text(QuestionResources.Single);
            $('.label').text(QuestionResources.TrueFalse);
            $('.label').text(QuestionResources.Weighted);
            $('.label').text(QuestionResources.ChoiceIsCorrectLabel);
            $('.ui-dialog-title').text(QuestionResources.EditQuestionPopUpTitle);
            $('.ui-dialog-title').text(QuestionResources.InsertQuestionPopUpTitle);
            $('.ui-button-text').text(QuestionResources.QuestionPopUpSaveButton);
            $('.ui-button-text').text(QuestionResources.QuestionPopUpCencelButton);

            $('.allfilters').hide();
            $('.tagswapper').hide();
            $('.questiontypeswapper').hide();

            $('.displayfilters').click(function () {

                if ($('#changeTextFilters').text() == '+') {
                    $('#changeTextFilters').text('-');
                }
                else {
                    $('#changeTextFilters').text('+');
                }

                $('.allfilters').toggle();
            });

            $('.displaytags').click(function () {

                if ($('#changeTextTags').text() == '+') {
                    $('#changeTextTags').text('-');
                }
                else {
                    $('#changeTextTags').text('+');
                }

                $('.tagswapper').toggle();
            });
            $('.displayquestiontypes').click(function () {

                if ($('#changeTextQuestionTypes').text() == '+') {
                    $('#changeTextQuestionTypes').text('-');
                }
                else {
                    $('#changeTextQuestionTypes').text('+');
                }

                $('.questiontypeswapper').toggle();
            });
            $(function () {
                $('.questiontexttooltip').tooltip({
                    track: true
                });
            });
            $(function () {
                $('.questiontagtooltip').tooltip({
                    track: true
                });
            });
        });
    </script>
    <script type="text/javascript" src="../Scripts/DropDownListCreator.js"> </script>
    <script type="text/javascript" src="../Scripts/ManageQuestions.js"> </script>

    <div>
        <label style="font-family: Arial; font-size: 24px;" id="QuestionsTitle"></label>
        <hr />
        <br />
        <br />
        <a class="displayfilters" href="#" style="text-decoration: none">
            <div class="filterexpand" style="display: inline"></div>
            <div id="changeTextFilters" style="color: darkblue; font-size: 18px; font-weight: 900; display: inline">+</div>
        </a>
    </div>
    <div class="allfilters" style="font-family: Arial; font-size: 12px;">
        <br />
        <div style="display: inline-block; width: 210px;">
            <label class="filtersSelectLevel" style="float: left; margin-left: 15px;"></label>
            <select class="leveldropdown" style="width: 100px; margin-top: -6px; float: right; height: 20px;">
                <option value="none">None</option>
            </select>
        </div>
        <br />
        <br />
        <div style="display: inline-block; width: 210px;">
            <label class="filtersSelectCategory" style="float: left; margin-left: 15px;"></label>

            <select class="categorydropdown" style="width: 100px; margin-top: -6px; float: right; height: 20px;">
                <option value="none">None</option>
            </select>
        </div>
        <br />
        <br />
        <div style="width: 410px; display: inline-block;">
            <a style="float: left; text-decoration: none;" class="displaytags" href="#">

                <div class="filtersSelectTags" style="display: inline; margin-left: 15px;"></div>
                <div id="changeTextTags" style="color: darkblue; font-size: 17px; font-weight: 900; display: inline">+</div>
            </a>
            <div style="float: right;" class="tagswapper">
            </div>
        </div>
        <br />
        <br />
        <div style="width: 410px; display: inline-block;">
            <a style="float: left; text-decoration: none;" class="displayquestiontypes" href="#">
                <div class="filtersSelectTypes" style="display: inline; margin-left: 15px;"></div>
                <div id="changeTextQuestionTypes" style="color: darkblue; font-size: 17px; font-weight: 900; display: inline">+</div>
            </a>
            <div style="float: right;" class="questiontypeswapper">
            </div>
        </div>
        <br />
        <br />
        &nbsp;&nbsp;&nbsp;&nbsp;
        <button type="button" value="Apply Filter" class="filterall" style="width: 100px"></button>
        &nbsp;&nbsp;&nbsp;
        <button type="button" class="resetallfilters" style="width: 100px"></button>
        <br />
    </div>
    <br />
    <div style="display: inline-block; width: 220px;">
        <label id="QuestionsOnPage" style="float: left; font-family: Arial; font-weight: bold; font-size: 12px;">
        </label>
        <select class="questionsonpagedropdown" style="width: 100px; float: right; margin-top: -5px;">
            <option value="five">5</option>
            <option value="ten">10</option>
            <option value="twenty">20</option>
            <option value="fifty">50</option>
        </select>
    </div>
    <br />
    <br />
    <button type="button" class="insertquestion" style="width: 50px;"></button>
    <div class="questionstableerrormessage" style="text-align: center; color: red"></div>
    <table id="questionContainer" class="gridviewtable">
        <tr>
            <th style="width: 30%; text-align: left;" class="textsort"></th>
            <th style="width: 20%; text-align: left;" class="categorysort"></th>
            <th style="width: 20%; text-align: left;" class="levelsort"></th>
            <th style="width: 20%; text-align: left;" class="tagsort"></th>
            <th style="width: 20%; text-align: left;" class="questiontypesort"></th>
            <th style="width: 20%; text-align: left;" class="editquestion"></th>
            <th style="width: 10%; text-align: left;" class="deletequestion"></th>
        </tr>
    </table>
    <script id="questionTemplate" type="text/x-jQuery-tmpl">
        <tr>
            <td title="${RealText}" class="questiontexttooltip">${Text}</td>
            <td>${CategoryName}</td>
            <td>${LevelName}</td>
            <td title="${RealTag}" class="questiontagtooltip">${TagName}</td>
            <td>${QuestionType}</td>
            <td>
                <div>
                    <a class="questionedit" href="#">
                        <img src="../Images/edit.png" style="width: 30px; height: 30px;" />
                    </a>
                </div>
            </td>
            <td>
                <a class="questiondelete" href="#">
                    <img src="../Images/x.png" style="width: 18px; height: 18px;" />
                </a>
            </td>
        </tr>
    </script>
    <div style="display: inline;">
        <button type="button" class="insertquestion" style="width: 50px; float: left;"></button>
        <div class="questionpager" style="float: right;">
        </div>
        <br />
    </div>
    <div id="editQuestionControl" style="display: none;">
        <div id="errors" class="toperrorList">
            <ul id="errorlist">
            </ul>
        </div>
        <div class="popupRow">
            <div class="labelColumn">
                <label class="label questiontextLabel"></label>
            </div>
            <div class="controlColumn">
                <textarea id="QuestionTextBox" class="questionTextBox textarea" rows="2"> </textarea>
            </div>
        </div>
        <div class="popupRow">
            <div class="labelColumn">
                <label class="label levelLabel"></label>
            </div>
            <div class="controlColumn">
                <select id="questionLevelDropDownList" class="dropdown" name="D1"></select>
            </div>
        </div>
        <div class="popupRow">
            <div class="labelColumn">
                <label class="label categorylabel"></label>
            </div>
            <div class="controlColumn">
                <select id="questionCategoryDropDownList" class="dropdown" name="D2"></select>
            </div>
        </div>
        <div id="tags" class="tagsCollection popupRow">
            <div class="labelColumn">
                <label class="label tagsInputLabel"></label>
            </div>
            <div class="controlColumn">
                <div class="tagscontainer">
                    <div id="tagsInputContainer">
                    </div>
                    <div id="drowntags"></div>
                </div>
            </div>
        </div>
        <div class="choicesection popupRow"></div>
        <script id="errorControl" type="text/x-jQuery-tmpl">
            <li class="errorListItem">${error}</li>
        </script>
    </div>
</asp:Content>