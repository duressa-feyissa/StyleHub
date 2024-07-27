import 'package:either_dart/either.dart';
import 'package:equatable/equatable.dart';

import '../../../../../core/errors/failure.dart';
import '../../../../../core/use_cases/usecase.dart';
import '../../entities/product/product_entity.dart';
import '../../repositories/shop.dart';

class AddProductsUseCase extends UseCase<ProductEntity, Params> {
  final ShopRepository repository;

  AddProductsUseCase(this.repository);

  @override
  Future<Either<Failure, ProductEntity>> call(Params params) async {
    return await repository.addProduct(
      token: params.token,
      title: params.title,
      description: params.description,
      price: params.price,
      isFixedPrice: params.isFixedPrice,
      condition: params.condition,
      inStock: params.inStock,
      shopId: params.shopId,
      status: params.status,
      images: params.images,
      colorIds: params.colorIds,
      sizeIds: params.sizeIds,
      categoryIds: params.categoryIds,
      brandIds: params.brandIds,
      materialIds: params.materialIds,
      designIds: params.designIds,
      videoUrl: params.videoUrl
    );
  }
}

class Params extends Equatable {
  final String token;
  final String title;
  final String description;
  final int price;
  final bool isFixedPrice;
  final String condition;
  final bool inStock;
  final String status;
  final String shopId;
  final List<String> images;
  final List<String> colorIds;
  final List<String> sizeIds;
  final List<String> categoryIds;
  final List<String> brandIds;
  final List<String> materialIds;
  final List<String> designIds;
  final String? videoUrl;

  const Params({
    required this.token,
    required this.title,
    required this.description,
    required this.price,
    required this.isFixedPrice,
    required this.condition,
    required this.inStock,
    required this.shopId,
    required this.status,
    required this.images,
    required this.colorIds,
    required this.sizeIds,
    required this.categoryIds,
    required this.brandIds,
    required this.materialIds,
    required this.designIds,
    this.videoUrl,
  });

  @override
  List<Object?> get props => [
        token,
        title,
        description,
        price,
        isFixedPrice,
        condition,
        inStock,
        status,
        images,
        shopId,
        colorIds,
        sizeIds,
        categoryIds,
        brandIds,
        materialIds,
        designIds
      ];
}
