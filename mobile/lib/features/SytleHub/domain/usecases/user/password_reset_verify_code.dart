import 'package:either_dart/either.dart';
import 'package:equatable/equatable.dart';

import '../../../../../core/errors/failure.dart';
import '../../../../../core/use_cases/usecase.dart';
import '../../entities/user/password_reset_verification_entity.dart';
import '../../repositories/user.dart';

class PasswordResetVerifyCodeUseCase
    extends UseCase<PasswordResetVerificationEntity, Params> {
  final UserRepository repository;

  PasswordResetVerifyCodeUseCase(this.repository);

  @override
  Future<Either<Failure, PasswordResetVerificationEntity>> call(
      Params params) async {
    return await repository.resetPasswordCodeVerification(
      email: params.email,
      code: params.code,
    );
  }
}

class Params extends Equatable {
  final String email;
  final String code;

  const Params({
    required this.email,
    required this.code,
  });

  @override
  List<Object> get props => [email, code];
}
