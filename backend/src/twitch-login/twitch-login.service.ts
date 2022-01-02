import { Injectable, Logger } from '@nestjs/common';
import { Cron, CronExpression } from '@nestjs/schedule';
import { TwitchLogin } from './twitch-login.interface';
import { HttpService } from '@nestjs/axios';
import { lastValueFrom, map, tap } from 'rxjs';
import { ConfigService } from '@nestjs/config';

@Injectable()
export class TwitchLoginService {
    public twitchLoginData: TwitchLogin;
    private readonly logger = new Logger(TwitchLoginService.name);
    private readonly clientId: string;
    private readonly clientSecret: string;

    public constructor(
        private httpService: HttpService,
        configService: ConfigService) {
            this.clientId = configService.get<string>('TWITCH_CLIENT_ID');
            this.clientSecret = configService.get<string>('TWITCH_CLIENT_SECRET');
        }

    @Cron(CronExpression.EVERY_10_SECONDS) // every 60 seconds.
    public async loginToTwitch() {
        const data: any = await lastValueFrom(
            this.httpService.post(`https://id.twitch.tv/oauth2/token?client_id=${this.clientId}&client_secret=${this.clientSecret}&grant_type=client_credentials`)
            .pipe(
                map(response => response.data)
            )
        );

        this.twitchLoginData = data as TwitchLogin;

        // this.logger.debug(JSON.stringify(data));
        
        this.logger.debug('Logged into twitch, token expires in ' + this.twitchLoginData.expires_in);
    }
}
