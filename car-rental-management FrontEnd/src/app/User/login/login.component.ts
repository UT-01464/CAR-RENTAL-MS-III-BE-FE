import { CommonModule } from '@angular/common';
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

  constructor(private authService: AuthService, private router: Router) {}


  onLogin() {
    this.authService.login(this.user).subscribe(
      (response: any) => {
        if (response && response.token) {
          this.authService.saveToken(response.token);
          const userDetails = {
            username: response.username,
            email: response.email,
          };
          localStorage.setItem('currentUser', JSON.stringify(userDetails));
  
          // Navigate based on role
          if (response.role === 'Admin') {
            this.router.navigate(['/admin']);
          } else {
            this.router.navigate(['/user']);
          }
        }
      },
      (error) => {
        console.error('Login failed:', error);
        alert('Invalid username or password.');
      }
    );
  }
  
}
