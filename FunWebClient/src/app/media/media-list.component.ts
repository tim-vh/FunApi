import { Component, OnInit } from '@angular/core';
import { FunApiProvider } from '../providers/fun-api.provider';

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
}