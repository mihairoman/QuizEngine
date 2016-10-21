<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="QuizEngine.ClassicWebForms.Pages.Login" %>

<!DOCTYPE html>
<html>
<head>
    <script type="text/javascript" src="../Scripts/jquery.min.js"> </script>
    <script src="jquery.js" temp_src="jquery.js" type="text/javascript"></script>
    <script src="../Scripts/LoginWithFacebook.js" type="text/javascript"></script>
    <script src="../Scripts/LoginWithWindows.js" type="text/javascript"></script>
    <link rel="stylesheet" type="text/css" href="../Styles/LoginStyle.css" />

    <script language="javascript" type="text/javascript">
          $(document).ready(function () {
        var OAUTHURL = 'https://accounts.google.com/o/oauth2/auth?';
        var VALIDURL = 'https://www.googleapis.com/oauth2/v1/tokeninfo?access_token=';
        var SCOPE = 'https://www.googleapis.com/auth/userinfo.profile https://www.googleapis.com/auth/userinfo.email';
        var CLIENTID = '486415422610-970r0thfliuqic72ks4vqjgjkrfjahmm.apps.googleusercontent.com';
        var REDIRECT = 'http://localhost:51820/Pages/ManageUsers.aspx';
        var LOGOUT = 'http://accounts.google.com/Logout';
        var TYPE = 'token';
        var _url = OAUTHURL + 'scope=' + SCOPE + '&client_id=' + CLIENTID + '&redirect_uri=' + REDIRECT + '&response_type=' + TYPE;
        var acToken;
        var tokenType;
        var expiresIn;
        var user;
        var loggedIn = false;
        function login() {
            var win = window.open(_url, "windowname1", 'width=800, height=600');
            var pollTimer = window.setInterval(function () {
                try {
                    console.log(win.document.URL);
                    if (win.document.URL.indexOf(REDIRECT) != -1) {
                        window.clearInterval(pollTimer);
                        var url = win.document.URL;
                        acToken = gup(url, 'access_token');
                        tokenType = gup(url, 'token_type');
                        expiresIn = gup(url, 'expires_in');
                        win.close();
                        validateToken(acToken);
                    }
                } catch (e) {
                }
            }, 500);
        }
        function validateToken(token) {
            $.ajax({
                url: VALIDURL + token,
                data: null,
                success: function (responseText) {
                    getUserInfo();
                    loggedIn = true;
                    $('#loginText').hide();
                    $('#logoutText').show();
                },
                dataType: "jsonp"
            });
        }
        function getUserInfo() {
            $.ajax({
                url: 'https://www.googleapis.com/oauth2/v1/userinfo?access_token=' + acToken,
                data: null,
                success: function (resp) {
                    user = resp;
                    console.log(user);
                    $('#uName').text('Welcome ' + user.name);
                    $('#imgHolder').attr('src', user.picture);
                },
                dataType: "jsonp"
            });
        }
        //credits: http://www.netlobo.com/url_query_string_javascript.html
        function gup(url, name) {
            name = name.replace(/[\[]/, "\\\[").replace(/[\]]/, "\\\]");
            var regexS = "[\\#&]" + name + "=([^&#]*)";
            var regex = new RegExp(regexS);
            var results = regex.exec(url);
            if (results == null)
                return "";
            else
                return results[1];
        }
        function startLogoutPolling() {
            $('#loginText').show();
            $('#logoutText').hide();
            loggedIn = false;
            $('#uName').text('Welcome ');
            $('#imgHolder').attr('src', 'none.jpg');
        }
          });
    </script>
</head>
<body>
    <div id="status"></div>


    <div class="container">
        <section id="content">
            <form action="">
                <h1>Login Form</h1>
                <div>
    <fb:login-button scope="public_profile,email" onlogin="checkLoginState();"></fb:login-button>

        <br />
        <br />

                    <form id="form1" runat="server" style="align-content: center;">
                        <asp:ImageButton ID="btnWindowsLogin" runat="server"  OnClientClick="loginWindows(); return false;"   ImageUrl="~/Images/windows-logo.png"/>
                    </form>
                </div>

    </form>
        </section>
        <!-- content -->
    </div>
    <!-- container -->



</body>
</html>
