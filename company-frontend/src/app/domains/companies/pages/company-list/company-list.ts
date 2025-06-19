import { Component, signal } from '@angular/core';
import { CompanyDataService } from '@app/services/company-data-service';
import { Observable } from 'rxjs';
import { Company } from '../../models/company';
import { MatTableModule } from '@angular/material/table';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatIconModule } from '@angular/material/icon';
import { RouterModule } from '@angular/router';

@Component({
  selector: 'app-company-list',
  imports: [
    MatTableModule,
    MatProgressSpinnerModule,
    MatIconModule,
    RouterModule,
  ],
  templateUrl: './company-list.html',
  styleUrl: './company-list.scss',
})
export class CompanyList {
  displayedColumns: string[] = [
    'id',
    'name',
    'exchange',
    'ticker',
    'isin',
    'website',
    'actions',
  ];
  companies: Company[] = [];
  loading = signal(true);
  constructor(companyDataService: CompanyDataService) {
    companyDataService.getCompanies().subscribe((result) => {
      this.companies = result;
      this.loading.set(false);
    });
  }
}
