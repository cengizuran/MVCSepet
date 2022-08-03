using MVCSepetTekrar_1.CustomTools;
using MVCSepetTekrar_1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCSepetTekrar_1.ViewModels
{
    public class ShoppingVM
    {
        public Product Product { get; set; }
        public List<Product> Products { get; set; }
        public Cart Cart { get; set; }


    }
}