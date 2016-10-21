<%@ Page Language="C#"  MasterPageFile="~/Pages/Site.Master" AutoEventWireup="true" CodeBehind="UserHomeScreen.aspx.cs" Inherits="QuizEngine.ClassicWebForms.Pages.UserHomeScreen" culture="auto" meta:resourcekey="Page" uiculture="auto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="server">
        <asp:Label ID="lblUser" runat="server" meta:resourcekey="lblUser"></asp:Label>

    <div>
    
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
                <asp:boundfield datafield="LevelName" meta:resourcekey="LevelName"/>
                <asp:boundfield datafield="OnlineOrDownloaded" meta:resourcekey="OnlineOrDownloaded"/>
                <asp:boundfield datafield="TakenOrNot" meta:resourcekey="TakenOrNot" HtmlEncode="False"/>
                <asp:boundfield datafield="Result" meta:resourcekey="Result"/>
            </Columns>
        </asp:GridView>
</asp:Content>