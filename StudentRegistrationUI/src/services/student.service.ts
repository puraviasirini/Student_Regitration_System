import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, catchError, throwError } from 'rxjs';
import { Student } from 'src/models/student';

@Injectable({
  providedIn: 'root'
})
export class StudentService {

  baseApiUrl : string = 'https://localhost:7289';

  constructor(private http: HttpClient) { }

  getAllStudents(): Observable<Student[]>{
    return this.http.get<Student[]>(this.baseApiUrl + '/api/Student/GetAllStudents');
  }

  addStudent(addStudentRequest : Student) : Observable<Student>{
    return this.http.post<Student>(this.baseApiUrl + '/api/Student/AddStudent', addStudentRequest);
  }

  getStudentDetailsById(studentId : number): Observable<Student>{
    return this.http.get<Student>(`${this.baseApiUrl}/api/Student/GetStudentDetailsById?id=${studentId}`);
  }

 editStudent(editStudentRequest : Student) : Observable<Student>{
   return this.http.put<Student>(this.baseApiUrl + '/api/Student/EditStudent', editStudentRequest);
 }

  deleteStudent(studentId: number): Observable<boolean> {
   return this.http.delete<boolean>(`${this.baseApiUrl}/api/Student/DeleteStudent/${studentId}`);
 }

 getProfileImageUrl(studentId: number): Observable<string> {
  const url = `${this.baseApiUrl}/api/Student/GetProfileImageUrl/${studentId}`; 
  return this.http.get<string>(url)
    .pipe(
      catchError(error => throwError(error))
    );
}



}
