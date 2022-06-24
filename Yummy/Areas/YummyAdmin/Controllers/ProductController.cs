using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Yummy.DAL;
using Yummy.Helpers;
using Yummy.Models;

namespace Yummy.Areas.YummyAdmin.Controllers
{
    [Area("YummyAdmin")]
    public class ProductController : Controller
    {
        private AppDbContext _context { get; }
        private IWebHostEnvironment _env { get; }
        public ProductController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public IActionResult Index()
        {
            return View(_context.Products);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Product product)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            if (!product.Photo.CheckFileSize(200))
            {
                ModelState.AddModelError("Photo", "Images max size>");
            }
            if (!product.Photo.CheckFileType("image/"))
            {
                ModelState.AddModelError("Photo", "type of file must be image");
                return View();
            }
            product.Url = await product.Photo.SaveFileAsync(_env.WebRootPath, "img");
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
            return Redirect(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            var pro = _context.Products.Find(id);
            if (pro == null)
            {
                return NotFound();
            }
            var path = Helper.GetPath(_env.WebRootPath, "img", pro.Url);
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }
            _context.Products.Remove(pro);
            await _context.SaveChangesAsync();
            return Redirect(nameof(Index));
        }
        public async Task<IActionResult> Update(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            Product pro = _context.Products.Find(id);
            if (pro == null)
            {
                return NotFound();
            }
            return View(pro);
        }
        public async Task<IActionResult> Update(Product pro, int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            Product prodb = _context.Products.Find(id);
            if (prodb == null)
            {
                return NotFound();
            }
            pro.Url = await pro.Photo.SaveFileAsync(_env.WebRootPath, "img");

            var pathDb = Helper.GetPath(_env.WebRootPath, "img", prodb.Url);
            if (System.IO.File.Exists(pathDb))
            {
                System.IO.File.Delete(pathDb);
            }
            prodb.Url = pro.Url;
            await _context.SaveChangesAsync();
            return Redirect(nameof(Index));
        }
    }
}

