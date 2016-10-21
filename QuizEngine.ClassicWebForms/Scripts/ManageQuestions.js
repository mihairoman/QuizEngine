$(document).ready(function () {

    $("#Manage").show();

    $(".textsort").append('<img src="../Images/down_arrow.png" class="arrowimg" style="float:right;"/>');

    // used to store all the retrieved filtered questions
    var _filteredQuestion = [];
    // used to store the fitler information in order to filter the questions
    var _filterInfo = [];

    // The next 5 represent the filter criteria and get a default value
    var _selectedCategory = '00000000-0000-0000-0000-000000000000';
    var _selectedLevel = '00000000-0000-0000-0000-000000000000';
    var _allGuidTags = new Array();
    var _allQuestionStringTypes = new Array();
    var _sortExpression = 'q.Text asc';

    // the sort direction specifies if it is ascending or descending
    var _sortDir = 0;
    // old sort text helps when desiring to sort another  column in order to aleays start from ascending order
    var _oldSortText;
    // the old page Number is used in order to rpevent displaying the same content on the page serveral times
    var _oldPageNr = 1;
    // the actual number of the page
    var _pageNumber = 1;
    // specifies how many information should be on the page, it has a default value 5
    var _rowsPerPage = 5;
    // this one is used to get the count of the fitlered questions
    var _filteredQuestionsCount;
    // the form of the pager
    var _pagerForm = 'questionpager';
    // used to retrieve the name of the Tag swapper object
    var _tagSwapperObject = new SwapperControl();
    _tagSwapperObject.Init('tagswapper', '../Webservices/QuestionFilteringService.asmx/GetAllTags');
    // used tor eturn the question type object
    var _questionTypeSwapperObject = new SwapperControl();
    _questionTypeSwapperObject.Init('questiontypeswapper', '../Webservices/QuestionFilteringService.asmx/GetAllQuestionTypes');
    // used to return the pager object
    var _pagerObject = new PagerControl();
    // slides down and up the fitler section

    // popualte the initial table
    PopulateTable();

    // create the filters

    //The level drop down list
    var levelDropDownList = new DropDownLists();
    levelDropDownList.Init("leveldropdown", "../Webservices/QuestionFilteringService.asmx/GetAllLevels");

    // the category drop down list
    var categoryDropDownList = new DropDownLists();
    categoryDropDownList.Init("categorydropdown", "../Webservices/QuestionFilteringService.asmx/GetAllCategories");

    // this event is for the dropdown lsit that decides how many items should be on the page
    $('.questionsonpagedropdown').on('change', function () {
        _oldPageNr = 1;
        _pageNumber = 1;
        _pagerObject.ResetLink();
        PopulateTable();
    });

    // reset filter event set all the variables to the default value
    $(".resetallfilters").on('click', function () {
        _oldPageNr = 1;
        _pageNumber = 1;
        $(".leveldropdown option:first-child").attr("selected", "selected");
        $(".categorydropdown option:first-child").attr("selected", "selected");
        _tagSwapperObject.Reset();
        _questionTypeSwapperObject.Reset();
        _selectedCategory = '00000000-0000-0000-0000-000000000000';
        _selectedLevel = '00000000-0000-0000-0000-000000000000';
        _allGuidTags = new Array();
        _allQuestionStringTypes = new Array();
        _sortExpression = 'q.Text asc';
        _pagerObject.ResetLink();
        PopulateTable();


    });
    // listener for filter event
    // when pressing the filter button this function will gather all the neccesary information in order to execute the filter
    $(".filterall").on('click', function () {
        _oldPageNr = 1;
        _pageNumber = 1;
        _selectedLevel = levelDropDownList.Values();
        if (_selectedLevel == "none") {
            _selectedLevel = '00000000-0000-0000-0000-000000000000';
        }
        _selectedCategory = categoryDropDownList.Values();
        if (_selectedCategory == "none") {
            _selectedCategory = '00000000-0000-0000-0000-000000000000';
        }

        var allTags = _tagSwapperObject.Values();
        _allGuidTags = new Array();

        for (var i = 0; i < allTags.length; i++) {
            _allGuidTags.push(allTags[i].OptionGUID);
        }

        var allQuestionTypes = _questionTypeSwapperObject.Texts();
        _allQuestionStringTypes = new Array();

        for (var i = 0; i < allQuestionTypes.length; i++) {
            _allQuestionStringTypes.push(allQuestionTypes[i].OptionName);
        }
        _pagerObject.ResetLink();
        PopulateTable();
    });

    // Inset button event listener
    $(".insertquestion").on('click', function (event) {
        event.preventDefault();
        event.stopPropagation();
        $("#QuestionTextBox").removeClass("incorrectfield");
    	$("#questionLevelDropDownList").removeClass("incorrectfield");
		$("#questionCategoryDropDownList").removeClass("incorrectfield");
		$("#tagsInput").removeClass("incorrectfield");
		$(".valuetext").removeClass("incorrectfield");
        if ($("#editQuestionControl").dialog("instance") != undefined)
            $("#editQuestionControl").dialog('destroy');
        var questionPopUp = new QuestionPopUp();
        questionPopUp.Init(PageMode.Insert, GenerateGuid(), QuestionType.Single);
        dialog = $("#editQuestionControl").dialog({
            autoOpen: false,
            width: 650,
			minHeight: 589,
			maxHeight: 589,
            modal: true,
            closeOnEscape: true,
            resizable: false,
			draggable: true,
            buttons: [
                    {
                        text: QuestionResources.QuestionPopUpSaveButton,
                        click: function (e) {
                        	$("#QuestionTextBox").removeClass("incorrectfield");
                        	$("#questionLevelDropDownList").removeClass("incorrectfield");
                        	$("#questionCategoryDropDownList").removeClass("incorrectfield");
                        	$("#tagsInput").removeClass("incorrectfield");
                        	$(".valuetext").removeClass("incorrectfield");
                            questionPopUp.Submit(e);
                        }

                    },
                    {
                        text: QuestionResources.QuestionPopUpCancelButton,
                        click: function () {
                            dialog.dialog("close");
                        }
                    }
            ]
        });
        dialog.dialog({ title: QuestionResources.InsertQuestionPopUpTitle });
        dialog.dialog("open");
    });

    // events for the sort header links
    $('.textsort').on('click', function (event) {

        SortTable('q.Text', $(this), event);
    })
    $('.categorysort').on('click', function (event) {

        SortTable('c.CategoryName', $(this), event);
    })
    $('.levelsort').on('click', function (event) {

        SortTable('l.LevelName', $(this), event);
    })
    $('.tagsort').on('click', function (event) {

        SortTable('Tag', $(this), event);
    })
    $('.questiontypesort').on('click', function (event) {

        SortTable('q.QuestionType', $(this), event);
    })

    // This funciton is used to srt the table based on the clicked header
    function SortTable(sortText, form, event) {
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

    // Binds the events for the links on the edit and delete columns
    function BindEvents() {
        $('#questionContainer').off('click', '.questionedit').on('click', '.questionedit', function (event) {
            event.preventDefault();
            event.stopPropagation();
            $("#QuestionTextBox").removeClass("incorrectfield");
            $("#questionLevelDropDownList").removeClass("incorrectfield");
            $("#questionCategoryDropDownList").removeClass("incorrectfield");
            $("#tagsInput").removeClass("incorrectfield");
            $(".valuetext").removeClass("incorrectfield");
            var index = $(this).closest('tr').index();
            index = index - 1;
            if ($("#editQuestionControl").dialog("instance") != undefined)
                $("#editQuestionControl").dialog('destroy');
            var questionPopUp = new QuestionPopUp();
            questionPopUp.Init(PageMode.Update, _filteredQuestion[index].QuestionGUID, GetFromStringQuestionType(_filteredQuestion[index].QuestionType));
            dialog = $("#editQuestionControl").dialog({
                autoOpen: false,
                width: 650,
                minHeight: 589,
                maxHeight: 589,
                modal: true,
                closeOnEscape: true,
                resizable: false,
				draggable: true,
                buttons: [
                    {
                        text: QuestionResources.QuestionPopUpSaveButton,
                        click: function (e) {
                            questionPopUp.Submit(e);
                        }
                    },
                    {
                        text: QuestionResources.QuestionPopUpCancelButton,
                        click: function () {
                            dialog.dialog("close");
                        }
                    }
                ]
            });
            dialog.dialog({ title: QuestionResources.EditQuestionPopUpTitle });
            dialog.dialog("open");
        });

        $('#questionContainer').off('click', '.questiondelete').on('click', '.questiondelete', function (event) {
            event.preventDefault();
            event.stopPropagation();
            var index = $(this).closest('tr').index();
            index = index - 1;
            var deleteQuestion = { questionGUID: JSON.stringify(_filteredQuestion[index].QuestionGUID) }

            $.ajax({
                type: "POST",
                url: "../Webservices/QuestionFilteringService.asmx/DeleteQuestion",
                data: JSON.stringify(deleteQuestion),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function () {

                }
            });//end ajax
            PopulateTable();
        });

        // pager event
        $(document).on('pagerIndexChanged', function (event) {
            event.preventDefault();
            event.stopPropagation();

            _pageNumber = parseInt(event.onPage);
            if (_oldPageNr != _pageNumber) {
                PopulateTable();
                _oldPageNr = _pageNumber;
            }
        });
    }

    //Populate table
    function PopulateTable() {

        for (var i = document.getElementById("questionContainer").rows.length; i > 1; i--) {
            document.getElementById("questionContainer").deleteRow(i - 1);
        }

        _rowsPerPage = $('.questionsonpagedropdown').find($('option:selected')).text();
        _filterInfo = [];
        _filteredQuestion = [];
        _filterInfo = {
            categoryGUID: JSON.stringify(_selectedCategory),
            levelGUID: JSON.stringify(_selectedLevel),
            tagGuidList: JSON.stringify(_allGuidTags),
            questionTypeList: JSON.stringify(_allQuestionStringTypes),
            sortExpression: JSON.stringify(_sortExpression),
            rowsPerPage: JSON.stringify(_rowsPerPage),
            pageNumber: JSON.stringify(_pageNumber)
        };
        CountFilteredQuestionsWithAjax();
        GetQuestionWithAjax();
    }

    // The Ajax call used to get the questions
    function GetQuestionWithAjax() {

        $.ajax({
            type: "POST",
            url: "../Webservices/QuestionFilteringService.asmx/GetFilteredQuestions",
            data: JSON.stringify(_filterInfo),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                var jsonostr = response.d;
                var jsonobj = eval('(' + jsonostr + ')');
                for (i in jsonobj) {
                    fq = {
                        QuestionGUID: jsonobj[i]["QuestionGUID"],
                        Text: TrimText(jsonobj[i]["Text"]),
                        RealText: jsonobj[i]["Text"],
                        QuestionType: jsonobj[i]["QuestionType"],
                        CategoryName: jsonobj[i]["CategoryName"],
                        LevelName: jsonobj[i]["LevelName"],
                        TagName: TrimTag(jsonobj[i]["TagName"]),
                        RealTag: jsonobj[i]["TagName"],
                        QuestionType: GetFromIntQuestionType(jsonobj[i]["QuestionType"]),

                    }
                    _filteredQuestion.push(fq);
                }

                //Message for no question filtered in DB
                $("#questionTemplate").tmpl(_filteredQuestion).appendTo("#questionContainer");
                BindEvents();
                $('.questionstableerrormessage').find('p').remove();
                if (_filteredQuestion.length == 0) {
                    $('.questionstableerrormessage').append('<p> There are no more questions.</p>');
                    $('.gridviewtable').hide();
                    _pagerObject.Reset();
                } else {
                    $('.gridviewtable').show();
                }
            }
        });//end ajax
    }

    // The Ajax call used to get the questions
    function CountFilteredQuestionsWithAjax() {

        var countFilterInfo = [];

        countFilterInfo = {
            categoryGUID: JSON.stringify(_selectedCategory),
            levelGUID: JSON.stringify(_selectedLevel),
            tagGuidList: JSON.stringify(_allGuidTags),
            questionTypeList: JSON.stringify(_allQuestionStringTypes),
        };

        $.ajax({
            type: "POST",
            url: "../Webservices/QuestionFilteringService.asmx/CountAllFilteredQuestions",
            data: JSON.stringify(countFilterInfo),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                _filteredQuestionsCount = response.d;
                _pagerObject.Reset();
                _pagerObject.Init(_filteredQuestionsCount, _rowsPerPage, _pagerForm);
            }
        });//end ajax
    }

    //This function show only 49 chars from a question.
    function TrimText(text) {

        if (text.length >= 50) {
            return text.substring(0, 49) + " ...";
        }
        return text;
    }

    function TrimTag(text) {
        var valtext = text;
        if (valtext.length >= 9) {
            valtext = text.substring(0, 8) + "...";
        }
        return valtext;
    }

});