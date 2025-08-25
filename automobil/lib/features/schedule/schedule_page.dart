import 'package:flutter/material.dart';

class SchedulePage extends StatefulWidget {
  const SchedulePage({super.key});

  @override
  State<SchedulePage> createState() => _SchedulePageState();
}

class _SchedulePageState extends State<SchedulePage> {
  final _formKey = GlobalKey<FormState>();
  String dealer = '';
  String scheduleType = 'Konsultasi';
  DateTime? selectedDate;

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(title: const Text('Schedule Book')),
      body: Padding(
        padding: const EdgeInsets.all(16.0),
        child: Form(
          key: _formKey,
          child: Column(
            children: [
              TextFormField(
                decoration: const InputDecoration(labelText: 'Tempat Dealer'),
                onSaved: (val) => dealer = val ?? '',
                validator: (val) => val == null || val.isEmpty ? 'Wajib diisi' : null,
              ),
              DropdownButtonFormField<String>(
                value: scheduleType,
                items: const [
                  DropdownMenuItem(value: 'Konsultasi', child: Text('Konsultasi')),
                  DropdownMenuItem(value: 'Tes Drive', child: Text('Tes Drive')),
                ],
                onChanged: (val) => setState(() => scheduleType = val ?? 'Konsultasi'),
                decoration: const InputDecoration(labelText: 'Tipe Schedule'),
              ),
              const SizedBox(height: 16),
              Row(
                children: [
                  Expanded(
                    child: Text(selectedDate == null
                        ? 'Pilih Waktu'
                        : 'Waktu: ${selectedDate!.toLocal()}'.split(' ')[0]),
                  ),
                  ElevatedButton(
                    onPressed: () async {
                      final picked = await showDatePicker(
                        context: context,
                        initialDate: DateTime.now(),
                        firstDate: DateTime.now(),
                        lastDate: DateTime(2100),
                      );
                      if (picked != null) {
                        setState(() => selectedDate = picked);
                      }
                    },
                    child: const Text('Pilih Tanggal'),
                  ),
                ],
              ),
              const SizedBox(height: 24),
              ElevatedButton(
                onPressed: () {
                  if (_formKey.currentState!.validate() && selectedDate != null) {
                    _formKey.currentState!.save();
                    Navigator.pushReplacementNamed(context, '/success');
                  }
                },
                child: const Text('Submit'),
              ),
            ],
          ),
        ),
      ),
    );
  }
}