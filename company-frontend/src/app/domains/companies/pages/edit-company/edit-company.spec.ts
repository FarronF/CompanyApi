import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CompanyEdit } from './edit-company';

describe('CompanyEdit', () => {
  let component: CompanyEdit;
  let fixture: ComponentFixture<CompanyEdit>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CompanyEdit],
    }).compileComponents();

    fixture = TestBed.createComponent(CompanyEdit);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
