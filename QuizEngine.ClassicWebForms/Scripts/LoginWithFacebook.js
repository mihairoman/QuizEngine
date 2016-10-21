function statusChangeCallback(response) {
    console.log('statusChangeCallback');
    console.log(response);
    if (response.status === 'connected') {
        testAPI();
    }
}

function checkLoginState() {
    FB.getLoginStatus(function (response) {
        statusChangeCallback(response);
    });
}

window.fbAsyncInit = function () {
    FB.init({
        appId: '505250972939454',
        cookie: true,
        xfbml: true,
        version: 'v2.0' 
    });
};

(function (d, s, id) {
    var js, fjs = d.getElementsByTagName(s)[0];
    if (d.getElementById(id)) return;
    js = d.createElement(s); js.id = id;
    js.src = "//connect.facebook.net/en_US/sdk.js";
    fjs.parentNode.insertBefore(js, fjs);
}(document, 'script', 'facebook-jssdk'));

function createCookie(email, userID, userType, minutes) {
    if (minutes) {
        var date = new Date();
        date.setTime(date.getTime() + (minutes * 60 * 1000));
        var expires = "; expires=" + date.toGMTString();
    }
    else var expires = "";
    document.cookie = "currentUser=email=" + email + "&userID=" +userID +"&userType="+userType+ expires + "; path=/";
}

function testAPI() {
    FB.api('/me', function (response) {
        var responseEmail = { username: JSON.stringify(response.email) }
        $.ajax({
            type: "POST",
            url: "../Webservices/LinkPermissionService.asmx/LoginFacebook",
            data: JSON.stringify(responseEmail),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                if (data.d == 'null') {
                    window.location = "AccessDenied.aspx";
                }
                else {
                    var jsonobj = JSON.parse(data.d);
                    fq = {
                        UserGuid: jsonobj["UserGuid"],
                        UserName: jsonobj["UserName"],
                        Password: jsonobj["Password"],
                        UserType: jsonobj["UserType"],
                        IsActive: jsonobj["IsActive"]
                    }
                    createCookie(fq.UserName, fq.UserGuid, fq.UserType, 30);
                    window.location = "ScoreQuizzes.aspx";
                }

            }
        });
    });
}

