import { HttpService } from '@nestjs/axios';
import { Injectable } from '@nestjs/common';
import { map, Observable } from 'rxjs';
import { TwitchLoginService } from 'src/twitch-login/twitch-login.service';

@Injectable()
export class GamesService {
    private readonly igdbEndpoint: string = 'https://api.igdb.com/v4/games/';

    constructor(
       private httpService: HttpService,
       private twitchLoginService: TwitchLoginService) {}

    public getGames(): Observable<any> {
       return this.httpService.get(this.igdbEndpoint, { headers: this.getHeaders() }).pipe(map(res => res.data));
    }

    public getGame(id: number): Observable<any> { // twitch game model needs implementing.
        return this.httpService.get(this.igdbEndpoint + `${id}?fields=name,cover,genres,platforms`, { headers: this.getHeaders() }).pipe(map(res => res.data));
    }

    private getHeaders(): any {
        return {
            'Client-ID': this.twitchLoginService.clientId,
            'Authorization': `Bearer ${this.twitchLoginService.twitchLoginData?.access_token}`
        };
    }
}
