import { Component, OnInit } from '@angular/core';
import { Http, Response } from '@angular/http';
import { DomSanitizer, SafeResourceUrl, SafeUrl } from '@angular/platform-browser';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/map';
import 'rxjs/Rx';
import 'rxjs/add/observable/of';
import { SearchResponse } from './model'

@Component({
  selector: 'my-app',
  templateUrl: 'app/app.component.html'
})
export class AppComponent implements OnInit  {
    public query: string;
    public items: Array<SearchResponse>
    public isLoadingVisible: boolean = false;

    constructor(private http: Http, private sanitizer: DomSanitizer) {

    }    

    public search(): void {
        this.isLoadingVisible = true;

        let url: string = encodeURI(`api/search?q=${this.query}`);

        this.http.get(url)
            .map((res: Response) => {
                this.isLoadingVisible = false;
                return this.handleSuccesfullResponse(res);
            }).catch((error: any) => {
                console.log(error);
                this.isLoadingVisible = false;
                return Observable.throw('an error has occurred');
            }).subscribe(x => { });
    }

    public ngOnInit(): void {

    }

    private handleSuccesfullResponse(res: Response): void {
        let body = res.json();
        let response: Array<SearchResponse> = (body || null) as Array<SearchResponse>;

        this.items = response;        
    }

    public getSanitizedUrl(url: string): SafeUrl {
       
        return this.sanitizer.bypassSecurityTrustResourceUrl(url);   
    }

    public fileProxy(url: string): string {
        return `api/file?f=${url}`;
    }
}
