part of 'user_bloc.dart';

@immutable
sealed class UserEvent {}

class SignInEvent extends UserEvent {
  final String email;
  final String password;

  SignInEvent({required this.email, required this.password});
}

class SignUpEvent extends UserEvent {
  final String firstName;
  final String lastName;
  final String email;
  final String password;

  SignUpEvent({
    required this.firstName,
    required this.lastName,
    required this.email,
    required this.password,
  });
}

class SendVerificationEmailRequestEvent extends UserEvent {
  final String email;

  SendVerificationEmailRequestEvent({required this.email});
}

class VerifyEmailEvent extends UserEvent {
  final String email;
  final String code;

  VerifyEmailEvent({required this.email, required this.code});
}

class VerifyPasswordCodeEvent extends UserEvent {
  final String email;
  final String code;

  VerifyPasswordCodeEvent({required this.email, required this.code});
}

class ResetPasswordRequestEvent extends UserEvent {
  final String email;

  ResetPasswordRequestEvent({required this.email});
}

class ResetPasswordEvent extends UserEvent {
  final String password;

  ResetPasswordEvent({
    required this.password,
  });
}


class ClearStateEvent extends UserEvent {}

class LoadCurrentUserEvent extends UserEvent {}

class SignOutEvent extends UserEvent {}