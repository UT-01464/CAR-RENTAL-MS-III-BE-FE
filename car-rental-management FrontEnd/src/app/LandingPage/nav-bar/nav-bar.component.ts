import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';

@Component({
  selector: 'app-nav-bar',
  standalone: true,
  imports: [FormsModule,CommonModule],
  templateUrl: './nav-bar.component.html',
  styleUrl: './nav-bar.component.css'
})
export class NavBarComponent implements OnInit {

  isExpanded: boolean = false;

  toggleReadMore(): void {
    this.isExpanded = !this.isExpanded;
  }

  constructor(private router: Router) {}

  getstart() {
    this.router.navigate(['/login']);
  }

  

  cars = [
    {
      name: 'BMW',
      price: 'Starting from $80/Day',
      image: '/assets/images/homecar.jpg',
      type: 'Sedan',
      seats: 4,
      doors: 4,
      ac: true,
    },
    {
      name: 'BMW',
      price: 'Starting from $80/Day',
      image: '/assets/images/homecar.jpg',
      type: 'Sedan',
      seats: 4,
      doors: 4,
      ac: true,
    },
    {
      name: 'BMW',
      price: 'Starting from $80/Day',
      image: '/assets/images/homecar.jpg',
      type: 'Sedan',
      seats: 4,
      doors: 4,
      ac: true,
    },
    {
      name: 'BMW',
      price: 'Starting from $80/Day',
      image: '/assets/images/homecar.jpg',
      type: 'Sedan',
      seats: 4,
      doors: 4,
      ac: true,
    },
    {
      name: 'Honda',
      price: 'Starting from $80/Day',
      image: '/assets/images/homecar.jpg',
      type: 'Sedan',
      seats: 4,
      doors: 4,
      ac: true,
    },
    {
      name: 'Ferrari',
      price: 'Starting from $80/Day',
      image: '/assets/images/homecar.jpg',
      type: 'Sedan',
      seats: 4,
      doors: 4,
      ac: true,
    },
    {
      name: 'Audi',
      price: 'Starting from $100/Day',
      image: '/assets/images/homecar.jpg',
      type: 'SUV',
      seats: 5,
      doors: 5,
      ac: true,
    },
    // Add more cars here if needed
  ];

  filterText: string = '';
  filteredCars = [...this.cars];
  visibleCars: any[] = []; // Cars to display (up to 6)

  ngOnInit(): void {
    this.filterCars();
  }

  filterCars(): void {
    this.filteredCars = this.cars.filter((car) =>
      car.name.toLowerCase().includes(this.filterText.toLowerCase())
    );
    this.setVisibleCars(); // Update visible cars after filtering
  }

  setVisibleCars(): void {
    this.visibleCars = this.filteredCars.slice(0, 6); // Only show the first 6 cars
  }

  viewAllCars(): void {
    this.router.navigate(['/all-cars']); // Navigate to the page that shows all cars
  }









}
