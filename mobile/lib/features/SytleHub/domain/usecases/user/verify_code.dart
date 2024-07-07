import 'package:either_dart/either.dart';
import 'package:equatable/equatable.dart';

import '../../../../../core/errors/failure.dart';
import '../../../../../core/use_cases/usecase.dart';
import '../../../data/models/user/email_verify_model.dart';
import '../../repositories/user.dart';

class VerifyCodeUseCase extends UseCase<EmailVerifyModel, Params> {
  final UserRepository repository;

  VerifyCodeUseCase(this.repository);

  @override
  Future<Either<Failure, EmailVerifyModel>> call(Params params) async {
    return await repository.verifyCode(
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
