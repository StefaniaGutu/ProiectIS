import { Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { Result } from 'src/app/models/result';
import { StudentService } from 'src/app/services/student.service';

@Component({
  selector: 'app-results-student',
  templateUrl: './results-student.component.html',
  styleUrls: ['./results-student.component.scss']
})
export class ResultsStudentComponent implements OnInit {

  displayedColumns: string[] = ['QuizTitle', 'Teacher', 'Score', 'PossibleScore'];
  dataSource = new MatTableDataSource<Result>();

  @ViewChild(MatPaginator) paginator!: MatPaginator;

  constructor(private studentClassroomService: StudentService) { }

  ngOnInit(): void {
    this.studentClassroomService.getStudentResults().subscribe(res => {
      this.dataSource = new MatTableDataSource<Result>(res);
      this.dataSource.paginator = this.paginator;
    })
  }

}