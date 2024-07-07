import 'package:either_dart/either.dart';

import '../../../../../core/errors/failure.dart';
import '../../../../../core/use_cases/usecase.dart';
import '../../entities/product/design_entity.dart';
import '../../repositories/product.dart';

class GetDesignsUseCase extends UseCase<List<DesignEntity>, NoParams> {
  final ProductRepository repository;

  GetDesignsUseCase(this.repository);

  @override
  Future<Either<Failure, List<DesignEntity>>> call(NoParams params) async {
    return await repository.getDesigns();
  }
}
