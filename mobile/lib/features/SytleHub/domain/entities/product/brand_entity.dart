import 'package:equatable/equatable.dart';

class BrandEntity extends Equatable {
  final String id;
  final String name;
  final String logo;
  final String country;

  const BrandEntity({
    required this.id,
    required this.name,
    required this.logo,
    required this.country,
  });

  @override
  List<Object> get props => [id, name, logo, country];
}
