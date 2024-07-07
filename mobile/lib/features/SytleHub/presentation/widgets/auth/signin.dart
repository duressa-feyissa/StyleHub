import 'package:flutter/material.dart';
import 'package:flutter_bloc/flutter_bloc.dart';

import '../../bloc/user/user_bloc.dart';
import '../../pages/layout.dart';
import '../CustomInputField.dart';

class SignIn extends StatefulWidget {
  const SignIn({super.key, required this.changeIndex});

  final void Function(int) changeIndex;

  @override
  State<SignIn> createState() => _SignInState();
}

class _SignInState extends State<SignIn> {
  final TextEditingController emailController = TextEditingController();
  final TextEditingController passwordController = TextEditingController();

  bool _passwordVisible = false;

  String? emailError;
  String? passwordError;

  @override
  void initState() {
    super.initState();
    context.read<UserBloc>().add(ClearStateEvent());
  }

  @override
  void dispose() {
    emailController.dispose();
    passwordController.dispose();
    super.dispose();
  }

  void validateEmail(String value) {
    setState(() {
      emailError = value.isEmpty ? 'Email cannot be empty' : null;
    });
  }

  void validatePassword(String value) {
    setState(() {
      passwordError = value.isEmpty ? 'Password cannot be empty' : null;
    });
  }

  void clearErrors() {
    setState(() {
      emailError = null;
      passwordError = null;
    });
  }

  void clearFields() {
    emailController.clear();
    passwordController.clear();
  }

  void onSignIn() {
    clearErrors();

    if (emailController.text.isEmpty) {
      setState(() {
        emailError = 'Email cannot be empty';
      });
    }

    if (passwordController.text.isEmpty) {
      setState(() {
        passwordError = 'Password cannot be empty';
      });
    }

    if (emailError == null && passwordError == null) {
      context.read<UserBloc>().add(
            SignInEvent(
              email: emailController.text,
              password: passwordController.text,
            ),
          );
    }
  }

  @override
  Widget build(BuildContext context) {
    return BlocListener<UserBloc, UserState>(
      listener: (context, state) {
        if (state.loginStatus == LoginStatus.success) {
          ScaffoldMessenger.of(context).showSnackBar(const SnackBar(
            content: Center(
                child: Text('Logged in successfully',
                    textAlign: TextAlign.center)),
          ));

          Navigator.of(context).push(
            MaterialPageRoute(
              builder: (context) => const Layout(),
            ),
          );

          clearFields();
        } else if (state.loginStatus == LoginStatus.failure) {
          ScaffoldMessenger.of(context).showSnackBar(SnackBar(
            content: Center(
                child: Text(state.errorMessage ?? 'Something went wrong',
                    textAlign: TextAlign.center)),
            backgroundColor: Theme.of(context).colorScheme.error,
          ));

          if (state.errorMessage?.trim() == 'Email not verified') {
            widget.changeIndex(2);
          }
        }
      },
      child: Column(
        mainAxisAlignment: MainAxisAlignment.center,
        children: [
          Text(
            "Sign In",
            style: Theme.of(context).textTheme.headlineLarge,
            textAlign: TextAlign.center,
          ),
          Text(
            "Enter your email and password to sign in",
            textAlign: TextAlign.center,
            style: Theme.of(context).textTheme.bodyLarge,
          ),
          const SizedBox(height: 40),
          CustomInputField(
            controller: emailController,
            hintText: 'Email',
            onChanged: validateEmail,
            errorText: emailError,
          ),
          const SizedBox(height: 16),
          Column(
            crossAxisAlignment: CrossAxisAlignment.end,
            children: [
              CustomInputField(
                controller: passwordController,
                hintText: 'Password',
                obscureText: !_passwordVisible,
                prefixIcon: Icons.lock,
                suffixIcon:
                    _passwordVisible ? Icons.visibility : Icons.visibility_off,
                onChanged: validatePassword,
                errorText: passwordError,
                suffixIconOnPressed: () {
                  setState(() {
                    _passwordVisible = !_passwordVisible;
                  });
                },
              ),
              const SizedBox(height: 8),
              GestureDetector(
                onTap: () {
                  widget.changeIndex(3);
                },
                child: Text(
                  "Forgot Password?",
                  style: Theme.of(context).textTheme.bodyMedium!.copyWith(
                        color: Theme.of(context).colorScheme.primary,
                      ),
                  textAlign: TextAlign.end,
                ),
              ),
            ],
          ),
          const SizedBox(height: 40),
          context.watch<UserBloc>().state.loginStatus == LoginStatus.loading
              ? Container(
                  height: 60,
                  decoration: BoxDecoration(
                    color: Theme.of(context).colorScheme.primary,
                    borderRadius: BorderRadius.circular(8),
                  ),
                  child: Center(
                    child: SizedBox(
                      width: 40,
                      height: 40,
                      child: CircularProgressIndicator(
                        valueColor: AlwaysStoppedAnimation<Color>(
                          Theme.of(context).colorScheme.onPrimary,
                        ),
                      ),
                    ),
                  ),
                )
              : ElevatedButton(
                  onPressed: onSignIn,
                  style: ElevatedButton.styleFrom(
                    backgroundColor: Theme.of(context).colorScheme.primary,
                    maximumSize: const Size(360, 60),
                    shape: RoundedRectangleBorder(
                      borderRadius: BorderRadius.circular(8),
                    ),
                  ),
                  child: Container(
                    width: double.infinity,
                    padding: const EdgeInsets.symmetric(
                      horizontal: 32,
                      vertical: 16,
                    ),
                    child: Text(
                      "Sign In",
                      style: TextStyle(
                        fontSize: 18,
                        color: Theme.of(context).colorScheme.onPrimary,
                      ),
                      textAlign: TextAlign.center,
                    ),
                  ),
                ),
          const SizedBox(height: 16),
          Row(
            mainAxisAlignment: MainAxisAlignment.center,
            children: [
              Text(
                "Don’t have an account?",
                style: Theme.of(context).textTheme.bodyMedium!.copyWith(
                      color: Theme.of(context).colorScheme.secondary,
                    ),
                textAlign: TextAlign.center,
              ),
              const SizedBox(width: 8),
              GestureDetector(
                onTap: () {
                  widget.changeIndex(1);
                },
                child: Text(
                  "Sign Up",
                  style: Theme.of(context).textTheme.titleMedium!.copyWith(
                        color: Theme.of(context).colorScheme.primary,
                      ),
                  softWrap: true,
                  textAlign: TextAlign.center,
                ),
              ),
            ],
          ),
        ],
      ),
    );
  }
}
