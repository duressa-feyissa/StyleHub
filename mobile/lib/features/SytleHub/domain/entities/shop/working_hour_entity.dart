import 'package:equatable/equatable.dart';

class WorkingHourEntity extends Equatable {
  final String id;
  final String shopId;
  final String day;
  final String time;

  const WorkingHourEntity({
    required this.id,
    required this.shopId,
    required this.day,
    required this.time,
  });

  @override
  List<Object?> get props => [
        id,
        shopId,
        day,
        time,
      ];
}
