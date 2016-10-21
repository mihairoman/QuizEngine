$(document).ready(function () {
	$("#Manage").show();
	var _id;
	var _mode;
	var _levels = [];
	var _l;

	PopulateTable();

	dialog = $("#LevelManagementPopup").dialog({
		width: 550,
		minHeight: 299,
		maxHeight: 299,
		modal: true,
		closeOnEscape: true,
		resizable: false,
		draggable: true,
		autoOpen: false,
		buttons: [
			{
				text: LevelResources.LevelPopupSaveButton,
				click: function () {
					var ok = true;
					$("#errorlist").empty();
					$("#errorlist").slideUp();
					$("#txtLevelName").removeClass("incorrectfield");
					$("#numDifficulty").removeClass("incorrectfield");
					if (!$("#txtLevelName").val()) {
						$("#errorlist").append("<li>" + LevelResources.LevelNameTooShortError + "</li>");
						$("#txtLevelName").addClass("incorrectfield");
						ok = false;
					}
					if ($("#txtLevelName").val().length > 50) {
						$("#errorlist").append("<li>" + LevelResources.LevelNameTooLongError + "</li>");
						$("#txtLevelName").addClass("incorrectfield");
						ok = false;
					}
					if ($("#numDifficulty").val() < 1 || $("#numDifficulty").val() > 5) {
						$("#errorlist").append("<li>" + LevelResources.LevelDifficultyError + "</li>");
						$("#numDifficulty").addClass("incorrectfield");
						ok = false;
					}
					var i = 0;
					while (i != _levels.length && _levels[i].LevelName != $("#txtLevelName").val())
						i++;
					if (i != _levels.length) {
						$("#errorlist").append("<li>" + LevelResources.LevelNameExistsError + "</li>");
						$("#txtLevelName").addClass("incorrectfield");
						ok = false;
					}
					i = 0;
					while (i != _levels.length && _levels[i].Difficulty != $("#numDifficulty").val())
						i++;
					if (i != _levels.length) {
						$("#errorlist").append("<li>" + LevelResources.DifficultyExistsError + "</li>");
						$("#numDifficulty").addClass("incorrectfield");
						ok = false;
					}
					if (ok) {
						var newLevel = {
							LevelUID: _id,
							LevelName: $("#txtLevelName").val(),
							Difficulty: $("#numDifficulty").val()
						}
						if (_mode == 1)
							InsertLevel(newLevel);
						else
							UpdateLevel(newLevel);
					}
					else {
						$("#errorlist").slideDown();
					}
				}
			},
			{
				text: LevelResources.LevelPopupCancelButton,
				click: function () {
					dialog.dialog("close");
				}
			}
		]
	});

	$("#levelContainer").on("click", "#btnEdit", function () {
		$("#manageerrorlist").empty();
		$("#manageerrorlist").slideUp();
		$("#txtLevelName").removeClass("incorrectfield");
		$("#numDifficulty").removeClass("incorrectfield");
		var index = $(this).closest("tr").index();
		_id = _levels[index - 1].LevelUID;
		$("#txtLevelName").val(_levels[index - 1].LevelName);
		$("#numDifficulty").val(_levels[index - 1].Difficulty);
		_mode = 2;
		dialog.dialog({ title: LevelResources.LevelEdit });
		$("#errorlist").empty();
		dialog.dialog("open");
	});

	$("#levelContainer").on("click", "#btnDelete", function () {
		$("#manageerrorlist").empty();
		$("#manageerrorlist").slideUp();
		var index = $(this).closest("tr").index();
		_id = _levels[index - 1].LevelUID;
		var levelName = _levels[index - 1].LevelName;
		var difficulty = _levels[index - 1].Difficulty;
		var newLevel = {
			LevelUID: _id,
			LevelName: levelName,
			Difficulty: difficulty
		}
		DeleteLevel(newLevel);
	});

	$(".btnInsert").on("click", function () {
		$("#manageerrorlist").empty();
		$("#manageerrorlist").slideUp();
		$("#txtLevelName").removeClass("incorrectfield");
		$("#numDifficulty").removeClass("incorrectfield");
		$("#txtLevelName").val("");
		$("#numDifficulty").val("");
		_id = "00000000-0000-0000-0000-000000000000";
		_mode = 1;
		dialog.dialog({ title: LevelResources.LevelInsert });
		$("#errorlist").empty();
		dialog.dialog("open");
	});

	function InsertLevel(level) {
		var x = { level: JSON.stringify(level) }
		$.ajax({
			type: "POST",
			url: "../Webservices/LevelService.asmx/InsertLevel",
			data: JSON.stringify(x),
			contentType: "application/json; charset=utf-8",
			dataType: "json",
			success: function () {
				$(".ui-dialog-content").dialog().dialog("close");
				PopulateTable();
			},
			failure: function (response) {
				$("#errorlist").append("<li>" + LevelResources.LevelInsertError + "</li>");
			}
		});
	}

	function UpdateLevel(level) {
		var x = { level: JSON.stringify(level) }
		$.ajax({
			type: "POST",
			url: "../Webservices/LevelService.asmx/UpdateLevel",
			data: JSON.stringify(x),
			contentType: "application/json; charset=utf-8",
			dataType: "json",
			success: function () {
				$(".ui-dialog-content").dialog().dialog("close");
				PopulateTable();
			},
			failure: function (response) {
				$("#errorlist").append("<li>" + LevelResources.LevelEditError + "</li>");
			}
		});
	}

	function DeleteLevel(level) {
		var x = { level: JSON.stringify(level) }
		$.ajax({
			type: "POST",
			url: "../Webservices/LevelService.asmx/DeleteLevel",
			data: JSON.stringify(x),
			contentType: "application/json; charset=utf-8",
			dataType: "json",
			success: function (response) {
				if (response.d != null) {
					$("#manageerrorlist").append("<li>" + LevelResources.LevelDeleteError + "</li><br/>");
					$("#manageerrorlist").slideDown();
				}
				PopulateTable();
			},
			failure: function (response) {
				alert(response.d);
			}
		});
	}

	function PopulateTable() {
		for (var i = document.getElementById("levelContainer").rows.length; i > 1; i--) {
			document.getElementById("levelContainer").deleteRow(i - 1);
		}
		_levels = [];

		$.ajax({
			type: "POST",
			url: "../Webservices/LevelService.asmx/ReadAll",
			contentType: "application/json; charset=utf-8",
			dataType: "json",
			success: function (response) {
				var jsonostr = response.d;
				var jsonobj = eval("(" + jsonostr + ")");
				for (i in jsonobj) {
					_l = {
						LevelName: jsonobj[i]["LevelName"],
						LevelUID: jsonobj[i]["LevelUID"],
						Difficulty: jsonobj[i]["Difficulty"]
					}
					_levels.push(_l);
				}
				$("#levelTemplate").tmpl(_levels).appendTo("#levelContainer");
			}
		});
	}
});