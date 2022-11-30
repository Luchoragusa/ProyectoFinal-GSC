import { Component, OnInit, ViewChild } from '@angular/core';
import { CategoryService } from 'src/app/services/category.service';
import { MatTableDataSource } from '@angular/material/table';
import { Category } from 'src/app/interfaces/category';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { EditComponent } from 'src/app/modules/category/edit/edit.component';
import { MatDialog } from '@angular/material/dialog';
import { DeleteComponent } from '../category/delete/delete.component';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  constructor(private cS:CategoryService, private dialog: MatDialog, private _authService:AuthService) {}

  displayedColumns: string[] = ['id', 'description', 'creationDate', 'actions'];
  dataSource !: MatTableDataSource<Category>;

  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;

  ngOnInit(): void {
    this.getCategories();
  }
  response: any;
  getCategories() {
    this.cS.getApiCategories().subscribe((categories: Category[]) => {

      this.dataSource = new MatTableDataSource(categories);
      this.dataSource.paginator = this.paginator;
      this.dataSource.sort = this.sort;
    });
  }

  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();

    if (this.dataSource.paginator) {
      this.dataSource.paginator.firstPage();
    }
  }

  editCategory(category: Category) {
    this.dialog.open(EditComponent, {
      data: category
    }).afterClosed().subscribe(() => {
      this.getCategories();
    });
  }

  deleteCategory(category: Category) {
    this.dialog.open(DeleteComponent, {
      data: category
    }).afterClosed().subscribe(() => {
      this.getCategories();
    });
  }

  checkRole() {
    return !this._authService.checkIsAdmin();
  }
}
