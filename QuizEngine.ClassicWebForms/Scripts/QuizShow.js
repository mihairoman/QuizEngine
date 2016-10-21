$(document).ready(function () {
    $("#HomeScreen").show();
    var quizQuestionChoice = [];
    var userAnswers = [];
    var quizID = GetQueryStringParam('QuizUID');
    var userID = GetQueryStringParam('UserUID');
    var result = GetQueryStringParam('Result');
    var quizDate = GetQueryStringParam('QuizDate');
    var userName = GetQueryStringParam('UserName');
    $("#userNameProp").text( userName);
    $("#quizResultProp").text( result);
    $("#quizDateProp").text( quizDate);
    getQuizQuestionChoicesWithAjax(userID, quizID);


    function getQuizQuestionChoicesWithAjax(userID, quizID) {
        quizQuestionChoice = [];
        var obj = { quizId: quizID, userId: userID };
        $.ajax
        ({
            type: "POST",
            url: "../Webservices/OnlineTestingWizardService.asmx/GetSerializedObject",
            data: JSON.stringify(obj),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                var jsonstr = response.d;
                var jsonobj = JSON.parse(jsonstr);
                for (i in jsonobj) {
                    qqc = {
                        QuestionText: jsonobj[i]["Text"],
                        QuestionType: jsonobj[i]["QuestionType"],
                        QuestionUID: jsonobj[i]["QuestionID"],
                        Choices: jsonobj[i]["Choices"]
                    }
                    quizQuestionChoice.push(qqc);
                }
                getAnswers(userID,quizID);
            },
            error: function (msg) {
                alert("Ajax error in get Questions" + msg.d);
            },
        }); //end ajax

    }

    function getAnswers(userID, quizID) {
        userAnswers = [];
        obj = {
            quizId: quizID,
            userId: userID
        };
        $.ajax
       ({
           type: "POST",
           url: "../Webservices/QuizViewListingService.asmx/ReadAllAnswersByQuizUserId",
           data: JSON.stringify(obj),
           contentType: "application/json; charset=utf-8",
           dataType: "json",
           success: function (response) {
               var jsonstr = response.d;
               var jsonobj = JSON.parse(jsonstr);
               for (i in jsonobj) {
                   answer = {
                       Text: jsonobj[i]["AnswerText"],
                       Grade: jsonobj[i]["Grade"],
                       QuestionUID: jsonobj[i]["ArchivedQuestionUID"],
                       ChoiceUID: jsonobj[i]["ArchivedChoiceUID"],
                   }
                   userAnswers.push(answer);
               }
               displayQuestions();
           },
           error: function (msg) {
               alert("Ajax error in get answers " + msg.d);
           },
       }); //end ajax

    }

    function GetQueryStringParam(key) {
        key = key.replace(/[*+?^$.\[\]{}()|\\\/]/g, "\\$&"); // escape RegEx meta chars
        var match = location.search.match(new RegExp("[?&]" + key + "=([^&]+)(&|$)"));
        return match && decodeURIComponent(match[1].replace(/\+/g, " "));

    };

    function displayQuestions() { //class="showedQuestion"
        var ul = $("#quizQuestions");
        ul.empty();
        for (var i = 0; i < quizQuestionChoice.length; i++) {
            var splitVector = quizQuestionChoice[i]["QuestionText"].split("\n");
            var toAppend = splitVector.join("<br />");
            ul.append("<li id= " + quizQuestionChoice[i]["QuestionUID"] + "><p id=" + quizQuestionChoice[i]["QuestionUID"] + "_" + quizQuestionChoice[i]["QuestionUID"] + ">" + (i+1) +". "+toAppend + "</p><br />");
            displayChoices(quizQuestionChoice[i]["QuestionType"], quizQuestionChoice[i]["QuestionUID"], quizQuestionChoice[i]["Choices"]);
            ul.append("</li>");
            ul.append("<hr>");
            $("#" + quizQuestionChoice[i]["QuestionUID"] + "_" + quizQuestionChoice[i]["QuestionUID"]).addClass("showedQuestion");
        }
    }

    function displayChoices(questionType,formId,obj ) {
        switch (questionType) {
            case 0: //free text
                showFreeText(formId,obj);
                break;
            case 1: // Multiple choice
               showMultiChoice(formId, obj);
                break;
            case 2: //single choice
                showSingleChoice(formId, obj);
                break;
            case 3: //true false
                showTrueFalse(formId,obj);
                break;
            case 4: //weighted
                showWeighted(formId,obj);
                break;
        }
    }

    function showMultiChoice(formId, obj) {
        var ul = $("#" + formId);
        for (var i = 0; i < obj.length; i++) { //obj = choices
         
            var j = isChoosen(obj[i]["QuestionID"], obj[i]["ChoiceID"]);
            var IsCorrect = obj[i]["IsCorrect"];
            var imgSource = null;
            if (IsCorrect) { //if the choice is correct
                imgSource = '<img src="../Images/OK.jpg" >';
            }
            if (j >= 0 && !IsCorrect) { // if the choice was choose and the is not correct
                imgSource = '<img src="../Images/wrong.png">';
            }
            if (imgSource != null) {
                ul.append("<p id= " + obj[i]["ChoiceID"] + "> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + imgSource + " " + obj[i]["AnswerText"] + "</p><br />");
            } else {
                ul.append("<p id= " + obj[i]["ChoiceID"] + "> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + obj[i]["AnswerText"] + "</p><br />");
            }
            if (j >= 0) { // if the user chooce these choice
                $("#" + obj[i]["ChoiceID"]).addClass("chosenAnswer");
            }
        }
    }

    function showSingleChoice(formId, obj ) {
        var ul = $("#" + formId);
        for (var i = 0; i < obj.length; i++) { //obj = choices
            var j = isChoosen(obj[i]["QuestionID"], obj[i]["ChoiceID"]);
            var IsCorrect = obj[i]["IsCorrect"];
            var imgSource = null;
            if (IsCorrect) {
                imgSource = '<img src="../Images/OK.jpg" >';
            }
            if (!IsCorrect && j >= 0) {
                imgSource = '<img src="../Images/wrong.png">';
            }
            if (imgSource != null) {
                ul.append("<p id= " + obj[i]["ChoiceID"] + "> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + imgSource + " " + obj[i]["AnswerText"] + "</p><br />");
            } else {
                ul.append("<p id= " + obj[i]["ChoiceID"] + "> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + obj[i]["AnswerText"] + "</p><br />");
            }
            if (j >= 0) { // if the user chooce these choice
                $("#" + obj[i]["ChoiceID"]).addClass("chosenAnswer");
            }
        }
    }

    function showFreeText(formId, obj) {
        var ul = $("#" + formId);
        ul.append("<p >&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; User response:</p><br />");
        var j = isChoosen(obj[0]["QuestionID"], obj[0]["ChoiceID"]);
        if (j >= 0) {
            var toAppend = userAnswers[j]["Text"]
        }
        if (typeof toAppend === 'undefined') {
            toAppend = "Not responded";
        }
        var splitVector = toAppend.split("\n");
        toAppend = splitVector.join("<br />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;");
        ul.append("<p  id= " + obj[0]["ChoiceID"]+ ">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + toAppend + "</p><br />");
        var j = isChoosen(obj[0]["QuestionID"], obj[0]["ChoiceID"]);
        $("#" + obj[0]["ChoiceID"]).addClass("chosenAnswer");
    }

    function showTrueFalse(formId,obj){
        var ul = $("#" + formId);
        for (var i = 0; i < obj.length; i++) {
            var IsCorrect = obj[i]["IsCorrect"];
            var imgSource = null;
            if (IsCorrect) {
                imgSource = '<img src="../Images/OK.jpg" >';
            }
            if (!IsCorrect && j >= 0) {
                imgSource = '<img src="../Images/wrong.png">';
            }

            if (imgSource != null) {
                ul.append("<p id= " + obj[i]["ChoiceID"] + "> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + imgSource + " " + obj[i]["AnswerText"] + "</p><br />");
            } else {
                ul.append("<p id= " + obj[i]["ChoiceID"] + "> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + obj[i]["AnswerText"] + "</p><br />");
            }
            var j = isChoosen(obj[i]["QuestionID"], obj[i]["ChoiceID"]);
            if (j >= 0) {
                $("#" + obj[i]["ChoiceID"]).addClass("chosenAnswer");
            }
        }
    }

    function showWeighted(formId, obj) {
        var ul = $("#" + formId);
        for (var i = 0; i < obj.length; i++) {
            ul.append("<p>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + obj[i]["AnswerText"] + "</p><br />");
            var j = isChoosen(obj[i]["QuestionID"], obj[i]["ChoiceID"]);
        }
    }

    function isChoosen(questionId, choiceId) {
        for (var i = 0; i < userAnswers.length; i++) {
            if (userAnswers[i]["QuestionUID"] == questionId &&(userAnswers[i]["ChoiceUID"] == choiceId)) {
                return i;
            }
        }
        return -1;
    }

});