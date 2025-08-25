import 'dart:io';
import 'package:flutter/material.dart';
import 'package:provider/provider.dart';
import 'app_router.dart';
import 'providers/auth_provider.dart';
import 'providers/car_provider.dart';
import 'providers/dealer_provider.dart';
import 'providers/dealer_car_provider.dart';
import 'providers/guest_provider.dart';
import 'providers/schedule_provider.dart';

void main() {
  HttpOverrides.global = MyHttpOverrides();
  runApp(const AutoMobilApp());
}

class MyHttpOverrides extends HttpOverrides{
  @override
  HttpClient createHttpClient(SecurityContext? context){
    return super.createHttpClient(context)
      ..badCertificateCallback = (X509Certificate cert, String host, int port)=> true;
  }
}

class AutoMobilApp extends StatelessWidget {
  
  const AutoMobilApp({super.key});

  @override
  Widget build(BuildContext context) {
    return MultiProvider(
      providers: [
        ChangeNotifierProvider(create: (context) => AuthProvider()),
        ChangeNotifierProvider(create: (context) => CarProvider()),
        ChangeNotifierProvider(create: (context) => DealerProvider()),
        ChangeNotifierProvider(create: (context) => DealerCarProvider()),
        ChangeNotifierProvider(create: (context) => GuestProvider()),
        ChangeNotifierProvider(create: (context) => ScheduleProvider()),
      ],
      child: MaterialApp.router(
        title: 'Auto Mobil',
        routerConfig: appRouter,
        theme: ThemeData(
          colorScheme: ColorScheme.fromSeed(seedColor: const Color(0xFF1E88E5)),
          useMaterial3: true,
        ),
      ),
    );
  }
}
