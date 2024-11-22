import { Component, OnInit } from '@angular/core';
import { AuthService } from '../../Service/auth.service';
import { CommonModule } from '@angular/common';

import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-userpage',
  standalone: true,
  imports: [CommonModule,FormsModule],
  templateUrl: './userpage.component.html',
  styleUrl: './userpage.component.css'
})
export class UserpageComponent implements OnInit {

  username: string | null = '';

  constructor(private authService: AuthService) {}

  ngOnInit(): void {
    // Retrieve the username from localStorage
    this.username = localStorage.getItem('username');
  }

  logout() {
    this.authService.logout();  // Call the logout method from AuthService
  }

}
