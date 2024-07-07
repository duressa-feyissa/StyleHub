import 'package:equatable/equatable.dart';
import 'brand_entity.dart';
import 'color_entity.dart';
import 'design_entity.dart';
import 'material_entity.dart';

import 'category_entity.dart';
import 'image_entity.dart';
import 'size_entity.dart';

class ProductEntity extends Equatable {
  final String id;
  final String title;
  final String description;
  final double price;
  final int quantity;
  final String condition;
  final double latitude;
  final double longitude;
  final String city;
  final List<SizeEntity> sizes;
  final List<ColorEntity> colors;
  final List<MaterialEntity> materials;
  final List<CategoryEntity> categories;
  final List<ImageEntity> images;
  final List<BrandEntity> brands;
  final List<DesignEntity> designs;
  final bool isNegotiable;
  final DateTime createdAt;
  final DateTime updatedAt;

  const ProductEntity({
    required this.id,
    required this.title,
    required this.description,
    required this.price,
    required this.quantity,
    required this.condition,
    required this.latitude,
    required this.longitude,
    required this.city,
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
  });

  @override
  List<Object?> get props => [
        id,
        title,
        description,
        price,
        quantity,
        condition,
        latitude,
        longitude,
        city,
        sizes,
        colors,
        materials,
        categories,
        images,
        brands,
        designs,
        isNegotiable,
        createdAt,
        updatedAt,
      ];
}
