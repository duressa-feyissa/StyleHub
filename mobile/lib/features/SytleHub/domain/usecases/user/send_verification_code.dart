import 'package:either_dart/either.dart';
import 'package:equatable/equatable.dart';

import '../../../../../core/errors/failure.dart';
import '../../../../../core/use_cases/usecase.dart';
import '../../repositories/user.dart';

class SendVerificationCodeUseCase extends UseCase<String, Params> {
  final UserRepository repository;

  SendVerificationCodeUseCase(this.repository);

  @override
  Future<Either<Failure, String>> call(Params params) async {
    return await repository.sendVerificationCode(
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
