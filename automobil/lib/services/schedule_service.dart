import 'dart:convert';
import 'package:http/http.dart' as http;
import '../models/schedule.dart';

class ScheduleService {
  final String baseUrl;
  ScheduleService(this.baseUrl);

  Future<List<Schedule>> fetchSchedules({Map<String, String>? headers}) async {
    final response = await http.get(
      Uri.parse('$baseUrl/schedulings'),
      headers: headers ?? {'Content-Type': 'application/json'},
    );
    if (response.statusCode == 200) {
      final List<dynamic> jsonResponse = json.decode(response.body);
      return jsonResponse.map((s) => Schedule.fromJson(s)).toList();
    } else {
      throw Exception('Failed to load schedules');
    }
  }

  Future<Schedule?> registerSchedule(Schedule schedule) async {
    final response = await http.post(
      Uri.parse('$baseUrl/schedulings'),
      headers: {'Content-Type': 'application/json'},
      body: jsonEncode(schedule.toJson()),
    );
    if (response.statusCode == 201 || response.statusCode == 200) {
      final data = jsonDecode(response.body);
      return Schedule.fromJson(data);
    }
    return null;
  }
}
