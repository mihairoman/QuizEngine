
$(document).ready(function () {
    currentUserType = 0;
    $("#HomeScreen").show();

    var _userGuid = {
        userGuid: null
    };
    if (readCookie("currentUser").split("&")[2].split("=")[1] == 1) { //if the current user is an admin
        currentUserType = 1;
    } else {
        _userGuid["userGuid"]=readCookie("currentUser").split("&")[1].split("=")[1];
        $("#selectedUser").hide();
        $(".correctColum").hide();
        $("#selectUserLabel").hide();
    }
    allRowCount = 0;
    _rowPerPage = 5;
    var sortDirByUserName = -1;
    var sortDirByResult = -1;
    var sortDirByCategory = -1;
    var sortDirByDate = -1;
    var sortDirByLevelDifficulty = -1;
    var sortDirByTime = -1;
    var _currentPageNumber = 1;
    var _clickedPageNumber = 1;
    var _sortExpression = ' UserName asc';
    var _quizView = [];
    var _allUsers = [];
    var _quizViewInfo = {
        pageNumber: _currentPageNumber,
        rowPerPage: _rowPerPage,
        sortExpression: _sortExpression,
        userGuid: _userGuid["userGuid"]
    };
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
    $("#rowPerPage").on('change', function () {
        _rowPerPage = $("#rowPerPage").find($('option:selected')).text();
        quizPager.Reset();
        quizPager.ResetLink();
        _currentPageNumber = 1;
        PopulateQuizTable();

    });

    $("#selectedUser").on('change', function () {
        var x = document.getElementById("selectedUser").selectedIndex;
        if (x == 0) {
            _userGuid["userGuid"] = null;

        } else {
            _userGuid["userGuid"] = _allUsers[x-1]["UserUID"];
        }
        _currentPageNumber = 1;
        quizPager.Reset();
        quizPager.ResetLink();
        PopulateQuizTable();
    });
    $(".userNameSort").on('click', function (e) {
        e.preventDefault();
        deleteTableRows();
        sortDirByUserName = sortDirByUserName * (-1);
        _sortExpression = " UserName ";
        sortTable(sortDirByUserName,this);
    });
    $(".resultSort").on('click', function (e) {
        e.preventDefault();
        deleteTableRows();
        sortDirByResult = sortDirByResult * (-1);
        _sortExpression = " Result ";
        sortTable(sortDirByResult, this);
    });
    $(".categoryNameSort").click(function (e) {
        e.preventDefault();
        deleteTableRows();
        sortDirByCategory = sortDirByCategory * (-1);
        _sortExpression = " CategoryName ";
        sortTable(sortDirByCategory, this);
    });
    $(".quizDateSort").click(function (e) {
        e.preventDefault();
        deleteTableRows();
        sortDirByDate = sortDirByDate * (-1);
        _sortExpression = " QuizDate ";
        sortTable(sortDirByDate, this);
    });
    $(".levelName").click(function (e) {
        e.preventDefault();
        deleteTableRows();
        sortDirByLevelDifficulty = sortDirByLevelDifficulty * (-1);
        _sortExpression = " Difficulty ";
        $('.arrowimg').remove();
        sortTable(sortDirByLevelDifficulty, this);
    });
    $(".quizTime").click(function (e) {
        e.preventDefault();
        deleteTableRows();
        sortDirByTime *= (-1);
        _sortExpression = " Time ";
        sortTable(sortDirByTime, this);
    });

    function sortTable(dir, form) {
        $('.arrowimg').remove();
        if (dir == 1) {
            _sortExpression += "ASC ";
            $(form).append('<img src="../Images/down_arrow.png" class="arrowimg" style="float:right;">');
        } else {
            _sortExpression += "DESC ";
            $(form).append('<img src="../Images/up_arrow.png"  class="arrowimg" style="float:right;">');
        }
        quizPager.Reset();
        PopulateQuizTable();
    }


    $.ajax({
        type: "POST",
        url: "../Webservices/QuizViewListingService.asmx/AllUsersRead",
        data: '{}',
        contentType: "application/json; charset=utf-8",
        dataType: "json", //the return data type
        success: function (response) {

            var jsonostr = response.d; //in response I have the data from webservice
            var jsonobj = JSON.parse(jsonostr);
            for (i in jsonobj) {
                user = {
                    UserName: jsonobj[i]["UserName"],
                    UserUID: jsonobj[i]["UserGuid"].toString(),
                }
                _allUsers.push(user);
            };
            $("#userDropDownPopulate").tmpl(_allUsers).appendTo("#selectedUser");
            PopulateQuizTable(); //get all quizes initially by all the users
        }
    }); //end ajax
    function PopulateQuizTable() {
        quizPager.Reset();
        deleteTableRows();
        getQuizViewCount();
        Bindvenets();
    };
    function Bindvenets() {
       
        $(document).on('pagerIndexChanged', function (event) {
            event.preventDefault();
            event.stopPropagation();
            _clickedPageNumber = parseInt(event.onPage);
            if (_currentPageNumber != _clickedPageNumber) {
                _currentPageNumber = _clickedPageNumber;
                quizPager.Reset();
                quizPager.Init(allRowCount, _rowPerPage, 'pager');
                PopulateQuizTable();
            }
        });
    }

    function getQuizView() {
        var localUserGuid = null;
        if (_userGuid["userGuid"] == null) {
            localUserGuid = '00000000-0000-0000-0000-000000000000';
        } else {
            localUserGuid = _userGuid["userGuid"];
        }
        _quizViewInfo = {
            pageNumber: _currentPageNumber,
            rowPerPage: _rowPerPage,
            sortExpression: _sortExpression,
            userGuid: localUserGuid
        };
        _quizView = [];
        $.ajax({
            type: "POST",
            url: "../Webservices/QuizViewListingService.asmx/QuizViewRead",
            data: JSON.stringify(_quizViewInfo),
            contentType: "application/json; charset=utf-8",
            dataType: "json", //the return data type
            success: function (response) {
                var jsonostr = response.d; //in response I have the data from webservice
                var jsonobj = eval('(' + jsonostr + ')');
                for (i in jsonobj) {
                    var inter = jsonobj[i]["Time"];
                    var displayTime
                    if (inter != null) {
                        var startDateTime = inter["Hours"] + " : " + inter["Minutes"] + " : " + inter["Seconds"];
                        var startMoment = time(startDateTime, "HH:mm:ss");
                         displayTime = startMoment.format("hh:mm:ss").toString();
                    } else {
                        displayTime = "undefined";
                    }
                  
                    fq = {
                        UserName: TrimText(jsonobj[i]["UserName"]),
                        RealUserName: jsonobj[i]["UserName"],
                        Result: jsonobj[i]["Result"],
                        CategoryName: TrimName(jsonobj[i]["CategoryName"]),
                        RealCategoryName: jsonobj[i]["CategoryName"],
                        LevelName: TrimName(jsonobj[i]["LevelName"]),
                        RealLevelName: jsonobj[i]["LevelName"],
                        TakenOnline: jsonobj[i]["OnlineOrDownloanded"],
                        QuizDate: new Date(parseInt(jsonobj[i]["QuizDate"].toString().substring(6), 10)),
                        QuizDateForUI: (new Date(parseInt(jsonobj[i]["QuizDate"].toString().substring(6), 10))).toString().substring(0,25),
                        QuizUID: jsonobj[i]["QuizUID"],
                        UserUID: jsonobj[i]["UserUID"],
                        QuizTime:  displayTime
                    }
                    _quizView.push(fq);
                }
                deleteTableRows();
                $("#quizTemplate").tmpl(_quizView).appendTo("#allQuizesTable");
                hideUnnecessaryElements();
                $(".correctBtn").on('click', function () {
                    var args = this.id.toString().split("_");
                    var URL = "FreeTextQuestionsPendingAdminResponse.aspx?QuizUID=" + args[0] + "&UserUID=" + args[1] + "&QuizDate="; //format the URL
                    window.location.href = URL; //make the redirect
                });
                $(".showBtn").on('click', function () {
                    var args = this.id.toString().split("_");
                    var i = getArrayIndex(args[0], args[1]);
                    var URL = "QuizShow.aspx?QuizUID=" + args[1] + "&UserUID=" + args[0] + "&QuizDate=" + _quizView[i]["QuizDateForUI"] + "&Result=" + _quizView[i]["Result"]
                        + "&UserName=" + _quizView[i]["UserName"]; //format the URL
                    window.location.href = URL;
                });
             
            },
            error: function () {
                alert("not enter in the getQuizView ");
            }
        }); //end ajax     
    };
    function getArrayIndex(userUID, quizUID) {
        for (var i = 0; i < _quizView.length; i++) {
            if (_quizView[i]["QuizUID"] == quizUID && _quizView[i]["UserUID"] == userUID) {
                return i;
            }
        }
        return -1;

    }
    function deleteTableRows() { //delete all the users
        for (var i = 0; i < _quizView.length; i++) {
            $("#allQuizesTable").find("tr:gt(" + i + ")").remove();
        }
    };

    function getQuizViewCount() {
        $.ajax({
            type: "POST",
            url: "../Webservices/QuizViewListingService.asmx/CountAllQuizView",
            data: JSON.stringify(_userGuid),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                allRowCount = response.d;
                quizPager.Reset();
                quizPager.Init(allRowCount, _rowPerPage, 'pager');
                getQuizView();
            },
            error: function () {
                alert("fail");
            }
        });//end ajax
    };
    function hideUnnecessaryElements() {
        var hastohidelastcoloum = true;
        if (_quizView.length == 0) {
            $("#allQuizesTable").hide();
            quizPager.Reset();
            $("#errorMsgDiv").show();
            $("#rowPerPageLabel").hide();
            $("#rowPerPage").hide();
        } else {
            $("#rowPerPageLabel").show();
            $("#rowPerPage").show();
            $("#errorMsgDiv").hide();
            $("#allQuizesTable").show();
            for (var i = 0; i < _quizView.length; i++) {
                if (_quizView[i]["Result"] != null || (!_quizView[i]["TakenOnline"]) || currentUserType != 1) {
                    var btnid = _quizView[i]["QuizUID"] + "_" + _quizView[i]["UserUID"];
                    $("#" + btnid).hide();
                } else {
                    $("#" + _quizView[i]["UserUID"] + "_" + _quizView[i]["QuizUID"]).hide(); //hide the unnacesarry show btn
                    hastohidelastcoloum = false;
                }
            }
            if (hastohidelastcoloum) {
                $("#correctcolum").hide();
            }
        };
    }
    function TrimText(text) {
        var valtext = text;
        if (valtext.length >= 25) {
            valtext = text.substring(0, 24) + "...";
        }
        return valtext;
    }
    function TrimName(text) {
        var valtext = text;
        if (valtext.length >= 15) {
            valtext = text.substring(0, 14) + "...";
        }
        return valtext;
    }
});