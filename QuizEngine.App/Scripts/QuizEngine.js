QuestionType = {
    //key    QuestionType[key]
    FreeText: 0,
    MultiChoice: 1,
    Single: 2,
    TrueFalse: 3,
    Weighted: 4

}

PageMode = {
    //key    QuestionType[key]
    View: 0,
    Insert: 1,
    Update: 2
}

UserType = {
    ClassicUser: 0,
    Administrator: 1,
    Candidate: 2
}

var emptyGUID = "00000000-0000-0000-0000-000000000000";

function GetFromStringQuestionType(questionString) {
    for (var key in QuestionType) {
        if (QuestionType.hasOwnProperty(key)) {
            if (questionString == key) { return QuestionType[key]; }
        }
    }
}

function GetFromIntQuestionType(questionNr) {
    for (var key in QuestionType) {
        if (QuestionType.hasOwnProperty(key)) {
            if (questionNr == QuestionType[key]) { return key; }
        }
    }
}

var GenerateGuid = (function () {
    function s4() {
        return Math.floor((1 + Math.random()) * 0x10000)
                   .toString(16)
                   .substring(1);
    }
    return function () {
        return s4() + s4() + '-' + s4() + '-' + s4() + '-' +
               s4() + '-' + s4() + s4() + s4();
    };
})();

/// Returns QueryString parameter that have been requested
function GetQueryStringParam(key) {
    key = key.replace(/[*+?^$.\[\]{}()|\\\/]/g, "\\$&"); // escape RegEx meta chars
    var match = location.search.match(new RegExp("[?&]" + key + "=([^&]+)(&|$)"));
    return match && decodeURIComponent(match[1].replace(/\+/g, " "));
};
