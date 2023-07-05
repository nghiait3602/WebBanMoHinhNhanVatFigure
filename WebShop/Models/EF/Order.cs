using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebShop.Models.EF
{
    [Table("tb_Order")]
    public class Order :CommonAbstract
    {
        public Order() { 
        
            this.orderDetails = new HashSet<OrderDetail>();
        }
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string Code { get; set; }
        [Required(ErrorMessage ="Tên khách hàng không được để trống")]
        public string CustomerName { get; set; }
        [Required(ErrorMessage = "Số điện thoại khách hàng không được để trống")]
        public string Phone { get; set; }
        [Required(ErrorMessage = "Địa chỉ email khách hàng không được để trống")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Địa chỉ khách hàng không được để trống")]

        public string Address { get; set; }
        public decimal TotalAmount { get; set; }
        public int Quantity { get; set; }
        public int TypePayment { get; set; }
        public  string userdangnhap { get; set; }   
        public virtual ICollection<OrderDetail> orderDetails { get; set; }




    }
}