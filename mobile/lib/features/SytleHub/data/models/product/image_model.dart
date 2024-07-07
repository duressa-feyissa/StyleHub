import '../../../domain/entities/product/image_entity.dart';

class ImageModel extends ImageEntity {
  const ImageModel({
    required super.id,
    required super.imageUri,
  });

  factory ImageModel.fromJson(Map<String, dynamic> json) {
    return ImageModel(
      id: json['id'],
      imageUri: json['imageUrl'],
    );
  }
}
