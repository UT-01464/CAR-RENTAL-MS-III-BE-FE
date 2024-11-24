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
    this.router.navigate(['/nav']);
  }




  getUserDetails() {
    const user = localStorage.getItem('currentUser');
    return user ? JSON.parse(user) : null;
  }
  
  getUserRole() {
    const user = this.getUserDetails();
    return user ? user.role : null;
  }
  





  // saveToken(token: string, expiresIn: number) {
  //   const expiryTime = new Date().getTime() + expiresIn * 1000; // Expiry in ms
  //   localStorage.setItem('authToken', token);
  //   localStorage.setItem('tokenExpiry', expiryTime.toString());
  // }
  
  isTokenExpired(): boolean {
    const expiryTime = parseInt(localStorage.getItem('tokenExpiry') || '0', 10);
    return new Date().getTime() > expiryTime;
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
