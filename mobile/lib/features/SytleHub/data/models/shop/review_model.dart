import 'package:style_hub/features/SytleHub/domain/entities/shop/review_entity.dart';

class ReviewModel extends ReviewEntity {
  const ReviewModel({
    required super.id,
    required super.shopId,
    required super.firstName,
    required super.lastName,
    required super.review,
    required super.rating,
    required super.createdAt,
     super.image,
  });

  factory ReviewModel.fromJson(Map<String, dynamic> json) {
    return ReviewModel(
      id: json['id'],
      shopId: json['shopId'],
      firstName: json['firstName'],
      lastName: json['lastName'],
      image: json['image'],
      review: json['review'],
      rating: json['rating'],
      createdAt: DateTime.parse(json['createdAt']),
    );
  }

  Map<String, dynamic> toJson() {
    return {
      'id': super.id,
      'shopId': super.shopId,
      'firstName': super.firstName,
      'lastName': super.lastName,
      'image': super.image,
      'review': super.review,
      'rating': super.rating,
      'createdAt': super.createdAt,
    };
  }
}
