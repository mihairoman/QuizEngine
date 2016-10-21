<%@ Page Language="C#" AutoEventWireup="true" Inherits="QuizEngine.App.Pages.QuizesPerUser" culture="auto" meta:resourcekey="Page" uiculture="auto" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:Label ID="lblSelectUser" runat="server" meta:resourcekey="lblSelectUser"></asp:Label>
    <div>
    
        <asp:DropDownList ID="ddlUsers" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlUsers_SelectedIndexChanged" meta:resourcekey="ddlUsers">
        </asp:DropDownList>
        <br />
    
        <asp:Label ID="lblTitle" runat="server" meta:resourcekey="lblTitle"></asp:Label>
    
    </div>
        <asp:GridView ID="grdQuizes" runat="server" AutoGenerateColumns="False" DataKeyNames="QuizUID" meta:resourcekey="grdQuizes">
            <rowstyle backcolor="LightCyan"  
                forecolor="DarkBlue"
                font-italic="true"/>

            <alternatingrowstyle backcolor="PaleTurquoise"  
                forecolor="DarkBlue"
                font-italic="true"/>
            <Columns>
                <asp:boundfield datafield="QuizDate" meta:resourcekey="QuizDate"/>
                <asp:boundfield datafield="CategoryName" meta:resourcekey="CategoryName"/>
                <asp:boundfield datafield="LevelName"  meta:resourcekey="LevelName"/>
                <asp:boundfield datafield="OnlineOrDownloaded" meta:resourcekey="OnlineOrDownloaded"/>
                <asp:boundfield datafield="TakenOrNot" headertext="Taken or Not" HtmlEncode="False" meta:resourcekey="TakenOrNot"/>
                <asp:boundfield datafield="Result" headertext="Result" meta:resourcekey="Result"/>
            </Columns>
        </asp:GridView>
        <asp:Label ID="lblMessage" runat="server"></asp:Label>
    </form>
</body>
</html>
