import { HttpModule } from '@nestjs/axios';
import { Module } from '@nestjs/common';
import { MongooseModule } from '@nestjs/mongoose';
import { TwitchLoginModule } from 'src/twitch-login/twitch-login.module';
import { GamesController } from './games.controller';
import { GamesService } from './games.service';
import { Game, GameSchema } from './schemas/game.schema';

@Module({
    imports: [
        MongooseModule.forFeature([{ name: Game.name, schema: GameSchema }]),
        TwitchLoginModule,
        HttpModule
    ],
    controllers: [GamesController],
    providers: [GamesService]
})
export class GamesModule {}
