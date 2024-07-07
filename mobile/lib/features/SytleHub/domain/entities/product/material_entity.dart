import 'package:equatable/equatable.dart';

class MaterialEntity extends Equatable {
  final String id;
  final String name;

  const MaterialEntity({
    required this.id,
    required this.name,
  });

  @override
  List<Object> get props => [id, name];
}
