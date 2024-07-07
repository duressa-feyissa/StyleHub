import 'package:bloc/bloc.dart';
import 'package:equatable/equatable.dart';
import 'package:meta/meta.dart';
import 'package:style_hub/features/SytleHub/domain/usecases/user/load_currect_user.dart'
    as load_current_user;
import 'package:style_hub/features/SytleHub/domain/usecases/user/password_reset_verify_code.dart'
    as verify_password_code;
import 'package:style_hub/features/SytleHub/domain/usecases/user/reset_password.dart'
    as reset_password;
import 'package:style_hub/features/SytleHub/domain/usecases/user/reset_password_request.dart'
    as reset_password_request;
import 'package:style_hub/features/SytleHub/domain/usecases/user/send_verification_code.dart'
    as send_verification_code;
import 'package:style_hub/features/SytleHub/domain/usecases/user/sign_in.dart'
    as sign_in_usecase;
import 'package:style_hub/features/SytleHub/domain/usecases/user/sign_out.dart'
    as sign_out;
import 'package:style_hub/features/SytleHub/domain/usecases/user/sign_up.dart'
    as sign_up_usecase;
import 'package:style_hub/features/SytleHub/domain/usecases/user/verify_code.dart'
    as verify_code;

import '../../../../../core/use_cases/usecase.dart';
import '../../../domain/entities/user/user_entity.dart';

part 'user_event.dart';
part 'user_state.dart';

class UserBloc extends Bloc<UserEvent, UserState> {
  final sign_in_usecase.SignInUseCase signInUseCase;
  final sign_up_usecase.SignUpUseCase signUpUseCase;
  final send_verification_code.SendVerificationCodeUseCase
      sendVerificationCodeUseCase;
  final verify_code.VerifyCodeUseCase verifyCodeUseCase;
  final reset_password_request.ResetPasswordRequestUseCase
      resetPasswordRequestUseCase;
  final reset_password.ResetPasswordUseCase resetPasswordUseCase;
  final verify_password_code.PasswordResetVerifyCodeUseCase
      verifyPasswordCodeUseCase;
  final sign_out.SignOutUseCase signOutUseCase;
  final load_current_user.LoadCurrectUserUseCase loadCurrentUserUseCase;

  UserBloc({
    required this.signInUseCase,
    required this.signUpUseCase,
    required this.sendVerificationCodeUseCase,
    required this.verifyCodeUseCase,
    required this.resetPasswordRequestUseCase,
    required this.resetPasswordUseCase,
    required this.verifyPasswordCodeUseCase,
    required this.signOutUseCase,
    required this.loadCurrentUserUseCase,
  }) : super(const UserState()) {
    on<SignInEvent>(_onSignIn);
    on<SignUpEvent>(_onSignUp);
    on<SendVerificationEmailRequestEvent>(_onSendVerificationCode);
    on<VerifyEmailEvent>(_onVerifyCode);
    on<ResetPasswordRequestEvent>(_onResetPasswordRequest);
    on<ResetPasswordEvent>(_onResetPassword);
    on<VerifyPasswordCodeEvent>(_onVerifyPasswordCode);
    on<ClearStateEvent>(_onClearState);
    on<SignOutEvent>(_onSignOut);
    on<LoadCurrentUserEvent>(_onLoadCurrentUser);
  }

  void _onSignIn(SignInEvent event, Emitter<UserState> emit) async {
    emit(state.copyWith(loginStatus: LoginStatus.loading, email: event.email));
    final result = await signInUseCase(sign_in_usecase.Params(
      email: event.email,
      password: event.password,
    ));
    result.fold(
      (failure) => emit(state.copyWith(
          loginStatus: LoginStatus.failure, errorMessage: failure.message)),
      (success) => emit(state.copyWith(
          loginStatus: LoginStatus.success, user: success, errorMessage: null)),
    );
  }

  void _onSignUp(SignUpEvent event, Emitter<UserState> emit) async {
    emit(
        state.copyWith(signUpStatus: SignUpStatus.loading, email: event.email));
    final result = await signUpUseCase(sign_up_usecase.Params(
      email: event.email,
      password: event.password,
      firstName: event.firstName,
      lastName: event.lastName,
    ));
    result.fold(
      (failure) => emit(state.copyWith(
          signUpStatus: SignUpStatus.failure, errorMessage: failure.message)),
      (success) => emit(state.copyWith(signUpStatus: SignUpStatus.success)),
    );
  }

