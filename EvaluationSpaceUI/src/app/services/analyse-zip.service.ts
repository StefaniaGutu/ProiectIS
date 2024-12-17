import {Injectable} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import { firstValueFrom } from 'rxjs';
import { Similarity } from '../models/similarity';

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

  getSimilarityReport(reportId: string){
    return firstValueFrom( this.http.get("basePath/Reports/GetSimilarityReport?reportId=" + reportId, {
      observe: 'response',
      responseType: 'json'
    }))
  }
}
