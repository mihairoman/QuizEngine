function TagsControl(container) {
    var tagsSource = [];
    var selectedTags = [];
    var isAutocompleted = false;
    var autocompleteSource = [];
    var _questionID = emptyGUID;
    var _pageMode = 0;
    var jqContainer = $("#" + container);

    init = function (questionID, pageMode) {
        _questionID = questionID;
        _pageMode = pageMode;
        CheckPageMode();
        $("#tagsInputContainer").append(
            '<input id="tagsInput" class="textbox" name="tagsInput" />'
            );
    }

    /// Reads all tags that have been received from the WebService
    var ReadTagsWithSuccess = function (data) {
        var tempSource = JSON.parse(data.d);
        $.each(tempSource, function (index, value) {
            tagsSource.push({
                label:value.TagName,
                TagUID: value.TagUID
            });
        });
        AutocompleteInput(tagsSource, selectedTags);
    };


    ///Sends a Request to the webservice
    function LoadTags() {
        var deff =new $.Deferred();
        $.ajax({
            type: "POST",
            url: "../WebServices/QuestionService.asmx/GetTags",
            data: "{}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                deff.resolve(data);
            },
            failure: function (response) {
                deff.reject(null);
                alert(response.d);
            }
        });
        return deff.promise();
    };

    /// Returns an array of tags that represents the tags that are in the Database but are not selected
    function differenceOfTagArrays(tagsSource, selectedTags) {
        autocompleteSource = [];
        $.each(tagsSource, function (indexsource, valuesource) {
            var ok = true;
            $.each(selectedTags, function (indexselected, valueselected) {
                if (valuesource.TagUID === valueselected.TagUID) {
                    ok = false;
                    return false;
                }
            });
            if (ok === true) {
                ok = false;
                autocompleteSource.push(valuesource);
            }
        });

        return autocompleteSource;
    };

    /// Sets the source for the autocomplete widget and handels behavior for focusing on an item and selecting an item
    function AutocompleteInput(tagsSource, selectedTags) {
        jqContainer.find("#tagsInput").autocomplete({
            minLength: 1,
            source: differenceOfTagArrays(tagsSource, selectedTags),
            focus: function (event, ui) {
                $(this).val(ui.item.label);
            },
            select: function (event, ui) {
                var tempVar = {
                    label:ui.item.label,
                    TagUID: ui.item.TagUID
                };
                var checkSelected = false;
                for (var j = 0; j < selectedTags.length; j++)
                    if (selectedTags[j].label.toLowerCase() === tempVar.label.toLowerCase()) {
                        checkSelected = true;
                        break;
                    }
                if (checkSelected == false) {
                    selectedTags.push(tempVar);
                    DrawTag(ui.item.label, ui.item.TagUID);
                    isAutocompleted = true;
                    $(this).val('');
                    if ($('#tagsInput').autocomplete("instance") != undefined) {
                        jqContainer.find('#tagsInput').autocomplete('option', 'source', differenceOfTagArrays(tagsSource, selectedTags));
                    } else {
                        AutocompleteInput(tagsSource, selectedTags);
                    }
                }
                return false;
            }
        });
    };

    /// Handels click on the close image, deletes from selectedTags the selected tag and refreshes the datasource for the autocomplete
    function RemoveTagOnClick() {
        jqContainer.on('click', '.closeImage', function (e) {
            e.preventDefault();
            e.stopPropagation();
            $(this).closest('.tagItem').remove();
            while (selectedTags.length > 0) {
                selectedTags.pop();
            }
            $(".tagItem").each(function () {
                var tagSelected = {
                    label: $(this).find("span").first().text(),
                    TagUID: $(this).find("span").first().attr('guid-value')
                };
                selectedTags.push(tagSelected);
            });
            if ($('#tagsInput').autocomplete("instance") != undefined) {
                jqContainer.find('#tagsInput').autocomplete('option', 'source', differenceOfTagArrays(tagsSource, selectedTags));
            } else {
                AutocompleteInput(tagsSource, selectedTags);
            }
        });
    }
    
    function DrawTag(text, guid) {
      
    	jqContainer.find("#drowntags").append('	<div class="tagItem">\
													<div class="tagcontrol">\
														<img class="closeImage" src="../Images/close.png">\
													</div>\
													<div class="taglabel">\
														<label class="tagText" guid-value="' + guid + '">' + text + '</label>\
													</div>\
												</div>');
    }

    /// Populates selected tags and Fills the body with the selected tags(Used for updating a question) 
    function ReadSelectedTags(data) {
        var tempSource = JSON.parse(data.d);
        $.each(tempSource, function (index, value) {
            selectedTags.push({
                label: value.TagName,
                TagUID: value.TagUID
            });
            DrawTag(value.TagName, value.TagUID);
        });
        if ($('#tagsInput').autocomplete("instance") != undefined) {
            jqContainer.find('#tagsInput').autocomplete('option', 'source', differenceOfTagArrays(tagsSource, selectedTags));
        } else {
            AutocompleteInput(tagsSource, selectedTags);
        }
    };

    /// Sends a request for the tags that are associated to the updated question
    function LoadSelectedTags() {
        var deff =new $.Deferred();
        $.ajax({
            type: "POST",
            url: "../WebServices/QuestionService.asmx/GetTagsByQuestionID",
            data: "{\"questionID\":\"" + _questionID + "\"}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                deff.resolve(data);
            },
            failure: function (response) {
                debugger;
                alert(response.d);
            }
        });
        return deff.promise();
    }
   

    /// Add a new tag(that doesn't exist in the database)
    jqContainer.on('keyup', '#tagsInput', function (event, ui) {
        if (event.keyCode == 13 && $(this).val() != "") {
            event.preventDefault();
            event.stopPropagation();
            $("#errorlist").find('.tagsInputError').each(function () {
                $(this).remove();
            });
            if (isAutocompleted == false) {
                $('#tagsInput').autocomplete("close");
                var tempVar = {
                    label: $(this).val().trim(),
                    TagUID: emptyGUID
                };
                var ok = true;
                if (tempVar.label.length > 50)
                {
                	$("#errors").children().append("<li class='tagsInputError'>" + QuestionResources.QuestionOverflowTagTextError + "</li>");
                	$("#tagsInput").addClass("incorrectfield");
                    $("#errors").slideDown();
                    ok = false;
                }else{
                    for (var i = 0; i < tagsSource.length; i++)
                        if (tagsSource[i].label.toLowerCase() === tempVar.label.toLowerCase()) {
                            tempVar.label = tagsSource[i].label;
                            tempVar.TagUID = tagsSource[i].TagUID;
                            break;
                        }
                    for (var j = 0; j < selectedTags.length; j++)
                        if (selectedTags[j].label.toLowerCase() === tempVar.label.toLowerCase()) {
                        	$("#errors").children().append("<li class='tagsInputError'>" + QuestionResources.QuestionDuplicateTagError + "</li>");
                        	$("#tagsInput").addClass("incorrectfield");
                            $("#errors").slideDown();
                            ok = false;
                            break;
                        }
                }
                if (ok == true) {
                    selectedTags.push(tempVar);
                    DrawTag(tempVar.label, tempVar.TagUID);
                    $(this).val('');
                }
            }               
            else {
                isAutocompleted = false;
            }
            if ($('#tagsInput').autocomplete("instance") != undefined) {
                jqContainer.find('#tagsInput').autocomplete('option', 'source', differenceOfTagArrays(tagsSource, selectedTags));
            } else {
                AutocompleteInput(tagsSource, selectedTags);
            }
        }
    });
    /// Renders the Control accordding to the PageMode
    function CheckPageMode() {
        $.when(LoadTags())// function that will be used for getting Tags from WebService
            .then(function (data) {
                ReadTagsWithSuccess(data);
                if (_pageMode === PageMode.View) {
                    $.when(LoadSelectedTags())
                        .then(function (datas) {
                            ReadSelectedTags(datas);
                            jqContainer.find('#tagsInput').prop('readonly', true);
                            $(".tagItem").each(function () { $(this).prop('disabled', 'disabled'); });
                        });
                } else if (_pageMode === PageMode.Update) {
                    $.when(LoadSelectedTags())
                       .then(function (dataq) {
                           ReadSelectedTags(dataq);
                           RemoveTagOnClick();
                       });
                } else {
                    RemoveTagOnClick();
                };
            });
    }
    //Creates an array of tags which will have the same variables as the one from server side
    function DecodeTags() {
        var decodedTags = [];
        $.each(selectedTags, function (index, value) {
            tempDecoded = {
                TagUID: value.TagUID,
                TagName: value.label
            };
            decodedTags.push(tempDecoded);
        });
        return decodedTags;
    }
    function CleanTags() {
        $(".tagItem").each(function () {
            $(this).remove();
        });
        $("#tagsInput").remove();
    }

    // Define public API for this control
    return {
        Init: init,
        SelectedTags: DecodeTags,
        CleanTags:CleanTags
    }
};