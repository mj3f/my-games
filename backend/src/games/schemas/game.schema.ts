import { Prop, Schema, SchemaFactory } from "@nestjs/mongoose";
import { GameNote, GameNoteSchema } from "./game-note.schema";

export type GameStatus = 
    | 'BACKLOG'
    | 'IN_PROGRESS'
    | 'WISHLIST'

@Schema()
export class Game {
    @Prop({ required: true })
    public igdbId: string;

    @Prop()
    public name: string;

    @Prop({ type: String })
    public status: GameStatus;

    @Prop([GameNoteSchema])
    public notes: GameNote[];
}

export const GameSchema = SchemaFactory.createForClass(Game);