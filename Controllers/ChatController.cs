using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using XChat.Data;
using XChat.Models;

namespace XChat.Controllers
{
    public class ChatController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ChatController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Chat
        public async Task<IActionResult> Index()
        {
              return View(await _context.Chats.ToListAsync());
        }

        // GET: Chat/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Chats == null)
            {
                return NotFound();
            }

            var chat = await _context.Chats
                .FirstOrDefaultAsync(m => m.ChatID == id);
            if (chat == null)
            {
                return NotFound();
            }

            return View(chat);
        }

        // GET: Chat/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Chat/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ChatID,SenderID,RecipientID,Message,SendDate,Read")] Chat chat)
        {
            //if (ModelState.IsValid)
            //{
                chat.Read = "NO";
                _context.Add(chat);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Create));
            //}
           
        }

        // GET: Chat/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Chats == null)
            {
                return NotFound();
            }

            var chat = await _context.Chats.FindAsync(id);
            if (chat == null)
            {
                return NotFound();
            }
            return View(chat);
        }

        // POST: Chat/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ChatID,SenderID,RecipientID,Message,SendDate,Read")] Chat chat)
        {
            if (id != chat.ChatID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(chat);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ChatExists(chat.ChatID))
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
            return View(chat);
        }

        // GET: Chat/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Chats == null)
            {
                return NotFound();
            }

            var chat = await _context.Chats
                .FirstOrDefaultAsync(m => m.ChatID == id);
            if (chat == null)
            {
                return NotFound();
            }

            return View(chat);
        }

        // POST: Chat/Delete/5
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var url = Request.RouteValues;
            if (_context.Chats == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Chats'  is null.");
            }
            var chat = await _context.Chats.FindAsync(id);
            if (chat != null)
            {
                _context.Chats.Remove(chat);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ChatExists(int id)
        {
          return _context.Chats.Any(e => e.ChatID == id);
        }
    }
}
