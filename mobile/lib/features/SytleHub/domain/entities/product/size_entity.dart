import 'package:equatable/equatable.dart';

class SizeEntity extends Equatable {
  final String id;
  final String name;
  final String abbreviation;

  const SizeEntity({
    required this.id,
    required this.name,
    required this.abbreviation,
  });

  @override
  List<Object> get props => [id, name, abbreviation];
}
