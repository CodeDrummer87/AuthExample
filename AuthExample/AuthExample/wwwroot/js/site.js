$(document).ready(function () {

	$('#registerButton').click(function () {
		let userLogin = {
			email: $('#regUserEmail').val(),
			password: $('#regUserPassword').val(),
			confirmPassword: $('#regUserConfirmPassword').val()
		};

		if (CheckPasswordForConfirm(userLogin.password, userLogin.confirmPassword) && userLogin.email !== '') {
			RegisterUser(userLogin);
		}
		else {
			ClearPasswordFields();
		}
	});

});

function RegisterUser(model) {
	$.ajax({
		url: 'https://localhost:44322/account/registerUser',
		method: 'POST',
		contentType: 'application/json',
		data: JSON.stringify(model),
		success: function (address) {
			ClearRegisterForm();
			window.location.href = address;
		},
		error: function () {
			DisplayCurrentMessage("Упс! Что-то пошло не так...", false);
		}
	});
}

function DisplayCurrentMessage(message, success) {
	if (success) {
		$('#regCurrentMessage').css('color', '#4cff00').text(message);
	}
	else {
		$('#regCurrentMessage').css('color', 'red').text(message);
	}

	setTimeout(ClearCurrentMessage, 2500);
}

function ClearCurrentMessage() {
	$('#regCurrentMessage').text(' ');
}

function ClearPasswordFields() {
	$('#regUserPassword').val('').focus();
	$('#regUserConfirmPassword').val('');
}

function CheckPasswordForConfirm(password, confirmPassword) {
	if (password === '') {
		DisplayCurrentMessage("Не введён пароль", false);
		return false;
	}
	if (password === confirmPassword) {
		return true;
	}
	else {
		DisplayCurrentMessage("Пароль и подтверждение пароля не совпадают", false);
		return false;
	}
}

function ClearRegisterForm() {
	$('#regUserEmail').val('');
	$('#regUserPassword').val('');
	$('#regUserConfirmPassword').val('');
}
