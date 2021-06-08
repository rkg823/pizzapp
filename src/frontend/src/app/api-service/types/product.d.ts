import { Base } from "./base";
import { Media } from "./media";
import { Size } from "./size";

export interface Product extends Base {
    sizes: Size[];
    toppings: Product[];
    sauces: Product[];
    cheeses: Product[];
    medias: Media[];
}