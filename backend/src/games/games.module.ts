import { HttpModule } from '@nestjs/axios';
import { Module } from '@nestjs/common';
import { TwitchLoginModule } from 'src/twitch-login/twitch-login.module';
import { GamesController } from './games.controller';
import { GamesService } from './games.service';

@Module({
    imports: [
        TwitchLoginModule,
        HttpModule
    ],
    controllers: [GamesController],
    providers: [GamesService]
})
export class GamesModule {}
