function wizardControl(validation)
{
    var listObjects = JSON.parse(validation);
    var j=0;
    for (j=0; j<listObjects.length; j++)
        listObjects[j].Grade="";

    var lblGrade = '<label id="lblGrade">Grade: </label>';
    var txtGrade = '<input id="txtGrade" type="text" />';
    var btnPrevious = '<input id="btnPrevious" type="button" value="Previous" />';
    var btnNext = '<input id="btnNext" type="button" value="Next" />';
    var btnSubmit = '<input id="btnSubmit" type="submit" value="Submit" />';
    var objectLength = listObjects.length - 1;
    var lblQuestionText = '<label id="lblQuestionText">Question: ' + listObjects[0].QuestionText + ' </label>';
    var lblAnswerText = '<label id="lblAnswerText">Answer: ' + listObjects[0].UserAnswerText + '  </label>';

   
    $("#components").append(lblQuestionText);
    $("#components1").append(lblAnswerText);
    $("#components2").append(lblGrade);
    $("#components2").append(txtGrade);

    $("#lblQuestionText").addClass("label1");
    $("#lblAnswerText").addClass("label2");
    $("#lblGrade").addClass("label3");
    $("#txtGrade").addClass("text");   
    $("#txtGrade").val(listObjects[0].Grade);

    var transfer_string;
    var i = 0;
    var put_submit = false;

    if (objectLength === 0)
    {
        $("#components3").append(btnSubmit);
        $("#btnSubmit").addClass("submitbtn");
        put_submit = true;       
        $("#btnSubmit").on("click", function ()
        {
            if (!validateAdminInput())
            {
                listObjects[i].Grade = $("#txtGrade").val();
                var temp =
                            {
                                listObjects: JSON.stringify(listObjects)
                            }
                var transmitJsonString = JSON.stringify(temp);
                sendResponse(transmitJsonString);
                confirm("Submited!");
            }
        });
        
    }
    else
    {
        $("#components3").append(btnPrevious);
        $("#components3").append(btnNext);
        $("#btnNext").addClass("nextbtn");
        $("#btnPrevious").addClass("previousbtn");

        $("#btnPrevious").on("click", function ()
        {
            if (!validateAdminInput())
            {
                if (i > 0)
                {
                    if (!validateAdminInput())
                    {
                        $("#lblQuestionText").text("Question " + listObjects[i - 1].QuestionText);
                        $("#lblAnswerText").text("Answer " + listObjects[i - 1].UserAnswerText);
                        if (put_submit)
                        {
                            $("#btnSubmit").remove();
                            put_submit = false;
                        }
                        listObjects[i].Grade = $("#txtGrade").val();
                        i--;
                    }
                    $("#txtGrade").val(listObjects[i].Grade);

                }
                else
                {
                    confirm("This was the first question!");
                }
            }
        });



        $("#btnNext").on("click", function ()
        {
            if (!validateAdminInput())
            {
                if (i < objectLength)
                {
                    if ((i + 1) === objectLength)
                    {
                        $("#components3").append(btnSubmit);
                        $("#btnSubmit").addClass("submitbtn");
                        put_submit = true;

                        $("#btnSubmit").on("click", function ()
                        {
                            if (!validateAdminInput())
                            {
                                listObjects[i].Grade = $("#txtGrade").val();
                                var temp =
                                    {
                                        listObjects: JSON.stringify(listObjects)
                                    }
                                var transmitJsonString = JSON.stringify(temp);
                                sendResponse(transmitJsonString);
                                confirm("Submited!");
                            }
                        });
                    }
                    $("#lblQuestionText").text("Question: " + listObjects[i + 1].QuestionText);
                    $("#lblAnswerText").text("Answer: " + listObjects[i + 1].UserAnswerText);


                    listObjects[i].Grade = $("#txtGrade").val();
                    i++;
                    $("#txtGrade").val(listObjects[i].Grade);
                }

                else
                {
                    confirm("This was the last question!");
                }
        }
        });

    }
}

function sendResponse(transmitJsonString) {
    $.ajax
        ({
            type: "POST",
            url: "../Webservices/FreeTextQuestionsPendingAdminResponseService.asmx/DeserializeJsonString",
            data: transmitJsonString,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response)
            {            
                if (response !== "")
                {                    
                    window.location.href = "../Pages/ScoreQuizzes.aspx";
                }
                else
                {
                    confirm("Transmited Json was altered!");
                    window.location.href = "../Pages/ScoreQuizzes.aspx";
                }
            },
            error: function (msg)
            {
                confirm("error: " + msg.val());
            }
        });
}

function validateAdminInput()
{
    if ($("#txtGrade").val() === "")
    {
        confirm("Required field!");
        return true;
    }
    if (!$.isNumeric($("#txtGrade").val()))
    {
        confirm("Not a number!");
        return true;
    }
    if (parseFloat($("#txtGrade").val()) < 0 || parseFloat($("#txtGrade").val()) > 1)
    {
        confirm("Value must be between 0 and 1!");
        return true;
    }   
}