import { Component, OnInit } from '@angular/core';
import { AuthService } from '../../Service/auth.service';
import { CommonModule } from '@angular/common';

import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';

@Component({
  selector: 'app-userpage',
  standalone: true,
  imports: [CommonModule,FormsModule],
  templateUrl: './userpage.component.html',
  styleUrl: './userpage.component.css'
})
export class UserpageComponent implements OnInit {

  username: string | null = '';
  
  recentRentals: any[] = []; // Placeholder for rental data


  constructor(private authService: AuthService,private router: Router) {}

  ngOnInit(): void {

    const user = this.authService.getUserDetails();
  if (user) {
    this.username = user.username;
  } else {
    this.router.navigate(['/login']); // Redirect to login if no user
  }




    // Retrieve the username from localStorage
    this.username = localStorage.getItem('username');

    this.loadUserData();
    this.loadRecentRentals();
  }


  // Load user data (simulate fetching from localStorage or API)
  loadUserData(): void {
    const user = JSON.parse(localStorage.getItem('currentUser') || '{}');
    if (user && user.username) {
      this.username = user.username;
    } else {
      // Redirect to login if user is not found
      this.router.navigate(['/login']);
    }
  }

  // Load recent rentals (simulate fetching from localStorage or API)
  loadRecentRentals(): void {
    this.recentRentals = JSON.parse(localStorage.getItem('recentRentals') || '[]');
  }

  // Navigation handler
  navigateTo(page: string): void {
    this.router.navigate([`/${page}`]);
  }

  // Logout handler
  logout(): void {
    this.authService.logout();
  }

}
