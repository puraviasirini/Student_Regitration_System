import { Component, ViewChild } from '@angular/core';
import { Router } from '@angular/router';
import { Student } from 'src/models/student';
import { FileUploadService } from 'src/services/file-upload.service';
import { StudentService } from 'src/services/student.service';
import { NgForm } from '@angular/forms';

@Component({
  selector: 'app-add-student',
  templateUrl: './add-student.component.html',
  styleUrls: ['./add-student.component.css']
})
export class AddStudentComponent {
  @ViewChild('studentForm') studentForm!: NgForm;
  student: Student;
  selectedProfileImage: File | null = null;
  submitted: boolean = false;

  constructor(private fileUploadService: FileUploadService, private studentService : StudentService, private router: Router) {
    this.student = {
      studentId: 0, 
      firstName: '',
      lastName: '',
      mobile: '',
      email: '',
      nic: '',
      dateOfBirth: '', 
      address: '',
      profileImageUrl: ''
    };
  }

  onSubmit(): void {
    this.submitted = true;

    if (this.studentForm.invalid) {
      console.error('Form is invalid.');
      return;
    }

    if (this.selectedProfileImage) {
      this.fileUploadService.uploadProfileImage(this.selectedProfileImage).subscribe({
        next: (imageUrl: string) => {
          console.log('Image uploaded successfully:', imageUrl);
          this.student.profileImageUrl = imageUrl;

          this.addStudent();
        },
        error: (error) => {
          console.error('Failed to upload profile image:', error);
        }
      });
    } else {
      this.addStudent();
    }
  }


  addStudent(): void {
    this.studentService.addStudent(this.student).subscribe({
      next: () => {
        console.log('Student added successfully.');
        this.router.navigate(['/studentList']);
      },
      error: (error) => {
        console.error('Failed to add student:', error);
      }
    });
  }

  onFileSelected(event: Event): void {
    const inputElement = event.target as HTMLInputElement;
    if (inputElement.files && inputElement.files.length > 0) {
      this.selectedProfileImage = inputElement.files[0];
    }
  }
}
