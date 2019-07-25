import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Category } from '../Models/Category';

@Injectable({
  providedIn: 'root'
})
export class CategoryService {

  AP_URI = 'https://localhost:5002/api';

  constructor(private http: HttpClient ) { }

  getCategories() {
    return this.http.get(`${this.AP_URI}/Category`);
  }

  getCategory(id: string) {
    return this.http.get(`${this.AP_URI}/Category/${id}`);
  }

  saveCategory(category: Category) {
    return this.http.post(`${this.AP_URI}/Category`, category);
  }

  deleteCategory(id: string) {
    return this.http.delete(`${this.AP_URI}/Category/${id}`);
  }

  updateategory(id: string, category: Category) {
    return this.http.put(`${this.AP_URI}/Category/${id}`, category);
  }

}
