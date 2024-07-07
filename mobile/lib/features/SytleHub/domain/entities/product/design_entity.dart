import 'package:equatable/equatable.dart';

class DesignEntity extends Equatable {
  final String id;
  final String name;

  const DesignEntity({
    required this.id,
    required this.name,
  });

  @override
  List<Object> get props => [id, name];
}
