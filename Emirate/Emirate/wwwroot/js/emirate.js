// Add Faculty
$("#submitFaculty").on("click", function () {
	debugger
	let name =  $("#facultyName").val();
	
	if (name != "") {
		$.ajax({
			type: 'POST',
			url: '/Admin/Faculty',
			data: { name: name },
			success: function (result) {
				debugger;
				if (!result.isError) {
					var url = window.location.href;
					successAlertWithRedirect(result.msg, url)
				}
				else {
					errorAlert(result.msg)

				}
			},
			error: function (ex) {
				"please fill the form correctly" + errorAlert(ex);
			}
		})
	} else {
		errorAlert("Fialed");
	}

});

//Swal.fire(
//	'Good job!',
//	'Added Successfully!',
//	'success'
//)

// Add Department
function submitDepartment() {
	debugger
	let data = {};
	data.Name = $("#departmentName").val();
	data.Faculty = $("#facultyId").val();
	let detail = JSON.stringify(data);
	if (data.Name != "" && data.Id != 0 && data.Faculty != "") {
		$.ajax({
			type: 'POST',
			url: '/Admin/Departments',
			data: { detail: detail },
			success: function (result) {
				if (!result.isError) {
					var url = window.location.href;
					//location.href = "/Admin/Departments"
					successAlertWithRedirect(result.msg, url)
				}
				else {
					errorAlert(result.msg)
				}
			},
			error: function (ex) {
				errorAlert("Network failure, please try again");
			}
		});
	}
	else {
		errorAlert("please fill the form correctly");
	}
};


//function updateFaculty() {
//	debugger;
//	let data = {};
//	data.Name = $("#facultyName").val();
//	data.Id = $("#edit_Id").val();
//	if (data.Id != "" && data.Name != "") {
//		let details = JSON.stringify(data);
//		$.ajax({
//			type: "POST",
//			dataType: "Json",
//			url: "/Admin/EditFaculty",
//			data: {details: details},
//			success: function (result) {
//				debugger;
//				if (!result.isError) {
//					var url = "/Admin/Faculty";
//					successAlertWithRedirect(result.msg, url);
//				}
//				else {
//					errorAlert(result.msg);
//				}
//			},
//			error: function () {
//				errorAlert("Error Occured");
//			}
//		})
//	}
//}


// Edit Faculty (Post method)
function EditFaculty() {
	debugger;
	var data = {};
	data.Id = $('#edit_Id').val();
	data.Name = $('#editfacultyName').val();
	if (data.Id != 0 && data.Name != "") {

		let facultyDetails = JSON.stringify(data);
		if (data != "") {
			$.ajax({
				type: 'Post',
				dataType: 'json',
				url: '/Admin/EditFaculty',
				data:
				{
					details: facultyDetails,
				},
				success: function (result) {
					debugger;
					if (!result.isError) {
						//location.href = '/Admin/Faculty';
						var url = window.location.href;
						successAlertWithRedirect(result.msg, url)
					}
					else {
						errorAlert(result.msg)
					}
				},
				error: function (ex) {
					errorAlert("Network failure, please try again");
				}
			});

		} else {
			errorAlert("Failed")
		}
		
		
	}
}


// Edit Faculty (Get method)
function GetFacultyByID(Id) {
	debugger;
	let data = Id;
	$.ajax({
		type: 'GET',
		url: '/Admin/EditFacultyByID',
		data: { EditFacultyId: data },
		dataType: 'json',
		success: function (data) {
			if (!data.isError) {
				$("#delete_Id").val(data.id);
				$("#edit_Id").val(data.id);
				$("#editfacultyName").val(data.name);
			}
		}
	});
}

// Delete Faculty (Post method)
function DeleteFaculty() {
	debugger;
	var data = {};
	data.Id = $('#delete_Id').val();
	data.Name = $('#deleteFacultyName').val();
	if (data.Id != 0 && data.Name != "") {

		let facultyDetails = JSON.stringify(data);
		if (data != "") {
			$.ajax({
				type: 'Post',
				dataType: 'json',
				url: '/Admin/DeleteFaculty',
				data:
				{
					details: facultyDetails,
				},
				success: function (result) {
					debugger;
					if (!result.isError) {
						var url = window.location.href;
						//location.href = '/Admin/Faculty';
						successAlertWithRedirect(result.msg, url)
					}
					else {
						errorAlert(result.msg)
					}
				},
				error: function (ex) {
					errorAlert("Network failure, please try again");
				}
			});

		} else {
			errorAlert("Failed")
		}
		
	}
}

