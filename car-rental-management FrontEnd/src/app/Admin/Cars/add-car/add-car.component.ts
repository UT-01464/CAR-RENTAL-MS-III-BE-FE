import { Component } from '@angular/core';
import { Brand, CarCategory, CarsService, Model } from '../../../Service/cars.service';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-add-car',
  standalone: true,
  imports: [CommonModule,ReactiveFormsModule,FormsModule],
  templateUrl: './add-car.component.html',
  styleUrl: './add-car.component.css'
})
export class AddCarComponent {
  carForm: FormGroup;
  categories: CarCategory[] = []; // Correct type
  brands: Brand[] = []; // Correct type
  models: Model[] = []; // Correct type
  selectedFile: File | null = null;

  availabilityStatuses = [];
  

  constructor(
    private fb: FormBuilder,
    private carService: CarsService,
    private router: Router,
    private http: HttpClient
  ) {
    this.carForm = this.fb.group({
      registrationNumber: ['', Validators.required],
      modelName: ['', Validators.required],
      brandName: ['', Validators.required],
      year: ['', Validators.required],
      categoryName: ['', Validators.required],
      unitsAvailable: ['', Validators.required],
      availabilityStatus: ['', Validators.required],
      imageUrl: [null, Validators.required]
    });
    
  }

  ngOnInit(): void {
    this.loadCategories();
    this.loadBrands();
  }

  // Load categories from the backend API
  loadCategories(): void {
    this.carService.getCategories().subscribe((data: CarCategory[]) => {
      this.categories = data;
    });
  }

  // Load brands from the backend API
  loadBrands(): void {
    this.carService.getBrands().subscribe((data: Brand[]) => {
      this.brands = data;
    });
  }

  // Load models based on the selected brandId
  loadModels(brandId: number): void {
    this.carService.getModels(brandId).subscribe((data: Model[]) => {
      this.models = data;
    });
  }

  onFileChange(event: any): void {
    const file = event.target.files[0];
    if (file) {
      this.selectedFile = file;
    }
  }
  

  // Handle brand change and load models accordingly
  onBrandChange(event: any): void {
    const selectedBrandId = event.target.value;
    this.loadModels(selectedBrandId);  // Load models based on selected brand
  }

  // Submit the form
  // onSubmit(): void {
  //   console.log('onSubmit triggered.');
  //   if (this.carForm.valid && this.selectedFile) {
  //     // Create form data to send the file and form data to the backend
  //     const formData = new FormData();
  //     formData.append('file', this.selectedFile);

  //     // Call the API to upload the image
  //     this.http.post('/api/Cars/UploadImage', formData).subscribe(
  //       (response: any) => {
  //         const imagePath = response.imagePath;

  //         // Add image path to the car data
  //         const carData = this.carForm.value;
  //         carData.imageUrl = imagePath; // Ensure imageUrl is correctly set

  //         // Now send the car data (including imageUrl) to your backend
  //         this.carService.createCar(carData).subscribe(
  //           (response) => {
  //             console.log('Car created successfully:', response);
  //             this.router.navigate(['/cars']);  // Navigate after successful creation
  //           },
  //           (error) => {
  //             console.error('Error creating car:', error);
  //           }
  //         );
  //       },
  //       (error) => {
  //         console.error('Error uploading image:', error);
  //       }
  //     );
  //   }
  // }


  onSubmit(): void {
    if (this.carForm.invalid) {
      console.log("Form is invalid"); // This helps verify if the form is invalid
      return; // Stop if form is invalid
    }
  
    console.log("Form submitted:", this.carForm.value); // Log form values to see if they're correct
  
    // Prepare FormData (or Car object) to be sent
    const formData = new FormData();
    formData.append('registrationNumber', this.carForm.get('registrationNumber')?.value);
    formData.append('modelName', this.carForm.get('modelName')?.value);
    formData.append('brandName', this.carForm.get('brandName')?.value);
    formData.append('year', this.carForm.get('year')?.value);
    formData.append('categoryName', this.carForm.get('categoryName')?.value);
    formData.append('unitsAvailable', this.carForm.get('unitsAvailable')?.value);
    formData.append('availabilityStatus', this.carForm.get('availabilityStatus')?.value);
    
    if (this.selectedFile) {
      formData.append('imageUrl', this.selectedFile, this.selectedFile.name);
    }
  
    // Call car service to add car
    this.carService.createCar(formData).subscribe(
      response => {
        console.log("Car added successfully", response);
        this.router.navigate(['/cars']);  // Adjust navigation as needed
        alert('Car added successfully!');
      },
      error => {
        console.log("Error adding car", error); // Log the error
        alert('Error adding car: ' + error.message);
      }
    );
  }
  


  // onSubmit(): void {
  //   console.log('onSubmit triggered.');
    
  //   // Check if form is valid and a file is selected
  //   if (this.carForm.valid && this.selectedFile) {
  //     // Create FormData object to hold the car data and image file
  //     const formData = new FormData();
      
  //     // Append form data fields to FormData
  //     formData.append('registrationNumber', this.carForm.get('registrationNumber')?.value);
  //     formData.append('modelName', this.carForm.get('modelName')?.value);
  //     formData.append('brandName', this.carForm.get('brandName')?.value);
  //     formData.append('year', this.carForm.get('year')?.value);
  //     formData.append('categoryName', this.carForm.get('categoryName')?.value);
  //     formData.append('unitsAvailable', this.carForm.get('unitsAvailable')?.value);
  //     formData.append('availabilityStatus', this.carForm.get('availabilityStatus')?.value);
      
  //     // Append the selected file (image) to FormData
  //     formData.append('imageFile', this.selectedFile, this.selectedFile.name); // Use the file name here
  
  //     // Step 1: Send the form data (car details + image) to the backend
  //     this.carService.createCar(formData).subscribe(
  //       (response: any) => {
  //         console.log('Car created successfully:', response);
  
  //         // Step 2: Redirect to car list page after success
  //         this.router.navigate(['/cars']);
  //       },
  //       (error) => {
  //         console.error('Error creating car:', error);
  //         alert('Error creating car. Please try again.');
  //       }
  //     );
  //   } else {
  //     console.error('Form is invalid or no file selected.');
  //     alert('Please complete the form and upload an image.');
  //   }
  // }
  
  
  
  
  
  

  // Reset form on cancel
  cancel(): void {
    this.carForm.reset();
  }
}