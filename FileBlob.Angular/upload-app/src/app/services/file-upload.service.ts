import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class FileUploadService {

  baseApiUrl = environment.baseApiUrl;

  constructor(private http: HttpClient) { }

  upload(formValue: any): Observable<any> {
    const formData = new FormData();

    for (const key of Object.keys(formValue)) {
      const value = formValue[key];
      formData.append(key, value);
    }

    const requestOptions: Object = {
      responseType: 'text'
    }

    return this.http.post<any>(this.baseApiUrl, formData, requestOptions);
  }
}
