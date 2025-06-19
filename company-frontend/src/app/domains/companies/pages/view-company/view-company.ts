import { Component, signal } from '@angular/core';
import { CompanyDataService } from '@app/services/company-data-service';
import { Company } from '../../models/company';
import { ActivatedRoute } from '@angular/router';
import { CompanyDetails } from '../../components/company-details/company-details';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { CompanyForm } from '../../components/company-form/company-form';
import { MatButtonModule } from '@angular/material/button';
import { mapCompanyToUpdateCompanyDto } from '../../models/update-company-dto';
import { PopupMessageService } from '@app/services/popup-message-service';

@Component({
  selector: 'app-view-company',
  imports: [
    CompanyDetails,
    CompanyForm,
    MatProgressSpinnerModule,
    MatButtonModule,
  ],
  templateUrl: './view-company.html',
  styleUrl: './view-company.scss',
})
export class ViewCompany {
  company!: Company;
  updating = false;
  loading = signal(true);
  noCompany = signal(false);
  editing = signal(false);
  constructor(
    private companyDataService: CompanyDataService,
    private popupMessageService: PopupMessageService,
    private activatedRoute: ActivatedRoute
  ) {
    var urlId = this.activatedRoute.snapshot.paramMap.get('id');

    var id = Number(urlId);
    if (!id || isNaN(id)) {
      this.noCompany.set(true);
      return;
    }

    companyDataService.getCompanyById(id).subscribe((result) => {
      if (!result) {
        this.noCompany.set(true);
      }
      this.company = result;
      this.loading.set(false);
    });
  }

  onSave(company: Company) {
    this.updating = true;

    this.companyDataService
      .updateCompany(company.id, mapCompanyToUpdateCompanyDto(company))
      .subscribe({
        next: (result) => {
          if (result) {
            this.company = company;
            this.editing.set(false);
          }
          this.updating = false;
        },
        error: (error) => {
          this.popupMessageService.error('Could not update company.');
          this.updating = false;
        },
      });
  }
}
