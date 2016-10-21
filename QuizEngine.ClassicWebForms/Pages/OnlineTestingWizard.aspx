<%@ Page Language="C#" MasterPageFile="~/Pages/Site.Master" AutoEventWireup="true" CodeBehind="OnlineTestingWizard.aspx.cs" Inherits="QuizEngine.ClassicWebForms.Pages.OnlineTestingWizard" Culture="auto" meta:resourcekey="Page" UICulture="auto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="server">
    <div id="wizardBody">
        <link rel="stylesheet" href="http://code.jquery.com/ui/1.11.0/themes/smoothness/jquery-ui.css" />
        <link rel="stylesheet" href="../Styles/OnlineTestingWizard.css" />
        <link rel="stylesheet" href="../Styles/jquery-ui.css" />
        <link rel="stylesheet" href="../Styles/jquery-ui.theme.css" />
        <script src="//code.jquery.com/jquery-1.10.2.js"></script>
        <script src="//code.jquery.com/ui/1.11.0/jquery-ui.js"></script>
        <script type="text/javascript" src="../Scripts/OnlineTestingWizard.js"></script>
        <script type="text/javascript" src="../Scripts/QuizEngine.js"></script>
        <script type="text/javascript" src="../Scripts/LoadResources.js"></script>

        <div id="quizBody">
            <div id="dontdelete" style="width: 1px; height: 1px">
            </div>
            <div class="buttons">
                <input class="ui-button ui-widget ui-state-default ui-corner-all ui-button-text-only" id="btnPrev" type="button" />
                <input class="ui-button ui-widget ui-state-default ui-corner-all ui-button-text-only" id="btnNext" type="button" />
                <input class="ui-button ui-widget ui-state-default ui-corner-all ui-button-text-only" id="btnSubmit" type="button" />
            </div>
            <div class="questions"></div>
            <div class="choices"></div>
            <div id="timer"></div>
        </div>

    </div>
</asp:Content>
