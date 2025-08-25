import 'package:flutter/material.dart';

class CarCatalogue extends StatelessWidget {
  const CarCatalogue({super.key});

  final List<Map<String, dynamic>> cars = const [
    {
      'car': 'XPander',
      'model': 'SUV',
      'fuel': 'Bensin',
      'stock': 5,
      'basePrice': 250000000,
    },
    {
      'car': 'Destinator',
      'model': 'SUV',
      'fuel': 'Bensin',
      'stock': 3,
      'basePrice': 180000000,
    },
    {
      'car': ' Pajero Sport',
      'model': 'SUV',
      'fuel': 'Diesel',
      'stock': 2,
      'basePrice': 550000000,
    },
  ];

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(title: const Text('Car Catalogue')),
      body: ListView.builder(
        itemCount: cars.length,
        itemBuilder: (context, index) {
          final car = cars[index];
          return Card(
            margin: const EdgeInsets.symmetric(horizontal: 16, vertical: 8),
            child: ListTile(
              title: Text(car['car']),
              subtitle: Text('Model: ${car['model']}\nFuel: ${car['fuel']}\nStock: ${car['stock']}'),
              trailing: Text('Rp${car['basePrice']}'),
            ),
          );
        },
      ),
      // floatingActionButton: FloatingActionButton(
      //   onPressed: () => Navigator.pop(context),
      //   child: const Icon(Icons.home),
      //   tooltip: 'Back to Home',
      // ),
    );
  }
}