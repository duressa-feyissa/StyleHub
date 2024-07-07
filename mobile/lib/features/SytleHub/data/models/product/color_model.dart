import '../../../domain/entities/product/color_entity.dart';

class ColorModel extends ColorEntity {
  const ColorModel(
      {required super.id, required super.name, required super.hexCode});

  factory ColorModel.fromJson(Map<String, dynamic> json) {
    return ColorModel(
      id: json['id'],
      name: json['name'],
      hexCode: json['hexCode'],
    );
  }

  Map<String, dynamic> toJson() {
    return {
      'id': id,
      'name': name,
      'hex_code': hexCode,
    };
  }
}
