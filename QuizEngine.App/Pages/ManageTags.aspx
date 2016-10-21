<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Site.Master" AutoEventWireup="true" Inherits="QuizEngine.App.Pages.ManageTags" culture="auto" meta:resourcekey="Page" uiculture="auto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="server">
        <asp:Label ID="Label1" runat="server" ForeColor="Black" Font-Bold="True" Text="Tags"></asp:Label>
        <asp:GridView ID="grdTag" runat="server" OnRowDeleting="grdTag_RowDeleting" OnRowEditing="grdTag_RowEditing" Height="200px" Width="500px" style="overflow: scroll;"
             DataKeyNames="TagUID" AutoGenerateColumns ="False" meta:resourcekey="grdTag" BackColor="White" BorderColor="#336666" BorderStyle="Double" BorderWidth="3px" CellPadding="4">
            <Columns>
                <asp:CommandField HeaderText="Delete" ShowDeleteButton="True" ButtonType="Button" meta:resourcekey="CommandField" />
                <asp:ButtonField ButtonType="Button" CommandName="Edit" HeaderText="Edit" ShowHeader="True" Text="Edit" meta:resourcekey="ButtonField" />
                <asp:BoundField DataField="TagName" HeaderText="TagName" SortExpression="TagName" meta:resourcekey="TagName" />
                 </Columns>
            <EditRowStyle BackColor="#336666" ForeColor="White" />
            <HeaderStyle BackColor="#C0C0C0" Font-Bold="True" ForeColor="Black" Height="35px" />
            <PagerStyle BackColor="#FFFF99" />
            <RowStyle BackColor="White" />
        </asp:GridView>
        <br />
        <asp:Button ID="btnInsert" runat="server" Height="32px" OnClick="btnInsert_Click" Text="Insert" Width="140px" meta:resourcekey="btnInsert" Font-Bold="True" />
        <br />
</asp:Content>