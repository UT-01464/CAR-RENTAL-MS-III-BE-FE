import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [CommonModule,FormsModule,RouterModule],
  templateUrl: './home.component.html',
  styleUrl: './home.component.css'
})
export class HomeComponent implements OnInit {



  menuValue:boolean=false;
 menu_icon :string ='bi bi-list';
 openMenu(){
    this.menuValue =! this.menuValue ;
    this.menu_icon = this.menuValue ? 'bi bi-x' : 'bi bi-list';
  }
   closeMenu() {
    this.menuValue = false;
    this.menu_icon = 'bi bi-list';
  }





 
  cars = [
    { 
      name: 'Mercedes-Benz', 
      price: '$150', 
      image: '/assets/images-copy/car1.jpg'
    },
    { 
      name: 'BMW X5', 
      price: '$120', 
      image: '/assets/images-copy/car2.jpg'
    },
    { 
      name: 'Audi A8', 
      price: '$140', 
      image: '/assets/images-copy/car3.jpg'
    }
  ];


  ngOnInit(): void {
    this.rentCar
  }

  rentCar(car: any): void {
    alert(`You have selected the ${car.name} for rent at ${car.price}/day`);
    // Implement logic for renting the car (e.g., open a rental form)
  }


}
