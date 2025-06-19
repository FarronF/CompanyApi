import { Component, effect, input, output } from '@angular/core';
import { Company } from '../../models/company';
import { MatFormFieldModule } from '@angular/material/form-field';
import {
  FormBuilder,
  FormControl,
  FormGroup,
  FormsModule,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';

@Component({
  selector: 'app-company-form',
  imports: [
    MatFormFieldModule,
    MatInputModule,
    FormsModule,
    ReactiveFormsModule,
    MatButtonModule,
  ],
  templateUrl: './company-form.html',
  styleUrl: './company-form.scss',
})
export class CompanyForm {
  companyForm: FormGroup;
  isinFormControl: FormControl;

  company = input<Company>();
  updating = input<boolean>();
  onSave = output<Company>();
  onCancel = output();

  constructor(public formBuilder: FormBuilder) {
    effect(() => {
      if (this.company()) {
        this.companyForm.patchValue({
          name: this.company()?.name ?? '',
          ticker: this.company()?.ticker ?? '',
          exchange: this.company()?.exchange ?? '',
          isin: this.company()?.isin ?? '',
          website: this.company()?.website ?? '',
        });
      }
    });
    this.companyForm = formBuilder.group({
      name: formBuilder.control('', Validators.required),
      ticker: formBuilder.control('', Validators.required),
      exchange: formBuilder.control('', Validators.required),
      isin: formBuilder.control('', [
        Validators.required,
        Validators.minLength(12),
        Validators.maxLength(12),
        Validators.pattern(/^[A-Z]{2}[A-Z0-9]{10}$/),
      ]),
      website: formBuilder.control(''),
    });

    this.isinFormControl = this.companyForm.get('isin') as FormControl;
  }

  onSubmit(): void {
    if (this.companyForm.valid) {
      const companyData: Company = {
        ...this.company(),
        ...this.companyForm.value,
      };
      this.onSave.emit(companyData);
    } else {
      this.companyForm.markAllAsTouched();
    }
  }

  cancel() {
    this.onCancel.emit();
  }
}
