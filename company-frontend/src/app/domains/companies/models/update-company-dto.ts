import { UpdateCompanyDto as UpdateCompanyApiDto } from '@api/models/update-company-dto';
import { Company } from './company';

export interface UpdateCompanyDto {
  exchange?: string;
  isin?: string;
  name?: string;
  ticker?: string;
  website?: string;
}

export function mapUpdateCompanyDtoToApi(
  updateCompanyDto: UpdateCompanyDto
): UpdateCompanyApiDto {
  return {
    exchange: updateCompanyDto.exchange,
    isin: updateCompanyDto.isin,
    name: updateCompanyDto.name,
    ticker: updateCompanyDto.ticker,
    website: updateCompanyDto.website,
  };
}

export function mapCompanyToUpdateCompanyDto(
  createCompanyDto: Company
): UpdateCompanyDto {
  return {
    exchange: createCompanyDto.exchange,
    isin: createCompanyDto.isin,
    name: createCompanyDto.name,
    ticker: createCompanyDto.ticker,
    website: createCompanyDto.website ?? undefined,
  };
}
