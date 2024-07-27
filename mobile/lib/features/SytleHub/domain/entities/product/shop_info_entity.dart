import 'package:equatable/equatable.dart';

class ShopInfoEntity extends Equatable {
  final String id;
  final String name;
  final String country;
  final String state;
  final String city;
  final String streetAddress;
  final double latitude;
  final double longitude;
  final String logo;
  final String phoneNumber;

  const ShopInfoEntity({
    required this.id,
    required this.name,
    required this.country,
    required this.state,
    required this.city,
    required this.streetAddress,
    required this.latitude,
    required this.longitude,
    required this.logo,
    required this.phoneNumber,
  });

  @override
  List<Object?> get props => [
        id,
        name,
        country,
        state,
        city,
        streetAddress,
        latitude,
        longitude,
        logo,
        phoneNumber
      ];
}
