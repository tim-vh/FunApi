import { Component, OnInit, Input } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { FunApiProvider } from '../providers/fun-api.provider';

@Component({
    templateUrl: './media-details.component.html'
  })
  export class MediaDetails implements OnInit {
    
    @Input() public fileName: string;

    constructor(private route: ActivatedRoute, private funApiProvider: FunApiProvider) {
        let fileName = this.route.snapshot.paramMap.get('fileName');
        
        if(fileName && fileName.length > 0) {
            this.fileName = fileName;
        }
    }

    ngOnInit(): void {
    }

    public play() : void {
        this.funApiProvider.playMedia(this.fileName);
    }
  }