import 'package:flutter/material.dart';
import 'package:flutter_bloc/flutter_bloc.dart';

import '../../bloc/user/user_bloc.dart';
import '../common/CustomInputField.dart';

class SignUp extends StatefulWidget {
  const SignUp({super.key, required this.changeIndex});

  final void Function(int) changeIndex;

  @override
  State<SignUp> createState() => _SignUpState();
}

class _SignUpState extends State<SignUp> {
  final TextEditingController firstNameController = TextEditingController();
  final TextEditingController lastNameController = TextEditingController();
  final TextEditingController phoneController = TextEditingController();
  final TextEditingController passwordController = TextEditingController();
  final TextEditingController confirmPasswordController =
      TextEditingController();

  bool _passwordVisible = false;
  bool _confirmPasswordVisible = false;

  String? firstNameError;
  String? lastNameError;
  String? phoneError;
  String? passwordError;
  String? confirmPasswordError;

  @override
  void initState() {
    super.initState();
    context.read<UserBloc>().add(ClearStateEvent());
  }

  @override
  void dispose() {
    firstNameController.dispose();
    lastNameController.dispose();
    phoneController.dispose();
    passwordController.dispose();
    confirmPasswordController.dispose();
    super.dispose();
  }

  void validateFirstName(String value) {
    setState(() {
      firstNameError = value.isEmpty ? 'First name cannot be empty' : null;
    });
  }

  void validateLastName(String value) {
    setState(() {
      lastNameError = value.isEmpty ? 'Last name cannot be empty' : null;
    });
  }

  void validatePhone(String value) {
    setState(() {
      phoneError = value.isEmpty ? 'Phone number cannot be empty' : null;
    });
  }

  void validatePassword(String value) {
    setState(() {
      passwordError = value.isEmpty ? 'Password cannot be empty' : null;
    });
  }

  void validateConfirmPassword(String value) {
    setState(() {
      confirmPasswordError =
          value != passwordController.text ? 'Passwords do not match' : null;
    });
  }

  void clearErrors() {
    setState(() {
      firstNameError = null;
      lastNameError = null;
      phoneError = null;
      passwordError = null;
      confirmPasswordError = null;
    });
  }

  void onSignUp() {
    clearErrors();

    validateFirstName(firstNameController.text);
    validateLastName(lastNameController.text);
    validatePhone(phoneController.text);
    validatePassword(passwordController.text);
    validateConfirmPassword(confirmPasswordController.text);

    if (firstNameError == null &&
        lastNameError == null &&
        phoneError == null &&
        passwordError == null &&
        confirmPasswordError == null) {
      context.read<UserBloc>().add(
            SignUpEvent(
              firstName: firstNameController.text,
              lastName: lastNameController.text,
              email: phoneController.text,
              password: passwordController.text,
            ),
          );
    }
  }

  @override
  Widget build(BuildContext context) {
    return BlocListener<UserBloc, UserState>(
      listener: (context, state) {
        if (state.signUpStatus == SignUpStatus.success) {
          ScaffoldMessenger.of(context).showSnackBar(const SnackBar(
            content: Center(
                child: Text('Account created successfully! Check your email',
                    textAlign: TextAlign.center)),
          ));
          widget.changeIndex(2);
        } else if (state.signUpStatus == SignUpStatus.failure) {
          ScaffoldMessenger.of(context).showSnackBar(SnackBar(
            content: Center(
                child: Text(state.errorMessage ?? 'Something went wrong',
                    textAlign: TextAlign.center)),
            backgroundColor: Theme.of(context).colorScheme.error,
          ));
        }
      },
      child: BlocBuilder<UserBloc, UserState>(
        builder: (context, state) {
          return Column(
            mainAxisAlignment: MainAxisAlignment.center,
            children: [
              Text(
                "Create Account",
                style: Theme.of(context).textTheme.headlineLarge,
                textAlign: TextAlign.center,
              ),
              Text(
                "Fill your information below or register with your social account",
                textAlign: TextAlign.center,
                style: Theme.of(context).textTheme.bodyLarge,
              ),
              const SizedBox(height: 40),
              CustomInputField(
                controller: firstNameController,
                hintText: 'First Name',
                onChanged: validateFirstName,
                errorText: firstNameError,
              ),
              const SizedBox(height: 16),
              CustomInputField(
                controller: lastNameController,
                hintText: 'Last Name',
                onChanged: validateLastName,
                errorText: lastNameError,
              ),
              const SizedBox(height: 16),
              CustomInputField(
                controller: phoneController,
                hintText: 'Phone Number',
                prefixIcon: Icons.phone,
                onChanged: validatePhone,
                errorText: phoneError,
              ),
              const SizedBox(height: 16),
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
              const SizedBox(height: 16),
              CustomInputField(
                controller: confirmPasswordController,
                hintText: 'Confirm Password',
                obscureText: !_confirmPasswordVisible,
                prefixIcon: Icons.lock,
                suffixIcon: _confirmPasswordVisible
                    ? Icons.visibility
                    : Icons.visibility_off,
                onChanged: validateConfirmPassword,
                errorText: confirmPasswordError,
                suffixIconOnPressed: () {
                  setState(() {
                    _confirmPasswordVisible = !_confirmPasswordVisible;
                  });
                },
              ),
              const SizedBox(height: 40),
              context.watch<UserBloc>().state.signUpStatus ==
                      SignUpStatus.loading
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
                      onPressed: onSignUp,
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
                          "Sign Up",
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
                    "Already have an account?",
                    style: Theme.of(context).textTheme.bodyMedium!.copyWith(
                          color: Theme.of(context).colorScheme.secondary,
                        ),
                    textAlign: TextAlign.center,
                  ),
                  const SizedBox(width: 8),
                  GestureDetector(
                    onTap: () {
                      widget.changeIndex(0);
                    },
                    child: Text(
                      "Sign In",
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
          );
        },
      ),
    );
  }
}
