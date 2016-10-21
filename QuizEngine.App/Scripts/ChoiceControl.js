function ChoiceControl() {

    var selectedValue = "select";
    var initialValue = "update";
    var countMap = {};
    var _questionGUID;
    var _questionType;
    var _isEditable;
    var _choicePozition = 0;

    // the init variable that represents the initialization of the var's in thecontrol
    init = function (questionGUID, questionType, isEditable, serviceURL) {
        $(".choicesection").append("<div class='labelColumn'>" +
										"<label class='label choiceLabel'>" + QuestionResources.QuestionTypeLabel + "</label>" +
									"</div>" +
									"<div class='choicecontainer controlColumn' >" +
										"<div class='questiontypeselect'></div> " +
										"<div class=\"separator\"></div>" +
										"<div class='choiceoptions FreeText'></div>" +
										"<div class='choiceoptions MultiChoice' ></div> " +
										"<div class='choiceoptions Single' ></div>" +
										"<div class='choiceoptions TrueFalse' ></div> " +
										"<div class='choiceoptions Weighted' ></div>" +
									"</div>");
        _questionGUID = questionGUID;
        _questionType = questionType;
        _isEditable = isEditable;
        createRadioButtons();

        $(".choicesection .choiceoptions").hide();
        if (isEditable == true) {
            GetInfoForUpdate(serviceURL);
        }

        $(".choicecontainer.controlColumn").children().each(function () {
            if (!$(this).hasClass('questiontypeselect')) {
                $(this).sortable();
                $(this).disableSelection();
            }
        });


        BindEvents();
        $(".choicesection .choiceoptions." + selectedValue).slideDown();
    }

    function createRadioButtons() {
        for (var key in QuestionType) {
            if (QuestionType.hasOwnProperty(key)) {

                $(".questiontypeselect").append('<div class="popupRow">\
													<div class="choicecontrol">\
														<input type="radio" class="questiontype ' + key + ' choiceselect radiobutton" name="questiontype" value="' + key + '" />' +
													'</div>' +
													'<div class="choicelabel">\
														<label class="label">' + QuestionResources[key] + '</label>\
													</div>\
												</div>');
                if (QuestionType[key] == _questionType && _isEditable == 'False') {
                    $(".questiontypeselect ." + key).prop('checked', true);
                    selectedValue = key;
                    if (countMap[selectedValue] == null) {
                        countMap[selectedValue] = 0;
                    }
                    createTemplate();
                }
            }
        }
    }

    // populates the itnerface items with info5rmation from the database
    function GetInfoForUpdate(serviceUrl) {
        var allOptions = [];
        var questionID = { questionGUID: _questionGUID, questionType: _questionType };
        $.ajax({
            type: "POST",
            url: serviceUrl,
            data: JSON.stringify(questionID),
            contentType: "application/json; charset=utf-8",
            dataType: 'json',
            success: function (data) {
                var jsonostr = data.d;
                var jsonobj = eval('(' + jsonostr + ')');
                for (i in jsonobj) {
                    var option;
                    if (_questionType != '4') {
                        option = { AnswerText: jsonobj[i]["AnswerText"], IsCorrect: jsonobj[i]["IsCorrect"] };
                    } else {
                        option = { AnswerText: jsonobj[i]["AnswerText"], Value: jsonobj[i]["Value"] };
                    }

                    allOptions.push(option);
                }
                PopulateFormForUpdate(allOptions, _questionType);
            }
        });

    }

    // used to create the templates and populate them with information for the update part
    function PopulateFormForUpdate(choiceUpdate, questionType) {
        var form;
        var templateForms;

        if (questionType != undefined) {
            for (var key in QuestionType) {
                if (QuestionType.hasOwnProperty(key)) {
                    if (questionType == QuestionType[key]) { selectedValue = key; initialValue = key; }
                }
            }
            if (countMap[selectedValue] == null) {
                countMap[selectedValue] = 0;
            }

            form = $(".choiceoptions." + initialValue);
            $('.questiontype.' + initialValue).prop('checked', true);
            form.slideDown();
            for (var i = 0; i < choiceUpdate.length; i++) {
                createTemplate();
            }

            templateForms = $(".choiceoptions." + initialValue + " ." + initialValue);
            if (QuestionType[initialValue] == QuestionType.TrueFalse) {
                var poz = 0;
                templateForms.each(function () {
                    $(this).find('.choicetext').val(choiceUpdate[poz].AnswerText);
                    $(this).find('.choiceselect.true').first().prop('checked', (choiceUpdate[poz].IsCorrect == true) ? true : false);
                    $(this).find('.choiceselect.false').first().prop('checked', (choiceUpdate[poz].IsCorrect == false) ? true : false);
                    poz++;
                });
            }
            else if (QuestionType[initialValue] == QuestionType.Single || QuestionType[initialValue] == QuestionType.MultiChoice) {
                var poz = 0;
                templateForms.each(function () {
                    $(this).find('.choicetext').val(choiceUpdate[poz].AnswerText);
                    $(this).find('.choiceselect').first().prop('checked', (choiceUpdate[poz].IsCorrect == true) ? true : false);
                    poz++;
                });
            } else if (QuestionType[initialValue] == QuestionType.Weighted) {
                var poz = 0;
                templateForms.each(function () {
                    $(this).find('.choicetext').val(choiceUpdate[poz].AnswerText);
                    $(this).find('.valuetext').first().val(choiceUpdate[poz].Value);
                    poz++;
                });
            }
        }
    }

    function BindEvents() {
        // when selecting a choice
        $(".choicesection .questiontype").on("change", function (event) {
            event.preventDefault();
            event.stopPropagation();
            // first step is to slideup all remaining opened choices
            $(".choicesection .choiceoptions").slideUp();
            // next get the value of the radio button that was pushed
            selectedValue = $(this).val();

            if (selectedValue != initialValue) {
                // countMap is used to know how many templates for each choice have been added
                if (countMap[selectedValue] == null) {
                    countMap[selectedValue] = 0;
                }

                // next open the desired choice section and
                $(".choicesection .choiceoptions." + selectedValue).slideDown();
                // everytime a new questiontype is selected, 
                // all the templates from the last questiontype will be removed
                while (countMap[selectedValue] > 1) {
                    countMap[selectedValue] = countMap[selectedValue] - 1;
                    $(".choicesection .choiceoptions." + selectedValue + " ." + selectedValue).first().remove();
                }
                if (countMap[selectedValue] == 1) {
                    $(".choicesection .choiceoptions." + selectedValue + " ." + selectedValue + " .choicetext").val("");
                    if (selectedValue == GetFromIntQuestionType(QuestionType.Weighted)) {
                        $(".choicesection .choiceoptions." + selectedValue + " ." + selectedValue + " .valuetext").val("");
                    } else {
                        $(".choicesection .choiceoptions." + selectedValue + " ." + selectedValue + " .choiceselect").prop('checked', false);
                    }
                }
                // because everything is created dynamically we need not make sure not to 
                // insert a field everytime we click on a radio button
                // and this is what we do here
                if (countMap[selectedValue] == 0) {
                    createTemplate();
                }
            } else {
                $(".choicesection .choiceoptions." + initialValue).slideDown();
            }
        });

        // when desiring to add a single choice
        $(".choicesection .choiceoptions").on("click", ".add_choice_form", function (event) {
            event.preventDefault();
            event.stopPropagation();
            createTemplate();
        });

        // when desiring to remove a single choice
        $(".choicesection .choiceoptions").on("click", ".remove_choice_form", function (event) {
            event.preventDefault();
            event.stopPropagation();
            if (countMap[selectedValue] > 1) {
                countMap[selectedValue] = countMap[selectedValue] - 1;
                // remove the form to the page
                $(this).closest('.' + selectedValue).remove();
            }
        });
    }

    // saves the choices based on the questiontype (selectedValue) chosen
    values = function () {
        // used to hold the choices written by the user
        var allChoices = [];

        if (QuestionType[selectedValue] == QuestionType.FreeText) {
            choice = {
                ChoiceGUID: "00000000-0000-0000-0000-000000000000",
                QuestionGUID: _questionGUID,
                AnswerText: "",
                Value: null,
                IsCorrect: null,
                ChoicePosition: _choicePozition
            };
            allChoices.push(choice);
            return allChoices;
        }

        // in this section we get the text and true/false state for the selected choice
        // single, multiple, true_false have similar format and we can get the information in one way
        $(".choicesection .choiceoptions").find("." + selectedValue).each(function () {
            var choice;
            _choicePozition++;

            if (QuestionType[selectedValue] == QuestionType.TrueFalse) {

                if ($(this).find(".choiceselect.true").first().prop('checked') || $(this).find(".choiceselect.false").first().prop('checked')) {
                    choice = {
                        ChoiceGUID: "00000000-0000-0000-0000-000000000000",
                        QuestionGUID: _questionGUID,
                        AnswerText: "",
                        Value: null,
                        IsCorrect: $(this).find(".choiceselect").first().prop('checked'),
                        ChoicePosition: _choicePozition
                    };
                } else {

                }
            }
            else if (QuestionType[selectedValue] == QuestionType.Single || QuestionType[selectedValue] == QuestionType.MultiChoice) {
                choice = {
                    ChoiceGUID: "00000000-0000-0000-0000-000000000000",
                    QuestionGUID: _questionGUID,
                    AnswerText: $(this).find(".choicetext").val(),
                    Value: null,
                    IsCorrect: $(this).find(".choiceselect").first().prop('checked'),
                    ChoicePosition: _choicePozition
                };
            }
            else if (QuestionType[selectedValue] == QuestionType.Weighted) {
                choice = {
                    ChoiceGUID: "00000000-0000-0000-0000-000000000000",
                    QuestionGUID: _questionGUID,
                    AnswerText: $(this).find(".choicetext").val(),
                    Value: $(this).find(".valuetext").val(),
                    IsCorrect: null,
                    ChoicePosition: _choicePozition
                };
            }
            allChoices.push(choice);
        });

        return allChoices;
    }

    // used to create the template based on the questiontype(selectedvalue) chosen
    function createTemplate() {
        var template = getChoiceTemplate(selectedValue, countMap[selectedValue]);
        countMap[selectedValue] = countMap[selectedValue] + 1;
        // append the form to the page
        $(".choicesection .choiceoptions." + selectedValue).append(template);
    }


    // returns the desired type of choice
    selectedChoice = function () { return QuestionType[selectedValue]; }

    function CleanChoices() {
        $(".choicesection").children().each(function () { $(this).remove(); });
    }

    return {
        Init: init,
        QuestionType: selectedChoice,
        SelectedChoices: values,
        CleanChoices: CleanChoices
    }
}