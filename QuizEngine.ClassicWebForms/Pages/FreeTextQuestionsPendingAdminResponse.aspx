<%@ Page Language="C#" MasterPageFile="~/Pages/Site.Master" AutoEventWireup="true" Culture="auto" meta:resourcekey="PageResource1" UICulture="auto" %>



<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="server">

    <link rel="stylesheet" href="http://code.jquery.com/ui/1.11.0/themes/smoothness/jquery-ui.css"/>
    <link href="/Styles/FreeTextQuestionValidationWizard.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../Scripts/CheckCookie.js"></script>

    <script type="text/javascript">
        checkPermission("FreeTextQuestionsPendingAdminResponse");
    </script>
    <script src="//code.jquery.com/jquery-1.10.2.js"></script>
    <script src="//code.jquery.com/ui/1.11.0/jquery-ui.js"></script>
    <script type="text/javascript" src="/Scripts/FreeTextQuestionsPendingAdminResponse.js"></script>

   
    <div id="Principal">
        <div id="messages">
            </div>
        <div id="title">
            </div>
     <div id="components">
       </div>
    <div id="components1">
        </div>
    <div id="components2">
        </div>
    <div id="components3">
        </div>
    </div>
      
  
    <script type="text/javascript">
        $(document).ready(function () {

            var quizId = GetQueryStringParam('QuizUID');
            var userId = GetQueryStringParam('UserUID');
            if (typeof quizId !== "undefined" && typeof userId !== "undefined") {
                getObjects(quizId,userId);
            }
        });

        function GetQueryStringParam(key) {
            key = key.replace(/[*+?^$.\[\]{}()|\\\/]/g, "\\$&"); // escape RegEx meta chars
            var match = location.search.match(new RegExp("[?&]" + key + "=([^&]+)(&|$)"));
            return match && decodeURIComponent(match[1].replace(/\+/g, " "));
        };

        function getObjects(quizId, userId) {
            var obj = { quiz: quizId, user:userId };
         $.ajax
         ({
             type: "POST",
             url: "../Webservices/FreeTextQuestionsPendingAdminResponseService.asmx/GetSerializedObject",
             data: JSON.stringify(obj),
             contentType: "application/json; charset=utf-8",
             dataType: "json",
             success: function (response) {
                 if (response !== "")
                 {
                     var validation = response;
                     var y = new wizardControl(validation.d);
                 }
                 else
                 {
                     confirm("No free text questions!");
                     window.location.href = "../Pages/ScoreQuizzes.aspx";
                 }
             }
             //error: function (msg) {
             //    confirm("Ajax error " + msg.d);
             //},
         });
     }
           </script>

</asp:Content>