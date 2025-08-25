import 'package:flutter/material.dart';
import '../models/dealer_car.dart';
import '../services/dealer_car_service.dart';
import '../config/api_config.dart';

class DealerCarProvider with ChangeNotifier {
  final DealerCarService _dealerCarService = DealerCarService(
    ApiConfig.baseUrl,
  );

  List<DealerCar> _dealerCars = [];
  List<DealerCar> _dealerCarsByDealer = [];
  List<DealerCar> _dealerCarsByCar = [];
  bool isLoading = false;
  DealerCar? _selectedDealerCar;

  List<DealerCar> get dealerCars => _dealerCars;
  List<DealerCar> get dealerCarsByDealer => _dealerCarsByDealer;
  List<DealerCar> get dealerCarsByCar => _dealerCarsByCar;
  DealerCar? get selectedDealerCar => _selectedDealerCar;

  Future<void> fetchDealerCars({Map<String, String>? headers}) async {
    isLoading = true;
    notifyListeners();
    try {
      debugPrint('Fetching dealer cars from API...');
      _dealerCars = await _dealerCarService.fetchDealerCars(headers: headers);
      debugPrint('Successfully fetched ${_dealerCars.length} dealer cars');
    } catch (e) {
      debugPrint('Error fetching dealer cars: $e');
      _dealerCars = [];
      throw Exception('Failed to load dealer cars: $e');
    }
    isLoading = false;
    notifyListeners();
  }

  Future<void> fetchDealerCarsByDealerId(
    String dealerId, {
    Map<String, String>? headers,
  }) async {
    isLoading = true;
    notifyListeners();
    try {
      _dealerCarsByDealer = await _dealerCarService.fetchDealerCarsByDealerId(
        dealerId,
        headers: headers,
      );
    } catch (e) {
      debugPrint('Error fetching dealer cars by dealer: $e');
      _dealerCarsByDealer = [];
    }
    isLoading = false;
    notifyListeners();
  }

  Future<void> fetchDealerCarsByCarId(
    String carId, {
    Map<String, String>? headers,
  }) async {
    isLoading = true;
    notifyListeners();
    try {
      _dealerCarsByCar = await _dealerCarService.fetchDealerCarsByCarId(
        carId,
        headers: headers,
      );
    } catch (e) {
      debugPrint('Error fetching dealer cars by car: $e');
      _dealerCarsByCar = [];
    }
    isLoading = false;
    notifyListeners();
  }

  Future<void> addDealerCar(
    DealerCar dealerCar, {
    Map<String, String>? headers,
  }) async {
    try {
      final newDealerCar = await _dealerCarService.createDealerCar(
        dealerCar,
        headers: headers,
      );
      _dealerCars.add(newDealerCar);

      // Update filtered lists if they contain related data
      if (_dealerCarsByDealer.isNotEmpty &&
          newDealerCar.dealerId == _dealerCarsByDealer.first.dealerId) {
        _dealerCarsByDealer.add(newDealerCar);
      }
      if (_dealerCarsByCar.isNotEmpty &&
          newDealerCar.carId == _dealerCarsByCar.first.carId) {
        _dealerCarsByCar.add(newDealerCar);
      }

      notifyListeners();
    } catch (e) {
      debugPrint('Error adding dealer car: $e');
      rethrow;
    }
  }

  Future<void> updateDealerCar(
    DealerCar dealerCar, {
    Map<String, String>? headers,
  }) async {
    try {
      final updatedDealerCar = await _dealerCarService.updateDealerCar(
        dealerCar,
        headers: headers,
      );

      // Update in main list
      final index = _dealerCars.indexWhere(
        (dc) => dc.dealerCarId == updatedDealerCar.dealerCarId,
      );
      if (index != -1) {
        _dealerCars[index] = updatedDealerCar;
      }

      // Update in dealer-specific list
      final dealerIndex = _dealerCarsByDealer.indexWhere(
        (dc) => dc.dealerCarId == updatedDealerCar.dealerCarId,
      );
      if (dealerIndex != -1) {
        _dealerCarsByDealer[dealerIndex] = updatedDealerCar;
      }

      // Update in car-specific list
      final carIndex = _dealerCarsByCar.indexWhere(
        (dc) => dc.dealerCarId == updatedDealerCar.dealerCarId,
      );
      if (carIndex != -1) {
        _dealerCarsByCar[carIndex] = updatedDealerCar;
      }

      notifyListeners();
    } catch (e) {
      debugPrint('Error updating dealer car: $e');
      rethrow;
    }
  }

  Future<void> deleteDealerCar(
    String dealerCarId, {
    Map<String, String>? headers,
  }) async {
    try {
      await _dealerCarService.deleteDealerCar(dealerCarId, headers: headers);

      _dealerCars.removeWhere(
        (dealerCar) => dealerCar.dealerCarId == dealerCarId,
      );
      _dealerCarsByDealer.removeWhere(
        (dealerCar) => dealerCar.dealerCarId == dealerCarId,
      );
      _dealerCarsByCar.removeWhere(
        (dealerCar) => dealerCar.dealerCarId == dealerCarId,
      );

      if (_selectedDealerCar?.dealerCarId == dealerCarId) {
        _selectedDealerCar = null;
      }

      notifyListeners();
    } catch (e) {
      debugPrint('Error deleting dealer car: $e');
      rethrow;
    }
  }

  Future<void> getDealerCarById(
    String dealerCarId, {
    Map<String, String>? headers,
  }) async {
    try {
      _selectedDealerCar = await _dealerCarService.getDealerCarById(
        dealerCarId,
        headers: headers,
      );
      notifyListeners();
    } catch (e) {
      debugPrint('Error getting dealer car by id: $e');
      _selectedDealerCar = null;
      rethrow;
    }
  }

  void setSelectedDealerCar(DealerCar? dealerCar) {
    _selectedDealerCar = dealerCar;
    notifyListeners();
  }

  void clearSelectedDealerCar() {
    _selectedDealerCar = null;
    notifyListeners();
  }

  void clearDealerCarsByDealer() {
    _dealerCarsByDealer = [];
    notifyListeners();
  }

  void clearDealerCarsByCar() {
    _dealerCarsByCar = [];
    notifyListeners();
  }
}
