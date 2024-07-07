import 'package:either_dart/either.dart';
import 'package:equatable/equatable.dart';
import '../../entities/user/user_entity.dart';

import '../../../../../core/errors/failure.dart';
import '../../../../../core/use_cases/usecase.dart';
import '../../repositories/user.dart';

class SignInUseCase extends UseCase<UserEntity, Params> {
  final UserRepository repository;

  SignInUseCase(this.repository);

  @override
  Future<Either<Failure, UserEntity>> call(Params params) async {
    return await repository.signIn(
      email: params.email,
      password: params.password,
    );
  }
}

class Params extends Equatable {
  final String email;
  final String password;

  const Params({required this.email, required this.password});

  @override
  List<Object> get props => [email, password];
}
