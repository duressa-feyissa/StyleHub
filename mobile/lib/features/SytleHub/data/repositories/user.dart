import 'package:either_dart/either.dart';
import '../datasources/local/user.dart';

import '../../../../core/errors/exception.dart';
import '../../../../core/errors/failure.dart';
import '../../../../core/network/internet.dart';
import '../../domain/entities/user/password_reset_request_entity.dart';
import '../../domain/entities/user/password_reset_verification_entity.dart';
import '../../domain/entities/user/user_entity.dart';
import '../../domain/repositories/user.dart';
import '../datasources/remote/user.dart';
import '../models/user/email_verify_model.dart';

class UserRepositoryImpl implements UserRepository {
  final UserRemoteDataSource remoteDataSource;
  final UserLocalDataSource localDataSource;
  final NetworkInfo networkInfo;

  UserRepositoryImpl({
    required this.remoteDataSource,
    required this.localDataSource,
    required this.networkInfo,
  });

  @override
  Future<Either<Failure, UserEntity>> signIn({
    required String email,
    required String password,
  }) async {
    if (await networkInfo.isConnected) {
      try {
        final user =
            await remoteDataSource.signIn(email: email, password: password);
        await localDataSource.cacheUser(user);
        return Right(user);
      } on ServerException catch (e) {
        return Left(ServerFailure(message: e.message));
      }
    } else {
      return const Left(CacheFailure(message: 'No internet connection'));
    }
  }

  @override
  Future<Either<Failure, UserEntity>> signUp({
    required String firstName,
    required String lastName,
    required String email,
    required String password,
  }) async {
    if (await networkInfo.isConnected) {
      try {
        final user = await remoteDataSource.signUp(
          firstName: firstName,
          lastName: lastName,
          email: email,
          password: password,
        );
        return Right(user);
      } on ServerException catch (e) {
        return Left(ServerFailure(message: e.message));
      }
    } else {
      return const Left(CacheFailure(message: 'No internet connection'));
    }
  }

  @override
  Future<Either<Failure, String>> sendVerificationCode({
    required String email,
  }) async {
    if (await networkInfo.isConnected) {
      try {
        final data = await remoteDataSource.sendVerificationCode(email);
        return Right(data);
      } on ServerException catch (e) {
        return Left(ServerFailure(message: e.message));
      }
    } else {
      return const Left(CacheFailure(message: 'No internet connection'));
    }
  }

  @override
  Future<Either<Failure, EmailVerifyModel>> verifyCode({
    required String email,
    required String code,
  }) async {
    if (await networkInfo.isConnected) {
      try {
        final data = await remoteDataSource.verifyCode(
          email: email,
          code: code,
        );
        return Right(data);
      } on ServerException catch (e) {
        return Left(ServerFailure(message: e.message));
      }
    } else {
      return const Left(CacheFailure(message: 'No internet connection'));
    }
  }

  @override
  Future<Either<Failure, PasswordResetRequestEntity>> resetPasswordRequest({
    required String email,
  }) async {
    if (await networkInfo.isConnected) {
      try {
        final user = await remoteDataSource.resetPasswordRequest(email);
        return Right(user);
      } on ServerException catch (e) {
        return Left(ServerFailure(message: e.message));
      }
    } else {
      return const Left(CacheFailure(message: 'No internet connection'));
    }
  }

  @override
  Future<Either<Failure, UserEntity>> resetPassword({
    required String email,
    required String password,
    required String code,
  }) async {
    if (await networkInfo.isConnected) {
      try {
        final user = await remoteDataSource.resetPassword(
            email: email, password: password, code: code);

        return Right(user);
      } on ServerException catch (e) {
        return Left(ServerFailure(message: e.message));
      }
    } else {
      return const Left(CacheFailure(message: 'No internet connection'));
    }
  }

  @override
  Future<Either<Failure, PasswordResetVerificationEntity>>
      resetPasswordCodeVerification(
          {required String email, required String code}) async {
    if (await networkInfo.isConnected) {
      try {
        final user = await remoteDataSource.resetPasswordCodeVerification(
            email: email, code: code);
        return Right(user);
      } on ServerException catch (e) {
        return Left(ServerFailure(message: e.message));
      }
    } else {
      return const Left(CacheFailure(message: 'No internet connection'));
    }
  }

  @override
  Future<Either<Failure, UserEntity>> getCurrentUser() async {
    try {
      final user = await localDataSource.getUser();
      return Right(user);
    } on CacheException catch (e) {
      return Left(CacheFailure(message: e.message));
    }
  }

  @override
  Future<Either<Failure, void>> signOut() async {
    try {
      return Right(await localDataSource.deleteUser());
    } on CacheException catch (e) {
      return Left(CacheFailure(message: e.message));
    }
  }
}
