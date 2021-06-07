import { Product } from "src/app/api-service/types/product";
import { Size } from "src/app/api-service/types/size";

export interface ProductCard {
    id: string;
    title: string;
    description: string;
    image: string;
    price: string;
    product: Product;
}