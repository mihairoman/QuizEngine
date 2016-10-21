$(document).ready(function () {
    if (parseInt($("#lbl_questionNumber").text()) > parseInt($("#lbl_number").text())) {
        $("#form1").hide();
        alert("Not Enough Questions in Our Database. Please select fewer questions...");
        window.location.href = "QuizGenerate.aspx";
    }
    else
    {
        $("#lbl_number").hide();
        $("#lbl_questionNumber").hide();
    }

});