import 'package:equatable/equatable.dart';

class EmailVerifyEntity extends Equatable {
  final String email;
  final bool isVerified;
  final String message;
  final DateTime verificationDate;
  final String token;

  const EmailVerifyEntity({
    required this.email,
    required this.isVerified,
    required this.message,
    required this.verificationDate,
    required this.token,
  });

  @override
  List<Object?> get props => [
        email,
        isVerified,
        message,
        verificationDate,
        token,
      ];
}
