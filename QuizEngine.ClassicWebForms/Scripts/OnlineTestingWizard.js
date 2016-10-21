////////////////////////////////////////////
/////////////////////////VARIABLES & OBJECTS
var buttons;
var quizID;
var userID;
var userType;
var choices;
var questionNr;
var quizQuestions;
var wizardAnswers = []; //list of WizardAnswer and WizardQuestion "objects" (each WizardQuestion has a list of WizardAnswer)
var answersList = [];   //list of all the answered choices
var quizBody;
var quizTime;
var hours, minutes, seconds;

var WizardAnswer = function (QuestionID, ChoiceID) {
    this.QuestionID = QuestionID;
    this.ChoiceID = ChoiceID;
};

var WizardQuestion = function (QuestionID) {
    this.QuestionID = QuestionID;
};

//adds fields to WizardAnswer and WizardQuestion "objects"
function setupObjects() {
    WizardAnswer.prototype.AnswerText = "";
    WizardAnswer.prototype.IsChecked = false;
    WizardQuestion.prototype.Answers = [];
};

//////////////////////////////////////////
/////////////////////////////MAIN FUNCTION
function main(jsonString, quizId, userId) {
    questionNr = 0;
    
    quizID = quizId;
    userID = userId;
    userType = document.cookie.split('&')[2].split('=')[1];
    quizQuestions = JSON.parse(jsonString);
    buttons = $(".buttons");
    choices = $(".choices");
    questions = $(".questions");
    quizBody = $("#quizBody");
    displayTimer();
    buttons.find('input').hover(function () {
        $(this).toggleClass("ui-state-hover");
    });

    setupObjects();
    initAnswers();
    displayQuestionList();
    displayQuestion();
    getBtnState();
    onClickNextBtn();
    onClickPreviousBtn();
    onClickSubmit();

};
/////////////////////////////////////////////////////////
///////////////////////////////////////////////FUNCTIONS

//Initializesc the wizardAnswers array, in which the info from quizQuestions
//and the state of each question and its related choices are stored.
//The IsChecked property of all the FreeText choices is by default set to true
function initAnswers() {
    for (var k = 0; k < quizQuestions.length; k++) {
        var wzQuestion = new WizardQuestion(quizQuestions[k].QuestionID);
        wzQuestion.Answers = [];
        for (var i = 0; i < quizQuestions[k].Choices.length; i++) {
            var tempWzAnswer = new WizardAnswer(quizQuestions[k].QuestionID, quizQuestions[k].Choices[i].ChoiceID);
            if (quizQuestions[k].QuestionType == QuestionType.FreeText) {
                tempWzAnswer.IsChecked = true;
            }
            wzQuestion.Answers.push(tempWzAnswer);
        }
        wizardAnswers.push(wzQuestion);
    }
};

function onClickNextBtn() {
    buttons.find("#btnNext").on("click", function () {
        isChecked();
        questionNr++;
        displayQuestion();
        getBtnState();
    });
};

function onClickPreviousBtn() {
    buttons.find("#btnPrev").on("click", function () {
        isChecked();
        questionNr--;
        displayQuestion();
        getBtnState();
    });
};

function onClickSubmit() {
    buttons.find("#btnSubmit").on("click", function () {
        var pop = confirm(OnlineWizardResources.SubmissionMessage);
        if (pop == true) {
            isChecked();
            getAnswersList();
            var tempAnwers = JSON.stringify(answersList);
            sendResponse(tempAnwers);
        }
    });
};

//Displays and hides the buttons depending on the question nr
function getBtnState() {
    if (questionNr > 0) {
        buttons.find("#btnPrev").show();
    }
    else {
        buttons.find("#btnPrev").hide();
    }

    if (questionNr == quizQuestions.length - 1) {
        buttons.find("#btnNext").hide();
        buttons.find("#btnSubmit").show();
    }
    else {
        buttons.find("#btnNext").show();
        buttons.find("#btnSubmit").hide();
    }
};


//Highlights the current question in the menu
function highlightCurrentQuestion() {
    $(".questionsList").children().removeClass("highlighted");
    $(".questionsList").children().find('div').removeClass("highlighted2");
    $(".questionsList").find("#question" + questionNr).addClass("highlighted");
    $(".questionsList").find("#text" + questionNr).addClass("highlighted2");
}

