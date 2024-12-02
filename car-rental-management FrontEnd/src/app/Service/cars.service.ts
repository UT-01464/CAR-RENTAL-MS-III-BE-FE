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
  
  // createCar(car: Cars): Observable<any> {
  //   return this.http.post<any>(`${this.url}/AddCar`, car);
  // }

  createCar(formData: FormData): Observable<any> {
    return this.http.post<any>(`${this.url}/AddCar`, formData);
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


  //cat,brand,model

 
  getCategories(): Observable<CarCategory[]> {
    return this.http.get<CarCategory[]>(`https://localhost:7122/api/Manager/GetCategories`);
  }

  // Get all Brands
  getBrands(): Observable<Brand[]> {
    return this.http.get<Brand[]>(`${this.url}/GetAllBrands`);
  }

  // Get Models based on selected Brand
  getModels(brandId: number): Observable<Model[]> {
    return this.http.get<Model[]>(`${this.url}/GetAllModels?brandId=${brandId}`);
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


export interface CarCategory {
  id: number;
  name: string;
  description: string;
}


export interface Brand {
  brandId: number;
  name: string;
}

export interface Model {
  modelId: number;
  name: string;
  brandId: number;
  brand: Brand; // The associated brand for the model
}

