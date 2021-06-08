import { ProductCustomizationOption } from "./product-customization-option";

export interface ProductSelection {
    size: ProductCustomizationOption;
    cheese: ProductCustomizationOption;
    sauces: ProductCustomizationOption[];
    toppings: ProductCustomizationOption[];
    total: number;
}