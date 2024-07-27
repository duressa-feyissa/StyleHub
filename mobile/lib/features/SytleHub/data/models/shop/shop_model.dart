import 'package:style_hub/features/SytleHub/data/models/shop/review_model.dart';
import 'package:style_hub/features/SytleHub/data/models/shop/working_hour_model.dart';

import '../../../domain/entities/shop/shop_entity.dart';
import '../product/image_model.dart';
import '../product/product_model.dart';

class ShopModel extends ShopEntity {
  const ShopModel({
    required super.id,
    required super.name,
    required super.description,
    required super.categories,
    required super.rating,
    required super.country,
    required super.state,
    required super.city,
    required super.streetAddress,
    required super.latitude,
    required super.longitude,
    required super.phoneNumber,
    required super.logo,
    required super.socialMediaLinks,
    required super.verified,
    required super.active,
    required super.lastSeenAt,
    required super.userId,
    required super.products,
    required super.videos,
    required super.reviews,
    required super.workingHours,
    required super.images,
    super.website,
    super.banner,
  });

  factory ShopModel.fromJson(Map<String, dynamic> json) {
    List<String> categories = [];
    if (json.containsKey('categories')) {
      categories = List<String>.from(json['categories']);
    }

    Map<String, String> socialMediaLinks = {};
    if (json.containsKey('socialMediaLinks')) {
      json['socialMediaLinks'].forEach((key, value) {
        socialMediaLinks[key] = value;
      });
    }

    return ShopModel(
      id: json['id'],
      name: json['name'],
      description: json['description'],
      categories: categories,
      rating: json['rating'],
      country: json['country'],
      state: json['state'],
      city: json['city'],
      streetAddress: json['streetAddress'],
      latitude: json['latitude'],
      longitude: json['longitude'],
      phoneNumber: json['phoneNumber'],
      banner: json['banner'],
      logo: json['logo'],
      socialMediaLinks: socialMediaLinks,
      verified: json['verified'],
      active: json['active'],
      lastSeenAt: DateTime.parse(json['lastSeenAt']),
      website: json['website'],
      userId: json['userId'],
      products: json.containsKey('products')
          ? List<ProductModel>.from(
              json['products'].map((e) => ProductModel.fromJson(e)))
          : [],
      videos:
          json.containsKey('videos') ? List<String>.from(json['videos']) : [],
      reviews: json.containsKey('reviews')
          ? List<ReviewModel>.from(
              json['reviews'].map((e) => ReviewModel.fromJson(e)))
          : [],
      workingHours: json.containsKey('workingHours')
          ? List<WorkingHourModel>.from(
              json['workingHours'].map((e) => WorkingHourModel.fromJson(e)))
          : [],
      images: json.containsKey('images')
          ? List<ImageModel>.from(
              json['images'].map((e) => ImageModel.fromJson(e)))
          : [],
    );
  }
}
