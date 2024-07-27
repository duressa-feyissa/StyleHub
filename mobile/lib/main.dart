import 'package:flutter/material.dart';
import 'package:flutter_bloc/flutter_bloc.dart';

import 'features/SytleHub/presentation/bloc/prdoct_filter/product_filter_bloc.dart';
import 'features/SytleHub/presentation/bloc/product/product_bloc.dart';
import 'features/SytleHub/presentation/bloc/scroll/scroll_bloc.dart';
import 'features/SytleHub/presentation/bloc/shop/shop_bloc.dart';
import 'features/SytleHub/presentation/bloc/user/user_bloc.dart';
import 'features/SytleHub/presentation/pages/onboarning.dart';
import 'injection_container.dart' as di;
import 'setUp/theme/ligth_theme.dart';
import 'simple_bloc_observer.dart';

void main() async {
  WidgetsFlutterBinding.ensureInitialized();
  Bloc.observer = const SimpleBlocObserver();
  await di.init();

  runApp(const Starter());
}

class Starter extends StatelessWidget {
  const Starter({super.key});
  @override
  Widget build(BuildContext context) {
    return MultiBlocProvider(
      providers: [
        BlocProvider(
          create: (context) => ScrollBloc(),
        ),
        BlocProvider(create: (context) => ProductFilterBloc()),
        BlocProvider(create: (context) => di.sl<ProductBloc>()),
        BlocProvider(create: (context) => di.sl<UserBloc>()),
        BlocProvider(create: (context) => di.sl<ShopBloc>()),
      ],
      child: MaterialApp(
          title: "Style Hub",
          debugShowCheckedModeBanner: false,
          theme: LigthTheme.theme,
          home: const OnBoarding()),
    );
  }
}
