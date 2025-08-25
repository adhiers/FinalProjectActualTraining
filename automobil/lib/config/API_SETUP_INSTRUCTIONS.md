# API Configuration Instructions

## To connect to an API running on a different device:

### Step 1: Find the IP address of the device running your API

**On Windows (API device):**

```cmd
ipconfig
```

Look for "IPv4 Address" under your active network connection (usually something like 192.168.1.XXX or 192.168.0.XXX)

**On macOS/Linux (API device):**

```bash
ifconfig
```

or

```bash
ip addr show
```

### Step 2: Update the API configuration

1. Open `lib/config/api_config.dart`
2. Replace the IP address in the `baseUrl`:

```dart
// Change this line:
static const String baseUrl = 'https://192.168.0.101:7291/api';

// To your API device's IP address:
static const String baseUrl = 'https://192.168.0.XXX:7291/api';
```

### Step 3: Ensure network connectivity

1. **Same Network**: Both devices must be on the same WiFi network or LAN
2. **Firewall**: Make sure the API device's firewall allows incoming connections on port 7291
3. **API Server**: Ensure your API server is configured to accept connections from any IP (not just localhost)

### Step 4: Test the connection

1. From your Flutter device, try to access the API in a web browser:
   `https://192.168.0.XXX:7291/api/Cars`
2. If you get a certificate error, that's normal for local HTTPS - proceed anyway
3. You should see JSON data or at least a response from the server

### Common Issues:

- **Connection refused**: API server not running or wrong IP/port
- **Timeout**: Firewall blocking connection or devices on different networks
- **Certificate errors**: Normal for local HTTPS development
- **CORS errors**: API server needs to allow cross-origin requests

### Alternative: Use HTTP instead of HTTPS

If you're having HTTPS certificate issues, you can temporarily use HTTP:

```dart
static const String baseUrl = 'http://192.168.0.XXX:7291/api';
```

Make sure your API server supports HTTP connections.
