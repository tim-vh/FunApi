import { Component, OnInit } from '@angular/core';
import { FunApiProvider } from '../providers/fun-api.provider';
import { Observable } from 'rxjs/internal/Observable';
import {debounceTime, distinctUntilChanged, map} from 'rxjs/operators';

@Component({
  selector: 'fun-media-list',
  templateUrl: './media-list.component.html'
})
export class MediaList implements OnInit {

  public files: string[];
  public fileFilter: string;

  constructor(private funApiProvider: FunApiProvider){
  }

  async ngOnInit(): Promise<void> {
    this.files = await this.funApiProvider.getFileNames();
  }

  public async play(fileName: string) : Promise<void> {
    await this.funApiProvider.playMedia(fileName);
  }

  public search = (text: Observable<string>) =>
    text.pipe(
      debounceTime(200),
      distinctUntilChanged(),
      map(term => term.length < 2 ? []
        : this.files.filter(v => v.toLowerCase().indexOf(term.toLowerCase()) > -1).slice(0, 10))
    )
}