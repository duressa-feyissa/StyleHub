import 'package:either_dart/either.dart';

import '../../../../core/errors/failure.dart';
import '../../data/models/user/email_verify_model.dart';
import '../entities/user/password_reset_request_entity.dart';
import '../entities/user/password_reset_verification_entity.dart';
import '../entities/user/user_entity.dart';

abstract class UserRepository {
  Future<Either<Failure, UserEntity>> signIn({
    required String email,
    required String password,
  });

  Future<Either<Failure, UserEntity>> signUp({
    required String firstName,
    required String lastName,
    required String email,
    required String password,
  });

  Future<Either<Failure, String>> sendVerificationCode({
    required String email,
  });

  Future<Either<Failure, EmailVerifyModel>> verifyCode({
    required String email,
    required String code,
  });

  Future<Either<Failure, PasswordResetRequestEntity>> resetPasswordRequest({
    required String email,
  });

  Future<Either<Failure, PasswordResetVerificationEntity>>
      resetPasswordCodeVerification({
    required String email,
    required String code,
  });

  Future<Either<Failure, UserEntity>> resetPassword({
    required String email,
    required String password,
    required String code,
  });

  Future<Either<Failure, void>> signOut();

  Future<Either<Failure, UserEntity>> getCurrentUser();
}
