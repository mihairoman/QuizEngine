function createCookie(email, userID, userType, minutes) {
    if (minutes) {
        var date = new Date();
        date.setTime(date.getTime() + (minutes * 60 * 1000));
        var expires = "; expires=" + date.toGMTString();
    }
    else var expires = "";
    document.cookie = "currentUser=email=" + email + "&userID=" + userID + "&userType=" + userType + expires + "; path=/";
}

function loginWindows() {
    $.ajax({
        type: "POST",
        url: "../Webservices/LinkPermissionService.asmx/LoginWindows",
        data: {},
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            if (response.d == 'null') {
                window.location = "AccessDenied.aspx";
            }
            else {
                var jsonobj = JSON.parse(response.d);
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
}