<%@ Page Language="C#" AutoEventWireup="true" Inherits="QuizEngine.App.Pages.GoodbyePage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel="stylesheet" type="text/css" href="../Content/LoginStyle.css" />
    <script src="//code.jquery.com/jquery-1.10.2.js"></script>
    <script src="//code.jquery.com/ui/1.11.0/jquery-ui.js"></script>
    <link rel="stylesheet" href="../Content/jquery-ui.css" />
    <link rel="stylesheet" href="../Content/jquery-ui.theme.css" />
    <script type="text/javascript" src="/Scripts/Goodbye.js"></script>
    <script type="text/javascript" src="/Scripts/QuizEngine.js"></script>
    <title></title>
</head>
<body>
    <form id="goodbyeForm" runat="server">
        <div class="container">
            <section id="content">
                <h2>Thank you</h2>
                <hr />
                <div id="message">
                </div>
                <div id="info">
                </div>
            </section>
        </div>
    </form>
</body>
</html>
