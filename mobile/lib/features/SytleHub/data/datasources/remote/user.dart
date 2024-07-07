import 'dart:convert';

import 'package:http/http.dart' show Client;

import '../../../../../core/errors/exception.dart';
import '../../../../../setUp/url/urls.dart';
import '../../models/user/email_verify_model.dart';
import '../../models/user/password_reset_request_model.dart';
import '../../models/user/password_reset_verification_model.dart';
import '../../models/user/user_model.dart';

abstract class UserRemoteDataSource {
  Future<UserModel> signIn({
    required String email,
    required String password,
  });
  Future<UserModel> signUp({
    required String firstName,
    required String lastName,
    required String email,
    required String password,
  });

  Future<String> sendVerificationCode(String email);
  Future<EmailVerifyModel> verifyCode({
    required String email,
    required String code,
  });
  Future<PasswordResetRequestModel> resetPasswordRequest(String email);
  Future<PasswordResetVerificationModel> resetPasswordCodeVerification({
    required String email,
    required String code,
  });
  Future<UserModel> resetPassword({
    required String email,
    required String code,
    required String password,
  });
}

class UserRemoteDataSourceImpl implements UserRemoteDataSource {
  final Client client;

  UserRemoteDataSourceImpl({required this.client});

  @override
  Future<UserModel> signIn(
      {required String email, required String password}) async {
    final response = await client.post(
      Uri.parse(Urls.signIn),
      body: jsonEncode({
        "loginRequest": {
          "email": email,
          "password": password,
        }
      }),
      headers: {'Content-Type': 'application/json'},
    );
    if (response.statusCode == 200) {
      return UserModel.fromJson(json.decode(response.body)['data']);
    } else {
      throw ServerException(
          message: json.decode(response.body)['Message'],
          statusCode: response.statusCode);
    }
  }

  @override
  Future<UserModel> signUp({
    required String firstName,
    required String lastName,
    required String email,
    required String password,
  }) async {
    final response = await client.post(
      Uri.parse(Urls.signUp),
      body: jsonEncode({
        "registeration": {
          'firstName': firstName,
          'lastName': lastName,
          'email': email,
          'password': password
        }
      }),
      headers: {'Content-Type': 'application/json'},
    );
    if (response.statusCode == 200) {
      return UserModel.fromJson(json.decode(response.body)['data']);
    } else {
      throw ServerException(
          message: json.decode(response.body)['Message'],
          statusCode: response.statusCode);
    }
  }

  @override
  Future<String> sendVerificationCode(String email) async {
    final response = await client.post(
      Uri.parse(Urls.sendVerificationCode),
      body: jsonEncode({'email': email}),
      headers: {'Content-Type': 'application/json'},
    );
    if (response.statusCode == 200) {
      return json.decode(response.body)['data'];
    } else {
      throw ServerException(
          message: json.decode(response.body)['Message'],
          statusCode: response.statusCode);
    }
  }

  @override
  Future<EmailVerifyModel> verifyCode({
    required String email,
    required String code,
  }) async {
    final response = await client.post(
      Uri.parse(Urls.verifyCode),
      body: jsonEncode({'email': email, 'code': code}),
      headers: {'Content-Type': 'application/json'},
    );
    print(response.body);
    if (response.statusCode == 200) {
      return EmailVerifyModel.fromJson(json.decode(response.body));
    } else {
      throw ServerException(
          message: json.decode(response.body)['Message'],
          statusCode: response.statusCode);
    }
  }

  @override
  Future<PasswordResetRequestModel> resetPasswordRequest(String email) async {
    final response = await client.post(
      Uri.parse(Urls.resetPasswordRequest),
      body: jsonEncode({'email': email}),
      headers: {'Content-Type': 'application/json'},
    );
    if (response.statusCode == 200) {
      return PasswordResetRequestModel.fromJson(json.decode(response.body));
    } else {
      throw ServerException(
          message: json.decode(response.body)['Message'],
          statusCode: response.statusCode);
    }
  }

  @override
  Future<UserModel> resetPassword({
    required String email,
    required String password,
    required String code,
  }) async {
    final response = await client.post(
      Uri.parse(Urls.resetPassword),
      body: jsonEncode({'email': email, 'password': password, 'code': code}),
      headers: {'Content-Type': 'application/json'},
    );
    if (response.statusCode == 200) {
      return UserModel.fromJson(json.decode(response.body));
    } else {
      throw ServerException(
          message: json.decode(response.body)['Message'],
          statusCode: response.statusCode);
    }
  }

  @override
  Future<PasswordResetVerificationModel> resetPasswordCodeVerification(
      {required String email, required String code}) async {
    final response = await client.post(
      Uri.parse(Urls.resetPasswordCodeVerification),
      body: jsonEncode({'email': email, 'code': code}),
      headers: {'Content-Type': 'application/json'},
    );
    if (response.statusCode == 200) {
      return PasswordResetVerificationModel.fromJson(
          json.decode(response.body));
    } else {
      throw ServerException(
          message: json.decode(response.body)['Message'],
          statusCode: response.statusCode);
    }
  }
}
