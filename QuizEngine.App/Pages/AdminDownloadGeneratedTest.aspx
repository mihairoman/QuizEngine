<%@ Page MasterPageFile="../MasterPages/Site.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="server">
   <script type="text/javascript" src="../Scripts/CheckCookie.js"></script>

 <%-- <script type="text/javascript">
        checkPermission("AdminDownloadGeneratedTest");
    </script>--%>
    <script src="../Scripts/PagerControl.js"></script>
    <script type="text/javascript" src="../Scripts/DropDownListCreator.js"> </script>
    <script type="text/javascript" src="../Scripts/AdminDownloadGeneratedTest.js"> </script>
    <label style="font-family: Arial; font-size: 24px;" id="generatetest"></label>
            <hr/><br/>

    <div>
        <label id="lblgeneratetest" style="font-size: 12px; font-family: Arial; font-weight: bold; color: #000000;">
            </label>&nbsp;
        <select class="randomquizzesonpagedropdown" style="width: 100px;">
            <option value="five">5</option>
            <option value="ten">10</option>
            <option value="twenty">20</option>
            <option value="fifty">50</option>
        </select>
    </div>
 
    <div class="gridview">
       <label id="lblMessageGeneratedTest" style="font-size: 12px; font-family: Arial; color:red;"></label>
    <table id="generatedQuizzes" class="gridviewtable" style="min-width: 600px; max-width:900px;">
        <tr>
            <th id="levelName" class ="levelNameSort" style="text-decoration: none;text-align:left; width:33%;" ></th>
            <th id="categoryName" class ="categoryNameSort" style="text-decoration: none;text-align:left; width:33%; "></th>
            <th id="downloadOrLink" align="left" width="20%"></th>
            <th id="create" align="left" width="14%"></th>
        </tr>
    </table>
        </div>
    <script id="generatedQuizzesTemplate" type="text/x-jQuery-tmpl">
        <tr>
            <td title="${RealLevelName}" class="leveltooltip">${LevelName}</td>
            <td title="${RealCategoryName}" class="categorytooltip">${CategoryName}</td>
            <td><button align="center" type="button" class="downloadPDF" >PDF</button> <button align="center" type="button" class="downloadWord" >Word</button> </td>
            <td><button align="center" type="button" class="createLinkRandom">Create</button></td>
        </tr>
    </script>
        <div class="pager" id="pager1" style="text-align: right;"></div>
</asp:Content>