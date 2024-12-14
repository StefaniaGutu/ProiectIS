import { Component, OnInit } from '@angular/core';
import {ClassroomTeacher} from "../../models/classroomTeacher";
import {TeacherService} from "../../services/teacher.service";
import {Guid} from "guid-typescript";
import {DialogComponent} from "../../components/dialog/dialog.component";
import {MatDialog} from "@angular/material/dialog";
import {MatSnackBar} from "@angular/material/snack-bar";
import {EditClassroom} from "../../models/editClassroom";
import {ViewUser} from "../../models/view-user";
import {DialogAddStudentComponent} from "../../components/dialog-add-student/dialog-add-student.component";

@Component({
  selector: 'app-classrooms-teacher',
  templateUrl: './classrooms-teacher.component.html',
  styleUrls: ['./classrooms-teacher.component.scss']
})
export class ClassroomsTeacherComponent implements OnInit {
  teacherClassrooms! : ClassroomTeacher[];
  studentDeleted! : ViewUser;
  editClassroom : EditClassroom = new EditClassroom(Guid.createEmpty(),'',[]);
  public messageDelete: string = 'Are you sure you want to delete this student from this classroom?';

  constructor(private teacherService: TeacherService, public dialog: MatDialog, private _snackBar: MatSnackBar) { }

  ngOnInit(): void {
    this.teacherService.GetTeacherClassrooms().subscribe(res => {
      this.teacherClassrooms = res;
    })
  }

  openDialog(idClassroom: Guid,classroomName: string, idStudent: Guid) {
    const dialogRef = this.dialog.open(DialogComponent, {
      data: this.messageDelete,
      height: '150px',
      width: '350px'
    });

    dialogRef.afterClosed().subscribe((result: string) => {
      if (result === 'Confirm') {
        this.editClassroom.id = idClassroom;
        this.editClassroom.name = classroomName;
        this.editClassroom.studentIds = [];
        this.teacherClassrooms.forEach((classroom : ClassroomTeacher) => {
          if (classroom.id === idClassroom){
            classroom.students.forEach((student:ViewUser) => {
              if (student.id !== idStudent){
                this.editClassroom.studentIds.push(student.id!);
              }
              else{
                this.studentDeleted = student;
              }
            })
          }
        })
        this.teacherService.editClassroom(this.editClassroom).subscribe(result => {
          this.teacherClassrooms.forEach((classroom : ClassroomTeacher) => {
            if (classroom.id === idClassroom){
              classroom.students = classroom.students.filter(student => student.id != this.studentDeleted.id);
            }
          })
        })
      }
    });
  }

  onAddStudent(classroomTeacher: ClassroomTeacher) {
    const dialogRef = this.dialog.open(DialogAddStudentComponent, {
      data: classroomTeacher,
    });

    dialogRef.afterClosed().subscribe();
  }
}
