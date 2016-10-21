function getChoiceTemplate(type, index) { //returns set of form fields as a string

    if (type == "Single") {
        return $('\
            <div class="Single popupRow" style="background-color: #F8F8F8;">\
                <div class="choicecontrolcheck choiceradiocolumn">\
					<div class="radiocontrolcolumn">\
						<input type="radio" class="choiceselect radiobutton" name="choiceradio" />\
					</div>\
					<div class="radiolabelcolumn">\
						<label for="choiceradio" class="label" >' + QuestionResources.ChoiceIsCorrectLabel + '</label>\
					</div>\
                </div>\
                <div class="choicecontroltext choicetextcolumn">\
					<div class="choicelabelcolumn">\
						<label for="choicetext" class="label">' + QuestionResources.ChoiceText + '</label>\
					</div>\
					<div class="choicecontrolcolumn">\
						<textarea type="text" class="choicetext" rows="1" name="choicetext' + index + '"/>\
					</div>\
                </div>\
                <div class="choicecontrolbtns choicebuttoncolumn">\
                    <a href="#"  class="add_choice_form popupButton"><img src="../Images/add.png" style="float: left; width: 42%; height: 90%; margin-right: 0.4em"></a>\
                    <a href="#"  class="remove_choice_form popupButton" ><img src="../Images/remove.png" style="float: left; width: 42%; height: 90%;"></a>\
                </div>\
          </div>\
        ');
    }
    else if (type == "MultiChoice") {
        return $('\
            <div class="MultiChoice popupRow" style="background-color: #F8F8F8;">\
                <div class="choicecontrolcheck choiceradiocolumn">\
					<div class="radiocontrolcolumn">\
						<input type="checkbox"  class="choiceselect radiobutton" name="choicecheck" />\
					</div>\
					<div class="radiolabelcolumn">\
						<label class="label">' + QuestionResources.ChoiceIsCorrectLabel + '</label>\
					</div>\
                </div>\
                <div class="choicecontroltext choicetextcolumn"> \
					<div class="choicelabelcolumn">\
						<label class="label">' + QuestionResources.ChoiceText + '</label> \
					</div>\
					<div class="choicecontrolcolumn">\
						<textarea type="text" class="choicetext" rows="1" name="choicetext' + index + '"/> \
					</div>\
                </div> \
                <div class="choicecontrolbtns choicebuttoncolumn">\
                    <a href="#"  class="add_choice_form popupButton"><img src="../Images/add.png" style="float: left; width: 42%; height: 90%; margin-right: 0.4em"></a>\
                    <a href="#"  class="remove_choice_form popupButton" ><img src="../Images/remove.png" style="float: left; width: 42%; height: 90%;"></a>\
                </div>\
          </div>\
        ');
    }
    else if (type == "Weighted") {
        return $('\
            <div class="Weighted popupRow" style="background-color: #F8F8F8;">\
                <div class="choicecontroltext Value">\
					<div class="valuelabelcolumn">\
						<label class="label">' + QuestionResources.ChoiceValue + '</label>\
					</div>\
					<div class="valuecontrolcolumn">\
						<input type="text" class="valuetext" name="valuetext' + index + '" />\
	                </div>\
                </div>\
                <div class="choicecontroltext choicetextcolumn"> \
					<div class="choicelabelcolumn">\
						<label class="label">' + QuestionResources.ChoiceText + '</label>\
					</div>\
					<div class="choicecontrolcolumn">\
						<textarea  type="text" class="choicetext" rows="1" name="choicetext' + index + '"/>\
					</div>\
                </div>\
                <div class="choicecontrolbtns choicebuttoncolumn">\
                    <a href="#"  class="add_choice_form popupButton"><img src="../Images/add.png" style="float: left; width: 42%; height: 90%; margin-right: 0.4em"></a>\
                    <a href="#"  class="remove_choice_form popupButton" ><img src="../Images/remove.png" style="float: left; width: 42%; height: 90%;"></a>\
                </div>\
          </div>\
        ');
    }
    else if (type == "TrueFalse") {
        return $('\
            <div class="TrueFalse popupRow" style="background-color: #F8F8F8;">\
                <div class="choicecontrolcheck choiceradiocolumn">\
					<div class="truefalsecontrolcolumn">\
						<input type="radio" class="choiceselect true radiobutton" name="choiceradio' + index + '" />\
					</div>\
					<div class="truefalselabelcolumn">\
						<label class="label">' + QuestionResources.ChoiceTrue + '</label>\
					</div>\
                </div>\
                <div class="choicecontrolcheck choiceradiocolumn">\
					<div class="truefalsecontrolcolumn">\
						<input type="radio" class="choiceselect false radiobutton" name="choiceradio' + index + '" />\
	                </div>\
					<div class="truefalselabelcolumn">\
						<label class="label">' + QuestionResources.ChoiceFalse + '</label>\
					</div>\
                </div>\
          </div>\
        ');
    }
}