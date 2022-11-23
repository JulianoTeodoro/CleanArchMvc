﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchMvc.Application.Products.Commands
{
    public class ProductCreateCommand : ProductCommand
    {
        public int Id { get; set; }
        public ProductCreateCommand(int id)
        {
            Id = id;
        }
    }
}
