import { Module } from '@nestjs/common';
import { AppController } from './app.controller';
import { AppService } from './app.service';
import { UsersModule } from './users/users.module';
import { GamesModule } from './games/games.module';
import { AuthModule } from './auth/auth.module';
import { MongooseModule } from '@nestjs/mongoose';
import { ScheduleModule } from '@nestjs/schedule';
import { BackgroundTasksService } from './background-tasks/background-tasks.service';

const mongoPassword = '';

@Module({
  imports: [
    MongooseModule.forRoot(`mongodb+srv://admin:${mongoPassword}@cluster0.pqof2.mongodb.net/myFirstDatabase?retryWrites=true&w=majority`),
    UsersModule,
    GamesModule,
    AuthModule,
    ScheduleModule.forRoot(),
  ],
  controllers: [AppController],
  providers: [AppService, BackgroundTasksService],
})
export class AppModule {}
