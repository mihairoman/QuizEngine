<%@ Page MasterPageFile="Site.Master" %>

<asp:Content ID="Content" ContentPlaceHolderID="Content" runat="server">
    <script type="text/javascript" src="../Scripts/CheckCookie.js"></script>
    <script type="text/javascript">
        checkPermission("QuizHistory");
    </script>
     <script type="text/javascript" src="../Scripts/QuizShow.js"> </script>
   
    <div id="quizInfo">
        <label id="userName"></label> <br />
        <label id="quizResult"></label> <br />
        <label id="quizDate"></label> <br />
    </div>
    <div>
        <hr />
        <ul id="quizQuestions" style ='list-style-type: none;'>
            <li></li>
        </ul>
    </div>
</asp:Content>
