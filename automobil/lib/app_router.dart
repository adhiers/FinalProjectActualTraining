// import 'package:flutter/material.dart';
import 'package:go_router/go_router.dart';

import 'features/splash/splash_page.dart';
import 'home/home_page.dart';
// import 'features/guest/guest_form_page.dart';
import 'status/success_page.dart';
// import 'features/schedule/schedule_page.dart';
import 'screens/car_list_screen.dart';
import 'screens/dealer_list_screen.dart';
import 'screens/dealer_car_list_screen.dart';
import 'screens/guest_form_screen.dart';
import 'screens/schedule_form_screen.dart';

final appRouter = GoRouter(
  initialLocation: '/',
  routes: [
    GoRoute(
      path: '/',
      name: 'splash',
      builder: (context, state) => const SplashPage(),
    ),
    GoRoute(
      path: '/home',
      name: 'home',
      builder: (context, state) => const HomePage(),
    ),
    GoRoute(
      path: '/guestform',
      name: 'guest',
      builder: (context, state) => const GuestFormScreen(),
    ),
    GoRoute(
      path: '/success',
      name: 'status',
      builder: (context, state) => const SuccessPage(),
    ),

    GoRoute(
      path: '/carcatalogue',
      name: 'catalogue',
      builder: (context, state) => CarListScreen(),
    ),
    GoRoute(
      path: '/dealers',
      name: 'dealers',
      builder: (context, state) => DealerListScreen(),
    ),
    GoRoute(
      path: '/dealercars',
      name: 'dealercars',
      builder: (context, state) => DealerCarListScreen(),
    ),
    GoRoute(
      path: '/scheduleform',
      name: 'scheduleform',
      builder: (context, state) => const ScheduleFormScreen(),
    ),
  ],
);