  void _onSendVerificationCode(
      SendVerificationEmailRequestEvent event, Emitter<UserState> emit) async {
    emit(state.copyWith(
        sendVerificationEmailRequestStatus:
            SendVerificationEmailRequestStatus.loading,
        email: event.email));
    final result =
        await sendVerificationCodeUseCase(send_verification_code.Params(
      email: event.email,
    ));
    result.fold(
      (failure) => emit(state.copyWith(
          sendVerificationEmailRequestStatus:
              SendVerificationEmailRequestStatus.failure,
          errorMessage: failure.message)),
      (success) => emit(state.copyWith(
          sendVerificationEmailRequestStatus:
              SendVerificationEmailRequestStatus.success)),
    );
  }

  void _onVerifyCode(VerifyEmailEvent event, Emitter<UserState> emit) async {
    emit(state.copyWith(
        verifyEmailStatus: VerifyEmailStatus.loading,
        email: event.email,
        code: event.code));
    final result = await verifyCodeUseCase(verify_code.Params(
      email: event.email,
      code: event.code,
    ));
    result.fold(
      (failure) => emit(state.copyWith(
          verifyEmailStatus: VerifyEmailStatus.failure,
          errorMessage: failure.message)),
      (success) =>
          emit(state.copyWith(verifyEmailStatus: VerifyEmailStatus.success)),
    );
  }

  void _onVerifyPasswordCode(
      VerifyPasswordCodeEvent event, Emitter<UserState> emit) async {
    emit(state.copyWith(
        verifyPasswordCodeStatus: VerifyPasswordCodeStatus.loading,
        email: event.email,
        code: event.code));
    final result = await verifyPasswordCodeUseCase(verify_password_code.Params(
      email: event.email,
      code: event.code,
    ));
    result.fold(
      (failure) => emit(state.copyWith(
          verifyPasswordCodeStatus: VerifyPasswordCodeStatus.failure,
          errorMessage: failure.message)),
      (success) => emit(state.copyWith(
          verifyPasswordCodeStatus: VerifyPasswordCodeStatus.success)),
    );
  }

  void _onResetPasswordRequest(
      ResetPasswordRequestEvent event, Emitter<UserState> emit) async {
    emit(state.copyWith(
        resetPasswordRequestStatus: ResetPasswordRequestStatus.loading,
        email: event.email));
    final result =
        await resetPasswordRequestUseCase(reset_password_request.Params(
      email: event.email,
    ));
    result.fold(
      (failure) => emit(state.copyWith(
          resetPasswordRequestStatus: ResetPasswordRequestStatus.failure,
          errorMessage: failure.message)),
      (success) => emit(state.copyWith(
          resetPasswordRequestStatus: ResetPasswordRequestStatus.success)),
    );
  }

  void _onResetPassword(
      ResetPasswordEvent event, Emitter<UserState> emit) async {
    emit(state.copyWith(resetPasswordStatus: ResetPasswordStatus.loading));
    final result = await resetPasswordUseCase(reset_password.Params(
      email: state.email ?? '',
      password: event.password,
      code: state.code ?? '',
    ));
    result.fold(
      (failure) => emit(state.copyWith(
          resetPasswordStatus: ResetPasswordStatus.failure,
          errorMessage: failure.message)),
      (success) => emit(
          state.copyWith(resetPasswordStatus: ResetPasswordStatus.success)),
    );
  }

  void _onClearState(ClearStateEvent event, Emitter<UserState> emit) {
    emit(const UserState());
  }

  void _onSignOut(SignOutEvent event, Emitter<UserState> emit) async {
    final result = await signOutUseCase(NoParams());
    result.fold(
      (failure) => emit(state.copyWith(errorMessage: failure.message)),
      (success) => emit(const UserState()),
    );
  }

  void _onLoadCurrentUser(LoadCurrentUserEvent event, Emitter<UserState> emit) {
    loadCurrentUserUseCase(NoParams()).then((result) {
      result.fold(
        (failure) => emit(state.copyWith(errorMessage: failure.message)),
        (user) => emit(state.copyWith(
            loadCurrentUserStatus: LoadCurrentUserStatus.success, user: user)),
      );
    });
  }
}
