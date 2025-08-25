import 'dart:convert';
import 'package:http/http.dart' as http;
import '../models/guest.dart';

class GuestService {
  final String baseUrl;
  GuestService(this.baseUrl);

  Future<List<Guest>> fetchGuests({Map<String, String>? headers}) async {
    try {
      final response = await http.get(
        Uri.parse('$baseUrl/guests'),
        headers: headers ?? {'Content-Type': 'application/json'},
      );
      if (response.statusCode == 200) {
        final List<dynamic> jsonResponse = json.decode(response.body);
        return jsonResponse.map((guest) => Guest.fromJson(guest)).toList();
      } else {
        throw Exception('Failed to load guests. Status: ${response.statusCode}, Body: ${response.body}');
      }
    } catch (e) {
      rethrow;
    }
  }

  Future<Guest?> registerGuest(Guest guest) async {
    final response = await http.post(
      Uri.parse('$baseUrl/guests'),
      headers: {'Content-Type': 'application/json'},
      body: jsonEncode(guest.toJson()),
    );
    if (response.statusCode == 201 || response.statusCode == 200) {
      final data = jsonDecode(response.body);
      return Guest.fromJson(data);
    }
    return null;
  }
}
