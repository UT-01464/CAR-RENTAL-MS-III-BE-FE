import { CommonModule, getLocaleMonthNames } from '@angular/common';
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { AuthService, User } from '../../Service/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [CommonModule,FormsModule],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent {

  user = { username: '', password: '' };
  errorMessage = '';


  // Register and login data
  isRegisterMode = false;
  registerData = {
    firstname: '',
    lastname: '',
    nicNumber: '',
    email: '',
    phoneNumber: '',
    address: '',
    username: '',
    password: ''
  };

  loginData = {
    username: '',
    password: ''
  };

  constructor(private authService: AuthService, private router: Router) {}

  // Toggle between login and registration modes
  toggleMode(): void {
    this.isRegisterMode = !this.isRegisterMode;
  }

  
  // Handle registration logic and store data in localStorage
  onRegister(): void {
    console.log('Register Data:', this.registerData);


    // You can also clear the form after registration
    this.registerData = {
      firstname: '',
      lastname: '',
      nicNumber: '',
      email: '',
      phoneNumber: '',
      address: '',
      username: '',
      password: ''
    };

    console.log('Registration successful, data saved to localStorage');
  }






  // Handle login logic
  onLogin(): void {
    console.log('Login Data:', this.loginData);

    // Retrieve the saved user data from localStorage
    const savedUser = JSON.parse(localStorage.getItem('user') || '{}');

    // Check if the username and password match
    if (savedUser.username === this.loginData.username && savedUser.password === this.loginData.password) {
      console.log('Login successful');
      this.router.navigate(['/navbar']);  // Redirect to home page after successful login
    } else {
      console.error('Login failed: Invalid credentials');
      this.errorMessage = 'Invalid credentials. Please try again.';
    }
  }
}