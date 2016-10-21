function createCookie(email, userID, userType, minutes) {
    if (minutes) {
        var date = new Date();
        date.setTime(date.getTime() + (minutes * 60 * 1000));
        var expires = "; expires=" + date.toGMTString();
    }
    else var expires = "";
    document.cookie = "currentUser=email=" + email + "&userID=" + userID + "&userType=" + userType + expires + "; path=/";
}
function OnSuccess(response) {
    if (response.d == 'null') {
        window.location = "AccessDenied.aspx";
    }
    else {
        var jsonobj = JSON.parse(JSON.parse(response).d);
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
function onError(response) {
    alert(response);
}

function runCode() {

    var clientContext = new SP.ClientContext();
    user = clientContext.get_web().get_currentUser();

    clientContext.load(user);
    clientContext.executeQueryAsync(Function.createDelegate(this, this.onQuerySucceeded), Function.createDelegate(this, this.onQueryFailed));

}

function onQuerySucceeded() {
    var req = new PEService();
    var obj = { username: user.get_loginName() };
    req.ExecuteCall("LinkPermissionService.asmx", "LoginWindows",  JSON.stringify(obj), OnSuccess, onError);
   
}

function onQueryFailed(sender, args) {
    alert('Request failed. ' + args.get_message() + '\n' + args.get_stackTrace());
}


function loginWindows() {
    runCode();
    //$.ajax({
    //    type: "POST",
    //    url: "../Webservices/LinkPermissionService.asmx/LoginWindows",
    //    data: {},
    //    contentType: "application/json; charset=utf-8",
    //    dataType: "json",
    //    success: function (response) {
    //        if (response.d == 'null') {
    //            window.location = "AccessDenied.aspx";
    //        }
    //        else {
    //            var jsonobj = JSON.parse(response.d);
    //            fq = {
    //                UserGuid: jsonobj["UserGuid"],
    //                UserName: jsonobj["UserName"],
    //                Password: jsonobj["Password"],
    //                UserType: jsonobj["UserType"],
    //                IsActive: jsonobj["IsActive"]
    //            }
    //            createCookie(fq.UserName, fq.UserGuid, fq.UserType, 30);
    //            window.location = "ScoreQuizzes.aspx";
    //        }

    //    }
    //});
}