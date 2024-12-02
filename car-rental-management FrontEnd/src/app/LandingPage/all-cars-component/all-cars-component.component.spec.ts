import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AllCarsComponentComponent } from './all-cars-component.component';

describe('AllCarsComponentComponent', () => {
  let component: AllCarsComponentComponent;
  let fixture: ComponentFixture<AllCarsComponentComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AllCarsComponentComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(AllCarsComponentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
