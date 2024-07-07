import 'package:equatable/equatable.dart';

class PasswordResetVerificationEntity extends Equatable {
  final String email;
  final bool isVerified;
  final String message;
  final DateTime verificationDate;

  const PasswordResetVerificationEntity({
    required this.email,
    required this.isVerified,
    required this.message,
    required this.verificationDate,
  });

  @override
  List<Object?> get props => [email, isVerified, message, verificationDate];
}
