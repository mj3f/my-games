import { Prop, Schema, SchemaFactory } from "@nestjs/mongoose";
import { Types, Document } from "mongoose";
import { Game } from "src/games/schemas/game.schema";

export type UserDocument = User & Document;

@Schema()
export class User {
    @Prop({ required: true })
    public username: string;

    @Prop()
    public password: string;

    @Prop({ type: [Types.ObjectId], ref: 'Game' })
    public games: Game[];
}

export const UserSchema = SchemaFactory.    createForClass(User);