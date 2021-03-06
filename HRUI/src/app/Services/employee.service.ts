import {observable} from '../../../node_modules/rxjs/src/internal/symbol/observable';
import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Employee } from '../Data_Types/employee';
import { map, filter, switchMap } from 'rxjs/operators';
import { environment } from 'src/environments/environment';


@Injectable({
  providedIn: 'root'
})
export class EmployeeService {

  constructor(private httpclient: HttpClient) { }
  httpHeader={headers: new HttpHeaders({
    'content-type':'application/json',
    'Accept': '*/*'
       
  })};
  

  getProfession()
  {
    const httpHeader={headers: new HttpHeaders({
      'content-type':'application/json',
      'Accept': '*/*'
         
    })};
    return this.httpclient.get(`${environment.ApiURL}/Professions`,httpHeader)
  }
  AddEmployee(emp:Employee)
  {
    //console.log(emp);
    const httpHeader={headers: new HttpHeaders({
      'content-type':'application/json',
      'Accept': '*/*'
         
    })};
    const httpOptions = {headers: new HttpHeaders({
      'Content-Type': 'application/json',
      //"Authorization": "bearer " + localStorage.getItem('token')
        })};
    return this.httpclient.post(`${environment.ApiURL}/Employees`,emp,httpOptions);
  }
  GetAllEmployees()
  {
    const httpOptions = {headers: new HttpHeaders({
      'Content-Type': 'application/json',
      //"Authorization": "bearer " + localStorage.getItem('token')
        })};
    return this.httpclient.get(`${environment.ApiURL}/Employees`,httpOptions);
  }
  GetEmployee (id): Observable<Employee>
  {
    const httpOptions = {headers: new HttpHeaders({
      'Content-Type': 'application/json',
      "Authorization": "bearer " + localStorage.getItem('token')
        })};
    return this.httpclient.get<Employee>(`${environment.ApiURL}/Employees/`+id,httpOptions);
  }
  UpdateEmployee(id,emp)
  {
    //console.log(emp);
    const httpHeader={headers: new HttpHeaders({
      'content-type':'application/json',
      'Accept': '*/*'
         
    })};
    const httpOptions = {headers: new HttpHeaders({
      'Content-Type': 'application/json',
      "Authorization": "bearer " + localStorage.getItem('token')
        })};
    return this.httpclient.put(`${environment.ApiURL}/Employees/`+id,emp,httpHeader);
  }

  postFile(fileToUpload: File): Observable<boolean> {
    const endpoint = `${environment.ApiURL}/Employees/api/dashboard/UploadImage`;
    const formData: FormData = new FormData();
    formData.append('file', fileToUpload, fileToUpload.name);
    return this.httpclient
      .post(endpoint, formData).pipe(
      map(() => { return true; }));
  }

  EmployeeByProfession()
  {
    const httpOptions = {headers: new HttpHeaders({
      'Content-Type': 'application/json',
      "Authorization": "bearer " + localStorage.getItem('token')
        })};
    return this.httpclient.get(`${environment.ApiURL}/Employees/EmployeeByProfession`,httpOptions);
  }
  GetAllEmployeesByProfession(id)
  {
    const httpOptions = {headers: new HttpHeaders({
      'Content-Type': 'application/json',
      "Authorization": "bearer " + localStorage.getItem('token')
        })};
    return this.httpclient.get(`${environment.ApiURL}/Employees/GetAllEmployeesByProfession/`+id,this.httpHeader);
  }
  delete(id)
  {
    const httpOptions = {headers: new HttpHeaders({
      'Content-Type': 'application/json',
      "Authorization": "bearer " + localStorage.getItem('token')
        })};
    return this.httpclient.delete(`${environment.ApiURL}/Employees/`+id,this.httpHeader);
  }
}




