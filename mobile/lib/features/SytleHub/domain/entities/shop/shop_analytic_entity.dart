import 'package:equatable/equatable.dart';

class ShopAnalyticEntity extends Equatable {
  final int totalFollowers;
  final int totalReviews;
  final int totalFavorites;
  final int totalProducts;
  final int totalContacts;
  final int totalViews;

  const ShopAnalyticEntity({
    required this.totalFollowers,
    required this.totalReviews,
    required this.totalFavorites,
    required this.totalProducts,
    required this.totalContacts,
    required this.totalViews,
  });

  @override
  List<Object?> get props => [
        totalFollowers,
        totalReviews,
        totalFavorites,
        totalProducts,
        totalContacts,
        totalViews,
      ];
}
