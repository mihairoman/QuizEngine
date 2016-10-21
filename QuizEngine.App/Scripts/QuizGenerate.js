$(document).ready(function () {
    $("#Quiz").show();
    $("#errorList").slideUp();

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
    $('#generatequiz').text(QuizGenerateResources.GenerateQuiz);
    $('#selectedfeatures').text(QuizGenerateResources.SelectedFeatures);

    var t = 0;
    var saved = false;
    var emails;
    var _c = [];
    var _aq = [];
    var quizID;
    var userID;
    var Tags = [];
    var _d = [];
    var _majorityNumber;
    var _minorityNumber;
    var _selectedLevel;
    var stringtag = "";
    var stringtype = "";
    var sendStringType = "";
    var userType;
    var takeQuiz = false;
    var userSwapperObjectId;
    var typeSwapperObjectId;
    var tagSwapperObjectId;
    var levelDropDownList;
    var categoryDropDownList;
    var i;
    var x;
    var y;
    var z;
    var loss = 0;
    var j = 0;
    var ok = true;
    var takeQuiz = false;

    userID = document.cookie.split('&')[1].split('=')[1];
    userType = document.cookie.split('&')[2].split('=')[1];
    quizID = GenerateGuid();


    userSwapperObjectId = new SwapperControl();
    typeSwapperObjectId = new SwapperControl();
    tagSwapperObjectId = new SwapperControl();
    typeSwapperObjectId = new SwapperControl();
    tagSwapperObjectId = new SwapperControl();
    levelDropDownList = new DropDownLists();
    categoryDropDownList = new DropDownLists();

    userSwapperObjectId.Init('swpUserList', 'GenerateQuizLinkService.asmx','GetUsers', 180);
    typeSwapperObjectId.Init('typeswapper', 'QuestionFilteringService.asmx','GetAllQuestionTypes');
    tagSwapperObjectId.Init('tagswapper', 'QuestionFilteringService.asmx', 'GetAllTags');
    levelDropDownList.Init("leveldropdown", "QuestionFilteringService.asmx","GetAllLevels");
    categoryDropDownList.Init("categorydropdown", "QuestionFilteringService.asmx","GetAllCategories");

    $(".save").on("focusin", function () {
        $(this).addClass("ui-state-focus");
    });
    $(".save").on("focusout", function () {
        $(this).removeClass("ui-state-focus");
    });
    $(".save").on("mouseenter", function () {
        $(this).addClass("ui-state-hover");
    });
    $(".save").on("mouseleave", function () {
        $(this).removeClass("ui-state-hover");
    });
    $(".save").on("mousedown", function () {
        $(this).addClass("ui-state-active");
    });
    $(".save").on("mouseup", function () {
        $(this).removeClass("ui-state-active");
    });
    $('.save').on('click', function () {
        var err = false;
        var nameerror = false;
        var templatelist = [];
        $("#errorlist").empty();
        $("#errorlist").slideUp();
        $("#approvallist").empty();
        $("#approvallist").slideUp();

        if ($("#savetemplate").val() == "") {
            $("#errorlist").append("<li>" + QuizGenerateResources.TemplateNameTooShortError + "</li>");
            err = true;
        }
        if ($("#savetemplate").val().length > 50) {
            $("#errorlist").append("<li>" + QuizGenerateResources.TemplateNameTooLong + "</li>");
            err = true;
        }
        if (saved) {
            $("#errorlist").append("<li>" + QuizGenerateResources.PopUpError + "</li>");
            err = true;
        }
        GetTemplate();
    });

    function GetTemplate() {
        var req = new PEService();
        req.ExecuteCall("QuestionService.asmx", "GetTemplateNames", "{}", OnGetTemplateSuccess, OnGetTemplateFailure);
    }
    function OnGetTemplateSuccess(response) {
        var jsonostr = JSON.parse(response);
        var jsonobj = JSON.parse(jsonostr.d);
            for (i in jsonobj) {
                _d = [];
                _d = {
                    QuizTemplateUID: jsonobj[i]['QuizTemplateGUID'],
                    TemplateName: jsonobj[i]['TypeName'],
                    Time: jsonobj[i]['Time']
                }
                templatelist.push(_d.TemplateName);
            }
            for (i in templatelist) {
                if ($("#savetemplate").val() == templatelist[i] && $("#savetemplate").val() != "") {
                    nameerror = true;
                    break;
                }
            }
            if (nameerror) {
                $("#errorlist").append("<li>" + QuizGenerateResources.TemplateNameError + "</li>");
                err = true;
            }
            if (!err) {
                i = 4;
                InsertTemplate();
                redirectuser();
            }
            else {
                $("#errorlist").slideDown();
            }
    }
    function OnGetTemplateFailure() {
        //nothing...
    }

    $(".cancel").on("focusin", function () {
        $(this).addClass("ui-state-focus");
    });
    $(".cancel").on("focusout", function () {
        $(this).removeClass("ui-state-focus");
    });
    $(".cancel").on("mouseenter", function () {
        $(this).addClass("ui-state-hover");
    });
    $(".cancel").on("mouseleave", function () {
        $(this).removeClass("ui-state-hover");
    });
    $(".cancel").on("mousedown", function () {
        $(this).addClass("ui-state-active");
    });
    $(".cancel").on("mouseup", function () {
        $(this).removeClass("ui-state-active");
    });
    $('.cancel').on('click', function () {
        $("#savetemplate").val("");
        $('#errorlist').empty();
        $("#approvallist").empty();
        $("#templateArea").slideUp();
        $("#linkArea").slideUp();
        $("#errorlist").slideUp();
        $("#approvallist").slideUp();
    });

    dialog = $("#questionPopup").dialog({
        dialogClass: "no-close",
        width: 750,
        minHeight: 682,
        maxHeight: 682,
        modal: true,
        closeOnEscape: true,
        resizable: false,
        draggable: true,
        autoOpen: false,
        close: function (event, ui) {
            $("#linkArea").hide();
            j = 0;
            $("#templateArea").hide();
            userSwapperObjectId.Reset();
        },
        buttons: [{
            id: "btn-genLink",
            text: QuizGenerateResources.GenerateLink,
            click: function () {
                i = 0;
                $('#errorlist').empty();
                $("#approvallist").empty();
                $("#templateArea").slideUp();
                $("#linkArea").slideDown();
            }
        }, {
            id: "btn-savequiz",
            text: QuizGenerateResources.SaveTemplate,
            click: function () {
                $('#errorlist').empty();
                $("#approvallist").empty();
                $('#errorlist').slideUp();
                $("#approvallist").slideUp();
                $("#linkArea").slideUp();
                $("#templateArea").slideDown();
            }
        }, {
            id: "btn-takeQuiz",
            text: QuizGenerateResources.TakeQuiz,
            click: function () {
                i = 1;
                if (j != 1) {
                    takeQuiz = true;
                    InsertQuestions();
                } else {
                    redirectuser();
                }
            }
        }, {
            id: "btn-downloadPDF",
            text: QuizGenerateResources.DownloadPDF,
            click: function () {
                i = 2;
                if (j != 1) {
                    j = 1;
                    InsertQuestions();
                }
                else {
                    redirectuser();
                }
            }
        }, {
            id: "btn-downloadWord",
            text: QuizGenerateResources.DownloadDOC,
            click: function () {
                i = 3;
                if (j != 1) {
                    InsertQuestions();
                } else {
                    redirectuser();
                }
            }
        }, {
            id: "btn-cancel",
            text: QuizGenerateResources.Cancelbutton,
            click: function () {
                j = 0;
                $('#errorlist').empty();
                $("#approvallist").empty();
                $('#errorlist').slideUp();
                $("#approvallist").slideUp();
                $("#errorList").slideUp();
                $("#errorContainer").empty();
                $("#savetemplate").val("");
                $(this).dialog("close");
            }
        }]
    });
    dialog.dialog({ title: QuizGenerateResources.Preview });

    if (dialog.dialog("instance") != "undefined") {
        if (userType == 1) {
            $("#btn-genLink").css("visibility", 'visible');
            $("#btn-savequiz").css("visibility", 'visible');
        }
        else {
            $("#btn-genLink").css("visibility", 'hidden');
            $("#btn-savequiz").css("visibility", 'hidden');
        }
    }

    $(".generatequizbutton").on('click', function () {
        $("#approvallist").empty();
        $("#errorlist").empty();
        $('#errorlist').slideUp();
        $("#errorList").slideUp();
        $("#errorContainer").empty();
        $("#approvallist").slideUp();
        $("#linkArea").slideUp();
        $("#templateArea").slideUp();
        var allType = typeSwapperObjectId.Texts();
        var allTag = tagSwapperObjectId.Texts();
        var length = allType.length;
        ok = true;
        saved = false;

        if ($(".leveldropdown").val() == "None") {
            ok = false;
            $("#errorContainer").append("<li>" + QuizGenerateResources.LevelError + "</li>");
        }
        if ($(".categorydropdown").val() == "None") {
            ok = false;
            $("#errorContainer").append("<li>" + QuizGenerateResources.CategoryError + "</li>");
        }
        if (allTag.length == 0) {
            ok = false;
            $("#errorContainer").append("<li>" + QuizGenerateResources.TagError + "</li>");
        }
        if (allType.length == 0) {
            ok = false;
            $("#errorContainer").append("<li>" + QuizGenerateResources.TypeError + "</li>");
        }
        if (!($(".numberofquestions").val() % 1 === 0)) {
            ok = false;
            $("#errorContainer").append("<li>" + QuizGenerateResources.IntegerError + "</li>");
        }
        if (($(".numberofquestions").val() <= 4) || ($(".numberofquestions").val() > 50)) {
            ok = false;
            $("#errorContainer").append("<li>" + QuizGenerateResources.QuestionNumberError + "</li>");
        }

        if (!ok) {
            $("#errorList").slideDown();
            return false;
        }
        stringtype = "";
        sendStringType = "";

        for (var i = 0; i < (length - 1) ; i++) {
            stringtype += allType[i].OptionName + ',';
            sendStringType += GetFromStringQuestionType(allType[i].OptionName) + ',';
        }

        stringtype += allType[length - 1].OptionName;
        sendStringType += GetFromStringQuestionType(allType[length - 1].OptionName);
        length = "";
        length = allTag.length;
        stringtag = "";
        Tags = [];
        for (var j = 0; j < (length - 1) ; j++) {
            stringtag += allTag[j].OptionName + ',';
            Tags.push(allTag[j].OptionName);
        }
        stringtag += allTag[length - 1].OptionName;
        Tags.push(allTag[length - 1].OptionName);
        $("#lblCategory").text(categoryDropDownList.Texts());
        $("#lblLevel").text(levelDropDownList.Texts());
        $("#lblTags").text(stringtag);
        $("#lblType").text(stringtype);
        PopulateTable()
        dialog.dialog("open");
    });

    $('.loadimg').hide();

    $(".cancelsend").on("focusin", function () {
        $(this).addClass("ui-state-focus");
    });
    $(".cancelsend").on("focusout", function () {
        $(this).removeClass("ui-state-focus");
    });
    $(".cancelsend").on("mouseenter", function () {
        $(this).addClass("ui-state-hover");
    });
    $(".cancelsend").on("mouseleave", function () {
        $(this).removeClass("ui-state-hover");
    });
    $(".cancelsend").on("mousedown", function () {
        $(this).addClass("ui-state-active");
    });
    $(".cancelsend").on("mouseup", function () {
        $(this).removeClass("ui-state-active");
    });
    $('.cancelsend').on('click', function () {
        $('#errorlist').empty();
        $("#approvallist").empty();
        $("#linkArea").slideUp();
        userSwapperObjectId.Reset();
    });

    $(".sendquizlinkmail").on("focusin", function () {
        $(this).addClass("ui-state-focus");
    });
    $(".sendquizlinkmail").on("focusout", function () {
        $(this).removeClass("ui-state-focus");
    });
    $(".sendquizlinkmail").on("mouseenter", function () {
        $(this).addClass("ui-state-hover");
    });
    $(".sendquizlinkmail").on("mouseleave", function () {
        $(this).removeClass("ui-state-hover");
    });
    $(".sendquizlinkmail").on("mousedown", function () {
        $(this).addClass("ui-state-active");
    });
    $(".sendquizlinkmail").on("mouseup", function () {
        $(this).removeClass("ui-state-active");
    });
    $('.sendquizlinkmail').on('click', function () {
        $('#approvePopupContainer').empty();
        $('#swapperMessage').empty();
        $('#approvallist').empty();
        $('#errorlist').slideUp();
        var allUserEmails = [];
        var allUserGUID = [];

        var swapperTexts = userSwapperObjectId.Texts();
        var swapperValues = userSwapperObjectId.Values();
        var swapperElems = swapperValues.length;

        if (swapperElems > 0) {
            for (var i = 0; i < swapperTexts.length; i++) {
                allUserEmails.push(swapperTexts[i].OptionName);
                allUserGUID.push(swapperValues[i].OptionGUID);
            }
            emails = {
                allEmails: JSON.stringify(allUserEmails),
                allGUID: JSON.stringify(allUserGUID),
                quizGUID: JSON.stringify(quizID)
            }
            if (j != 1) {
                takeQuiz = true;
                InsertQuestions();
                j = 1;
            }

            $('.loadimg').show();
            $('.sendquizlinkmail').hide();

            SendMails();
        }
        else {
            $("#errorlist").append("<li>" + QuizGenerateResources.MailDeliveryTooFewError + "</li>");
            $("#errorlist").slideDown();
        }
    });

    function SendMails() {
        var req = new PEService();
        req.ExecuteCall("GenerateQuizLinkService.asmx", "SendDefaultEmailAndInsertQuizLink", JSON.stringify(emails), OnSuccess, OnError);
    }

    function OnSuccess() {
        if (swapperElems > 0) {
            $('.sendquizlinkmail').show();
            $('.loadimg').hide();
            $("#approvallist").append("<li>" + QuizGenerateResources.MailDeliverySuccess + "</li>");
            $("#approvallist").slideDown();
            userSwapperObjectId.Reset();
        }
    }
    function OnError() {
        $("#errorlist").append("<li>" + QuizGenerateResources.MailDeliveryError + "</li>");
        $("#errorlist").slideDown();
    }

    function InsertTemplate() {
        $.getScript('../Scripts/moment.js', function () {
            var now = moment.duration('14.00:00:00');

            z = {
                questionGuidList: JSON.stringify(_questionlist),
                specialQuizName: JSON.stringify($("#savetemplate").val()),
                timespan: JSON.stringify(now)
            }
            MakeTemplate();
        });
    }

    function MakeTemplate() {
        var req = new PEService();
        req.ExecuteCall("QuizGenerate.asmx", "MakeTemplate", JSON.stringify(z), OnTemplateSuccess, OnTemplateError);
    }

    function OnTemplateSuccess() {
        saved = true;
        takeQuiz = true;
        if (j != 1) {
            InsertQuestions();
        }
        $("#savetemplate").val("");
    }
    function OnTemplateError() {
        //nothing...
    }

    function InsertQuestions() {

        _aq = {
            CategoryUID: categoryDropDownList.Values(),
            LevelUID: levelDropDownList.Values(),
            QuizUID: quizID
        }
        x = { quiz: JSON.stringify(_aq) }
        InsertIntoDatabase();
    }

    function InsertIntoDatabase() {
        var req = new PEService();
        req.ExecuteCall("QuizGenerate.asmx", "InsertQuiz", JSON.stringify(x), OnSaveSuccess, OnSaveError);
    }

    function OnSaveSuccess() {
        var table = $("#questionContainer");
        var listofquestions = [];
        var listofrows = [];
        var g = 0;
        if (userType == 1) {
            table.find('tr').each(function (i) {
                var $tds = $(this).find('td'),
                    questionobject = {
                        IndexOrder: i,
                        QuestionID: $tds.eq(0).text(),
                        QuestionText: $tds.eq(1).text(),
                        QuestionType: $tds.eq(2).text(),
                        TagName: $tds.eq(3).text()
                    };
                g += 1;
                if (g != 1) {
                    listofrows.push(questionobject.IndexOrder);
                    listofquestions.push(questionobject.QuestionID);
                }
            });
            y = {
                questionlist: JSON.stringify(listofquestions),
                quizUID: JSON.stringify(quizID),
                userUID: JSON.stringify(userID),
                takequiz: JSON.stringify(takeQuiz),
                indexorder: JSON.stringify(listofrows)
            }
        }
        else {
            for (var i = 1; i <= _questionlist.length; i++) {
                listofrows.push(i);
            }

            y = {
                questionlist: JSON.stringify(_questionlist),
                quizUID: JSON.stringify(quizID),
                userUID: JSON.stringify(userID),
                takequiz: JSON.stringify(takeQuiz),
                indexorder: JSON.stringify(listofrows)
            }
        }
        InsertIntoArchive();
    }

            function InsertIntoArchive() {
                var req = new PEService();
                req.ExecuteCall("QuizGenerate.asmx", "InsertArchivedQuiz", JSON.stringify(y), OnArchiveSuccess, OnArchiveFailure);
            }

            function OnArchiveSuccess() {
                redirectuser();
            }
            function OnArchiveFailure() {
                //nothing...
            }

    function OnSaveError() {
        //nothing...
    }

    function PopulateTable() {
        if (document.getElementById("questionContainer")) {
            //for (var i = document.getElementById("questionContainer").rows.length; i > 1; i--)
            //    document.getElementById("questionContainer").deleteRow(i - 1);

            $("#questionContainer").remove();
            $("#popUp").empty();
            $("#popUp").remove();
        }
        $("#tableContainer").empty();

        _questions = [];
        _questionlist = new Array();
        _selectedLevel = levelDropDownList.Values();
        if (_selectedLevel == "none") {
            _selectedLevel = '00000000-0000-0000-0000-000000000000';
        }
        ReceiveDifficulty(_selectedLevel);
    }

    function redirectuser() {
        switch (i) {
            case 1:
                location.replace("../Pages/OnlineTestingWizard.aspx?QuizUID=" + quizID + "&UserUID=" + userID);
                dialog.dialog("close");
                break;
            case 2:
                //var user = document.cookie.split('&')[0].split('=')[2];
                location.replace("../Pages/GenerateFileHandler.ashx?FileType=0&QuizUID=" + quizID);
                dialog.dialog("close");
                break;
            case 3:
                //var user = document.cookie.split('&')[0].split('=')[2];
                location.replace("../Pages/GenerateFileHandler.ashx?FileType=1&QuizUID=" + quizID);
                quizID = GenerateGuid();
                dialog.dialog("close");
                break;
            case 4:
                $("#templateArea").slideUp();
                $('#approvallist').empty();
                $("#approvallist").append("<li>" + QuizGenerateResources.TemplateSave + "</li>");
                $("#approvallist").slideDown();
                break;
        }
    }

    function ReceiveDifficulty(level) {
        x = { levelUID: JSON.stringify(level) }
    }

    function ReceiveDifficultyName() {
        var req = new PEService();
        req.ExecuteCall("QuestionService.asmx", "GetLevelByID", JSON.stringify(x), OnDifficultySuccess, OnDiffucultyFailure);
    }
    function OnDifficultySuccess(response) {
        var jsonostr = JSON.parse(response);
        var jsonobj = JSON.parse(jsonostr.d);
        _d = [];
        _d = {
            LevelUID: jsonobj['LevelUID'],
            LevelName: jsonobj['LevelName'],
            Difficulty: jsonobj['Difficulty']
        }
        _filter = [];
        _majorityNumber = Math.round($(".numberofquestions").val() * 7 / 10);
        _minorityNumber = Math.round($(".numberofquestions").val() * 15 / 100);
        if ((_majorityNumber + _minorityNumber + _minorityNumber) > $(".numberofquestions").val()) {
            _majorityNumber -= ((_majorityNumber + _minorityNumber + _minorityNumber) - $(".numberofquestions").val());
        }
        else if ((_majorityNumber + _minorityNumber + _minorityNumber) < $(".numberofquestions").val()) {
            _majorityNumber += ($(".numberofquestions").val() - (_majorityNumber + _minorityNumber + _minorityNumber));
        }
        _filter = {
            levelDifficulty: JSON.stringify(_d.Difficulty),
            majorityNumber: JSON.stringify(_majorityNumber),
            lowerLevel: JSON.stringify(_d.Difficulty - 1),
            higherLevel: JSON.stringify(_d.Difficulty + 1),
            minorityNumber: JSON.stringify(_minorityNumber),
            categoryName: JSON.stringify(categoryDropDownList.Texts()),
            tagList: JSON.stringify(stringtag),
            questionTypeList: JSON.stringify(sendStringType)
        }
        ReceiveQuestionFilter();
    }

            function ReceiveQuestionFilter() {
                var req = new PEService();
                req.ExecuteCall("QuestionFilteringService.asmx", "GetQuestionsByDifficulty", JSON.stringify(_filter), OnFilterSuccess, OnFilterFailure);
            }
            function OnFilterSuccess(response) {
                _questions = [];
                _questionlist = [];
                var jsonostr = JSON.parse(response);
                var jsonobj = JSON.parse(jsonostr.d);
                var index = 0;
                for (i in jsonobj) {
                    _c = {
                        QuestionGUID: jsonobj[i]['QuestionGUID'],
                        QuestionText: TrimText(jsonobj[i]['Text']),
                        RealText: jsonobj[i]["Text"],
                        QuestionType: jsonobj[i]['QuestionType'],
                        CategoryUID: jsonobj[i]['CategoryUID'],
                        LevelUID: jsonobj[i]['LevelUID'],
                        TagName: TrimTag(jsonobj[i]['TagName']),
                        RealTag: jsonobj[i]['TagName']
                    }
                    _questionlist.push(_c.QuestionGUID);
                    _questions.push(_c);
                }
                if (userType == 1) {
                    if ((_questions.length != 0) && (_questions.length == $(".numberofquestions").val())) {
                        index += 1;
                        if (index == 1) {
                            $("#grdQuestionPreview").append("<div id=\"popUp\" class=\"popupRow\"><div class=\"labelColumn\">" +
                                        "<label id=\"listofquestions\">" + QuizGenerateResources.ListOfQuestions + "</label></div><div class=\"controlColumn\">" +
                                "<div id=\"tableContainer\"></div></div></div>");

                            $("#tableContainer").append("<table id='questionContainer' class='gridviewtable2' style='width: 670px;'><tbody class='connectedSortable'> <tr>" +
                                                            "<th style='display: none;'>Question ID</th><th>" + QuizGenerateResources.QuestionText + "</th>" +
                                                            "<th>" + QuizGenerateResources.QuestionType + "</th><th>" + QuizGenerateResources.TagName + "</th>" +
                                                            "<th>" + QuizGenerateResources.ReplaceText + "</th></tr></tbody></table>");
                        }
                        $("#questionTemplate").tmpl(_questions).appendTo("#questionContainer");
                        $("#templateArea").slideUp("fast");
                        $("#linkArea").slideUp();
                        DragAndDrop();
                        SetReplaceFilter();
                    }
                    else if (_questions.length == 0) {
                        alert(QuizGenerateResources.NoQuestionsError);
                    }
                    else if (_questions.length != $(".numberofquestions").val()) {
                        alert(QuizGenerateResources.NotEnoughQuestionsError);
                    }
                }
            }
            function OnFilterFailure() {
                //nothing...
            }

    function OnDiffucultyFailure() {
        //nothing...
    }

    function SetReplaceFilter() {

        $('#questionContainer').off('click', '.questionreplace').on('click', '.questionreplace', function (event) {
            event.preventDefault();
            event.stopPropagation();
            var indexx = $(this).closest('tr').index();
            indexx = indexx - 1;

            _questionlist.splice(indexx, 1);

            $("#questionContainer tr:has(td)")[indexx].remove();
            _filter = [];
            _majorityNumber = 1;
            _minorityNumber = 0;

            _filter = {
                levelDifficulty: JSON.stringify(_d.Difficulty),
                majorityNumber: JSON.stringify(_majorityNumber),
                lowerLevel: JSON.stringify(_d.Difficulty - 1),
                higherLevel: JSON.stringify(_d.Difficulty + 1),
                minorityNumber: JSON.stringify(_minorityNumber),
                categoryName: JSON.stringify(categoryDropDownList.Texts()),
                tagList: JSON.stringify(stringtag),
                questionTypeList: JSON.stringify(sendStringType)
            }
            loss = 1;
            ReplaceQuestion();
        });
    }

    function ReplaceQuestion() {
        if (loss == 1) {
            GetQuestionsByFilter();
        }
    }
    ReplaceQuestion();

    function GetQuestionsByFilter() {
        var req = new PEService();
        req.ExecuteCall("QuestionFilteringService.asmx", "GetQuestionsByDifficulty", JSON.stringify(_filter), OnQuestionSuccess, OnQuestionFailure);
    }
    function OnQuestionSuccess() {
                var _questionss = [];
                var _questionlists = [];
                var jsonostr = JSON.parse(response);
                var jsonobj = JSON.parse(jsonostr.d);
                var indexxx = 0;
                for (i in jsonobj) {
                    _c = {
                        QuestionGUID: jsonobj[i]['QuestionGUID'],
                        QuestionText: TrimText(jsonobj[i]['Text']),
                        RealText: jsonobj[i]["Text"],
                        QuestionType: jsonobj[i]['QuestionType'],
                        CategoryUID: jsonobj[i]['CategoryUID'],
                        LevelUID: jsonobj[i]['LevelUID'],
                        TagName: TrimTag(jsonobj[i]['TagName']),
                        RealTag: jsonobj[i]['TagName']
                    }
                    _questionlists.push(_c.QuestionGUID);
                    _questionss.push(_c);
                }
                for (i in _questionlist) {
                    if (_questionlist[i] == _questionlists[0]) {
                        indexxx = 1;
                        ReplaceQuestion();  // Duplicate verify
                    }
                }
                if (indexxx == 0) {
                    _questionlist.push(_questionlists[0]);
                    $("#questionTemplate").tmpl(_questionss).appendTo("#questionContainer");
                }
    }
    function OnQuestionFailure() {
        //nothing...
    }

    function DragAndDrop() {
        $tabs = $("#questionContainer");

        $("tbody.connectedSortable")
            .sortable({
                connectWith: ".connectedSortable",
                items: "> tr:not(:first)",
                appendTo: $tabs,
                helper: "clone",
                zIndex: 999990,
                start: function () { $tabs.addClass("dragging") },
                stop: function () { $tabs.removeClass("dragging") }
            })
            .disableSelection();
    }

    function TrimText(text) {

        if (text.length >= 50) {
            return text.substring(0, 49) + " ...";
        }
        return text;
    }

    function TrimTag(text) {
        var valtext = text;
        if (valtext.length >= 9) {
            valtext = text.substring(0, 10) + "...";
        }
        return valtext;
    }

});





