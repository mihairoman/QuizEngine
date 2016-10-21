﻿$(document).ready(function () {
	$("#Manage").show();
	var _id;
	var _mode;
	var _categories = [];
	var _c;

	PopulateTable();

	dialog = $("#CategoryManagementPopup").dialog({
		width: 550,
		minHeight: 232,
		maxHeight: 232,
		modal: true,
		closeOnEscape: true,
		resizable: false,
		draggable: true,
		autoOpen: false,
		buttons: [
			{
				text: CategoryResources.CategoryPopupSaveButton,
				click: function () {
					var ok = true;
					$("#errorlist").empty();
					$("#errorlist").slideUp();
					$("#txtCategoryName").removeClass("incorrectfield");
					if (!$("#txtCategoryName").val()) {
						$("#errorlist").append("<li>" + CategoryResources.CategoryNameTooShortError + "</li>");
						$("#txtCategoryName").addClass("incorrectfield");
						ok = false;
					}
					if ($("#txtCategoryName").val().length > 50) {
						$("#errorlist").append("<li>" + CategoryResources.CategoryNameTooLongError + "</li>");
						$("#txtCategoryName").addClass("incorrectfield");
						ok = false;
					}
					var i = 0;
					while (i != _categories.length && _categories[i].CategoryName != $("#txtCategoryName").val())
						i++;
					if (i != _categories.length) {
						$("#errorlist").append("<li>" + CategoryResources.CategoryNameExistsError + "</li>");
						$("#txtCategoryName").addClass("incorrectfield");
						ok = false;
					}
					if (ok) {
						var newCategory = {
							CategoryUID: _id,
							CategoryName: $("#txtCategoryName").val()
						}
						if (_mode == 1)
							InsertCategory(newCategory);
						else
							UpdateCategory(newCategory);
					}
					else {
						$("#errorlist").slideDown();
					}
				}
			},
			{
				text: CategoryResources.CategoryPopupCancelButton,
				click: function () {
					dialog.dialog("close");
				}
			}
		]
	});

	$('#categoryContainer').on('click', '#btnEdit', function () {
		$("#manageerrorlist").empty();
		$("#manageerrorlist").slideUp();
		$("#txtCategoryName").removeClass("incorrectfield");
		var index = $(this).closest('tr').index();
		_id = _categories[index - 1].CategoryUID;
		$('#txtCategoryName').val(_categories[index - 1].CategoryName);
		_mode = 2;
		dialog.dialog({ title: CategoryResources.CategoryEdit });
		$("#errorlist").empty();
		$("#errorlist").slideUp();
		dialog.dialog("open");
	});

	$('#categoryContainer').on('click', '#btnDelete', function () {
		$("#manageerrorlist").empty();
		$("#manageerrorlist").slideUp();
		var index = $(this).closest('tr').index();
		_id = _categories[index - 1].CategoryUID;
		var categoryName = _categories[index - 1].CategoryName;
		var newCategory = {
			CategoryUID: _id,
			CategoryName: categoryName
		}
		DeleteCategory(newCategory);
	});

	$(".btnInsert").on('click', function () {
		$("#manageerrorlist").empty();
		$("#manageerrorlist").slideUp();
		$("#txtCategoryName").removeClass("incorrectfield");
		$('#txtCategoryName').val("");
		_id = '00000000-0000-0000-0000-000000000000';
		_mode = 1;
		dialog.dialog({ title: CategoryResources.CategoryInsert });
		$("#errorlist").empty();
		$("#errorlist").slideUp();
		dialog.dialog("open");
	});

	function InsertCategory(category) {
		var x = { category: JSON.stringify(category) }
		InsertCategoryName();
	}

	function InsertCategoryName() {
	    var req = new PEService();
	    req.ExecuteCall("CategoryService.asmx", "InsertCategory", JSON.stringify(x), OnInsertCategorySuccess, OnInsertCategoryFailure);
	}
	function OnInsertCategorySuccess() {
	    $(".ui-dialog-content").dialog().dialog("close");
	    PopulateTable();
	}
	function OnInsertCategoryFailure() {
	    $("#errorlist").append("<li>" + CategoryResources.CategoryInsertError + "</li>");
	}

	function UpdateCategory(category) {
		var x = { category: JSON.stringify(category) }
		UpdateCategoryName();
	}

	function UpdateCategoryName() {
	    var req = new PEService();
	    req.ExecuteCall("CategoryService.asmx", "UpdateCategory", JSON.stringify(x), OnUpdateCategorySuccess, OnUpdateCategoryFailure);
	}
	function OnUpdateCategorySuccess() {
	    $(".ui-dialog-content").dialog().dialog("close");
	    PopulateTable();
	}
	function OnUpdateCategoryFailure() {
	    $("#errorlist").append("<li>" + CategoryResources.CategoryEditError + "</li>");
	}

	function DeleteCategory(category) {
		var x = { category: JSON.stringify(category) }
		DeleteCategoryName();
	}

	function DeleteCategoryName() {
	    var req = new PEService();
	    req.ExecuteCall("CategoryService.asmx", "DeleteCategory", JSON.stringify(x), OnDeleteCategorySuccess, OnDeleteCategoryFailure);
	}
	function OnDeleteCategorySuccess(response) {
	    if (response.d != null) {
	        $("#manageerrorlist").append("<li>" + CategoryResources.CategoryDeleteError + "</li><br/>");
	        $("#manageerrorlist").slideDown();
	    }
	    PopulateTable();
	}
	function OnDeleteCategoryFailure() {
	    //nothing...
	}

	function PopulateTable() {
		for (var i = document.getElementById("categoryContainer").rows.length; i > 1; i--) {
			document.getElementById("categoryContainer").deleteRow(i - 1);
		}
		_categories = [];
		Populate();
	}

	function Populate() {
	    var req = new PEService();
	    req.ExecuteCall("CategoryService.asmx", "ReadAll", "{}", OnPopulateSuccess, OnPopulateFailure);
	}
	function OnPopulateSuccess(response) {
	    var jsonostr = JSON.parse(response);
	    var jsonobj = JSON.parse(jsonostr.d);
	    for (i in jsonobj) {
	        _c = {
	            CategoryName: TrimCategoryName(jsonobj[i]['CategoryName']),
	            RealCategoryName: jsonobj[i]['CategoryName'],
	            CategoryUID: jsonobj[i]['CategoryUID'],
	            NumberOfUsingQuestions: jsonobj[i]['NumberOfUsingQuestions']
	        }
	        _categories.push(_c);
	    }
	    $("#categoryTemplate").tmpl(_categories).appendTo("#categoryContainer");
	}
	function OnPopulateFailure() {
	    //nothing...
	}


	function TrimCategoryName(text) {
		if (text.length >= 10) {
			return text.substring(0, 9) + " ...";
		}
		return text;
	}
});