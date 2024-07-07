import '../../../domain/entities/product/sub_domain_entity.dart';
import 'category_model.dart';

class SubDomainModel extends SubDomainEntity {
  const SubDomainModel({required super.name, required super.category});

  factory SubDomainModel.fromJson(Map<String, dynamic> json) {
    final List<CategoryModel> category = [];
    for (var entry in json['category']) {
      category.add(CategoryModel.fromJson(entry));
    }
    category.sort((a, b) => a.name.compareTo(b.name));
    return SubDomainModel(
      name: json['name'],
      category: category,
    );
  }

  static List<SubDomainModel> fromJsonList(Map<String, dynamic> json) {
    final List<SubDomainModel> subDomain = [];
    for (var entry in json.entries) {
      subDomain.add(SubDomainModel.fromJson({
        'name': entry.key,
        'category': entry.value,
      }));
    }
    subDomain.sort((a, b) => a.name.compareTo(b.name));
    return subDomain;
  }
}
