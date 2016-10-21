$(document).ready(function () {
    $("#Quiz").show();
    templateQuizPager = new PagerControl();
    templateQuizes = [];
    var _currentPageNumber = 1;
    var _clickedPageNumber = -1;
    var _templateQuizNumber = 0;
    var _quizPerPage = 5;
    var _sortExpression = null;
    var _sortDirByQuizName = 1;
    var _sortDirByQuestionNr = 1;
    var quizTemplateInfo = {
        userID: null,
        pageNumber: 1,
        quizTemplatePerPage: 5,
        sortExpression: null
    };
    populateTable();
    function populateTable() {
        deleteTableRows();
        templateQuizPager.Reset();
        getQuizTemplateNr();
        bindEvents();
    }

    function readCookie(name) {
        var nameEQ = name + "=";
        var ca = document.cookie.split(';');
        for (var i = 0; i < ca.length; i++) {
            var c = ca[i];
            while (c.charAt(0) == ' ') c = c.substring(1, c.length);
            if (c.indexOf(nameEQ) == 0) return c.substring(nameEQ.length, c.length);
        }
        return null;
    }
    
    function getQuizTemplateNr() {
        var _userID = {
            userGuid:  readCookie("currentUser").split('&')[1].split('=')[1]
        };
        $.ajax({
            type: "POST",
            url: "../Webservices/QuizViewListingService.asmx/CountAllTemplateQuizes",
            data: JSON.stringify(_userID),
            contentType: "application/json; charset=utf-8",
            dataType: "json", //the return data type
            success: function (response) {
                _templateQuizNumber = response.d;
                templateQuizPager.Init(_templateQuizNumber, _quizPerPage, 'pager');
                getQuizTemplates();
            }
        }); //end ajax
    }

    function getQuizTemplates() {
        templateQuizes = [];
        quizTemplateInfo["userID"] = readCookie("currentUser").split('&')[1].split('=')[1];
        quizTemplateInfo["pageNumber"] = _currentPageNumber;
        quizTemplateInfo["quizTemplatePerPage"] = _quizPerPage;
        quizTemplateInfo["sortExpression"] = _sortExpression;
        $.ajax({
            type: "POST",
            url: "../Webservices/QuizViewListingService.asmx/ReadAllTemplateQuizes",
            data: JSON.stringify(quizTemplateInfo),
            contentType: "application/json; charset=utf-8",
            dataType: "json", //the return data type
            success: function (response) {
                var jsonostr = response.d; //in response I have the data from webservice
                var jsonobj = eval('(' + jsonostr + ')');
                for (i in jsonobj) {
                    qt = {
                        QuizName: TrimName(jsonobj[i]["QuizTemplateName"]),
                        RealQuizName: jsonobj[i]["QuizTemplateName"],
                        QuizGUID: jsonobj[i]["QuizTemplateUID"],
                        QuestionNumber: jsonobj[i]["QuestionNumber"]
                    };
                    templateQuizes.push(qt);
                }
                $("#templateQuizPopulate").tmpl(templateQuizes).appendTo("#templateQuizTable");
                $(".startQuizBtn").on('click', function () {
                  
                    var URL = "../Pages/OnlineTestingWizard.aspx?QuizUID=" + this.id.toString() + "&UserUID=" + readCookie("currentUser").split("&")[1].split("=")[1];
                    location.replace(URL);
                });
            },
            error: function () {
                alert("error occured");
            }
        });//end ajax
        
        }

    function bindEvents() {
        $(document).on('pagerIndexChanged', function (event) {
            event.preventDefault();
            event.stopPropagation();
            _clickedPageNumber = parseInt(event.onPage);
            if (_currentPageNumber != _clickedPageNumber) {
                _currentPageNumber = _clickedPageNumber;
                populateTable();
            }
    });

    }
    
    function deleteTableRows() {
        for (var i = 0; i < templateQuizes.length; i++) {
            $("#templateQuizTable").find("tr:gt(" + i + ")").remove();
        }
    }

    $("#quizPerPageDropDown").on('change', function (){
        _quizPerPage = $("#quizPerPageDropDown").find($('option:selected')).text();
        populateTable();
        })

    $(".quizNameSort").on('click', function () {
        _sortExpression = " dbo.QuizTemplates.TypeName ";
        _sortDirByQuizName *= (-1);
        sortTable(_sortDirByQuizName,this);

    });

    $(".questionNrSort").on('click', function () {
        _sortExpression = " QuestionNumber ";
        _sortDirByQuestionNr *= (-1);
        sortTable(_sortDirByQuestionNr,this);
        });

    function sortTable(value, form) {
        $('.arrowimg').remove();
        if (value == 1) {
            _sortExpression += "ASC ";
            $(form).append('<img src="../Images/down_arrow.png" class="arrowimg" style="float:right;">');
        } else {
            _sortExpression += "DESC ";
            $(form).append('<img src="../Images/up_arrow.png"  class="arrowimg" style="float:right;">');
        }
        populateTable();
        }
    function TrimName(text) {
        var valtext = text;
        if (valtext.length >= 35) {
            valtext = text.substring(0, 34) + "...";
        }
        return valtext;
    }

});