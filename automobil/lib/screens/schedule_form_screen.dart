import 'package:flutter/material.dart';
import 'package:go_router/go_router.dart';
import 'package:provider/provider.dart';
import '../models/schedule.dart';
import '../providers/schedule_provider.dart';
import '../providers/dealer_provider.dart';

class ScheduleFormScreen extends StatefulWidget {
  const ScheduleFormScreen({super.key});

  @override
  State<ScheduleFormScreen> createState() => _ScheduleFormScreenState();
}


class _ScheduleFormScreenState extends State<ScheduleFormScreen> {
  final _formKey = GlobalKey<FormState>();
  int? guestId;
  String? dealerId;
  String? program;
  final TextEditingController _guestIdController = TextEditingController();
  final TextEditingController _startDateController = TextEditingController();
  final TextEditingController _startTimeController = TextEditingController();
  final TextEditingController _endDateController = TextEditingController();
  final TextEditingController _endTimeController = TextEditingController();
  DateTime? availableStart;
  DateTime? availableEnd;

  @override
  void didChangeDependencies() {
    super.didChangeDependencies();
    final params = GoRouterState.of(context).uri.queryParameters;
    if (params['guestId'] != null && _guestIdController.text.isEmpty) {
      _guestIdController.text = params['guestId']!;
      guestId = int.tryParse(params['guestId']!);
    }
  }

  @override
  void dispose() {
    _guestIdController.dispose();
    _startDateController.dispose();
    _startTimeController.dispose();
    _endDateController.dispose();
    _endTimeController.dispose();
    super.dispose();
  }

