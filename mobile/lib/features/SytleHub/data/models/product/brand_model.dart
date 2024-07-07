import '../../../domain/entities/product/brand_entity.dart';

class BrandModel extends BrandEntity {
  const BrandModel(
      {required super.id,
      required super.name,
      required super.logo,
      required super.country});

  factory BrandModel.fromJson(Map<String, dynamic> json) {
    return BrandModel(
      id: json['id'],
      name: json['name'],
      logo: json['logo'],
      country: json['country'],
    );
  }

  Map<String, dynamic> toJson() {
    return {
      'id': id,
      'name': name,
      'logo': logo,
      'country': country,
    };
  }
}
