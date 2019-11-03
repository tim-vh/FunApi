import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';

@Injectable()
export class FunApiProvider
{
    constructor(private http: HttpClient) {
    }

    private baseUrl: string = 'http://localhost:20014/api/';
    private headers: HttpHeaders = new HttpHeaders({'X-API-KEY':'my-secret-key'});

    public async getFileNames(): Promise<string[]> {
        return await this.http.get<string[]>(this.baseUrl + 'media/list', {headers: this.headers}).toPromise();
    }

    public async playMedia(fileName: string): Promise<void> {
        await this.http.get<any>(this.baseUrl + 'media/play/' + fileName, { headers: this.headers}).toPromise();
    }
}