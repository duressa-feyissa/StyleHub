import 'package:flutter/material.dart';
import 'package:flutter_bloc/flutter_bloc.dart';
import 'package:flutter_svg/svg.dart';
import 'package:persistent_bottom_nav_bar/persistent_tab_view.dart';
import 'package:style_hub/features/SytleHub/presentation/bloc/shop/shop_bloc.dart';
import 'package:style_hub/setUp/size/app_size.dart';

import '../../bloc/user/user_bloc.dart';
import '../../pages/my_shop.dart';

class AppBarTwo extends StatelessWidget {
  const AppBarTwo({
    super.key,
  });

  @override
  Widget build(BuildContext context) {
    String imageLink = '';
    if (context.watch<UserBloc>().state.user != null) {
      imageLink = context.watch<UserBloc>().state.user!.profilePicture ?? '';
    }
    return Row(
      children: [
        CircleAvatar(
          backgroundImage: imageLink.isNotEmpty
              ? NetworkImage(
                  imageLink,
                ) as ImageProvider
              : const AssetImage(
                  "assets/images/Screens/person.png",
                ),
          radius: 22.5,
        ),
        const Spacer(),
        Row(
          children: [
            ElevatedButton.icon(
              onPressed: () {
                if (context.read<ShopBloc>().state.myShopId != null) {
                  PersistentNavBarNavigator.pushNewScreenWithRouteSettings(
                    context,
                    settings: const RouteSettings(name: '/shop/review'),
                    withNavBar: false,
                    screen: const MyShopScreen(),
                    pageTransitionAnimation: PageTransitionAnimation.fade,
                  );
                }
              },
              style: ElevatedButton.styleFrom(
                backgroundColor: Theme.of(context).colorScheme.primary,
                foregroundColor: Theme.of(context).colorScheme.onPrimary,
                shape: RoundedRectangleBorder(
                  borderRadius: BorderRadius.circular(20),
                ),
                elevation: 1,
                padding:
                    const EdgeInsets.symmetric(horizontal: 12, vertical: 8),
              ),
              icon: const Icon(Icons.storefront, size: 20),
              label: Text(
                'My Shop',
                style: Theme.of(context).textTheme.titleSmall!.copyWith(
                      color: Theme.of(context).colorScheme.onPrimary,
                    ),
              ),
            ),
            const SizedBox(width: AppSize.smallSize),
            SvgPicture.asset(
              "assets/icons/notifaction.svg",
              height: 32,
            ),
          ],
        ),
      ],
    );
  }
}
