import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { Classroom } from "../../models/classroom";
import { RegisterService } from "../../services/register.service";

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent implements OnInit {
  public myForm!: FormGroup;
  public classrooms = new FormControl([], [Validators.required]);
  public allClassrooms: Classroom[] = [];
  public errorMessage: string | boolean = false;

  constructor(
    private router: Router,
    private fromBuilder: FormBuilder,
    private registerService: RegisterService) {
  }

  ngOnInit(): void {
    this.registerService.getClassrooms().subscribe(result => {
      this.allClassrooms = result;
    })
    this.myForm = this.fromBuilder.group({
      firstName: ['', [Validators.required]],
      lastName: ['', [Validators.required]],
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required, Validators.minLength(10)]],
      confirmPassword: ['', [Validators.required, Validators.minLength(10)]]
    })
  }

  doRegister() {
    this.registerService.doRegister({
      firstName: this.myForm.controls['firstName'].value,
      lastName: this.myForm.controls['lastName'].value,
      email: this.myForm.controls['email'].value,
      password: this.myForm.controls['password'].value,
      idRole: this.getCheckedValue() ? 2 : 1,
      classroomIds: this.getCheckedValue() ? [this.classrooms.value] : this.classrooms.value
    }).subscribe(response => {
      this.router.navigate(['/login']);
    }, error => {
      this.errorMessage = error.error;
    }
    )
  }

  getCheckedValue() {
    var radioValues = document.getElementsByName("flexRadioDefault");
    var student = radioValues[0] as HTMLInputElement;
    return student.checked;
  }

  resetCheckbox() {
    this.classrooms.reset();
  }

}
