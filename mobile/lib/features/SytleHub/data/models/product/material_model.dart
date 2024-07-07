import '../../../domain/entities/product/material_entity.dart';

class MaterialModel extends MaterialEntity {
  const MaterialModel({required super.id, required super.name});

  factory MaterialModel.fromJson(Map<String, dynamic> json) {
    return MaterialModel(
      id: json['id'],
      name: json['name'],
    );
  }

  Map<String, dynamic> toJson() {
    return {
      'id': id,
      'name': name,
    };
  }
}
