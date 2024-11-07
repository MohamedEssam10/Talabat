using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities.Order;

namespace Talabat.Core.Specification.Contracts.Order.Specifications
{
    public class OrderSpecifications : BaseSpecifications<Entities.Order.Order>
    {
        public OrderSpecifications(string BuyerEmail) : base(O => O.BuyerEmail == BuyerEmail)   
        {
            OrderByDescending = O => O.OrderDate;
            Include.Add(O => O.DeliveryMethod);
            Include.Add(O => O.Items);
        }

        public OrderSpecifications(string BuyerEmail, int Id) : base(O => O.BuyerEmail == BuyerEmail && O.Id == Id)
        {
            Include.Add(O => O.DeliveryMethod);
            Include.Add(O => O.Items);
        }

    }
}
