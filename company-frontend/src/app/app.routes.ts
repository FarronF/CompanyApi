import { Routes } from '@angular/router';
import { CompanyList } from './domains/companies/pages/company-list/company-list';
import { CompanyDetails } from './domains/companies/components/company-details/company-details';
import { ViewCompany } from './domains/companies/pages/view-company/view-company';
import { CompanySearch } from './domains/companies/pages/company-search/company-search';
import { CreateCompany } from './domains/companies/pages/create-company/create-company';

export const routes: Routes = [
  { path: '', component: CompanyList },
  { path: 'view/:id', component: ViewCompany },
  { path: 'search', component: CompanySearch },
  { path: 'create', component: CreateCompany },
];
