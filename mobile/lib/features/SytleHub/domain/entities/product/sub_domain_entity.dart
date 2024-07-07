import 'package:equatable/equatable.dart';

import 'category_entity.dart';

class SubDomainEntity extends Equatable {
  final String name;
  final List<CategoryEntity> category;

  const SubDomainEntity({
    required this.name,
    required this.category,
  });

  @override
  List<Object> get props => [name, category];
}
