<%@ Page MasterPageFile="../MasterPages/Site.Master"%>



<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="server">
    <script type="text/javascript" src="../Scripts/CheckCookie.js"></script>

   <%-- <script type="text/javascript">
        checkPermission("AdminDownloadGeneratedTest");
    </script>--%>
    <script src="../Scripts/PagerControl.js"></script>
    <script type="text/javascript" src="../Scripts/DropDownListCreator.js"> </script>
    <script src="../Scripts/AdminGenerateOnlineLink.js"></script>     
        <label style="font-family: Arial; font-size: 24px;" id="onlinelink"></label>
            <hr/><br/>
        
    <div>
        <label id="lblonlinelink" style="font-size: 12px; font-family: Arial; font-weight: bold; color: #000000;">
            </label>&nbsp;
        <select class="usersonpagedropdown" style="width: 100px;">
            <option value="five">5</option>
            <option value="ten">10</option>
            <option value="twenty">20</option>
        </select>
    </div>
    <br />
    <div class="gridview">
             <label id="lblMessageUsers" style="font-size: 12px; font-family: Arial; color:red;"></label>
        <div id="send">
             <label id="lblSendEmail" style="font-size: 12px; font-family: Arial; color:green;"></label>
            </div>
          <table id="usersTable" class="gridviewtable" style="min-width: 300px; max-width:700px;">
        <tr>
            <th href="#"  class="userNameSort" id="usernamelink" style="text-decoration: none; text-align:left; width:35%; "></th>
            <th href="#"  class="userTypeSort" id="usertypelink" style="text-decoration: none; text-align:left; width:35%;" ></th>
            <th align="left" id="sendmail"  width="15%">Send email</th>
        </tr>
    </table>
        </div>
     <script id="usersTemplate" type="text/x-jQuery-tmpl">
         <tr>
            <td title="${RealUserName}" class="usernametooltip">${UserName}</td>
            <td title="${RealTypeName}" class="typenametooltip">${TypeName}</td>
            <td><button align="center" type="button" class="generateLinkForUser" id="generateLink">Send</button></td>
        </tr>
    </script>
     <div>
        <div class="pager"  style="text-decoration:none; text-align: right;"></div>
    </div>     
</asp:Content>