import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private apiUrl='https://localhost:7122/api/Authentication';

  constructor(private http: HttpClient, private router: Router) { }

  // Register method using Customer interface
  register(customer: Customer) {
    return this.http.post(`${this.apiUrl}/register`, customer);
  }

  // Login method using User interface
  login(user: User) {
    return this.http.post(`${this.apiUrl}/login`, user);
  }

  saveToken(token: string) {
    localStorage.setItem('authToken', token);
  }

  getToken() {
    return localStorage.getItem('authToken');
  }

  
  logout() {
    localStorage.removeItem('authToken');
    localStorage.removeItem('username'); // Clear the username
    this.router.navigate(['/login']);
  }



}




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


export interface User {
  username: string;
  password: string;
}
