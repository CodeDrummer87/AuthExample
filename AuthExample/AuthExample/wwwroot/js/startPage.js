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
		url: 'https://localhost:44322/content/saveUserData',
		method: 'POST',
		contentType: 'application/json',
		data: JSON.stringify(user),
		success: function (address) {
			window.location.href = address;
		},
		error: function () {
			DisplayCurrentMessage('#startPageCurrentMessage', "Ваши данные улетели куда-то не туда...", false);
		}
	});
}