  @override
  Widget build(BuildContext context) {
    final provider = Provider.of<ScheduleProvider>(context);
    final dealerProvider = Provider.of<DealerProvider>(context);
    // Auto-fetch dealers if not loaded
    if (dealerProvider.dealers.isEmpty && !dealerProvider.isLoading) {
      Future.microtask(() => dealerProvider.fetchDealers());
    }
    return Scaffold(
      appBar: AppBar(
        title: const Text('Register Schedule'),
        leading: IconButton(
          icon: const Icon(Icons.arrow_back),
          onPressed: () => context.go('/home'),
        ),
      ),
      body: Container(
        width: double.infinity,
        decoration: const BoxDecoration(
          gradient: LinearGradient(
            colors: [Color(0xFFe3f2fd), Color(0xFFbbdefb)],
            begin: Alignment.topLeft,
            end: Alignment.bottomRight,
          ),
        ),
        child: Center(
          child: SingleChildScrollView(
            child: Card(
              elevation: 8,
              shape: RoundedRectangleBorder(borderRadius: BorderRadius.circular(24)),
              margin: const EdgeInsets.symmetric(horizontal: 24, vertical: 32),
              child: Padding(
                padding: const EdgeInsets.all(24.0),
                child: Form(
                  key: _formKey,
                  child: Column(
                    mainAxisSize: MainAxisSize.min,
                    crossAxisAlignment: CrossAxisAlignment.stretch,
                    children: [
                      const Text(
                        'Schedule Registration',
                        style: TextStyle(fontSize: 24, fontWeight: FontWeight.bold),
                        textAlign: TextAlign.center,
                      ),
                      const SizedBox(height: 24),
                      TextFormField(
                        controller: _guestIdController,
                        decoration: const InputDecoration(
                          labelText: 'Guest ID',
                          prefixIcon: Icon(Icons.person),
                          border: OutlineInputBorder(),
                        ),
                        keyboardType: TextInputType.number,
                        validator: (value) => value == null || value.isEmpty ? 'Enter guest ID' : null,
                        onSaved: (value) => guestId = int.tryParse(value ?? ''),
                        readOnly: _guestIdController.text.isNotEmpty,
                      ),
                      const SizedBox(height: 16),
                      DropdownButtonFormField<String>(
                        value: dealerId,
                        decoration: const InputDecoration(
                          labelText: 'Dealer',
                          prefixIcon: Icon(Icons.store),
                          border: OutlineInputBorder(),
                        ),
                        items: dealerProvider.dealers
                            .map<DropdownMenuItem<String>>((dealer) {
                          return DropdownMenuItem<String>(
                            value: dealer.dealerId,
                            child: Text(dealer.dealerName),
                          );
                        }).toList(),
                        onChanged: (value) {
                          setState(() {
                            dealerId = value ?? '';
                          });
                        },
                        isExpanded: true,
                        hint: dealerProvider.isLoading
                            ? const Text('Loading dealers...')
                            : const Text('Select Dealer'),
                      ),
                      const SizedBox(height: 16),
                      DropdownButtonFormField<String>(
                        value: program,
                        decoration: const InputDecoration(
                          labelText: 'Program',
                          prefixIcon: Icon(Icons.event_note),
                          border: OutlineInputBorder(),
                        ),
                        items: const [
                          DropdownMenuItem(
                            value: 'Consultation',
                            child: Text('Consultation'),
                          ),
                          DropdownMenuItem(
                            value: 'Test Drive',
                            child: Text('Test Drive'),
                          ),
                        ],
                        onChanged: (value) {
                          setState(() {
                            program = value;
                          });
                        },
                        validator: (value) => value == null || value.isEmpty ? 'Select a program' : null,
                      ),
                      const SizedBox(height: 24),
                      const Text(
                        'Select available time for the program',
                        style: TextStyle(fontSize: 16, fontWeight: FontWeight.w600),
                        textAlign: TextAlign.left,
                      ),
                      const SizedBox(height: 12),
                      const SizedBox(height: 16),
                      Row(
                        children: [
                          Expanded(
                            child: TextFormField(
                              controller: _startDateController,
                              decoration: const InputDecoration(
                                labelText: 'Start Date',
                                prefixIcon: Icon(Icons.date_range),
                                border: OutlineInputBorder(),
                              ),
                              readOnly: true,
                              validator: (value) => value == null || value.isEmpty ? 'Enter start date' : null,
                              onTap: () async {
                                FocusScope.of(context).requestFocus(FocusNode());
                                final picked = await showDatePicker(
                                  context: context,
                                  initialDate: DateTime.now(),
                                  firstDate: DateTime.now(),
                                  lastDate: DateTime(2100),
                                );
                                if (picked != null) {
                                  _startDateController.text = picked.toIso8601String().split('T')[0];
                                  if (availableStart == null) availableStart = picked;
                                  else availableStart = DateTime(picked.year, picked.month, picked.day, availableStart!.hour, availableStart!.minute);
                                }
                              },
                              onSaved: (value) {},
                            ),
                          ),
                          const SizedBox(width: 8),
                          Expanded(
                            child: TextFormField(
                              controller: _startTimeController,
                              decoration: const InputDecoration(
                                labelText: 'Start Time',
                                prefixIcon: Icon(Icons.access_time),
                                border: OutlineInputBorder(),
                              ),
                              readOnly: true,
                              validator: (value) => value == null || value.isEmpty ? 'Enter start time' : null,
                              onTap: () async {
                                FocusScope.of(context).requestFocus(FocusNode());
                                final picked = await showTimePicker(
                                  context: context,
                                  initialTime: TimeOfDay.now(),
                                );
                                if (picked != null && _startDateController.text.isNotEmpty) {
                                  final dateParts = _startDateController.text.split('-');
                                  availableStart = DateTime(
                                    int.parse(dateParts[0]),
                                    int.parse(dateParts[1]),
                                    int.parse(dateParts[2]),
                                    picked.hour,
                                    picked.minute,
                                  );
                                  _startTimeController.text = picked.format(context);
                                }
                              },
                              onSaved: (value) {},
                            ),
                          ),
                        ],
                      ),
                      const SizedBox(height: 16),
                      // Available End
                      Row(
                        children: [
                          Expanded(
                            child: TextFormField(
                              controller: _endDateController,
                              decoration: const InputDecoration(
                                labelText: 'End Date',
                                prefixIcon: Icon(Icons.date_range),
                                border: OutlineInputBorder(),
                              ),
                              readOnly: true,
                              validator: (value) => value == null || value.isEmpty ? 'Enter end date' : null,
                              onTap: () async {
                                FocusScope.of(context).requestFocus(FocusNode());
                                final picked = await showDatePicker(
                                  context: context,
                                  initialDate: availableStart ?? DateTime.now(),
                                  firstDate: availableStart ?? DateTime.now(),
                                  lastDate: DateTime(2100),
                                );
                                if (picked != null) {
                                  _endDateController.text = picked.toIso8601String().split('T')[0];
                                  if (availableEnd == null) availableEnd = picked;
                                  else availableEnd = DateTime(picked.year, picked.month, picked.day, availableEnd!.hour, availableEnd!.minute);
                                }
                              },
                              onSaved: (value) {},
                            ),
                          ),
                          const SizedBox(width: 8),
                          Expanded(
                            child: TextFormField(
                              controller: _endTimeController,
                              decoration: const InputDecoration(
                                labelText: 'End Time',
                                prefixIcon: Icon(Icons.access_time),
                                border: OutlineInputBorder(),
                              ),
                              readOnly: true,
                              validator: (value) => value == null || value.isEmpty ? 'Enter end time' : null,
                              onTap: () async {
                                FocusScope.of(context).requestFocus(FocusNode());
                                final picked = await showTimePicker(
                                  context: context,
                                  initialTime: TimeOfDay.now(),
                                );
                                if (picked != null && _endDateController.text.isNotEmpty) {
                                  final dateParts = _endDateController.text.split('-');
                                  availableEnd = DateTime(
                                    int.parse(dateParts[0]),
                                    int.parse(dateParts[1]),
                                    int.parse(dateParts[2]),
                                    picked.hour,
                                    picked.minute,
                                  );
                                  _endTimeController.text = picked.format(context);
                                }
                              },
                              onSaved: (value) {},
                            ),
                          ),
                        ],
                      ),
                      const SizedBox(height: 28),
                      provider.isLoading
                          ? const Center(child: CircularProgressIndicator())
                          : ElevatedButton(
                              style: ElevatedButton.styleFrom(
                                backgroundColor: Color.fromARGB(255, 163, 38, 38),
                                foregroundColor: Colors.white,
                                padding: const EdgeInsets.symmetric(vertical: 18),
                                shape: RoundedRectangleBorder(borderRadius: BorderRadius.circular(16)),
                                textStyle: const TextStyle(fontSize: 18, fontWeight: FontWeight.bold),
                              ),
                              onPressed: () async {
                                if (_formKey.currentState!.validate()) {
                                  _formKey.currentState!.save();
                                  if (guestId != null && dealerId != null && program != null && availableStart != null && availableEnd != null) {
                                    final schedule = Schedule(
                                      guestId: guestId!,
                                      dealerId: dealerId!,
                                      program: program!,
                                      availableStart: availableStart!,
                                      availableEnd: availableEnd!,
                                    );
                                    final registeredSchedule = await provider.registerSchedule(schedule);
                                    if (registeredSchedule != null && registeredSchedule.scheduleId != null) {
                                      showDialog(
                                        context: context,
                                        barrierDismissible: false,
                                        builder: (context) => AlertDialog(
                                          title: const Text('Schedule Registered'),
                                          content: Text('Your Schedule ID is ${registeredSchedule.scheduleId}'),
                                          actions: [
                                            TextButton(
                                              onPressed: () {
                                                Navigator.of(context).pop();
                                                context.go('/home');
                                              },
                                              child: const Text('Back to Home'),
                                            ),
                                          ],
                                        ),
                                      );
                                    } else {
                                      ScaffoldMessenger.of(context).showSnackBar(
                                        SnackBar(content: Text(provider.errorMessage ?? 'Registration failed')),
                                      );
                                    }
                                  } else {
                                    ScaffoldMessenger.of(context).showSnackBar(
                                      const SnackBar(content: Text('Please fill all fields.')),
                                    );
                                  }
                                }
                              },
                              child: const Text('Register'),
                            ),
                    ],
                  ),
                ),
              ),
            ),
          ),
        ),
      ),
    );
  }
}
