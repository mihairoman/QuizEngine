<%@ Page MasterPageFile="../MasterPages/Site.Master"%>
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
            $('#usermail').text(QuizGenerateResources.UserMail);
            $('#selectedfeatures').text(QuizGenerateResources.SelectedFeatures);
            $('#preview').text(QuizGenerateResources.Preview);
        });
	</script>
    
    <script type="text/javascript" src="../Scripts/SwapperCreator.js"></script>
    <script type="text/javascript" src="../Scripts/DropDownListCreator.js"></script>
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
        <div id="errorpopup" style="color: red;">
            <ul id="errorlist">
            </ul>
        </div>
        <div id="approvePopupList" style="color: green;">
            <ul id="approvePopupContainer">
            </ul>
        </div>

    <div class="popupRow">
        <div class="labelColumn">
            <label id="selectedfeatures"></label>
        </div>
    <div class="controlColumn">
        <div class="popupRow">
            <div class="labelColumn">
                <label class="categorieslabel" ></label>
            </div>
            <div class="controlColumn">
                <label id="lblCategory"></label>
            </div>
        </div>
        <div class="popupRow">
            <div class="labelColumn">
                <label class="levelslabel"></label>
            </div>
            <div class="controlColumn">
                <label id="lblLevel"></label>
            </div>
        </div>
        <div class="popupRow">
            <div class="labelColumn">
                <label class="taglabel"></label>
            </div>
            <div class="controlColumn">
                 <label id="lblTags"></label>
            </div>
        </div>
        <div class="popupRow">
            <div class="labelColumn">
                <label class="questionlabel"></label>
            </div>
            <div class="controlColumn">
                <label id="lblType"></label>
            </div>
         </div>

    <div id="linkArea" style="display: none; width: 1000px; height: 150px; margin: 5px 10px 20px 20px;">
        <div style="margin-bottom: 15px; font-size: larger;">
            <label id="usermail"></label>
        </div>
        <div class="swpUserList" style="float: left; font: 12px arial;"></div>
        <div class="sendmailsection" style="display: inline-block; margin-left: 40px; margin-top: 40px;">
            <input type="button" class="sendquizlinkmail" name="btnSendMail" />
            <img class="loadimg" src="../Images/loadingcircles.gif" />
            <p id="swapperMessage"></p>
        </div>
    </div>

    <div id="templateArea">

        <div style="margin-bottom: 15px; font-size: larger;">
            <label id="templatetext"></label>
        </div>
        <input type="text" class="savetemplate" name="txtsavetemplate" />
            <input type="button" class="save" name="btnsave" />
            <input type="button" class="cancel" name="btncancel" />
            
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
            <td>
                <div align="center">
                    <input type="image" class="questionreplace" style="width: 30px; height: 30px; background: url(..//Images/delete_and_replace.png); background-size: 30px 30px; border: none; background-position: center; background-repeat: no-repeat;" />
                </div>
            </td>
        </tr>
    </script>

</asp:Content>

