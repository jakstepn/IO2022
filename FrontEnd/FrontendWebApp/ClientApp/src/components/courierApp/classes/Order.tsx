import { OrderItem } from "./OrderItem";
import { OrderStatus } from "./OrderStatus";

export interface Order {
    orderId:        string;
    orderItems:     OrderItem[];
    creationDate:   Date;
    deliveryDate:   Date;
    clientAddress:  {
        street:     string;
        city:       string;
        zipCode:    string;
    };
    additionalInfo: string;
    orderStatus:    OrderStatus;
}