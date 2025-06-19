import { Component, input, signal } from '@angular/core';
import { MatCardModule } from '@angular/material/card';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { Company } from '../../models/company';

@Component({
  selector: 'app-company-details',
  imports: [MatCardModule, MatProgressSpinnerModule],
  templateUrl: './company-details.html',
  styleUrl: './company-details.scss',
})
export class CompanyDetails {
  company = input<Company>();
}
