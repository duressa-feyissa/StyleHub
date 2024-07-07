import 'package:either_dart/either.dart';
import 'package:equatable/equatable.dart';

import '../../../../../core/errors/failure.dart';
import '../../../../../core/use_cases/usecase.dart';
import '../../entities/user/user_entity.dart';
import '../../repositories/user.dart';

class ResetPasswordUseCase extends UseCase<UserEntity, Params> {
  final UserRepository repository;

  ResetPasswordUseCase(this.repository);

  @override
  Future<Either<Failure, UserEntity>> call(Params params) async {
    return await repository.resetPassword(
      email: params.email,
      password: params.password,
      code: params.code,
    );
  }
}

class Params extends Equatable {
  final String email;
  final String password;
  final String code;

  const Params({
    required this.email,
    required this.password,
    required this.code,
  });

  @override
  List<Object> get props => [email, password, code];
}
