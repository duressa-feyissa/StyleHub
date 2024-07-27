import 'package:style_hub/features/SytleHub/domain/entities/shop/working_hour_entity.dart';

class WorkingHourModel extends WorkingHourEntity {
  const WorkingHourModel(
      {required super.id,
      required super.shopId,
      required super.day,
      required super.time});

  factory WorkingHourModel.fromJson(Map<String, dynamic> json) {
    return WorkingHourModel(
      id: json['id'],
      shopId: json['shopId'],
      day: json['day'],
      time: json['time'],
    );
  }

  Map<String, dynamic> toJson() {
    return {
      'id': super.id,
      'shopId': super.shopId,
      'day': super.day,
      'time': super.time,
    };
  }
}
