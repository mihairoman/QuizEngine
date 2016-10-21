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

function logout() {

    document.cookie = "currentUser=; expires=Thu, 01 Jan 2014 00:00:00 GMT ; path=/";

    FB.getLoginStatus(function (response) {
        if (response && response.status === 'connected') {
            FB.logout(function (response) { });
        }

    });
    window.location.href = "Login.aspx";
}