namespace GamesStore.Controllers
{
    using System.Collections.Generic;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using GamesStore.Services;

    using Model.ViewModels;
    
    using Constants;

    public class GameController : Controller
    {
        private readonly GameService gameService;

        public GameController(GameService gameService)
        {
            this.gameService = gameService;
        }

        // GET: Game
        public IActionResult Index()
        {
            List<GameViewModel> games = this.gameService.LoadAllGames();

            return View(games);
        }
        
        // GET: Game/Details
        public IActionResult Details(string id)
        {
            GameViewModel game = this.gameService.CheckIfGameIdIsValid(id);

            if (game is null)
            {
                return NotFound();
            }

            return View(game);
        }

        [Authorize(Roles = Role.ADMIN)]
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
                this.gameService.CreateGame(game);

                return RedirectToAction(nameof(Index));
            }

            return View(game);
        }

        [Authorize(Roles = Role.ADMIN)]
        // GET: Game/Edit
        public IActionResult Edit(string id)
        {
            GameViewModel game = this.gameService.CheckIfGameIdIsValid(id);

            return View(game);
        }
        
        // POST: Game/Edit
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

            this.gameService.SaveEditedGame(game);

            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = Role.ADMIN)]
        // GET: Game/Delete
        public IActionResult Delete(string id)
        {
            GameViewModel game = this.gameService.CheckIfGameIdIsValid(id);

            return View(game);
        }
        
        // POST: Game/Delete
        [HttpPost, ActionName(ActionName.DELETE)]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(GameViewModel game)
        {
            this.gameService.DeleteGame(game);

            return RedirectToAction(nameof(Index));
        }

        private bool GameExists(string id)
        {
            bool exists = this.gameService.CheckIfGameExists(id);

            return exists;
        }
    }
}
