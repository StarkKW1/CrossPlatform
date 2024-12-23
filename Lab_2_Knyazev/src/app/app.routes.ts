import { Routes } from '@angular/router';
import {canActivateAuth} from './services/access.guard';
import {LoginPageComponent} from './pages/login-page/login-page.component';
import {LayoutComponent} from './Components/layout/layout.component';
import {SearchStudentComponent} from './pages/search-student/search-student.component';
import {SearchExamComponent} from './pages/search-exam/search-exam.component';
import {StudentProfileComponent} from './pages/student-profile/student-profile.component';
import {ExamProfileComponent} from './pages/exam-profile/exam-profile.component';
import {SearchGroupComponent} from './pages/search-group/search-group.component';
import {GroupProfileComponent} from './pages/group-profile/group-profile.component';

export const routes: Routes = [
  {path: "", component: LayoutComponent, children: [
      {path: "Students", component: SearchStudentComponent},
      {path: "Students/:id", component: StudentProfileComponent},
      {path: "Exams", component: SearchExamComponent},
      {path: "Exams/:id", component: ExamProfileComponent},
      {path: "Groups", component: SearchGroupComponent},
      {path: "Groups/:id", component: GroupProfileComponent},
    ],
    canActivate: [canActivateAuth]
  },
  {path: "login", component: LoginPageComponent},
];
