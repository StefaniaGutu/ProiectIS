import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from "./pages/home/home.component";
import { LoginComponent } from './pages/login/login.component';
import { RegisterComponent } from './pages/register/register.component';
import { ProfileComponent } from "./pages/profile/profile.component";
import { QuizTeacherComponent } from './pages/quiz-teacher/quiz-teacher.component';
import { QuizzesTeacherComponent } from "./pages/quizzes-teacher/quizzes-teacher.component";
import { EditQuizTeacherComponent } from './pages/edit-quiz-teacher/edit-quiz-teacher.component';
import { QuizStudentComponent } from './pages/quiz-student/quiz-student.component';
import { ClassroomStudentComponent } from './pages/classroom-student/classroom-student.component';
import { ResultsStudentComponent } from './pages/results-student/results-student.component';
import { ClassroomsTeacherComponent } from "./pages/classrooms-teacher/classrooms-teacher.component";
import { ResultsTeacherComponent } from './pages/results-teacher/results-teacher.component';
import { AuthGuard } from './guards/auth.guard';
import { AuthTeacherGuard } from './guards/auth-teacher.guard';
import { AuthStudentGuard } from './guards/auth-student.guard';
import {AnalyseZipComponent} from "./pages/analyse-zip/analyse-zip.component";

const routes: Routes = [
  {
    path: '',
    redirectTo: 'home',
    pathMatch: 'full'
  },
  {
    path: 'home',
    component: HomeComponent
  },
  {
    path: 'register',
    component: RegisterComponent
  },
  {
    path: 'login',
    component: LoginComponent
  },
  {
    path: 'profile',
    component: ProfileComponent,
    canActivate: [AuthGuard]
  },
  {
    path: 'quizTeacher',
    component: QuizTeacherComponent,
    canActivate: [AuthGuard, AuthTeacherGuard]
  },
  {
    path: 'analyseSubmissions',
    component: AnalyseZipComponent,
    canActivate: [AuthGuard, AuthTeacherGuard]
  },
  {
    path: 'quizzes',
    component: QuizzesTeacherComponent,
    canActivate: [AuthGuard]
  },
  {
    path: 'editQuizTeacher/:id',
    component: EditQuizTeacherComponent,
    canActivate: [AuthGuard, AuthTeacherGuard]
  },
  {
    path: 'takeQuiz/:id',
    component: QuizStudentComponent,
    canActivate: [AuthGuard, AuthStudentGuard]
  },
  {
    path: 'classroomStudent',
    component: ClassroomStudentComponent,
    canActivate: [AuthGuard, AuthStudentGuard]
  },
  {
    path: 'resultsStudent',
    component: ResultsStudentComponent,
    canActivate: [AuthGuard, AuthStudentGuard]
  },
  {
    path: 'teachersStudent',
    component: ClassroomStudentComponent,
    canActivate: [AuthGuard, AuthStudentGuard]
  },
  {
    path: 'teacherClassrooms',
    component: ClassroomsTeacherComponent,
    canActivate: [AuthGuard, AuthTeacherGuard]
  },
  {
    path: 'resultsTeacher',
    component: ResultsTeacherComponent,
    canActivate: [AuthGuard, AuthTeacherGuard]
  },
  {
    path: '**',
    redirectTo: 'home',
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
