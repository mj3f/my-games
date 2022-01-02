import { Body, Controller, Get, HttpCode, NotFoundException, Param, Post, Put } from '@nestjs/common';
import { AddGameDto } from './dtos/add-game-dto.model';
import { UserDto } from './dtos/user-dto.model';
import { UserWithPasswordDto } from './dtos/user-with-password-dto.model';
import { User } from './schemas/user.schema';
import { UsersService } from './users.service';

@Controller('api/v0/users')
export class UsersController {

    public constructor(private usersService: UsersService) {}

    @Get()
    @HttpCode(200)
    public async getUsers(): Promise<User[]> {
        return await this.usersService.findAll();
    }

    @Post()
    @HttpCode(200)
    public async createUser(@Body() userWithPassword: UserWithPasswordDto): Promise<string> {
        await this.usersService.createUser(userWithPassword);
        return 'user created';
    }

    @Get(':username')
    @HttpCode(200)
    @HttpCode(404)
    public async getUserById(@Param('username') username: string): Promise<User> {
        const user: User = await this.usersService.findByUsername(username);
        if (!user) {
            throw new NotFoundException(`No user with username ${username} found`);
        }

        return user;
    }

    @Put(':id/add-game')
    @HttpCode(200)
    public async addGameToUsersLibrary(@Param('id') id: string, @Body() addGame: AddGameDto): Promise<string> {
        await this.usersService.addGameToUsersLibrary(addGame);
        return 'game added, (I think?)';
    }
}
