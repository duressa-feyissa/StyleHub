import '../../../domain/entities/product/design_entity.dart';

class DesignModel extends DesignEntity {
  const DesignModel({required super.id, required super.name});

  factory DesignModel.fromJson(Map<String, dynamic> json) {
    return DesignModel(
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
