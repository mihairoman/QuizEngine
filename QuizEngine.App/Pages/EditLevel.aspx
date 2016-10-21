<%@ Page Language="C#" AutoEventWireup="true"  Inherits="QuizEngine.App.Pages.EditLevel" culture="auto" meta:resourcekey="PageResource" uiculture="auto" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:Label ID="lblInserOrUpdate" runat="server" Text="Insert or update an element:" meta:resourcekey="lblInserOrUpdate1"></asp:Label>
        <br />
        <br />
        <asp:Label ID="lblName" runat="server" Text="Level's name:" meta:resourcekey="lblName"></asp:Label>
&nbsp;
        <asp:TextBox ID="txtName" runat="server" Width="145px" meta:resourcekey="txtName"></asp:TextBox>
        <br />
        <br />
        <asp:Label ID="lblDifficulty" runat="server" Text="Level's difficulty:" meta:resourcekey="lblDifficulty"></asp:Label>
&nbsp;
        <asp:TextBox ID="txtDifficulty" runat="server" Width="131px" meta:resourcekey="txtDifficulty"></asp:TextBox>
        <br />
        <br />
        <br />
        <asp:Button ID="btnSubmit" runat="server" OnClick="btnSubmit_Click" Text="Submit" meta:resourcekey="btnSubmit" />
        <br />
    </div>
    </form>
</body>
</html>
