import { Product }          from "../classes/Product"
import { OrderJson }        from "../classes/OrderJson";

import productsFromJson     from "./exampleProducts.json"
import productFromJson      from "./exampleProduct.json" 
import ordersFromJson       from "./exampleOrders.json"
import orderFromJson        from "./exampleOrder.json"


export const exampleProducts:   Product[]   =   productsFromJson;
export const exampleProduct:    Product     =   productFromJson;
export const exampleOrders:     OrderJson[] =   ordersFromJson;
export const exampleOrder:      OrderJson   =   orderFromJson;
