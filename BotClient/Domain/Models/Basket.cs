using System;
using System.Collections.Generic;
using Domain.Models;



namespace Domain.Models
{
    public partial class Basket
    {
        public int UserIdd { get; set; }
        public int ProductId { get; set; }
        public int QuantityOfGoods { get; set; }

        public virtual Product Product { get; set; } = null!;
        public virtual User UserIddNavigation { get; set; } = null!;
    }
}
