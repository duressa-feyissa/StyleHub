import '../../../domain/entities/user/password_reset_request_entity.dart';

class PasswordResetRequestModel extends PasswordResetRequestEntity {
  const PasswordResetRequestModel({
    required super.email,
    required super.message,
    required super.status,
    required super.expirationDate,
  });

  factory PasswordResetRequestModel.fromJson(Map<String, dynamic> json) {
    return PasswordResetRequestModel(
      email: json['email'],
      message: json['message'],
      status: json['status'],
      expirationDate: DateTime.parse(json['expirationDate']),
    );
  }

  Map<String, dynamic> toJson() {
    return {
      'email': email,
      'message': message,
      'status': status,
      'expirationDate': expirationDate.toIso8601String(),
    };
  }
}
