import 'package:either_dart/either.dart';

import '../../../../../core/errors/failure.dart';
import '../../../../../core/use_cases/usecase.dart';
import '../../entities/product/color_entity.dart';
import '../../repositories/product.dart';

class GetColorsUseCase extends UseCase<List<ColorEntity>, NoParams> {
  final ProductRepository repository;

  GetColorsUseCase(this.repository);

  @override
  Future<Either<Failure, List<ColorEntity>>> call(NoParams params) async {
    return await repository.getColors();
  }
}
