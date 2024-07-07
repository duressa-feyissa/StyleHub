import 'package:either_dart/either.dart';
import 'package:equatable/equatable.dart';
import '../../entities/user/user_entity.dart';

import '../../../../../core/errors/failure.dart';
import '../../../../../core/use_cases/usecase.dart';
import '../../repositories/user.dart';

class SignUpUseCase extends UseCase<UserEntity, Params> {
  final UserRepository repository;

  SignUpUseCase(this.repository);

  @override
  Future<Either<Failure, UserEntity>> call(Params params) async {
    return await repository.signUp(
      firstName: params.firstName,
      lastName: params.lastName,
      email: params.email,
      password: params.password,
    );
  }
}

class Params extends Equatable {
  final String firstName;
  final String lastName;
  final String email;
  final String password;

  const Params({
    required this.firstName,
    required this.lastName,
    required this.email,
    required this.password,
  });

  @override
  List<Object> get props => [firstName, lastName, email, password];
}
