import 'package:equatable/equatable.dart';

class ColorEntity extends Equatable {
  final String id;
  final String name;
  final String hexCode;

  const ColorEntity({
    required this.id,
    required this.name,
    required this.hexCode,
  });

  @override
  List<Object> get props => [id, name, hexCode];
}