//Displays the current question and calls the displayChoices functions
function displayQuestion() {
    highlightCurrentQuestion()
    questions.empty();
    var questionText = $('<p id="questionText">' + quizQuestions[questionNr].Text + '</p>');
    questions.append(questionText);
    displayChoice(quizQuestions[questionNr]);
};

//Dsiplays the choices of the current questions
function displayChoice(question) {
    choices.empty();
    for (var i = 0; i < question.Choices.length; i++) {
        var item = $('<div></div>');
        var element = createElementOfType(question, question.Choices[i], i);
        $(item).append(element);
        choices.append(item);
    }
};

//Creates a choice (element) depending on the type of the current question
function createElementOfType(question, choice, choiceIndex) {
    var element;
    var checkedState = wizardAnswers[questionNr].Answers[choiceIndex].IsChecked;
    var answerText = wizardAnswers[questionNr].Answers[choiceIndex].AnswerText;
    if (answerText == "No answer")
        answerText = "";

    switch (question.QuestionType) {
        case QuestionType.FreeText:
            element = $('<textarea style="width: 90%; height: 200px" id="' + choice.ChoiceID + '"></textarea>');
            element.val(answerText);
            break;
        case QuestionType.MultiChoice:
            element = $('<input type="checkbox" id="' + choice.ChoiceID + '">' + choice.AnswerText + '</input>');
            element.prop('checked', checkedState);
            break;
        case QuestionType.Single:
            element = $('<input type="radio" id="' + choice.ChoiceID + '">' + choice.AnswerText + '</input>');
            $(element).attr('name', question.QuestionID);
            element.prop('checked', checkedState);
            break;
        case QuestionType.TrueFalse:
            var radioTrue = $('<input type="radio" value="true">' + "True" + '</input>');
            $(radioTrue).attr('name', 'trueFalse');
            var radioFalse = $('<input type="radio" value="false">' + "False" + '</input>');
            $(radioFalse).attr('name', 'trueFalse');
            element = $('<div id="' + choice.ChoiceID + '"></div>');
            $(element).append(radioTrue);
            $(element).append(radioFalse);
            element.find('input[value="' + answerText + '"]').prop('checked', answerText);
            break;
        case QuestionType.Weighted:
            element = $('<input type="radio" id="' + choice.ChoiceID + '">' + choice.AnswerText + '</input>');
            $(element).attr('name', question.QuestionID);
            element.prop('checked', checkedState);
            break;
    }
    return element;
};

//Checks if the current choices are selected or not and saves their state in the wizardAnswers array.
//FreeTexts are checked by default
function isChecked() {
    var answerText = $.trim(choices.find('textarea').val());
    var encodedText = choices.find('textarea').text(answerText).html();
    switch (quizQuestions[questionNr].QuestionType) {
        case QuestionType.FreeText:
            if (encodedText != "")
                wizardAnswers[questionNr].Answers[0].AnswerText = encodedText;
            else
                wizardAnswers[questionNr].Answers[0].AnswerText = "No answer";
            break;
        case QuestionType.TrueFalse:
            if (((quizQuestions[questionNr].Choices[0].IsCorrect === true) &&
                    (choices.find('input:checked').val() === "true")) ||
                    ((quizQuestions[questionNr].Choices[0].IsCorrect === false) &&
                    (choices.find('input:checked').val() === "false"))) {
                wizardAnswers[questionNr].Answers[0].Grade = 1;
                wizardAnswers[questionNr].Answers[0].IsChecked = true;
                wizardAnswers[questionNr].Answers[0].AnswerText = choices.find('input:checked').val();
            }
            else
                if ((choices.find('input:checked').val() === "true") ||
                     (choices.find('input:checked').val() === "false")) {
                    wizardAnswers[questionNr].Answers[0].Grade = 0;
                    wizardAnswers[questionNr].Answers[0].IsChecked = true;
                    wizardAnswers[questionNr].Answers[0].AnswerText = choices.find('input:checked').val();
                }
            break;
        case QuestionType.MultiChoice:
        case QuestionType.Single:
        case QuestionType.Weighted:
            choices.find('input').each(function (index) {
                wizardAnswers[questionNr].Answers[index].IsChecked = $(this).prop("checked");
            });
            break;
    }
};

