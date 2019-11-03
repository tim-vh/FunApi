import { Injectable } from '@angular/core';
import { CanActivate, Router, ActivatedRouteSnapshot } from '@angular/router';

@Injectable()
export class MediaDetailsGuardService implements CanActivate {

    constructor() { }

    canActivate(route: ActivatedRouteSnapshot): boolean {
        const fileName = route.paramMap.get('fileName');

        if (fileName === '6.mp4') {            
            return true;
        }
        
        return true;
    }
}