import 'package:flutter/material.dart';
import '../models/dealer.dart';
import '../services/dealer_service.dart';
import '../config/api_config.dart';

class DealerProvider with ChangeNotifier {
  final DealerService _dealerService = DealerService(ApiConfig.baseUrl);

  List<Dealer> _dealers = [];
  bool isLoading = false;
  Dealer? _selectedDealer;

  List<Dealer> get dealers => _dealers;
  Dealer? get selectedDealer => _selectedDealer;

  Future<void> fetchDealers({Map<String, String>? headers}) async {
    isLoading = true;
    notifyListeners();
    try {
      debugPrint('Fetching dealers from API...');
      _dealers = await _dealerService.fetchDealers(headers: headers);
      debugPrint('Successfully fetched ${_dealers.length} dealers');
    } catch (e) {
      debugPrint('Error fetching dealers: $e');
      _dealers = [];
      // Show error message to user
      throw Exception('Failed to load dealers: $e');
    }
    isLoading = false;
    notifyListeners();
  }

  Future<void> addDealer(Dealer dealer, {Map<String, String>? headers}) async {
    try {
      final newDealer = await _dealerService.createDealer(
        dealer,
        headers: headers,
      );
      _dealers.add(newDealer);
      notifyListeners();
    } catch (e) {
      debugPrint('Error adding dealer: $e');
      rethrow;
    }
  }

  Future<void> updateDealer(
    Dealer dealer, {
    Map<String, String>? headers,
  }) async {
    try {
      final updatedDealer = await _dealerService.updateDealer(
        dealer,
        headers: headers,
      );
      final index = _dealers.indexWhere(
        (d) => d.dealerId == updatedDealer.dealerId,
      );
      if (index != -1) {
        _dealers[index] = updatedDealer;
        notifyListeners();
      }
    } catch (e) {
      debugPrint('Error updating dealer: $e');
      rethrow;
    }
  }

  Future<void> deleteDealer(
    String dealerId, {
    Map<String, String>? headers,
  }) async {
    try {
      await _dealerService.deleteDealer(dealerId, headers: headers);
      _dealers.removeWhere((dealer) => dealer.dealerId == dealerId);
      if (_selectedDealer?.dealerId == dealerId) {
        _selectedDealer = null;
      }
      notifyListeners();
    } catch (e) {
      debugPrint('Error deleting dealer: $e');
      rethrow;
    }
  }

  Future<void> getDealerById(
    String dealerId, {
    Map<String, String>? headers,
  }) async {
    try {
      _selectedDealer = await _dealerService.getDealerById(
        dealerId,
        headers: headers,
      );
      notifyListeners();
    } catch (e) {
      debugPrint('Error getting dealer by id: $e');
      _selectedDealer = null;
      rethrow;
    }
  }

  void setSelectedDealer(Dealer? dealer) {
    _selectedDealer = dealer;
    notifyListeners();
  }

  void clearSelectedDealer() {
    _selectedDealer = null;
    notifyListeners();
  }
}
