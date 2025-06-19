import { Company as CompanyApiModel } from '@api/models/company';

export interface Company {
    exchange: string;
    id: number;
    isin: string;
    name: string;
    ticker: string;
    website?: string | null;
}

/**
 * Maps an API Company model to a UI Company model.
 * @param apiCompany The API Company model to map.
 * @returns The mapped UI Company model.
 */
export function mapApiCompanyToUi(apiCompany: CompanyApiModel): Company {
  return {
    exchange: apiCompany.exchange,
    id: apiCompany.id,
    name: apiCompany.name,
    isin: apiCompany.isin,
    ticker: apiCompany.ticker,
    website: apiCompany.website ?? null
  };
}