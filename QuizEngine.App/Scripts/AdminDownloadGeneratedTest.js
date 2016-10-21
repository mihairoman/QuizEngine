﻿$(document).ready(function () {
    //for the first table
    $("#HomeScreen").show();
    $("#tab1").addClass("current");
    var _filteredQuizzesData = [];
    var _filterInfoTable1 = [];
    var _sortExpression = "LevelName asc ";
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

    var req = new PEService();

    // populate the table
    PopulateTable1();

    $(function () {
        $('.categorytooltip').tooltip({
            track: true
        });
    });
    $(function () {
        $('.leveltooltip').tooltip({
            track: true
        });
    });
   

    $('.randomquizzesonpagedropdown').on('change', function () {
        _oldPageNr = 1;
        _pageNumber = 1;
        _pagerObject.ResetLink();
        PopulateTable1();
    });


    $('.levelNameSort').on('click', function () {

        SortTable('LevelName ', $(this));
    })
    $('.categoryNameSort').on('click', function () {

        SortTable('CategoryName ', $(this));
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
        PopulateTable1();

    }
    function GOnSuccess(response) {        
        var jsonobj = JSON.parse(JSON.parse(response).d);
        for (i in jsonobj) {
            fq = {
                QuizUID: jsonobj[i]["QuizUID"],
                LevelUID: jsonobj[i]["LevelUID"],
                CategoryUID: jsonobj[i]["CategoryUID"],
                LevelName: TrimName(jsonobj[i]["LevelName"]),
                RealLevelName: jsonobj[i]["LevelName"],
                LevelDifficulty: jsonobj[i]["LevelDifficulty"],
                CategoryName: TrimName(jsonobj[i]["CategoryName"]),
                RealCategoryName: jsonobj[i]["CategoryName"]
            }
            _filteredQuizzesData.push(fq);
        }
        $("#generatedQuizzesTemplate").tmpl(_filteredQuizzesData).appendTo("#generatedQuizzes");
        BindEventsTable1();
    }

    function GonError(msg) {
       
    }



    function GetGeneratedQuizzes() {            
        req.ExecuteCall("AdminDownloadGeneratedTestViewService.asmx", "GetRandomGeneratedQuizzes", JSON.stringify(_filterInfoTable1), GOnSuccess, GonError);
    }

    function BindEventsTable1() {

        $(document).on('pagerIndexChanged', function (event) {
            event.preventDefault();
            event.stopPropagation();

            _pageNumber = parseInt(event.onPage);
            if (_oldPageNr != _pageNumber) {
                PopulateTable1();
                _oldPageNr = _pageNumber;
                _pagerObject.Reset();
                _pagerObject.Init(_filteredQuizzesCount, _rowsPerPage, _pagerForm);
            }

        });

        _pagerObject.Reset();
        _pagerObject.Init(_filteredQuizzesCount, _rowsPerPage, _pagerForm);
        $('.downloadPDF').on('click', function (event) {
            event.preventDefault();
            var index = $(this).closest('tr').index();
            index = index - 1;
            window.location.replace("http://localhost/QuizEngineWebservice/GenerateFileHandler.ashx?FileType=0&QuizUID=" + _filteredQuizzesData[index].QuizUID+"&UserUID="+readCookie("currentUser").split("&")[1].split("=")[1]);
        });
        $('.downloadWord').on('click', function (event) {
            event.preventDefault();
            var index = $(this).closest('tr').index();
            index = index - 1;
            window.location.replace("http://localhost/QuizEngineWebservice/GenerateFileHandler.ashx?FileType=1&QuizUID=" + _filteredQuizzesData[index].QuizUID+ "&UserUID=" + readCookie("currentUser").split("&")[1].split("=")[1]);
        });
        $('.createLinkRandom').on('click', function (event) {
            event.preventDefault();
            var index = $(this).closest('tr').index();
            index = index - 1;
            window.location.replace("../Pages/AdminGenerateOnlineLink.aspx?&QuizUID=" + _filteredQuizzesData[index].QuizUID);
        });

    }

    //Populate first table

    function PopulateTable1() {
        for (var i = document.getElementById("generatedQuizzes").rows.length; i > 1; i--) {
            document.getElementById("generatedQuizzes").deleteRow(i - 1);
        }
        _rowsPerPage = $('.randomquizzesonpagedropdown').find($('option:selected')).text();
        _filterInfoTable1 = [];
        _filteredQuizzesData = [];
        _filterInfoTable1 = {
            QuizUID: JSON.stringify(),
            LevelUID: JSON.stringify(),
            CategoryUID: JSON.stringify(),
            LevelName: JSON.stringify(),
            LevelDifficulty: JSON.stringify(),
            CategoryName: JSON.stringify(),
            sortExpression: JSON.stringify(_sortExpression),
            rowsPerPage: JSON.stringify(_rowsPerPage),
            pageNumber: JSON.stringify(_pageNumber)
        };
        CountGeneratedQuizzesWithAjax();
       
    }

    function CountGeneratedQuizzesWithAjaxOnSuccess(response) {
        _filteredQuizzesCount = JSON.parse(JSON.parse(response).d);
        _pagerObject.Reset();
        _pagerObject.Init(_filteredQuizzesCount, _rowsPerPage, _pagerForm);
        GetGeneratedQuizzes();
    }

    function CountGeneratedQuizzesWithAjaxonError(msg) {

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
    function CountGeneratedQuizzesWithAjax() {
        req.ExecuteCall("AdminDownloadGeneratedTestViewService.asmx", "CountAllRandomGeneratedQuizzes", JSON.stringify(_filterInfoTable1), CountGeneratedQuizzesWithAjaxOnSuccess, CountGeneratedQuizzesWithAjaxonError);
    }

    function TrimName(text) {
        var valtext = text;
        if (valtext.length >= 15) {
            valtext = text.substring(0, 14) + "...";
        }
        return valtext;
    }
});