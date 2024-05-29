﻿using System.ComponentModel.DataAnnotations;

namespace Mango.services.CouponAPI.Models
{
    public class Coupon
    {
        [Key]
        public int CouponId { get; set; }
        [Required]
        public string CouponCode { get; set; }=string.Empty;
        [Required]
        public double DiscountAmount { get; set; }
        [Required]
        public int  MinAmount { get; set; }

    }
}
