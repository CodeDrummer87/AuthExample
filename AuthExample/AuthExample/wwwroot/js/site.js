$(document).ready(function () {

	$('#registerButton').click(function () {
		let userLogin = {
			email: $('#regUserEmail').val(),
			password: $('#regUserPassword').val(),
			confirmPassword: $('#regUserConfirmPassword').val()
		};

		if (CheckPasswordForConfirm(userLogin.password, userLogin.confirmPassword)) {
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
		success: function () {
			
		},
		error: function () {
			
		}
	});
}

function DisplayCurrentMessage(message, success) {
	if (success) {
		$('#regCurrentMessage').css('color', 'green').text(message);
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
	$('#regUserPassword').val('');
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