import { Controller, Get, HttpCode, NotFoundException, Param } from '@nestjs/common';
import { UserDto } from './dtos/user-dto.model';
import { UsersService } from './users.service';

@Controller('users')
export class UsersController {

    public constructor(private usersService: UsersService) {}

    @Get()
    @HttpCode(200)
    public getUsers(): UserDto[] {
        return this.usersService.getAll();
    }

    @Get(':id')
    @HttpCode(200)
    @HttpCode(404)
    public getUserById(@Param('id') id: string): UserDto {
        const user: UserDto = this.usersService.getById(id);
        if (!user) {
            throw new NotFoundException(`No user with id ${id} found`);
        }

        return user;
    }

    @Get(':id/games')
    @HttpCode(200)
    @HttpCode(404)
    public getUserGames(@Param('id') id: string): string[] {
        const user: UserDto = this.usersService.getById(id);
        if (!user) {
            throw new NotFoundException(`No user with id ${id} found`);
        }

        return this.usersService.getUserGames(id);
    }
}
