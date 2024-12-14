import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ClassroomStudent } from 'src/app/models/classroomStudent';
import { StudentService } from 'src/app/services/student.service';


@Component({
  selector: 'app-classroom-student',
  templateUrl: './classroom-student.component.html',
  styleUrls: ['./classroom-student.component.scss']
})
export class ClassroomStudentComponent implements OnInit {

  colleaguesClassroom!: ClassroomStudent;
  routeUrl!: string;

  constructor(private studentClassroomService: StudentService, private router: Router) {
    this.routeUrl = router.url;
  }

  ngOnInit(): void {
    this.studentClassroomService.GetStudentClassrooms().subscribe(res => {
      this.colleaguesClassroom = res;
    })

  }

}
