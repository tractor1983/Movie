export class Video {
    public Url: string;
    public Iframe: string;
}

export class SearchResponse {   
    MovieTitle: string;
    Year: string;
    Rating: string;
    Released: string;
    PosterUrl: string;
    Videos: Array<Video>;        
}