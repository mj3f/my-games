import { Injectable } from '@nestjs/common';
import { UserDto } from './dtos/user-dto.model';

@Injectable()
export class UsersService {
    private readonly users: UserDto[] = [
        new UserDto('test1'),
        new UserDto('test-user2'),
        new UserDto('dynamite69')
    ];


    public getAll(): UserDto[] {
        return this.users;
    }

    public getById(id: string): UserDto {
        return this.users.find(u => u.username === id);
    }

    public getUserGames(id: string): string[] {
        return this.users.find(u => u.username === id).games;
    }
}
