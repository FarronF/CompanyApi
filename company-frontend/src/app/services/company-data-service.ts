import { Injectable } from '@angular/core';
import { CompanyService } from '@app/api/services/company.service';
import {
  Company,
  mapApiCompanyToUi,
} from '@app/domains/companies/models/company';
import {
  CreateCompanyDto,
  mapCreateCompanyDtoToApi,
} from '@app/domains/companies/models/create-company-dto';
import {
  UpdateCompanyDto,
  mapUpdateCompanyDtoToApi,
} from '@app/domains/companies/models/update-company-dto';
import { map, Observable, tap } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class CompanyDataService {
  constructor(private companyApiService: CompanyService) {}

  getCompanies(): Observable<Company[]> {
    return this.companyApiService.apiCompanyGet$Json().pipe(
      tap((companies) => console.log('Companies fetched:', companies)),
      map((companies) =>
        companies?.map((company) => mapApiCompanyToUi(company))
      )
    );
  }

  getCompanyById(id: number): Observable<Company> {
    return this.companyApiService
      .apiCompanyIdIdGet$Json({ id: id })
      .pipe(map((company) => mapApiCompanyToUi(company)));
  }

  getCompanyByIsin(isin: string): Observable<Company> {
    return this.companyApiService
      .apiCompanyIsinIsinGet$Json({ isin: isin })
      .pipe(map((company) => mapApiCompanyToUi(company)));
  }

  updateCompany(id: number, company: UpdateCompanyDto): Observable<boolean> {
    let apiDto = mapUpdateCompanyDtoToApi(company);
    return this.companyApiService
      .apiCompanyIdIdPut$Response({ id: id, body: apiDto })
      .pipe(map((response) => (response.ok ? true : false)));
  }

  createCompany(
    company: CreateCompanyDto
  ): Observable<[boolean, Company | null]> {
    let apiDto = mapCreateCompanyDtoToApi(company);
    return this.companyApiService
      .apiCompanyPost$Json$Response({ body: apiDto })
      .pipe(
        tap((response) => console.log(response)),
        map((response) => {
          if (!response.ok || !response.body) return [false, null];
          const createdCompany = mapApiCompanyToUi(response.body);
          return [true, createdCompany];
        })
      );
  }
}
