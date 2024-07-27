import 'package:equatable/equatable.dart';
import 'package:style_hub/features/SytleHub/domain/entities/product/shop_info_entity.dart';

import 'brand_entity.dart';
import 'category_entity.dart';
import 'color_entity.dart';
import 'design_entity.dart';
import 'image_entity.dart';
import 'material_entity.dart';
import 'size_entity.dart';

class ProductEntity extends Equatable {
  final String id;
  final String title;
  final String description;
  final bool isFavorite;
  final double price;
  final String status;
  final bool inStock;
  final String condition;
  final String? videoUrl;
  final List<SizeEntity> sizes;
  final List<ColorEntity> colors;
  final List<MaterialEntity> materials;
  final List<CategoryEntity> categories;
  final List<ImageEntity> images;
  final List<BrandEntity> brands;
  final List<DesignEntity> designs;
  final ShopInfoEntity shopInfo;
  final bool isNegotiable;
  final DateTime createdAt;
  final DateTime updatedAt;

  const ProductEntity({
    required this.id,
    required this.title,
    required this.isFavorite,
    required this.description,
    required this.shopInfo,
    required this.price,
    required this.status,
    required this.inStock,
    required this.condition,
    required this.sizes,
    required this.colors,
    required this.materials,
    required this.categories,
    required this.images,
    required this.brands,
    required this.designs,
    required this.isNegotiable,
    required this.createdAt,
    required this.updatedAt,
    this.videoUrl,
  });

  @override
  List<Object?> get props => [
        id,
        title,
        description,
        price,
        status,
        inStock,
        condition,
        shopInfo,
        sizes,
        colors,
        materials,
        isFavorite,
        isFavorite,
        categories,
        images,
        brands,
        designs,
        isNegotiable,
        createdAt,
        updatedAt,
      ];
}
