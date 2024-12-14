import {Component, Inject, OnInit} from '@angular/core';
import {MatDialogRef, MAT_DIALOG_DATA} from "@angular/material/dialog";
import {ViewUser} from "../../models/view-user";
import {ClassroomTeacher} from "../../models/classroomTeacher";
import {MatCheckboxChange} from "@angular/material/checkbox";
import {TeacherService} from "../../services/teacher.service";
import {EditClassroom} from "../../models/editClassroom";
import {Guid} from "guid-typescript";

@Component({
  selector: 'app-dialog-add-student',
  templateUrl: './dialog-add-student.component.html',
  styleUrls: ['./dialog-add-student.component.scss']
})
export class DialogAddStudentComponent implements OnInit {
  public studentsWithoutClassrooms : ViewUser[] = [];
  public studentsAdded : ViewUser[] = [];
  editClassroom : EditClassroom = new EditClassroom(Guid.createEmpty(),'',[]);

  constructor(public dialogRef: MatDialogRef<DialogAddStudentComponent>,
              @Inject(MAT_DIALOG_DATA) public data: ClassroomTeacher,private teacherService: TeacherService) { }

  ngOnInit(): void {
    this.teacherService.GetStudentsWithoutClassrooms().subscribe(result => {
      this.studentsWithoutClassrooms = result;
    })
  }

  putAnswerMC(event: any, j: number) {
    let students = this.studentsAdded;
    if (event instanceof MatCheckboxChange) {
      if (event.checked) {
        this.studentsAdded.push(this.studentsWithoutClassrooms[j]);
      }
      else {
        students = students.filter(a => a.id !== this.studentsWithoutClassrooms[j].id);
        this.studentsAdded = students;
      }
    }
  }

  onNoClick() : void {
    this.dialogRef.close();
  }

  addStudents() {
    this.studentsAdded.forEach((student: ViewUser) => {
      this.data.students.push(student);
    })
    this.editClassroom.id = this.data.id;
    this.editClassroom.name = this.data.name;
    this.data.students.forEach((student: ViewUser) => {
      this.editClassroom.studentIds.push(student.id!);
    })
    this.teacherService.editClassroom(this.editClassroom).subscribe();
  }
}
