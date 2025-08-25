import 'package:go_router/go_router.dart';
import 'package:flutter/material.dart';
import 'package:provider/provider.dart';
import '../providers/car_provider.dart';
import '../providers/auth_provider.dart';
import '../models/car.dart';

class CarFormScreen extends StatefulWidget {
  final Car? car;

  CarFormScreen({this.car});

  @override
  _CarFormScreenState createState() => _CarFormScreenState();
}

class _CarFormScreenState extends State<CarFormScreen> {
  final _formKey = GlobalKey<FormState>();
  late String _carId;
  late String _modelType;
  late String _fuelType;
  late int _basePrice;
  late int _stock;

  @override
  void initState() {
    super.initState();
    _carId = widget.car?.carId ?? '';
    _modelType = widget.car?.modelType ?? '';
    _fuelType = widget.car?.fuelType ?? '';
    _basePrice = widget.car?.basePrice ?? 0;
    _stock = widget.car?.stock ?? 0;
  }

  @override
  Widget build(BuildContext context) {
    final provider = Provider.of<CarProvider>(context, listen: false);
    final authProvider = Provider.of<AuthProvider>(context, listen: false);

    return Scaffold(
      appBar: AppBar(
        title: Text(widget.car == null ? 'Add Car' : 'Edit Car'),
        leading: IconButton(
          icon: const Icon(Icons.arrow_back),
          onPressed: () => context.pop(),
        ),
      ),
      body: Padding(
        padding: EdgeInsets.all(16.0),
        child: Form(
          key: _formKey,
          child: Column(
            children: [
              TextFormField(
                initialValue: _carId,
                decoration: InputDecoration(labelText: 'Car ID'),
                validator: (value) =>
                    value!.isEmpty ? 'Please enter the car ID' : null,
              ),
              TextFormField(
                initialValue: _modelType,
                decoration: InputDecoration(labelText: 'Model'),
                validator: (value) =>
                    value!.isEmpty ? 'Please enter the model' : null,
                onSaved: (value) => _modelType = value!,
              ),
              TextFormField(
                initialValue: _fuelType,
                decoration: InputDecoration(labelText: 'Fuel Type'),
                validator: (value) =>
                    value!.isEmpty ? 'Please enter the fuel type' : null,
                onSaved: (value) => _fuelType = value!,
              ),
              TextFormField(
                initialValue: _basePrice.toString(),
                decoration: InputDecoration(labelText: 'Base Price'),
                validator: (value) =>
                    value!.isEmpty ? 'Please enter the base price' : null,
                onSaved: (value) => _basePrice = int.parse(value!),
              ),
              TextFormField(
                initialValue: _stock.toString(),
                decoration: InputDecoration(labelText: 'Stock'),
                validator: (value) =>
                    value!.isEmpty ? 'Please enter the stock' : null,
                onSaved: (value) => _stock = int.parse(value!),
              ),
              SizedBox(height: 20),
              ElevatedButton(
                child: Text('Save'),
                onPressed: () async {
                  if (_formKey.currentState!.validate()) {
                    _formKey.currentState!.save();

                    try {
                      if (widget.car == null) {
                        await provider.addCar(
                          Car(
                            carId: _carId,
                            modelType: _modelType,
                            fuelType: _fuelType,
                            basePrice: _basePrice,
                            stock: _stock,
                          ),
                          headers: authProvider.getAuthHeaders(),
                        );

                        if (mounted) {
                          ScaffoldMessenger.of(context).showSnackBar(
                            SnackBar(
                              content: Text('Car added successfully'),
                              backgroundColor: Colors.green,
                              duration: Duration(seconds: 2),
                            ),
                          );
                        }
                      } else {
                        await provider.updateCar(
                          Car(
                            carId: _carId,
                            modelType: _modelType,
                            fuelType: _fuelType,
                            basePrice: _basePrice,
                            stock: _stock,
                          ),
                          headers: authProvider.getAuthHeaders(),
                        );

                        if (mounted) {
                          ScaffoldMessenger.of(context).showSnackBar(
                            SnackBar(
                              content: Text('Car updated successfully'),
                              backgroundColor: Colors.green,
                              duration: Duration(seconds: 2),
                            ),
                          );
                        }
                      }

                      if (mounted) {
                        Navigator.pop(context);
                      }
                    } catch (e) {
                      if (mounted) {
                        ScaffoldMessenger.of(context).showSnackBar(
                          SnackBar(
                            content: Text('Error: $e'),
                            backgroundColor: Colors.red,
                            duration: Duration(seconds: 3),
                          ),
                        );
                      }
                    }
                  }
                },
              ),
            ],
          ),
        ),
      ),
    );
  }
}
