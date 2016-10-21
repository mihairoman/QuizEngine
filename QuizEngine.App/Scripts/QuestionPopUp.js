function QuestionPopUp() {
    var _pageMode;
    var _questionUID;
    var _questionType;
    var tagsControl = new TagsControl('tags');
    var editQuestion = new EditQuestion();
    var choiceControlObject = new ChoiceControl();
    init = function (pageMode, questionUID, questionType) {
        $('.label.questiontextLabel').text(QuestionResources.QuestionTextLabel);
        $('.label.levelLabel').text(QuestionResources.LevelLabel);
        $('.label.categorylabel').text(QuestionResources.CategoryLabel);
        $('.label.tagsInputLabel').text(QuestionResources.TagsLabel);
        _pageMode = pageMode;
        _questionUID = questionUID;
        _questionType = questionType;
        tagsControl.CleanTags();
        editQuestion.CleanQuestion();
        choiceControlObject.CleanChoices();
        choiceControlObject.Init(_questionUID, _questionType, _pageMode == PageMode.Update, "../WebServices/QuestionService.asmx/GetChoiceByQuestionID");
        tagsControl.Init(_questionUID, _pageMode);
        editQuestion.Init(_pageMode, _questionUID);
    }

    function submitQuestion(e) {
        e.preventDefault();
        e.stopPropagation();
        $("#errors").slideUp();
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
            if (_pageMode == PageMode.Insert) {
                var req = new PEService();
                req.ExecuteCall("QuestionService.asmx", "AddQuestion", JSON.stringify(prepareForSent),
                    function () { window.location.href = "ManageQuestions.aspx"; },
                    function (response) {
                        alert(response.d);
                    });
                //$.ajax({
                //    type: "POST",
                //    url: "../WebServices/QuestionService.asmx/AddQuestion",
                //    data: JSON.stringify(prepareForSent),
                //    contentType: "application/json; charset=utf-8",
                //    dataType: "json",
                //    success: function () { window.location.href = "ManageQuestions.aspx"; },
                //    failure: function (response) {
                //        alert(response.d);
                //    }
                //});
            } else if (_pageMode == PageMode.Update) {
                var req = new PEService();
                req.ExecuteCall("QuestionService.asmx", "UpdateQuestion", JSON.stringify(prepareForSent),
                    function () { window.location.href = "ManageQuestions.aspx"; },
                    function (response) {
                        alert(response.d);
                    });
                //$.ajax({
                //    type: "POST",
                //    url: "../WebServices/QuestionService.asmx/UpdateQuestion",
                //    data: JSON.stringify(prepareForSent),
                //    contentType: "application/json; charset=utf-8",
                //    dataType: "json",
                //    success: function () { window.location.href = "ManageQuestions.aspx"; },
                //    failure: function (response) {
                //        alert(response.d);
                //    }
                //});
            }

        } else {
            $(".errorListItem").each(function () { $(this).remove(); });
            $("#errorControl").tmpl(valid.Errors).appendTo("#errorlist");
            $("#errors").slideDown();
        }
        return false;
    };
    return {
        Init: init,
        Submit: submitQuestion
    }
};