import 'dart:convert';
import 'package:http/http.dart' as http;
import '../models/dealer.dart';

class DealerService {
  final String baseUrl;

  DealerService(this.baseUrl);

  Future<List<Dealer>> fetchDealers({Map<String, String>? headers}) async {
    try {
      final response = await http
          .get(
            Uri.parse('$baseUrl/Dealers'),
            headers: headers ?? {'Content-Type': 'application/json'},
          )
          .timeout(Duration(seconds: 10)); // Add timeout

      print('Dealer API Response Status: ${response.statusCode}');
      print('Dealer API Response Body: ${response.body}');

      if (response.statusCode == 200) {
        final List<dynamic> jsonResponse = json.decode(response.body);
        return jsonResponse.map((dealer) => Dealer.fromJson(dealer)).toList();
      } else {
        throw Exception(
          'Failed to load dealers. Status: ${response.statusCode}, Body: ${response.body}',
        );
      }
    } catch (e) {
      print('Dealer Service Error: $e');

      // Check if it's a connection issue
      if (e.toString().contains('Failed to fetch') ||
          e.toString().contains('ClientException') ||
          e.toString().contains('TimeoutException')) {
        // Return mock data for demo purposes
        print('API server not available, returning mock data');
        return _getMockDealers();
      }

      rethrow;
    }
  }

  // Mock data for when API is not available
  List<Dealer> _getMockDealers() {
    return [
      Dealer(
        dealerId: 'dealer-1',
        dealerName: 'Downtown Auto Sales',
        dealerAddress: '123 Main Street',
        city: 'Jakarta',
        province: 'DKI Jakarta',
        taxRate: 10,
      ),
      Dealer(
        dealerId: 'dealer-2',
        dealerName: 'Premium Car Center',
        dealerAddress: '456 Business District',
        city: 'Surabaya',
        province: 'East Java',
        taxRate: 11,
      ),
      Dealer(
        dealerId: 'dealer-3',
        dealerName: 'Metro Motors',
        dealerAddress: '789 Shopping Mall',
        city: 'Bandung',
        province: 'West Java',
        taxRate: 10,
      ),
    ];
  }

  Future<Dealer> createDealer(
    Dealer dealer, {
    Map<String, String>? headers,
  }) async {
    final response = await http.post(
      Uri.parse('$baseUrl/Dealers'),
      headers: headers ?? {'Content-Type': 'application/json'},
      body: json.encode(dealer.toJson()),
    );

    if (response.statusCode == 201) {
      return Dealer.fromJson(json.decode(response.body));
    } else {
      throw Exception('Failed to create dealer');
    }
  }

  Future<Dealer> updateDealer(
    Dealer dealer, {
    Map<String, String>? headers,
  }) async {
    final response = await http.put(
      Uri.parse('$baseUrl/Dealers/${dealer.dealerId}'),
      headers: headers ?? {'Content-Type': 'application/json'},
      body: json.encode(dealer.toJson()),
    );

    if (response.statusCode == 200) {
      return Dealer.fromJson(json.decode(response.body));
    } else {
      throw Exception('Failed to update dealer');
    }
  }

  Future<void> deleteDealer(String id, {Map<String, String>? headers}) async {
    final response = await http.delete(
      Uri.parse('$baseUrl/Dealers/$id'),
      headers: headers ?? {'Content-Type': 'application/json'},
    );

    if (response.statusCode == 200) {
      // Dealer deleted successfully
      return;
    } else {
      throw Exception('Failed to delete dealer');
    }
  }

  Future<Dealer> getDealerById(
    String id, {
    Map<String, String>? headers,
  }) async {
    final response = await http.get(
      Uri.parse('$baseUrl/Dealers/$id'),
      headers: headers ?? {'Content-Type': 'application/json'},
    );

    if (response.statusCode == 200) {
      return Dealer.fromJson(json.decode(response.body));
    } else {
      throw Exception('Failed to load dealer');
    }
  }
}
