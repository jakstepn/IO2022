import { OrderItem } from "./OrderItem";
import { OrderStatus } from "./OrderStatus";

export interface OrderJson {
    orderId:        string;
    orderItems:     OrderItem[];
    creationDate:   string;
    deliveryDate:   string;
    clientAddress:  {
        street:     string;
        city:       string;
        zipCode:    string;
    };
    additionalInfo: string;
    orderStatus:    string;
}