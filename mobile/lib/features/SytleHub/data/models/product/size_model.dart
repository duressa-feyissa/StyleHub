import '../../../domain/entities/product/size_entity.dart';

class SizeModel extends SizeEntity {
  const SizeModel(
      {required super.id, required super.name, required super.abbreviation});

  factory SizeModel.fromJson(Map<String, dynamic> json) {
    return SizeModel(
      id: json['id'],
      name: json['name'],
      abbreviation: json['abbreviation'],
    );
  }

  Map<String, dynamic> toJson() {
    return {
      'id': id,
      'name': name,
      'abbreviation': abbreviation,
    };
  }
}
