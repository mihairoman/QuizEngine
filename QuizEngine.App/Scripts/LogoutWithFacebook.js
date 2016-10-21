window.fbAsyncInit = function () {
    FB.init({
        appId: '505250972939454',
        cookie: true,
        xfbml: true,  // parse social plugins on this page
        version: 'v2.0' // use version 2.0
    });

    FB.getLoginStatus(function (response) {
        statusChangeCallback(response);
    });
};

// Load the SDK asynchronously
(function (d, s, id) {
    var js, fjs = d.getElementsByTagName(s)[0];
    if (d.getElementById(id)) return;
    js = d.createElement(s); js.id = id;
    js.src = "//connect.facebook.net/en_US/sdk.js";
    fjs.parentNode.insertBefore(js, fjs);
}(document, 'script', 'facebook-jssdk'));

function fbLogoutUser() {

    FB.getLoginStatus(function (response) {
        if (response && response.status === 'connected') {
            FB.logout(function (response) {
                document.cookie = "currentUser=; expires=Thu, 01 Jan 1970 00:00:00 GMT";
                //document.location.reload();
                // docCookies.removeItem("currentUser");
                window.location.href = "Login.aspx";
            });
        }
    });

}