import { Component } from '@angular/core';
import { Company } from '../../models/company';
import { CompanyDataService } from '@app/services/company-data-service';
import { PopupMessageService } from '@app/services/popup-message-service';
import { Router } from '@angular/router';
import { CompanyForm } from '../../components/company-form/company-form';
import { mapCompanyToCreateCompanyDto } from '../../models/create-company-dto';

@Component({
  selector: 'app-create-company',
  imports: [CompanyForm],
  templateUrl: './create-company.html',
  styleUrl: './create-company.scss',
})
export class CreateCompany {
  updating = false;
  constructor(
    private companyDataService: CompanyDataService,
    private popupMessageService: PopupMessageService,
    private router: Router
  ) {}

  onSave(company: Company) {
    this.updating = true;

    this.companyDataService
      .createCompany(mapCompanyToCreateCompanyDto(company))
      .subscribe({
        next: (result) => {
          if (result[0] && result[1]) {
            this.router.navigate(['/view', result[1].id]);
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
