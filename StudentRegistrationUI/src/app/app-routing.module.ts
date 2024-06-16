import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { StudentListViewComponent } from './components/student-list-view/student-list-view.component';
import { AddStudentComponent } from './components/add-student/add-student.component';
import { StudentDetailComponent } from './components/student-detail/student-detail.component';

const routes: Routes = [
  {
    path: '',
    component : StudentListViewComponent
  },
  {
    path: 'studentList',
    component : StudentListViewComponent
  },
  {
    path: 'add-student',
    component : AddStudentComponent
  },
  { path: 'student-detail/:id', 
    component: StudentDetailComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
