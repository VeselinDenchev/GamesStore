using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Data;
using Model;
using GamesStore.Services;
using Model.ViewModels;

namespace GamesStore.Controllers
{
    public class GameController : Controller
    {
        private readonly GameService _service;

        public GameController(GameService service)
        {
            _service = service;
        }

        // GET: Game
        public IActionResult Index()
        {
            List<GameViewModel> games = _service.LoadAllGames();

            return View(games);
        }
        
        // GET: Game/Details
        public IActionResult Details(string id)
        {
            GameViewModel game = _service.CheckIfGameIdIsValid(id);

            if (game is null)
            {
                return NotFound();
            }

            return View(game);
        }

        // GET: Game/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Game/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(GameViewModel game)
        {
            if (ModelState.IsValid)
            {
                _service.CreateGame(game);

                return RedirectToAction(nameof(Index));
            }

            return View(game);
        }
        
        // GET: Game/Edit
        public IActionResult Edit(string id)
        {
            GameViewModel game = _service.CheckIfGameIdIsValid(id);

            return View(game);
        }
        
        // POST: Game/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(GameViewModel game)
        {
            if (game is null)
            {
                return NotFound();
            }

            _service.SaveEditedGame(game);

            return RedirectToAction(nameof(Index));

            //return View(game);
        }
        
        // GET: Game/Delete
        public IActionResult Delete(string id)
        {
            GameViewModel game = _service.CheckIfGameIdIsValid(id);

            return View(game);
        }
        
        // POST: Game/Delete/
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(GameViewModel game)
        {
            _service.DeleteGame(game);

            return RedirectToAction(nameof(Index));
        }

        private bool GameExists(string id)
        {
            bool exists = _service.CheckIfGameExists(id);

            return exists;
        }
    }
}
