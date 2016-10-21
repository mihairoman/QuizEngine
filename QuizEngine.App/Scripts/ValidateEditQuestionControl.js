/// Validates all data that are sent to the WebService
function ValidateQuestion(selectedTags, choices, question) {
	var valid = true;
	var decimalOnly = /^\s*?[0](\.\d{1,5})?\s*$/;
	var errString = [];
	if (question.Text == "") {
		valid = false;
		errString.push({ error: QuestionResources.QuestionTextError });
		$("#QuestionTextBox").addClass("incorrectfield");
	}

	if (question.LevelGUID == emptyGUID) {
		valid = false;
		errString.push({ error: QuestionResources.QuestionLevelError });
		$("#questionLevelDropDownList").addClass("incorrectfield");
	}
	if (question.CategoryGUID == emptyGUID) {
		valid = false;
		errString.push({ error: QuestionResources.QuestionCategoryError });
		$("#questionCategoryDropDownList").addClass("incorrectfield");
	}
	if (selectedTags.length < 1) {
		valid = false;
		errString.push({ error: QuestionResources.QuestionTagError });
		$("#tagsInput").addClass("incorrectfield");
	}

	if (((question.QuestionType == QuestionType.FreeText) && choices.length != 1) || ((question.QuestionType == QuestionType.TrueFalse) && choices.length != 1) ||
		((question.QuestionType != QuestionType.FreeText && (question.QuestionType != QuestionType.TrueFalse)) && choices.length < 2)) {
		valid = false;
		errString.push({ error: QuestionResources.QuestionNotEnoughChoicesError });
	}


	var checked = false;
	var checkWeighted = true;
	for (var i = 0; i < choices.length; i++) {
		if (question.QuestionType == QuestionType.TrueFalse) {
			if (choices[i] == null || choices[i] == undefined) {
				valid = false;
				errString.push({ error: QuestionResources.QuestionTrueFalseError });
			}
		}
		if (question.QuestionType == QuestionType.Single || question.QuestionType == QuestionType.MultiChoice)
			if (choices[i].IsCorrect == true)
				checked = true;
		if (question.QuestionType == QuestionType.Weighted && choices[i].Value == null)
			checkWeighted = false;
		if (choices[i] != null && choices[i] != undefined) {
			if (choices[i].AnswerText == '' && question.QuestionType != QuestionType.FreeText && question.QuestionType != QuestionType.TrueFalse) {
				valid = false;
				errString.push({ error: QuestionResources.QuestionChoiceTextError });
				break;
			}
		}
		if (question.QuestionType == QuestionType.Weighted) {
			if (!decimalOnly.test(choices[i].Value)) {
				valid = false;
				errString.push({ error: QuestionResources.QuestionChoiceWeightedValueFormatError });
				$(".valuetext").addClass("incorrectfield");
				break;
			}
		}
	}

	if (checked == false && (question.QuestionType != QuestionType.Weighted) && (question.QuestionType != QuestionType.FreeText)
		&& (question.QuestionType != QuestionType.TrueFalse)) {
		valid = false;
		errString.push({ error: QuestionResources.QuestionNumberOfCorrectChoicesError });
	}

	if (checkWeighted == false) {
		valid = false;
		errString.push({ error: QuestionResources.QuestionChoiceValueNeededError });
		$(".valuetext").addClass("incorrectfield");
	}
	return {
		isValid: valid,
		Errors: errString
	}
}