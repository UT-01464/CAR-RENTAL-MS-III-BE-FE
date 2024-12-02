import { Pipe, PipeTransform } from '@angular/core';
import { Cars } from '../Service/cars.service';

@Pipe({
  name: 'carFilter',
  standalone: true
})
export class CarFilterPipe implements PipeTransform {
  transform(value: Cars[], ...args: string[]): Cars[] {
    let searchText = args[0];

    return value.filter(a => a.registrationNumber.toLowerCase().includes(searchText.toLowerCase()))
    

  }
}
