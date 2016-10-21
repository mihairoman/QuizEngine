$(document).ready(function () {
    $("#HomeScreen").show();
    var quizId = GetQueryStringParam('QuizUID');
    function GetQueryStringParam(key) {
        key = key.replace(/[*+?^$.\[\]{}()|\\\/]/g, "\\$&"); // escape RegEx meta chars
        var match = location.search.match(new RegExp("[?&]" + key + "=([^&]+)(&|$)"));
        return match && decodeURIComponent(match[1].replace(/\+/g, " "));
    };
    //for the users table
    var _filteredUsers = [];
    var _filterInfoTable3 = [];
    var _sortExpression = "u.Username asc ";
    var _sortDir = 0;
    // the old page Number is used in order to rpevent displaying the same content on the page serveral times
    var _oldPageNr = 1;
    // the actual number of the page
    var _pageNumber = 1;
    // specifies how many information should be on the page, it has a default value 5
    var _rowsPerPage = 1;
    // old sort text helps when desiring to sort another  column in order to aleays start from ascending order
    var _oldSortText;
    // popualte the initial table
    var _scorQuizesPager = 1;
    // used to return the pager object
    var _pagerObject = new PagerControl();
    // the form of the pager
    var _pagerForm = 'pager';
    // this one is used to get the count of the fitlered questions
    var _filteredUsersCount;
    // popualte the tables
    PopulateTable3();

    $('.usersonpagedropdown').on('change', function () {
        _oldPageNr = 1;
        _pageNumber = 1;
        _pagerObject.ResetLink();
        PopulateTable3();
    });

    $('.userNameSort').on('click', function () {

        SortTable('u.Username', $(this));
    })
    $('.userTypeSort').on('click', function () {

        SortTable('ut.TypeName', $(this));
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
        PopulateTable3();

    }
    //functions for the users table
    function ReadAllUsersWithoutTheQuizID() {
        var object = { sortExpression: JSON.stringify(_sortExpression), rowsPerPage: JSON.stringify(_rowsPerPage), pageNumber: JSON.stringify(_pageNumber), quiz: JSON.stringify(quizId) };
        $.ajax({
            type: "POST",
            url: " ../Webservices/AdminCreateOnlineLinkForUser.asmx/ReadAllUsersWithoutTheQuizID",
            data: JSON.stringify(object),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                var jsonostr = response.d;
                var jsonobj = JSON.parse(jsonostr);
                for (i in jsonobj) {
                    fq = {
                        UserGuid: jsonobj[i]["UserGuid"],
                        UserName: TrimName(jsonobj[i]["UserName"]),
                        RealUserName: jsonobj[i]["UserName"],
                        Password: jsonobj[i]["Password"],
                        TypeName: jsonobj[i]["TypeName"],
                        RealTypeName: jsonobj[i]["TypeName"],
                        IsActive: jsonobj[i]["IsActive"],
                    }
                    _filteredUsers.push(fq);
                }
                $("#usersTemplate").tmpl(_filteredUsers).appendTo("#usersTable");
                BindEventsTable3();
            },
            error: function (msg) {
            },
        });//end ajax
    }

    function BindEventsTable3() {
        $(document).on('pagerIndexChanged', function (event) {
            event.preventDefault();
            event.stopPropagation();

            _pageNumber = parseInt(event.onPage);
            if (_oldPageNr != _pageNumber) {
                PopulateTable3();
                _oldPageNr = _pageNumber;
                _pagerObject.Reset();
                _pagerObject.Init(_filteredUsersCount, _rowsPerPage, _pagerForm);
            }

        });

        _pagerObject.Reset();
        _pagerObject.Init(_filteredUsersCount, _rowsPerPage, _pagerForm);


      
            $('.generateLinkForUser').one('click', function (event) {
                event.preventDefault();
                event.stopPropagation();
                var allUserEmails = [];
                var allUserGUID = [];
                var emailFrom = readCookie("currentUser").split("&")[0].split("=")[1];
                var emailSubject = "The link for your test";

                var index = $(this).closest('tr').index();
                index = index - 1;
                allUserGUID.push(_filteredUsers[index].UserGuid);
                allUserEmails.push(_filteredUsers[index].RealUserName);
                var text = "";
                text = text + "Hello " + _filteredUsers[index].RealUserName + "\n\n" + "You can take the test at the following link: " + "\n" + "http://localhost:51820/Pages/OnlineTestingWizard.aspx?QuizUID=" + quizId + "&UserUID=" + _filteredUsers[index].UserGuid + "\n\n" + " Have a nice day!";
                var emailBody = text;
                var obj2 = { quiz: quizId, user: _filteredUsers[index].UserGuid };

                var emails = {
                    allGUID: JSON.stringify(allUserGUID),
                    quizGUID: JSON.stringify(quizId),
                    allEmails: JSON.stringify(allUserEmails),
                    emailFrom: JSON.stringify(emailFrom),
                    emailSubject: JSON.stringify(emailSubject),
                    emailBody: JSON.stringify(emailBody)
                }

                $.ajax({
                    type: "POST",
                    url: "../Webservices/GenerateQuizLinkService.asmx/SendCustomEmailAndInsertQuizLink",
                    data: JSON.stringify(emails),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function () {

                        PopulateTable3();
                        $("#lblSendEmail").text(AdminGenerateOnlineLink.Success);
                        var tId;
                        $("#send").hide().slideDown();
                        clearTimeout(tId);
                        tId = setTimeout(function () {
                            $("#send").hide();
                        }, 3000);
                    },
                    failure: function (response) {
                        $("#lblMessageUsers").text(AdminGenerateOnlineLink.ErrorSending);
                    }

                });//end ajax
            });
    }


    //Populate users table

    function PopulateTable3() {
        for (var i = document.getElementById("usersTable").rows.length; i > 1; i--) {
            document.getElementById("usersTable").deleteRow(i - 1);
        }
        _rowsPerPage = $('.usersonpagedropdown').find($('option:selected')).text();
        _filterInfoTable3 = [];
        _filteredUsers = [];
        _filterInfoTable3 = {
            UserGuid: JSON.stringify(),
            UserName: JSON.stringify(),
            Password: JSON.stringify(),
            TypeName: JSON.stringify(),
            IsActive: JSON.stringify(),
            sortExpression: JSON.stringify(_sortExpression),
            rowsPerPage: JSON.stringify(_rowsPerPage),
            pageNumber: JSON.stringify(_pageNumber)
        };
        CountFilteredUsersWithAjax();
        ReadAllUsersWithoutTheQuizID();
    }


    function CountFilteredUsersWithAjax() {

        var obj2 = { quiz: quizId };
        $.ajax({
            type: "POST",
            url: "../Webservices/AdminCreateOnlineLinkForUser.asmx/CountAllUsers",
            data: JSON.stringify(obj2),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                _filteredUsersCount = response.d;
            }
        });//end ajax

    }

    function TrimName(text) {
        var valtext = text;
        if (valtext.length >= 25) {
            valtext = text.substring(0, 24) + "...";
        }
        return valtext;
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


});