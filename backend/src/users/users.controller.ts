import { Body, Controller, Get, HttpCode, NotFoundException, Param, Post } from '@nestjs/common';
import { UserDto } from './dtos/user-dto.model';
import { UserWithPasswordDto } from './dtos/user-with-password-dto.model';
import { User } from './schemas/user.schema';
import { UsersService } from './users.service';

@Controller('users')
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
}
