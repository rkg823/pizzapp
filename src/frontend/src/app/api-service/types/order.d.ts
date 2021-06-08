import { Base } from "./base";
import { OrderItem } from "./order-item";

export interface Order extends Base {
    productId: string;
    items: OrderItem[];
    amount: number;
}