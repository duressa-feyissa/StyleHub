import 'package:flutter/material.dart';
import 'package:flutter_bloc/flutter_bloc.dart';

import '../../bloc/user/user_bloc.dart';
import '../CustomInputField.dart';

class ResetYourPassword extends StatefulWidget {
  const ResetYourPassword({super.key, required this.changeIndex});

  final void Function(int) changeIndex;

  @override
  State<ResetYourPassword> createState() => _ResetYourPasswordState();
}

class _ResetYourPasswordState extends State<ResetYourPassword> {
  final TextEditingController emailController = TextEditingController();
  String? emailError;

  @override
  void dispose() {
    emailController.dispose();
    super.dispose();
  }

  void validateEmail(String value) {
    setState(() {
      emailError = value.isEmpty ? 'Email cannot be empty' : null;
    });
  }

  void onClearEmailError() {
    setState(() {
      emailError = null;
    });
  }

  void onResetPasswordRequest() {
    if (emailError == null) {
      context.read<UserBloc>().add(
            ResetPasswordRequestEvent(email: emailController.text),
          );
    } else {
      ScaffoldMessenger.of(context).showSnackBar(
        SnackBar(
          content: Center(
            child: Text(
              'Please enter a valid email address',
              style: TextStyle(
                color: Theme.of(context).colorScheme.onError,
              ),
            ),
          ),
          backgroundColor: Theme.of(context).colorScheme.error,
        ),
      );
    }
  }

  @override
  Widget build(BuildContext context) {
    return BlocListener<UserBloc, UserState>(
      listener: (context, state) {
        if (state.resetPasswordRequestStatus ==
            ResetPasswordRequestStatus.success) {
          widget.changeIndex(4);
        } else if (state.resetPasswordRequestStatus ==
            ResetPasswordRequestStatus.failure) {
          ScaffoldMessenger.of(context).showSnackBar(
            SnackBar(
              content: Center(
                child: Text(
                  state.errorMessage ?? 'Something went wrong',
                  style: TextStyle(
                    color: Theme.of(context).colorScheme.onError,
                  ),
                ),
              ),
              backgroundColor: Theme.of(context).colorScheme.error,
            ),
          );
        }
      },
      child: Column(
        mainAxisAlignment: MainAxisAlignment.center,
        children: [
          Text(
            "Reset Password",
            style: Theme.of(context).textTheme.headlineLarge,
            textAlign: TextAlign.center,
          ),
          Text(
            "Forget your password? No problem! Enter your email below, and we'll send a reset link your way.",
            textAlign: TextAlign.center,
            style: Theme.of(context).textTheme.bodyLarge,
          ),
          const SizedBox(height: 40),
          CustomInputField(
            controller: emailController,
            prefixIcon: Icons.email,
            hintText: 'Email',
            onChanged: validateEmail,
            errorText: emailError,
          ),
          const SizedBox(height: 40),
          context.watch<UserBloc>().state.resetPasswordRequestStatus ==
                  ResetPasswordRequestStatus.loading
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
                  onPressed: onResetPasswordRequest,
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
                      "Send",
                      style: TextStyle(
                        fontSize: 18,
                        color: Theme.of(context).colorScheme.onPrimary,
                      ),
                      textAlign: TextAlign.center,
                    ),
                  ),
                ),
        ],
      ),
    );
  }
}
