import { Prop, Schema, SchemaFactory } from "@nestjs/mongoose";
import { Document, Types } from "mongoose";
import { User } from "src/users/schemas/user.schema";

export type GameDocument = Game & Document;

@Schema()
export class Game {

    @Prop({ required: true })
    public igdbId: string;

    @Prop()
    public name: string;
}

export const GameSchema = SchemaFactory.createForClass(Game);