<%@ Page Language="C#" MasterPageFile="~/Pages/Site.Master" AutoEventWireup="true" Culture="auto" meta:resourcekey="PageResource1" UICulture="auto" %>



<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="server">
    <style>
        .quizzesvalidationtable {
            border: 1px solid black;
            border-collapse: collapse;
        }

            .quizzesvalidationtable tr {
                border: 1px solid black;
                border-collapse: collapse;
            }

            .quizzesvalidationtable td {
                border: 1px solid black;
                border-collapse: collapse;
            }

            .quizzesvalidationtable th {
                border: 1px solid black;
                border-collapse: collapse;
            }
       
    </style>

 
    <div class="gridview">
    <table id="quizzesValidation" class="quizzesvalidationtable" style="width: 900px">
        <tr>
            <th>Correct</th>
            <th><a href="#" class="userNameSort">UserName</a></th>
            <th><a href="#" class="quizDateSort">QuizDate</a></th>
            <th><a href="#" class="levelNameSort">LevelName</a></th>
            <th><a href="#" class="categoryNameSort">CategoryName</a></th>
        </tr>
    </table>
        </div>
    <script id="validationTemplate" type="text/x-jQuery-tmpl">
        <tr>
            <td><a href="#" class="quizCorrect">Correct</a></td>
            <td>${UserName}</td>
            <td>${QuizDate}</td>
            <td>${LevelName}</td>
            <td>${CategoryName}</td>

        </tr>
    </script>


    <script type="text/javascript">
        $(document).ready(function () {
            var _filteredQuestion = [];
            var _filterInfo = [];
            var _sortExpression = 'UserName asc';
            var _sortDir = 0;
            // popualte the initial table
            PopulateTable();

            $('.userNameSort').on('click', function () {

                SortTable('Users.UserName', $(this));
            })
            $('.quizDateSort').on('click', function () {

                SortTable('QuizDate', $(this));
            })
            $('.levelNameSort').on('click', function () {

                SortTable('LevelName', $(this));
            })
            $('.categoryNameSort').on('click', function () {

                SortTable('CategoryName', $(this));
            })
            
            function SortTable(sortText, form) {

                   if (_sortDir == 0) {
                       $('.arrowimg').remove();
                       form.append('<img src="Images/down_arrow.png" class="arrowimg">');
                       _sortExpression = sortText + ' asc';
                       _sortDir = 1;
                   } else if (_sortDir == 1) {
                       $('.arrowimg').remove();
                       form.append('<img src="Images/up_arrow.png"  class="arrowimg">');
                       _sortExpression = sortText + ' desc';
                       _sortDir = 0;
                   }
                   PopulateTable();
               }


            function GetFreeTextQuizes() {

                $.ajax({
                    type: "POST",
                    url: " ../Webservices/FreeTextQuizViewService.asmx/GetFreeTextQuizes",
                    data: JSON.stringify(_filterInfo),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (response) {
                        var jsonostr = response.d;
                        var jsonobj = eval('(' + jsonostr + ')');
                        for (i in jsonobj) {
                            fq = {
                                UserUID: jsonobj[i]["UserUID"],
                                QuizUID: jsonobj[i]["QuizUID"],
                                UserName: jsonobj[i]["UserName"],
                                QuizDate: jsonobj[i]["QuizDate"],
                                CategoryName: jsonobj[i]["CategoryName"],
                                LevelName: jsonobj[i]["LevelName"],
                            }
                            _filteredQuestion.push(fq);
                        }
                        $("#validationTemplate").tmpl(_filteredQuestion).appendTo("#quizzesValidation");
                        BindEvents();
                    }
                });//end ajax
            }

            function BindEvents() {
                $('#quizzesValidation').on('click', '.quizCorrect', function (event) {
                    var index = $(this).closest('tr').index();
                    index = index - 1;
                    window.location.replace("FreeTextQuestionsPendingAdminResponse.aspx?&QuizUID=" + _filteredQuestion[index].QuizUID + "&UserUID=" + _filteredQuestion[index].UserUID);
                });

            }

            //Populate table

            function PopulateTable() {
                for (var i = document.getElementById("quizzesValidation").rows.length; i > 1; i--) {
                    document.getElementById("quizzesValidation").deleteRow(i - 1);
                }
                _filterInfo = [];
                _filteredQuestion = [];
                _filterInfo = {
                         UserUID: JSON.stringify(),
                         QuizUID: JSON.stringify(),
                         UserName: JSON.stringify(),
                         QuizDate: JSON.stringify(),
                         CategoryName: JSON.stringify(),
                         LevelName: JSON.stringify(),
                         sortExpression: JSON.stringify(_sortExpression)
                        
                 };

                GetFreeTextQuizes();
            }

        });
    </script>


</asp:Content>

<%--<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="server">
        <div>
            <asp:Label ID="lblAdminHomeScreen" runat="server" Text="The quizes that contain free text questions:" meta:resourcekey="lblAdminHomeScreen"></asp:Label>
            <br />
            <br />
            <asp:GridView ID="grdAdminHomeScreen" runat="server" AutoGenerateColumns="False" DataKeyNames="UserUID,QuizUID" OnRowCommand="grdAdminHomeScreen_RowCommand" meta:resourcekey="grdAdminHomeScreen" Visible="true">
                <Columns>
                    <asp:BoundField DataField="UserName" meta:resourcekey="UserName" />
                    <asp:BoundField DataField="QuizDate" HeaderText="Date and Time" meta:resourcekey="QuizDate" />
                    <asp:BoundField DataField="LevelName" HeaderText="Level Name" meta:resourcekey="LevelName" />
                    <asp:BoundField DataField="CategoryName" HeaderText="Category Name" meta:resourcekey="CategoryName" />
                    <asp:TemplateField HeaderText="Grade Quizes" meta:resourcekey="TemplateField">
                        <ItemTemplate>
                            <asp:LinkButton Text="Validate" runat="server" ID="Validate"
                                CommandName="Validate" CausesValidation="False" CommandArgument='<%# String.Concat(Eval("QuizUID").ToString(),"_",Eval("UserUID").ToString()) %>' meta:resourcekey="Validate" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
</asp:Content>--%>
