import { CreateCompanyDto as CreateCompanyApiDto } from '@api/models/create-company-dto';
import { Company } from './company';

export interface CreateCompanyDto {
  exchange: string;
  isin: string;
  name: string;
  ticker: string;
  website?: string;
}

export function mapCreateCompanyDtoToApi(
  createCompanyDto: CreateCompanyDto
): CreateCompanyApiDto {
  return {
    exchange: createCompanyDto.exchange,
    isin: createCompanyDto.isin,
    name: createCompanyDto.name,
    ticker: createCompanyDto.ticker,
    website: createCompanyDto.website,
  };
}

export function mapCompanyToCreateCompanyDto(
  createCompanyDto: Company
): CreateCompanyDto {
  return {
    exchange: createCompanyDto.exchange,
    isin: createCompanyDto.isin,
    name: createCompanyDto.name,
    ticker: createCompanyDto.ticker,
    website: createCompanyDto.website ?? undefined,
  };
}
