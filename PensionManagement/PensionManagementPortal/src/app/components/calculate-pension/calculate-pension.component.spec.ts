import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CalculatePensionComponent } from './calculate-pension.component';

describe('CalculatePensionComponent', () => {
  let component: CalculatePensionComponent;
  let fixture: ComponentFixture<CalculatePensionComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CalculatePensionComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(CalculatePensionComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
