import 'dart:convert';
import 'package:http/http.dart' as http;
import '../models/dealer_car.dart';

class DealerCarService {
  final String baseUrl;

  DealerCarService(this.baseUrl);

  Future<List<DealerCar>> fetchDealerCars({
    Map<String, String>? headers,
  }) async {
    try {
      final response = await http
          .get(
            Uri.parse('$baseUrl/DealerCars'),
            headers: headers ?? {'Content-Type': 'application/json'},
          )
          .timeout(Duration(seconds: 10)); // Add timeout

      print('DealerCar API Response Status: ${response.statusCode}');
      print('DealerCar API Response Body: ${response.body}');

      if (response.statusCode == 200) {
        final List<dynamic> jsonResponse = json.decode(response.body);
        return jsonResponse
            .map((dealerCar) => DealerCar.fromJson(dealerCar))
            .toList();
      } else {
        throw Exception(
          'Failed to load dealer cars. Status: ${response.statusCode}, Body: ${response.body}',
        );
      }
    } catch (e) {
      print('DealerCar Service Error: $e');

      // Check if it's a connection issue
      if (e.toString().contains('Failed to fetch') ||
          e.toString().contains('ClientException') ||
          e.toString().contains('TimeoutException')) {
        // Return mock data for demo purposes
        print('API server not available, returning mock data');
        return _getMockDealerCars();
      }

      rethrow;
    }
  }

  // Mock data for when API is not available
  List<DealerCar> _getMockDealerCars() {
    return [
      DealerCar(
        dealerCarId: 'dc-1',
        carId: 'mock-1',
        dealerId: 'dealer-1',
        dealerCarPrice: 26000,
      ),
      DealerCar(
        dealerCarId: 'dc-2',
        carId: 'mock-1',
        dealerId: 'dealer-2',
        dealerCarPrice: 25500,
      ),
      DealerCar(
        dealerCarId: 'dc-3',
        carId: 'mock-2',
        dealerId: 'dealer-1',
        dealerCarPrice: 23000,
      ),
      DealerCar(
        dealerCarId: 'dc-4',
        carId: 'mock-3',
        dealerId: 'dealer-3',
        dealerCarPrice: 36000,
      ),
    ];
  }

  Future<List<DealerCar>> fetchDealerCarsByDealerId(
    String dealerId, {
    Map<String, String>? headers,
  }) async {
    final response = await http.get(
      Uri.parse('$baseUrl/DealerCars/dealer/$dealerId'),
      headers: headers ?? {'Content-Type': 'application/json'},
    );

    if (response.statusCode == 200) {
      final List<dynamic> jsonResponse = json.decode(response.body);
      return jsonResponse
          .map((dealerCar) => DealerCar.fromJson(dealerCar))
          .toList();
    } else {
      throw Exception('Failed to load dealer cars for dealer $dealerId');
    }
  }

  Future<List<DealerCar>> fetchDealerCarsByCarId(
    String carId, {
    Map<String, String>? headers,
  }) async {
    final response = await http.get(
      Uri.parse('$baseUrl/DealerCars/car/$carId'),
      headers: headers ?? {'Content-Type': 'application/json'},
    );

    if (response.statusCode == 200) {
      final List<dynamic> jsonResponse = json.decode(response.body);
      return jsonResponse
          .map((dealerCar) => DealerCar.fromJson(dealerCar))
          .toList();
    } else {
      throw Exception('Failed to load dealer cars for car $carId');
    }
  }

  Future<DealerCar> createDealerCar(
    DealerCar dealerCar, {
    Map<String, String>? headers,
  }) async {
    final response = await http.post(
      Uri.parse('$baseUrl/DealerCars'),
      headers: headers ?? {'Content-Type': 'application/json'},
      body: json.encode(dealerCar.toJson()),
    );

    if (response.statusCode == 201) {
      return DealerCar.fromJson(json.decode(response.body));
    } else {
      throw Exception('Failed to create dealer car');
    }
  }

  Future<DealerCar> updateDealerCar(
    DealerCar dealerCar, {
    Map<String, String>? headers,
  }) async {
    final response = await http.put(
      Uri.parse('$baseUrl/DealerCars/${dealerCar.dealerCarId}'),
      headers: headers ?? {'Content-Type': 'application/json'},
      body: json.encode(dealerCar.toJson()),
    );

    if (response.statusCode == 200) {
      return DealerCar.fromJson(json.decode(response.body));
    } else {
      throw Exception('Failed to update dealer car');
    }
  }

  Future<void> deleteDealerCar(
    String id, {
    Map<String, String>? headers,
  }) async {
    final response = await http.delete(
      Uri.parse('$baseUrl/DealerCars/$id'),
      headers: headers ?? {'Content-Type': 'application/json'},
    );

    if (response.statusCode == 200) {
      // Dealer car deleted successfully
      return;
    } else {
      throw Exception('Failed to delete dealer car');
    }
  }

  Future<DealerCar> getDealerCarById(
    String id, {
    Map<String, String>? headers,
  }) async {
    final response = await http.get(
      Uri.parse('$baseUrl/DealerCars/$id'),
      headers: headers ?? {'Content-Type': 'application/json'},
    );

    if (response.statusCode == 200) {
      return DealerCar.fromJson(json.decode(response.body));
    } else {
      throw Exception('Failed to load dealer car');
    }
  }
}
