import { Component, OnInit,Inject } from '@angular/core';
import {FormControl, FormGroup, FormBuilder, Validators} from '@angular/forms';
import { MatDialog, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Category } from 'src/app/interfaces/category';
import { CategoryService } from 'src/app/services/category.service';
import { AlertDialogComponent } from '../../shared/alert-dialog/alert-dialog.component';

@Component({
  selector: 'app-edit',
  templateUrl: './edit.component.html',
  styleUrls: ['./edit.component.css']
})
export class EditComponent implements OnInit {

  categoryForm !: FormGroup;

  constructor(
    @Inject (MAT_DIALOG_DATA) public data: Category, 
    private formBuilder: FormBuilder,
    private dialog: MatDialog,
    private cS:CategoryService ) { }

  ngOnInit(): void {
    this.categoryForm = this.formBuilder.group({
      id: [this.data.id],
      description: [this.data.description, Validators.required],
      creationDate: [this.data.creationDate, Validators.required]
    });
  }

  editCategory(){
    this.cS.getOneCategory(this.categoryForm.value.description).subscribe({
      next: (category: Category) => { // this category aleady exists
        if (category.id != this.categoryForm.value.id) {
          this.dialog.open(AlertDialogComponent, {
            data: {
              title: 'Error editing category',
              message: 'This category already exists'
            }
          })
        } else
        this.cS.editCategory(this.categoryForm.value).subscribe((response: any) => {});
      },
      error: (err) => { // this category does not exist
        this.cS.editCategory(this.categoryForm.value).subscribe((response: any) => {});
      }
    });    
  }
}
