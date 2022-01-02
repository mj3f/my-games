import { Injectable } from '@nestjs/common';
import { InjectModel } from '@nestjs/mongoose';
import { Model } from 'mongoose';
import { UserDto } from './dtos/user-dto.model';
import { UserWithPasswordDto } from './dtos/user-with-password-dto.model';
import { User, UserDocument } from './schemas/user.schema';

@Injectable()
export class UsersService {
    private readonly users: UserDto[] = [
        new UserDto('test1'),
        new UserDto('test-user2'),
        new UserDto('dynamite69')
    ];

    public constructor(@InjectModel(User.name) private userModel: Model<UserDocument>) {}

    // public getAll(): UserDto[] {
    //     return this.users;
    // }

    // public getById(id: string): UserDto {
    //     return this.users.find(u => u.username === id);
    // }

    // public getUserGames(id: string): string[] {
    //     return this.users.find(u => u.username === id).games;
    // }

    public findAll(): Promise<User[]> {
        return this.userModel.find().exec();
    }

    public findByUsername(name: string): Promise<User> {
        return this.userModel.findOne({ username: { $eq: name } }).exec();
    }

    public createUser(userWithPassword: UserWithPasswordDto): Promise<User> {
        const createdUser = new this.userModel(userWithPassword); // TODO: don't store password in plaintext!
        return createdUser.save();

    }
}
