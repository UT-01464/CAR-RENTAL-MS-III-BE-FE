import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { AuthService } from '../../Service/auth.service';
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
        this.authService.saveToken(response.token);
        if (response.role === 'Admin') {
          this.router.navigate(['/admin']); // Redirect Admin to admin page
        } else {
          this.router.navigate(['/user']); // Redirect User to user page
        }
      },
      (error) => {
        this.errorMessage = 'Invalid username or password.';
        console.error(error);
      }
    );
  }

}
