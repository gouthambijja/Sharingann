using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpressTrackerLogicLayer.Models
{
    public class BLTransaction
    {
        public string TransactionId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Category { get; set; }
        public DateTime Date { get; set; }
        public double Amount { get; set; }
        public virtual string? UserId { get; set; }
    }
}
