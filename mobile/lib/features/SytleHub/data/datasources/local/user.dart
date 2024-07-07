import 'dart:convert';

import 'package:shared_preferences/shared_preferences.dart';

import '../../../../../core/errors/exception.dart';
import '../../models/user/user_model.dart';

abstract class UserLocalDataSource {
  Future<void> cacheUser(UserModel user);
  Future<UserModel> getUser();
  Future<void> deleteUser();
}

class UserLocalDataSourceImpl implements UserLocalDataSource {
  final SharedPreferences sharedPreferences;

  UserLocalDataSourceImpl({required this.sharedPreferences});

  @override
  Future<void> cacheUser(UserModel user) {
    return sharedPreferences.setString('user', jsonEncode(user.toJson()));
  }

  @override
  Future<void> deleteUser() {
    return sharedPreferences.remove('user');
  }

  @override
  Future<UserModel> getUser() {
    final userJson = sharedPreferences.getString('user');
    if (userJson != null) {
      return Future.value(UserModel.fromJson(jsonDecode(userJson)));
    } else {
      throw const CacheException(message: 'No user cached');
    }
  }
}
