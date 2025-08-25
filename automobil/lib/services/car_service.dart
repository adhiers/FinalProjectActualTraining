import 'dart:convert';
import 'package:http/http.dart' as http;
import '../models/car.dart';

class CarService {
  final String baseUrl;

  CarService(this.baseUrl);

  Future<List<Car>> fetchCars({Map<String, String>? headers}) async {
    try {
      final response = await http
          .get(
            Uri.parse('$baseUrl/Cars'),
            headers: headers ?? {'Content-Type': 'application/json'},
          )
          .timeout(Duration(seconds: 10)); // Add timeout

      print('Car API Response Status: ${response.statusCode}');
      print('Car API Response Body: ${response.body}');

      if (response.statusCode == 200) {
        final List<dynamic> jsonResponse = json.decode(response.body);
        return jsonResponse.map((car) => Car.fromJson(car)).toList();
      } else {
        throw Exception(
          'Failed to load cars. Status: ${response.statusCode}, Body: ${response.body}',
        );
      }
    } catch (e) {
      print('Car Service Error: $e');

      // Check if it's a connection issue
      if (e.toString().contains('Failed to fetch') ||
          e.toString().contains('ClientException') ||
          e.toString().contains('TimeoutException')) {
        // Return mock data for demo purposes
        print('API server not available, returning mock data');
        return _getMockCars();
      }

      rethrow;
    }
  }

  // Mock data for when API is not available
  List<Car> _getMockCars() {
    return [
      Car(
        carId: 'mock-1',
        modelType: 'Toyota Camry',
        fuelType: 'Gasoline',
        basePrice: 25000,
        stock: 10,
      ),
      Car(
        carId: 'mock-2',
        modelType: 'Honda Civic',
        fuelType: 'Gasoline',
        basePrice: 22000,
        stock: 8,
      ),
      Car(
        carId: 'mock-3',
        modelType: 'Tesla Model 3',
        fuelType: 'Electric',
        basePrice: 35000,
        stock: 5,
      ),
      Car(
        carId: 'mock-4',
        modelType: 'BMW X5',
        fuelType: 'Gasoline',
        basePrice: 55000,
        stock: 3,
      ),
    ];
  }

  Future<Car> createCar(Car car, {Map<String, String>? headers}) async {
    final response = await http.post(
      Uri.parse('$baseUrl/Cars'),
      headers: headers ?? {'Content-Type': 'application/json'},
      body: json.encode(car.toJson()),
    );

    if (response.statusCode == 201) {
      return Car.fromJson(json.decode(response.body));
    } else {
      throw Exception('Failed to create car');
    }
  }

  Future<Car> updateCar(Car car, {Map<String, String>? headers}) async {
    final response = await http.put(
      Uri.parse('$baseUrl/Cars/${car.carId}'),
      headers: headers ?? {'Content-Type': 'application/json'},
      body: json.encode(car.toJson()),
    );

    if (response.statusCode == 200) {
      return Car.fromJson(json.decode(response.body));
    } else {
      throw Exception('Failed to update car');
    }
  }

  Future<void> deleteCar(String id, {Map<String, String>? headers}) async {
    final response = await http.delete(
      Uri.parse('$baseUrl/Cars/$id'),
      headers: headers ?? {'Content-Type': 'application/json'},
    );

    if (response.statusCode == 200) {
      // Car deleted successfully
      return;
    } else {
      throw Exception('Failed to delete car');
    }
  }
}
