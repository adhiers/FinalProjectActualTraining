import 'package:flutter/material.dart';
import '../models/schedule.dart';
import '../services/schedule_service.dart';
import '../config/api_config.dart';

class ScheduleProvider with ChangeNotifier {
  final ScheduleService scheduleService = ScheduleService(ApiConfig.baseUrl);
  List<Schedule> _schedules = [];
  bool isLoading = false;
  String? errorMessage;

  List<Schedule> get schedules => _schedules;

  Future<void> fetchSchedules({Map<String, String>? headers}) async {
    isLoading = true;
    notifyListeners();
    try {
      _schedules = await scheduleService.fetchSchedules(headers: headers);
    } catch (e) {
      errorMessage = e.toString();
      _schedules = [];
    }
    isLoading = false;
    notifyListeners();
  }

  Future<Schedule?> registerSchedule(Schedule schedule) async {
    isLoading = true;
    errorMessage = null;
    notifyListeners();
    try {
      final result = await scheduleService.registerSchedule(schedule);
      isLoading = false;
      notifyListeners();
      return result;
    } catch (e) {
      isLoading = false;
      errorMessage = e.toString();
      notifyListeners();
      return null;
    }
  }
}
