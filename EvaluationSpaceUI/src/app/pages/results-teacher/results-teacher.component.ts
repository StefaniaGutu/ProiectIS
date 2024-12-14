import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { ResultTeacher } from 'src/app/models/resultTeacher';
import { TeacherService } from 'src/app/services/teacher.service';


@Component({
  selector: 'app-results-teacher',
  templateUrl: './results-teacher.component.html',
  styleUrls: ['./results-teacher.component.scss']
})
export class ResultsTeacherComponent implements AfterViewInit {
  displayedColumns: string[] = ['quizTitle', 'studentName', 'score', 'totalScore', 'className'];
  dataSource!: MatTableDataSource<ResultTeacher>;

  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;

  constructor(private teacherService: TeacherService) {
  }

  ngAfterViewInit() {
    this.teacherService.GetResultsStatistics().subscribe(res => {
      this.dataSource = new MatTableDataSource<ResultTeacher>(res);
      this.dataSource.paginator = this.paginator;
      this.dataSource.sort = this.sort;
    })
  }

  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();

    if (this.dataSource.paginator) {
      this.dataSource.paginator.firstPage();
    }
  }

}
