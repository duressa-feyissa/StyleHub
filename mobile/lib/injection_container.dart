import 'package:get_it/get_it.dart';
import 'package:http/http.dart' as http;
import 'package:internet_connection_checker/internet_connection_checker.dart';
import 'package:shared_preferences/shared_preferences.dart';
import 'package:style_hub/features/SytleHub/domain/usecases/shop/get_shop.dart';
import 'package:style_hub/features/SytleHub/domain/usecases/shop/get_shop_by_id.dart';
import 'package:style_hub/features/SytleHub/domain/usecases/shop/get_shop_products.dart';
import 'package:style_hub/features/SytleHub/domain/usecases/shop/get_shop_products_images.dart';
import 'package:style_hub/features/SytleHub/domain/usecases/shop/get_shop_products_video.dart';
import 'package:style_hub/features/SytleHub/domain/usecases/shop/get_shop_reviews.dart';
import 'package:style_hub/features/SytleHub/domain/usecases/shop/get_shop_working_hour.dart';
import 'package:style_hub/features/SytleHub/presentation/bloc/shop/shop_bloc.dart';

import 'core/network/internet.dart';
import 'features/SytleHub/data/datasources/local/user.dart';
import 'features/SytleHub/data/datasources/remote/product.dart';
import 'features/SytleHub/data/datasources/remote/shop.dart';
import 'features/SytleHub/data/datasources/remote/user.dart';
import 'features/SytleHub/data/repositories/product.dart';
import 'features/SytleHub/data/repositories/shop.dart';
import 'features/SytleHub/data/repositories/user.dart';
import 'features/SytleHub/domain/repositories/product.dart';
import 'features/SytleHub/domain/repositories/shop.dart';
import 'features/SytleHub/domain/repositories/user.dart';
import 'features/SytleHub/domain/usecases/product/get_brand.dart';
import 'features/SytleHub/domain/usecases/product/get_category.dart';
import 'features/SytleHub/domain/usecases/product/get_color.dart';
import 'features/SytleHub/domain/usecases/product/get_design.dart';
import 'features/SytleHub/domain/usecases/product/get_domain.dart';
import 'features/SytleHub/domain/usecases/product/get_location.dart';
import 'features/SytleHub/domain/usecases/product/get_material.dart';
import 'features/SytleHub/domain/usecases/product/get_product.dart';
import 'features/SytleHub/domain/usecases/product/get_size.dart';
import 'features/SytleHub/domain/usecases/shop/add_image.dart';
import 'features/SytleHub/domain/usecases/shop/add_product.dart';
import 'features/SytleHub/domain/usecases/shop/delete_product.dart';
import 'features/SytleHub/domain/usecases/user/load_currect_user.dart';
import 'features/SytleHub/domain/usecases/user/password_reset_verify_code.dart';
import 'features/SytleHub/domain/usecases/user/reset_password.dart';
import 'features/SytleHub/domain/usecases/user/reset_password_request.dart';
import 'features/SytleHub/domain/usecases/user/send_verification_code.dart';
import 'features/SytleHub/domain/usecases/user/sign_in.dart';
import 'features/SytleHub/domain/usecases/user/sign_out.dart';
import 'features/SytleHub/domain/usecases/user/sign_up.dart';
import 'features/SytleHub/domain/usecases/user/verify_code.dart';
import 'features/SytleHub/presentation/bloc/product/product_bloc.dart';
import 'features/SytleHub/presentation/bloc/user/user_bloc.dart';

final sl = GetIt.instance;

