import { Controller, Get, HttpCode, Param } from '@nestjs/common';
import { GamesService } from './games.service';

@Controller('api/v0/games')
export class GamesController {

    constructor(private gamesService: GamesService) {}

    @Get()
    @HttpCode(200)
    public async getGamesFromIgdb(): Promise<any> {
        return await this.gamesService.getGames();
    }

    @Get(':id')
    @HttpCode(200)
    @HttpCode(404)
    public async getGameById(@Param('id') id: number): Promise<any> {
        return await this.gamesService.getGame(id);
    }

}
