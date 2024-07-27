import 'package:style_hub/features/SytleHub/domain/entities/product/shop_info_entity.dart';

class ShopInfoModel extends ShopInfoEntity {
  const ShopInfoModel({
    required super.id,
    required super.name,
    required super.country,
    required super.state,
    required super.city,
    required super.streetAddress,
    required super.latitude,
    required super.longitude,
    required super.logo,
    required super.phoneNumber,
  });

  factory ShopInfoModel.fromJson(Map<String, dynamic> json) {
    return ShopInfoModel(
      id: json['id'],
      name: json['name'],
      country: json['country'],
      state: json['state'],
      city: json['city'],
      streetAddress: json['streetAddress'],
      latitude: json['latitude'].toDouble(),
      longitude: json['longitude'].toDouble(),
      logo: json['logo'],
      phoneNumber: json['phoneNumber'],
    );
  }

  Map<String, dynamic> toJson() {
    return {
      'id': id,
      'name': name,
      'country': country,
      'state': state,
      'city': city,
      'streetAddress': streetAddress,
      'latitude': latitude,
      'longitude': longitude,
      'logo': logo,
      'phoneNumber': phoneNumber,
    };
  }
}
