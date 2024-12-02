import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class CarsService {

  url = 'https://localhost:7122/api/Car';

  constructor(private http: HttpClient) { }

  getCars(): Observable<Cars[]> {
    return this.http.get<Cars[]>(`${this.url}/GetAllCars`);
  }
  
  createCar(car: Cars): Observable<any> {
    return this.http.post<any>(`${this.url}/AddCar`, car);
  }
  
  deleteCar(carId: number): Observable<any> {
    return this.http.delete<any>(`${this.url}/DeleteCar${carId}`);
  }
  
  getCarById(carId: number): Observable<Cars> {
    return this.http.get<Cars>(`${this.url}/GetCarById/${carId}`);
  }
  
  updateCar(car: Cars): Observable<any> {
    return this.http.put<any>(`${this.url}/UpdateCar/${car.carId}`, car);
  }
  
}

export enum AvailabilityStatus {
  Available = 'Available',
  Rented = 'Rented',
  Maintenance = 'Maintenance'
}

export interface Cars {
  carId:number;
  registrationNumber: string;
  modelName: string;
  brandName:string;
  year: number;
  categoryName: string;
  unitsAvailable: number;
  availabilityStatus: AvailabilityStatus; // Use the enum type
  imageUrl: string;
}

