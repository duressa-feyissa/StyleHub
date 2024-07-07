abstract class Urls {
  // Base URL
  static const String baseUrl = 'https://stylehub-mgow.onrender.com/api';

  // Product
  static const String color = '$baseUrl/Color';
  static const String product = '$baseUrl/Product';
  static const String category = '$baseUrl/Category';
  static const String brand = '$baseUrl/Brand';
  static const String size = '$baseUrl/Size';
  static const String material = '$baseUrl/Material';
  static const String location = '$baseUrl/Location';
  static const String design = '$baseUrl/Design';
  static const String domain = '$baseUrl/Category/domain/detail';

  // User
  static const String signIn = '$baseUrl/Authentication/Login';
  static const String signUp = '$baseUrl/Authentication/Register';
  static const String sendVerificationCode =
      '$baseUrl/Authentication/Send-Verfication-Email-Code';
  static const String verifyCode = '$baseUrl/Authentication/Verify-Email';
  static const String resetPasswordRequest =
      '$baseUrl/Authentication/Send-Reset-Password-Code';
  static const String resetPassword = '$baseUrl/Authentication/Reset-Password';
  static const String resetPasswordCodeVerification =
      '$baseUrl/Authentication/Verify-Password-Reset-Code';
}
