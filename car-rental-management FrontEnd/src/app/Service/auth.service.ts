import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private apiUrl = 'https://localhost:7122/api/Authentication';

  constructor(private http: HttpClient, private router: Router) { }

  // Register method using Customer interface
  register(customer: Customer) {
    return this.http.post(`${this.apiUrl}/register`, customer);
  }

  // Login method using User interface
  login(user: User) {
    return this.http.post(`${this.apiUrl}/login`, user);
  }

  // Save token to localStorage
  saveToken(token: string) {
    localStorage.setItem('authToken', token);
  }

  // Retrieve token from localStorage
  getToken() {
    return localStorage.getItem('authToken');
  }

  // Logout method to clear session
  logout() {
    localStorage.removeItem('authToken');
    localStorage.removeItem('currentUser'); // Clear user details
    this.router.navigate(['/login']);
  }

  // Retrieve user details from localStorage
  getUserDetails() {
    const user = localStorage.getItem('currentUser');
    return user ? JSON.parse(user) : null;
  }

  // Get user role from the details stored in localStorage
  getUserRole() {
    const user = this.getUserDetails();
    return user ? user.role : null;
  }

  // Check if the token is expired
  isTokenExpired(): boolean {
    const expiryTime = parseInt(localStorage.getItem('tokenExpiry') || '0', 10);
    return new Date().getTime() > expiryTime;
  }
}

// Customer interface for registration
export interface Customer {
  nicNumber: string;
  firstName: string;
  lastName: string;
  email: string;
  passwordHash: string;
  phoneNumber: string;
  address: string;
  username: string;
}

// User interface for login
export interface User {
  username: string;
  password: string;
}
