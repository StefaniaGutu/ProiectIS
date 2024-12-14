import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import { MatDialog } from "@angular/material/dialog";
import { DialogComponent } from "../../components/dialog/dialog.component";
import { ProfileService } from "../../services/profile.service";
import { ViewUser } from "../../models/view-user";
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.scss']
})
export class ProfileComponent implements OnInit {
  public profileForm!: FormGroup;
  public isSaveDisabled: boolean = true;
  public messageDelete: string = 'Are you sure you want to delete your profile?';
  public profileUser?: ViewUser;
  public errorMessage: string | boolean = false;

  constructor(private fromBuilder: FormBuilder, public dialog: MatDialog, private profileService: ProfileService, private authService: AuthService) { }

  ngOnInit(): void {
    this.profileForm = this.fromBuilder.group({
      firstName: ['', [Validators.required]],
      lastName: ['', [Validators.required]],
      email: [''],
      classroom: [''],
      role: ['']
    })
    this.profileService.getProfile().subscribe(result => {
      this.profileUser = result;
      this.profileForm.controls['firstName'].setValue(this.profileUser.firstName);
      this.profileForm.controls['lastName'].setValue(this.profileUser.lastName);
      this.profileForm.controls['email'].setValue(this.profileUser.email);
      this.profileForm.controls['classroom'].setValue(this.profileUser.studentClassroom);
      if (this.profileUser.studentClassroom !== null) {
        this.profileForm.controls['role'].setValue('Student');
      }
      else {
        this.profileForm.controls['role'].setValue('Teacher');
      }
    })
    this.profileForm.disable();
  }

  editField(formField: any) {
    formField.enable();
    this.isSaveDisabled = false;
  }

  saveProfile() {
    this.profileService.editProfile({
      firstName: this.profileForm.controls['firstName'].value,
      lastName: this.profileForm.controls['lastName'].value,
      email: this.profileForm.controls['email'].value,
      studentClassroom: this.profileForm.controls['classroom'].value,
    }).subscribe(result => {
      this.profileForm.disable();
    }, error => {
      this.errorMessage = error.error;
    })
  }

  openDialog() {
    const dialogRef = this.dialog.open(DialogComponent, {
      data: this.messageDelete,
      height: '150px',
      width: '350px'
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result === 'Confirm') {
        this.profileService.deleteProfile().subscribe(result => {
          this.authService.logout();
        })
      }
    });

  }
}
