$(document).ready(function () {
    $("#HomeScreen").show();
    var quizQuestionChoice = [];
    var userAnswers = [];
    var quizID = GetQueryStringParam('QuizUID');
    var userID = GetQueryStringParam('UserUID');
    var result = GetQueryStringParam('Result');
    var quizDate = GetQueryStringParam('QuizDate');
    var userName = GetQueryStringParam('UserName');
    var userNameLabelText = $("#userName").text();
    var quizResultLabelText = $("#quizResult").text();
    var quizDateLabelText = $("#quizDate").text();
    $("#userName").text(userNameLabelText + " "+ userName);
    $("#quizResult").text(quizResultLabelText+ " "+ result);
    $("#quizDate").text(quizDateLabelText + " " + quizDate);
    var req = new PEService();
    getQuizQuestionChoicesWithAjax(userID, quizID);
  

    function getQuizQuestionChoicesWithAjax(userID, quizID) {
        quizQuestionChoice = [];
        var obj = { quizId: quizID, userId: userID };
        req.ExecuteCall("OnlineTestingWizardService.asmx", "GetSerializedObject", JSON.stringify(obj), GetQuizQuestionWithAjaxSuccess, GetQuizQuestionWithAjaxError);
        //$.ajax
        //({
        //    type: "POST",
        //    url: "../Webservices/OnlineTestingWizardService.asmx/GetSerializedObject",
        //    data: JSON.stringify(obj),
        //    contentType: "application/json; charset=utf-8",
        //    dataType: "json",
        //    success: function (response) {
        //        var jsonstr = response.d;
        //        var jsonobj = JSON.parse(jsonstr);
        //        for (i in jsonobj) {
        //            qqc = {
        //                QuestionText: jsonobj[i]["Text"],
        //                QuestionType: jsonobj[i]["QuestionType"],
        //                QuestionUID: jsonobj[i]["QuestionID"],
        //                Choices: jsonobj[i]["Choices"]
        //            }
        //            quizQuestionChoice.push(qqc);
        //        }
        //        getAnswers(userID,quizID);
        //    },
        //    error: function (msg) {
        //        alert("Ajax error in get Questions" + msg.d);
        //    },
        //}); //end ajax
    }

    function GetQuizQuestionWithAjaxSuccess(response) {
        alert("enter with success");
        var response2 = JSON.parse(response);
        var jsonstr = response2.d;
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
        getAnswers(userID, quizID);
    }

    function GetQuizQuestionWithAjaxError() {
        alert("Ajax error durring getting the question!");
    }

    function getAnswers(userID, quizID) {
        userAnswers = [];
        obj = {
            quizId: quizID,
            userId: userID
        };
        req.ExecuteCall("QuizViewListingService.asmx", "ReadAllAnswersByQuizUserId", JSON.stringify(obj), getAnswersSuccess, getAnsewersError);
       // $.ajax
       //({
       //    type: "POST",
       //    url: "../Webservices/QuizViewListingService.asmx/ReadAllAnswersByQuizUserId",
       //    data: JSON.stringify(obj),
       //    contentType: "application/json; charset=utf-8",
       //    dataType: "json",
       //    success: function (response) {
       //      //  alert("enter in getAnswers");
       //        var jsonstr = response.d;
       //        var jsonobj = JSON.parse(jsonstr);
       //        for (i in jsonobj) {
       //            answer = {
       //                Text: jsonobj[i]["AnswerText"],
       //                Grade: jsonobj[i]["Grade"],
       //                QuestionUID: jsonobj[i]["ArchivedQuestionUID"],
       //                ChoiceUID: jsonobj[i]["ArchivedChoiceUID"],
       //            }
       //            userAnswers.push(answer);
       //        }
       //        displayQuestions();
       //    },
       //    error: function (msg) {
       //        alert("Ajax error in get answers " + msg.d);
       //    },
       //}); //end ajax
    }

    function getAnswersSuccess(response) {
        var response2 = JSON.parse(response);
        var jsonstr = response2.d;
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
    }

    function getAnsewersError() {
        alert("Ajax error getting the answers!");
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
            ul.append("<li id= " + quizQuestionChoice[i]["QuestionUID"] + "><p id=" + quizQuestionChoice[i]["QuestionUID"] + "_" + quizQuestionChoice[i]["QuestionUID"] + ">" + (i+1) +". "+quizQuestionChoice[i]["QuestionText"] + "</p><br />");
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
            var imgSource;
            if (IsCorrect) {
                imgSource = '<img src="../Images/OK.jpg" >';
            } else {
                imgSource  = '<img src="../Images/wrong.png">';
            }
            ul.append("<p id= " + obj[i]["ChoiceID"] + "> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + imgSource + " "+obj[i]["AnswerText"] + "</p><br />");
            if (j >= 0) { // if the user chooce these choice
                if (IsCorrect) {  // if the answer is correct
                    $("#" + obj[i]["ChoiceID"]).addClass("correctAnswer");
                } else { // if the answer is wrong
                    $("#" + obj[i]["ChoiceID"]).addClass("wrongAnswer");
                }
            }
        }
    }

    function showSingleChoice(formId, obj ) {
        var ul = $("#" + formId);
        for (var i = 0; i < obj.length; i++) { //obj = choices
            var j = isChoosen(obj[i]["QuestionID"], obj[i]["ChoiceID"]);
            var IsCorrect = obj[i]["IsCorrect"];
            var imgSource;
            if (IsCorrect) {
                imgSource = '<img src="../Images/OK.jpg" >';
            } else {
                imgSource = '<img src="../Images/wrong.png">';
            }
            ul.append("<p id= " + obj[i]["ChoiceID"] + "> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + imgSource + " " + obj[i]["AnswerText"] + "</p><br />");
            if (j >= 0) { // if the user chooce these choice
                if (IsCorrect) {  // if the answer is correct
                    $("#" + obj[i]["ChoiceID"]).addClass("correctAnswer");
                } else { // if the answer is wrong
                    $("#" + obj[i]["ChoiceID"]).addClass("wrongAnswer");
                }
            }
        }
    }

    function showFreeText(formId, obj) {
        var ul = $("#" + formId);
        ul.append("<p >&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; User response:</p><br />");
        var toAppend = obj["AnswerText"];
        if (typeof toAppend === 'undefined') {
            toAppend = "Not responded";
        }
        ul.append("<p  id= " + obj[0]["ChoiceID"]+ ">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + toAppend + "</p><br />");
        var j = isChoosen(obj[0]["QuestionID"], obj[0]["ChoiceID"]);
        var grade = userAnswers[j]["Grade"];
            if (grade == 1) {
                $("#" + obj[0]["ChoiceID"]).addClass("correctAnswer");
            }
            if (grade == 0) {
                $("#" + obj[0]["ChoiceID"]).addClass("wrongAnswer");
            }
            if (grade < 1 && grade > 0) {
                $("#" + obj[0]["ChoiceID"]).addClass("partiallyCorrect");
            }
    }

    function showTrueFalse(formId,obj){
        var ul = $("#" + formId);
        for (var i = 0; i < obj.length; i++) {
            var IsCorrect = obj[i]["IsCorrect"];
            var imgSource;
            if (IsCorrect) {
                imgSource = '<img src="../Images/OK.jpg" >';
            } else {
                imgSource = '<img src="../Images/wrong.png">';
            }
            //ul.append("<p>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + obj[i]["AnswerText"] + "</p><br />");
            ul.append("<p id= " + obj[i]["ChoiceID"] + "> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + imgSource + " " + obj[i]["AnswerText"] + "</p><br />");
            var j = getChoiceByQuestioniD(obj[i]["QuestionID"], obj[i]["ChoiceID"]);
            if (j >= 0) {
                var grade = userAnswers[j]["Grade"];
                if (grade == 1) {  // if the answer is correct
                    $("#" + obj[i]["ChoiceID"]).addClass("correctAnswer");
                }
                if (grade == 0) { // if the answer is wrong
                    $("#" + obj[i]["ChoiceID"]).addClass("wrongAnswer");
                }
            }
        }
    }

    function showWeighted(formId, obj) {
        var ul = $("#" + formId);
        for (var i = 0; i < obj.length; i++) {
            ul.append("<p>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + obj[i]["AnswerText"] + "</p><br />");
            var j = getChoiceByQuestioniD(obj[i]["QuestionID"], obj[i]["ChoiceID"]);
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