import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ViewCompany } from './view-company';

describe('ViewCompany', () => {
  let component: ViewCompany;
  let fixture: ComponentFixture<ViewCompany>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ViewCompany]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ViewCompany);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
