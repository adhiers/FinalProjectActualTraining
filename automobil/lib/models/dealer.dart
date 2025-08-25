class Dealer {
  final String dealerId;
  final String dealerName;
  final String dealerAddress;
  final String city;
  final String province;
  final int taxRate;

  Dealer({
    required this.dealerId,
    required this.dealerName,
    required this.dealerAddress,
    required this.city,
    required this.province,
    required this.taxRate,
  });

  factory Dealer.fromJson(Map<String, dynamic> json) {
    return Dealer(
      dealerId: json['DealerId'],
      dealerName: json['DealerName'],
      dealerAddress: json['DealerAddress'],
      city: json['City'],
      province: json['Province'],
      taxRate: json['TaxRate'],
    );
  }

  Map<String, dynamic> toJson() {
    return {
      'DealerId': dealerId,
      'DealerName': dealerName,
      'DealerAddress': dealerAddress,
      'City': city,
      'Province': province,
      'TaxRate': taxRate,
    };
  }
}
