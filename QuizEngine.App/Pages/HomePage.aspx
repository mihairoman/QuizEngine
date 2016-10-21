<%@ Page Language="C#" MasterPageFile="~/Pages/Site.Master" AutoEventWireup="true" Inherits="QuizEngine.App.Pages.HomePage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="server">

<head>
   <link rel="stylesheet" href="../Content/HomePage.css">
   <script src="http://code.jquery.com/jquery-latest.min.js" type="text/javascript"></script>
   <script src="../Scripts/HomePage.js"></script>
   <title>Quiz Engine</title>
</head>
<body id='body'>

    <section id="wrapper" class="wrapper">

  <h1 class="title">Welcome to Quiz Engine !!!</h1>

  <div id="v-nav">

  <ul>
    <li tab="tab1" class="first current">Score Quizzes</li>
    <li tab="tab2">Pending Quizzes</li>
    <li tab="tab3">History</li>
    <li tab="tab4" class="last">Statistics</li>
 </ul>

 <div class="tab-content">
   <h4>Score Quizzes</h4>
 </div>

 <div class="tab-content">
   <h4>Pending Quizzes</h4>
 </div>

 <div class="tab-content">
    <h4>History</h4>   
 </div>

 <div class="tab-content">
   <h4>Statistics</h4>                
 </div>

</div>

</section>
	
</body>

</asp:Content>