
import 'package:equatable/equatable.dart';

class ImageEntity extends Equatable {
  final String id;
  final String imageUri;

  const ImageEntity({
    required this.id,
    required this.imageUri,
  });

  @override
  List<Object?> get props => [id, imageUri];
}