import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, catchError, throwError } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class FileUploadService {

  private uploadUrl = 'https://localhost:7289/api/Upload/profile-image';

  constructor(private http: HttpClient) { }

  uploadProfileImage(file: File): Observable<string> {
    const formData = new FormData();
    formData.append('image', file, file.name);

    const headers = new HttpHeaders(); 

    return this.http.post<string>(`${this.uploadUrl}`, formData, { headers, responseType: 'text' as 'json' })
      .pipe(
        catchError(error => throwError(error))
      );
  }

  getProfileImageUrl(studentId: number): Observable<string> {
    const url = `${this.uploadUrl}Upload/profile-image/${studentId}`; 

    return this.http.get<string>(url)
      .pipe(
        catchError(error => throwError(error))
      );
  }

}
