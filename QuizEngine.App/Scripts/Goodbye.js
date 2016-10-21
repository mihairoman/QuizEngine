var response;
var userType;
var messageArea;
var link;

$(document).ready(function () {
    var homeLink = '../Pages/QuizHistory.aspx';
    var contactLink = 'http://umtsoftware.com/#CONTACT';
    response = GetQueryStringParam('Response');
    userType = GetQueryStringParam('UserType');
    messageArea = $('.container').find('#message');
    infoArea = $('.container').find('#info');
    messageArea.empty();
    infoArea.empty();

    if (response !== "freeText") {
        var result = $('<p>Your score: ' + response + '%</p>');
        messageArea.append(result);
    }
    else {
        var msg1 = $('<p>You will soon be given your results.</p>');
        var msg2 = $('<p>Have a nice day.</p>');
        messageArea.append(msg1);
        messageArea.append($('<br />'));
        messageArea.append(msg2);
    }

    if (userType !== UserType.Candidate) {
        var home = $('<input type="button" class="ui-button ui-widget ui-state-default ui-corner-all ui-button-text-only"/>');
        home.val("Home Page");
        infoArea.append(home);
        link = homeLink;
    }
    else {
        var contact = $('<input type="button" class="ui-button ui-widget ui-state-default ui-corner-all ui-button-text-only"/>');
        contact.val("Contact Us");
        infoArea.append(contact);
        link = contactLink;
    }

    infoArea.find('input').hover(function () {
        $(this).toggleClass("ui-state-hover");
    });

    infoArea.on('click', function () {
        location.replace(link);
    });
});
