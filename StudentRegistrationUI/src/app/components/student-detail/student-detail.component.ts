import { Component, EventEmitter, Input, Output, OnInit } from '@angular/core';
import { Student } from 'src/models/student';
import { StudentService } from 'src/services/student.service';
import { Router } from '@angular/router';
import { ChangeDetectorRef } from '@angular/core';

@Component({
  selector: 'app-student-detail',
  templateUrl: './student-detail.component.html',
  styleUrls: ['./student-detail.component.css']
})
export class StudentDetailComponent implements OnInit {
  @Input() student!: Student;
  @Output() close = new EventEmitter<void>();
  profileImageUrl: string | undefined;

  constructor(private studentService: StudentService, private router: Router, private cdr: ChangeDetectorRef) {}

  ngOnInit(): void {
    if (!this.student) {
      console.error('Student data is not available.');
    } else {
      this.student.dateOfBirth = this.formatDate(this.student.dateOfBirth);
      setTimeout(() => {
        this.fetchProfileImageUrl();
        console.log('Student data:', this.student);
      });
    }
  }

  fetchProfileImageUrl(): void {
    if (!this.student || !this.student.studentId) {
      console.error('Student ID is not defined.');
      return;
    }

    this.studentService.getProfileImageUrl(this.student.studentId).subscribe(
      (imageUrl: string) => {
        this.profileImageUrl = imageUrl;
        this.cdr.detectChanges(); 
      },
      (error) => {
        console.error('Failed to fetch profile image URL:', error);
      }
    );
  }

  
  formatDate(date: string | Date): string {
    if (typeof date === 'string') {
      date = new Date(date);
    }
    const year = date.getFullYear();
    const month = ('0' + (date.getMonth() + 1)).slice(-2);
    const day = ('0' + date.getDate()).slice(-2);
    return `${year}-${month}-${day}`;
  }

  saveStudent(): void {
    if (!this.student || !this.student.studentId) {
      console.error('Student data is not properly set. Cannot save.');
      return;
    }

    this.studentService.editStudent(this.student).subscribe({
      next: updatedStudent => {
        console.log('Student updated:', updatedStudent);
      },
      error: error => {
        console.error('Failed to update student:', error);
      }
    });
  }

  deleteStudent(): void {
    if (!this.student || !this.student.studentId) {
      console.error('Student ID is undefined. Cannot delete.');
      return;
    }
  
    console.log('Deleting student with ID:', this.student.studentId);
  
    this.studentService.deleteStudent(this.student.studentId).subscribe({
      next: (success: boolean) => {
        if (success) {
          this.close.emit();
          this.router.navigate(['/studentList']);
        } else {
          console.error('Delete operation failed.');
        }
      },
      error: error => {
        console.error('Failed to delete student:', error);
      }
    });
  }
}
