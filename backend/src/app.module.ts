import { Module } from '@nestjs/common';
import { AppController } from './app.controller';
import { AppService } from './app.service';
import { UsersModule } from './users/users.module';
import { GamesModule } from './games/games.module';
import { AuthModule } from './auth/auth.module';
import { MongooseModule } from '@nestjs/mongoose';
import { ScheduleModule } from '@nestjs/schedule';
import { HttpModule } from '@nestjs/axios';
import { ConfigModule } from '@nestjs/config';
import { TwitchLoginModule } from './twitch-login/twitch-login.module';

const mongoPassword = '';

@Module({
  imports: [
    MongooseModule.forRoot(`mongodb+srv://admin:${mongoPassword}@cluster0.pqof2.mongodb.net/myFirstDatabase?retryWrites=true&w=majority`),
    UsersModule,
    GamesModule,
    AuthModule,
    ScheduleModule.forRoot(),
    ConfigModule.forRoot({ envFilePath: '.env.local', isGlobal: true }),
    TwitchLoginModule,
  ],
  controllers: [AppController],
  providers: [AppService],
})
export class AppModule {}
