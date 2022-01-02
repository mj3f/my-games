import { Module } from '@nestjs/common';
import { AppController } from './app.controller';
import { AppService } from './app.service';
import { UsersModule } from './users/users.module';
import { GamesModule } from './games/games.module';
import { AuthModule } from './auth/auth.module';
import { MongooseModule } from '@nestjs/mongoose';
import { ScheduleModule } from '@nestjs/schedule';
import { HttpModule } from '@nestjs/axios';
import { TwitchLoginService } from './twitch-login/twitch-login.service';
import { ConfigModule } from '@nestjs/config';

const mongoPassword = 'How to get this from configService in app module??';

@Module({
  imports: [
    MongooseModule.forRoot(`mongodb+srv://admin:${mongoPassword}@cluster0.pqof2.mongodb.net/myFirstDatabase?retryWrites=true&w=majority`),
    UsersModule,
    GamesModule,
    AuthModule,
    HttpModule,
    ScheduleModule.forRoot(),
    ConfigModule.forRoot({ envFilePath: '.env.local', isGlobal: true }),
  ],
  controllers: [AppController],
  providers: [AppService, TwitchLoginService],
})
export class AppModule {}
