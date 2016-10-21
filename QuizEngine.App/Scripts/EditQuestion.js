function EditQuestion() {
    var levelSource = [];
    var categorySource = [];
    var _pageMode;
    var _questionID;
    var newQuestion = "";
    var selectedTags = [];

    init = function (pageMode, questionID) {
        _pageMode = pageMode;
        _questionID = questionID;
        CheckPageMode();
    }

    /// Reads All Categories From the webservice request
    var ReadCategoriesOnSuccess = function (data) {

        var tempSource = JSON.parse(JSON.parse(data).d);
        $.each(tempSource, function (index, value) {
            categorySource.push({
                label: value.CategoryName,
                guid: value.CategoryUID
            });
        });
        $("#questionCategoryDropDownList").append($("<option  guid-value=" + emptyGUID + ">" + "None" + "</option>"));
        $.each(categorySource, function (index, value) {
            $("#questionCategoryDropDownList").append($("<option  guid-value=" + value.guid + ">" + value.label + "</option>"));
        });

    }

    ///Sends a request to the webservice for all the categories
    function LoadCategories() {

        var deff = new $.Deferred();
        var req = new PEService();
        req.ExecuteCall("QuestionService.asmx", "GetCategories", "{}",
           function (data) {
               ReadCategoriesOnSuccess(data);
               deff.resolve(data);
           },
            function (response) {
                deff.reject(null);
                alert(response.d);
            });
        //$.ajax({
        //    type: "POST",
        //    url: "../WebServices/QuestionService.asmx/GetCategories",
        //    data: "{}",
        //    contentType: "application/json; charset=utf-8",
        //    dataType: "json",
        //    success: function (data) {
        //        ReadCategoriesOnSuccess(data);
        //        deff.resolve(data);
        //    },
        //    failure: function (response) {
        //        alert(response.d);
        //    }
        //});

        return deff.promise();
    };

    /// Reads All Levels From the webservice request
    var ReadLevelsWithSuccess = function (data) {
        var tempSource = JSON.parse(JSON.parse(data).d);
        $.each(tempSource, function (index, value) {
            levelSource.push({
                label: value.LevelName,
                guid: value.LevelUID
            });
        });
        $("#questionLevelDropDownList").append($("<option  guid-value=" + emptyGUID + ">" + "None" + "</option>"));
        $.each(levelSource, function (index, value) {
            $("#questionLevelDropDownList").append($("<option  guid-value=" + value.guid + ">" + value.label + "</option>"));
        });
    }

    ///Sends a request to the webservice for all the Levels
    function LoadLevels() {
        var deff = new $.Deferred();
        var req = new PEService();
        req.ExecuteCall("QuestionService.asmx", "GetLevels", "{}",
             function (data) {
                 ReadLevelsWithSuccess(data);
                 deff.resolve(data);

             },
            function (response) {
                deff.reject(null);
                alert(response.d);
            });
        //$.ajax({
        //    type: "POST",
        //    url: "../WebServices/QuestionService.asmx/GetLevels",
        //    data: "{}",
        //    contentType: "application/json; charset=utf-8",
        //    dataType: "json",
        //    success: function (data) {
        //        ReadLevelsWithSuccess(data);
        //        deff.resolve(data);

        //    },
        //    failure: function (response) {
        //        alert(response.d);
        //    }
        //});
        return deff.promise();
    };

    /// Prepares the added or updated question for sending it to the server 
    prepareQuestion = function () {
        var temQuestion = {
            QuestionGUID: _questionID,
            Text: $('#QuestionTextBox').val().trim(),
            QuestionType: "",
            LevelGUID: $("#questionLevelDropDownList").find(":selected").attr("guid-value"),
            CategoryGUID: $("#questionCategoryDropDownList").find(":selected").attr("guid-value"),
        };
        return temQuestion;
    };

    /// Sends a request to the webservice for a question
    function LoadQuestion() {
        var deff = new $.Deferred();
        var data = { questionID: _questionID };
        var req = new PEService();
        req.ExecuteCall("QuestionService.asmx", "GetQuestionByID", JSON.stringify(data),
             function (data) {
                 deff.resolve(data);

             },
            function (response) {
                deff.reject(null);
                alert(response.d);
            });
        //$.ajax({
        //    type: "POST",
        //    url: "../WebServices/QuestionService.asmx/GetQuestionByID",
        //    data: JSON.stringify(data),
        //    contentType: "application/json; charset=utf-8",
        //    dataType: "json",
        //    success: function (data) {
        //        deff.resolve(data);

        //    },
        //    failure: function (response) {
        //        alert(response.d);
        //    }
        //});
        return deff.promise();

    }

    ///Reads a question data from a JSON
    function ReadQuestionWithSuccess(data) {
        var tempQuestion = JSON.parse(data).d;
        newQuestion = {
            questionGUID: _questionID,
            questionText: tempQuestion.Text,
            questionLevelID: tempQuestion.LevelGUID,
            questioCategoryID: tempQuestion.CategoryGUID,
            questionType: tempQuestion.QuestionType
        };
        PopulatePage();
    }

    ///Set the selected option from level and category dropdownlist by guid
    function ChooseSelectedOptionByID(control, guid) {
        control.find('option').each(function () {
            if ($(this).attr('guid-value') == guid) {
                $(this).prop('selected', true);
            }
        });
    };

    /// In case of view and update populates the controls with current data
    function PopulatePage() {
        $('#QuestionTextBox').val(newQuestion.questionText);
        ChooseSelectedOptionByID($("#questionLevelDropDownList"), newQuestion.questionLevelID);
        ChooseSelectedOptionByID($("#questionCategoryDropDownList"), newQuestion.questioCategoryID);
        $(".ChoiceControls1").attr("QuestionGUID", _questionID);
        $(".ChoiceControls1").attr("QuestionType", newQuestion.questionType);
    }

    ///In case of view state makes the controls unreachable
    function MakeContentReadOnly() {
        $("#ContentId").find('input').each(function () { $(this).prop('readonly', true) });
        $("#questionLevelDropDownList").prop('disabled', 'disabled');//??
        $("#questionCategoryDropDownList").prop('disabled', 'disabled');//??

    };

    ///Checks the PageMode of in which the page should be rendered
    function CheckPageMode() {
        $("#errors").slideUp();
        $.when(LoadLevels(), LoadCategories())
             .then(function (data) {
                 if (_pageMode === PageMode.View) {
                     $.when(LoadQuestion())
                        .then(function (datas) {
                            ReadQuestionWithSuccess(datas);
                            MakeContentReadOnly();
                        });
                 } else if (_pageMode === PageMode.Update) {
                     $.when(LoadQuestion())
                        .then(function (dataq) {
                            ReadQuestionWithSuccess(dataq);
                        });
                 };
             });
    }

    function CleanQuestions() {

        $("#questionCategoryDropDownList").find('option').each(function () { $(this).remove(); });
        $("#questionLevelDropDownList").find('option').each(function () { $(this).remove(); });
        $("#QuestionTextBox").val("");
        $("#tagsInput").val("");
        $(".errorListItem").each(function () {
            $(this).remove();
        });
    }

    return {
        Init: init,
        Question: prepareQuestion,
        CleanQuestion: CleanQuestions
    }
};



