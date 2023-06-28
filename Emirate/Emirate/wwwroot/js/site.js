document.addEventListener("DOMContentLoaded", function (event) {

    const showNavbar = (toggleId, navId, bodyId, headerId) => {
        const toggle = document.getElementById(toggleId),
            nav = document.getElementById(navId),
            bodypd = document.getElementById(bodyId),
            headerpd = document.getElementById(headerId)

        // Validate that all variables exist
        if (toggle && nav && bodypd && headerpd) {
            toggle.addEventListener('click', () => {
                // show navbar
                nav.classList.toggle('showSidebar')
                // change icon
                toggle.classList.toggle('bx-x')
                // add padding to body
                bodypd.classList.toggle('body-pd')
                // add padding to header
                headerpd.classList.toggle('body-pd')
            })
        }
    }

    showNavbar('header-toggle', 'nav-bar', 'body-pd', 'header')

    /*===== LINK ACTIVE =====*/
    const linkColor = document.querySelectorAll('.nav_link')

    function colorLink() {
        if (linkColor) {
            linkColor.forEach(l => l.classList.remove('active'))
            this.classList.add('active')
        }
    }
    linkColor.forEach(l => l.addEventListener('click', colorLink))

    // Your code to run since DOM is loaded and ready
});



// Edit User (Post method)
function EditUser() {
	debugger;
	var data = {};
	data.UserId = $('#userEdit_Id').val();
	data.DepartmentId = $('#departmentId').val();
	data.LevelId = $('#levelId').val();
	data.PhoneNumber = $('#phoneId').val();
	data.GenderId = $('#genderId').val();
	data.Username = $('#username').val();
	data.Firstname = $('#firstName').val();
	data.Middlename = $('#middleName').val();
	data.Lastname = $('#lastName').val();
	if (data.UserId != 0 ) {
		let userDetails = JSON.stringify(data);
		if (data != "") {
			$.ajax({
				type: 'Post',
				dataType: 'json',
				url: '/Account/EditUser',
				data:
				{
					userDetails: userDetails,
				},
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
					errorAlert("Network failure, please try again");
				}
			});
		}
	}
}

// Edit User (Get method)
function GetUserByID(Id) {
	debugger;
	let data = Id;
	$.ajax({
		type: 'GET',
		url: '/Account/EditUserByID',
		data: { EditUserId: data },
		dataType: 'json',
		success: function (data) {
			debugger
			if (!data.isError) {
				$("#userEdit_Id").val(data.data.id);
				$("#departmentId").val(data.data.departmentId);
				$("#levelId").val(data.data.levelId);
				$("#phoneId").val(data.data.phoneNumber);
				$("#firstName").val(data.data.firstName);
				$("#middleName").val(data.data.middleName);
				$("#lastName").val(data.data.lastName);
				$("#username").val(data.data.userName);
				$("#genderId").val(data.data.genderId);
			}

		}
	});
}



// Delete User (Post method)
function DeleteUser() {
	debugger;
	var dataId = $('#userDelete_Id').val();
	if (dataId != "") {
		let itToDelete = dataId;
		$.ajax({
			type: 'Post',
			dataType: 'json',
			url: '/Account/DeleteUser',
			data:
			{
				id: itToDelete,
			},
			success: function (result) {
				debugger;
				if (!result.isError) {
					var url = window.location.href;
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


// Delete User (Get method)
function GetUserID(Id) {
	debugger;
	let data = Id;
	$.ajax({
		type: 'GET',
		url: '/Account/DeleteUserByID',
		data: { DeleteUserId: data },
		dataType: 'json',
		success: function (data) {
			if (!data.isError) {
				$("#userDelete_Id").val(data.id);
				
			}
		}
	});
}





