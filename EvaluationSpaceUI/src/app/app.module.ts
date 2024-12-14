import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NavbarComponent } from './components/navbar/navbar.component';
import { MatSidenavModule } from '@angular/material/sidenav';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatButtonModule } from '@angular/material/button';
import { HomeComponent } from './pages/home/home.component';
import { RegisterComponent } from './pages/register/register.component';
import { MatSelectModule } from '@angular/material/select';
import { LoginComponent } from './pages/login/login.component';
import { ProfileComponent } from './pages/profile/profile.component';
import { MatInputModule } from "@angular/material/input";
import { DialogComponent } from './components/dialog/dialog.component';
import { MatDialogModule } from '@angular/material/dialog';
import { HTTP_INTERCEPTORS, HttpClientModule } from "@angular/common/http";
import { AuthConfigInterceptor } from "./services/auth-config.interceptor";
import { AuthService } from "./services/auth.service";
import { RegisterService } from "./services/register.service";
import { ProfileService } from "./services/profile.service";
import { QuizTeacherComponent } from './pages/quiz-teacher/quiz-teacher.component';
import { MatCardModule } from '@angular/material/card';
import { MatSlideToggleModule } from '@angular/material/slide-toggle';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatRadioModule } from '@angular/material/radio';
import { QuizzesTeacherComponent } from './pages/quizzes-teacher/quizzes-teacher.component';
import { DatePipe } from "@angular/common";
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { EditQuizTeacherComponent } from './pages/edit-quiz-teacher/edit-quiz-teacher.component';
import { QuizStudentComponent } from './pages/quiz-student/quiz-student.component';
import { ClassroomStudentComponent } from './pages/classroom-student/classroom-student.component';
import { MatListModule } from '@angular/material/list';
import { MatIconModule } from '@angular/material/icon';
import { ResultsStudentComponent } from './pages/results-student/results-student.component';
import { MatTableModule } from '@angular/material/table';
import { MatPaginatorModule } from '@angular/material/paginator';
import { ClassroomsTeacherComponent } from './pages/classrooms-teacher/classrooms-teacher.component';
import { MatExpansionModule } from "@angular/material/expansion";
import { DialogAddStudentComponent } from './components/dialog-add-student/dialog-add-student.component';
import { ResultsTeacherComponent } from './pages/results-teacher/results-teacher.component';
import { MatSortModule } from '@angular/material/sort';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';

@NgModule({
  declarations: [
    AppComponent,
    NavbarComponent,
    HomeComponent,
    RegisterComponent,
    LoginComponent,
    ProfileComponent,
    DialogComponent,
    QuizTeacherComponent,
    QuizzesTeacherComponent,
    EditQuizTeacherComponent,
    QuizStudentComponent,
    ClassroomStudentComponent,
    ResultsStudentComponent,
    ClassroomsTeacherComponent,
    DialogAddStudentComponent,
    ResultsTeacherComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    MatSidenavModule,
    BrowserAnimationsModule,
    MatButtonModule,
    FormsModule,
    ReactiveFormsModule,
    MatSelectModule,
    MatInputModule,
    MatDialogModule,
    HttpClientModule,
    MatCardModule,
    MatSlideToggleModule,
    MatCheckboxModule,
    MatRadioModule,
    MatSnackBarModule,
    MatListModule,
    MatIconModule,
    MatTableModule,
    MatPaginatorModule,
    MatExpansionModule,
    MatSortModule,
    MatProgressSpinnerModule
  ],
  providers: [
    {
      provide: HTTP_INTERCEPTORS,
      useClass: AuthConfigInterceptor,
      multi: true
    },
    AuthService,
    RegisterService,
    ProfileService,
    DatePipe
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
