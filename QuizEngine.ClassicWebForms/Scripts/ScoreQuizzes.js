$(document).ready(function () {

    
    $("#HomeScreen").show();
  
    $("#tab1").addClass("current");
    var _filteredQuizzes = [];
    var _filterInfo = [];
    var _sortExpression = 'UserName asc';
    var _sortDir = 0;
    // the old page Number is used in order to rpevent displaying the same content on the page serveral times
    var _oldPageNr = 1;
    // the actual number of the page
    var _pageNumber = 1;
    // specifies how many information should be on the page, it has a default value 5
    var _rowsPerPage = 1;
    // popualte the initial table
    var _scorQuizesPager = 1;
    // used to return the pager object
    var _pagerObject = new PagerControl();
    // the form of the pager
    var _pagerForm = 'pager';
    // this one is used to get the count of the fitlered questions
    var _filteredQuizzesCount;
    // old sort text helps when desiring to sort another  column in order to aleays start from ascending order
    var _oldSortText;

    PopulateTable();
    $('.quizzesonpagedropdown').on('change', function () {
        _oldPageNr = 1;
        _pageNumber = 1;
        _pagerObject.ResetLink();
        PopulateTable();
    });

    $('.userNameSort').on('click', function () {

        SortTable('Users.UserName', $(this));
    })
    $('.quizDateSort').on('click', function () {

        SortTable('QuizDate', $(this));
    })
    $('.levelNameSort').on('click', function () {

        SortTable('LevelName', $(this));
    })
    $('.categoryNameSort').on('click', function () {

        SortTable('CategoryName', $(this));
    })

    function SortTable(sortText, form) {

    
        event.preventDefault();
        event.stopPropagation();

        _oldPageNr = 1;
        _pageNumber = 1;

        if (_oldSortText != sortText) { _sortDir = 0; }

        if (_sortDir == 0) {
            $('.arrowimg').remove();
            form.append('<img src="../Images/up_arrow.png"  class="arrowimg" style="float:right;"/>');
            _sortExpression = sortText + ' asc';
            _sortDir = 1;
        } else if (_sortDir == 1) {
            $('.arrowimg').remove();
            form.append('<img src="../Images/down_arrow.png" class="arrowimg" style="float:right;"/>');
            _sortExpression = sortText + ' desc';
            _sortDir = 0;
        }
        _oldSortText = sortText;
        _pagerObject.ResetLink();
        PopulateTable();

    }


    function GetFreeTextQuizes() {

        $.ajax({
            type: "POST",
            url: " ../Webservices/FreeTextQuizViewService.asmx/GetFreeTextQuizes",
            data: JSON.stringify(_filterInfo),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                var jsonostr = response.d;
                var jsonobj = eval('(' + jsonostr + ')');
                for (i in jsonobj) {
                    fq = {
                        UserUID: jsonobj[i]["UserUID"],
                        QuizUID: jsonobj[i]["QuizUID"],
                        UserName: TrimText(jsonobj[i]["UserName"]),
                        RealUserName: jsonobj[i]["UserName"],
                        QuizDate: new Date(parseInt(jsonobj[i]["QuizDate"].toString().substring(6), 10)),
                        QuizDateForUI: (new Date(parseInt(jsonobj[i]["QuizDate"].toString().substring(6), 10))).toString().substring(0, 25),
                        CategoryName: TrimName(jsonobj[i]["CategoryName"]),
                        RealCategoryName: jsonobj[i]["CategoryName"],
                        LevelName: TrimName(jsonobj[i]["LevelName"]),
                        RealLevelName: jsonobj[i]["LevelName"]
                    }
                    _filteredQuizzes.push(fq);
                }
                $("#validationTemplate").tmpl(_filteredQuizzes).appendTo("#quizzesValidation");
                $('.correctBtn').html(ScoreQuizResources.Correct);
                BindEvents();
               

            }
        });//end ajax
    }

  function BindEvents() {
    $(document).on('pagerIndexChanged', function (event) {
        event.preventDefault();
        event.stopPropagation();

        _pageNumber = parseInt(event.onPage);
        if (_oldPageNr != _pageNumber) {
            PopulateTable();
            _oldPageNr = _pageNumber;
            _pagerObject.Reset();
            _pagerObject.Init(_filteredQuizzesCount, _rowsPerPage, _pagerForm);
        }

    });

    _pagerObject.Reset();
    _pagerObject.Init(_filteredQuizzesCount, _rowsPerPage, _pagerForm);
    $(".correctBtn").click(function (e) {
        e.preventDefault();
        var index = $(this).closest('tr').index();
        index = index - 1;
        var URL = "FreeTextQuestionsPendingAdminResponse.aspx?&QuizUID=" + _filteredQuizzes[index].QuizUID + "&UserUID=" + _filteredQuizzes[index].UserUID; //format the URL
        window.location.href = URL; //make the redirect 
    });

   }

    

    //Populate table

    function PopulateTable() {
        for (var i = document.getElementById("quizzesValidation").rows.length; i > 1; i--) {
            document.getElementById("quizzesValidation").deleteRow(i - 1);
        }
        _rowsPerPage = $('.quizzesonpagedropdown').find($('option:selected')).text();
        _filterInfo = [];
        _filteredQuizzes = [];
        _filterInfo = {
            UserUID: JSON.stringify(),
            QuizUID: JSON.stringify(),
            UserName: JSON.stringify(),
            QuizDate: JSON.stringify(),
            CategoryName: JSON.stringify(),
            LevelName: JSON.stringify(),
            sortExpression: JSON.stringify(_sortExpression),
            rowsPerPage: JSON.stringify(_rowsPerPage),
            pageNumber: JSON.stringify(_pageNumber)

        };
        CountFilteredQuizzesWithAjax();
        GetFreeTextQuizes();


    }
    
    function CountFilteredQuizzesWithAjax() {

    

        $.ajax({
            type: "POST",
            url: "../Webservices/FreeTextQuizViewService.asmx/CountAllFilteredQuizzes",
            data:"{ }",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                _filteredQuizzesCount = response.d;
                _pagerObject.Reset();
                _pagerObject.Init(_filteredQuizzesCount, _rowsPerPage, _pagerForm);
            }
        });//end ajax

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

    if (readCookie("currentUser").split("&")[2].split("=")[1] != 1) {
        $("#scoretabel").hide();

    }

    function TrimText(text) {
        var valtext = text;
        if (valtext.length >= 21) {
            valtext = text.substring(0, 20) + "...";
        }
        return valtext;
    }
    function TrimName(text) {
        var valtext = text;
        if (valtext.length >=15) {
            valtext = text.substring(0, 14) + "...";
        }
        return valtext;
    }
});