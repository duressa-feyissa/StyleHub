import 'package:either_dart/either.dart';

import '../../../../../core/errors/failure.dart';
import '../../../../../core/use_cases/usecase.dart';
import '../../entities/product/location_entity.dart';
import '../../repositories/product.dart';

class GetLocationUseCase extends UseCase<List<LocationEntity>, NoParams> {
  final ProductRepository repository;

  GetLocationUseCase(this.repository);

  @override
  Future<Either<Failure, List<LocationEntity>>> call(NoParams params) async {
    return await repository.getLocations();
  }
}
