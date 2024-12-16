import {Injectable} from '@angular/core';
import {HttpClient} from "@angular/common/http";

@Injectable({
  providedIn: 'root'
})
export class AnalyseZipService {

  constructor(private http: HttpClient) { }

  analyseZip(zipDetails: FormData){
    return this.http.post("basePath/Reports/UpoloadZip", zipDetails, {
      observe: 'response',
      responseType: 'text' as 'json'
    })
  }
}
