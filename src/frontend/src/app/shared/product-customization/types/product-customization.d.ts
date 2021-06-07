import { Product } from "src/app/api-service/types/product";
import { ProductCustomizationOption } from "./product-customization-option";

export interface ProductCustomization{
    title: string;
    image: string;
    desccription: string;
    sauces: ProductCustomizationOption[];
    sizes: ProductCustomizationOption[];
    toppings: ProductCustomizationOption[];
    cheeses: ProductCustomizationOption[];
    product: Product;
} 
