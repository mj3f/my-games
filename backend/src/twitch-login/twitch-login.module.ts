import { HttpModule } from '@nestjs/axios';
import { Module } from '@nestjs/common';
import { TwitchLoginService } from './twitch-login.service';

@Module({
    imports: [HttpModule],
    providers: [TwitchLoginService],
    exports: [TwitchLoginService]
})
export class TwitchLoginModule {}
