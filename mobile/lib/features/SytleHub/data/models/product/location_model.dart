import '../../../domain/entities/product/location_entity.dart';

class LocationModel extends LocationEntity {
  const LocationModel(
      {required super.id,
      required super.name,
      required super.latitude,
      required super.longitude});

  factory LocationModel.fromJson(Map<String, dynamic> json) {
    return LocationModel(
      id: json['id'],
      name: json['name'],
      latitude: json['latitude'].toDouble(),
      longitude: json['longitude'].toDouble(),
    );
  }

  Map<String, dynamic> toJson() {
    return {
      'id': id,
      'name': name,
      'latitude': latitude,
      'longitude': longitude,
    };
  }
}
