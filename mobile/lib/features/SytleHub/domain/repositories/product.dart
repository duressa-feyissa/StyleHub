import 'package:either_dart/either.dart';

import '../../../../core/errors/failure.dart';
import '../entities/product/brand_entity.dart';
import '../entities/product/category_entity.dart';
import '../entities/product/color_entity.dart';
import '../entities/product/design_entity.dart';
import '../entities/product/domain_entity.dart';
import '../entities/product/location_entity.dart';
import '../entities/product/material_entity.dart';
import '../entities/product/product_entity.dart';
import '../entities/product/size_entity.dart';

abstract class ProductRepository {
  Future<Either<Failure, List<ColorEntity>>> getColors();
  Future<Either<Failure, List<BrandEntity>>> getBrands();
  Future<Either<Failure, List<CategoryEntity>>> getCategories();
  Future<Either<Failure, List<SizeEntity>>> getSizes();
  Future<Either<Failure, List<MaterialEntity>>> getMaterials();
  Future<Either<Failure, List<LocationEntity>>> getLocations();
  Future<Either<Failure, List<DesignEntity>>> getDesigns();
  Future<Either<Failure, List<DomainEntity>>> getDomains();
  Future<Either<Failure, List<ProductEntity>>> getProducts({
    String? search,
    List<String>? colorIds,
    List<String>? sizeIds,
    List<String>? categoryIds,
    List<String>? brandIds,
    List<String>? materialIds,
    List<String>? designIds,
    bool? isNegotiable,
    double? minPrice,
    double? maxPrice,
    int? minQuantity,
    int? maxQuantity,
    double? latitudes,
    double? longitudes,
    double? radiusInKilometers,
    String? condition,
    String? sortBy,
    String? sortOrder,
    int? skip = 0,
    int? limit = 15,
  });
}
