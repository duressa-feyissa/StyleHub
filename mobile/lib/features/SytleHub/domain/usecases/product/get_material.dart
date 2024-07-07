import 'package:either_dart/either.dart';
import '../../../../../core/errors/failure.dart';
import '../../../../../core/use_cases/usecase.dart';
import '../../entities/product/material_entity.dart';

import '../../repositories/product.dart';

class GetMaterialsUseCase extends UseCase<List<MaterialEntity>, NoParams> {
  final ProductRepository repository;

  GetMaterialsUseCase(this.repository);

  @override
  Future<Either<Failure, List<MaterialEntity>>> call(NoParams params) async {
    return await repository.getMaterials();
  }
}
