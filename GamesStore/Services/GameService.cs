using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Data;

using Model;
using Model.ViewModels;

namespace GamesStore.Services
{
    public class GameService
    {
        private readonly GamesStoreDbContext _dbContext;

        public GameService(GamesStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<GameViewModel> LoadAllGames()
        {
            List<GameViewModel> games = _dbContext.Games.Select(x => new GameViewModel()
            {
                Id = x.Id,
                Name = x.Name,
                Platform = x.Platform,
                ImgUrl = x.ImgUrl,
                Developer = x.Developer,
                AgeRating = x.AgeRating,
                Description = x.Description,
                YearOfRelease = x.YearOfRelease,
                Price = x.Price,
                Quantity = x.Quantity
            }).ToList();

            return games;
        }

        public void CreateGame(GameViewModel game)
        {
            Game newGame = new Game();
            newGame.Name = game.Name;
            newGame.Platform = game.Platform;
            newGame.ImgUrl = game.ImgUrl;
            newGame.Developer = game.Developer;
            newGame.AgeRating = game.AgeRating;
            newGame.Description = game.Description;
            newGame.YearOfRelease = game.YearOfRelease;
            newGame.Price = game.Price;
            newGame.Quantity = game.Quantity;

            _dbContext.Games.Add(newGame);
            _dbContext.SaveChanges();
        }
    }
}
