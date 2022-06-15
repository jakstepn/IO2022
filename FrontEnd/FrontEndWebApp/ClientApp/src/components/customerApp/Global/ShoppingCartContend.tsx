import { Product } from "../classes/Product"

export type ShoppingCart = {
    loaded: boolean;
    Products: Product[];
    Sum: number;
};

const defaultState: ShoppingCart = {
    loaded: true,
    Products: [],
    Sum: 0
};




declare global {
    function RemoveItem(id: string): void;
    function AddItem(Item: Product): void;
    function RemoveAll(): void;
    var Cart: ShoppingCart;
}

globalThis.Cart = defaultState;


const RemoveItemLocal = (id: string) => {

    var cart = globalThis.Cart;
    var ItemToRemove = cart.Products.find(item => item.id == id);
    if (ItemToRemove == undefined)
        return cart;
    else {
        var newProductList = cart.Products.filter(item => item.id != id);
        console.log(ItemToRemove);
        var newSum = cart.Sum - (ItemToRemove.price * ItemToRemove.quantity);
        
        return { loaded: true, Products: newProductList, Sum: newSum };
    }
}

globalThis.RemoveItem = (id: string) => {
    globalThis.Cart = RemoveItemLocal(id) ;
}

globalThis.RemoveAll = () => {
    globalThis.Cart = { loaded: true, Products: [], Sum: 0 };
}

globalThis.AddItem = (Item: Product) => {
    
    var tmp = globalThis.Cart;
    
    tmp.Products.push(Item);
    tmp.Sum = tmp.Sum + Item.price * Item.quantity;
    globalThis.Cart = tmp;
}