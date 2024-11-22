import { Component } from '@angular/core';
import { AuthService, Customer } from '../../Service/auth.service';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [CommonModule,FormsModule],
  templateUrl: './register.component.html',
  styleUrl: './register.component.css'
})
export class RegisterComponent {

  constructor(private authService: AuthService) {}


  customer = {
    nicNumber: '',
    firstName: '',
    lastName: '',
    username: '',
    phoneNumber:'',
    email: '',
    passwordHash: '',
    address:'',
  };


  onRegister() {
    this.authService.register(this.customer).subscribe(
      (response) => {
        console.log('User registered successfully.');
      },
      (error) => {
        console.error(error);
      }
    );
  }

}
