    $('head').on('loadResources', function () {
        QuestionResources = {
            //Page ManageQuestions
            "QuestionsTitle": "Manage Questions",
            "QuestionsOnPage": "Questions On Page",
            "BtnInsertQuestion": "Insert",
            "BtnApplyFilter": "Apply Filter",
            "BtnResetFilter": "Reset Filter",
            //PopUp ManageQuestions
            "QuestionTextLabel": "Question Text",
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
            //Errors on PopUp ManageQuestions
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
            "QuestionOverflowTagTextError": "Tags Text should be shorter than 50 characters. \n",
            //Table ManageQuestions
            "TableTextSort": "Text",
            "TableCategorySort": "Category",
            "TableLevelSort": "Level",
            "TableTagSort": "Tag",
            "TableTypeSort": "Type",
            "TableEdit": "Edit",
            "TableDelete": "Delete",
            "BtnFilters": "Filters",
            "FiltersSelectLevel": "Select Level",
            "FiltersSelectCategory": "Select Category",
            "FiltersSelectTags": "Select Tags",
            "FiltersSelectTypes": "Select Types",           

        };
        MasterResources = {
            "CategoryDescriptionRes": "Manage categories",
            "LevelDescriptionRes": "Manage levels",
            "QuestionDescriptionRes": "Manage questions",
            "UserDescriptionRes": "Manage users",
            "BasicQuizDescriptionRes": "Generate a random quiz",
            "TemplateQuizDescriptionRes": "Take a generated quiz",
            "ScoreQuizDescriptionRes": "Score quizzes that have free text",
            "HistoryDescriptionRes": "History of quizzes",
            "DownloadQuizDescriptionRes": " Download or generate link",
            "DownloadTemplateDescriptionRes": " Download or generate link",
            "SettingsRes": "Settings",
            "GenerateQuizRes": "Generate Quiz",
            "AboutRes": "About"

        };
        QuizHistoryResources = {
            "QuizHistory":"Quiz History",
            "ChooseUserLabel": "Choose User",
            "UserNameColoum": " User Name",
            "ResultColoum": "Result [%]",
            "QuizDateColoum": "Quiz Date",
            "CategoryNameColoum": "Category",
            "LevelNameColoum": "Level",
            "QuizTimeColoum": "Quiz Time",
            "TakeOnlineColoum": "Taken Online?",
            "CorectColoum": "Correct Quiz",
            "ShowColoum": "Show Quiz",
		"QuizPerPage": "Quizzes on Page"
            
        };

        GenerateQuizResources = {
            "QuizPerPageLabel": "Quizzes on Page",
            "QuizNameCol": "Quiz Name",
            "QuestionNrCol": "Question Number",
            "TakeQuizCol": "Start the Quiz"

        };

        QuizShowResource = {
            "UserNameLabel": "User Name ",
            "QuizReusltLabel": "Result",
            "QuizDateLabel": "Quiz Date"
        };

        QuizGenerateResources = {
            "QuizTime": "Quiz Time",
            "LevelError": "Please select a level.",
            "CategoryError": "Please select a category.",
            "TagError": "Please add some Tags.",
            "TypeError": "Please add the question types in order to continue.",
            "IntegerError": "Please enter an integer value.",
            "QuestionNumberError": "Please enter a number of questions between 5 and 50.",
            "GenerateLink": "Generate Link",
            "TakeQuiz": "Take Quiz",
            "DownloadDOC": "Download DOC",
            "DownloadPDF": "Download PDF",
            "Cancelbutton": "Cancel",
            "SelectCategoryLabel": "Select Category",
            "SelectLevelLabel": "Select Level",
            "SelectTagsLabel": "Select Tags",
            "SelectQuestionTypesLabel": "Select Types",
            "NumberOfQuestions": "Number of Questions",
            "SelectedCategoryLabel": "Selected Category",
            "SelectedLevelLabel": "Selected Level",
            "SelectedTagsLabel": "Selected Tags",
            "SelectedQuestionTypesLabel": "Selected Types",
            "ListOfQuestions": "Question List",
            "QuestionText": "Question Text",
            "QuestionType": "Question Type",
            "TagName": "Tag Name",
            "SaveTemplate": "Save Template",
            "TemplateSave": "Your quiz has been saved.",
            "NoQuestionsError": "There are no questions with the selected filters.",
            "NotEnoughQuestionsError": "There are not enough questions with the selected filters.",
            "TemplateText": "Template Name",
            "Save": "Save",
            "Cancel": "Cancel",
            "SendMail": "Send Mail",
            "UserMail": "Users",
            "GenerateQuiz": "Generate Basic Quiz",
            "SelectedFeatures": "Selected Features",
            "Preview": "Generated quiz preview",
            "PopUpError": "You have already saved this quiz as template!",
            "ReplaceText": "Delete and replace",
            "TemplateNameTooShortError": "The provided Template Name should have at least 1 character.",
            "MailDeliveryTooFewError": "No users were selected.",
            "MailDeliveryError": "An error occured while sending the e-mail.",
            "MailDeliverySuccess": "Mail(s) delivered successfully.",
            "TemplateNameError": "This template name has already been taken.",
            "TemplateNameTooLong": "The template name is too long.",
            "InvalidTimeFormatError": "Please insert a valid time format ([minutes]:[seconds]).",
            "InvalidSecondsValueError": "The provided value for the seconds field should be a number between 0 and 59.",
            "InvalidMinutesValueError": "Please provide a value for the minutes field."
        };
        CategoryResources = {
            "Categories": "Manage Categories",
            "CategoryName": "Category Name",
            "CorrespondingNumberOfQuestions": "Questions / Category",
            "Edit": "Edit",
            "Delete": "Delete",
            "CategoryPopupSaveButton": "Save",
            "CategoryPopupCancelButton": "Cancel",
            "CategoryNameTooShortError": "The provided Category Name should have at least 1 character.",
            "CategoryNameTooLongError": "The provided Category Name should have at most 50 characters.",
            "CategoryNameExistsError": "The provided Category Name already exists.",
            "CategoryInsertError": "An error occured while inserting the provided Category.",
            "CategoryEditError": "An error occured while editing the provided Category.",
            "CategoryDeleteError": "The selected Category cannot be deleted.",
            "CategoryInsert": "Insert Category",
            "CategoryEdit": "Edit Category"
        };
        ScoreQuizResources = {
            "ScoreQuizzes": "Score Quizzes",
            "LabelOnPageScore": "Quizzes on Page    ",
            "UserNameScore": "User Name",
            "QuizDateScore": "Quiz Date",
            "Correct": "Correct",
            "CategoryNameScore": "Category Name",
            "LevelNameScore": "Level Name"

        };

        LevelResources = {
            "Levels":"Manage Levels",
            "LevelName": "Level Name",
            "LevelDifficultyLabel": "Difficulty",
            "LevelPopupSaveButton": "Save",
            "LevelPopupCancelButton": "Cancel",
            "Edit": "Edit",
            "Delete":"Delete",
            "LevelNameTooShortError": "The provided Level Name should have at least 1 character.",
            "LevelNameTooLongError": "The provided Level Name should have at most 50 characters.",
            "LevelDifficultyError": "The provided Difficulty must be a number between 1 and 5.",
            "LevelDeleteError": "The selected Level cannot be deleted.",
            "LevelInsertError": "An error occured while inserting the provided Level.",
            "LevelEditError": "An error occured while editing the provided Level.",
            "LevelInsert": "Insert Level",
            "LevelEdit": "Edit Level",
            "LevelNameExistsError": "The provided Level Name already exists.",
            "DifficultyExistsError": "The provided Difficulty already exists."
        };
	UserResources = {
	    "Users": "Manage Users",
        "UsersOnPage":"Users on Page",
		"UsernameLabel": "Username",
		"UsernameRestrictionMessage": "* The provided Username should be an e-mail address, or a domain name.",
		"IsDomainNameLabel": "Is Domain Name",
		"UsernameCheckboxMessage": "* Please check this box if the preferred Username is a domain name.",
		"UserTypeLabel": "User Type",
		"IsActiveLabel": "Is Active",
		"PermissionsLabel": "Permissions",
		"ManageUsers": "Manage Users",
		"ManageQuestions": "Manage Questions",
		"ManageCategories": "Manage Categories",
		"ManageLevels": "Manage Levels",
		"ManageTags": "Manage Tags",
		"GenerateQuizzes": "Generate Quizzes",
		"ScoreQuizzes": "Score Quizzes",
		"QuizHistory": "Quiz History",
		"AdminDownloadGeneratedTest": "Download Generated Test",
		"AdminDownloadPredefinedTest": "Download Predefined Test",
		"FreeTextQuestionsPendingAdminResponse": "Free Text Question Validation",
		"UserPopupSaveButton": "Save",
		"UserPopupCancelButton": "Cancel",
		"UsernameNotDomainError": "The provided Username is not a valid Domain Name.",
        "UsernameDomainTooLongError": "The provided Domain Name has too many characters.",
        "UsernameNotEmailError": "The provided Username is not a valid E-mail address.",
        "UsernameEmailTooLongError": "The provided E-mail address has too many characters.",
		"UsernameEmptyError": "Please fill the Username field.",
		"UserTypeNotSelectedError": "Please choose a User Type.",
		"UserInsertError": "An error occured while inserting the provided User.",
		"UserEditError": "An error occured while editing the provided User.",
        "UsernameAlreadyExistsError": "The provided Username already exists.",
		"UserInsert": "Insert User",
		"UserEdit": "Edit User",
		"UsersTitle": "Manage Users",
		"TableUsername": "Username",
		"TableUserType": "User Type",
		"TableIsActive": "Is Active",
		"TableDeactivate": "Deactivate",
		"TableEdit": "Edit",
        "InsertButton": "Insert"
	};

	OnlineWizardResources = {
	    "PreviousButton": "Previous",
	    "NextButton": "Next",
	    "SubmitButton": "Submit",
	    "ListQuestion": "Question",
        "SubmissionMessage" : "Are you sure you want to submit?"
	};

	$('.label.questiontextLabel').text(QuestionResources.QuestionTextLabel);
	$('.label.levelLabel').text(QuestionResources.LevelLabel);
	$('.label.categorylabel').text(QuestionResources.CategoryLabel);
	$('.label.tagsInputLabel').text(QuestionResources.TagsLabel);
	AdminDownloadGeneratedTest = {
	        "RandomGeneratedTest": "Random Generated Quizzes",
	        "LblGeneratedTest":"Quizzes on Page",
            "LevelName": "Level Name",
            "LevelDifficulty": "Level Difficulty",
            "CategoryName": "Category Name",
            "DownloadOrLink": "Download Quizzes",
            "RandomGeneratedQuizzes": "Random Generated Quizzes",
            "RandomGeneratedQuizzesOnPage": "Random generated quizzes on Page",
            "CreateLink": "Create Link",
            "ErrorMakingAction": "An error occured"
        };
	AdminDownloadPredefinedTest = {
	        "PredefinedTest": "Predefined Quizzes",
	        "LblPredefinedTest": "Quizzes on Page",
            "TypeName": "Template Name",
            "DownloadOrLink": "Download Quizzes",
            "PredefinedQuizzes": "Predefined Quizzes",
            "PredefinedQuizzesOnPage": "Predefined Quizzes on Page",
            "ErrorDownloadingOrGenerating": "An error occured"
        };

        AdminGenerateOnlineLink = {
            "UsersGenerateLink": "Send Email to the Chosen User",
	        "LblUsersGenerateLink":"Users on Page",
            "UserName": "User Name",
            "UserType": "User Type",
            "SendMail": "Send Email",
            "ErrorSending": "Error sending the email",
            "Success":"Mail delivered succesfully"
        };

        FreeTextQuestionsPendingAdminResponse = {
            "TitleLabel": "Free text questions validation"
        };


        $.holdReady(false);
        $(document).ready(function () {

        $('.label.questiontextLabel').text(QuestionResources.QuestionTextLabel);
            $('.label.levelLabel').text(QuestionResources.LevelLabel);
            $('.label.categorylabel').text(QuestionResources.CategoryLabel);
            $('.label.tagsInputLabel').text(QuestionResources.TagsLabel);
           
            $('#scorequizzes').text(ScoreQuizResources.ScoreQuizzes);
            $('#labelonpagescore').text(ScoreQuizResources.LabelOnPageScore);
            $('#usernamescore').text(ScoreQuizResources.UserNameScore);
            $('#quizdatescore').text(ScoreQuizResources.QuizDateScore);
            $('#categorynamescore').text(ScoreQuizResources.CategoryNameScore);
            $('#levelnamescore').text(ScoreQuizResources.LevelNameScore);
            $('#correctlabel').text(ScoreQuizResources.Correct);
  
            $('#QuizHistory').text(QuizHistoryResources.QuizHistory);
            $('#selectUserLabel').text(QuizHistoryResources.ChooseUserLabel);
            $('#userNameCol').text(QuizHistoryResources.UserNameColoum);
            $('#resultCol').text(QuizHistoryResources.ResultColoum);
            $('#quizDateCol').text(QuizHistoryResources.QuizDateColoum);
            $('#categoryNameCol').text(QuizHistoryResources.CategoryNameColoum);
            $('#levelNameCol').text(QuizHistoryResources.LevelNameColoum);
            $('#quizTimeCol').text(QuizHistoryResources.QuizTimeColoum);
            $('#takeOnlineCol').text(QuizHistoryResources.TakeOnlineColoum);
            $('#correctCol').text(QuizHistoryResources.CorectColoum);
            $('#showCol').text(QuizHistoryResources.ShowColoum);
            $('#rowPerPageLabel').text(QuizHistoryResources.QuizPerPage);

            $('#quizPerPageLabel').text(GenerateQuizResources.QuizPerPageLabel);
            $('#QuizNameCol').text(GenerateQuizResources.QuizNameCol);
            $('#QuestionNrCol').text(GenerateQuizResources.QuestionNrCol);
            $('#TakeQuizCol').text(GenerateQuizResources.TakeQuizCol);

            $('#levelName').text(AdminDownloadGeneratedTest.LevelName);
            $('#levelDifficulty').text(AdminDownloadGeneratedTest.LevelDifficulty);
            $('#categoryName').text(AdminDownloadGeneratedTest.CategoryName);
            $('#downloadOrLink').text(AdminDownloadGeneratedTest.DownloadOrLink);
            $('#RandomGeneratedQuizzes').text(AdminDownloadGeneratedTest.RandomGeneratedQuizzes);
            $('#RandomGeneratedQuizzesOnPage').text(AdminDownloadGeneratedTest.RandomGeneratedQuizzesOnPage);

            $('#typename').text(AdminDownloadPredefinedTest.TypeName);
            $('#downloadOrLink').text(AdminDownloadPredefinedTest.DownloadOrLink);
            $('#PredefinedQuizzes').text(AdminDownloadPredefinedTest.PredefinedQuizzes);
            $('#PredefinedQuizzesOnPage').text(AdminDownloadPredefinedTest.PredefinedQuizzesOnPage);

            



            $('#Questions').text(QuestionResources.Questions);
            $('#QuestionsOnPage').text(QuestionResources.QuestionsOnPage);

            $('#Users').text(UserResources.Users);
            $('#UsersOnPage').text(UserResources.UsersOnPage);
            $('#levelnamedownload').text(ScoreQuizResources.LevelNameScore);
            $('#categorynamedownload').text(ScoreQuizResources.CategoryNameScore);
            $('#typenamedownload').text(ScoreQuizResources.CategoryNameScore);
            $('#create').text(AdminDownloadGeneratedTest.CreateLink);

            $('#sendmail').text(AdminGenerateOnlineLink.SendMail);
            $('#usernamelink').text(AdminGenerateOnlineLink.UserName);
            $('#usertypelink').text(AdminGenerateOnlineLink.UserType);
            $('#onlinelink').text(AdminGenerateOnlineLink.UsersGenerateLink);
            $('#lblonlinelink').text(AdminGenerateOnlineLink.LblUsersGenerateLink);
            $('#generatetest').text(AdminDownloadGeneratedTest.RandomGeneratedTest);
            $('#lblgeneratetest').text(AdminDownloadGeneratedTest.LblGeneratedTest);
            $('#predefinedtest').text(AdminDownloadPredefinedTest.PredefinedTest);
            $('#lblpredefinedtest').text(AdminDownloadPredefinedTest.LblPredefinedTest);

            $('#userName').text(QuizShowResource.UserNameLabel);
            $('#quizResult').text(QuizShowResource.QuizReusltLabel);
            $('#quizDate').text(QuizShowResource.QuizDateLabel);

        });
    });
    //Triggers the event from LoadResources.js that will trigger the 'loadResources' event which will load 
    //the resources ad will free the document.ready() event
    $('head').trigger('registerLoadResources');