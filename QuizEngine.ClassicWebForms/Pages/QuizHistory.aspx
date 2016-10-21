<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="QuizHistory.aspx.cs" MasterPageFile="~/Pages/Site.Master" Inherits="QuizEngine.ClassicWebForms.Pages.ViewQuizes" Culture="auto" meta:resourcekey="Page" UICulture="auto" %>


<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="server">
    <script type="text/javascript" src="../Scripts/CheckCookie.js"></script>

    <script type="text/javascript">
        checkPermission("QuizHistory");
    </script>
    <script src="../Scripts/PagerControl.js"></script>
    <script type="text/javascript" src="../Scripts/QuizHistory.js"> </script>
    <script src="../Scripts/moment.js"></script>
        <div>
            <label style="font-family: Arial; font-size: 24px;" id="QuizHistory"></label>
            <hr/><br/>
            <div id="errorMsgDiv" style="color: red; display:none" >
                <p>No quiz exist</p>
            </div>
            <label id="selectUserLabel" style="font-size: 12px; font-family: Arial; font-weight: bold; color: #000000;"></label>
            <select id="selectedUser">
                <option value="AllUsers" selected="true">All Users</option>
            </select> 
            <br/>
            <label id="rowPerPageLabel" style="font-size: 12px; font-family: Arial; font-weight: bold; color: #000000;"></label>
            <select style="width: 100px"  id="rowPerPage">
                <option value="five">5</option>
                <option value="ten">10</option>
                <option value="twenty">20</option>
                 <option value="fifty">50</option>
            </select>
            <br />
            <label id="userInfo"></label>
             <br />
            <table id="allQuizesTable" class="gridviewtable" style="min-width: 300px; max-width:1200px; float: left; font-family: Arial; font-size: 12px;">
                <tr>
                <th id="userNameCol" class="userNameSort" style="text-decoration: none; text-align: left; width:20%;"></th>
                <th id="resultCol" class="resultSort" style="text-decoration: none; text-align: left;width:8%;"></th>
                <th id="quizDateCol" class="quizDateSort" style="text-decoration: none; text-align: left; width:18%;"></th>
                <th id="categoryNameCol" class="categoryNameSort" style="text-decoration: none; text-align: left;width:10%;"></th>
                <th id="levelNameCol" class="levelName" style="text-decoration: none; text-align: left;width:10%;"></th>
                <th id="quizTimeCol" class="quizTime" style="text-decoration: none; text-align: left; width:10%;"></th>
                <th id="takeOnlineCol" align="left"></th>
                <th id="correctCol" class="correctColum" align="left" width="8%"></th>
                    <th id="showCol" class="showColum" align="left" width="6%"></th>
                </tr>
            </table>
            <br />
            <br /> 
              <%--  <div class="pager"> 
                </div>--%>
            <script id="quizTemplate" type="text/x-jQuery-tmpl">
                <tr>
                    <td title="${RealUserName}" class="usertooltip">${UserName}</td>
                    <td>${Result}</td>
                    <td>${QuizDateForUI}</td>
                    <td title="${RealCategoryName}" class="categorytooltip">${CategoryName}</td>
                    <td title="${RealLevelName}" class="leveltooltip">${LevelName}</td>
                    <td>${QuizTime}</td>
                    <td>${TakenOnline}</td>
                    <td><input type="button" class="correctBtn" value="Correct"   id="${QuizUID}_${UserUID}"/></td>
                    <td><input type="button" class="showBtn"    value="Show" id="${UserUID}_${QuizUID}" /></td>
                </tr>
            </script>

            <script id ="userDropDownPopulate" type="text/x-jquery-tmpl">
                <option value = "${UserUID}" >${UserName}</option>
            </script>
            <script>
                $(document).ready(function () {
                    $(function () {
                        $('.usertooltip').tooltip({
                            track: true
                        });
                    });
                    $(function () {
                        $('.categorytooltip').tooltip({
                            track: true
                        });
                    });
                    $(function () {
                        $('.leveltooltip').tooltip({
                            track: true
                        });
                    });
                    quizPager = new PagerControl();
                    time = moment;
                });
            </script>
            <div class="pager" style="text-decoration:none; text-align: right;"></div>
 
        </div>
</asp:Content>


