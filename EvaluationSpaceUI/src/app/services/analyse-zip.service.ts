import { Injectable } from '@angular/core';
import {HttpClient, HttpResponse} from "@angular/common/http";
import {ZipDetails} from "../models/zipDetails";
import {Observable} from "rxjs";

@Injectable({
  providedIn: 'root'
})
export class AnalyseZipService {

  constructor(private http: HttpClient) { }

  analyseZip(zipDetails: ZipDetails){
    return this.http.post("UpoloadZip", {
      "zip" : zipDetails.zipFile,
      "name" : zipDetails.name,
      "programmingLanguage" : zipDetails.language
    }, {
      observe: 'response',
      responseType: 'text' as 'json'
    })
  }
}
