import { MediaType } from "./media-type";

export interface Media {
    id: string;
    description: string;
    url: string;
    type: MediaType;
    isPrimary: boolean;
}