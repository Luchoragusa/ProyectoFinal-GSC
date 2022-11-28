import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { Category } from 'src/app/interfaces/category';
import { CategoryService } from 'src/app/services/category.service';
import { ErrorComponent } from '../error/error.component';

@Component({
  selector: 'app-create',
  templateUrl: './create.component.html',
  styleUrls: ['./create.component.css']
})
export class CreateComponent implements OnInit {
  @Output() refresh = new EventEmitter<void>();
  categoryForm !: FormGroup;

  constructor(
    private formBuilder: FormBuilder,
    private dialog: MatDialog,
    private cS:CategoryService ) { }

   fecha = new Date();

  ngOnInit(): void {
    this.categoryForm = this.formBuilder.group({
      description: ['', Validators.required],
      creationDate: [this.fecha, Validators.required]
    });
  }

  createCategory(){
    this.cS.getOneCategory(this.categoryForm.value.description).subscribe({
      next: (category: Category) => { // this category aleady exists
        this.dialog.open(ErrorComponent);
      },
    error: (err) => { // this category does not exist
      this.cS.createCategory(this.categoryForm.value).subscribe((response: any) => {
        });
      }
    });  
  }
}
