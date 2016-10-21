<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="QuizShow.aspx.cs"  MasterPageFile="~/Pages/Site.Master" Inherits="QuizEngine.ClassicWebForms.Pages.QuizShow" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="server">
    <script type="text/javascript" src="../Scripts/CheckCookie.js"></script>

    <script type="text/javascript">
        checkPermission("QuizHistory");
    </script>
     <script type="text/javascript" src="../Scripts/QuizShow.js"> </script>
    <div id="quizInfo">
        <label id="userName"></label> <label id="userNameProp" style=" font-weight:bold;"></label> <br />
        <label id="quizResult"></label> <label id="quizResultProp" style=" font-weight:bold;"></label> <br />
        <label id="quizDate"></label> <label id="quizDateProp" style=" font-weight:bold;"></label> <br />
    </div>
    <div>
        <hr />
        <ul id="quizQuestions" style ='list-style-type: none;'>
            <li></li>
        </ul>
    </div>
</asp:Content>
