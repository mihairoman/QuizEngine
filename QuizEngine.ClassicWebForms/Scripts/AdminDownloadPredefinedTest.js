﻿$(document).ready(function () {
    $("#HomeScreen").show();
    $("#tab1").addClass("current");
    //for the second table
    var _filteredPredefinedQuizzes = [];
    var _filterInfoTable2 = [];

    var _sortExpressionPredefined = " qt.TypeName asc ";
    var _sortDirPredefined = 0;
    // the old page Number is used in order to rpevent displaying the same content on the page serveral times
    var _oldPageNrPredefined = 1;
    // the actual number of the page
    var _pageNumberPredefined = 1;
    // specifies how many information should be on the page, it has a default value 5
    var _rowsPerPagePredefined = 1;
    // popualte the initial table
    var _scorQuizesPagerPredefined = 1;
    // used to return the pager object
    var _pagerObjectPredefined = new PagerControl();
    // the form of the pager
    var _pagerFormPredefined = 'pager';
    // this one is used to get the count of the fitlered questions
    var _filteredQuizzesCountPredefined;
    // old sort text helps when desiring to sort another  column in order to aleays start from ascending order
    var _oldSortTextPredefined;

    // popualte the tables

    PopulateTable2();

   

    $('.predefinedquizzesonpagedropdown').on('change', function () {
        _oldPageNrPredefined = 1;
        _pageNumberPredefined = 1;
        _pagerObjectPredefined.ResetLink();
        PopulateTable2();
    });
  
    $('.typeNameSort').on('click', function () {

        SortTable2('TypeName ', $(this));
    })

    
   

    function SortTable2(sortText2, form2) {

        event.preventDefault();
        event.stopPropagation();

        _oldPageNrPredefined = 1;
        _pageNumberPredefined = 1;

        if (_oldSortTextPredefined != sortText2) { _sortDirPredefined = 0; }

        if (_sortDirPredefined == 0) {
            $('.arrowimg').remove();
            form2.append('<img src="../Images/up_arrow.png"  class="arrowimg" style="float:right;"/>');
            _sortExpressionPredefined = sortText2 + ' asc';
            _sortDirPredefined = 1;
        } else if (_sortDirPredefined == 1) {
            $('.arrowimg').remove();
            form2.append('<img src="../Images/down_arrow.png" class="arrowimg" style="float:right;"/>');
            _sortExpressionPredefined = sortText2 + ' desc';
            _sortDirPredefined = 0;
        }
        _oldSortTextPredefined = sortText2;
        _pagerObjectPredefined.ResetLink();
        PopulateTable2();

    }


    //functions for the second table
    function GetPredefinedGeneratedQuizzes() {
        $.ajax({
            type: "POST",
            url: " ../Webservices/AdminDownloadGeneratedTestViewService.asmx/GetPredefinedQuizzes",
            data: JSON.stringify(_filterInfoTable2),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                var jsonostr = response.d;
                var jsonobj = JSON.parse(jsonostr);
                for (i in jsonobj) {
                    fq = {
                        QuizUID: jsonobj[i]["QuizUID"],
                        TypeName: TrimName(jsonobj[i]["TypeName"]),
                        RealTypeName: jsonobj[i]["TypeName"]
                    }
                    _filteredPredefinedQuizzes.push(fq);
                }
                $("#predefinedQuizzesTemplate").tmpl(_filteredPredefinedQuizzes).appendTo("#predefinedQuizzes");
                BindEventsTable2();
            },
            error: function (msg) {
                $("#lblMessagePredefinedTest").text(AdminDownloadPredefinedTest.ErrorDownloadingOrGenerating);
            },
        });//end ajax
    }

    function BindEventsTable2() {

        $(document).on('pagerIndexChanged', function (event) {
            event.preventDefault();
            event.stopPropagation();

            _pageNumberPredefined = parseInt(event.onPage);
            if (_oldPageNrPredefined != _pageNumberPredefined) {
                PopulateTable2();
                _oldPageNrPredefined = _pageNumberPredefined;
                _pagerObjectPredefined.Reset();
                _pagerObjectPredefined.Init(_filteredQuizzesCountPredefined, _rowsPerPagePredefined, _pagerFormPredefined);
            }

        });

        _pagerObjectPredefined.Reset();
        _pagerObjectPredefined.Init(_filteredQuizzesCountPredefined, _rowsPerPagePredefined, _pagerFormPredefined);
        $('.downloadPDFPredefined').on('click', function (event) {
            event.preventDefault();
            var index = $(this).closest('tr').index();
            index = index - 1;
            window.location.href = "../Pages/GenerateFileHandler.ashx?FileType=0&QuizUID=" + _filteredPredefinedQuizzes[index].QuizUID;
        });
        $('.downloadWordPredefined').on('click', function (event) {
            event.preventDefault();
            var index = $(this).closest('tr').index();
            index = index - 1;
            window.location.href = "../Pages/GenerateFileHandler.ashx?FileType=1&QuizUID=" + _filteredPredefinedQuizzes[index].QuizUID;
        });
        $('.createLinkPredefined').on('click', function (event) {
            event.preventDefault();
            var index = $(this).closest('tr').index();
            index = index - 1;
            window.location.replace("../Pages/AdminGenerateOnlineLink.aspx?&QuizUID=" + _filteredPredefinedQuizzes[index].QuizUID);
        });

    }

    //Populate first table

    function PopulateTable2() {
        for (var i = document.getElementById("predefinedQuizzes").rows.length; i > 1; i--) {
            document.getElementById("predefinedQuizzes").deleteRow(i - 1);
        }
        _rowsPerPagePredefined = $('.predefinedquizzesonpagedropdown').find($('option:selected')).text();
        _filterInfoTable2 = [];
        _filteredPredefinedQuizzes = [];
        _filterInfoTable2 = {
            QuizUID: JSON.stringify(),
            TypeName: JSON.stringify(),
            sortExpressionPredefined: JSON.stringify(_sortExpressionPredefined),
            rowsPerPagePredefined: JSON.stringify(_rowsPerPagePredefined),
            pageNumberPredefined: JSON.stringify(_pageNumberPredefined)
        };
        CountPredefinedQuizzesWithAjax();
        GetPredefinedGeneratedQuizzes();
    }

    function CountPredefinedQuizzesWithAjax() {
        $.ajax({
            type: "POST",
            url: "../Webservices/AdminDownloadGeneratedTestViewService.asmx/CountAllPredefinedQuizzes",
            data: "{ }",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                _filteredQuizzesCountPredefined = response.d;
                _pagerObjectPredefined.Reset();
                _pagerObjectPredefined.Init(_filteredQuizzesCountPredefined, _rowsPerPagePredefined, _pagerFormPredefined);
            }
        });//end ajax
    }
    function TrimName(text) {
        var valtext = text;
        if (valtext.length >= 15) {
            valtext = text.substring(0, 14) + "...";
        }
        return valtext;
    }

});