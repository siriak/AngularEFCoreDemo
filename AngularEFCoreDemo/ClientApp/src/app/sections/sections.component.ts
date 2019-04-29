import { Component, Input } from '@angular/core';
import { SectionsClient, Section } from '../clients';
import { ActivatedRoute } from '@angular/router';
import { MatDialog } from '@angular/material';
import { ConfirmDialogComponent } from '../confirm-dialog/confirm-dialog.component';

@Component({
  selector: 'sections',
  templateUrl: './sections.component.html',
  styles: ['td { white-space: nowrap; }'],
})
export class SectionsComponent {
  public showDeleted: boolean;
  sections: Section[];

  constructor(
    private client: SectionsClient,
    private route: ActivatedRoute,
    private dialog: MatDialog,
    ) {
    this.get();
  }

  update() {
    this.client.put(this.sections).subscribe(
      () => {},
      () => {},
      () => this.get());
  }

  get() {
    this.route.params.subscribe(p => {
      const id = p['id'];
      if (id) {
        this.client.get(id).subscribe(
          result => this.sections = [result],
          error => console.error(error));
      } else {
        this.client.getAll().subscribe(
          result => this.sections = result,
          error => console.error(error));
      }
    });
  }

  getList() {
    if (this.sections) {
	  return this.sections.filter(book => this.showDeleted || !book.isDeleted);
    }
  }

  entryDeleted(isDeleted: boolean, section: Section) {
		if (isDeleted) {
      const dialogRef = this.dialog.open(ConfirmDialogComponent);
      dialogRef.afterClosed().subscribe(res => {
        if (res !== 'yes') {
          section.isDeleted = false;
        }
      });
		}
  }
}
