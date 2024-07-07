import '../../../domain/entities/product/domain_entity.dart';

import 'sub_domain_model.dart';

class DomainModel extends DomainEntity {
  const DomainModel({required super.name, required super.subDomain});

  factory DomainModel.fromJson(Map<String, dynamic> json) {
    return DomainModel(
      name: json['name'],
      subDomain: SubDomainModel.fromJsonList(json['subDomain']),
    );
  }

  static List<DomainModel> fromJsonList(Map<String, dynamic> json) {
    final List<DomainModel> domain = [];
    for (var entry in json.entries) {
      domain.add(DomainModel.fromJson({
        'name': entry.key,
        'subDomain': entry.value,
      }));
    }
    domain.sort((a, b) => a.name.compareTo(b.name));
    return domain;
  }

  Map<String, dynamic> toJson() {
    return {
      'name': name,
      'subDomain': subDomain,
    };
  }
}