//Searches for all the checked choices in the wizardAnswers and adds them in the answersList
function getAnswersList() {
    for (var question in wizardAnswers) {
        if (wizardAnswers.hasOwnProperty(question)) {
            for (var wzAnswer in wizardAnswers[question].Answers) {
                if (wizardAnswers[question].Answers.hasOwnProperty(wzAnswer)) {
                    if (wizardAnswers[question].Answers[wzAnswer].IsChecked) {
                        var Answer = {
                            QuizUID: quizID,
                            ArchivedQuestionUID: wizardAnswers[question].QuestionID,
                            Grade: wizardAnswers[question].Answers[wzAnswer].Grade !== undefined ? wizardAnswers[question].Answers[wzAnswer].Grade : 0,
                            AnswerText: wizardAnswers[question].Answers[wzAnswer].AnswerText,
                            UserUID: userID,
                            ArchivedChoiceUID: wizardAnswers[question].Answers[wzAnswer].ChoiceID
                        };
                        answersList.push(Answer);
                    }
                }
            }
        }
    }
};

//Checks if the quiz has free texts or not
function hasFreeText() {
    for (var i = 0; i < quizQuestions.length; i++) {
        if (quizQuestions[i].QuestionType === QuestionType.FreeText)
            return true;
    }
    return false;
}

function sendResponse(transmitJsonString) {
    var jsonObj =
        {
            transmitJsonString: transmitJsonString,
            hasFreeText: hasFreeText(),
            quizId: quizID,
            userId: userID,
            questionsNr: quizQuestions.length
        }
    $.ajax
        ({
            type: "POST",
            url: "../Webservices/OnlineTestingWizardService.asmx/DeserializeJsonString",
            data: JSON.stringify(jsonObj),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                quizBody.empty();
                location.replace("../Pages/GoodbyePage.aspx?Response=" + response.d + "&UserType=" + userType);
            },
            error: function (msg) {
            }
        });
}
//Displays the questions menu. Listens for the click event on the list items
function displayQuestionList() {
    for (var i = 0; i < quizQuestions.length; i++) {
        $(".questionsList").append($('<li id="question' + i + '" data-index="' + i + '"></li>'));
        var currentQuestion = $(".questionsList").find("#question" + i);
        currentQuestion.append($('<span id="span' + i + '">' + OnlineWizardResources.ListQuestion + " " + (i + 1) + '</span>'));
        currentQuestion.find('#span' + i).append($('<div id="text' + i + '" class="v-nav-description">' + quizQuestions[i].Text.substring(0, 21) + "..." + '</div>'));
    }
    $(".questionsList").show();

    $("#wrapper").find(".questionsList").on('click', 'li', function () {
        isChecked();
        var index = $(this).data("index");
        questionNr = index;
        displayQuestion();
        getBtnState();
    });
}

function GetQueryStringParam(key) {
    key = key.replace(/[*+?^$.\[\]{}()|\\\/]/g, "\\$&"); // escape RegEx meta chars
    var match = location.search.match(new RegExp("[?&]" + key + "=([^&]+)(&|$)"));
    return match && decodeURIComponent(match[1].replace(/\+/g, " "));

};

function getObjects(quizID, userID) {
    var obj = {
        quizId: quizID,
        userId: userID
    };
    $.ajax
    ({
        type: "POST",
        url: "../Webservices/OnlineTestingWizardService.asmx/GetSerializedObject",
        data: JSON.stringify(obj),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            var jsonString = response;
            var jsFunction = new main(jsonString.d, quizID, userID);
        },
        error: function (msg) {
        },
    });
}

$(document).ready(function () {
    $('#cssmenu').hide();
    $("#btnPrev").val(OnlineWizardResources.PreviousButton);
    $("#btnNext").val(OnlineWizardResources.NextButton);
    $("#btnSubmit").val(OnlineWizardResources.SubmitButton);
    $("#v-nav").children('ul').empty();
    $("#v-nav").prepend('<ul class="questionsList" style="display:none"></ul>');
    var quizID = GetQueryStringParam('QuizUID');
    var userID = GetQueryStringParam('UserUID');
    quizTime = GetQueryStringParam('QuizTime');
    if ((typeof quizID !== 'undefined') && (typeof userID !== 'undefined')) {
        getObjects(quizID, userID);
    }
});


function displayTimer() {
    $("#timer").append('<span id="countdown"></span>');
    hours = parseInt(quizTime.split(":")[0]);
    minutes = parseInt(quizTime.split(":")[1]);
    seconds = parseInt(quizTime.split(":")[2]);
    var totalSeconds = parseInt(hours) * 3600 + parseInt(minutes) * 60 + parseInt(seconds);
    setInterval(function () {
        $("#countdown").text(hours+":"+minutes+":"+seconds);
        minutes--;
    }, 1000);

}