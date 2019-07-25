import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Book } from '../Models/Book';

@Injectable({
  providedIn: 'root'
})
export class BookService {

  AP_URI = 'https://localhost:5002/api';

  constructor(private http: HttpClient ) { }

  getBooks() {
    return this.http.get(`${this.AP_URI}/Book`);
  }

  getBook(id: string) {
    return this.http.get(`${this.AP_URI}/Book/${id}`);
  }

  saveBook(category: Book) {
    return this.http.post(`${this.AP_URI}/Book`, category);
  }

  deleteBook(id: string) {
    return this.http.delete(`${this.AP_URI}/Book/${id}`);
  }

  updateBook(id: string, category: Book) {
    return this.http.put(`${this.AP_URI}/Book/${id}`, category);
  }
}
