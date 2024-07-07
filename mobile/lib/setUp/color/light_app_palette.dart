import 'package:flutter/material.dart';

abstract class LightAppPalette {
  static const MaterialColor primary = MaterialColor(
    0xFFEE1E80,
    <int, Color>{
      50: Color(0xFFFFF0F5),
      100: Color(0xFFFFB0C9),
      200: Color(0xFFFF70A3),
      300: Color(0xFFFF3E7E),
      400: Color(0xFFFF1E63),
      500: Color(0xFFEE1E80),
      600: Color(0xFFD91E78),
      700: Color(0xFFC01A6A),
      800: Color(0xFFA8175C),
      900: Color(0xFF900D49),
    },
  );
  static const Color onPrimary = Color(0xFFFFFFFF);
  static const Color primaryContainer = Color.fromARGB(255, 237, 243, 248);
  static const Color secondary = Color(0xFF5A5D72);
  static const Color outline = Color(0xFF767680);
  static const Color onSecondary = Color(0xFFFFFFFF);
  static const Color background = Color(0xFFFBF8FF);
  static const Color surfaceContainerLow = Color(0xFFF4F2FA);
  static const Color onSurface = Color(0xFF1A1B21);
  static const Color error = Colors.red;
  static const Color onError = Color(0xFFFFFFFF);
  static const Color shimmerBase = Color(0xFFE0E0E0);
  static const Color shimmerHighlight = Color(0xFFD0D0D0);
  static const Color surfaceContainerHigh = Color.fromARGB(255, 214, 211, 226);
}
