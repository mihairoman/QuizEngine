<%@ Page MasterPageFile="../MasterPages/Site.Master"%>
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
         </select>
        <br/>
        <br/>
         <table id="templateQuizTable" class="gridviewtable" style="width: 900px; float: left; font-family: Arial; font-size: 12px;">
             <tr>
                 <th><a href="#" id="QuizNameCol" class ="quizNameSort"></a></th>
                 <th><a href="#" id="QuestionNrCol" class ="questionNrSort"></a></th>
                 <th id="TakeQuizCol"></th>
             </tr>
         </table>
         <div class="pager" style="text-decoration:none; text-align: right;">

         </div>
         <script id="templateQuizPopulate" type="text/x-jQuery-tmpl">
                <tr>
                    <td>${QuizName}</td>
                    <td>${QuestionNumber}</td>
                    <td><input type="button" id="${QuizGUID}" value="Start Now" class="startQuizBtn"/></td>
                </tr>
          </script>
          <script>
              $(document).ready(function () {
                
              });
          </script>
     </div>
</asp:Content>
