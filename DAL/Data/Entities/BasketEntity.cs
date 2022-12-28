﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DAL.Data.Entities
{
    [Table("tblBaskets")]
    public class BasketEntity
    {
        public short Count { get; set; }
        [ForeignKey("ProductId")]
        public int ProductId { get; set; }
        [ForeignKey("UserId")]
        public short UserId { get; set; }

        public virtual ProductEntity Product { get; set; }
        public virtual UserEntity User { get; set; }
    }
}
