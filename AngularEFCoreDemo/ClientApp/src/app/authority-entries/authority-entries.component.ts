import { Component } from '@angular/core';
import { AuthorityEntry, AuthorityEntriesClient, BookEditionsClient, BookEdition, PeopleClient, Person } from '../clients';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'authority-entries',
  templateUrl: './authority-entries.component.html',
  styles: ['td { white-space: nowrap; }'],
})
export class AuthorityEntriesComponent {
  public showDeleted: boolean;
  authorityEntries: AuthorityEntry[];
  bookEditions: BookEdition[];
  people: Person[];

  constructor(private client: AuthorityEntriesClient, private bookEditionsClient: BookEditionsClient, private peopleClient: PeopleClient, private route: ActivatedRoute) {
    this.get();
    bookEditionsClient.getAll().subscribe(
      result => this.bookEditions = result,
      error => console.error(error));
    peopleClient.getAll().subscribe(
      result => this.people = result,
      error => console.error(error));
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
		if (this.authorityEntries) {
		  return this.authorityEntries.filter(authorityEntry => this.showDeleted || !authorityEntry.isDeleted);
		}
  }
}
