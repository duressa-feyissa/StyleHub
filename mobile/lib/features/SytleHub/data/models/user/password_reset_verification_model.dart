import '../../../domain/entities/user/password_reset_verification_entity.dart';

class PasswordResetVerificationModel extends PasswordResetVerificationEntity {
  const PasswordResetVerificationModel({
    required super.email,
    required super.isVerified,
    required super.message,
    required super.verificationDate,
  });

  factory PasswordResetVerificationModel.fromJson(Map<String, dynamic> json) {
    return PasswordResetVerificationModel(
      email: json['email'],
      isVerified: json['isVerified'],
      message: json['message'],
      verificationDate: DateTime.parse(json['verificationDate']),
    );
  }

  Map<String, dynamic> toJson() {
    return {
      'email': email,
      'isVerified': isVerified,
      'message': message,
      'verificationDate': verificationDate.toIso8601String(),
    };
  }
}
