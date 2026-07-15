import { Platform } from 'react-native';

export const API_BASE_URL =
  Platform.OS === 'android'
    ? 'http://10.0.2.2:5029/api'
    : 'http://localhost:5029/api';
