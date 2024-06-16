import { Component } from '@angular/core';
import { Student } from 'src/models/student';
import { StudentService } from 'src/services/student.service';

@Component({
  selector: 'app-student-list-view',
  templateUrl: './student-list-view.component.html',
  styleUrls: ['./student-list-view.component.css']
})
export class StudentListViewComponent {

  students: Student[] = [];
  filteredStudents: Student[] = [];
  currentPage = 1;
  pageSize = 10;
  searchTerm = '';
  defaultImageUrl: string = '/assets/no-profile-image.png'; 
  sortColumn: string = ''; 
  sortDirection: string = 'asc'; 
  selectedStudent: Student | null = null; 
  
  constructor(private studentService: StudentService) {}

  ngOnInit(): void {
    this.studentService.getAllStudents().subscribe((data: Student[]) => {
      this.students = data;
      this.filteredStudents = data;
      console.log('this.students'+ data);
    });
  }

  get pagedStudents(): Student[] {
    const start = (this.currentPage - 1) * this.pageSize;
    return this.filteredStudents.slice(start, start + this.pageSize);
  }

  searchStudents(): void {
    if (this.searchTerm) {
      const searchTermLower = this.searchTerm.toLowerCase();
      this.filteredStudents = this.students.filter(student =>
        student.firstName.toLowerCase().includes(searchTermLower) ||
        student.lastName.toLowerCase().includes(searchTermLower) ||
        student.mobile.includes(this.searchTerm) ||
        student.email.toLowerCase().includes(searchTermLower) ||
        student.nic.toLowerCase().includes(searchTermLower) ||
        (student.dateOfBirth && student.dateOfBirth.toString().includes(this.searchTerm)) ||
        student.address.toLowerCase().includes(searchTermLower)
      );
    } else {
      this.filteredStudents = this.students;
    }
    this.currentPage = 1;
    this.sortTable(this.sortColumn);
  }


  previousPage(): void {
    if (this.currentPage > 1) {
      this.currentPage--;
    }
  }

  nextPage(): void {
    if ((this.currentPage * this.pageSize) < this.filteredStudents.length) {
      this.currentPage++;
    }
  }

  sortTable(column: string): void {
    if (this.sortColumn === column) {
      this.sortDirection = this.sortDirection === 'asc' ? 'desc' : 'asc';
    } else {
      this.sortColumn = column;
      this.sortDirection = 'asc';
    }


    this.filteredStudents.sort((a, b) => {
      const direction = this.sortDirection === 'asc' ? 1 : -1;
      const valueA = this.getPropertyValue(a, column);
      const valueB = this.getPropertyValue(b, column);
      return direction * valueA.localeCompare(valueB, undefined, { numeric: true });
    });
  }

  getPropertyValue(item: any, column: string): string {
    switch (column) {
      case 'firstName':
        return item.firstName;
      case 'lastName':
        return item.lastName;
      case 'mobile':
        return item.mobile;
      case 'email':
        return item.email;
      case 'nic':
        return item.nic;
      case 'dateOfBirth':
        return item.dateOfBirth.toString(); 
      case 'address':
        return item.address;
      default:
        return ''; 
    }
  }

  onRowClick(student: Student): void {
    if (this.selectedStudent === student) {
      this.selectedStudent = null; 
    } else {
      this.selectedStudent = student;
    }
  }
  
}
