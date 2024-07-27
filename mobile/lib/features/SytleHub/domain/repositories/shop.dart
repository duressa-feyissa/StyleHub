import 'package:either_dart/either.dart';
import 'package:style_hub/core/errors/failure.dart';
import 'package:style_hub/features/SytleHub/domain/entities/product/image_entity.dart';
import 'package:style_hub/features/SytleHub/domain/entities/shop/working_hour_entity.dart';

import '../entities/product/product_entity.dart';
import '../entities/shop/review_entity.dart';
import '../entities/shop/shop_entity.dart';

abstract class ShopRepository {
  Future<Either<Failure, List<ShopEntity>>> getShop({
    required String token,
    String? search,
    List<String>? category,
    int? rating,
    bool? verified,
    bool? active,
    String? ownerId,
    double? latitudes,
    double? longitudes,
    double? radiusInKilometers,
    String? condition,
    String? sortBy,
    String? sortOrder,
    int? skip = 0,
    int? limit = 15,
  });

  Future<Either<Failure, ShopEntity>> getShopById({
    required String id,
  });

  Future<Either<Failure, List<ImageEntity>>> getShopProductsImages({
    required String shopId,
    int? skip = 0,
    int? limit = 15,
  });

  Future<Either<Failure, List<String>>> getShopProductsVideos({
    required String shopId,
    int? skip = 0,
    int? limit = 15,
  });

  Future<Either<Failure, List<ReviewEntity>>> getShopReviews({
    required String shopId,
    String? userId,
    String? sortBy,
    String? sortOrder,
    int? rating,
    int? skip = 0,
    int? limit = 15,
  });

  Future<Either<Failure, List<ProductEntity>>> getShopProducts({
    required String shopId,
    String? sortBy,
    String? sortOrder,
    int? skip = 0,
    int? limit = 15,
  });

  Future<Either<Failure, List<WorkingHourEntity>>> getShopWorkingHours({
    required String shopId,
  });

  Future<Either<Failure, ProductEntity>> addProduct({
    required String token,
    required String title,
    required String description,
    required int price,
    required bool isFixedPrice,
    required String condition,
    required String shopId,
    required bool inStock,
    required String status,
    required List<String> images,
    required List<String> colorIds,
    required List<String> sizeIds,
    required List<String> categoryIds,
    required List<String> brandIds,
    required List<String> materialIds,
    required List<String> designIds,
    String? videoUrl,
  });

  Future<Either<Failure, ImageEntity>> addProductImage({
    required String token,
    required String base64Image,
  });

   Future<Either<Failure, ProductEntity>> deleteProductById({
    required String token,
    required String productId,
  });
}