// Delete Faculty (Get method)
function GetFacultyById(Id) {
	debugger;
	let data = Id;
	$.ajax({
		type: 'GET',
		url: '/Admin/DeleteFacultyByID',
		data: { DeleteFacultyId: data },
		dataType: 'json',
		success: function (data) {
			if (!data.isError) {
				$("#delete_Id").val(data.id);
				$("#edit_Id").val(data.id);
				$("#deleteFacultyName").val(data.name);
			}
		}
	});
}


// Edit Department (Post method)
function EditDepartment() {
	debugger;
	var data = {};
	data.Id = $('#edit_Id').val();
	data.Name = $('#editDepartmentName').val();
	if (data.Id != 0 && data.Name != "") {
		let deptDetails = JSON.stringify(data);
		if (data != "") {
			$.ajax({
				type: 'Post',
				dataType: 'json',
				url: '/Admin/EditDepartment',
				data:
				{
					details: deptDetails,
				},
				success: function (result) {
					debugger;
					if (!result.isError) {
						var url = window.location.href;
						//location.href = '/Admin/Departments';
						successAlertWithRedirect(result.msg, url)
						
					}
					else {
						errorAlert(result.msg)
					}
				},
				error: function (ex) {
					errorAlert("Network failure, please try again");
				}
			});
		}
		

	}
}

// Edit Department (Get method)
function GetDepartmentByID(Id) {
	debugger;
	let data = Id;
	$.ajax({
		type: 'GET',
		url: '/Admin/EditDepartmentByID',
		data: { EditDepartmentId: data },
		dataType: 'json',
		success: function (data) {
			if (!data.isError) {
				$("#delete_Id").val(data.id);
				$("#edit_Id").val(data.id);
				$("#editDepartmentName").val(data.name);
			}
		}
	});
}

// Delete Department (Post method)
function DeleteDepartment() {
	debugger;
	var dataId = $('#delete_Id').val();
	if (dataId != "") {
		let itToDelete = dataId;
		$.ajax({
			type: 'Post',
			dataType: 'json',
			url: '/Admin/DeleteDepartment',
			data:
			{
				id: itToDelete,
			},
			success: function (result) {
				debugger;
				if (!result.isError) {
					var url = window.location.href;
					//var url  = '/Admin/Departments';
					successAlertWithRedirect(result.msg, url);
				}
				else {
					errorAlert(result.msg)
				}
			},
			error: function (ex) {
				errorAlert("Network failure, please try again");
			}
		});
	}
}

// Delete Department (Get method)
function GetDepartmentById(Id) {
	debugger;
	let data = Id;
	$.ajax({
		type: 'GET',
		url: '/Admin/DeleteDepartmentByID',
		data: { DeleteDepartmentId: data },
		dataType: 'json',
		success: function (data) {
			if (!data.isError) {
				$("#delete_Id").val(data.id);
				$("#edit_Id").val(data.id);
				$("#deleteFacultyName").val(data.name);
			}
		}
	});
}

// Add Level
function submitLevel() {
	debugger
	let data = {};
	data.Name = $("#levelName").val();
	data.Department = $("#departmentId").val();
	data.Semester = $("#semesterId").val();
	data.TotalCreditLoad = $("#totalCreditLoad").val();
	let details = JSON.stringify(data);
	if (data.Department != "" && data.Semester != "" && data.TotalCreditLoad != "" && data.Name != "") {
		$.ajax({
			type: 'POST',
			url: '/Admin/Level',
			data: { details: details },
			success: function (result) {
				debugger
				if (!result.isError) {
					var url = window.location.href;
					successAlertWithRedirect(result.msg, url)
				}
				else {
					errorAlert(result.msg)
				}
			},
			error: function (ex) {
				errorAlert("Network failure, please try again");
			}
		});

	} else {
		errorAlert("please fill the form correctly");
	}
	
}


