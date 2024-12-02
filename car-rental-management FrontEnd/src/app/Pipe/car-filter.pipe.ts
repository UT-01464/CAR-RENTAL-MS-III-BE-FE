import { Pipe, PipeTransform } from '@angular/core';
import { Cars } from '../Service/cars.service';

@Pipe({
  name: 'carFilter',
  standalone: true
})
export class CarFilterPipe implements PipeTransform {
  transform(value: Cars[], searchText: string = ''): Cars[] {
    // Return the full list if no search text or value is null/undefined
    if (!value || !Array.isArray(value)) return [];
    if (!searchText) return value;

    // Perform filtering
    return value.filter(car => 
      car.registrationNumber?.toLowerCase().includes(searchText.toLowerCase())
    );
  }
}
