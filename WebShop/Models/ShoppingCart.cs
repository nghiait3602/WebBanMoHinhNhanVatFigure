using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebShop.Models
{
    public class ShoppingCart
    {
        public List<ShoppingCartItem> Items { get; set; }
        public ShoppingCart()
        {
            this.Items = new List<ShoppingCartItem>();

        }
        public void AddToCart(ShoppingCartItem item,int Quantity)
        {
            var checkExits = Items.FirstOrDefault(x => x.ProductId == item.ProductId);
            if(checkExits != null)
            {
                checkExits.Quantity += Quantity; 
                checkExits.TotalPrice =checkExits.Price * checkExits.Quantity;
            }
            else
            {
                Items.Add(item);
            }
        }
        public void Remove(int id)
        {
            var checkExit = Items.SingleOrDefault(x =>x.ProductId == id);
            if(checkExit != null) { 
                Items.Remove(checkExit);
            }
        }
        public void UpdateQuantity(int id, int Quantity)
        {
            var checkExit = Items.SingleOrDefault(x => x.ProductId == id);
            if (checkExit != null)
            {
                checkExit.Quantity = Quantity;
                checkExit.TotalPrice =checkExit.Price * checkExit.Quantity;
            }
        }
        public decimal GetTotal()
        {
            return Items.Sum(x => x.TotalPrice);  
        }
        public decimal GetQuantity()
        {
            return Items.Sum(x => x.Quantity);
        }
        public void ClearCart()
        {
            Items.Clear();
        }
    }
    public class ShoppingCartItem
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string Alias { get; set; }
        public string CategoryName { get; set; }
        public string ProductImage { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal TotalPrice { get; set; }

    }
}