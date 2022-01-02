import { Injectable, Logger } from '@nestjs/common';
import { Cron, CronExpression } from '@nestjs/schedule';
import { TwitchLogin } from './twitch-login.interface';
import secrets from '../../secrets.json';
import { HttpService } from '@nestjs/axios';
import { map } from 'rxjs';

@Injectable()
export class TwitchLoginService {
    public twitchLoginData: TwitchLogin;
    private readonly logger = new Logger(TwitchLoginService.name);
    private readonly clientId: string = secrets.twitch.clientId;
    private readonly clientSecret: string = secrets.twitch.secret;

    public constructor(private httpService: HttpService) {}

    @Cron(CronExpression.EVERY_10_SECONDS) // every 60 seconds.
    public loginToTwitch() {
        const data: any = this.httpService.get(`https://id.twitch.tv/oauth2/token?client_id=${this.clientId}&client_secret=${this.clientSecret}&grant_type=client_credentials`)
            .pipe(map(response => response.data));

        this.twitchLoginData = data as TwitchLogin;
        
        this.logger.debug('Logged into twitch, token expires in ' + this.twitchLoginData.expires_in);
    }
}
