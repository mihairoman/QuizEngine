<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EditArchivedChoice.aspx.cs" Inherits="QuizEngine.ClassicWebForms.Pages.EditArchivedChoice" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <p>
            <asp:Label ID="Label1" runat="server" Text="AnswerText:"></asp:Label> <br />

            <asp:TextBox ID="txtAnswerText" runat="server"></asp:TextBox>
        </p>
        <p>
            <asp:Label ID="Label2" runat="server" Text="Value:"></asp:Label> <br />

            <asp:TextBox ID="txtValue" runat="server"></asp:TextBox>
        </p>
        <p>
            <asp:Label ID="Label3" runat="server" Text="IsCorrect:"></asp:Label> <br />

            <asp:TextBox ID="txtIsCorrect" runat="server"></asp:TextBox>
        </p>
        <p>
            <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" />
        </p>
    </div>
    </form>
</body>
</html>
