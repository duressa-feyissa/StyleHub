import 'package:flutter/material.dart';

import '../widgets/auth/new_password.dart';
import '../widgets/auth/reset_your_password.dart';
import '../widgets/auth/signin.dart';
import '../widgets/auth/signup.dart';
import '../widgets/auth/verify_code.dart';
import '../widgets/auth/verify_password_code.dart';

class Auth extends StatefulWidget {
  const Auth({super.key});

  @override
  State<Auth> createState() => _AuthState();
}

class _AuthState extends State<Auth> {
  int currentIndex = 0;

  void onChangeIndex(int index) {
    setState(() {
      currentIndex = index;
    });
  }

  late final List<Widget> _children = [
    SignIn(changeIndex: onChangeIndex),
    SignUp(changeIndex: onChangeIndex),
    VerifyCode(changeIndex: onChangeIndex),
    ResetYourPassword(changeIndex: onChangeIndex),
    VerifyPasswordCode(changeIndex: onChangeIndex),
    NewPassword(changeIndex: onChangeIndex),
  ];

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      body: Padding(
        padding: const EdgeInsets.all(16),
        child: Center(
          child: SingleChildScrollView(
            child: ConstrainedBox(
              constraints: const BoxConstraints(
                maxWidth: 360,
              ),
              child: _children[currentIndex],
            ),
          ),
        ),
      ),
    );
  }
}
