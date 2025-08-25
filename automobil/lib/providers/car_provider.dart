import 'package:flutter/material.dart';
import '../models/car.dart';
import '../services/car_service.dart';
import '../config/api_config.dart';

class CarProvider with ChangeNotifier {
  final CarService _carsService = CarService(ApiConfig.baseUrl);

  List<Car> _cars = [];
  bool isLoading = false;

  List<Car> get cars => _cars;

  Future<void> fetchCars({Map<String, String>? headers}) async {
    isLoading = true;
    notifyListeners();
    try {
      debugPrint('Fetching cars from API...');
      _cars = await _carsService.fetchCars(headers: headers);
      debugPrint('Successfully fetched ${_cars.length} cars');
    } catch (e) {
      debugPrint('Error fetching cars: $e');

      // Check if it's a connection issue and service might have provided mock data
      if (e.toString().contains('Failed to fetch') ||
          e.toString().contains('ClientException')) {
        debugPrint('Using fallback/mock data');
        // Don't throw error for connection issues, as service provides mock data
      } else {
        _cars = [];
        throw Exception('Failed to load cars: $e');
      }
    }
    isLoading = false;
    notifyListeners();
  }

  Future<void> addCar(Car car, {Map<String, String>? headers}) async {
    try {
      final newCar = await _carsService.createCar(car, headers: headers);
      _cars.add(newCar);
      notifyListeners();
    } catch (e) {
      debugPrint('Error adding car: $e');
      rethrow;
    }
  }

  Future<void> updateCar(Car car, {Map<String, String>? headers}) async {
    try {
      final updatedCar = await _carsService.updateCar(car, headers: headers);
      final index = _cars.indexWhere((c) => c.carId == updatedCar.carId);
      if (index != -1) {
        _cars[index] = updatedCar;
        notifyListeners();
      }
    } catch (e) {
      debugPrint('Error updating car: $e');
      rethrow;
    }
  }

  Future<void> deleteCar(String carId, {Map<String, String>? headers}) async {
    try {
      await _carsService.deleteCar(carId, headers: headers);
      _cars.removeWhere((car) => car.carId == carId);
      notifyListeners();
    } catch (e) {
      debugPrint('Error deleting car: $e');
      rethrow;
    }
  }
}
