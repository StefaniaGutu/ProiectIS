import {Component, OnInit} from '@angular/core';
import {FormBuilder, FormControl, FormGroup, Validators} from "@angular/forms";
import {AuthService} from "../../services/auth.service";
import {AnalyseZipService} from "../../services/analyse-zip.service";
import { Similarity } from 'src/app/models/similarity';

@Component({
  selector: 'app-analyse-zip',
  templateUrl: './analyse-zip.component.html',
  styleUrls: ['./analyse-zip.component.scss']
})
export class AnalyseZipComponent implements OnInit {
  public analysisForm!: FormGroup;
  public errorMessage: string | boolean = false;
  public allLanguages = [
    { label: "C++", value: "cpp" },
    { label: "Angular", value: "angular" },
    { label: "Java", value: "java" },
    { label: "SQL", value: "sql" },
    { label: "React", value: "react" },
    { label: "Python", value: "python" }
  ];
  selectedFile: File | null = null;
  analysedFiles: Similarity[] = [];


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
    this.analyseSubmissions();
  }

  onFileSelect(event: any): void {
    const file = event.target.files[0];
    const fileExtension = file.type.split('.').pop()?.toLowerCase(); // Get the extension and make it lowercase
    if (file && fileExtension.includes("zip")) {
      this.selectedFile = file;
      this.analysisForm.patchValue({ file: file, zipName: file.name });
    } else {
      this.selectedFile = null;
      this.analysisForm.patchValue({ file:null });
      alert('Please select a valid ZIP file.');
    }
  }

  analyseSubmissions(): void {
    if (!this.analysisForm.valid || !this.selectedFile) {
      return;
    }

    const formData = new FormData();
    console.log(this.analysisForm)
    formData.append('zip', this.selectedFile, this.selectedFile?.name);
    formData.append('programmingLanguage', this.analysisForm.get('language')?.value);
    formData.append('name', this.analysisForm.get('zipName')?.value);

    this.analyseZipService.analyseZip(formData).subscribe(res => {
      if (res.body != null)
      this.analysedFiles = JSON.parse(res.body as string) as Similarity[];
      console.log(this.analysedFiles);
      var reportId = res.body;
      var dolosReportUrl = '';


    });
  }
}
