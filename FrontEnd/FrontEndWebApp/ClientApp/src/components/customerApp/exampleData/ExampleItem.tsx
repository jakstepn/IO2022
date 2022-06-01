import { Delivery } from "../classes/Delivery";
import { Product } from "../classes/Product";
import ProductsFromJSON from "./exampleProducts.json"
import DeliveryInfoFromJSON from "./exampleDeliveryInfos.json"

export const exampleProducts: Product[] = ProductsFromJSON;
export const exampleDeliveryInfos: Delivery[] = DeliveryInfoFromJSON;