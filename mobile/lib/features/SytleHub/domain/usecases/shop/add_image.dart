import 'package:either_dart/either.dart';
import 'package:equatable/equatable.dart';

import '../../../../../core/errors/failure.dart';
import '../../../../../core/use_cases/usecase.dart';
import '../../entities/product/image_entity.dart';
import '../../repositories/shop.dart';

class AddImageUseCase extends UseCase<ImageEntity, Params> {
  final ShopRepository repository;

  AddImageUseCase(this.repository);

  @override
  Future<Either<Failure, ImageEntity>> call(Params params) async {
    return await repository.addProductImage(
      token: params.token,
      base64Image: params.base64Image,
    );
  }
}

class Params extends Equatable {
  final String token;
  final String base64Image;

  const Params({
    required this.token,
    required this.base64Image,
  });

  @override
  List<Object> get props => [token, base64Image];
}
