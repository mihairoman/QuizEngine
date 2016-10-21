<%@ Page Language="C#" MasterPageFile="~/Pages/Site.Master" AutoEventWireup="true" Culture="auto" meta:resourcekey="PageResource1" UICulture="auto" %>



<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="server">
    <script type="text/javascript" src="../Scripts/CheckCookie.js"></script>

    <script type="text/javascript">
        checkPermission("AdminDownloadPredefinedTest");
    </script>
    <script src="../Scripts/PagerControl.js"></script>
    <script type="text/javascript" src="../Scripts/DropDownListCreator.js"> </script>

    <script type="text/javascript" src="../Scripts/AdminDownloadPredefinedTest.js"> </script>
  <label style="font-family: Arial; font-size: 24px;" id="predefinedtest"></label>
            <hr/><br/>
        
    <div>
        <label id="lblpredefinedtest" style="font-size: 12px; font-family: Arial; font-weight: bold; color: #000000;">
            </label>&nbsp;
        <select class="predefinedquizzesonpagedropdown" style="width: 100px;">
            <option value="five">5</option>
            <option value="ten">10</option>
            <option value="twenty">20</option>
            <option value="fifty">50</option>
        </select>
    </div>
     <div class="gridview">
      <label id="lblMessagePredefinedTest" style="font-size: 12px; font-family: Arial; color:red;"></label>
    <table id="predefinedQuizzes" class="gridviewtable" style="width: 600px">
        <tr>
            <th href="#" class="typeNameSort" id="typename" style="text-decoration: none;text-align:left; width:35%; "></th>
            <th align="left" width="20%" id="downloadOrLink"></th>
            <th align="left" width="20%" id="create">Create link</th>
       
        </tr>
    </table>
        </div>
    <script id="predefinedQuizzesTemplate" type="text/x-jQuery-tmpl">
        <tr>
            <td title="${RealTypeName}" class="typenametooltip">${TypeName}</td>
            <td><button type="button" class="downloadPDFPredefined">PDF</button>  &nbsp; &nbsp;<button type="button" class="downloadWordPredefined">DOC</button></td>
            <td><button align="left" type="button" class="createLinkPredefined">Create</button></td>
        </tr>
    </script>
        <div class="pager" id="pager1" style="text-align: right;"></div>
   


    <script type="text/javascript">
        $(document).ready(function () {

            $(function () {
                $('.typenametooltip').tooltip({
                    track: true
                });
            });
            $.ajax({
                type: "POST",
                url: "../Webservices/AdminDownloadGeneratedTestViewService.asmx/CountAllPredefinedQuizzes",
                data: '{ }',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    _filteredQuizzesCountPredefined = response.d;
                }
            });//end ajax

        });



    </script>
</asp:Content>