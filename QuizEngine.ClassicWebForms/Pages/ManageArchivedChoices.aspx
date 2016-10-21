<%@ Page Language="C#"  AutoEventWireup="true" CodeBehind="ManageArchivedChoices.aspx.cs" Inherits="QuizEngine.ClassicWebForms.ManageArchivedChoices" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:GridView ID="gvArchivedChoices" runat="server" AutoGenerateColumns="false" DataKeyNames="ChoiceID,QuestionID"
            OnRowEditing="gvArchivedChoices_RowEditing"
            OnRowUpdating="gvArchivedChoices_RowUpdating"
            OnRowDeleting="gvArchivedChoices_RowDeleting"
            OnRowDataBound="gvArchivedChoices_RowDataBound">
            <Columns>
                <asp:ButtonField ButtonType="Button" CommandName="Edit" HeaderText="Edit" ShowHeader="true" Text="Edit" />
                <asp:ButtonField ButtonType="Button" CommandName="Delete" HeaderText="Delete" ShowHeader="true" Text="Delete" />
                <asp:BoundField DataField="AnswerText" HeaderText="AnswerText" SortExpression="AnswerText" />
                <asp:BoundField DataField="Value" HeaderText="Value" SortExpression="Value"  />
                <asp:BoundField DataField="IsCorrect" HeaderText="IsCorrect" SortExpression="IsCorrect" />
            </Columns>
        </asp:GridView>
    </div>
        <br />
    <div>
        <asp:Button ID="btnInsert" runat="server" OnClick="btnInsert_Click" Text="Insert" />
    </div>
    </form>
</body>
</html>
