import 'package:flutter/material.dart';
import 'package:flutter/services.dart';
import 'package:flutter_bloc/flutter_bloc.dart';
import 'package:persistent_bottom_nav_bar/persistent_tab_view.dart';

import '../bloc/shop/shop_bloc.dart';
import '../bloc/user/user_bloc.dart';
import 'auth.dart';
import 'layout.dart';

class OnBoarding extends StatefulWidget {
  const OnBoarding({super.key});

  @override
  State<OnBoarding> createState() => _OnBoardingState();
}

class _OnBoardingState extends State<OnBoarding> {
  int currentIndex = 0;

  void _onPageChanged(int index) {
    setState(() {
      currentIndex = index;
    });
  }

  @override
  void initState() {
    super.initState();
    context.read<UserBloc>().add(LoadCurrentUserEvent());
    SystemChrome.setSystemUIOverlayStyle(const SystemUiOverlayStyle(
      statusBarColor: Colors.black,
      statusBarIconBrightness: Brightness.light,
    ));
  }

  @override
  Widget build(BuildContext context) {
    const data = [
      {
        "title": "Welcome to Style Hub",
        "description":
            "Find trendy clothes and accessories from nearby shops. Shop local, stay stylish!",
        "image": "assets/images/onboarding/1.png",
      },
      {
        "title": "Shop Local, Stay Stylish",
        "description":
            "Explore the latest fashion from neighborhood boutiques. Easy, trendy, and local.",
        "image": "assets/images/onboarding/2.png",
      },
      {
        "title": "Your Fashion Hub",
        "description":
            "Connect with local shops for unique styles and exclusive deals. Shop smarter, stay chic!",
        "image": "assets/images/onboarding/3.png",
      },
    ];

    return Scaffold(
      body: Padding(
        padding: const EdgeInsets.all(16),
        child: BlocListener<UserBloc, UserState>(
          listener: (context, state) {
            if (state.loadCurrentUserStatus == LoadCurrentUserStatus.success) {
              context.read<ShopBloc>().add(GetMyShopEvent(
                  userId: state.user?.id ?? '', token: state.user?.token));
              PersistentNavBarNavigator.pushNewScreenWithRouteSettings(
                context,
                settings: const RouteSettings(name: '/home'),
                withNavBar: false,
                screen: const Layout(),
                pageTransitionAnimation: PageTransitionAnimation.fade,
              );
            }
          },
          child: Center(
            child: Column(
              mainAxisAlignment: MainAxisAlignment.center,
              children: [
                Expanded(
                  child: PageView.builder(
                    itemCount: data.length,
                    onPageChanged:
                        _onPageChanged, // Added onPageChanged callback
                    itemBuilder: (context, index) {
                      return Column(
                        mainAxisAlignment: MainAxisAlignment.center,
                        children: [
                          Image.asset(data[currentIndex]["image"]!,
                              width: 300, height: 300),
                          Text(
                            data[currentIndex]["title"]!,
                            style: const TextStyle(
                                fontSize: 28, fontWeight: FontWeight.bold),
                            textAlign: TextAlign.center,
                          ),
                          const SizedBox(height: 8),
                          ConstrainedBox(
                            constraints: const BoxConstraints(
                              maxWidth: 340,
                            ),
                            child: SizedBox(
                              width: double.infinity,
                              child: Text(
                                data[currentIndex]["description"]!,
                                style: Theme.of(context)
                                    .textTheme
                                    .bodyLarge!
                                    .copyWith(
                                      color: Theme.of(context)
                                          .colorScheme
                                          .secondary,
                                    ),
                                textAlign: TextAlign.center,
                              ),
                            ),
                          ),
                          const SizedBox(height: 32),
                          Row(
                            mainAxisAlignment: MainAxisAlignment.center,
                            children: List.generate(data.length, (index) {
                              return GestureDetector(
                                onTap: () {
                                  _onPageChanged(index);
                                },
                                child: Container(
                                  margin: const EdgeInsets.all(4),
                                  width: 24,
                                  height: 24,
                                  decoration: BoxDecoration(
                                    color: currentIndex == index
                                        ? Theme.of(context).colorScheme.primary
                                        : Colors.grey.shade300,
                                    borderRadius: BorderRadius.circular(12),
                                  ),
                                ),
                              );
                            }),
                          ),
                          const SizedBox(height: 32),
                          ElevatedButton(
                            onPressed: () {
                              Navigator.push(
                                context,
                                MaterialPageRoute(
                                    builder: (context) => const Auth()),
                              );
                            },
                            style: ElevatedButton.styleFrom(
                              backgroundColor:
                                  Theme.of(context).colorScheme.primary,
                              maximumSize: const Size(340, 60),
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
                                "Get Started",
                                style: TextStyle(
                                  fontSize: 18,
                                  color:
                                      Theme.of(context).colorScheme.onPrimary,
                                ),
                                textAlign: TextAlign.center,
                              ),
                            ),
                          ),
                        ],
                      );
                    },
                  ),
                ),
              ],
            ),
          ),
        ),
      ),
    );
  }
}
