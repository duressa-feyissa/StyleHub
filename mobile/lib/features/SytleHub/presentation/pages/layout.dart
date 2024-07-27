import 'package:flutter/material.dart';
import 'package:flutter_bloc/flutter_bloc.dart';
import 'package:persistent_bottom_nav_bar/persistent_tab_view.dart';
import 'package:style_hub/features/SytleHub/presentation/bloc/shop/shop_bloc.dart';
import 'package:style_hub/features/SytleHub/presentation/pages/chat.dart';
import 'package:style_hub/features/SytleHub/presentation/pages/shop.dart';

import '../bloc/product/product_bloc.dart';
import '../bloc/scroll/scroll_bloc.dart';
import '../bloc/user/user_bloc.dart';
import 'category.dart';
import 'home.dart';
import 'profile.dart';

class Layout extends StatefulWidget {
  const Layout({super.key});

  @override
  State<StatefulWidget> createState() {
    return _LayoutState();
  }
}

class _LayoutState extends State<Layout> {
  final PersistentTabController _controller =
      PersistentTabController(initialIndex: 0);

  @override
  void initState() {
    context.read<ProductBloc>().add(GetProductsEvent(
        token: context.read<UserBloc>().state.user?.token ?? ''));
    context.read<ScrollBloc>().add(ScrollEventInitial());
    context.read<ProductBloc>().add(GetColorsEvent());
    context.read<ProductBloc>().add(GetBrandsEvent());
    context.read<ProductBloc>().add(GetMaterialsEvent());
    context.read<ProductBloc>().add(GetSizesEvent());
    context.read<ProductBloc>().add(GetCategoriesEvent());
    context.read<ProductBloc>().add(GetLocationsEvent());
    context.read<ProductBloc>().add(GetDesignsEvent());
    context.read<ProductBloc>().add(GetDomainsEvent());
    context.read<ShopBloc>().add(GetAllShopEvent(
        token: context.read<UserBloc>().state.user?.token ?? ''));

    super.initState();
  }

  @override
  void dispose() {
    _controller.dispose();
    super.dispose();
  }

  List<PersistentBottomNavBarItem> _navBarsItems(BuildContext context) {
    return [
      PersistentBottomNavBarItem(
        icon: const Icon(Icons.home),
        inactiveIcon: const Icon(Icons.home_outlined),
        title: 'Home',
        activeColorPrimary: Theme.of(context).colorScheme.primary,
        inactiveColorPrimary: Theme.of(context).colorScheme.outline,
      ),
      PersistentBottomNavBarItem(
        icon: const Icon(Icons.dashboard_customize),
        inactiveIcon: const Icon(Icons.dashboard_customize_outlined),
        title: 'Category',
        activeColorPrimary: Theme.of(context).colorScheme.primary,
        inactiveColorPrimary: Theme.of(context).colorScheme.outline,
      ),
      PersistentBottomNavBarItem(
        icon: const Icon(Icons.storefront_rounded),
        inactiveIcon: const Icon(Icons.storefront_outlined),
        title: 'Shops',
        activeColorPrimary: Theme.of(context).colorScheme.primary,
        inactiveColorPrimary: Theme.of(context).colorScheme.outline,
      ),
      PersistentBottomNavBarItem(
        icon: const Icon(Icons.chat_bubble),
        inactiveIcon: const Icon(Icons.chat_bubble_outline),
        title: 'Chat',
        activeColorPrimary: Theme.of(context).colorScheme.primary,
        inactiveColorPrimary: Theme.of(context).colorScheme.outline,
      ),
      PersistentBottomNavBarItem(
        icon: const Icon(Icons.person),
        inactiveIcon: const Icon(Icons.person_outline),
        title: 'Profile',
        activeColorPrimary: Theme.of(context).colorScheme.primary,
        inactiveColorPrimary: Theme.of(context).colorScheme.outline,
      )
    ];
  }

  List<Widget> _buildScreens() {
    return [
      const HomeScreen(),
      const CategoryScreen(),
      const ShopScreen(),
      const ChatScreen(),
      const ProfileScreen(),
    ];
  }

  @override
  Widget build(BuildContext context) {
    return PersistentTabView(
      context,
      controller: _controller,
      screens: _buildScreens(),
      items: _navBarsItems(context),
      confineInSafeArea: true,
      handleAndroidBackButtonPress: true,
      resizeToAvoidBottomInset: true,
      stateManagement: true,
      hideNavigationBarWhenKeyboardShows: true,
      hideNavigationBar: !context.watch<ScrollBloc>().state.isVisible,
      popAllScreensOnTapOfSelectedTab: true,
      popActionScreens: PopActionScreensType.all,
      itemAnimationProperties: const ItemAnimationProperties(
        duration: Duration(milliseconds: 200),
        curve: Curves.easeInOut,
      ),
      screenTransitionAnimation: const ScreenTransitionAnimation(
        animateTabTransition: true,
        curve: Curves.easeInOut,
        duration: Duration(milliseconds: 200),
      ),
      navBarStyle: NavBarStyle.style8,
    );
  }
}
