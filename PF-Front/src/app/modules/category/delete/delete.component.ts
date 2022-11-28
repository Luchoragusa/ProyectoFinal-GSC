import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { MatDialog, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Category } from 'src/app/interfaces/category';
import { CategoryService } from 'src/app/services/category.service';

@Component({
  selector: 'app-delete',
  templateUrl: './delete.component.html',
  styleUrls: ['./delete.component.css']
})
export class DeleteComponent implements OnInit {

  constructor(
    @Inject (MAT_DIALOG_DATA) public data: Category, 
    private formBuilder: FormBuilder, 
    private cS:CategoryService ) { }

  ngOnInit(): void {
  }

  deleteCategory(){
    this.cS.deleteCategory(this.data).subscribe((response: any) => {
      console.log(response);
    });
  }
}
