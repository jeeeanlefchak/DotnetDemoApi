using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Model.Entity
{
    [Table("address")]
    public class Address : EntityBase
    {
        [Column("name")]
        public string Name { get; set; }

    }
}
