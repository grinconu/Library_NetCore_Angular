import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Author } from '../Models/Author';

@Injectable({
  providedIn: 'root'
})
export class AuthorService {

  AP_URI = 'https://localhost:5002/api';

  constructor(private http: HttpClient ) { }

  getAuthors() {
    return this.http.get(`${this.AP_URI}/Author`);
  }

  getAuthor(id: string) {
    return this.http.get(`${this.AP_URI}/Author/${id}`);
  }

  saveAuthor(category: Author) {
    return this.http.post(`${this.AP_URI}/Author`, category);
  }

  deleteAuthor(id: string) {
    return this.http.delete(`${this.AP_URI}/Author/${id}`);
  }

  updateAuthor(id: string, category: Author) {
    return this.http.put(`${this.AP_URI}/Author/${id}`, category);
  }
}
