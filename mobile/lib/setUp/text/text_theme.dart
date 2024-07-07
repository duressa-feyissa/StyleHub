import 'package:flutter/material.dart';

class ExtendedTextTheme extends TextTheme {
  final TextStyle titleModerate;
  final TextStyle bodyXSmall;

  const ExtendedTextTheme(this.titleModerate, this.bodyXSmall,
      {super.displayLarge,
      super.displayMedium,
      super.displaySmall,
      super.headlineLarge,
      super.headlineMedium,
      super.headlineSmall,
      super.titleLarge,
      super.titleMedium,
      super.titleSmall,
      super.bodyLarge,
      super.bodyMedium,
      super.bodySmall});
}