// Edit Level (Post method)
function EditLevel() {
	debugger;
	var data = {};
	data.Id = $('#levelEdit_Id').val();
	data.Name = $('#editLevelName').val();
	data.Department = $("#editDepartmentId").val();
	data.Semester = $("#editSemesterId").val();
	data.TotalCreditLoad = $("#editCreditLoad").val();
	if (data.Id != 0 && data.Name != "") {

		let levelDetails = JSON.stringify(data);

		$.ajax({
			type: 'Post',
			dataType: 'json',
			url: '/Admin/EditLevel',
			data:
			{
				details: levelDetails,
			},
			success: function (result) {
				debugger;
				if (!result.isError) {
					var url = window.location.href;
					//location.href = '/Admin/Level';
					successAlertWithRedirect(result.msg, url)
				}
				else {
					errorAlert(result.msg)
				}
			},
			error: function (ex) {
				errorAlert("Network failure, please try again");
			}
		});
		//Swal.fire(
		//	'Good job!',
		//	'Updated Successfully!',
		//	'success'
		//)
	}
}

// Edit Level (Get method)
function GetLevelByID(Id) {
	debugger;
	let data = Id;
	$.ajax({
		type: 'GET',
		url: '/Admin/EditLevelByID',
		data: { EditLevelId: data },
		dataType: 'json',
		success: function (data) {
			debugger
			if (!data.isError) {
				$("#delete_Id").val(data.id);
				$("#levelEdit_Id").val(Id);
				$("#editLevelName").val(data.name);
				$("#editSemesterId").val(data.semester);
				$("#editDepartmentId").val(data.departmentId);
				$("#editCreditLoad").val(data.totalCreditLoad);
			}
		}
	});
}



// Delete Level (Post method)
function DeleteLevel() {
	debugger;
	var data = {};
	data.Id = $('#delete_Id').val(); 
	data.Name = $('#deleteLevelName').val();
	if (data.Id != 0 && data.Name != "") {

		let levelDetails = JSON.stringify(data);

		$.ajax({
			type: 'Post',
			dataType: 'json',
			url: '/Admin/DeleteLevel',
			data:
			{
				details: levelDetails,
			},
			success: function (result) {
				debugger;
				if (!result.isError) {
					var url = window.location.href;
					//location.href = '/Admin/Level';
					successAlertWithRedirect(result.msg, url)
				}
				else {
					errorAlert(result.msg)
				}
			},
			error: function (ex) {
				errorAlert("Network failure, please try again");
			}
		});

	}
}

// Delete Level (Get method)
function GetLevelID(Id) {
	debugger;
	let data = Id;
	$.ajax({
		type: 'GET',
		url: '/Admin/DeleteLevelByID',
		data: { DeleteLevelId: data },
		dataType: 'json',
		success: function (data) {
			debugger
			if (!data.isError) {
				$("#delete_Id").val(data.id);
				$("#edit_Id").val(data.id);
				$("#deleteLevelName").val(data.name);
			}
		}
	});
}


//Add Course
function submitCourses() {
	debugger
	let data = {};
	data.Name = $("#course_name").val();
	data.CourseUnit = $("#course_unit").val();
	data.Semester = $("#cou_Semester").val();
	data.LevelId = $("#cou_Level").val();
	data.DepartmentId = $("#cou_Department").val();
	let courseDetail = JSON.stringify(data);
	if (data.Name != "" && data.CourseUnit != 0 && data.Semester != 0 && data.LevelId != 0 && data.DepartmentId != 0) {
		$.ajax({
			type: 'POST',
			url: '/Admin/Courses',
			data: { courseDetail: courseDetail },
			success: function (result) {
				debugger
				if (!result.isError) {
					var url = window.location.href;
					successAlertWithRedirect(result.msg, url)
				}
				else {
					errorAlert(result.msg)
				}
			},
			error: function (ex) {
				errorAlert("Network failure, please try again");
			}
		});
	}
	else {
		errorAlert("please fill the form correctly");
	}
}

