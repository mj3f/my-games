import { Prop, Schema, SchemaFactory } from "@nestjs/mongoose";

@Schema()
export class GameNote {
    @Prop({ required: true })
    public created_at: number;

    @Prop({ required: true })
    public content: string;
}

export const GameNoteSchema = SchemaFactory.createForClass(GameNote);