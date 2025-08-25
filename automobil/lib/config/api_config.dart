class ApiConfig {
  // Replace this IP address with the IP address of the device running your API
  // To find the IP address of your API device:
  // - Windows: Run 'ipconfig' in command prompt, look for IPv4 Address
  // - macOS/Linux: Run 'ifconfig' or 'ip addr' in terminal
  // - Make sure both devices are on the same network (WiFi/LAN)

  static const String baseUrl = 'https://172.17.143.42:7291/api';

  // Alternative configurations you can switch to:
  // For local development:
  // static const String baseUrl = 'https://localhost:7291/api';

  // For different device (replace with actual IP):
  // static const String baseUrl = 'https://192.168.0.XXX:7291/api';

  // For Azure cloud deployment:
  // static const String baseUrl = 'https://handsonlabapi20250807134636-gjfahzdkfug4b9cs.indonesiacentral-01.azurewebsites.net/api';

  // Common headers
  static const Map<String, String> defaultHeaders = {
    'Content-Type': 'application/json',
  };
}
