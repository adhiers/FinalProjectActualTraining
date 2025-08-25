import 'package:go_router/go_router.dart';
import 'package:flutter/material.dart';
import 'package:provider/provider.dart';
import '../providers/car_provider.dart';
import '../providers/auth_provider.dart';

class CarListScreen extends StatefulWidget {
  const CarListScreen({super.key});

  @override
  _CarListScreenState createState() => _CarListScreenState();
}

class _CarListScreenState extends State<CarListScreen> {
  String _selectedPage = 'car';
  String? errorMessage;

  String formatRupiah(int amount) {
    // Simple formatter for Indonesian Rupiah
    return 'Rp ' + amount.toString().replaceAllMapped(
      RegExp(r'\B(?=(\d{3})+(?!\d))'),
      (match) => '.',
    );
  }

  @override
  void initState() {
    super.initState();
    WidgetsBinding.instance.addPostFrameCallback((_) {
      if (mounted) {
        _fetchData();
      }
    });
  }

  Future<void> _fetchData() async {
    try {
      final authProvider = Provider.of<AuthProvider>(context, listen: false);
      final carProvider = Provider.of<CarProvider>(context, listen: false);
      await carProvider.fetchCars(headers: authProvider.getAuthHeaders());
      if (mounted) {
        setState(() {
          errorMessage = null;
        });
      }
    } catch (e) {
      if (mounted) {
        setState(() {
          errorMessage = e.toString();
        });
      }
    }
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: DropdownButtonHideUnderline(
          child: DropdownButton<String>(
            value: _selectedPage,
            icon: const Icon(Icons.arrow_drop_down, color: Colors.white),
            dropdownColor: Colors.white,
            style: const TextStyle(color: Colors.black, fontWeight: FontWeight.bold, fontSize: 20),
            items: const [
              DropdownMenuItem(
                value: 'car',
                child: Row(
                  children: [Icon(Icons.directions_car, color: Colors.blue), SizedBox(width: 8), Text('Car')],
                ),
              ),
              DropdownMenuItem(
                value: 'dealers',
                child: Row(
                  children: [Icon(Icons.store, color: Colors.blue), SizedBox(width: 8), Text('Dealers')],
                ),
              ),
              DropdownMenuItem(
                value: 'dealerCars',
                child: Row(
                  children: [Icon(Icons.car_rental, color: Colors.blue), SizedBox(width: 8), Text('Dealer Cars')],
                ),
              ),
            ],
            onChanged: (value) {
              if (value == null || value == _selectedPage) return;
              setState(() => _selectedPage = value);
              switch (value) {
                case 'dealers':
                  context.go('/dealers');
                  break;
                case 'dealerCars':
                  context.go('/dealercars');
                  break;
                case 'car':
                  // Already here
                  break;
              }
            },
          ),
        ),
        centerTitle: true,
        leading: IconButton(
          icon: const Icon(Icons.arrow_back),
          onPressed: () => context.go('/home'),
        ),
      ),
      body: Consumer<CarProvider>(
        builder: (context, provider, child) {
          if (provider.isLoading) {
            return Center(child: CircularProgressIndicator());
          }

          if (errorMessage != null) {
            return Center(
              child: Column(
                mainAxisAlignment: MainAxisAlignment.center,
                children: [
                  Icon(Icons.error_outline, size: 64, color: Colors.red),
                  SizedBox(height: 16),
                  Text(
                    'Error loading cars',
                    style: TextStyle(fontSize: 18, fontWeight: FontWeight.bold),
                  ),
                  SizedBox(height: 8),
                  Padding(
                    padding: EdgeInsets.symmetric(horizontal: 32),
                    child: Text(
                      errorMessage!,
                      textAlign: TextAlign.center,
                      style: TextStyle(color: Colors.grey[600]),
                    ),
                  ),
                  SizedBox(height: 16),
                  ElevatedButton(onPressed: _fetchData, child: Text('Retry')),
                ],
              ),
            );
          }

          if (provider.cars.isEmpty) {
            return Center(
              child: Column(
                mainAxisAlignment: MainAxisAlignment.center,
                children: [
                  Icon(Icons.directions_car, size: 64, color: Colors.grey),
                  SizedBox(height: 16),
                  Text('No cars found.'),
                  SizedBox(height: 16),
                  ElevatedButton(onPressed: _fetchData, child: Text('Refresh')),
                ],
              ),
            );
          }

          return ListView.builder(
            itemCount: provider.cars.length,
            itemBuilder: (context, index) {
              final car = provider.cars[index];
              return Card(
                elevation: 4,
                shape: RoundedRectangleBorder(borderRadius: BorderRadius.circular(16)),
                margin: const EdgeInsets.symmetric(horizontal: 16, vertical: 10),
                child: Padding(
                  padding: const EdgeInsets.symmetric(vertical: 12, horizontal: 8),
                  child: ListTile(
                    leading: CircleAvatar(
                      backgroundColor: Colors.blue[50],
                      child: Icon(Icons.directions_car, color: Colors.blue[700]),
                    ),
                    title: Text(
                      car.modelType,
                      style: const TextStyle(fontWeight: FontWeight.bold, fontSize: 18),
                    ),
                    subtitle: Padding(
                      padding: const EdgeInsets.only(top: 4.0),
                      child: Column(
                        crossAxisAlignment: CrossAxisAlignment.start,
                        children: [
                          Row(
                            children: [
                              Icon(Icons.local_gas_station, size: 16, color: Colors.grey[600]),
                              SizedBox(width: 4),
                              Text(car.fuelType, style: TextStyle(color: Colors.grey[700])),
                            ],
                          ),
                          SizedBox(height: 6),
                          Row(
                            children: [
                              Text(
                                formatRupiah(car.basePrice),
                                style: TextStyle(fontWeight: FontWeight.bold, color: Colors.green[700], fontSize: 16),
                              ),
                            ],
                          ),
                        ],
                      ),
                    ),
                    trailing: Container(
                      padding: const EdgeInsets.symmetric(horizontal: 10, vertical: 4),
                      decoration: BoxDecoration(
                        color: Colors.blue[50],
                        borderRadius: BorderRadius.circular(8),
                      ),
                      child: Text('Stock: ${car.stock}', style: TextStyle(color: Colors.blue[900], fontWeight: FontWeight.w600)),
                    ),
                  ),
                ),
              );
            },
          );
        },
      ),
    );
  }
}
