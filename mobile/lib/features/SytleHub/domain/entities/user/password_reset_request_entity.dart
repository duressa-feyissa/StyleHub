import 'package:equatable/equatable.dart';

class PasswordResetRequestEntity extends Equatable {
  final String email;
  final String message;
  final String status;
  final DateTime expirationDate;

  const PasswordResetRequestEntity({
    required this.email,
    required this.message,
    required this.status,
    required this.expirationDate,
  });

  @override
  List<Object?> get props => [email, message, status, expirationDate];
}