Future<void> init() async {
  // Features
  // - Product
  sl.registerFactory(() => ProductBloc(
        getDesignsUseCase: sl(),
        getColorsUseCase: sl(),
        getBrandsUseCase: sl(),
        getMaterialsUseCase: sl(),
        getSizesUseCase: sl(),
        getCategoriesUseCase: sl(),
        getLocationUseCase: sl(),
        getDomainsUseCase: sl(),
        getProductsUseCase: sl(),
      ));

  // User
  sl.registerFactory(() => UserBloc(
        signInUseCase: sl(),
        signUpUseCase: sl(),
        sendVerificationCodeUseCase: sl(),
        verifyCodeUseCase: sl(),
        resetPasswordRequestUseCase: sl(),
        resetPasswordUseCase: sl(),
        verifyPasswordCodeUseCase: sl(),
        loadCurrentUserUseCase: sl(),
        signOutUseCase: sl(),
      ));

  // Shop
  sl.registerFactory(() => ShopBloc(
        getShopByIdUseCase: sl(),
        getShopProductsImageUseCase: sl(),
        getShopProductsVideoUseCase: sl(),
        getShopProductUseCase: sl(),
        getShopReviewUseCase: sl(),
        getShopWorkingHourUseCase: sl(),
        getShopUseCase: sl(),
        addImageUseCase: sl(),
        addProductsUseCase: sl(),
        deleteProductByIdUseCase: sl(),
      ));

  // Use cases
  // - Product
  sl.registerLazySingleton(() => GetColorsUseCase(sl()));
  sl.registerLazySingleton(() => GetBrandsUseCase(sl()));
  sl.registerLazySingleton(() => GetMaterialsUseCase(sl()));
  sl.registerLazySingleton(() => GetSizesUseCase(sl()));
  sl.registerLazySingleton(() => GetCategoriesUseCase(sl()));
  sl.registerLazySingleton(() => GetLocationUseCase(sl()));
  sl.registerLazySingleton(() => GetDesignsUseCase(sl()));
  sl.registerLazySingleton(() => GetDomainsUseCase(sl()));
  sl.registerLazySingleton(() => GetProductsUseCase(sl()));

  // - User
  sl.registerLazySingleton(() => SignInUseCase(sl()));
  sl.registerLazySingleton(() => SignUpUseCase(sl()));
  sl.registerLazySingleton(() => SendVerificationCodeUseCase(sl()));
  sl.registerLazySingleton(() => VerifyCodeUseCase(sl()));
  sl.registerLazySingleton(() => ResetPasswordRequestUseCase(sl()));
  sl.registerLazySingleton(() => ResetPasswordUseCase(sl()));
  sl.registerLazySingleton(() => PasswordResetVerifyCodeUseCase(sl()));
  sl.registerLazySingleton(() => LoadCurrectUserUseCase(sl()));
  sl.registerLazySingleton(() => SignOutUseCase(sl()));

  // - Shop
  sl.registerLazySingleton(() => GetShopByIdUseCase(sl()));
  sl.registerLazySingleton(() => GetShopProductsImageUseCase(sl()));
  sl.registerLazySingleton(() => GetShopProductsVideoUseCase(sl()));
  sl.registerLazySingleton(() => GetShopProductUseCase(sl()));
  sl.registerLazySingleton(() => GetShopReviewUseCase(sl()));
  sl.registerLazySingleton(() => GetShopWorkingHourUseCase(sl()));
  sl.registerLazySingleton(() => GetShopUseCase(sl()));
  sl.registerLazySingleton(() => AddImageUseCase(sl()));
  sl.registerLazySingleton(() => AddProductsUseCase(sl()));
  sl.registerLazySingleton(() => DeleteProductByIdUseCase(sl()));

  // Repository
  // - Product
  sl.registerLazySingleton<ProductRepository>(
    () => ProductRepositoryImpl(remoteDataSource: sl(), networkInfo: sl()),
  );

  sl.registerLazySingleton<UserRepository>(
    () => UserRepositoryImpl(
        remoteDataSource: sl(), networkInfo: sl(), localDataSource: sl()),
  );

  sl.registerLazySingleton<ShopRepository>(
    () => ShopRepositoryImpl(remoteDataSource: sl(), networkInfo: sl()),
  );

  // Data sources - Remote
  // - Product
  sl.registerLazySingleton<ProductRemoteDataSource>(
      () => ProductRemoteDataSourceImpl(client: sl()));

  sl.registerLazySingleton<UserRemoteDataSource>(
      () => UserRemoteDataSourceImpl(client: sl()));

  sl.registerLazySingleton<ShopRemoteDataSource>(
      () => ShopRemoteDataSourceImpl(client: sl()));

  // Data sources - Local
  // - User
  sl.registerLazySingleton<UserLocalDataSource>(
    () => UserLocalDataSourceImpl(sharedPreferences: sl()),
  );

  // Core
  sl.registerLazySingleton<NetworkInfo>(() => NetworkInfoImpl(sl()));

  // External
  sl.registerLazySingleton(() => http.Client());
  final sharedPreferences = await SharedPreferences.getInstance();
  sl.registerLazySingleton(() => sharedPreferences);
  sl.registerLazySingleton(() => InternetConnectionChecker());
}
