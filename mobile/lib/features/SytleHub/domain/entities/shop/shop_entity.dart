import 'package:equatable/equatable.dart';

import '../product/image_entity.dart';
import '../product/product_entity.dart';
import 'review_entity.dart';
import 'working_hour_entity.dart';

class ShopEntity extends Equatable {
  final String id;
  final String name;
  final String description;
  final List<String> categories;
  final int rating;
  final String country;
  final String state;
  final String city;
  final String streetAddress;
  final double latitude;
  final double longitude;
  final String phoneNumber;
  final String? banner;
  final String logo;
  final Map<String, String> socialMediaLinks;
  final bool verified;
  final bool active;
  final DateTime lastSeenAt;
  final String? website;
  final String userId;
  final List<ProductEntity> products;
  final List<String> videos;
  final List<ReviewEntity> reviews;
  final List<WorkingHourEntity> workingHours;
  final List<ImageEntity> images;

  const ShopEntity({
    required this.id,
    required this.name,
    required this.description,
    required this.categories,
    required this.rating,
    required this.country,
    required this.state,
    required this.city,
    required this.streetAddress,
    required this.latitude,
    required this.longitude,
    required this.phoneNumber,
    required this.logo,
    required this.socialMediaLinks,
    required this.verified,
    required this.active,
    required this.lastSeenAt,
    required this.userId,
    required this.products,
    required this.videos,
    required this.reviews,
    required this.workingHours,
    required this.images,
    this.banner,
    this.website,
  });

  @override
  List<Object?> get props => [
        id,
        name,
        description,
        categories,
        rating,
        country,
        state,
        city,
        streetAddress,
        latitude,
        longitude,
        phoneNumber,
        banner,
        logo,
        socialMediaLinks,
        verified,
        active,
        lastSeenAt,
        website,
        userId,
        products,
        videos,
        reviews,
        workingHours,
        images,
      ];

  ShopEntity copyWith({
    String? id,
    String? name,
    String? description,
    List<String>? categories,
    int? rating,
    String? country,
    String? state,
    String? city,
    String? streetAddress,
    double? latitude,
    double? longitude,
    String? phoneNumber,
    String? banner,
    String? logo,
    Map<String, String>? socialMediaLinks,
    bool? verified,
    bool? active,
    DateTime? lastSeenAt,
    String? website,
    String? userId,
    List<ProductEntity>? products,
    List<String>? videos,
    List<ReviewEntity>? reviews,
    List<WorkingHourEntity>? workingHours,
    List<ImageEntity>? images,
  }) {
    return ShopEntity(
      id: id ?? this.id,
      name: name ?? this.name,
      description: description ?? this.description,
      categories: categories ?? this.categories,
      rating: rating ?? this.rating,
      country: country ?? this.country,
      state: state ?? this.state,
      city: city ?? this.city,
      streetAddress: streetAddress ?? this.streetAddress,
      latitude: latitude ?? this.latitude,
      longitude: longitude ?? this.longitude,
      phoneNumber: phoneNumber ?? this.phoneNumber,
      banner: banner ?? this.banner,
      logo: logo ?? this.logo,
      socialMediaLinks: socialMediaLinks ?? this.socialMediaLinks,
      verified: verified ?? this.verified,
      active: active ?? this.active,
      lastSeenAt: lastSeenAt ?? this.lastSeenAt,
      website: website ?? this.website,
      userId: userId ?? this.userId,
      products: products ?? this.products,
      videos: videos ?? this.videos,
      reviews: reviews ?? this.reviews,
      workingHours: workingHours ?? this.workingHours,
      images: images ?? this.images,
    );
  }
}
