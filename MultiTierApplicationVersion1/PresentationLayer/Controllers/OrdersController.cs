using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BusinessLayer;
using DataLayer;
using ServiceLayer;

namespace PresentationLayer.Controllers
{
    public class OrdersController : Controller
    {
        private readonly OrderManager orderManager;
        private readonly CustomerManager customerManager;
        public OrdersController(OrderManager orderManager, CustomerManager customerManager)
        {
            this.orderManager = orderManager;
            this.customerManager = customerManager;
        }

        // GET: Orders
        public async Task<IActionResult> Index()
        {
            return View(await orderManager.ReadAllAsync(true));
        }

        // GET: Orders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await orderManager.ReadAsync((int)id, true);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // GET: Orders/Create
        public async Task<IActionResult> Create()
        {
            ViewData["Customers"] = new SelectList(await customerManager.ReadAllAsync(), "Id", "Name");
            return View();
        }

        // POST: Orders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CustomerId")] Order order)
        {
            Customer customer = await customerManager.ReadAsync(order.CustomerId);

            if (customer == null)
            {
                ModelState.AddModelError("Customer", "There are no customers!");
            }

            order.Customer = customer;

            if (ModelState.IsValid)
            {
                Order newOrder = new Order(customer);
                await orderManager.CreateAsync(newOrder);
                return RedirectToAction(nameof(Index));
            }

            ViewData["Customers"] = new SelectList(await customerManager.ReadAllAsync(), "Id", "Name", order.CustomerId);
            return View(order);
        }

        // GET: Orders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await orderManager.ReadAsync((int)id, true);
            if (order == null)
            {
                return NotFound();
            }
            ViewData["Customers"] = new SelectList(await customerManager.ReadAllAsync(), "Id", "Name", order.CustomerId);
            return View(order);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CustomerId")] Order order)
        {
            if (id != order.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await orderManager.UpdateAsync(order, true);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await OrderExists(order.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["Customers"] = new SelectList(await customerManager.ReadAllAsync(), "Id", "Name", order.CustomerId);
            return View(order);
        }

        // GET: Orders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await orderManager.ReadAsync((int)id, true);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await orderManager.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        public async Task<bool> OrderExists(int id)
        {
            return await orderManager.OrderExists(id);
        }
    }
}
