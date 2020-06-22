import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';


@Injectable({
    providedIn: 'root'
  })
export class ApiService {
  http: HttpClient;
  headers: HttpHeaders;

  constructor(http: HttpClient) {
    this.http = http;
    this.headers = new HttpHeaders();
  }
}
