import '../../../domain/entities/user/email_verify_entity.dart';

class EmailVerifyModel extends EmailVerifyEntity {
  const EmailVerifyModel({
    required super.email,
    required super.isVerified,
    required super.message,
    required super.verificationDate,
    required super.token,
  });

  factory EmailVerifyModel.fromJson(Map<String, dynamic> json) {
    return EmailVerifyModel(
      email: json['email'],
      isVerified: json['isVerified'],
      message: json['message'],
      verificationDate: DateTime.parse(json['verificationDate']),
      token: json['token'],
    );
  }

  Map<String, dynamic> toJson() {
    return {
      'email': email,
      'isVerified': isVerified,
      'message': message,
      'verificationDate': verificationDate.toIso8601String(),
      'token': token,
    };
  }
}