// Edit Courses (Post method)
function EditCourses() {
	debugger;
	var data = {};
	data.Id = $('#coursesEdit_Id').val();
	data.DepartmentId = $('#edit_cous_Department').val();
	data.LevelId = $('#edit_cous_Level').val();
	data.Semester = $('#edit_cous_Semester').val();
	data.CourseUnit = $('#editCourseUnit').val();
	data.Name = $('#editCoursesName').val();
	if (data.Id != 0 && data.Name != "") {
		let deptDetails = JSON.stringify(data);
		if (data != "") {
			$.ajax({
				type: 'Post',
				dataType: 'json',
				url: '/Admin/EditCourses',
				data:
				{
					details: deptDetails,
				},
				success: function (result) {
					debugger;
					if (!result.isError) {
						var url = window.location.href;
						//location.href = '/Admin/Departments';
						successAlertWithRedirect(result.msg, url)

					}
					else {
						errorAlert(result.msg)
					}
				},
				error: function (ex) {
					errorAlert("Network failure, please try again");
				}
			});
		}
	}
}

// Edit Courses (Get method)
function GetCoursesByID(Id) {
	debugger;
	let data = Id;
	$.ajax({
		type: 'GET',
		url: '/Admin/EditCoursesByID',
		data: { courseId: data },
		dataType: 'json',
		success: function (data) {
			debugger
			if (!data.isError) {
				$("#coursesEdit_Id").val(data.data.Id);
				$("#edit_cous_Department").val(data.data.departmentId);
				$("#edit_cous_Level").val(data.data.levelId);
				$("#edit_cous_Semester").val(data.data.semester);
				$("#editCoursesName").val(data.data.name);
				$("#editCourseUnit").val(data.data.courseUnit);
			}
		
		}
	});
}




	// Delete Courses (Post method)
function DeleteCourses() {
	debugger;
	var dataId = $('#courseDelete_Id').val();
	if (dataId != "") {
		let itToDelete = dataId;
		$.ajax({
			type: 'Post',
			dataType: 'json',
			url: '/Admin/DeleteCourses',
			data:
			{
				id: itToDelete,
			},
			success: function (result) {
				debugger;
				if (!result.isError) {
					var url = window.location.href;
					//var url  = '/Admin/Departments';
					successAlertWithRedirect(result.msg, url);
				}
				else {
					errorAlert(result.msg)
				}
			},
			error: function (ex) {
				errorAlert("Network failure, please try again");
			}
		});
	}
}
	

// Delete Courses (Get method)
function GetCourseByID(Id) {
	debugger;
	let data = Id;
	$.ajax({
		type: 'GET',
		url: '/Admin/DeleteCoursesByID',
		data: { DeleteCoursesId: data },
		dataType: 'json',
		success: function (data) {
			if (!data.isError) {
				$("#courseDelete_Id").val(data.id);
				$("#deleteFacultyName").val(data.name);
			}
		}
	});
}





	$(document).ready(function () {
		$("#myTable").DataTable();
		//$(".makeFilter").select2();
		//$(".mySelect").select2();
	});


	var options = [];

	function getAllFacluty() {
		debugger
		$.ajax({
			type: 'GET',
			url: '/User/GetArreyListOfFaculties',
			success: function (result) {
				debugger
				options = result;
				return true;
			},
		});
	}

	var activeSearchInputId = "";
	var activeSearchName = "";
	function SearchFiltere(e) {
		debugger
		var inputId = e.target.id;
		var inputName = e.target.name;
		activeSearchInputId = inputId;
		activeSearchName = inputName;
		var searchTerm = $("#" + inputId).val().toLowerCase();

		// Show the dropdown options
		$("#dropdown-options" + inputName).show();

		// Filter the options based on the search term
		$("#dropdown-options" + inputName).html("");
		options.forEach(function (option) {
			if (option.toLowerCase().indexOf(searchTerm) > -1) {
				$("#dropdown-options" + inputName).append(`<div class="dropdown-option">${option}</div>`);
			}
		});

		// Hide the dropdown options if the search term is empty
		if (searchTerm === "") {
			$("#dropdown-options" + inputName).hide();
		}
	}

	$(document).ready(function () {
		// Select an option from the dropdown
		$(document).on("click", ".dropdown-option", function () {
			$("#" + activeSearchInputId).val($(this).text());
			$("#dropdown-options" + activeSearchName).hide();
		});

		// Hide the dropdown options when clicking outside of the container
		$(document).click(function (event) {
			if (!$(event.target).closest(".container").length) {
				$("#dropdown-options" + activeSearchName).hide();
			}
		});
	});


