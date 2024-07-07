import 'package:either_dart/either.dart';
import '../../../../../core/errors/failure.dart';
import '../../../../../core/use_cases/usecase.dart';

import '../../entities/product/size_entity.dart';
import '../../repositories/product.dart';

class GetSizesUseCase extends UseCase<List<SizeEntity>, NoParams> {
  final ProductRepository repository;

  GetSizesUseCase(this.repository);

  @override
  Future<Either<Failure, List<SizeEntity>>> call(NoParams params) async {
    return await repository.getSizes();
  }
}
