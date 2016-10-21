<%@ Page MasterPageFile="Site.Master"%>
<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="server">
    <div id="wizardBody">
        <link rel="stylesheet" href="http://code.jquery.com/ui/1.11.0/themes/smoothness/jquery-ui.css" />
        <link rel="stylesheet" href="../Content/OnlineTestingWizard.css" />
        <link rel="stylesheet" href="../Content/jquery-ui.css" />
        <link rel="stylesheet" href="../Content/jquery-ui.theme.css" />
         <script type="text/javascript" src="../Scripts/CheckCookie.js"></script>

    <script type="text/javascript">
        checkCookie();
    </script>
        <script type="text/javascript" src="../Scripts/OnlineTestingWizard.js"></script>
        <script type="text/javascript" src="../Scripts/QuizEngine.js"></script>

        <div id="quizBody">
            <div style="width: 1px; height:1px">
            </div>
            <div class="buttons">
                <input class="ui-button ui-widget ui-state-default ui-corner-all ui-button-text-only" id="btnPrev" type="button" value="Previous" />
                <input class="ui-button ui-widget ui-state-default ui-corner-all ui-button-text-only" id="btnNext" type="button" value="Next" />
                <input class="ui-button ui-widget ui-state-default ui-corner-all ui-button-text-only" id="btnSubmit" type="button" value="Submit" />
            </div>
            <div class="questions"></div>
            <div class="choices"></div>

        </div>
    <script type="text/javascript">

        $(document).ready(function () {
            var quizID = GetQueryStringParam('QuizUID');
            var userID = GetQueryStringParam('UserUID');
            if ((typeof quizID !== 'undefined') && (typeof userID !== 'undefined')) {
                getObjects(quizID, userID);
            }
        });

        function GetQueryStringParam(key) {
            key = key.replace(/[*+?^$.\[\]{}()|\\\/]/g, "\\$&"); // escape RegEx meta chars
            var match = location.search.match(new RegExp("[?&]" + key + "=([^&]+)(&|$)"));
            return match && decodeURIComponent(match[1].replace(/\+/g, " "));

        };

        function getObjects(quizID, userID) {
            var obj = { quizId: quizID, userId: userID };
            $.ajax
            ({
                type: "POST",
                url: "../Webservices/OnlineTestingWizardService.asmx/GetSerializedObject",
                data: JSON.stringify(obj),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    var jsonString = response;
                    var jsFunction = new main(jsonString.d, quizID, userID);
                },
                error: function (msg) {
                    //alert("Ajax error " + msg.d);
                },
            });
        }

    </script>
    </div>
</asp:Content>
