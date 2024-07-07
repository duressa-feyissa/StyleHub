part of 'user_bloc.dart';

enum LoginStatus { initial, loading, success, failure }

enum SignUpStatus { initial, loading, success, failure }

enum SendVerificationEmailRequestStatus { initial, loading, success, failure }

enum VerifyEmailStatus { initial, loading, success, failure }

enum ResetPasswordRequestStatus { initial, loading, success, failure }

enum ResetPasswordStatus { initial, loading, success, failure }

enum VerifyPasswordCodeStatus { initial, loading, success, failure }

enum LoadCurrentUserStatus { initial, loading, success, failure }

class UserState extends Equatable {
  final LoginStatus loginStatus;
  final SignUpStatus signUpStatus;
  final SendVerificationEmailRequestStatus sendVerificationEmailRequestStatus;
  final VerifyEmailStatus verifyEmailStatus;
  final VerifyPasswordCodeStatus verifyPasswordCodeStatus;
  final ResetPasswordRequestStatus resetPasswordRequestStatus;
  final ResetPasswordStatus resetPasswordStatus;
  final LoadCurrentUserStatus loadCurrentUserStatus;
  final UserEntity? user;
  final String? errorMessage;
  final String? email;
  final String? code;

  const UserState({
    this.loginStatus = LoginStatus.initial,
    this.signUpStatus = SignUpStatus.initial,
    this.loadCurrentUserStatus = LoadCurrentUserStatus.initial,
    this.sendVerificationEmailRequestStatus =
        SendVerificationEmailRequestStatus.initial,
    this.verifyEmailStatus = VerifyEmailStatus.initial,
    this.verifyPasswordCodeStatus = VerifyPasswordCodeStatus.initial,
    this.resetPasswordRequestStatus = ResetPasswordRequestStatus.initial,
    this.resetPasswordStatus = ResetPasswordStatus.initial,
    this.user,
    this.errorMessage,
    this.email,
    this.code,
  });

  @override
  List<Object?> get props => [
        loginStatus,
        signUpStatus,
        sendVerificationEmailRequestStatus,
        verifyEmailStatus,
        resetPasswordRequestStatus,
        resetPasswordStatus,
        user,
        verifyPasswordCodeStatus,
        errorMessage,
        loadCurrentUserStatus,
        email,
        code,
      ];

  UserState copyWith(
      {LoginStatus? loginStatus,
      SignUpStatus? signUpStatus,
      SendVerificationEmailRequestStatus? sendVerificationEmailRequestStatus,
      VerifyEmailStatus? verifyEmailStatus,
      ResetPasswordRequestStatus? resetPasswordRequestStatus,
      ResetPasswordStatus? resetPasswordStatus,
      VerifyPasswordCodeStatus? verifyPasswordCodeStatus,
      UserEntity? user,
      LoadCurrentUserStatus? loadCurrentUserStatus,
      String? email,
      String? code,
      String? errorMessage}) {
    return UserState(
      loginStatus: loginStatus ?? this.loginStatus,
      signUpStatus: signUpStatus ?? this.signUpStatus,
      sendVerificationEmailRequestStatus: sendVerificationEmailRequestStatus ??
          this.sendVerificationEmailRequestStatus,
      verifyEmailStatus: verifyEmailStatus ?? this.verifyEmailStatus,
      resetPasswordRequestStatus:
          resetPasswordRequestStatus ?? this.resetPasswordRequestStatus,
      resetPasswordStatus: resetPasswordStatus ?? this.resetPasswordStatus,
      user: user ?? this.user,
      verifyPasswordCodeStatus:
          verifyPasswordCodeStatus ?? this.verifyPasswordCodeStatus,
      errorMessage: errorMessage ?? this.errorMessage,
      email: email ?? this.email,
      code: code ?? this.code,
      loadCurrentUserStatus: loadCurrentUserStatus ?? this.loadCurrentUserStatus,
    );
  }
}
