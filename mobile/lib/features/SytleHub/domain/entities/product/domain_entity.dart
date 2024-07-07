import 'package:equatable/equatable.dart';

import 'sub_domain_entity.dart';

class DomainEntity extends Equatable {
  final String name;
  final List<SubDomainEntity> subDomain;

  const DomainEntity({
    required this.name,
    required this.subDomain,
  });

  @override
  List<Object> get props => [name, subDomain];
}
