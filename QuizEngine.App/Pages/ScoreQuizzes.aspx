<%@ Page MasterPageFile="../MasterPages/Site.Master" %>

<asp:Content ID="Content" ContentPlaceHolderID="Content" runat="server">
    <script type="text/javascript" src="../Scripts/CheckCookie.js"></script>
     <script type="text/javascript" src="../Scripts/PagerControl.js"></script>
    <script type="text/javascript" src="../Scripts/DropDownListCreator.js"> </script>
<%--    <script type="text/javascript">
        checkPermission("ScoreQuizzes");
    </script>--%>
    <script type="text/javascript" src="../Scripts/ScoreQuizzes.js"> </script>
    
        <label style="font-family: Arial; font-size: 24px;" id="scorequizzes"></label>
            <hr/><br/>
        
    <div>
        <label id="labelonpagescore" style="font-size: 12px; font-family: Arial; font-weight: bold; color: #000000;">
            </label>&nbsp;
        <select class="quizzesonpagedropdown" style="width: 100px;">
            <option value="five">5</option>
            <option value="ten">10</option>
            <option value="twenty">20</option>
            <option value="fifty">50</option>
        </select>
    </div>
    <br />
    <div>
    <div class="gridview">
        <table id="quizzesValidation" class="gridviewtable" style="min-width: 300px; max-width:900px;">
            <tr>
                <th class="userNameSort" id="usernamescore" style="text-decoration:none;text-align:left; width:30%;" ></th>
                <th class="quizDateSort" id="quizdatescore" style="text-decoration: none;text-align:left;width: 30%; "></th>    
                 <th class="categoryNameSort" id="categorynamescore"  style="text-decoration: none;text-align:left; width:16%;"></th>
                <th class="levelNameSort" id="levelnamescore"  style="text-decoration: none;text-align:left;width:16%;"></th>
                <th  align="left" id="correctlabel" width="8%" ></th>
            </tr>
        </table>
    </div>
    <script id="validationTemplate" type="text/x-jQuery-tmpl">
        <tr>
            <td title="${RealUserName}" class="usertooltip">${UserName}</td>
            <td>${QuizDateForUI}</td>
            <td title="${RealCategoryName}" class="categorytooltip">${CategoryName}</td>
            <td title="${RealLevelName}" class="leveltooltip">${LevelName}</td>
            <td>
                <button  align="center" type='button' id="correctbutton" class="correctBtn" ></button></td>
        </tr>
    </script>
          <div class="pager" style="text-decoration:none; text-align: right;"></div>
    </div>
</asp:Content>
