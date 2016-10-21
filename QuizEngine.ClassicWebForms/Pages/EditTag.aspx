<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EditTag.aspx.cs" Inherits="QuizEngine.ClassicWebForms.Pages.EditTag" culture="auto" meta:resourcekey="Page" uiculture="auto" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <br />
        <br />
        <asp:Label ID="lblTagName" runat="server" Text="Tag Name" meta:resourcekey="lblTagName"></asp:Label>
        <asp:TextBox ID="txtTagName" runat="server" style="margin-left: 24px" Width="455px" meta:resourcekey="txtTagName"></asp:TextBox>
        <br />
        <br />
        <asp:Button ID="btnSubmit" runat="server" OnClick="btnSubmit_Click" Text="Submit" meta:resourcekey="btnSubmit" />
    
    </div>
    </form>
</body>
</html>

