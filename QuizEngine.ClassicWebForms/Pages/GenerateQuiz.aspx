<%@ Page Language="C#" MasterPageFile="~/Pages/Site.Master" AutoEventWireup="true" CodeBehind="GenerateQuiz.aspx.cs" Inherits="QuizEngine.ClassicWebForms.Pages.GenerateQuiz" Culture="auto" meta:resourcekey="Page" UICulture="auto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="server">
    <script type="text/javascript" src="../Scripts/CheckCookie.js"></script>

    <script type="text/javascript">
        checkPermission("GenerateQuizzes");
    </script>
     <script  type="text/javascript" src ="../Scripts/GenerateQuizManagePage.js"></script>
     <script src="../Scripts/PagerControl.js"></script>
     <label style="font-family: Arial; font-size: 24px">Take a Generated Quiz</label>
            <hr/>
    <div>
        <br/>
         <label id ="quizPerPageLabel" style="font-size: 12px; font-family: Arial; font-weight: bold; color: #000000;"></label>
         <select id="quizPerPageDropDown">
             <option value="5">5</option>
             <option value="10">10</option>
             <option value="20">20</option>
             <option value="50">50</option>
         </select>
        <br/>
        <br/>
         <table id="templateQuizTable" class="gridviewtable" style="min-width: 300px; max-width:900px; float: left; font-family: Arial; font-size: 12px;">
             <tr>
                 <th id="QuizNameCol" class ="quizNameSort"  style="text-decoration:none;text-align:left; width:46%;"></th>
                 <th id="QuestionNrCol" class ="questionNrSort" style="text-decoration:none;text-align:left; width:40%;"></th>
                 <th align="left" id="TakeQuizCol"></th>
             </tr>
         </table>
         <div class="pager" style="text-decoration:none; text-align: right;">

         </div>
         <script id="templateQuizPopulate" type="text/x-jQuery-tmpl">
                <tr>
                    <td title="${RealQuizName}" class="quiztooltip">${QuizName}</td>
                    <td>${QuestionNumber}</td>
                    <td><input type="button" id="${QuizGUID}" value="Start Now" class="startQuizBtn"/></td>
                </tr>
          </script>
          <script>
              $(document).ready(function () {
                  $(function () {
                      $('.quiztooltip').tooltip({
                          track: true
                      });
                  });
              });
          </script>
     </div>
</asp:Content>
