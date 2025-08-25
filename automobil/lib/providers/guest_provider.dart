import 'package:flutter/material.dart';
import '../models/guest.dart';
import '../services/guest_service.dart';
import '../config/api_config.dart';

class GuestProvider with ChangeNotifier {
  final GuestService guestService = GuestService(ApiConfig.baseUrl);
  List<Guest> _guests = [];
  bool isLoading = false;
  String? errorMessage;

  List<Guest> get guests => _guests;

  Future<void> fetchGuests({Map<String, String>? headers}) async {
    isLoading = true;
    notifyListeners();
    try {
      _guests = await guestService.fetchGuests(headers: headers);
    } catch (e) {
      errorMessage = e.toString();
      _guests = [];
    }
    isLoading = false;
    notifyListeners();
  }

  Future<Guest?> registerGuest(Guest guest) async {
    isLoading = true;
    errorMessage = null;
    notifyListeners();
    try {
      final result = await guestService.registerGuest(guest);
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
