﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Web_final.Models;
using Web_final.ViewModels;

namespace Web_final.Controllers
{
    public class CartController : Controller
    {
        private readonly WebsiteNuocHoaContext _context;
        private const string CartSession = "CartSession";
        public CartController(WebsiteNuocHoaContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var menus = await _context.Menus.Where(m => m.Hide == 0).OrderBy(m => m.Order).ToListAsync();
            var cart = HttpContext.Session.GetString(CartSession);
            var list = new List<CartItem>();
            if (!string.IsNullOrEmpty(cart))
            {
                list = JsonConvert.DeserializeObject<List<CartItem>>(cart);
            }
            var cartViewModel = new CartViewModel
            {
                Menus = menus,
                CartItems = list
            };
            return View(cartViewModel);
        }
        public IActionResult AddItem(int ProductId, int Quantity)
        {
            var product = _context.Products.Find(ProductId);
            var cart = HttpContext.Session.GetString(CartSession);
            if (!string.IsNullOrEmpty(cart))
            {
                var list = JsonConvert.DeserializeObject<List<CartItem>>(cart);
                if (list.Exists(x => x.Product.IdPro == ProductId))
                {
                    foreach (var item in list)
                    {
                        if (item.Product.IdPro == ProductId)
                        {
                            item.Quantity += Quantity;
                        }
                    }
                }
                else
                {
                    var item = new CartItem();
                    item.Product = product;
                    item.Quantity = Quantity;
                    list.Add(item);
                }
                HttpContext.Session.SetString(CartSession, JsonConvert.SerializeObject(list));
            }
            else
            {
                var item = new CartItem();
                item.Product = product;
                item.Quantity = Quantity;
                var list = new List<CartItem>();
                list.Add(item);
                HttpContext.Session.SetString(CartSession, JsonConvert.SerializeObject(list));
            }
            return RedirectToAction("Index");
        }
        public IActionResult DeleteAll()
        {
            HttpContext.Session.Remove(CartSession);
            return Json(new
            {
                status = true
            });
        }
        public IActionResult Delete(int id)
        {
            var sessionCart =
           JsonConvert.DeserializeObject<List<CartItem>>(HttpContext.Session.GetString(CartSession));
            sessionCart.RemoveAll(x => x.Product.IdPro == id);
            HttpContext.Session.SetString(CartSession,
           JsonConvert.SerializeObject(sessionCart));
            return Json(new
            {
                status = true
            });
        }
        public IActionResult Update(string cartModel)
        {
            var jsonCart = JsonConvert.DeserializeObject<List<CartItem>>(cartModel);
            var sessionCart =
           JsonConvert.DeserializeObject<List<CartItem>>(HttpContext.Session.GetString(CartSession));
            foreach (var item in sessionCart)
            {
                var jsonItem = jsonCart.SingleOrDefault(x => x.Product.IdPro ==
               item.Product.IdPro);
                if (jsonItem != null)
                {
                    if(jsonItem.Quantity >= 0 && jsonItem.Quantity <= item.Product.Nums)
                        item.Quantity = jsonItem.Quantity;
                    else 
                        item.Quantity = 0;
                }
            }
            HttpContext.Session.SetString(CartSession, JsonConvert.SerializeObject(sessionCart));
            return Json(new
            {
                status = true
            });
        }
        [HttpGet]
        public async Task<IActionResult> Payment(string name)
        {
            var menus = await _context.Menus.Where(m => m.Hide == 0).OrderBy(m => m.Order).ToListAsync();

            var cart = HttpContext.Session.GetString(CartSession);
            var list = new List<CartItem>();
            if (!string.IsNullOrEmpty(cart))
            {
                list = JsonConvert.DeserializeObject<List<CartItem>>(cart);
            }
            var cartViewModel = new CartViewModel
            {
                Menus = menus,
                CartItems = list
            };
            return View(cartViewModel);
        }
        [HttpPost]
        public async Task<IActionResult> Payment()
        {
            var order = new Cart();
            order.Hide = 0;
            order.Datebegin = DateTime.Now;
            var users = new User();
            if (User.Identity.IsAuthenticated)
            {
                string username = User.Identity.Name;
                if (username != null) users = await _context.Users.FirstOrDefaultAsync(m =>
               m.Username == username);
            }
            order.IdUsers = users.IdUsers;
            try
            {
                _context.Carts.Add(order);
                _context.SaveChanges();
                var id = order.IdCart;
                var cart =
               JsonConvert.DeserializeObject<List<CartItem>>(HttpContext.Session.GetString(CartSession));
                foreach (var item in cart)
                {
                    var detail = new CartDetail();
                    detail.IdPro = item.Product.IdPro;
                    detail.IdCart = id;
                    detail.SoldNum = item.Quantity;
                    detail.Hide = 0;
                    _context.CartDetails.Add(detail);
                    _context.SaveChanges();
                }
                foreach (var item in cart)
                {
                    var product = await _context.Products.FirstOrDefaultAsync(m=>m.IdPro == item.Product.IdPro);
                    product.Nums = product.Nums - item.Quantity;
                    product.Order = product.Order + item.Quantity;
                    _context.Products.Update(product);
                    _context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return Redirect("/hoan-thanh");
        }
        public async Task<IActionResult> Success()
        {
            var menus = await _context.Menus.Where(m => m.Hide == 0).OrderBy(m => m.Order).ToListAsync();
            var cartViewModel = new CartViewModel
            {
                Menus = menus,
            };
            return View(cartViewModel);
        }
    }
}
