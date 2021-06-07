import { Media } from "./media";
import { Size } from "./size";

export interface Product {
    id: string;
    title: string;
    description: string;
    sizes: Size[];
    toppings: Product[];
    sauces: Product[];
    medias: Media[];
}