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

	$('#loginButton').click(function () {
		let loginModel = {
			email: $('#loginUserEmail').val(),
			password: $('#loginUserPassword').val()
		};

		LoginUser(loginModel);
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
			DisplayCurrentMessage('#regCurrentMessage', "Упс! Что-то пошло не так...", false);
		}
	});
}

function LoginUser(model) {
	$.ajax({
		url: 'https://localhost:44322/account/loginUser',
		method: 'POST',
		contentType: 'application/json',
		data: JSON.stringify(model),
		success: function () {

		},
		error: function () {

		}
	});
}

function DisplayCurrentMessage(pid, message, success) {
	if (success) {
		$(pid).css('color', '#4cff00').text(message);
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
		DisplayCurrentMessage('#regCurrentMessage', "Не введён пароль", false);
		return false;
	}
	if (password === confirmPassword) {
		return true;
	}
	else {
		DisplayCurrentMessage('#regCurrentMessage', "Пароль и подтверждение пароля не совпадают", false);
		return false;
	}
}

function ClearRegisterForm() {
	$('#regUserEmail').val('');
	$('#regUserPassword').val('');
	$('#regUserConfirmPassword').val('');
}
