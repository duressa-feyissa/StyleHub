import 'package:either_dart/either.dart';
import 'package:equatable/equatable.dart';
import '../../entities/user/password_reset_request_entity.dart';

import '../../../../../core/errors/failure.dart';
import '../../../../../core/use_cases/usecase.dart';
import '../../repositories/user.dart';

class ResetPasswordRequestUseCase
    extends UseCase<PasswordResetRequestEntity, Params> {
  final UserRepository repository;

  ResetPasswordRequestUseCase(this.repository);

  @override
  Future<Either<Failure, PasswordResetRequestEntity>> call(
      Params params) async {
    return await repository.resetPasswordRequest(
      email: params.email,
    );
  }
}

class Params extends Equatable {
  final String email;

  const Params({
    required this.email,
  });

  @override
  List<Object> get props => [email];
}
