import 'package:either_dart/either.dart';
import '../../entities/user/user_entity.dart';

import '../../../../../core/errors/failure.dart';
import '../../../../../core/use_cases/usecase.dart';
import '../../repositories/user.dart';

class LoadCurrectUserUseCase extends UseCase<UserEntity, NoParams> {
  final UserRepository userRepository;

  LoadCurrectUserUseCase(this.userRepository);

  @override
  Future<Either<Failure, UserEntity>> call(NoParams params) async {
    return await userRepository.getCurrentUser();
  }
}
