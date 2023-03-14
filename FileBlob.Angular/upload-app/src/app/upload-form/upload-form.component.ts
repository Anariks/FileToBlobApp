import { ChangeDetectorRef, Component } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { catchError, throwError } from 'rxjs';
import { FileUploadService } from '../services/file-upload.service';
import { MyErrorStateMatcher } from './MyErrorStateMatcher';
import { PopUpComponent } from './pop-up/pop-up.component';

@Component({
  selector: 'app-upload-form',
  templateUrl: './upload-form.component.html',
  styleUrls: ['./upload-form.component.scss']
})

export class UploadFormComponent {
  myForm: FormGroup;
  fileError: boolean = false;
  matcher = new MyErrorStateMatcher();

  constructor(private fb: FormBuilder,
    private httpService: FileUploadService,
    private cd: ChangeDetectorRef,
    private dialogRef: MatDialog) {
    this.myForm = fb.group({
      "email": ['', [Validators.required, Validators.email]],
      "file": ['', [Validators.required, this.fileExtensionValidator.bind(this)]]
    })
  }

  onFileChange(event: any) {
    if (event.target.files && event.target.files.length) {
      const file = event.target.files[0];
      this.myForm.patchValue({
        file: file
      });
    };
  }

  submit() {
    this.httpService.upload(this.myForm.value).pipe(
      catchError((error: any) => {

        let errorMessage;
        if (error.status === 400) {
          errorMessage = JSON.parse(error.error);
        }

        this.NotSuccess(errorMessage.Message || "Something went wrong, I tried my best 🤦‍♂️");
        return throwError(() => error);
      })
    ).subscribe((response) => {
      this.Success();
    });
  }

  fileExtensionValidator(control: FormControl): { [s: string]: boolean } | null {
    const file = control.value as File;

    if (file && file.type === `application/vnd.openxmlformats-officedocument.wordprocessingml.document`) {
      this.fileError = false;
      return null;
    }

    this.fileError = true;
    return { "file": true };
  }

  private Success(): void {
    this.myForm.reset();
    this.dialogRef.open(PopUpComponent,
      {
        data: {
          header: `Your doocument was successfully uploaded!`,
          message: `Once it on Azure Storage you should receive email letter, but possible that you don't gonna receive it immediately, I bet it's because of my consumption subscribe, my function willing to stop working after some period of time till I go to AzurePortal and poke it to proceed all the new blobs. Thank you for interesing project! 🎉🎉🎉`
        }
      })
  }

  private NotSuccess(error: string): void {
    this.dialogRef.open(PopUpComponent,
      {
        data: {
          header: `Your doocument was was not uploaded to Azure Storage `,
          message: error
        }
      })
  }
}




