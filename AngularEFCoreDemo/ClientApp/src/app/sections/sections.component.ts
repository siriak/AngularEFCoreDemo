import { Component } from '@angular/core';
import { SectionsClient, Section } from '../clients';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'sections',
  templateUrl: './sections.component.html'
})
export class SectionsComponent {
  public showDeleted: boolean;
  sections: Section[];

  constructor(private client: SectionsClient, private route: ActivatedRoute) {
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
    return this.sections.filter(book => this.showDeleted || !book.isDeleted)
  }
}
