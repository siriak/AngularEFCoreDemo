import { Component } from '@angular/core';
import { AuthorityEntry, AuthorityEntriesClient } from '../clients';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'authority-entries',
  templateUrl: './authority-entries.component.html'
})
export class AuthorityEntriesComponent {
  public showDeleted: boolean;
  authorityEntries: AuthorityEntry[];

  constructor(private client: AuthorityEntriesClient, private route: ActivatedRoute) {
    this.get();
  }

  update() {
    this.client.put(this.authorityEntries).subscribe(
      () => {},
      () => {},
      () => this.get());
  }

  get() {
    this.route.params.subscribe(p => {
      const id = p['id'];
      if (id) {
        this.client.get(id).subscribe(
          result => this.authorityEntries = [result],
          error => console.error(error));
      } else {
        this.client.getAll().subscribe(
          result => this.authorityEntries = result,
          error => console.error(error));
      }
    });
  }

  getList() {
    return this.authorityEntries.filter(authorityEntry => this.showDeleted || !authorityEntry.isDeleted)
  }
}
