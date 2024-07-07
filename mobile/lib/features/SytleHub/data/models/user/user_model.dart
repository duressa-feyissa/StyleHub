import '../../../domain/entities/user/user_entity.dart';
import 'role_model.dart';

class UserModel extends UserEntity {
  const UserModel({
    required super.id,
    required super.firstName,
    required super.lastName,
    super.phoneNumber,
    required super.email,
    super.latitude,
    super.longitude,
    super.profilePicture,
    super.country,
    super.city,
    super.address,
    super.token,
    super.role,
  });

  factory UserModel.fromJson(Map<String, dynamic> json) {
    return UserModel(
      id: json['id'],
      firstName: json['firstName'],
      lastName: json['lastName'],
      phoneNumber: json.containsKey('phoneNumber') ? json['phoneNumber'] : null,
      email: json['email'],
      latitude:
          json.containsKey('latitude') ? json['latitude']?.toDouble() : null,
      longitude:
          json.containsKey('longitude') ? json['longitude']?.toDouble() : null,
      profilePicture:
          json.containsKey('profilePicture') ? json['profilePicture'] : null,
      country: json.containsKey('country') ? json['country'] : null,
      city: json.containsKey('city') ? json['city'] : null,
      address: json.containsKey('address') ? json['address'] : null,
      token: json.containsKey('token') ? json['token'] : null,
      role: json.containsKey('role') ? RoleModel.fromJson(json['role']) : null,
    );
  }

  Map<String, dynamic> toJson() {
    return {
      'id': id,
      'firstName': firstName,
      'lastName': lastName,
      'phoneNumber': phoneNumber,
      'email': email,
      'latitude': latitude,
      'longitude': longitude,
      'profilePicture': profilePicture,
      'country': country,
      'city': city,
      'address': address,
    };
  }
}
