import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Category } from '../interfaces/category';

@Injectable({
  providedIn: 'root'
})
export class CategoryService {

  constructor(private _http: HttpClient) { }

  getApiCategories() {
    return this._http.get<Category[]>(environment.url + 'category');
  }
  
  getOneCategory(description: string) {
    return  this._http.get<Category>(environment.url + 'category/' + description);      
  }

  createCategory(category: Category) {
    return this._http.post<Category>(environment.url + 'category', category);
  }

  editCategory(category: Category) {
    return this._http.put(environment.url + `category/${category.id}`, category);
  }

  deleteCategory(category: Category) {
    return this._http.delete(environment.url + `category/${category.id}`);
  }
}
