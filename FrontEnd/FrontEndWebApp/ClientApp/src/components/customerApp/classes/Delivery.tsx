import { string } from "yup";
import { Product } from "./Product";
import { Shop } from "./Shop";

export interface Delivery {
    id: string;
    status: string;
    orderTime: string;
    delivertTime: string;
    products: Product[];
    price: number;
}
