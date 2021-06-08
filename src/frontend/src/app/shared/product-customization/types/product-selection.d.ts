import { ProductCustomizationOption } from "./product-customization-option";

export interface ProductSelection {
    product: ProductCustomizationOption;
    size: ProductCustomizationOption;
    cheese: ProductCustomizationOption;
    sauces: ProductCustomizationOption[];
    toppings: ProductCustomizationOption[];
    total: number;
}