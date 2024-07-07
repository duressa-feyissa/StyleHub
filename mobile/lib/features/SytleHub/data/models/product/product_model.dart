import '../../../domain/entities/product/product_entity.dart';
import 'brand_model.dart';
import 'category_model.dart';
import 'color_model.dart';
import 'design_model.dart';
import 'image_model.dart';
import 'material_model.dart';
import 'size_model.dart';

class ProductModel extends ProductEntity {
  const ProductModel({
    required super.id,
    required super.title,
    required super.description,
    required super.price,
    required super.quantity,
    required super.condition,
    required super.latitude,
    required super.longitude,
    required super.city,
    required super.sizes,
    required super.colors,
    required super.materials,
    required super.categories,
    required super.images,
    required super.brands,
    required super.designs,
    required super.isNegotiable,
    required super.createdAt,
    required super.updatedAt,
  });

  factory ProductModel.fromJson(Map<String, dynamic> json) {
    return ProductModel(
      id: json['id'],
      title: json['title'],
      description: json['description'],
      price: json['price'].toDouble(),
      quantity: json['quantity'],
      condition: json['condition'],
      latitude: json['latitude'],
      longitude: json['longitude'],
      city: json['city'],
      sizes:
          List<SizeModel>.from(json['sizes'].map((x) => SizeModel.fromJson(x))),
      colors: List<ColorModel>.from(
          json['colors'].map((x) => ColorModel.fromJson(x))),
      materials: List<MaterialModel>.from(
          json['materials'].map((x) => MaterialModel.fromJson(x))),
      categories: List<CategoryModel>.from(
          json['categories'].map((x) => CategoryModel.fromJson(x))),
      images: List<ImageModel>.from(
          json['images'].map((x) => ImageModel.fromJson(x))),
      brands: List<BrandModel>.from(
          json['brands'].map((x) => BrandModel.fromJson(x))),
      designs: List<DesignModel>.from(
          json['designs'].map((x) => DesignModel.fromJson(x))),
      isNegotiable: json['isNegotiable'],
      createdAt: DateTime.parse(json['createdAt']),
      updatedAt: DateTime.parse(json['updatedAt']),
    );
  }
}
