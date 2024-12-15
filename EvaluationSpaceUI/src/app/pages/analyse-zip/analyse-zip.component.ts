import {Component, OnInit} from '@angular/core';
import {FormBuilder, FormControl, FormGroup, Validators} from "@angular/forms";
import {ViewUser} from "../../models/view-user";
import {MatDialog} from "@angular/material/dialog";
import {ProfileService} from "../../services/profile.service";
import {AuthService} from "../../services/auth.service";
import {Classroom} from "../../models/classroom";
import {AnalyseZipService} from "../../services/analyse-zip.service";
import {ZipDetails} from "../../models/zipDetails";

@Component({
  selector: 'app-analyse-zip',
  templateUrl: './analyse-zip.component.html',
  styleUrls: ['./analyse-zip.component.scss']
})
export class AnalyseZipComponent implements OnInit {
  public analysisForm!: FormGroup;
  public errorMessage: string | boolean = false;
  public allLanguages: string[] = ["C++", "Angular", "Java", "SQL", "React", "Python"];
  selectedFile: File | null = null;


  constructor(private analyseZipService: AnalyseZipService, private fromBuilder: FormBuilder, private authService: AuthService) {
  }

  ngOnInit(): void {
    this.analysisForm = this.fromBuilder.group({
      zipName: new FormControl([], [Validators.required]),
      language: new FormControl([], [Validators.required]),
      file: new FormControl([], [Validators.required]),
    })
  }

  onSubmit() {
    console.log("djhbgkjfn")
    this.analyseSubmissions();
  }

  onFileSelect(event: any): void {
    const file = event.target.files[0];
    const fileExtension = file.type.split('.').pop()?.toLowerCase(); // Get the extension and make it lowercase
    if (file && fileExtension.includes("zip")) {
      this.selectedFile = file;
      this.analysisForm.patchValue({ file });
    } else {
      this.selectedFile = null;
      this.analysisForm.patchValue({ file:null });
      alert('Please select a valid ZIP file.');
    }
  }

  analyseSubmissions(): void {
    console.log("jhgknjgn")
    if (!this.analysisForm.valid || !this.selectedFile) {
      console.log("aici")
      return;
    }

    let formData = this.analysisForm.value;
    let zipDetails = new ZipDetails(formData.file, formData.name, formData.language)
    console.log(zipDetails)
    this.analyseZipService.analyseZip(zipDetails).subscribe(res => {
      console.log(res)
    });
  }
}
