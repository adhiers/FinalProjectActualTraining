class Guest {
  final int? guestId;
  final String guestName;
  final String email;
  final String phoneNumber;

  Guest({
    this.guestId,
    required this.guestName,
    required this.email,
    required this.phoneNumber,
  });

  factory Guest.fromJson(Map<String, dynamic> json) {
    return Guest(
      guestId: json['GuestId'],
      guestName: json['GuestName'],
      email: json['Email'],
      phoneNumber: json['PhoneNumber'],
    );
  }

  Map<String, dynamic> toJson() {
    return {
      'GuestId': guestId,
      'GuestName': guestName,
      'Email': email,
      'PhoneNumber': phoneNumber,
    };
  }
}
