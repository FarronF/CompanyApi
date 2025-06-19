import { Component, inject, OnInit, signal } from '@angular/core';
import {
  FormControl,
  FormGroup,
  FormsModule,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatRadioChange, MatRadioModule } from '@angular/material/radio';
import { CompanyDataService } from '@app/services/company-data-service';
import { Observable } from 'rxjs';
import { Company } from '../../models/company';
import { Router } from '@angular/router';
import { PopupMessageService } from '@app/services/popup-message-service';

@Component({
  selector: 'app-company-search',
  imports: [
    MatFormFieldModule,
    MatInputModule,
    FormsModule,
    ReactiveFormsModule,
    MatButtonModule,
    MatRadioModule,
  ],
  templateUrl: './company-search.html',
  styleUrl: './company-search.scss',
})
export class CompanySearch implements OnInit {
  searching = signal(false);
  searchType: 'id' | 'isin' = 'id';
  searchFormControl = new FormControl('');
  idValidators = [Validators.required, Validators.pattern(/^\d+$/)];
  isinValidators = [
    Validators.required,
    Validators.minLength(12),
    Validators.maxLength(12),
    Validators.pattern(/^[A-Z]{2}[A-Z0-9]{10}$/),
  ];

  constructor(
    private companyDataService: CompanyDataService,
    private popupMessageService: PopupMessageService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.searchFormControl.setValidators(this.idValidators);
  }

  onSearchTypeChange(event: MatRadioChange) {
    if (event.value === 'id') {
      this.searchFormControl.setValidators(this.idValidators);
    } else if (event.value === 'isin') {
      this.searchFormControl.setValidators(this.isinValidators);
    }
    this.searchFormControl.updateValueAndValidity();
  }

  showCouldNotFindMessage() {
    this.popupMessageService.error(
      'Could not find company with the provided search criteria. Please check the ID or ISIN and try again.'
    );
  }

  search() {
    if (!this.searchFormControl.valid) {
      this.popupMessageService.error('Search form is invalid');
    }

    var searchQuery = this.searchFormControl.value || '';

    if (this.searchFormControl.value === '') {
      this.popupMessageService.error('Search query is empty');
      return;
    }

    let observable: Observable<Company>;

    if (this.searchType === 'id') {
      const id = Number(searchQuery);
      if (isNaN(id)) {
        this.popupMessageService.error('Invalid ID format, should be a number');
        return;
      }
      observable = this.companyDataService.getCompanyById(id);
    } else if (this.searchType === 'isin') {
      observable = this.companyDataService.getCompanyByIsin(searchQuery);
    } else {
      this.popupMessageService.error('Invalid search type');
      return;
    }

    this.searching.set(true);
    observable.subscribe({
      next: (company) => {
        if (company) {
          this.router.navigate([`/view/${company.id}`]);
        } else {
          this.showCouldNotFindMessage();
        }
        this.searching.set(false);
      },
      error: (err) => {
        if (err.status === 404) {
          this.showCouldNotFindMessage();
        } else {
          this.popupMessageService.error('An error occurred');
        }
        this.searching.set(false);
      },
    });
  }
}
