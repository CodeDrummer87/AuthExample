$(document).ready(function () {
	$('#userDataSubmitButton').click(function () {
		let userData = {
			firstName: $('#userFirstName').val(),
			lastName: $('#userLastName').val(),
			middleName: $('#userMiddleName').val()
		};

		SendUserData(userData);
	});
});

function SendUserData(user) {
	$.ajax({
		url: 'https://localhost:44322/content/saveUserData?firstname='+ user.firstName
			+ '&lastname=' + user.lastName
			+ '&middlename=' + user.middleName,
		method: 'GET',
		success: function (address) {
			window.location.href = address;
		},
		error: function () {
			DisplayCurrentMessage('#startPageCurrentMessage', "Ваши данные улетели куда-то не туда...", false);
		}
	});
}