class Car {
  final String carId;
  final String modelType;
  final String fuelType;
  final int basePrice;
  final int stock;

  Car({
    required this.carId,
    required this.modelType,
    required this.fuelType,
    required this.basePrice,
    required this.stock,
  });

  factory Car.fromJson(Map<String, dynamic> json) {
    return Car(
      carId: json['CarId'],
      modelType: json['ModelType'],
      fuelType: json['FuelType'],
      basePrice: json['BasePrice'],
      stock: json['Stock'],
    );
  }

  Map<String, dynamic> toJson() {
    return {
      'CarId': carId,
      'ModelType': modelType,
      'FuelType': fuelType,
      'BasePrice': basePrice,
      'Stock': stock,
    };
  }
}
