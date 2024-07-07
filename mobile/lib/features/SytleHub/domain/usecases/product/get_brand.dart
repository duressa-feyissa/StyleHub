import 'package:either_dart/either.dart';
import '../../../../../core/errors/failure.dart';
import '../../../../../core/use_cases/usecase.dart';

import '../../entities/product/brand_entity.dart';
import '../../repositories/product.dart';

class GetBrandsUseCase extends UseCase<List<BrandEntity>, NoParams> {
  final ProductRepository repository;

  GetBrandsUseCase(this.repository);

  @override
  Future<Either<Failure, List<BrandEntity>>> call(NoParams params) async {
    return await repository.getBrands();
  }
}
