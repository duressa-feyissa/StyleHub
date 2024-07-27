import 'package:equatable/equatable.dart';

class ReviewEntity extends Equatable {
  final String id;
  final String shopId;
  final String firstName;
  final String lastName;
  final String review;
  final int rating;
  final DateTime createdAt;
  final String? image;

  const ReviewEntity({
    required this.id,
    required this.shopId,
    required this.firstName,
    required this.lastName,
    required this.review,
    required this.rating,
    required this.createdAt,
    this.image,
  });

  @override
  List<Object?> get props => [
        id,
        shopId,
        firstName,
        lastName,
        image,
        review,
        rating,
        createdAt,
      ];
}
