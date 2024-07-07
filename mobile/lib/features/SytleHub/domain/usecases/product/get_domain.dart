import 'package:either_dart/either.dart';

import '../../../../../core/errors/failure.dart';
import '../../../../../core/use_cases/usecase.dart';
import '../../entities/product/domain_entity.dart';
import '../../repositories/product.dart';

class GetDomainsUseCase extends UseCase<List<DomainEntity>, NoParams> {
  final ProductRepository repository;

  GetDomainsUseCase(this.repository);

  @override
  Future<Either<Failure, List<DomainEntity>>> call(NoParams params) async {
    return await repository.getDomains();
  }
}
