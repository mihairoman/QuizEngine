

    function checkPermission(permissionName) {


        if (readCookie('currentUser') === null) {
            window.location = "Login.aspx";
        }
        else {
            var username = readCookie('currentUser').split('&')[0].split('=')[1];
            var userID = readCookie('currentUser').split('&')[1].split('=')[1];
            var userType = readCookie('currentUser').split('&')[2].split('=')[1];
            createCookie(username, userID, userType, 30);
            var PermissionObject = { userGuid: userID, PermissionName: permissionName };
            $.ajax({
                type: "POST",
                url: "../Webservices/LinkPermissionService.asmx/HasPermission",
                data: JSON.stringify(PermissionObject),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    if (JSON.parse(data.d) === "false") {
                        window.location = "PermissionDenied.aspx";
                    }

                }
            });

        }

    }

    function checkCookie() {
        if (readCookie('currentUser') != null) {
            window.location = "Login.aspx";
        }
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

    function createCookie(email, userID, userType, minutes) {
        if (minutes) {
            var date = new Date();
            date.setTime(date.getTime() + (minutes * 60 * 1000));
            var expires = "; expires=" + date.toGMTString();
        }
        else var expires = "";
        document.cookie = "currentUser=email=" + email + "&userID=" + userID + "&userType=" + userType + expires + "; path=/";
    }
