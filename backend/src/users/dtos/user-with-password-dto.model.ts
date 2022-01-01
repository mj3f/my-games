import { UserDto } from "./user-dto.model";

export class UserWithPasswordDto extends UserDto {
    constructor(public username: string, public password: string) {
        super(username);
    }
}