import 'package:either_dart/either.dart';

import '../../../../../core/errors/failure.dart';
import '../../../../../core/use_cases/usecase.dart';
import '../../repositories/user.dart';

class SignOutUseCase extends UseCase<void, NoParams> {
  final UserRepository userRepository;

  SignOutUseCase(this.userRepository);

  @override
  Future<Either<Failure, void>> call(NoParams params) async {
    return await userRepository.signOut();
  }
}
