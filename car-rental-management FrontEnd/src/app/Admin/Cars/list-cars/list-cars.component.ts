import { Component, OnInit, TemplateRef } from '@angular/core';
import { Router } from '@angular/router';
import { Cars, CarsService } from '../../../Service/cars.service';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { CarFilterPipe } from "../../../Pipe/car-filter.pipe";

@Component({
  selector: 'app-list-cars',
  standalone: true,
  imports: [FormsModule, CommonModule, ReactiveFormsModule, CarFilterPipe],
  templateUrl: './list-cars.component.html',
  styleUrl: './list-cars.component.css'
})
export class ListCarsComponent implements OnInit {
  searchText: string = '';

  cars: Cars[] = [];

 

  constructor(private carService: CarsService,private router: Router) {
    console.log(this.cars);
  
  }

  ngOnInit(): void {
    this.LoadCars();
  }

 

  // onDelete(carId: number) {
  //   if (confirm('Do you want to delete?')) {
  //     this.carService.deleteCar(carId).subscribe(
  //       (data) => {
  //         alert('Task is deleted successfully');
  //         this.LoadCars();
  //       },
  //       (error) => {
  //         console.error('Error deleting task:', error);
  //         alert('An error occurred while deleting the task. Please try again.');
  //       }
  //     );
  //   }
  // }

  onDelete(carId: number) {
    console.log('Attempting to delete car with ID:', carId); // Debug log
    if (confirm('Do you want to delete?')) {
      this.carService.deleteCar(carId).subscribe(
        (data) => {
          alert('Car is deleted successfully');
          this.LoadCars(); // Refresh the list after deletion
        },
        (error) => {
          console.error('Error deleting car:', error); // Detailed error
          alert('An error occurred while deleting the car. Please try again.');
        }
      );
    }
  }
  
  



  LoadCars()
  {
    this.carService.getCars().subscribe(d=>
    {
      this.cars=d
    }
    )
  }

  onEdit(taskId: number) {
    this.router.navigate(['/edit', taskId])
  }

}


