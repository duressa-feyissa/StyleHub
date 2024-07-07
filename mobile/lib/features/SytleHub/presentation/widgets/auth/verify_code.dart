import 'package:flutter/material.dart';
import 'package:flutter_bloc/flutter_bloc.dart';
import 'package:flutter_otp_text_field/flutter_otp_text_field.dart';

import '../../bloc/user/user_bloc.dart';

class VerifyCode extends StatefulWidget {
  const VerifyCode({super.key, required this.changeIndex});
  final void Function(int) changeIndex;

  @override
  State<VerifyCode> createState() => _VerifyCodeState();
}

class _VerifyCodeState extends State<VerifyCode> {
  final TextEditingController codeController = TextEditingController();

  @override
  void dispose() {
    codeController.dispose();
    super.dispose();
  }

  @override
  Widget build(BuildContext context) {
    return BlocListener<UserBloc, UserState>(
      listener: (context, state) {
        if (state.verifyEmailStatus == VerifyEmailStatus.success) {
          ScaffoldMessenger.of(context).showSnackBar(
            const SnackBar(
              content: Center(child: Text("Email verified")),
              duration: Duration(seconds: 1),
            ),
          );
          widget.changeIndex(0);
        } else if (state.verifyEmailStatus == VerifyEmailStatus.failure) {
          ScaffoldMessenger.of(context).showSnackBar(SnackBar(
            content: Center(
                child: Text(state.errorMessage ?? 'Something went wrong',
                    textAlign: TextAlign.center)),
            backgroundColor: Theme.of(context).colorScheme.error,
          ));
        }
      },
      child: Column(
        mainAxisAlignment: MainAxisAlignment.center,
        children: [
          Text(
            "Verify Email",
            style: Theme.of(context).textTheme.headlineLarge,
            textAlign: TextAlign.center,
          ),
          Wrap(
            alignment: WrapAlignment.center,
            children: [
              Text(
                "Please enter the code we just sent to email",
                textAlign: TextAlign.center,
                style: Theme.of(context).textTheme.bodyLarge,
              ),
              const SizedBox(width: 8),
              Text(
                context.watch<UserBloc>().state.email ?? '',
                textAlign: TextAlign.center,
                style: Theme.of(context).textTheme.bodyLarge!.copyWith(
                      color: Theme.of(context).colorScheme.primary,
                    ),
              ),
            ],
          ),
          const SizedBox(height: 40),
          OtpTextField(
            numberOfFields: 4,
            filled: true,
            fieldWidth: 50,
            margin: const EdgeInsets.only(right: 12),
            enabledBorderColor: Theme.of(context).colorScheme.primaryContainer,
            fillColor: Theme.of(context).colorScheme.primaryContainer,
            focusedBorderColor: Theme.of(context).colorScheme.primary,
            showFieldAsBox: true,
            onCodeChanged: (String code) {
              codeController.text += code;
            },
            onSubmit: (String verificationCode) {
              context.read<UserBloc>().add(VerifyEmailEvent(
                  email: context.read<UserBloc>().state.email ?? '',
                  code: verificationCode));

              ScaffoldMessenger.of(context).showSnackBar(
                const SnackBar(
                  content: Center(child: Text("Verifying code")),
                  duration: Duration(seconds: 1),
                ),
              );
            },
          ),
          const SizedBox(height: 40),
          context.watch<UserBloc>().state.verifyEmailStatus ==
                  VerifyEmailStatus.loading
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
                  onPressed: () {
                    ScaffoldMessenger.of(context).showSnackBar(
                      SnackBar(
                        content: Center(
                          child: Text(
                            "Verifying code should be 4 digits.",
                            style: TextStyle(
                              color: Theme.of(context).colorScheme.onError,
                            ),
                          ),
                        ),
                        backgroundColor: Theme.of(context).colorScheme.error,
                      ),
                    );
                  },
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
                      "Verify",
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
                "Didn’t receive OPT?",
                style: Theme.of(context).textTheme.bodyMedium!.copyWith(
                      color: Theme.of(context).colorScheme.secondary,
                    ),
                textAlign: TextAlign.center,
              ),
              const SizedBox(width: 8),
              GestureDetector(
                onTap: () {
                  context.read<UserBloc>().add(
                      SendVerificationEmailRequestEvent(
                          email: context.read<UserBloc>().state.email ?? ''));
                  ScaffoldMessenger.of(context).showSnackBar(
                    const SnackBar(
                      content: Center(child: Text("Code sent to email")),
                      duration: Duration(seconds: 1),
                    ),
                  );
                },
                child: Text(
                  "Resend Code",
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
