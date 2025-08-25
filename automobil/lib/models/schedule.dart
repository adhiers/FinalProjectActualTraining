class Schedule {
  final int? scheduleId;
  final int guestId;
  final String dealerId;
  final String program;
  final DateTime availableStart;
  final DateTime availableEnd;

  Schedule({
    this.scheduleId,
    required this.guestId,
    required this.dealerId,
    required this.program,
    required this.availableStart,
    required this.availableEnd,
  });

  factory Schedule.fromJson(Map<String, dynamic> json) {
    return Schedule(
      scheduleId: json['ScheduleId'],
      guestId: json['GuestId'],
      dealerId: json['DealerId'],
      program: json['Program'],
      availableStart: DateTime.parse(json['AvailableStart']),
      availableEnd: DateTime.parse(json['AvailableEnd']),
    );
  }

  Map<String, dynamic> toJson() {
    return {
      'ScheduleId': scheduleId,
      'GuestId': guestId,
      'DealerId': dealerId,
      'Program': program,
      'AvailableStart': availableStart.toIso8601String(),
      'AvailableEnd': availableEnd.toIso8601String(),
    };
  }
}
