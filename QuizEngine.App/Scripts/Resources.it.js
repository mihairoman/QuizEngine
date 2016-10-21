$('head').on('loadResources', function () {
    QuestionResources = {
        "QuestionTextLabel": "Question Text italiano",
        "LevelLabel": "Level",
        "CategoryLabel": "Category",
        "TagsLabel": "Tags",
        "QuestionTypeLabel": "Question Type",
        "ChoiceIsCorrectLabel": "Is Correct",
        "ChoiceText": "Text",
        "ChoiceValue": "Value",
        "ChoiceTrue": "True",
        "ChoiceFalse": "False",
        "FreeText": "Free Text",
        "MultiChoice": "Multiple Choice",
        "Single": "Single Choice",
        "TrueFalse": "True/False",
        "Weighted": "Weighted",
        "EditQuestionPopUpTitle": "Edit Question",
        "InsertQuestionPopUpTitle": "Insert Question",
        "QuestionPopUpSaveButton": "Save",
        "QuestionPopUpCancelButton": "Cancel",
        "QuestionTextError": "All questions must have Text. \n",
        "QuestionLevelError": "You must choose a Level. \n",
        "QuestionCategoryError": "You must choose a Category. \n",
        "QuestionTagError": "You must choose at least one Tag. \n",
        "QuestionNotEnoughChoicesError": "You must add at least one choice if question type is Free Text and True/False or at least two choices otherwise. \n",
        "QuestionTrueFalseError": "You must select True or False. \n",
        "QuestionChoiceWeightedValueFormatError": "The value for the weighted choice must be a positive decimal number e.g: 0.2. \n",
        "QuestionNumberOfCorrectChoicesError": "At least one choice must be correct. \n",
        "QuestionChoiceValueNeededError": "All Choices must have value for Weighted questions. \n",
        "QuestionChoiceTextError": "All Choices must have Text. \n",
        "QuestionDuplicateTagError": "This tag has already been selected. \n",
        "QuestionOverflowTagTextError": "Tags Text should be shorter than 50 characters. \n"
    };
    QuizGenerateResources = {
        "LevelError": "Please select a level!",
        "CategoryError": "Please select a category!",
        "TagError": "Please add some Tags!",
        "TypeError": "Please add the question types in order to continue!",
        "IntegerError": "Please enter an integer value !",
        "QuestionNumberError": "Please enter a number of questions between 5 and 50 !",
        "GenerateLink": "Generate Link",
        "TakeQuiz": "Take Quiz",
        "DownloadDOC": "Download DOC",
        "DownloadPDF": "Download PDF",
        "Cancelbutton": "Cancel",
        "SelectCategoryLabel": "Select Category : ",
        "SelectLevelLabel": "Select Level : ",
        "SelectTagsLabel": "Select Tags : ",
        "SelectQuestionTypesLabel": "Select Question Types :",
        "NumberOfQuestions": "Number of Questions :",
        "SelectedCategoryLabel": "Selected Category : ",
        "SelectedLevelLabel": "Selected Level : ",
        "SelectedTagsLabel": "Selected Tags : ",
        "SelectedQuestionTypesLabel": "Selected Question Types :",
        "ListOfQuestions": "List of Questions",
        "QuestionText": "Question Text",
        "QuestionType": "Question Type",
        "TagName": "Tag Name",
        "SaveTemplate": "Save as Template",
        "TemplateSave": "Your quiz has been saved !",
        "NoQuestionsError": "There are no questions with the selected filters.",
        "NotEnoughQuestionsError": "There are not enough questions with the selected filters.",
        "TemplateText": "Please select a name for the template :",
        "Save": "Save",
        "Cancel": "Cancel",
        "SendMail": "Send Mail",
        "UserMail": "Select the users for which you want to generate the quiz link :",
        "GenerateQuiz": "Generate Basic Quiz",
        "SelectedFeatures": "These are the selected features for the quiz.",
        "Preview": "Generated quiz preview"
    };
    CategoryResources = {
        "CategoryNameLabel": "Category name",
        "CategoryPopupSaveButton": "Save",
        "CategoryPopupCancelButton": "Cancel",
        "CategoryNameTooShortError": "The provided Category Name should have at least 1 character.",
        "CategoryNameTooLongError": "The provided Category Name should have at most 50 characters.",
        "CategoryInsertError": "An error occured while inserting the provided Category.",
        "CategoryEditError": "An error occured while editing the provided Category.",
        "CategoryInsert": "Insert Category",
        "CategoryEdit": "Edit Category"
    };
    LevelResources = {
        "LevelNameLabel": "Level name",
        "LevelDifficultyLabel": "Difficulty",
        "LevelPopupSaveButton": "Save",
        "LevelPopupCancelButton": "Cancel",
        "LevelNameTooShortError": "The provided Level Name should have at least 1 character.",
        "LevelNameTooLongError": "The provided Level Name should have at most 50 characters.",
        "LevelDifficultyError": "The provided Difficulty must be a number between 1 and 5.",
        "LevelInsertError": "An error occured while inserting the provided Level.",
        "LevelEditError": "An error occured while editing the provided Level.",
        "LevelInsert": "Insert Level",
        "LevelEdit": "Edit Level"
    };
    $.holdReady(false);
    $(document).ready(function () {

        $('.categorieslabel').text(QuizGenerateResources.SelectedCategoryLabel);
        $('.levelslabel').text(QuizGenerateResources.SelectedLevelLabel);
        $('.taglabel').text(QuizGenerateResources.SelectedTagsLabel);
        $('.questionlabel').text(QuizGenerateResources.SelectedQuestionTypesLabel);

        $('#lblAddLevel').text(QuizGenerateResources.SelectLevelLabel);
        $('#lblAddCategory').text(QuizGenerateResources.SelectCategoryLabel);
        $('#lblAddTag').text(QuizGenerateResources.SelectTagsLabel);
        $('#lblAddType').text(QuizGenerateResources.SelectQuestionTypesLabel);
        $('#lblNumberOfQuestions').text(QuizGenerateResources.NumberOfQuestions);
        $('#templatetext').text(QuizGenerateResources.TemplateText);
        $('.save').attr('value', QuizGenerateResources.Save);
        $('.cancel').attr('value', QuizGenerateResources.Cancel);
        $('.sendquizlinkmail').attr('value', QuizGenerateResources.SendMail);
        $('#usermail').text(QuizGenerateResources.UserMail);
        $('#generatequiz').text(QuizGenerateResources.GenerateQuiz);
        $('#selectedfeatures').text(QuizGenerateResources.SelectedFeatures);
        $('#preview').text(QuizGenerateResources.Preview);
    });
});
//Triggers the event from LoadResources.js that will trigger the 'loadResources' event which will load 
//the resources ad will free the document.ready() event
$('head').trigger('registerLoadResources');