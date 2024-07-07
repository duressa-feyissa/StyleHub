import 'package:equatable/equatable.dart';

import 'role_entity.dart';

class UserEntity extends Equatable {
  final String id;
  final String firstName;
  final String lastName;
  final String? phoneNumber;
  final String email;
  final double? latitude;
  final double? longitude;
  final String? profilePicture;
  final String? country;
  final String? city;
  final String? address;
  final String? token;
  final RoleEntity? role;

  const UserEntity({
    required this.id,
    required this.firstName,
    required this.lastName,
    this.phoneNumber,
    required this.email,
    this.latitude,
    this.longitude,
    this.profilePicture,
    this.country,
    this.city,
    this.address,
    this.token,
    this.role,
  });

  @override
  List<Object?> get props => [
        id,
        firstName,
        lastName,
        phoneNumber,
        email,
        latitude,
        longitude,
        profilePicture,
        country,
        city,
        address,
        token,
        role,
      ];
}
