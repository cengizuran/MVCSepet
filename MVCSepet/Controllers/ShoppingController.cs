using MVCSepetTekrar_1.CustomTools;
using MVCSepetTekrar_1.DesignPatterns.SingletonPattern;
using MVCSepetTekrar_1.Models;
using MVCSepetTekrar_1.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCSepetTekrar_1.Controllers
{
    //This project uses a database from a virtual company named Northwind (Database is integrated to the project with Database First), and a cart system is simulated where the user can add and remove products to their cart. Singleton pattern is used to ensure database is only instanced once. FYI, payment view is to be integrated in a future date, so that view does not work yet.
    public class ShoppingController : Controller
    {
        NorthwindEntities _db;

        public ShoppingController()
        {
            _db = DBTool.DBInstance;
        }
        
        public ActionResult ListProducts()
        {
            ShoppingVM svm = new ShoppingVM
            {
                Products = _db.Products.ToList()
            };

            return View(svm);
        }

        public ActionResult AddToCart(int id)
        {
            Cart c = Session["scart"] == null ? new Cart() : Session["scart"] as Cart;

            Product eklenecekUrun = _db.Products.Find(id);

            CartItem ci = new CartItem();
            ci.ProductName = eklenecekUrun.ProductName;
            ci.ID = eklenecekUrun.ProductID;
            ci.UnitPrice = eklenecekUrun.UnitPrice;
            c.SepeteEkle(ci);

            Session["scart"] = c;
            TempData["mesaj"] = $"{ci.ProductName} sepete eklendi";
            return RedirectToAction("ListProducts");

        }

        public ActionResult CartPage()
        {
            if (Session["scart"] != null)
            {
                Cart c = Session["scart"] as Cart;
                ShoppingVM svm = new ShoppingVM
                {
                    Cart = c,
                };
                return View(svm);

            }

            ViewBag.SepetBos = "Sepetinizde ürün bulunmamaktadır";
            return View();
        }

        public ActionResult RemoveFromCart(int id)
        {
            if (Session["scart"] != null)
            {
                Cart c = Session["scart"] as Cart;
                c.SepettenSil(id);
                if (c.Sepetim.Count == 0) Session.Remove("scart");
                return RedirectToAction("CartPage");
            }
            return RedirectToAction("ListProducts");
        }
    }
}