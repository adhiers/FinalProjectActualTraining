class DealerCar {
  final String dealerCarId;
  final String carId;
  final String dealerId;
  final int dealerCarPrice;

  DealerCar({
    required this.dealerCarId,
    required this.carId,
    required this.dealerId,
    required this.dealerCarPrice,
  });

  factory DealerCar.fromJson(Map<String, dynamic> json) {
    return DealerCar(
      dealerCarId: json['DealerCarId'],
      carId: json['CarId'],
      dealerId: json['DealerId'],
      dealerCarPrice: json['DealerCarPrice'],
    );
  }

  Map<String, dynamic> toJson() {
    return {
      'DealerCarId': dealerCarId,
      'CarId': carId,
      'DealerId': dealerId,
      'DealerCarPrice': dealerCarPrice,
    };
  }
}
