(function ($) {

    $(document).ready(function () {
    

        GetLinkPermissions();

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
      
        
        function test(userPermissions) {
            var y = userPermissions;

            //$('#home').after('<li class=\'active\'><a href=\'#\'><span>Settings</span></a></li>');
            //$('.active').append('<ul class=\'components\'></ul>');


            for (var i = 0; i < y.length; i++) {
                $("." + y[i].CSSClass).append('<li class="menu-item" tab="tab' + i + '"> <a href=' + y[i].Link + '><span>' + y[i].Name + '<div id="' + y[i].Name.split(' ').join('') + 'Description" class="v-nav-description"></div></span></a></li>');
                
            }

            if ($(".homescreen").children.length > 0)
            {
                $("#mainMenu").prepend('<li ><a href="../Pages/' + $('.homescreen').children("li:first").children("a").attr("href") + '"><span id="title">QuizEngine</span></a></li>')
            }

            if ($(".components").children("li").length > 0) {
                $(".has-sub").before('<li ><a href="../Pages/' + $('.components').children("li:first").children("a").attr("href") + '"><span >Settings</span></a></li>')
            }

            if ($(".quizzes").children("li").length > 0) {
                $(".has-sub").before('<li ><a href="../Pages/' + $('.quizzes').children("li:first").next().children("a").attr("href") + '"><span >Generate Quiz</span></a></li>')
            }


            $("")
            $('#CategoriesDescription').text(MasterResources.CategoryDescriptionRes);
            $('#LevelsDescription').text(MasterResources.LevelDescriptionRes);
            $('#QuestionsDescription').text(MasterResources.QuestionDescriptionRes);
            $('#UsersDescription').text(MasterResources.UserDescriptionRes);
            $('#BasicQuizDescription').text(MasterResources.BasicQuizDescriptionRes);
            $('#TemplateDescription').text(MasterResources.TemplateQuizDescriptionRes);
            $('#ScoreQuizzesDescription').text(MasterResources.ScoreQuizDescriptionRes);
            $('#HistoryDescription').text(MasterResources.HistoryDescriptionRes);
            $('#DownloadTemplateDescription').text(MasterResources.DownloadTemplateDescriptionRes);
            $('#DownloadQuizDescription').text(MasterResources.DownloadQuizDescriptionRes);

            
            $('#settings').text(MasterResources.SettingsRes);
            $('#genquiz').text(MasterResources.GenerateQuizRes);
            $('#about').text(MasterResources.AboutRes);



            //if ($('.components').children("li").length <= 0) {
            //    $('.Settings').remove();
            //}

            if ($('.quizzes').children("li").length <= 0) { 
                $('.GenerateQuiz').remove();
            }

            $('#username').text(readCookie('currentUser').split('&')[0].split('=')[1]);

            $('#cssmenu > ul').closest('li').addClass('active');
        }

        function GetLinkPermissions() {
           

            var x = { userGUID: document.cookie.split('&')[1].split('=')[1].split(';')[0] }
            $.ajax({
                type: "POST",
                url: "../Webservices/LinkPermissionService.asmx/GetLinkPermissions",
                data: JSON.stringify(x),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    var jsonobj = JSON.parse(response.d);
                    test(jsonobj);
                }
            });
        }
    

        var items = $('#v-nav>ul>li.menu-item').each(function () {
            $(this).click(function () {
                //remove previous class and add it to clicked tab
                items.removeClass('current');
                $(this).addClass('current');

                //hide all content divs and show current one
                $('#v-nav>div.tab-content').hide().eq(items.index($(this))).show('fast');

                window.location.hash = $(this).attr('tab');
            });
        });

        if (location.hash) {
            showTab(location.hash);
        }
        else {
            showTab("tab0");
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

        function showTab(tab) {
            $("#myid").on('click', 'li', function (event) {
                //alert($(this).text());
            });
        }

        if (readCookie("currentUser").split("&")[2].split("=")[1] != 1) {
            $("#Score").hide();
            $("#PendingQuizzes").hide();
        }



        $('#cssmenu > ul').prepend('<li class=\"mobile\" ><a href=\"#\"><span>Menu <i>&#9776;</i></span></a></li>');
        $(window).resize(function () {
            if ($('#cssmenu li').hasClass('mobile')) {
                $('#cssmenu ul ').css('overflow', 'hidden');
                $('#cssmenu ul ul:visible').slideUp('fast');
            }
        });
        $('#cssmenu > ul > li > a').click(function (e) {
            $('#cssmenu li').removeClass('active');
            $(this).closest('li').addClass('active');
            var checkElement = $(this).next();
            if ((checkElement.is('ul')) && (checkElement.is(':visible'))) {
                $(this).closest('li').removeClass('active');
                checkElement.slideUp('fast');
            }
            if ((checkElement.is('ul')) && (!checkElement.is(':visible'))) {
                $('#cssmenu ul ul:visible').slideUp('fast');
                $('#cssmenu ').css('overflow', 'visible');
                $('#cssmenu ul ').css('overflow', 'visible');
                checkElement.slideDown('fast');
            }
            if ($(this).parent().hasClass('mobile')) {

                $('#cssmenu ul ul:visible').slideUp('fast');
                $('#cssmenu ul ').css('overflow', 'hidden');
                $('#cssmenu ').css('overflow', 'hidden');
                e.preventDefault();
                $('#cssmenu').toggleClass('expand');



            }
            if ($(this).closest('li').find('ul').children().length == 0) {
                return true;
            } else {
                return false;
            }
        });
    });
})(jQuery);



