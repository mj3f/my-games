import { Injectable, Logger } from '@nestjs/common';
import { Cron, CronExpression } from '@nestjs/schedule';

@Injectable()
export class BackgroundTasksService {
    private readonly logger = new Logger(BackgroundTasksService.name);

    @Cron(CronExpression.EVERY_10_SECONDS) // every 60 seconds.
    public loginToTwitch() {
        this.logger.debug('grigdijgidjfgidjfidjfidjfidjf');
    }
}
